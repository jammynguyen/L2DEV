using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.Common;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
//using PE.DTO.Internal.Rollshop;
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
  public class RollSetManager : IRollSetManager
  {
    private readonly IRollSetManagerSendOffice _sendOffice;

    private readonly RollHandler _rollHandler;
    private readonly RollSetHandler _rollSetHandler;
    private readonly RollSetHistoryHandler _rollSetHistoryHandler;


    public RollSetManager(IRollSetManagerSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _rollHandler = new RollHandler();
      _rollSetHandler = new RollSetHandler();
      _rollSetHistoryHandler = new RollSetHistoryHandler();
    }

    public virtual async Task<DataContractBase> InsertRollSetAsync(DCRollSetData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null || (String.IsNullOrEmpty(dc.RollSetName)))
      {
        NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while inserting RollSet to DB. RollSetName is not valid");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while inserting RollSet to DB. RollSetName is not valid");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSRollSet rollSet = _rollSetHandler.GetRollSetFromName(ctx, dc.RollSetName);

          if (rollSet == null)
          {
            rollSet = new RLSRollSet();
            //rollSet.Created = DateTime.Now;
            //rollSet.CreatedTs = DateTime.Now;
            //rollSet.LastUpdateTs = DateTime.Now;
            //rollSet.RollSetDescription = dc.Description;
            //rollSet.RollSetName = dc.RollSetName;
            //rollSet.Status = (RollSetStatus)dc.RollSetStatus;
            //rollSet.RollSetType = dc.RollSetType;

            _rollSetHandler.CreateRollSet(ctx, ref rollSet, dc);

            ctx.RLSRollSets.Add(rollSet);

            await ctx.SaveChangesAsync();

            DCRollSetHistoryData dcHistory = new DCRollSetHistoryData();
            dcHistory.CassetteId = dc.CassetteId;
            dcHistory.RollSetId = rollSet.RollSetId;
            //dcHistory.Dismounted = dc.Dismounted;
            dcHistory.Status = RollSetHistoryStatus.Actual;
            dcHistory.Mounted = dc.Mounted;
            dcHistory.PositionInCassette = dc.PositionInCassette;
            dcHistory.CassetteId = dc.CassetteId;

            RLSRollSetHistory rollSetHistory = new RLSRollSetHistory();
            _rollSetHistoryHandler.UpdateRollSetHistory(ctx, ref rollSetHistory, dcHistory);
            ctx.RLSRollSetHistories.Add(rollSetHistory);
            await ctx.SaveChangesAsync();

            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
          //else
          //{
          //  NotificationController.RegisterAlarm(Defs.RollTypeAlreadyExist, "Error while inserting RollSet to DB. RollSet already exist");
          //  NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          //  throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeAlreadyExist, "Error while inserting RollSet to DB. RollSet already exist");
          //}
        }
      }
      catch(Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while inserting RollSet to DB.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while inserting RollSet to DB.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> AssembleRollSetAsync(DCRollSetData dc)
    {
      DataContractBase result = new DataContractBase();

      //PEUnitOfWork uow = new PEUnitOfWork();
      //validate data

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.RollSetAssembleError, "Error while assembling RollSet. RollSetName is not valid");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetAssembleError, "Error while assembling RollSet. RollSetName is not valid");
      }
     // short rsType = dc.RollSetType ?? 0;
      switch (dc.RollSetType)
      {
        case RollSetType.TwoActiveRollsIM:
        case RollSetType.TwoActiveRollsRM:
          {
            if (dc.UpperRollId == null)
            {
              NotificationController.RegisterAlarm(Defs.RollSetAssembleError, "Error while assembling RollSet. Upper roll is missing");
              NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
              throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetAssembleError, "Error while assembling RollSet. Upper roll is missing");
            }
            if (dc.BottomRollId == null)
            {
              NotificationController.RegisterAlarm(Defs.RollSetAssembleError, "Error while assembling RollSet. Bottom roll is missing");
              NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
              throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetAssembleError, "Error while assembling RollSet. Bottom roll is missing");
            }
            if (dc.UpperRollId == dc.BottomRollId)
            {
              NotificationController.RegisterAlarm(Defs.RollSetAssembleError, "Error while assembling RollSet. Upper and bottom roll can't be the same");
              NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
              throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetAssembleError, "Error while assembling RollSet. Upper and bottom roll can't be the same");
            }
            break;
          }
        default:
          {
            NotificationController.RegisterAlarm(Defs.RollSetAssembleError, "Error while assembling RollSet. Unrecognized rollset type");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetAssembleError, "Error while assembling RollSet. Unrecognized rollset type");
          }
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, dc.Id); // uow.Repository<RollSet>().Query(z => z.RollSetId == dc.Id).GetSingle();
          if (rollSet == null)
          {
            NotificationController.RegisterAlarm(Defs.RollSetAssembleError, "Error while assembling RollSet. Unknown rollset");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetAssembleError, "Error while assembling RollSet. Unknown rollset");
          }
          RLSRoll upperRoll = _rollHandler.GetRollFromId(ctx, dc.UpperRollId); // uow.Repository<Roll>().Query(r => r.RollId == dc.UpperRollId).GetSingle();
          if (upperRoll == null)
          {
            NotificationController.RegisterAlarm(Defs.RollSetAssembleError, "Error while assembling RollSet. Unknown upper roll");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetAssembleError, "Error while assembling RollSet. Unknown upper roll");
          }
          RLSRoll bottomRoll = _rollHandler.GetRollFromId(ctx, dc.BottomRollId);  //uow.Repository<Roll>().Query(r => r.RollId == dc.BottomRollId).GetSingle();
          if (bottomRoll == null)
          {
            NotificationController.RegisterAlarm(Defs.RollSetAssembleError, "Error while assembling RollSet. Unknown bottom roll");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetAssembleError, "Error while assembling RollSet. Unknown bottom roll");
          }


          if (rollSet.Status != RollSetStatus.Empty)
          {
            NotificationController.RegisterAlarm(Defs.RollSetAssembleError, "Error while assembling RollSet. Rollset is not empty");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetAssembleError, "Error while assembling RollSet. Rollset is not empty");
          }

          //update data
          rollSet.LastUpdateTs = DateTime.Now;
          rollSet.RollSetDescription = dc.Description;
          rollSet.FKUpperRollId = dc.UpperRollId;
          rollSet.FKBottomRollId = dc.BottomRollId;


          rollSet.Status = RollSetStatus.Ready;
          upperRoll.Status = RollStatus.InRollSet;
          //uow.Repository<Roll>().Update(upperRoll);
          bottomRoll.Status = RollStatus.InRollSet;
          //uow.Repository<Roll>().Update(bottomRoll);
          //rollSet.Status = (short)PE.Core.Constants.RollSetStatus.Ready;
          //uow.Repository<RollSet>().Update(rollSet);
          //uow.SaveChanges();
          //DoRollSetChangedEvent(rollSet);
          await ctx.SaveChangesAsync();
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSetAssembleError, "Error while assembling RollSet");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetAssembleError, "Error while assembling RollSet");
      }
      return result;
    }

    public virtual async Task<DataContractBase> UpdateRollSetStatusAsync(DCRollSetData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while updating RollSet. RollSet is not empty");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetAssembleError, "Error while updating RollSet. RollSet is not empty");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, dc.Id);
          if (rollSet != null)
          {
            rollSet.Status = (RollSetStatus)dc.RollSetStatus;
            await ctx.SaveChangesAsync();
          }
          else
          {
            NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while updating RollSet status. Specified rollset does not exist.");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while updating RollSet status.Specified rollset does not exist.");
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while updating RollSet status.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while updating RollSet status.");
      }
      return result;
    }

    public virtual async Task<DataContractBase> DeleteRollSetAsync(DCRollSetData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while deleting RollSet. Empty DataContract.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while deleting RollSet. Empty DataContract.");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, dc.Id); // uow.Repository<RollSet>().Query(z => z.RollSetId == dc.DeleteItemId).GetSingle();
          if (rollSet != null)
          {
            List<RLSRollSetHistory> rollsetHistories = _rollSetHistoryHandler.GetRollSetHistoryRecordListFromRollSetId(ctx, (long)dc.Id); // uow.Repository<RollSetHistory>().Query(z => z.FkRollSetId == dc.DeleteItemId).Get().ToList();
            if (rollsetHistories.Count > 0)
            {
              foreach (RLSRollSetHistory element in rollsetHistories)
              {
                ctx.RLSRollSetHistories.Remove(element);
              }
            }
            ctx.RLSRollSets.Remove(rollSet);
            await ctx.SaveChangesAsync();
          }
          else
          {
            NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while deleting RollSet. RollSet does not exist.");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while deleting RollSet. RollSet does not exist.");
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while deleting RollSet.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while deleting RollSet.");
      }
      return result;
    }


    public virtual async Task<DataContractBase> DisassembleRollSetAsync(DCRollSetData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while disassembling RollSet. Empty DataContract.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while disassembling RollSet. Empty DataContract.");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, dc.Id); //  uow.Repository<RollSet>().Query(z => z.RollSetId == dc.Id).GetSingle();
          RLSRoll bottomRoll = null;
          RLSRoll upperRoll = null;
          if (rollSet != null)
          {
            if (rollSet.FKBottomRollId != null)
            {
              bottomRoll = _rollHandler.GetRollFromId(ctx, rollSet.FKBottomRollId);
              bottomRoll.LastUpdateTs = DateTime.Now;
              bottomRoll.Status = RollStatus.Unassigned;
            }
            if (rollSet.FKUpperRollId != null)
            {
              upperRoll = _rollHandler.GetRollFromId(ctx, rollSet.FKUpperRollId);
              upperRoll.LastUpdateTs = DateTime.Now;
              upperRoll.Status = RollStatus.Unassigned;
            }

            rollSet.LastUpdateTs = DateTime.Now;
            rollSet.Status = RollSetStatus.Empty;
            rollSet.FKBottomRollId = null;
            rollSet.FKUpperRollId = null;

            //  RollSet.IsThirdRoll = (short)PE.Core.Constants.NumberOfActiveRoll.twoActiveRollsRM;

            await ctx.SaveChangesAsync();
          }
          else
          {
            NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while disassembling RollSet. RollSet does not exist.");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while disassembling RollSet. RollSet does not exist.");
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while disassembling RollSet.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while disassembling RollSet.");
      }
      return result;
    }

    public virtual async Task<DataContractBase> ConfirmRollSetStatusAsync(DCRollSetData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while confirming RollSet status. Empty DataContract.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while confirming RollSet status.Empty DataContract.");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, dc.Id); //  uow.Repository<RollSet>().Query(z => z.RollSetId == dc.Id).GetSingle();
          if (rollSet != null)
          {
            rollSet.LastUpdateTs = DateTime.Now;
            if (dc.RollSetStatus != null)
              rollSet.Status = (RollSetStatus)dc.RollSetStatus;
            else
              rollSet.Status = RollSetStatus.Ready;
            await ctx.SaveChangesAsync();
          }
          else
          {
            NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while confirming RollSet status. Specified rollset does not exist.");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while confirming RollSet status.Specified rollset does not exist.");
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while confirming RollSet status.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while confirming RollSet status.");
      }
      return result;
    }

    //Commented out 18.09.2019. Kocks are not part od Lite solution
    //public static bool ConfirmRollSetForKocksStatus(DCRollSetData dc)
    //{
    //  if (dc == null)
    //  {
    //    return false;
    //  }
    //  try
    //  {
    //    using (PEUnitOfWork uow = new PEUnitOfWork())
    //    {
    //      RollSet RollSet = uow.Repository<RollSet>().Query(z => z.RollSetId == dc.Id).GetSingle();
    //      if (RollSet != null)
    //      {
    //        RollSet.LastUpdateTs = DateTime.Now;
    //        if (dc.RollSetStatus != null)
    //          RollSet.Status = (short)dc.RollSetStatus;
    //        else
    //          RollSet.Status = (short)PE.Core.Constants.RollSetStatus.Ready;
    //        RollSet.State = SMF.RepositoryPattern.Infrastructure.ObjectState.Modified;
    //        uow.Repository<RollSet>().Update(RollSet);
    //        uow.SaveChanges();
    //        DoRollSetChangedEvent(RollSet);
    //        ModuleController.Logger.Info(String.Format("Roll Set {0} status changed to {1}.", dc.RollSetName, ResourceController.GetEnumValue("RollSetStatus", (short)dc.RollSetStatus)), dc.RollSetName, ResourceController.GetEnumValue("RollSetStatus", (short)dc.RollSetStatus));
    //        NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetStatusBeChanged, String.Format("Roll Set {0} status changed to {1}.", dc.RollSetName, ResourceController.GetEnumValue("RollSetStatus", (short)dc.RollSetStatus)), dc.RollSetName, ResourceController.GetEnumValue("RollSetStatus", (short)dc.RollSetStatus));
    //        return true;
    //      }
    //      else
    //        ModuleController.Logger.Info(String.Format("Roll Set {0} not exists.", dc.RollSetName));
    //      NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetNotExists, String.Format("Roll Set {0} not exists.", dc.RollSetName), dc.RollSetName);
    //      return false;
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    DbExceptionResult result = DbExceptionHelper.ProcessException(ex, "UpdateRollSet::Database operation failed!", null);
    //    return false;
    //  }
    //}

    //Commented out 18.09.2019. Hard to say if there is any need for it.
    //public virtual async Task<DataContractBase> CancelRollSetStatusAsync(DCRollSetData dc)
    //{
    //  DataContractBase result = new DataContractBase();
    //  if (dc == null)
    //  {
    //    NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while cancelling RollSet status. Empty DataContract");
    //    NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
    //    throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while cancelling RollSet status.Empty DataContract.");
    //  }
    //  try
    //  {
    //    using (PEContext ctx = new PEContext())
    //    {
    //      RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, dc.Id);// uow.Repository<RollSet>().Query(z => z.RollSetId == dc.Id).GetSingle();
    //      //RLSRollSetHistory RollSetHis = uow.Repository<RollSetHistory>().Query(z => z.FkRollSetId == dc.Id && (z.Status == PE.Core.Constants.RollSetHistoryStatus.Actual || z.Status == PE.Core.Constants.RollSetHistoryStatus.Planned)).GetSingle();
    //      RLSRollSetHistory rollSetHis = _rollSetHistoryHandler.GetPlannedOrActualRollSetHistory(ctx, (long)dc.Id);
    //      // Roll roll = uow.Repository<Roll>().Query(z => z.RollId == dc.Id).GetSingle();
    //      if (rollSet != null)
    //      {
    //        if (dc.RollSetStatus == RollSetStatus.Empty)
    //        {
    //          if (rollSet.FKBottomRollId != null)
    //          {
    //            DCRollData dcRollData = new DCRollData();
    //            dcRollData.Id = rollSet.FKBottomRollId;
    //            dcRollData.Status = RollStatus.Unassigned;
    //            RollDataHandler.UpdateRollStatus(dcRollData);

    //            DCRollGroovesHistoryStatusData dcRollBGrooveStatusData = new DCRollGroovesHistoryStatusData();

    //            //RLSRoll rollB = uow.Repository<Roll>().Query(z => z.RollId == dc.Id).GetSingle();
    //            RLSRoll rollB = _rollHandler.GetRollFromId(ctx, dc.Id);
    //            //IList<RollGroovesHistory> roll = uow.Repository<RollGroovesHistory>().Query(z => z.FkRollSetHistoryId == RollSetHis.RollSetHistoryId).Include(o => o.RollSetHistory).Get().ToList();

    //            IList<RLSRollGroovesHistory> rgs = ctx.RLSRollGroovesHistories.Where(z => z.FKRollSetHistoryId == rollSetHis.RollSetHistoryId).Include(o => o.RLSRollSetHistory).ToList();

    //            foreach (RLSRollGroovesHistory lr in rgs)
    //            {
    //              dcRollBGrooveStatusData.Id = lr.RollGroovesHistoryId;
    //              dcRollBGrooveStatusData.RollId = (long)rollSet.FKBottomRollId;
    //              dcRollBGrooveStatusData.GrooveNumber = rollB.GroovesNumber;
    //              dcRollBGrooveStatusData.RollSetHistoryId = rollSetHis.RollSetHistoryId;
    //              dcRollBGrooveStatusData.GrooveHistoryStatus = (short)RollGrooveStatus.Old;
    //              RollGroovesHistoryDataHandler.UpdateRollGroovesHistoryStatus(dcRollBGrooveStatusData);
    //            }
    //          }
    //          if (rollSet.FKUpperRollId != null)
    //          {
    //            DCRollData dcRollData = new DCRollData();
    //            dcRollData.Id = rollSet.FKUpperRollId;
    //            dcRollData.Status = RollStatus.Unassigned;
    //            RollDataHandler.UpdateRollStatus(dcRollData);

    //            DCRollGroovesHistoryStatusData dcRollUGrooveStatusData = new DCRollGroovesHistoryStatusData();

    //            //Roll rollB = uow.Repository<Roll>().Query(z => z.RollId == dc.Id).GetSingle();
    //            RLSRoll rollB = _rollHandler.GetRollFromId(ctx, dc.Id);
    //            //IList<RollGroovesHistory> roll = uow.Repository<RollGroovesHistory>().Query(z => z.FkRollSetHistoryId == RollSetHis.RollSetHistoryId).Include(o => o.RollSetHistory).Get().ToList();
    //            IList<RLSRollGroovesHistory> rgs = ctx.RLSRollGroovesHistories.Where(z => z.FKRollSetHistoryId == rollSetHis.RollSetHistoryId).Include(o => o.RLSRollSetHistory).ToList();

    //            foreach (RLSRollGroovesHistory lr in rgs)
    //            {
    //              dcRollUGrooveStatusData.Id = lr.RollGroovesHistoryId;
    //              dcRollUGrooveStatusData.RollId = (long)rollSet.FKBottomRollId;
    //              dcRollUGrooveStatusData.GrooveNumber = rollB.GroovesNumber;
    //              dcRollUGrooveStatusData.RollSetHistoryId = rollSetHis.RollSetHistoryId;
    //              dcRollUGrooveStatusData.GrooveHistoryStatus = RollGrooveStatus.Old;
    //              RollGroovesHistoryDataHandler.UpdateRollGroovesHistoryStatus(dcRollUGrooveStatusData);
    //            }
    //          }
    //          //if ((rollSet.IsThirdRoll == (short)PE.Core.Constants.RollSetType.threeActiveRollsK500) || (rollSet.IsThirdRoll == (short)PE.Core.Constants.RollSetType.threeActiveRollsK370) && rollSet.FkThirdRollId != null)
    //          //{
    //          //  DCRollData dcRollData = new DCRollData();
    //          //  dcRollData.Id = rollSet.FkThirdRollId;
    //          //  dcRollData.Status = (short)PE.Core.Constants.RollStatus.Unassigned;
    //          //  RollDataHandler.UpdateRollStatus(dcRollData);

    //          //  DCRollGroovesHistoryStatusData dcRollTGrooveStatusData = new DCRollGroovesHistoryStatusData();

    //          //  Roll rollB = uow.Repository<Roll>().Query(z => z.RollId == dc.Id).GetSingle();
    //          //  IList<RollGroovesHistory> roll = uow.Repository<RollGroovesHistory>().Query(z => z.FkRollSetHistoryId == RollSetHis.RollSetHistoryId).Include(o => o.RollSetHistory).Get().ToList();


    //          //  foreach (RollGroovesHistory lr in roll)
    //          //  {
    //          //    dcRollTGrooveStatusData.Id = lr.RollGroovesHistoryId;
    //          //    dcRollTGrooveStatusData.RollId = (long)rollSet.FkBottomRollId;
    //          //    dcRollTGrooveStatusData.GrooveNumber = rollB.GroovesNumber;
    //          //    dcRollTGrooveStatusData.RollSetHistoryId = RollSetHis.RollSetHistoryId;
    //          //    dcRollTGrooveStatusData.GrooveHistoryStatus = (short)PE.Core.Constants.RollGrooveStatus.Old;
    //          //    RollGroovesHistoryDataHandler.UpdateRollGroovesHistoryStatus(dcRollTGrooveStatusData);
    //          //  }
    //          //}

    //          rollSet.FkBottomRollId = null;
    //          rollSet.FkUpperRollId = null;
    //          //rollSet.FkThirdRollId = null;
    //          await ctx.SaveChangesAsync();
    //        }

    //        rollSet.LastUpdateTs = DateTime.Now;

    //        if (dc.RollSetStatus == RollSetStatus.Ready)
    //        {
    //          rollSet.Status = RollSetStatus.Ready;
    //        }
    //        else/* if (dc.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.ScheduledForAssemble)*/
    //        {
    //          rollSet.Status = RollSetStatus.Empty;
    //        }

    //        //rollSet.State = SMF.RepositoryPattern.Infrastructure.ObjectState.Modified;
    //        //uow.Repository<RollSet>().Update(rollSet);
    //        uow.SaveChanges();

    //        ModuleController.Logger.Info(String.Format("Roll Set {0} status changed to {1}.", dc.RollSetName, ResourceController.GetEnumValue("RollSetHistoryStatus", (short)dc.RollSetHistoryStatus)), dc.RollSetName, ResourceController.GetEnumValue("RollSetHistoryStatus", (short)dc.RollSetHistoryStatus));
    //        NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetStatusBeChanged, String.Format("Roll Set {0} status changed to {1}.", dc.RollSetName, ResourceController.GetEnumValue("RollSetHistoryStatus", (short)RollSetHis.Status)), dc.RollSetName, ResourceController.GetEnumValue("RollSetStatus", (short)RollSetHis.Status));
    //        if (RollSetHis != null)
    //        {
    //          RollSetHis.Status = PE.Core.Constants.RollSetHistoryStatus.Actual;
    //          RollSetHis.State = SMF.RepositoryPattern.Infrastructure.ObjectState.Modified;
    //          uow.Repository<RollSetHistory>().Update(RollSetHis);
    //          uow.SaveChanges();
    //          ModuleController.Logger.Info(String.Format("Roll Set {0} status changed to {1}.", dc.RollSetName, ResourceController.GetEnumValue("RollSetHistoryStatus", (short)dc.RollSetHistoryStatus)), dc.RollSetName, ResourceController.GetEnumValue("RollSetHistoryStatus", (short)dc.RollSetHistoryStatus));
    //          NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetStatusBeChanged, String.Format("Roll Set {0} status changed to {1}.", RollSetHis.RollSetHistoryId, ResourceController.GetEnumValue("RollSetHistoryStatus", (short)RollSetHis.Status)), RollSetHis.RollSetHistoryId, ResourceController.GetEnumValue("RollSetHistoryStatus", (short)RollSetHis.Status));
    //        }
    //        else
    //        {
    //          return false;
    //        }
    //        DoRollSetChangedEvent(rollSet);
    //        return true;
    //      }
    //      else
    //        NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetNotExists, String.Format("Roll Set {0} not exists.", dc.RollSetName), dc.RollSetName);
    //      return false;
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    DbExceptionResult result = DbExceptionHelper.ProcessException(ex, "UpdateRollSet::Database operation failed!", null);
    //    return false;
    //  }
    //  return result;
    //}


    #region helpers
    //internal static RollSet GetRollSetById(long fkRollSetId)
    //{
    //  try
    //  {
    //    using (PEUnitOfWork uow = new PEUnitOfWork())
    //    {
    //      RollSet RollSet = uow.Repository<RollSet>().Query(z => z.RollSetId == fkRollSetId).GetSingle();
    //      if (RollSet == null)
    //        return null;
    //      else
    //      {
    //        return RollSet;
    //      }
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    DbExceptionHelper.ProcessException(ex, "GetRollSetById::Database operation failed!", ModuleController.Logger);
    //    return null;
    //  }
    //}
    public virtual DCRollGroovesHistoryData PrepareRollGroovesHistoryData(long rollId, long rollSetHistoryId, long grooveTemplateId)
    {
      DCRollGroovesHistoryData dcLocal = new DCRollGroovesHistoryData();
      dcLocal.RollId = rollId;
      dcLocal.RollSetHistoryId = rollSetHistoryId;
      dcLocal.GrooveTemplateId = grooveTemplateId;
      dcLocal.AccBilletCnt = 0;
      dcLocal.AccWeight = 0;
      dcLocal.ActDiameter = 0;
      dcLocal.GrooveNumber = 1;
      dcLocal.Status = (short)RollGrooveStatus.Active;
      return dcLocal;
    }

    public virtual async Task<DataContractBase> UpdateRollSetAsync(RLSRollSet rec)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRollSet rs = _rollSetHandler.GetRollSetFromId(ctx, rec.RollSetId);
          if (rs != null)
          {
            _rollSetHandler.UpdateRollSet(ctx, ref rs, rec);
            await ctx.SaveChangesAsync();
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollSetSaveError, "Error while updating RollSet.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollSetSaveError, "Error while updating RollSet.");
      }
      return result;
    }
    #endregion
  }
}
