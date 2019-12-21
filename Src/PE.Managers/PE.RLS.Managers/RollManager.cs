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
  public class RollManager : IRollManager
  {
    private readonly IRollManagerSendOffice _sendOffice;

    private readonly RollHandler _rollHandler;


    public RollManager(IRollManagerSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _rollHandler = new RollHandler();
    }

    public virtual async Task<DataContractBase> InsertRollAsync(DCRollData dc)
    {
      DataContractBase result = new DataContractBase();

      if (String.IsNullOrEmpty(dc.RollName))
      {
        NotificationController.RegisterAlarm(Defs.RollSaveError, "Error while inserting Roll to DB. RollName is not valid");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSaveError, "Error while inserting Roll to DB. RollName is not valid");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {

          //RLSRoll roll = _rollHandler.GetRollFromName(ctx, dc.RollName);
          RLSRoll roll = new RLSRoll();
          if (roll != null)
          {

            roll.ActualDiameter = dc.ActualDiameter ?? 0;
            roll.CreatedTs = DateTime.Now;
            roll.LastUpdateTs = DateTime.Now;
            roll.RollDescription = dc.Description;
            roll.FKRollTypeId = dc.RollTypeId ?? -1;
            roll.GroovesNumber = 0;
            //Roll.RollType = dc.RollType;
            //if (dc.GroovesNumber == null)
            //  Roll.GroovesNumber = 1;  //0
            //else
            //  Roll.GroovesNumber = (short)dc.GroovesNumber;
            roll.InitialDiameter = dc.InitialDiameter ?? 0.0;
            roll.MinimumDiameter = dc.MinimumDiameter ?? 0.0;
            roll.RollName = dc.RollName;
            roll.Status = dc.Status;
            roll.Supplier = dc.Supplier;
            roll.Location = dc.Location;

            ctx.RLSRolls.Add(roll);


            await ctx.SaveChangesAsync();

            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
          else
          {
            NotificationController.RegisterAlarm(Defs.RollAlreadyExist, "Error while inserting Roll to DB. Roll with specified name already exists in DB");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollAlreadyExist, "Error while inserting Roll to DB. Roll with specified name already exists in DB");
          }
        }
      }
      catch(Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSaveError, "Error while inserting Roll to DB.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSaveError, "Error while inserting Roll to DB.");
      }

      return result;
    }


    public virtual async Task<DataContractBase> UpdateRollAsync(DCRollData dc)
    {
      DataContractBase result = new DataContractBase();

      if (String.IsNullOrEmpty(dc.RollName))
      {
        NotificationController.RegisterAlarm(Defs.RollSaveError, "Error while updating Roll. RollName is not valid");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSaveError, "Error while updating Roll. RollName is not valid");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSRoll roll = _rollHandler.GetRollFromId(ctx, dc.Id);

          if (roll != null)
          {
            //roll.ActualDiameter = dc.ActualDiameter ?? 0;
            //roll.CreatedTs = DateTime.Now;
            //roll.RollDescription = dc.Description;
            //roll.FKRollTypeId = dc.RollTypeId ?? -1;
            ////if (dc.GroovesNumber == null)
            ////  Roll.GroovesNumber = 1;  // 0
            ////else
            ////  Roll.GroovesNumber = (short)dc.GroovesNumber;
            //roll.InitialDiameter = dc.InitialDiameter ?? 0.0;
            //roll.MinimumDiameter = dc.MinimumDiameter ?? 0.0;
            //roll.RollName = dc.RollName;
            //roll.Status = dc.Status;
            //roll.Supplier = dc.Supplier;

            //ctx.RLSRolls.Add(roll);

            _rollHandler.UpdateRoll(ctx, ref roll, dc);
            await ctx.SaveChangesAsync();

            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollSaveError, "Error while updating Roll.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSaveError, "Error while updating Roll.");
      }

      return result;
    }


    public virtual async Task<DataContractBase> DeleteRollAsync(DCRollData dc)
    {
      DataContractBase result = new DataContractBase();

      //if (String.IsNullOrEmpty(dc.RollName))
      //{
      //  NotificationController.RegisterAlarm(Defs.RollSaveError, "Error while removing Roll. RollName is not valid");
      //  NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
      //  throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSaveError, "Error while removing Roll. RollName is not valid");
      //}
      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSRoll roll = _rollHandler.GetRollFromId(ctx, dc.Id);

          if (roll != null)
          {
            ctx.RLSRolls.Remove(roll);
            await ctx.SaveChangesAsync();
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollSaveError, "Error while removing Roll.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSaveError, "Error while removing Roll.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> ScrapRollAsync(DCRollData dc)
    {
      DataContractBase result = new DataContractBase();

      if (String.IsNullOrEmpty(dc.RollName))
      {
        NotificationController.RegisterAlarm(Defs.RollSaveError, "Error while scraping Roll. RollName is not valid");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSaveError, "Error while sdcraping Roll. RollName is not valid");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSRoll roll = _rollHandler.GetRollFromId(ctx, dc.Id);

          if (roll != null)
          {
            roll.ScrapDate = dc.ScrapDate;
            roll.ScrapReason = dc.ScrapReason;
            roll.Status = RollStatus.Scrapped;


            await ctx.SaveChangesAsync();

            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollSaveError, "Error while scraping Roll.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSaveError, "Error while scraping Roll.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateRollStatusAsync(DCRollData dc)
    {
      DataContractBase result = new DataContractBase();

      if (String.IsNullOrEmpty(dc.RollName))
      {
        NotificationController.RegisterAlarm(Defs.RollSaveError, "Error while updating Roll status. RollName is not valid");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSaveError, "Error while updating Roll. RollName is not valid");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRoll roll = _rollHandler.GetRollFromId(ctx, dc.Id);
          //uow.Repository<Roll>().Query(z => z.RollId == dc.Id).GetSingle();
          if (roll != null)
          {
            roll.LastUpdateTs = DateTime.Now;
            roll.Status = dc.Status;

            await ctx.SaveChangesAsync();
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSaveError, "Error while updating Roll status.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSaveError, "Error while updating Roll status.");
      }
      return result;
    }

    public virtual async Task<DataContractBase> UpdateRollDiameterAsync(long rollId, double diameter)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRoll roll = _rollHandler.GetRollFromId(ctx, rollId);
          //uow.Repository<Roll>().Query(z => z.RollId == dc.Id).GetSingle();
          if (roll != null)
          {
            roll.LastUpdateTs = DateTime.Now;
            roll.ActualDiameter = diameter;

            await ctx.SaveChangesAsync();
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSaveError, "Error while updating Roll diameter.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSaveError, "Error while updating Roll diameter.");
      }
      return result;
    }
    public virtual async Task<DataContractBase> UpdateStandConfigurationAsync(DCStandConfigurationData dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          //PEUnitOfWork uowStand = new PEUnitOfWork();
          //PEUnitOfWork uowCassette = new PEUnitOfWork();
          //VActualStandConfiguration viewToStore = uow.Repository<VActualStandConfiguration>().Query(z => z.StandId == dc.Id).GetSingle();
          RLSStand stand = _rollHandler.GetStandFromId(ctx, dc.Id);
          RLSCassette cassette = _rollHandler.GetCassetteFromStandId(ctx, dc.Id);
          if (stand != null && cassette != null)
          {
            cassette.Arrangement = (CassetteArrangement)dc.Arrangement;

            stand.Status = (short)dc.Status;

            await ctx.SaveChangesAsync();
          }
          else
          {
            NotificationController.RegisterAlarm(Defs.RollSaveError, "Error while updating Stand configuration. stand or cassette not found");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSaveError, "Error while updating Stand configuration. stand or cassette not found.");
          }

        }
        }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSaveError, "Error while updating Stand configuration.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSaveError, "Error while updating Stand configuration.");
      }
      return result;
    }
  }
}
