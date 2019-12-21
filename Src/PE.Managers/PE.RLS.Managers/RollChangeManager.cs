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
  public class RollChangeManager : IRollChangeManager
  {
    private readonly IRollChangeManagerSendOffice _sendOffice;

    private readonly RollChangeHandler _rollChangeHandler;
    private readonly RollSetHandler _rollSetHandler;
    private readonly CassetteHandler _cassetteHandler;
    private readonly RollSetHistoryHandler _rollSetHistoryHandler;
    private readonly RollHandler _rollHandler;
    private readonly RollGroovesHistoryHandler _rollGroovesHistoryHandler;



    public RollChangeManager(IRollChangeManagerSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _rollChangeHandler = new RollChangeHandler();
      _rollSetHandler = new RollSetHandler();
      _cassetteHandler = new CassetteHandler();
      _rollSetHistoryHandler = new RollSetHistoryHandler();
      _rollHandler = new RollHandler();
      _rollGroovesHistoryHandler = new RollGroovesHistoryHandler();
    }
    #region members
    //public enum CassetteStatus : short
    //{
    //	Undefined = 0,
    //	New = 1,
    //	Empty = 2,
    //	RollSetInside = 3,
    //	MountedInStand = 4,
    //	InRegeneration = 5,
    //	NotAvailable = 6,
    //	AssembleIncomplete = 7
    //}

    private static short[,] CassetteStatusMatrix = new short[,]
    {
            {0, 0,  1,  0,  0,  0,  1,  0},
            {0, 0,  1,  0,  0,  0,  1,  1},
            {0, 0,  0,  1,  0,  1,  1,  1},
            {0, 0,  1,  0,  1,  0,  0,  1},
            {0, 0,  0,  1,  0,  0,  0,  0},
            {0, 0,  1,  0,  0,  0,  1,  0},
            {0, 0,  1,  0,  0,  1,  0,  0}
    };


    #endregion
    #region public methods
    public virtual async Task<DataContractBase> RollSetToCassetteAction(DCRollSetToCassetteAction dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollSet to Cassette operation. DataContract is empty");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RollSet to Cassette operation. DataContract is empty");
      }
      if (dc.Postion == null)
        dc.Postion = 0;
      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSRollSet rollSet = null;
          if (dc.RollSetIdList != null)  //this if section does not look right. Why do we have dc.RollSetId and dc.RollSetIdList?
          {
            foreach (long rollsetId in dc.RollSetIdList)
            {
              rollSet = _rollSetHandler.GetRollSetFromId(ctx, rollsetId); // uow.Repository<RollSet>().Query(z => z.RollSetId == rollsetId).GetSingle();
              if (rollSet == null)
              {
                NotificationController.Info("Roll Set {0} does not exists.", rollsetId);
                throw new Exception();
                // NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetNotExists, String.Format("Roll Set {0} not exists.", dc.RollSetId), dc.RollSetId);
                //return false;
              }
            }
          }
          else
          {
            dc.RollSetIdList = new List<long?>();
            dc.RollSetIdList.Add(dc.RollSetId);
            //Get RollSet
            rollSet = _rollSetHandler.GetRollSetFromId(ctx, dc.RollSetId);// uow.Repository<RollSet>().Query(z => z.RollSetId == dc.RollSetId).GetSingle();
            if (rollSet == null)
            {
              NotificationController.Info("Roll Set {0} does not exists.", dc.RollSetId);
              throw new Exception();
            }
          }
          //Get Cassette 
          RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, dc.CassetteId); //  uow.Repository<Cassette>().Query(z => z.CassetteId == dc.CassetteId).GetSingle();
          if (cassette == null)
          {
            NotificationController.Info("Cassette {0} does not exists.", dc.CassetteId);
            throw new Exception();
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollSet to Cassette operation.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RollSet to Cassette operation.");
      }
      switch (dc.Action)
      {
        case RollSetCassetteAction.PlanRollSet:
          {
            short i = 1;
            //bool output = false;
            foreach (long element in dc.RollSetIdList)
            {
              //rollSet = _rollSetHandler.GetRollSetFromId(ctx, element); // uow.Repository<RollSet>().Query(z => z.RollSetId == element).GetSingle();
              await RollSetActionforStatusPlanRollset((long)dc.CassetteId, element, i);
              i++;
            }
            await EvaluateCassetteStatusAfterRollSetAction(dc.CassetteId);
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
            return result;
          }
        case RollSetCassetteAction.CancelPlan:
          {
            short i = 1;
            //bool output = false;
            foreach (long element in dc.RollSetIdList)
            {
              //rollSet = _rollSetHandler.GetRollSetFromId(ctx, element); // uow.Repository<RollSet>().Query(z => z.RollSetId == element).GetSingle();
              //RollSetActionforStatusCancelPlan(ref Cassette, ref rollSet, (long)dc.CassetteId, i);
              await RollSetActionforStatusCancelPlan((long)dc.CassetteId, element, i);
              i++;
            }
            await EvaluateCassetteStatusAfterRollSetAction(dc.CassetteId); //TODO check if necessary
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
            return result;
          }
        case RollSetCassetteAction.ConfirmRollSet:
          {
            //bool result = false;
            foreach (long element in dc.RollSetIdList)
            {
              //rollSet = _rollSetHandler.GetRollSetFromId(ctx, element); // uow.Repository<RollSet>().Query(z => z.RollSetId == element).GetSingle();
              //result = RollSetActionforStatusConfirmRollSet(ref Cassette, ref rollSet);
              await RollSetActionforStatusConfirmRollSet((long)dc.CassetteId, element);
            }
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
            return result;
          }
        case RollSetCassetteAction.RemoveRollSet:
          {
            short i = 1;
            //bool result = false;
            foreach (long element in dc.RollSetIdList)
            {
              //rollSet = _rollSetHandler.GetRollSetFromId(ctx, element); // uow.Repository<RollSet>().Query(z => z.RollSetId == element).GetSingle();
              //RollSetActionforStatusRemoveRollset(ref Cassette, ref rollSet, i);
              await RollSetActionforStatusRemoveRollset((long)dc.CassetteId, element, i);
              i++;
            }
            //result = EvaluateCassetteStatusAfterRemoveRollsetAction(Cassette);
            await EvaluateCassetteStatusAfterRemoveRollsetAction(dc.CassetteId);
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
            return result;
          }
        case RollSetCassetteAction.Undefined:
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet Action not recognized");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet Action not recognized");
          }
        default:
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet Action not recognized");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet Action not recognized");
          }
      }
      //return result;
    }


    public virtual async Task<DataContractBase> RollChangeActionAsync(DCRollChangeOperationData dc)
    {
      DataContractBase result = new DataContractBase();
      PEContext ctx = new PEContext();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. DataContract is empty");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. DataContract is empty");
      }

      RollChangeAction rcAction = (RollChangeAction)(dc.Action ?? 0);
      NotificationController.Info("RollChange action: {0}", rcAction);
      //ModuleController.Logger.Info("RollChange action: {0}", rcAction);
      switch (dc.Action)
      {
        case (short)RollChangeAction.SwapCassette:
          {
            await RollChangeActionSwapCassette(dc, ctx);
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
            return result;
          }
        case (short)RollChangeAction.SwapRollSetOnly:
          {
            await RollChangeActionSwapRollSetOnly(dc, ctx);
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
            return result;
          }
        case (short)RollChangeAction.DismountWithCassette:
          {
            await RollChangeActionDismountWithCassette(dc, ctx);
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
            return result;
          }
        case (short)RollChangeAction.DismountRollSetOnly:
          {
            await RollChangeActionDismountRollSetOnly(dc, ctx);
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
            return result;
          }
        case (short)RollChangeAction.MountWithCassette:
          {
            await RollChangeActionMountWithCassette(dc, ctx);
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
            return result;
          }
        case (short)RollChangeAction.MountRollSetOnly:
          {
            await RollChangeActionMountRollSetOnly(dc, ctx);
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
            return result;
          }
        default:
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. DataContract is empty");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. DataContract is empty");
          }
          return result;
      }
    }


    #endregion
    #region private methods
    //public virtual async Task<DataContractBase> RollSetActionforStatusPlanRollset(ref RLSCassette cassette, ref RLSRollSet rollSet, long cassetteId, short position)
    public virtual async Task<DataContractBase> RollSetActionforStatusPlanRollset(long cassetteId, long rollSetId, short position)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, cassetteId);
          RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, rollSetId);

          if (cassette.Status != CassetteStatus.Empty
                      && cassette.Status != CassetteStatus.AssembleIncomplete
                      && cassette.Status != CassetteStatus.RollSetInside
                      && cassette.Status != CassetteStatus.New
                     && (cassette.Status != CassetteStatus.MountedInStand)) // <-
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing Rollhange operation. Cassette status does not allow for this operation");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new Exception();
            //throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette status does not allow for this operation");
          }
          if (rollSet.Status != RollSetStatus.Ready)
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet status does not allow for this operation");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new Exception();
            //throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet status does not allow for this operation");
          }


          //Get Roll Set history record
          RLSRollSetHistory rollSetHistory = _rollSetHistoryHandler.GetRollSetHistory(ctx, rollSet.RollSetId, RollSetHistoryStatus.Actual); // RollSetHistoryDataHandler.GetRollSetHistory(rollSet.RollSetId, PE.Core.Constants.RollSetHistoryStatus.Actual);
          //rollSetHistory = _rollSetHistoryHandler.GetRollSetHistory(ctx, rollSet.RollSetId, RollSetHistoryStatus.Actual); // RollSetHistoryDataHandler.GetRollSetHistory(rollSet.RollSetId, PE.Core.Constants.RollSetHistoryStatus.Actual);
          if (rollSetHistory == null)
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet history does not exist");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new Exception();
            //throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet history does not exist");
          }

          //Prepare Local Data Contract For Update Roll Set History Record
          DCRollSetHistoryData dcRollSetHistoryLocal = new DCRollSetHistoryData();
          dcRollSetHistoryLocal.CassetteId = cassetteId;
          dcRollSetHistoryLocal.PositionInCassette = position;
          dcRollSetHistoryLocal.Dismounted = rollSetHistory.Dismounted;
          dcRollSetHistoryLocal.Id = rollSetHistory.RollSetHistoryId;
          dcRollSetHistoryLocal.Mounted = rollSetHistory.Mounted;
          dcRollSetHistoryLocal.RollSetId = rollSetHistory.FKRollSetId;
          dcRollSetHistoryLocal.Status = rollSetHistory.Status;

          _rollSetHistoryHandler.UpdateRollSetHistory(ctx, ref rollSetHistory, dcRollSetHistoryLocal);
          //Update Roll Set History
          //if (!RollSetHistoryDataHandler.UpdateRollSetHistory(dcRollSetHistoryLocal))
          //{
          //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetUpdateFailed, String.Format("Roll Set History {0} update failed.", rollSet.RollSetName), rollSet.RollSetName);
          //  return false;
          //}

          //Prepare Local Data Contract For Update Roll Set History Record
          //Update Roll Set Status Data

          rollSet.Status = RollSetStatus.ScheduledForCassette;

          await ctx.SaveChangesAsync();
          //if (!RollSetDataHandler.UpdateRollSetStatus(rollSet.RollSetId, PE.Core.Constants.RollSetStatus.ScheduledForCassette))
          //{
          //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetStatusCannotBeChanged, String.Format("Roll Set {0} status cannot be changed.", rollSet.RollSetName), rollSet.RollSetName);
          //  return false;
          //}
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet Action for status PlanRollset Failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet Action for status PlanRollset Failed");
      }
      //evaluate and update cassette status
      //NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetScheduledForMontageInCassette, String.Format("Roll Set {0} has been scheduled for montage in cassette.", rollSet.RollSetName), rollSet.RollSetName);

      return result;
    }
    public virtual async Task<DataContractBase> RollSetActionforStatusCancelPlan(long cassetteId, long rollSetId, short position)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, cassetteId);
          RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, rollSetId);
          //Check roll set
          if (rollSet.Status != RollSetStatus.ScheduledForCassette)
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing Rollhange operation. RollSet status does not allow for this operation");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new Exception();
          }
          //Get Roll Set history record
          RLSRollSetHistory rollSetHistory = _rollSetHistoryHandler.GetRollSetHistory(ctx, rollSet.RollSetId, RollSetHistoryStatus.Actual); //  RollSetHistoryDataHandler.GetRollSetHistory(rollSet.RollSetId, PE.Core.Constants.RollSetHistoryStatus.Actual);
          if (rollSetHistory == null)
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet history does not exist");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new Exception();
          }

          //Prepare Local Data Contract For Update Roll Set History Record
          DCRollSetHistoryData dcRollSetHistoryLocal = new DCRollSetHistoryData();
          dcRollSetHistoryLocal.CassetteId = null;
          dcRollSetHistoryLocal.PositionInCassette = position;
          dcRollSetHistoryLocal.Dismounted = rollSetHistory.Dismounted;
          dcRollSetHistoryLocal.Id = rollSetHistory.RollSetHistoryId;
          dcRollSetHistoryLocal.Mounted = rollSetHistory.Mounted;
          dcRollSetHistoryLocal.RollSetId = rollSetHistory.FKRollSetId;
          dcRollSetHistoryLocal.Status = rollSetHistory.Status;


          _rollSetHistoryHandler.UpdateRollSetHistory(ctx, ref rollSetHistory, dcRollSetHistoryLocal);

          //Update Roll Set History
          //if (!RollSetHistoryDataHandler.UpdateRollSetHistory(dcRollSetHistoryLocal))
          //{
          //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetNotExists, String.Format("Roll Set {0} not exists.", rollSet.RollSetName), rollSet.RollSetName);
          //  ModuleController.Logger.Error(String.Format("Roll Set {0} not exists.", rollSet.RollSetName));
          //  return false;
          //}
          rollSet.Status = RollSetStatus.Ready;
          //Prepare Local Data Contract For Update Roll Set History Record
          //Update Roll Set Status Data
          //if (!RollSetDataHandler.UpdateRollSetStatus(rollSet.RollSetId, PE.Core.Constants.RollSetStatus.Ready))
          //{
          //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetStatusCannotBeChanged, String.Format("Roll Set {0} status cannot be changed.", rollSet.RollSetName), rollSet.RollSetName);
          //  ModuleController.Logger.Error(String.Format("Roll Set {0} status cannot be changed.", rollSet.RollSetName));
          //  return false;
          //}

          //evaluate and update cassette status
          await ctx.SaveChangesAsync();
          //NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetScheduledForMontageInCassette, String.Format("Roll Set {0} has been scheduled for montage in cassette.", rollSet.RollSetName), rollSet.RollSetName);
          //bool result = EvaluateCassetteStatusAfterRollsetAction(cassette);
          await EvaluateCassetteStatusAfterRollSetAction(cassetteId);
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet Action for cancel plan Failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet Action for cancel plan Failed");
      }
      return result;
    }
    //private static bool RollSetActionforStatusConfirmRollSet(ref Cassette cassette, ref RollSet rollSet)
    public virtual async Task<DataContractBase> RollSetActionforStatusConfirmRollSet(long cassetteId, long rollSetId)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, cassetteId);
          RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, rollSetId);
          //Check roll set
          if (rollSet.Status != RollSetStatus.ScheduledForCassette)
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing Rollhange operation. RollSet status does not allow for this operation");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new Exception();
          }

          //Prepare Local Data Contract For Update Roll Set History Record
          //Update Roll Set Status Data

          rollSet.Status = RollSetStatus.ReadyForMounting;
          //if (!RollSetDataHandler.UpdateRollSetStatus(rollSet.RollSetId, PE.Core.Constants.RollSetStatus.ReadyForMounting))
          //{
          //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetStatusCannotBeChanged, String.Format("Roll Set {0} status cannot be changed.", rollSet.RollSetName), rollSet.RollSetName);
          //  ModuleController.Logger.Error(String.Format("Roll Set {0} status cannot be changed.", rollSet.RollSetName));
          //  return false;
          //}

          //Get Roll Set history record
          RLSRollSetHistory rollSetHistory = _rollSetHistoryHandler.GetRollSetHistory(ctx, rollSetId, RollSetHistoryStatus.Actual); // RollSetHistoryDataHandler.GetRollSetHistory(rollSet.RollSetId, PE.Core.Constants.RollSetHistoryStatus.Actual);
          if (rollSetHistory == null)
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet history does not exist");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new Exception();
          }

          await ctx.SaveChangesAsync();
          //change status, but ignore if cassette is already mounter in line (for morgan like cassettes with multiple rollset positions)
          if (cassette.Status != CassetteStatus.MountedInStand)
          {
            //evaluate and update cassette status
            //bool result = EvaluateCassetteStatusAfterRollsetAction(cassette);
            //return result;
            await EvaluateCassetteStatusAfterRollSetAction(cassette.CassetteId);
          }
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet Action for confirm status Failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet Action for confirm status Failed");
      }
      return result;
    }
    //private static bool RollSetActionforStatusRemoveRollset(ref Cassette cassette, ref RollSet rollSet, short position)
    public virtual async Task<DataContractBase> RollSetActionforStatusRemoveRollset(long cassetteId, long rollSetId, short position)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, cassetteId);
          RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, rollSetId);
          if (rollSet.Status != RollSetStatus.ReadyForMounting && rollSet.Status != RollSetStatus.Dismounted && rollSet.Status != RollSetStatus.Mounted)
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollChange operation. Wrong RollSet status");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new Exception();
          }

          //Get Roll Set history record
          RLSRollSetHistory rollSetHistory = _rollSetHistoryHandler.GetRollSetHistory(ctx, rollSetId, RollSetHistoryStatus.Actual); // RollSetHistoryDataHandler.GetRollSetHistory(rollSet.RollSetId, PE.Core.Constants.RollSetHistoryStatus.Actual);
          if (rollSetHistory == null)
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollChange operation. Current RollSet status does not alow for operation");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new Exception();
          }

          //Prepare Local Data Contract For Update Roll Set History Record
          DCRollSetHistoryData dcRollSetHistoryLocal = new DCRollSetHistoryData();
          dcRollSetHistoryLocal.CassetteId = null;
          dcRollSetHistoryLocal.PositionInCassette = position;
          dcRollSetHistoryLocal.Dismounted = rollSetHistory.Dismounted;
          dcRollSetHistoryLocal.Id = rollSetHistory.RollSetHistoryId;
          dcRollSetHistoryLocal.Mounted = rollSetHistory.Mounted;
          dcRollSetHistoryLocal.RollSetId = rollSetHistory.FKRollSetId;
          dcRollSetHistoryLocal.Status = rollSetHistory.Status;


          //Update Roll Set History
          _rollSetHistoryHandler.UpdateRollSetHistory(ctx, ref rollSetHistory, dcRollSetHistoryLocal);
          //if (!RollSetHistoryDataHandler.UpdateRollSetHistory(dcRollSetHistoryLocal))
          //{
          //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetUpdateFailed, String.Format("Roll Set History {0} update failed.", rollSet.RollSetName), rollSet.RollSetName);
          //  ModuleController.Logger.Error(String.Format("Roll Set History {0} update failed.", rollSet.RollSetName));
          //  return false;
          //}

          //Prepare Local Data Contract For Update Roll Set History Record
          rollSet.Status = RollSetStatus.Ready;
          //Update Roll Set Status Data
          //if (!RollSetDataHandler.UpdateRollSetStatus(rollSet.RollSetId, PE.Core.Constants.RollSetStatus.Ready))
          //{
          //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetStatusCannotBeChanged, String.Format("Roll Set {0} status cannot be changed.", rollSet.RollSetName), rollSet.RollSetName);
          //  ModuleController.Logger.Error(String.Format("Roll Set {0} status cannot be changed.", rollSet.RollSetName));
          //  return false;
          //}

          await ctx.SaveChangesAsync();
          //if (cassette.Status != (short)PE.Core.Constants.CassetteStatus.MountedInStand)
          //{
          //  bool result = true;
          //  return result;
          //}
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet Action for remove rollset Failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RollChange operation. RollSet Action for remove rollset Failed");
      }
      return result;
    }
    //private static bool StatusChangeNotPossible(string cassetteName)
    //{
    //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_CassetteStatusCannotBeUpdated, String.Format("Cassette {0} status cannot be updated.", cassetteName), cassetteName);
    //  ModuleController.Logger.Error(String.Format("Cassette {0} status cannot be updated.", cassetteName));
    //  return false;
    //}
    //private static bool StatusChangePossible(string cassetteName)
    //{
    //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_CassetteStatusCanBeUpdated, String.Format("Cassette {0} status can be updated.", cassetteName), cassetteName);
    //  ModuleController.Logger.Info(String.Format("Cassette {0} status can be updated.", cassetteName));
    //  return true;
    //}
    public virtual async Task<DataContractBase> EvaluateCassetteStatusAfterRollSetAction(long? cassetteId)
    {
      DataContractBase result = new DataContractBase();
      //if (cassette != null)
      //{
      //  DCCassetteData dcCassetteDataLocal = new DCCassetteData();
      //  dcCassetteDataLocal.Id = cassette.CassetteId;
      try
      {
        using (PEContext ctx = new PEContext())
        {
          bool AtLeastOneRSisScheduledForAssemble = false;
          bool AllRollsetsAssembled = true;
          bool IsEmpty = true;
          RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, cassetteId);
          //Get rollsets planned of mounted for cassette
          IList<V_RollsetOverviewNewest> RollSetsWithPlannedOfMountedList = _rollSetHandler.GetRollSetFromCassette(ctx, cassette.CassetteId); // GetRollSetFromId uow.Repository<VRollsetOverviewNewest>().Query(z => z.CassetteId == cassette.CassetteId).Get().ToList();
          foreach (V_RollsetOverviewNewest element in RollSetsWithPlannedOfMountedList)
          {
            IsEmpty = false;
            if (element.RollSetStatus == (short)RollSetStatus.ScheduledForCassette)
            {
              AtLeastOneRSisScheduledForAssemble = true;
              AllRollsetsAssembled = false;
              break;
            }
          }
          CassetteStatus OldStatus = cassette.Status;

          if (IsEmpty)

          {
            if (cassette.FKStandId != null)
            {
              // if  you dismount rollset  from cassette (when cassette is mounted in Stand)
              cassette.Status = CassetteStatus.MountedInStand;
            }
            else
            {
              cassette.Status = CassetteStatus.Empty;
            }

          }
          else if (AtLeastOneRSisScheduledForAssemble)
          {
            cassette.Status = CassetteStatus.AssembleIncomplete;
          }
          else if (AllRollsetsAssembled)
          {
            cassette.Status = CassetteStatus.RollSetInside;
          }
          //return CassetteDataHandler.UpdateCassetteStatus(dcCassetteDataLocal);
          await ctx.SaveChangesAsync();
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette Evaluation failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette Evaluation failed");
      }
      return result;
    }


    public virtual async Task<DataContractBase> EvaluateCassetteStatusAfterRemoveRollsetAction(long? cassetteId)
    {
      DataContractBase result = new DataContractBase();

      //if (cassette != null)
      //{
      //  DCCassetteData dcCassetteDataLocal = new DCCassetteData();
      //  dcCassetteDataLocal.Id = cassette.CassetteId;
      try
      {
        using (PEContext ctx = new PEContext())
        {
          bool AtLeastOneRSisScheduledForAssemble = false;
          bool AllRollsetsAssembled = true;
          bool IsEmpty = true;

          RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, cassetteId);

          //Get rollsets planned of mounted for cassette
          IList<V_RollsetOverviewNewest> RollSetsWithPlannedOfMountedList = _rollSetHandler.GetRollSetFromCassette(ctx, cassetteId);
          //IList<VRollsetOverviewNewest> RollSetsWithPlannedOfMountedList = uow.Repository<VRollsetOverviewNewest>().Query(z => z.CassetteId == cassette.CassetteId).Get().ToList();
          foreach (V_RollsetOverviewNewest element in RollSetsWithPlannedOfMountedList)
          {
            IsEmpty = false;
            if (element.RollSetStatus == (short)RollSetStatus.ScheduledForCassette)
            {
              AtLeastOneRSisScheduledForAssemble = true;
              AllRollsetsAssembled = false;
              break;
            }
          }
          CassetteStatus OldStatus = cassette.Status;

          if (IsEmpty)
          {
            cassette.Status = CassetteStatus.Empty;
          }
          else if (AtLeastOneRSisScheduledForAssemble)
          {
            cassette.Status = CassetteStatus.AssembleIncomplete;
          }
          else if (AllRollsetsAssembled)
          {
            cassette.Status = CassetteStatus.RollSetInside;
          }
          //return CassetteDataHandler.UpdateCassetteStatus(dcCassetteDataLocal);
          await ctx.SaveChangesAsync();
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette Evaluation after remove rollset action failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette Evaluation after remove rollset action failed");
      }
      return result;
    }

    public virtual async Task<DataContractBase> RollChangeActionSwapCassette(DCRollChangeOperationData dc, PEContext ctx)
    {
      DataContractBase result = new DataContractBase();
      //verify arrangement
      if (dc.Arrangement == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Arrangement is not defined");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Arrangement is not defined");
      }
      if (dc.ArrangementNew == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Arrangement is not defined");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Arrangement is not defined");
      }

      //verify stand
      RLSStand stand = _rollHandler.GetStandFromStandNo(ctx, dc.StandNo ?? 0); //  RollChangeManager.GetStandByStandNo(dc.StandNo ?? 0);
      if (stand.StandNo < 1)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Stand is not specified.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Stand is not specified.");
      }

      //CASETTE TO BE MOUNTED
      //verify is cassette exists
      if (dc.MountedCassette == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
      }

      //get cassette
      RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, dc.MountedCassette); // CassetteDataHandler.GetCassetteById((long)dc.MountedCassette);
      if (cassette == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
      }

      //check if cassette status is correct
      if (cassette.Status != CassetteStatus.RollSetInside && cassette.Status != CassetteStatus.Empty)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette Status does not allow this operation.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette Status does not allow this operation.");
      }

      //CASSETTE to BE DISMOUNTED
      //verify if cassette to be dismounted is valid
      if (dc.DismountedCassette == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
      }
      //get cassette
      RLSCassette dismountedCassette = _cassetteHandler.GetCassetteFromId(ctx, dc.DismountedCassette); // CassetteDataHandler.GetCassetteById((long)dc.DismountedCassette);
      if (dismountedCassette == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
      }

      //check if cassette status is correct
      if (dismountedCassette.Status != CassetteStatus.MountedInStand)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette Status does not allow this operation.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette Status does not allow this operation.");
      }

      //NOT USED IN VAL CONDITIONS--------------------------------------------------------------------------------------------------------------------------------------------------------------
      //-------------------------------------
      //DISMOUNT ALL ROLLSETS FROM REMOVED CASSETTE
      //-------------------------------------

      return result;
    }
    public virtual async Task<DataContractBase> RollChangeActionSwapRollSetOnly(DCRollChangeOperationData dc, PEContext ctx)
    {
      DataContractBase result = new DataContractBase();
      //get cassette
      RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, dc.MountedCassette ?? 0); // CassetteDataHandler.GetCassetteById(dc.MountedCassette ?? 0);
      if (cassette == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
      }

      //check if cassette status is correct
      if (cassette.Status != CassetteStatus.MountedInStand)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette Status does not allow this operation.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette Status does not allow this operation.");
      }

      //verify position
      if (dc.Position == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Position can not be empty.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Position can not be empty.");
      }

      //-------------------------------------
      //VERIFY ROLLSET WHICH WILL BE DISMOUNTED
      //-------------------------------------
      //verify is rollset exists
      if (dc.DismountedRollSet == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet does not exist.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet does not exist.");
      }
      RLSRollSet rsToBeDisMounted = _rollSetHandler.GetRollSetFromId(ctx, dc.DismountedRollSet); // RollSetDataHandler.GetRollSetById((long)dc.DismountedRollSet);
      if (rsToBeDisMounted == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet does not exist.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet does not exist.");
      }
      //check if rollset is in status "mounted"
      if (rsToBeDisMounted.Status != RollSetStatus.Mounted)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Roll Set status cannot be changed.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Roll Set status cannot be changed.");
      }

      //check if rollset has cassette assigned
      RLSRollSetHistory rollSetHistoryToDismount = _rollSetHistoryHandler.GetRollSetHistory(ctx, rsToBeDisMounted.RollSetId, RollSetHistoryStatus.Actual); // RollSetHistoryDataHandler.GetRollSetHistory(rsToBeDisMounted.RollSetId, PE.Core.Constants.RollSetHistoryStatus.Actual);
      if (rollSetHistoryToDismount == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Roll Set histories are empty.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Roll Set histories are empty.");
      }
      if (rollSetHistoryToDismount.FKCassetteId == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
      }

      //-------------------------------------
      //VERIFY ROLLSET WHICH WILL BE MOUNTED
      //-------------------------------------

      //verify is rollset exists
      if (dc.MountedRollSet == null)
      {
        return result;
      }
      RLSRollSet rsToBeMounted = _rollSetHandler.GetRollSetFromId(ctx, dc.MountedRollSet); // RollSetDataHandler.GetRollSetById(dc.MountedRollSet ?? -1);
      if (rsToBeMounted == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet does not exist.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet does not exist.");
      }
      //check if rollset is in status "mounted"
      if (rsToBeMounted.Status != RollSetStatus.Ready && rsToBeMounted.Status != RollSetStatus.Dismounted)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Wrong RollSet status.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Wrong RollSet status.");
      }

      //check if rollset has cassette assigned
      RLSRollSetHistory rollSetHistoryToMount = _rollSetHistoryHandler.GetRollSetHistory(ctx, rsToBeMounted.RollSetId, RollSetHistoryStatus.Actual); //  RollSetHistoryDataHandler.GetRollSetHistory(rsToBeMounted.RollSetId, PE.Core.Constants.RollSetHistoryStatus.Actual);
      if (rollSetHistoryToMount == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet history does not exist.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet history does not exist.");
      }

      //---------------------------------------------
      //EXECUTE SWAP ROLL CHANGE
      //---------------------------------------------

      //dismount rollset, set status to dismounted

      // _rollSetHandler.UpdateRollSet(ctx, rsToBeDisMounted)
      //if (!RollSetDataHandler.UpdateRollSet(rsToBeDisMounted))
      //{
      //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetStatusCannotBeChanged, String.Format("Roll Set {0} status cannot be changed.", rsToBeDisMounted.RollSetName), rsToBeDisMounted.RollSetName);
      //  return false;
      //}
      //NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetDismounted, String.Format("Roll Set {0} has been dismounted.", rsToBeDisMounted.RollSetName), rsToBeDisMounted.RollSetName);


      //check if this stand and position is free
      List<V_RollsetInCassettes> alreadyInstalled = _rollSetHistoryHandler.GetInstalledRollsetInCassette(ctx, cassette.CassetteId, dc.Position ?? 0); // RollSetHistoryDataHandler.GetInstalledRollsetInCassette(cassette.CassetteId, dc.Position ?? 0);
      //if (alreadyInstalled != null && alreadyInstalled.Count > 0)
      //{
      //  NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. There is already installed rollset in this cassette and position. Can not install another one on the same position!.");
      //  NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
      //  throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.There is already installed rollset in this cassette and position. Can not install another one on the same position!.");
      //  //ModuleController.Logger.Error("There is already installed rollset in this cassette and position. Can not install another one on the same position!");
      //  //return false;
      //}
      try
      {
        rsToBeDisMounted.Status = RollSetStatus.Dismounted;
        rollSetHistoryToDismount.Dismounted = DateTime.Now;
        rollSetHistoryToDismount.LastUpdateTs = DateTime.Now;
        rollSetHistoryToDismount.FKCassetteId = null;

        //mount rollset
        rsToBeMounted.Status = RollSetStatus.Mounted;
        //if (RollSetDataHandler.UpdateRollSet(rsToBeMounted) != true)
        //{
        //  return false;
        //}

        rollSetHistoryToMount.PositionInCassette = dc.Position;
        rollSetHistoryToMount.FKCassetteId = cassette.CassetteId;
        rollSetHistoryToMount.Mounted = DateTime.Now;
        rollSetHistoryToMount.Dismounted = null;

        //assign weight limit based on rollset type (RM and IM)
        SetSpecificAccWeightLimitPerRollset(ref rollSetHistoryToMount, rsToBeMounted, dc);

        //if (!RollSetHistoryDataHandler.UpdateRollSetHistory(rollSetHistoryToMount))
        //{
        //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_CannotUpdateRollSetHistory, String.Format("Cannot update roll set history."));
        //  ModuleController.Logger.Error("Cannot update roll set history.");
        //  return false;
        //}
        //NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetMounted, String.Format("Roll Set {0} has been mounted.", rsToBeMounted.RollSetName), rsToBeMounted.RollSetName);
        //return true;

        await ctx.SaveChangesAsync();
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. .");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.");
      }
      return result;
    }
    public virtual async Task<DataContractBase> RollChangeActionDismountWithCassette(DCRollChangeOperationData dc, PEContext ctx)
    {
      DataContractBase result = new DataContractBase();
      //verify stand
      RLSStand stand = _rollHandler.GetStandFromStandNo(ctx, dc.StandNo ?? 0); // RollChangeManager.GetStandByStandNo(dc.StandNo ?? 0);
      if (stand == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Invalid stand No.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Invalid stand No.");
      }

      //get cassette
      RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, dc.DismountedCassette); // CassetteDataHandler.GetCassetteById(dc.DismountedCassette ?? 0);
      if (cassette == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found.");
      }

      //update cassette record
      try
      {
        List<V_RollsetOverviewNewest> rollSetList = _rollSetHandler.GetRollSetFromCassette(ctx, dc.DismountedCassette);
        if(rollSetList!=null)
        {
          foreach(V_RollsetOverviewNewest rollset in rollSetList)
          {
            RLSRollSetHistory singleRollSetHistory = _rollSetHistoryHandler.GetRollSetHistory(ctx, rollset.RollSetId, RollSetHistoryStatus.Actual);

            singleRollSetHistory.Dismounted = DateTime.Now;
            //singleRollSetHistory.Mounted = null;
            //singleRollSetHistory.PositionInCassette = null;
            //singleRollSetHistory.FKCassetteId = null;
            await ctx.SaveChangesAsync();
          }
        }
        cassette.Status = CassetteStatus.RollSetInside;
        cassette.FKStandId = null;
        await ctx.SaveChangesAsync();
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.");
      }
      return result;
      //if (!CassetteDataHandler.UpdateCassette(cassette))
      //{
      //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_CassetteCannotBeUpdated, String.Format("Cassette {0} cannot be updated.", dc.CassetteName), dc.CassetteName);
      //  ModuleController.Logger.Error("Cassette {0} cannot be updated.", dc.CassetteName);
      //  return false;
      //}
      //NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_CassetteDismounted, String.Format("Cassette {0} has been dismounted from stand {1}.", cassette.CassetteName, stand.StandNo), cassette.CassetteName, stand.StandNo);
      //ModuleController.Logger.Info("Cassette {0} has been dismounted from stand {1}.", cassette.CassetteName, stand.StandNo);
      //return true;
    }
    public virtual async Task<DataContractBase> RollChangeActionDismountRollSetOnly(DCRollChangeOperationData dc, PEContext ctx)
    {
      DataContractBase result = new DataContractBase();
      //verify roll set
      if (dc.DismountedRollSet == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation.RollSetId is invalid");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.RollSetId is invalid");
      }
      RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, dc.DismountedRollSet); // RollSetDataHandler.GetRollSetById(dc.DismountedRollSet ?? 0);
      if (rollSet == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet not found");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.RollSet not found");
      }
      //check if rollset is in status "mounted"
      if (rollSet.Status != RollSetStatus.Mounted)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet status is invalid");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.RollSet status is invalid");
      }

      //check if rollset has cassette assigned
      RLSRollSetHistory rollSetHistory = _rollSetHistoryHandler.GetRollSetHistory(ctx, (long)dc.DismountedRollSet, RollSetHistoryStatus.Actual); //  RollSetHistoryDataHandler.GetRollSetHistory((dc.DismountedRollSet ?? 0), PE.Core.Constants.RollSetHistoryStatus.Actual);
      if (rollSetHistory == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSetHistory not found");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.RollSetHistory not found");
      }

      //get cassette
      RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, dc.MountedCassette); // CassetteDataHandler.GetCassetteById(dc.MountedCassette ?? 0);
      if (cassette == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.Cassette not found");
      }

      //check if cassette status is correct
      if ((rollSet.RollSetType == RollSetType.TwoActiveRollsIM) || (rollSet.RollSetType == RollSetType.TwoActiveRollsRM))
      {
        if (cassette.Status != CassetteStatus.MountedInStand && cassette.Status != CassetteStatus.AssembleIncomplete)
        {
          NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Wrong Cassette status");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Wrong Cassette status");
        }
      }

      List<RLSRollGroovesHistory> groovesHistory = _rollGroovesHistoryHandler.GetRollGrooveHistoryFromRollSetHistoryId(ctx, rollSetHistory.RollSetHistoryId);

      if (groovesHistory == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Grooves history not found");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.Grooves history not found");
      }

      try
      {
        _rollGroovesHistoryHandler.InactiveAllGrooves(ctx, ref groovesHistory);

        //set status to dismounted
        rollSet.Status = RollSetStatus.Dismounted;
        //if (!RollSetDataHandler.UpdateRollSet(rollSet))
        //{
        //  ModuleController.Logger.Error("Can not update RollSet record. Operation failed.");
        //  return false;
        //}
        //update dismount time in rollsethistory

        cassette.Status = CassetteStatus.MountedInStand;

        rollSetHistory.Dismounted = DateTime.Now;
        rollSetHistory.Mounted = null;
        rollSetHistory.PositionInCassette = null;
        rollSetHistory.FKCassetteId = null;
        await ctx.SaveChangesAsync();
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.");
      }
      //rollSetHistory.FkInterCassetteId = null;
      //if (!RollSetHistoryDataHandler.UpdateRollSetHistory(rollSetHistory))
      //{
      //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_CannotUpdateRollSetHistory, String.Format("Can not update RollSetHistory record. Operation failed"));
      //  ModuleController.Logger.Error("Can not update RollSetHistory record. Operation failed.");
      //  return false;
      //}

      //NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetDismounted, String.Format("Roll Set {0} has been dismounted.", rollSet.RollSetName), rollSet.RollSetName);
      //return true;
      return result;
    }
    public virtual async Task<DataContractBase> RollChangeActionMountWithCassette(DCRollChangeOperationData dc, PEContext ctx)
    {
      DataContractBase result = new DataContractBase();
      RLSStand stand = _rollHandler.GetStandFromStandNo(ctx, dc.StandNo ?? 0); // RollChangeManager.GetStandByStandNo(dc.StandNo ?? 0);
      if (stand == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Stand not found");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Stand not found");
      }

      if (dc.MountedCassette == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette id missing");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.Cassette id missing");
      }
      //get cassette
      RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, dc.MountedCassette); // CassetteDataHandler.GetCassetteById(dc.MountedCassette ?? 0);
      if (cassette == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.Cassette not found");
      }
      //check if cassette status is correct
      if ((cassette.Status != CassetteStatus.RollSetInside) && (cassette.Status != CassetteStatus.Empty) && (cassette.Status != CassetteStatus.AssembleIncomplete))
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette in invalid state");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette in invalid state");
      }
      //check if stand no is not occupied with another cassetee
      List<V_CassettesInStands> cassInStand = _cassetteHandler.GetCassetteInStand(ctx, stand.StandNo);
      //List<VCassettesInStand> cassInStand = uow.Repository<VCassettesInStand>().Query(q => q.StandNo == stand.StandNo && q.RollsetHistoryStatus == (short)PE.Core.Constants.RollSetHistoryStatus.Actual && q.CassetteStatus == (short)PE.Core.Constants.CassetteStatus.MountedInStand).Get().ToList();
      if (cassInStand != null && cassInStand.Count > 0)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette in invalid stand");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette in invalid stand");
      }
      try
      {
        cassette.Status = CassetteStatus.MountedInStand;
        if(dc.Arrangement!=null) cassette.Arrangement = (CassetteArrangement)dc.Arrangement;
        cassette.FKStandId = stand.StandId;
        //required rollSet status needs verification
        //List<V_RollsetOverviewNewest> list = ctx.V_RollsetOverviewNewest.Where(p => p.CassetteId == dc.MountedCassette && p.RollSetStatus == (short)RollSetStatus.ReadyForMounting).ToList();
        List<V_RollsetOverviewNewest> list = ctx.V_RollsetOverviewNewest.Where(p => p.CassetteId == dc.MountedCassette ).ToList();
        if (list != null)
        {
          foreach (V_RollsetOverviewNewest rollSetOverview in list)
          {
            RLSRollSetHistory history = _rollSetHistoryHandler.GetRollSetHistory(ctx, rollSetOverview.RollSetId, RollSetHistoryStatus.Actual);
            if(history!=null)
            {
              history.PositionInCassette = dc.Position;
              history.FKCassetteId = cassette.CassetteId;
              history.Mounted = DateTime.Now;
              history.Dismounted = null;
            }

            RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, rollSetOverview.RollSetId);
            if(rollSet!=null)
            {
              rollSet.Status = RollSetStatus.Mounted;
            }
            await ctx.SaveChangesAsync();
          }
        }
        await ctx.SaveChangesAsync();
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.");
      }
      //if (!CassetteDataHandler.UpdateCassette(cassette))
      //{
      //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_CassetteStatusCannotBeUpdated, String.Format("Error occured while mounting cassette {0}, opearation failed.", cassette.CassetteName), cassette.CassetteName);
      //  return false;
      //}
      //NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_CassetteMounted, String.Format("Cassette {0} has been mounter at stand {1}. Opearation completed.", cassette.CassetteName, stand.StandNo), cassette.CassetteName, stand.StandNo);
      return result;
    }

    public virtual async Task<DataContractBase> RollChangeActionMountRollSetOnly(DCRollChangeOperationData dc, PEContext ctx)
    {
      DataContractBase result = new DataContractBase();
      if (dc.MountedRollSet == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Invalid RollSet id");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Invalid RollSet id");
      }
      RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, dc.MountedRollSet); // RollSetDataHandler.GetRollSetById(dc.MountedRollSet ?? 0);
      if (rollSet == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet does not exist");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet does not exist");
      }
      //check rollset status is ready
      if ((rollSet.Status != RollSetStatus.Ready) && (rollSet.Status != RollSetStatus.Dismounted))
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet status does not allow operation");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet status does not allow operation");
      }

      //check if rollset has cassette assigned
      RLSRollSetHistory rollSetHistory = _rollSetHistoryHandler.GetRollSetHistory(ctx, rollSet.RollSetId, RollSetHistoryStatus.Actual); //RollSetHistoryDataHandler.GetRollSetHistory(rollSet.RollSetId, PE.Core.Constants.RollSetHistoryStatus.Actual);
      if (rollSetHistory == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet history not found");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. RollSet history not found");
      }

      //check if rollset is already mounted in cassette
      if (dc.MountedCassette == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette Id is null");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette Id is null");
      }

      //get cassette
      RLSCassette cassette = _cassetteHandler.GetCassetteFromId(ctx, dc.MountedCassette); // CassetteDataHandler.GetCassetteById(dc.MountedCassette ?? 0);
      if (cassette == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Cassette not found");
      }

      //check if cassette status is correct
      if ((rollSet.RollSetType == RollSetType.TwoActiveRollsIM) || (rollSet.RollSetType == RollSetType.TwoActiveRollsRM))
      {
        if (cassette.Status != CassetteStatus.MountedInStand && cassette.Status != CassetteStatus.AssembleIncomplete)
        {
          NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Wrong Cassette status");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Wrong cassette status");
        }
      }
      if (dc.Position == null && cassette.NumberOfPositions == 1)
        dc.Position = 1;
      //verify position
      if (dc.Position == null)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Wrong position");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Wrong position");
      }
      short position = dc.Position ?? 0;
      if (position < 1 || position > cassette.NumberOfPositions)
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation. Wrong position in Cassette ");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation. Wrong position in cassette");
      }

      try
      {
        //mount rollset
        rollSet.Status = RollSetStatus.Mounted;
        //if (RollSetDataHandler.UpdateRollSet(rollSet) != true)
        //{
        //  return false;
        //}

        rollSetHistory.PositionInCassette = dc.Position;
        rollSetHistory.FKCassetteId = cassette.CassetteId;
        rollSetHistory.Mounted = DateTime.Now;
        rollSetHistory.Dismounted = null;

        //assign weight limit based on rollset type (RM and IM)
        //SetSpecificAccWeightLimitPerRollset(ref rollSetHistory, dc);
        SetSpecificAccWeightLimitPerRollset(ref rollSetHistory, rollSet, dc);
        await ctx.SaveChangesAsync();

        //if (!RollSetHistoryDataHandler.UpdateRollSetHistory(rollSetHistory))
        //{
        //  NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_CannotUpdateRollSetHistory, String.Format("Cannot update roll set history."));
        //  ModuleController.Logger.Error("Cannot update roll set history.");
        //  return false;
        //}
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while performing RolChange operation.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollChanegOperationError, "Error while performing RolChange operation.");
      }
      return result;
      //NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_RollSetMountedInCassette, String.Format("Roll set {0} has been mounted in cassette {1}.", rollSet.RollSetName, cassette.CassetteName), rollSet.RollSetName, cassette.CassetteName);
      //ModuleController.Logger.Info("Roll set {0} has been mounted in cassette {1}.", rollSet.RollSetName, cassette.CassetteName);
      //return true;
    }

    //Determine weight limit for a rollset (project specific)
    //implemented for Valbruna BWRM project!
    public void SetSpecificAccWeightLimitPerRollset(ref RLSRollSetHistory rsh, RLSRollSet rollSet, DCRollChangeOperationData dcRollChange)
    {
      //DataContractBase result = new DataContractBase();
      //get roll rollset
      //RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(rsh.FKRollSetId);// RollSetDataHandler.GetRollSetById(rsh.FkRollSetId);
      //if (rollSet == null)
      //{
      //  ModuleController.Logger.Info("Can not find rollset while setting rolling wieight limits");
      //}
      rsh.AccWeightLimit = 0; //default
                              //handle RM and IM rolls
      if (rollSet.RollSetType == RollSetType.TwoActiveRollsRM || rollSet.RollSetType == RollSetType.TwoActiveRollsIM)
      {
        switch (dcRollChange.StandNo) //direct reference to data in LRStands
        {
          case 1: //RM Stand 1
            {
              rsh.AccWeightLimit = 18000000.0;
              break;
            }
          case 2: //RM Stand 2
            {
              rsh.AccWeightLimit = 16000000.0;
              break;
            }
          case 3: //RM Stand 3
            {
              rsh.AccWeightLimit = 10000000.0;
              break;
            }
          case 4: //RM Stand 4
            {
              rsh.AccWeightLimit = 7000000.0;
              break;
            }
          case 5: //RM Stand 5
            {
              rsh.AccWeightLimit = 8000000.0;
              break;
            }
          case 6: //RM Stand 6
            {
              rsh.AccWeightLimit = 8000000.0;
              break;
            }
          case 7: //IM Stand 1
            {
              rsh.AccWeightLimit = 7000000.0;
              break;
            }
          case 8: //IM Stand 2
            {
              rsh.AccWeightLimit = 7000000.0;
              break;
            }
          case 9: //IM Stand 3
            {
              rsh.AccWeightLimit = 7000000.0;
              break;
            }
          case 10: //IM Stand 4
            {
              rsh.AccWeightLimit = 7000000.0;
              break;
            }
        }
      }

      return;
    }



    /// <summary>
    /// Activates first (and the only one expected) groove on each rollset mounted in given Casstte
    /// </summary>
    /// <param name="cassetteId">PK ID from LRCassttes table</param>
    /// <param name="uow"></param>
    /// <returns></returns>
    //private static bool ActivateDefaultGroove(long cassetteId, ref PEUnitOfWork uow)
    //{
    //  //get intercassette
    //  Cassette cassette = CassetteDataHandler.GetCassetteById(cassetteId);
    //  if (cassette == null)
    //  {
    //    NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_CassetteNotFound, String.Format("Cassette id is invalid {0} does not exist.", cassetteId), cassetteId);
    //    ModuleController.Logger.Error("Cassette id is invalid {0} does not exist.", cassetteId);
    //    return false;
    //  }

    //  //check if cassette status is correct
    //  if (cassette.Status != (short)PE.Core.Constants.CassetteStatus.MountedInStand)
    //  {
    //    NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_CassetteStatusNotAllow, String.Format("Cassette {0} in invalid state, opearation is not possible.", cassette.CassetteName), cassette.CassetteName);
    //    ModuleController.Logger.Error("Cassette {0} in invalid state, opearation is not possible.", cassette.CassetteName);
    //    return false;
    //  }

    //  //get all actual RollsetHistory records
    //  List<VRollsetOverviewNewest> rshList = uow.Repository<VRollsetOverviewNewest>().Query(q => q.CassetteId == cassette.CassetteId && q.CassetteStatus == (short)PE.Core.Constants.CassetteStatus.MountedInStand && q.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Mounted).Get().ToList();
    //  if (rshList != null && rshList.Count() > 0)
    //  {
    //    foreach (VRollsetOverviewNewest rec in rshList)
    //    {
    //      if (!RollGroovesHistoryDataHandler.ActivateGroove(rec.RollSetHistoryId, 1, ref uow))
    //      {
    //        ModuleController.Logger.Error("Error occured while activationg groove {1} for in Rollset {2}", 1, rec.RollSetName);
    //        return false;
    //      }
    //    }
    //  }
    //  return true;
    //}
    #endregion
    #region helpers


    #endregion
  }
}
