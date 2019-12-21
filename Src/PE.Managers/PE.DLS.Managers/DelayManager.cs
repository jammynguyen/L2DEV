using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Enums;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.Delay;
using SMF.Core.DC;
using SMF.Module.Core;
using PE.DbEntity;
using SMF.Module.Parameter;
using PE.DbEntity.Models;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using PE.Common;
using PE.Interfaces.Interfaces.SendOffice;
using PE.DLS.Handlers;
using PE.Interfaces.Managers;

namespace PE.DLS.Managers
{
  public class DelayManager : IDelayManager
  {
    private readonly IDelayManagerSendOffice _sendOffice;
    private readonly int _maxMaterialInterval;
    private DLSDelay _currentDelay = new DLSDelay();
    private DelayHandler _delayHandler;


    public DelayManager(IDelayManagerSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _delayHandler = new DelayHandler();

      _maxMaterialInterval = ParameterController.GetParameter("DLS_MaxGapTime").ValueInt.GetValueOrDefault();
      InitCurrentDelayWithLastNotClosedDelay();
    }
  

    public virtual async Task<DataContractBase> ProcessHeadEnterAsync(DCDelayEvent dcDelayEvent)
    {
      DataContractBase result = new DataContractBase();

      await EndCurrentDelayAsync();
           
      return result;
    }

    public virtual async Task<DataContractBase> ProcessTailLeavesAsync(DCDelayEvent dcDelayEvent)
    {
      DataContractBase result = new DataContractBase();

      StartNewDelay();

      await Task.FromResult(0);
      return result;
    }
    public virtual async Task<bool> SaveDeleyToDbAsync()
    {
      bool returnvalue = false;
      try
      {
        using (PEContext ctx = new PEContext())
        {
          long delDelayCatalogueId = await _delayHandler.GetDefaultDefectCatalogueId(ctx);
          if (_currentDelay.DelayId==0)
          {
            _currentDelay.DelayStart = _currentDelay.DelayStart.AddSeconds(_maxMaterialInterval);
            _currentDelay.CreatedTs = DateTime.Now;
            _currentDelay.LastUpdateTs = DateTime.Now;
            _currentDelay.FKDelayCatalogueId = delDelayCatalogueId;
            ctx.DLSDelays.Add(_currentDelay);
            await ctx.SaveChangesAsync();
            returnvalue = true;
            NotificationController.Info("New delay has been saved to DB");
          }
          else
          {
            DLSDelay delay = ctx.DLSDelays.Where(x => x.DelayId == _currentDelay.DelayId).Single();
            delay.DelayEnd = _currentDelay.DelayEnd;
            delay.LastUpdateTs = DateTime.Now;
            _currentDelay.LastUpdateTs = DateTime.Now;
            await ctx.SaveChangesAsync();
            returnvalue = true;
            NotificationController.Info("Delay with id: {0} has been updated. Delay started at: {1}, Ended at {2}", _currentDelay.DelayId, _currentDelay.DelayStart, _currentDelay.DelayEnd);
          }         
        }    

      }
      catch(Exception)
      {
        NotificationController.RegisterAlarm(Defs.AC_DelaySave, "Error during delay save operation");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.AC_DelaySave, "Error during delay save operation");
      }
      if (_currentDelay.DelayEnd != null)
      {
        //_currentDelay = new DLSDelay();
        _currentDelay.DelayStart = DateTime.Now;
        _currentDelay.DelayEnd = null;
        _currentDelay.DelayId = 0;
      }
      return returnvalue;
    }
    public async Task<bool> UpdateCurrentDelayStatusAsync()
    {
      bool result = false;
      NotificationController.Info("UpdateCurrentDelayStatus method has been called");
      if (_currentDelay.DelayStart != DateTime.MinValue)
      {
        NotificationController.Info("UpdateCurrentDelayStatus: Current Delay duration: {0}, max allowed gap between materials: {1}, current delayId: {2}", (int)(DateTime.Now - _currentDelay.DelayStart).TotalSeconds, _maxMaterialInterval, _currentDelay.DelayId);
        if ((DateTime.Now - _currentDelay.DelayStart).TotalSeconds > _maxMaterialInterval && _currentDelay.DelayId==0)
        {
          _currentDelay.DelayEnd = null;
          await SaveDeleyToDbAsync();

        }
      }
      else
      {
        _currentDelay.DelayStart = DateTime.Now;
      }
      return result;
    }
    public virtual async Task<DataContractBase> MergeDelaysAsync(DCDelaysToMerge delaysToMerge)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          DLSDelay firstDelay = ctx.DLSDelays.Where(x => x.DelayId == delaysToMerge.firstDelayId).Single();
          DLSDelay secondDelay = ctx.DLSDelays.Where(x => x.DelayId == delaysToMerge.secondDelayId).Single();

