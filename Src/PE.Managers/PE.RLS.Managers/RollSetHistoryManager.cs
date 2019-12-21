using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.Common;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.RollShop;
using PE.Interfaces.Interfaces.Managers;
using PE.Interfaces.Interfaces.SendOffice;
using PE.RLS.Handlers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;

namespace PE.RLS.Managers
{
  public class RollSetHistoryManager : IRollSetHistoryManager
  {
    private readonly IRollSetHistoryManagerSendOffice _sendOffice;

    private readonly RollSetHistoryHandler _rollSetHistoryHandler;
    private readonly RollHandler _rollHandler;
    private readonly RollSetHandler _rollSetHandler;
    private readonly RollGroovesHistoryHandler _rollGroovesHistoryHandler;

    //private readonly RollShopManager _rollShopManager;



    public RollSetHistoryManager(IRollSetHistoryManagerSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _rollSetHistoryHandler = new RollSetHistoryHandler();
      _rollHandler = new RollHandler();
      _rollSetHandler = new RollSetHandler();
      _rollGroovesHistoryHandler = new RollGroovesHistoryHandler();

      //_rollShopManager = new RollShopManager();
    }

    #region basic operations
    public virtual async Task<DataContractBase> InsertRollSetHistoryAsync(DCRollSetHistoryData dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRollSetHistory rollSetHistory = _rollSetHistoryHandler.GetRollSetHistoryFromRollSetId(ctx, dc.RollSetId);
          if (rollSetHistory == null)
          {
            rollSetHistory = new RLSRollSetHistory();
            rollSetHistory.CreatedTs = DateTime.Now;
            rollSetHistory.Created = DateTime.Now;
            rollSetHistory.LastUpdateTs = DateTime.Now;
            rollSetHistory.FKRollSetId = (long)dc.RollSetId;
            rollSetHistory.Dismounted = dc.Dismounted;
            rollSetHistory.Mounted = dc.Mounted;
            rollSetHistory.PositionInCassette = dc.PositionInCassette;
            rollSetHistory.Status = RollSetHistoryStatus.Actual;
            rollSetHistory.FKCassetteId = dc.CassetteId;

            ctx.RLSRollSetHistories.Add(rollSetHistory);

            await ctx.SaveChangesAsync();
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
          else
          {
            NotificationController.RegisterAlarm(Defs.RollSetHistoryAlreadyExist, "Error while inserting RollSetHistory to DB. RollSetHistory with specified rollset already exists in DB");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistoryAlreadyExist, "Error while inserting RollSetHistory to DB. RollSetHistory with specified rollset already exists in DB");
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while inserting RollSetHistory to DB.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while inserting RollSetHistory to DB.");
      }
      return result;
    }
    public virtual async Task<RLSRollSetHistory> CreateRollSetHistoryAsync(DCRollSetHistoryData dc)
    {
      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRollSetHistory rsh = new RLSRollSetHistory();
          rsh.Created = DateTime.Now;
          rsh.CreatedTs = DateTime.Now;
          rsh.LastUpdateTs = DateTime.Now;
          rsh.FKRollSetId = (long)dc.RollSetId;
          rsh.Dismounted = dc.Dismounted;
          rsh.Mounted = dc.Mounted;
          rsh.PositionInCassette = dc.PositionInCassette;
          rsh.Status = RollSetHistoryStatus.Planned;
          rsh.FKCassetteId = dc.CassetteId;


          ctx.RLSRollSetHistories.Add(rsh);

          await ctx.SaveChangesAsync();
          await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          return rsh;
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while inserting RollSetHistory to DB.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while inserting RollSetHistory to DB.");
      }
    }
    public virtual async Task<DataContractBase> UpdateRollSetHistoryAsync(DCRollSetHistoryData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollSetHistory. RollsetHistory is empty");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollSetHistory. RollSetHistory is empty");
      }
      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSRollSetHistory rollSetHistory = _rollSetHistoryHandler.GetRollSetHistoryFromId(ctx, dc.Id);
          if (rollSetHistory != null)
          {
            if (dc.CassetteId != null) rollSetHistory.FKCassetteId = dc.CassetteId;
            rollSetHistory.LastUpdateTs = DateTime.Now;
            if (dc.InterCassetteId != null) rollSetHistory.FKRollSetId = (long)dc.RollSetId;
            if (dc.Dismounted != null) rollSetHistory.Dismounted = dc.Dismounted;
            if (dc.Mounted != null) rollSetHistory.Mounted = dc.Mounted;
            if (dc.PositionInCassette != null) rollSetHistory.PositionInCassette = dc.PositionInCassette;
            rollSetHistory.Status = dc.Status;
            if (dc.AccWeightLimit != null) rollSetHistory.AccWeightLimit = dc.AccWeightLimit ?? 0.0;

            await ctx.SaveChangesAsync();
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
          else
          {
            NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollSetHistory. RollSetHistory does not exist in DB");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollSetHistory.RollSetHistory does not exist in DB");
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while inserting RollSetHistory to DB.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while inserting RollSetHistory to DB.");
      }
      return result;
    }

    public virtual async Task<DataContractBase> UpdateRollSetHistoryAsync(RLSRollSetHistory rsh)
    {
      DataContractBase result = new DataContractBase();

      if (rsh == null)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollSetHistory. RollsetHistory is empty");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollSetHistory. RollSetHistory is empty");
      }
      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSRollSetHistory rollSetHistory = _rollSetHistoryHandler.GetRollSetHistoryFromId(ctx, rsh.FKRollSetId);
          if (rollSetHistory != null)
          {
            if (rsh.FKCassetteId != null) rollSetHistory.FKCassetteId = rsh.FKCassetteId;
            rollSetHistory.LastUpdateTs = DateTime.Now;
             rollSetHistory.FKRollSetId = rsh.FKRollSetId;
            if (rsh.Dismounted != null) rollSetHistory.Dismounted = rsh.Dismounted;
            if (rsh.Mounted != null) rollSetHistory.Mounted = rsh.Mounted;
            if (rsh.PositionInCassette != null) rollSetHistory.PositionInCassette = rsh.PositionInCassette;
            rollSetHistory.Status = rsh.Status;
            if (rsh.AccWeightLimit != null) rollSetHistory.AccWeightLimit = rsh.AccWeightLimit ?? 0.0;

            await ctx.SaveChangesAsync();
          }
          else
          {
            NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollSetHistory. RollSetHistory does not exist in DB");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollSetHistory.RollSetHistory does not exist in DB");
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while inserting RollSetHistory to DB.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while inserting RollSetHistory to DB.");
      }
      return result;
    }
    public virtual async Task<DataContractBase> UpdateGroovesToRollSetAsync(DCRollSetGrooveSetup dc)
    {
      DataContractBase result = new DataContractBase();
      //PEUnitOfWork uow = new PEUnitOfWork();
      //PEContext ctx = new PEContext();

      //VALIDATION
      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Empty DataContract");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Empty DataContract.");
      }
      if (dc.Action != GrindingTurningAction.Plan && dc.Action != GrindingTurningAction.Confirm && dc.Action != GrindingTurningAction.Cancel)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Invalid ActionRequest");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Invalid ActionRequest.");
      }
      //END OF VALIDATION

      if (dc.Id != null)
      {
        try
        {
          using (PEContext ctx = new PEContext())
          {
            //Get rollset
            RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, dc.Id);
            if (rollSet == null)
            {
              NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollGrooves. RollSet does not exist");
              NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
              //throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollGrooves. RollSet does not exist.");
              throw new Exception();
            }
            //Rolls in rollset check
            RollSetType rsType = RollSetType.Undefined;
            rsType = (RollSetType)rollSet.RollSetType;
            switch (rsType)
            {

              case RollSetType.TwoActiveRollsIM:
              case RollSetType.TwoActiveRollsRM:
                {
                  if (rollSet.FKBottomRollId == null || rollSet.FKBottomRollId == null)
                  {
                    NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Missing or invalid bottom and upper rolls.");
                    NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                    //throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Missing or invalid bottom and upper rolls.");
                    throw new Exception();
                  }
                  break;
                }
              default:
                {
                  NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Missing or invalid bottom and upper rolls.");
                  NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                  //throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Missing or invalid bottom and upper rolls.");
                  throw new Exception();
                }
            }

            //Get upper roll
            RLSRoll upperRollInRollSet = _rollHandler.GetRollFromId(ctx, rollSet.FKUpperRollId);
            if (upperRollInRollSet == null)
            {
              NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Upper roll does not exist.");
              NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
              //throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollGrooves.  Upper roll does not exist.");
              throw new Exception();
            }

            //Get bottom roll
            RLSRoll bottomRollInRollSet = _rollHandler.GetRollFromId(ctx, rollSet.FKBottomRollId);
            if (bottomRollInRollSet == null)
            {
              NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Bottom roll does not exist.");
              NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
              //throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Bottom roll does not exist.");
              throw new Exception();
            }

            switch (dc.Action)
            {
              case GrindingTurningAction.Plan:
                {
                  if ((rollSet.Status == RollSetStatus.Undefined) || (rollSet.Status == RollSetStatus.NotAvailable) || (rollSet.Status == RollSetStatus.Empty))
                  {
                    NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Current Roll state does not allow for this operation.");
                    NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                    //throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Current Roll state does not allow for this operation.");
                    throw new Exception();
                  }

                  //Create new rollset history and initialize grooves with status planned (leave actual rollsetHistory unchanged!)
                  //change rollset status to Turning

                  //TUDU
                  //RollSetDataHandler.UpdateRollSetStatus(rollSet.RollSetId, RollSetStatus.Turning);
                  //await UpdateRollSetStatusAsync(rollSet.RollSetId, RollSetStatus.Turning);
                  rollSet.Status = RollSetStatus.Turning;
                  //return result;


                  //create new RollSetHistory entry
                  DCRollSetHistoryData dcrsh = new DCRollSetHistoryData();
                  dcrsh.RollSetId = rollSet.RollSetId;
                  RLSRollSetHistory rsh = await CreateRollSetHistoryAsync(dcrsh);
                  //iterate over each supplied roll and then over each groove setup
                  if (dc.RollDataInfoList != null)
                  {
                    foreach (SingleRollDataInfo ri in dc.RollDataInfoList)
                    {
                      if (ri.RollId < 1)
                        continue;
                      foreach (SingleGrooveSetup lr in dc.GrooveSetupList)
                      {
                        DCRollGroovesHistoryData dcGrhHist = new DCRollGroovesHistoryData();
                        dcGrhHist.AccBilletCnt = 0;
                        dcGrhHist.AccWeight = 0;
                        dcGrhHist.ActDiameter = ri.Diameter;
                        dcGrhHist.GrooveNumber = lr.GrooveIdx;
                        dcGrhHist.RollId = ri.RollId;
                        dcGrhHist.RollSetHistoryId = rsh.RollSetHistoryId;
                        dcGrhHist.GrooveTemplateId = lr.GrooveTemplate;
                        dcGrhHist.Status = (short)RollGrooveStatus.PlannedAndTurning;

                        RLSRollGroovesHistory rollGroovesHistory = _rollGroovesHistoryHandler.CreateRollGrooveHistory(ctx, dcGrhHist);
                        ctx.RLSRollGroovesHistories.Add(rollGroovesHistory);
                        
                        //TUDU
                       // if (!RollGroovesHistoryDataHandler.InsertRollGroovesHistory(dcGrhHist))
                        //  return false;
                      }
                      //NotificationController.Logger.Info("Grooves (in planned state) has been created for RollID {0} ", ri.RollId);
                    }
                    await ctx.SaveChangesAsync();
                    await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
                  }
                  break;
                  //return true;
                }
              case GrindingTurningAction.Confirm:
                {
                  //find all RollSetHistory record and associated grooves and change their statuses to old
                  //if (MakeActualRollSetHistoryEntryOld(rollSet.RollSetId) != 0)
                  //  return false;

                  await MakeActualRollSetHistoryEntryOldAsync(rollSet.RollSetId);

                  //change status of actually planned rollset/roll configuration to actual
                  //if (MakePlannedRollSetHistoryEntryActual(rollSet.RollSetId, dc.RollDataInfoList) != 0)
                  //  return false;

                  await MakePlannedRollSetHistoryEntryActualAsync(rollSet.RollSetId, dc.RollDataInfoList);
                  //update Roll diameters 
                  if (dc.RollDataInfoList != null)
                  {
                    foreach (SingleRollDataInfo rollInfo in dc.RollDataInfoList)
                    {
                      //RollDataHandler.UpdateRollDiameter(rollInfo.RollId, rollInfo.Diameter);
                      //await RollShopManager.UpdateRollDiameterAsync(rollInfo.RollId, rollInfo.Diameter);
                      RLSRoll roll = _rollHandler.GetRollFromId(ctx, rollInfo.RollId);
                      //uow.Repository<Roll>().Query(z => z.RollId == dc.Id).GetSingle();
                      if (roll != null)
                      {
                        roll.LastUpdateTs = DateTime.Now;
                        roll.ActualDiameter = rollInfo.Diameter;
                      }
                      await ctx.SaveChangesAsync();
                    }
                  }


                  //update rollset status to readyForMounting

                  //TUDU
                  //RollSetDataHandler.UpdateRollSetStatus(rollSet.RollSetId, PE.Core.Constants.RollSetStatus.Ready);
                  await UpdateRollSetStatusAsync(rollSet.RollSetId, RollSetStatus.Ready);
                  await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
                  break;
                  //return true;
                  //return result;
                }
              case GrindingTurningAction.Cancel:
                {
                  //delete all planned entries and update rollset status to ready
                  //if (DeletePlannedRollSetDataAsync(rollSet.RollSetId) != 0)
                  //  return false;
                  await DeletePlannedRollSetDataAsync(rollSet.RollSetId);

                  //TUDU
                  //RollSetDataHandler.UpdateRollSetStatus(rollSet.RollSetId, PE.Core.Constants.RollSetStatus.Ready);
                  //return true;

                  await UpdateRollSetStatusAsync(rollSet.RollSetId, RollSetStatus.Ready);
                  await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
                  break;
                  //return result;
                }
              case GrindingTurningAction.Undefined:
                {
                  NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Unrecognized grinding turning action.");
                  NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                  //throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Unrecognized grinding turning action.");
                  throw new Exception();
                }
              default:
                {
                  NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Unrecognized grinding turning action.");
                  NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                  //throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollGrooves. Unrecognized grinding turning action.");
                  throw new Exception();
                }
            }
          }
        }
        catch (Exception)
        {
          NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollGrooves.");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollGrooves.");
        }
      }

      return result;

    }

    public virtual async Task<DataContractBase> DeleteRollSetHistoryByRollIdAsync(DCRollSetHistoryData dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRollSetHistory rsh = _rollSetHistoryHandler.GetRollSetHistoryFromRollSetId(ctx, dc.RollSetId);
          if (rsh != null)
          {
            ctx.RLSRollSetHistories.Remove(rsh);
            await ctx.SaveChangesAsync();
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while removing RollSetHistory from DB.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while removing RollSetHistory from DB.");
      }
      return result;
    }
    public virtual async Task<DataContractBase> UpdateRollsetHistoryRecordsAsync(long rollSetId, RollSetHistoryStatus oldStatus, RollSetHistoryStatus newStatus)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {

          List<RLSRollSetHistory> rollSetHistoryList = _rollSetHistoryHandler.GetRollSetHistoryList(ctx, rollSetId, oldStatus);


          if (rollSetHistoryList!=null && rollSetHistoryList.Count > 0)
          {
            foreach (RLSRollSetHistory rec in rollSetHistoryList)
            {
              rec.Status = newStatus;
            }
            await ctx.SaveChangesAsync();
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollSetHistoryRecords.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollSetHistoryRecords.");
      }
      return result;
    }

    /// <summary>
    /// Sets all actual RollSetHistory data for given RollSet to status Old (including associated grooves)
    /// </summary>
    /// <returns>0 when all OK</returns>
    public virtual async Task<DataContractBase> MakeActualRollSetHistoryEntryOldAsync(long rollSetId)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          //get all actual (should be only one) RollSetHistory entries for given RollSetId and update their status to PE.Core.Constants.RollSetHistoryStatus.Old
          //List<RollSetHistory> actRSHists = uow.Repository<RollSetHistory>().Query(q => q.FkRollSetId == rollSetId && q.Status == PE.Core.Constants.RollSetHistoryStatus.Actual).Get().ToList();
          List<RLSRollSetHistory> actRSHists = _rollSetHistoryHandler.GetRollSetHistoryRecordList(ctx, rollSetId, RollSetHistoryStatus.Actual);
          if (actRSHists != null)
          {
            foreach (RLSRollSetHistory rsh in actRSHists)
            {
              //IList<RollGroovesHistory> result = uow.Repository<RollGroovesHistory>().Query(f => f.FkRollSetHistoryId == rsh.RollSetHistoryId).Get().ToList();
              List<RLSRollGroovesHistory> rollGrooveHistoryList = _rollGroovesHistoryHandler.GetRollGrooveHistoryFromRollSetHistoryId(ctx, rsh.RollSetHistoryId);

              DateTime dtnow = DateTime.Now;
              foreach (RLSRollGroovesHistory rec in rollGrooveHistoryList)
              {
                rec.Status = RollGrooveStatus.Old;
                rec.Deactivated = dtnow;
                rec.LastUpdateTs = dtnow;
                //uow.Repository<RollGroovesHistory>().Update(rec);
              }
              //await UpdateRollSetHistoryStatusAsync(rsh.RollSetHistoryId, RollSetHistoryStatus.Old);
              rsh.Status = RollSetHistoryStatus.Old;
            }

          }
          await ctx.SaveChangesAsync();
        }
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollSetHistory.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollSetHistory.");
      }
      return result;
    }
    /// <summary>
    /// Changes status of planned rollsetHistory and its grooves to actual
    /// </summary>
    /// <param name="rollSetId"></param>
    /// <returns>0 when all OK</returns>
    public virtual async Task<DataContractBase> MakePlannedRollSetHistoryEntryActualAsync(long rollSetId, List<SingleRollDataInfo> rolls)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          //get all actual (should be only one) RollSetHistory entries for given RollSetId and update their status to PE.Core.Constants.RollSetHistoryStatus.Old
          List<RLSRollSetHistory> actRollSetHistory = _rollSetHistoryHandler.GetRollSetHistoryRecordList(ctx, rollSetId, RollSetHistoryStatus.Planned);

          if (actRollSetHistory != null)
          {
            foreach (RLSRollSetHistory rsh in actRollSetHistory)
            {
              foreach (SingleRollDataInfo rollInfo in rolls)
              {
                //IList<RLSRollGroovesHistory> result = uow.Repository<RollGroovesHistory>().Query(f => f.FkRollSetHistoryId == rsh.RollSetHistoryId && f.FkRollId == rollInfo.RollId).Get().ToList();
                List<RLSRollGroovesHistory> rollGrooveHistoryList = _rollGroovesHistoryHandler.GetRollGrooveHistory(ctx, rsh.RollSetHistoryId, rollInfo.RollId);


                DateTime dtnow = DateTime.Now;
                foreach (RLSRollGroovesHistory rec in rollGrooveHistoryList)
                {
                  rec.Status = RollGrooveStatus.Actual;
                  rec.ActDiameter = rollInfo.Diameter;
                  rec.LastUpdateTs = dtnow;
                  //uow.Repository<RollGroovesHistory>().Update(rec);
                }
              }
              //await UpdateRollSetHistoryStatusAsync(rsh.RollSetHistoryId, RollSetHistoryStatus.Actual);
              rsh.Status = RollSetHistoryStatus.Actual;
            }
          }
          await ctx.SaveChangesAsync();
          //uow.SaveChanges();
        }
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollSetHistory.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollSetHistory.");
      }
      return result;
    }
    /// <summary>
    /// Deletes from DB all planned grinding operations for given RollSet ID
    /// </summary>
    /// <param name="rollSetId"></param>
    /// <returns>0 when all OK/returns>
    public virtual async Task<DataContractBase> DeletePlannedRollSetDataAsync(long rollSetId)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          //get all actual (should be only one) RollSetHistory entries for given RollSetId and update their status to PE.Core.Constants.RollSetHistoryStatus.Old
          RLSRollSetHistory rsh = _rollSetHistoryHandler.GetRollSetHistory(ctx, rollSetId, RollSetHistoryStatus.Planned);
          //RollSetHistory rsh = uow.Repository<RollSetHistory>().Query(q => q.FkRollSetId == rollSetId && q.Status == PE.Core.Constants.RollSetHistoryStatus.Planned).GetSingle();
          if (rsh != null)
          {
            //Due to on delete cascade relation between RLSRollSetHistory and RLSRollGroovesHistory all records from RLSRollGroovesHistory are deleted on DB layer

            //delete all planned grooves
            //uow.Repository<RollGroovesHistory>().DeleteBatch(f => f.FkRollSetHistoryId == rsh.RollSetHistoryId);
            //List<RLSRollGroovesHistory> rgh = _rollGroovesHistoryHandler.GetRollGrooveHistoryFromRollSetHistoryId(ctx, rsh.RollSetHistoryId);
            //foreach(RLSRollGroovesHistory element in rgh)
            //{
            //  ctx.RLSRollGroovesHistories.Remove(element);
            //}

            //delete rollsetHistory record 
            //uow.Repository<RollGroovesHistory>().Delete(rsh);
            ctx.RLSRollSetHistories.Remove(rsh);
          }

          await ctx.SaveChangesAsync();
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while delating RollSetHistory.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while delating RollSetHistory.");
      }
      return result;
    }
    #endregion
   

    public virtual async Task<DataContractBase> UpdateRollSetHistoryStatusAsync(long rollSetHistoryId, RollSetHistoryStatus status)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRollSetHistory rollSetHistory = _rollSetHistoryHandler.GetRollSetHistoryFromId(ctx, rollSetHistoryId);
          if (rollSetHistory != null)
          {
            rollSetHistory.Status = status;
            await ctx.SaveChangesAsync();
          }
        }
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.RollSetHistorySaveError, "Error while updating RollSetHistory.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetHistorySaveError, "Error while updating RollSetHistory.");
      }
      return result;
    }

    public virtual async Task<DataContractBase> UpdateRollSetStatusAsync(long rollSetId, RollSetStatus status)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, rollSetId);
          if (rollSet != null)
          {
            rollSet.Status = status;
            await ctx.SaveChangesAsync();
          }
        }
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while updating RollSet.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while updating RollSet.");
      }
      return result;
    }
  }
}
