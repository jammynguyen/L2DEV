using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
  public class CassetteManager : ICassetteManager
  {
    private readonly ICassetteManagerSendOffice _sendOffice;

    private readonly CassetteHandler _cassetteHandler;


    public CassetteManager(ICassetteManagerSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _cassetteHandler = new CassetteHandler();
    }

    public virtual async Task<DataContractBase> InsertCassetteAsync(DCCassetteData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null || (String.IsNullOrEmpty(dc.CassetteName)))
      {
        NotificationController.RegisterAlarm(Defs.CassetteSaveError, "Error while inserting Cassette to DB. CassetteName is not valid");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteSaveError, "Error while inserting Cassette to DB. CassetteName is not valid");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSCassette cassette = _cassetteHandler.GetCassetteFromName(ctx, dc.CassetteName);

          if (cassette == null)
          {
            cassette = new RLSCassette();

            _cassetteHandler.CreateCassette(ctx, ref cassette, dc);

            //cassette.Arrangement = CassetteArrangement.Undefined;
            //cassette.CassetteName = dc.CassetteName;
            //cassette.CreatedTs = DateTime.Now;
            //cassette.LastUpdateTs = DateTime.Now;
            //cassette.FKCassetteTypeId = (short)dc.CassetteTypeId;
            //cassette.FKStandId = dc.StandId;
            //cassette.NumberOfPositions = (short)dc.NumberOfPositions;
            //cassette.Status = CassetteStatus.New;


            ctx.RLSCassettes.Add(cassette);


            await ctx.SaveChangesAsync();

            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
          else
          {
            NotificationController.RegisterAlarm(Defs.CassetteAlreadyExist, "Error while inserting Cassette to DB. Cassette with specified name already exists in DB");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteAlreadyExist, "Error while inserting Cassette to DB. Cassette  with specified name already exists in DB");
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.CassetteSaveError, "Error while inserting Cassette to DB.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteSaveError, "Error while inserting Cassette to DB.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateCassetteAsync(DCCassetteData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.CassetteSaveError, "Error while updating Cassette");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteSaveError, "Error while updating Cassette.");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, dc.Id);

          if (cassette != null)
          {
            //cassette.CassetteName = dc.CassetteName;
            //cassette.CreatedTs = DateTime.Now;
            //cassette.FKCassetteTypeId = (short)dc.CassetteTypeId;
            //cassette.FKStandId = dc.StandId;
            //cassette.NumberOfPositions = (short)dc.NumberOfPositions;
            //cassette.Status = (CassetteStatus)dc.Status;

            _cassetteHandler.UpdateCassette(ctx, ref cassette, dc);

            await ctx.SaveChangesAsync();

            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.CassetteSaveError, "Error while updating Cassette.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteSaveError, "Error while updating Cassette.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateCassetteStatusAsync(DCCassetteData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.CassetteSaveError, "Error while updating Cassette status");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteSaveError, "Error while updating Cassette status.");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, dc.Id);

          if (cassette != null)
          {
            cassette.Status = (CassetteStatus)dc.Status;

            await ctx.SaveChangesAsync();

            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.CassetteSaveError, "Error while updating Cassette status.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteSaveError, "Error while updating Cassette status.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> DeleteCassetteAsync(DCCassetteData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.CassetteSaveError, "Error while removing Cassette.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteSaveError, "Error while removing Cassette.");
      }
      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, dc.Id);

          if (cassette != null)
          {
            ctx.RLSCassettes.Remove(cassette);
            await ctx.SaveChangesAsync();
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.CassetteSaveError, "Error while removing Cassette.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteSaveError, "Error while removing Cassette.");
      }

      return result;
    }

  }
}