          if (firstDelay.DelayEnd == secondDelay.DelayStart || firstDelay.DelayStart == secondDelay.DelayEnd)
          {
            DLSDelay mergedDelay = new DLSDelay()
            {
              CreatedTs = DateTime.Now,
              LastUpdateTs = DateTime.Now,
              DelayStart = firstDelay.DelayStart > secondDelay.DelayStart ? secondDelay.DelayStart : firstDelay.DelayStart,
              DelayEnd = firstDelay.DelayEnd > secondDelay.DelayEnd ? firstDelay.DelayEnd : secondDelay.DelayEnd
            };
            ctx.DLSDelays.Add(_currentDelay);
            ctx.DLSDelays.Remove(firstDelay);
            ctx.DLSDelays.Remove(secondDelay);
            await ctx.SaveChangesAsync();
          }
          else
          {

          }
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.AC_DelayMerge, "Error during delays merge");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.AC_DelayMerge, "Error during delays merge");
      }
      return result;
    }
    public virtual async Task<DataContractBase> DivideDelayAsync(DCDelayToDivide delayToDivide)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          DLSDelay oldDelay = _delayHandler.GetDelayById(ctx, delayToDivide.DelayId);

          if ((oldDelay.DelayEnd - oldDelay.DelayStart).Value.TotalSeconds > delayToDivide.DurationOfNewDelay)
          {
            DLSDelay newDelay = new DLSDelay()
            {
              CreatedTs = DateTime.Now,
              LastUpdateTs = DateTime.Now,
              FKDelayCatalogueId = oldDelay.FKDelayCatalogueId,
              DelayStart = oldDelay.DelayStart,
              DelayEnd = oldDelay.DelayStart.AddSeconds(delayToDivide.DurationOfNewDelay),
            };
            oldDelay.DelayStart = oldDelay.DelayStart.AddSeconds(delayToDivide.DurationOfNewDelay);
            ctx.DLSDelays.Add(newDelay);
            await ctx.SaveChangesAsync();
          }
        }
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.AC_DelayDivision, "Error during delays division");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.AC_DelayDivision, "Error during delays division");
      }
      return result;
    }
    public virtual async Task<DataContractBase> UpdateDelayAsync(DCDelay dc)
    {
      DataContractBase result = new DataContractBase();

      if(dc.DelayStart>=dc.DelayEnd)
      {
        NotificationController.RegisterAlarm(Defs.AC_DelayInvalidDates, "Error while updating Delay. End date is not valid");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.AC_DelayInvalidDates , "Error while updating Delay. End date is not valid ");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {
          DLSDelay delay = _delayHandler.GetDelayById(ctx, dc.Id);

          delay = _delayHandler.UpdateDelay(ctx, delay, dc);

          await ctx.SaveChangesAsync();

          await ModuleController.HmiRefresh(HMIRefreshKeys.Delay);
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.AC_DelayUpadate, "Error while updating Delay ");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.AC_DelayUpadate, "Error while updating Delay ");

      }

      return result;
    }


    private void StartNewDelay()
    {
      NotificationController.Info("Tail Leaves signal received");
      if ((_currentDelay.DelayStart == DateTime.MinValue) || (_currentDelay.DelayId == 0))
        _currentDelay.DelayStart = DateTime.Now;
    }
    private async Task<bool> EndCurrentDelayAsync()
    {
      bool result = false;
      NotificationController.Info("Head Enter signal received");
      if (_currentDelay.DelayStart != DateTime.MinValue)
      {
        if ((DateTime.Now - _currentDelay.DelayStart).TotalSeconds > _maxMaterialInterval)
        {
          _currentDelay.DelayEnd = DateTime.Now;
          await SaveDeleyToDbAsync();
          result = true;
        }
        else
        {
          _currentDelay.DelayStart = DateTime.Now;
        //  _currentDelay.DelayEnd = null;
        //  _currentDelay.DelayId = 0;
        //  result = true;
        }
      }
      return result;
    }
    private async void InitCurrentDelayWithLastNotClosedDelay()
    {
      try
      {
        using (PEContext ctx = new PEContext())
        {
          DLSDelay lastDelay =  await _delayHandler.GetLastDelay(ctx);
          if (lastDelay.DelayEnd == null)
          {
            _currentDelay.DelayStart = lastDelay.DelayStart;
            _currentDelay.DelayId = lastDelay.DelayId;
          }
        }
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.AC_DelayModuleInit, "Error during module initialization");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.AC_DelayModuleInit, "Error during module initialization");
      }
    }

  }
}
