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
  public class CustomRollGroovesManager : ICustomRollGroovesManager
  {
    private readonly ICustomRollGroovesManagerSendOffice _sendOffice;

    //private readonly CassetteHandler _cassetteHandler;
    private readonly RollSetHistoryHandler _rollSetHistoryHandler;
    private readonly RollSetHandler _rollSetHandler;
    private readonly RollGroovesHistoryHandler _rollGroovesHistoryHandler;


    public CustomRollGroovesManager(ICustomRollGroovesManagerSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      //_cassetteHandler = new CassetteHandler();
      _rollSetHistoryHandler = new RollSetHistoryHandler();
      _rollSetHandler = new RollSetHandler();
      _rollGroovesHistoryHandler = new RollGroovesHistoryHandler();
    }

    // For RM & IM Commented out 07.10.2019. Method not used
    //public static int VerifyAndCreateRollGrooves(DCRollSetGrooveSetup dc, Roll upperRoll, Roll bottomRoll, RollSetHistory rollSetHistory)
    //{
    //  if (dc.GrooveSetupList != null)
    //  {
    //    foreach (SingleGrooveSetup SingleGrooveSetup in dc.GrooveSetupList)
    //    {
    //      if (SingleGrooveSetup.GrooveIdx < 0 || SingleGrooveSetup.GrooveIdx > MaxGrooveIdx)
    //      {
    //        ModuleController.Logger.Error(String.Format("Invalid Groove Number {0}.", SingleGrooveSetup.GrooveIdx));
    //        NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_InvalidGrooveNumber, String.Format("Invalid Groove Number {0}.", SingleGrooveSetup.GrooveIdx), SingleGrooveSetup.GrooveIdx);
    //        return -1;
    //      }
    //      if (SingleGrooveSetup.GrooveIdx == 0)
    //      {
    //        if (SingleGrooveSetup.GrooveIdx < 0 || SingleGrooveSetup.GrooveIdx > MaxGrooveIdx)
    //        {
    //          ModuleController.Logger.Error(String.Format("Invalid Groove Template For Groove Number {0}.", SingleGrooveSetup.GrooveIdx));
    //          NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_InvalidGrooveTemplateForNumber, String.Format("Invalid Groove Template For Groove Number {0}.", SingleGrooveSetup.GrooveIdx), SingleGrooveSetup.GrooveIdx);
    //          return -1;
    //        }
    //      }
    //      if (SingleGrooveSetup.GrooveStatus > (short)PE.Core.Constants.RollGrooveStatus.PlannedAndNotAvailable || SingleGrooveSetup.GrooveStatus < (short)PE.Core.Constants.RollGrooveStatus.PlannedAndNoChange)
    //      {
    //        NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_InvalidGrooveStatus, String.Format("Invalid status {0} for groove number {1}.", SingleGrooveSetup.GrooveStatus, SingleGrooveSetup.GrooveIdx), SingleGrooveSetup.GrooveStatus, SingleGrooveSetup.GrooveIdx);
    //        return -1;
    //      }
    //    }

    //    //Create all Grooves
    //    foreach (SingleGrooveSetup SingleGrooveSetup in dc.GrooveSetupList)
    //    {
    //      PE.Core.Constants.RollSetType rollSetType = new PE.Core.Constants.RollSetType();
    //      PEUnitOfWork uow = new PEUnitOfWork();
    //      RollSet rollSet = uow.Repository<RollSet>().Query(z => z.RollSetId == rollSetHistory.FkRollSetId).GetSingle();
    //      rollSetType = (PE.Core.Constants.RollSetType)rollSet.IsThirdRoll;
    //      //Upper roll
    //      if (upperRoll != null)
    //      {
    //        if (rollSetHistory != null)
    //        {
    //          DCRollGroovesHistoryData dcLocalUpper = new DCRollGroovesHistoryData();
    //          dcLocalUpper.RollId = upperRoll.RollId;
    //          dcLocalUpper.GrooveNumber = SingleGrooveSetup.GrooveIdx;
    //          dcLocalUpper.Status = SingleGrooveSetup.GrooveStatus;
    //          dcLocalUpper.ActDiameter = (float)upperRoll.ActualDiameter;
    //          dcLocalUpper.GrooveTemplateId = SingleGrooveSetup.GrooveTemplate;
    //          dcLocalUpper.RollSetHistoryId = rollSetHistory.RollSetHistoryId;
    //          if (!RollGroovesHistoryDataHandler.InsertRollGroovesHistory(dcLocalUpper))
    //          {
    //            ModuleController.Logger.Error(String.Format("Error while created planned groove on roll {0}.", dcLocalUpper.RollId));
    //            NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_InvalidGrooveStatus, String.Format("Error while created planned groove on roll {0}.", dcLocalUpper.RollId), dcLocalUpper.RollId);
    //            return -1;
    //          }
    //        }
    //        else
    //        {
    //          DCRollGroovesHistoryData dcLocalUpper = new DCRollGroovesHistoryData();
    //          dcLocalUpper.RollId = upperRoll.RollId;
    //          dcLocalUpper.GrooveNumber = SingleGrooveSetup.GrooveIdx;
    //          dcLocalUpper.Status = SingleGrooveSetup.GrooveStatus;
    //          dcLocalUpper.ActDiameter = (float)upperRoll.ActualDiameter;
    //          dcLocalUpper.GrooveTemplateId = SingleGrooveSetup.GrooveTemplate;
    //          if (!RollGroovesHistoryDataHandler.InsertRollGroovesHistory(dcLocalUpper))
    //          {
    //            ModuleController.Logger.Error(String.Format("Error while created planned groove on roll {0}.", dcLocalUpper.RollId));
    //            NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_InvalidGrooveStatus, String.Format("Error while created planned groove on roll {0}.", dcLocalUpper.RollId), dcLocalUpper.RollId);
    //            return -1;
    //          }
    //        }
    //      }
    //      //Bottom roll
    //      if (upperRoll != null)
    //      {
    //        if (rollSetHistory != null)
    //        {
    //          DCRollGroovesHistoryData dcLocalBottom = new DCRollGroovesHistoryData();
    //          dcLocalBottom.RollId = bottomRoll.RollId;
    //          dcLocalBottom.GrooveNumber = SingleGrooveSetup.GrooveIdx;
    //          dcLocalBottom.Status = SingleGrooveSetup.GrooveStatus;
    //          dcLocalBottom.ActDiameter = (float)bottomRoll.ActualDiameter;
    //          dcLocalBottom.GrooveTemplateId = SingleGrooveSetup.GrooveTemplate;
    //          dcLocalBottom.RollSetHistoryId = rollSetHistory.RollSetHistoryId;
    //          if (!RollGroovesHistoryDataHandler.InsertRollGroovesHistory(dcLocalBottom))
    //          {
    //            ModuleController.Logger.Error(String.Format("Error while created planned groove on roll {0}.", dcLocalBottom.RollId));
    //            NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_InvalidGrooveStatus, String.Format("Error while created planned groove on roll {0}.", dcLocalBottom.RollId), dcLocalBottom.RollId);
    //            return -1;
    //          }
    //        }
    //        else
    //        {
    //          DCRollGroovesHistoryData dcLocalBottom = new DCRollGroovesHistoryData();
    //          dcLocalBottom.RollId = bottomRoll.RollId;
    //          dcLocalBottom.GrooveNumber = SingleGrooveSetup.GrooveIdx;
    //          dcLocalBottom.Status = SingleGrooveSetup.GrooveStatus;
    //          dcLocalBottom.ActDiameter = (float)bottomRoll.ActualDiameter;
    //          dcLocalBottom.GrooveTemplateId = SingleGrooveSetup.GrooveTemplate;
    //          if (!RollGroovesHistoryDataHandler.InsertRollGroovesHistory(dcLocalBottom))
    //          {
    //            ModuleController.Logger.Error(String.Format("Error while created planned groove on roll {0}.", dcLocalBottom.RollId));
    //            NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_InvalidGrooveStatus, String.Format("Error while created planned groove on roll {0}.", dcLocalBottom.RollId), dcLocalBottom.RollId);
    //            return -1;
    //          }
    //        }
    //      }
    //      ////Third roll
    //      //if ((rollSetType == PE.Core.Constants.NumberOfActiveRoll.threeActiveRollsK370) || (rollSetType == PE.Core.Constants.NumberOfActiveRoll.threeActiveRollsK500) && upperRoll != null )
    //      //{
    //      //    if (rollSetHistory != null)
    //      //    {
    //      //        DCRollGroovesHistoryData dcLocalThird = new DCRollGroovesHistoryData();
    //      //        dcLocalThird.RollId = thirdRoll.RollId;
    //      //        dcLocalThird.GrooveNumber = SingleGrooveSetup.GrooveIdx;
    //      //        dcLocalThird.Status = SingleGrooveSetup.GrooveStatus;
    //      //        dcLocalThird.ActDiameter = (float)thirdRoll.ActualDiameter;
    //      //        dcLocalThird.GrooveTemplateId = SingleGrooveSetup.GrooveTemplate;
    //      //        dcLocalThird.RollSetHistoryId = rollSetHistory.RollSetHistoryId;
    //      //        if (!RollGroovesHistoryDataHandler.InsertRollGroovesHistory(dcLocalThird))
    //      //        {
    //      //            ModuleController.Logger.Error(String.Format("Error while created planned groove on roll {0}.", dcLocalThird.RollId));
    //      //            NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_InvalidGrooveStatus, String.Format("Error while created planned groove on roll {0}.", dcLocalThird.RollId), dcLocalThird.RollId);
    //      //            return -1;
    //      //        }
    //      //    }
    //      //    else
    //      //    {
    //      //        DCRollGroovesHistoryData dcLocalThird = new DCRollGroovesHistoryData();
    //      //        dcLocalThird.RollId = thirdRoll.RollId;
    //      //        dcLocalThird.GrooveNumber = SingleGrooveSetup.GrooveIdx;
    //      //        dcLocalThird.Status = SingleGrooveSetup.GrooveStatus;
    //      //        dcLocalThird.ActDiameter = (float)thirdRoll.ActualDiameter;
    //      //        dcLocalThird.GrooveTemplateId = SingleGrooveSetup.GrooveTemplate;
    //      //        if (!RollGroovesHistoryDataHandler.InsertRollGroovesHistory(dcLocalThird))
    //      //        {
    //      //            ModuleController.Logger.Error(String.Format("Error while created planned groove on roll {0}.", dcLocalThird.RollId));
    //      //            NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_InvalidGrooveStatus, String.Format("Error while created planned groove on roll {0}.", dcLocalThird.RollId), dcLocalThird.RollId);
    //      //            return -1;
    //      //        }
    //      //    }
    //      //}
    //    }
    //  }
    //  else
    //  {
    //    ModuleController.Logger.Error(String.Format("Groove Setup List Is Empty."));
    //    NotificationController.RegisterAlarm(RollshopDefs.AlarmCode_GrooveSetupListIsEmpty, String.Format("Groove Setup List Is Empty."));
    //    return -1;
    //  }
    //  return 0;
    //}

    public virtual async Task<DataContractBase> SelectActiveGrooves(DCRollSetGrooveSetup dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while Selecting active Groove. Empty DataContract");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while Selecting active Groove. Empty DataContract");
      }
      try
      {
        using (PEContext ctx = new PEContext())
        {
          //RollSet rollSet = uow.Repository<RollSet>().Query(z => z.RollSetId == dc.Id).GetSingle();
          RLSRollSet rollSet = _rollSetHandler.GetRollSetFromId(ctx, dc.Id); // ctx.RLSRollSets.Where(z => z.RollSetId == dc.Id).FirstOrDefault();

          if (rollSet == null)
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while selecting active groove. RollSet not found");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new Exception();
          }
          if (rollSet.Status != RollSetStatus.Mounted)
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while selecting active groove.Wrong RollSet status");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new Exception();
          }
          //RollSetHistory rollSetHistory = RollSetHistoryDataHandler.GetRollSetHistory(rollSet.RollSetId, PE.Core.Constants.RollSetHistoryStatus.Actual);
          RLSRollSetHistory rollSetHistory = _rollSetHistoryHandler.GetRollSetHistory(ctx, rollSet.RollSetId, RollSetHistoryStatus.Actual);
          if (rollSetHistory == null)
          {
            NotificationController.RegisterAlarm(Defs.RollChanegOperationError, "Error while selecting active groove.Wrong RollSetHistory not found.");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new Exception();
          }

          if (dc.GrooveSetupList.Count > 0)
          {
            //Update Grooves Statuses
            foreach (SingleGrooveSetup singleGrooveSetupElement in dc.GrooveSetupList)
            {
              //SingleGrooveSetupElement
              //DCRollGrooveStatusData dcGrooveSetup = new DCRollGrooveStatusData();
              //dcGrooveSetup.GrooveStatus = (PE.Core.Constants.RollGrooveStatus)singleGrooveSetupElement.GrooveStatus;
              //dcGrooveSetup.RollSetHistoryId = rollSetHistory.RollSetHistoryId;
              //dcGrooveSetup.GrooveNumber = singleGrooveSetupElement.GrooveIdx;
              if (rollSet.FKUpperRollId != null)
              {
                RLSRollGroovesHistory rollGrooveHistory = _rollGroovesHistoryHandler.GetRollGrooveHistoryForStatusUpdate(ctx, rollSetHistory.RollSetHistoryId, rollSet.FKUpperRollId ?? -1, singleGrooveSetupElement.GrooveIdx);
                //dcGrooveSetup.RollId = rollSet.FkUpperRollId ?? -1;
                _rollGroovesHistoryHandler.UpdateRollGrooveHistoryStatus(ctx, ref rollGrooveHistory, (RollGrooveStatus)singleGrooveSetupElement.GrooveStatus);
                //RollGroovesHistoryDataHandler.UpdateRollGrooveStatus(dcGrooveSetup);
                await ctx.SaveChangesAsync();
              }
              if (rollSet.FKBottomRollId != null)
              {
                RLSRollGroovesHistory rollGrooveHistory = _rollGroovesHistoryHandler.GetRollGrooveHistoryForStatusUpdate(ctx, rollSetHistory.RollSetHistoryId, rollSet.FKBottomRollId ?? -1, singleGrooveSetupElement.GrooveIdx);
                //dcGrooveSetup.RollId = rollSet.FkBottomRollId ?? -1;
                //RollGroovesHistoryDataHandler.UpdateRollGrooveStatus(dcGrooveSetup);
                _rollGroovesHistoryHandler.UpdateRollGrooveHistoryStatus(ctx, ref rollGrooveHistory, (RollGrooveStatus)singleGrooveSetupElement.GrooveStatus);
                await ctx.SaveChangesAsync();
                await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
              }
            }
            return result;
          }
          else
          {
            return result;
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while Selecting active Groove.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while Selecting active Groove.");
      }

    }


    public virtual async Task<DataContractBase> AccumulateRollsUsageAsync(DCRollsAccu message)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          await _rollGroovesHistoryHandler.UpdateActiveRollGrooveHistoryWithProductWeight(ctx, message.MaterialWeight);
          await ctx.SaveChangesAsync();
          NotificationController.Info("All active grooves ware updated with processed weight of {0} kg", message.MaterialWeight);
          await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
        }
      }
      catch
      {
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
      }
      return result;
    }


      //public static bool UpdateAccummulateData(DCRollSetAccumulationData dc)
      //{
      //  //get standIds separated
      //  string[] standIds = dc.StandIds.Split(',');
      //  if (standIds.Length > 0)
      //  {
      //    try
      //    {
      //      using (PEContext ctx = new PEContext())
      //      {
      //        //get billet weight
      //        //Billet billetWt = uow.Repository<Billet>().Query(f => f.BilletId == dc.BilletId)
      //        //                                          .Include(z => z.Heat)
      //        //                                          .Include(z => z.Heat.BilletCatalogue).GetSingle();

      //        double weight = 0.0;
      //        if (billetWt == null)
      //        {
      //          ModuleController.Logger.Info(String.Format("Billet id {0} not found. Accumulated weight has not been updated.", dc.BilletId));
      //          return false;
      //        }
      //        else
      //        {
      //          if (billetWt.Weight != null)
      //          {
      //            weight = billetWt.Weight ?? 0.0;
      //            ModuleController.Logger.Info(String.Format("Real billet weight {0} taken.", weight));
      //          }
      //          else if (billetWt.Heat.BilletCatalogueId != null)
      //          {
      //            weight = billetWt.Heat.BilletCatalogue.Weight ?? 0.0;
      //            ModuleController.Logger.Info(String.Format("Catalogue billet weight {0} taken.", weight));
      //          }
      //          else
      //          {
      //            ModuleController.Logger.Info(String.Format("Neither real, nor catalogue weight is available"));
      //            return false;
      //          }
      //        }

      //        if (dc.GrooveNo == -1)
      //        {
      //          foreach (string stId in standIds)
      //          {
      //            if (stId == string.Empty)
      //              continue;
      //            long standId = Convert.ToInt64(stId);
      //            //find active rollsets and update groove data in all active rollsethistory records
      //            IRepository<VGroovesView4Accumulation> repAccu = uow.Repository<VGroovesView4Accumulation>();
      //            IList<VGroovesView4Accumulation> resultAccu;
      //            resultAccu = repAccu.Query(f => f.StandStatus == (short)PE.Core.Constants.StandStatus.Active &&
      //                                                                            f.GrooveStatus == (short)PE.Core.Constants.RollGrooveStatus.Active &&
      //                                                                            f.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Mounted &&
      //                                                                            f.RollSetHistoryStatus == (short)PE.Core.Constants.RollSetHistoryStatus.Actual &&
      //                                                                            f.StandId == standId)
      //                                        .Get().ToList();
      //            if (resultAccu == null)
      //            {
      //              ModuleController.Logger.Info(String.Format("Can not get data from VGroovesView4Accumulation"));
      //              return false;
      //            }
      //            if (resultAccu.Count > 0)
      //            {
      //              foreach (VGroovesView4Accumulation vrec in resultAccu)
      //              {
      //                RollGroovesHistoryDataHandler.AccumulateGrooveData(vrec.RollGroovesHistoryId, (long)weight, ref uow);
      //              }
      //              ModuleController.Logger.Info(String.Format("{0} records updated in RollGroovesHistory table", resultAccu.Count));
      //            }
      //            else
      //            {
      //              ModuleController.Logger.Info(String.Format("No active grooves for accumulation."));
      //              continue;
      //            }
      //          }
      //        }
      //        else if (dc.GrooveNo > 0)
      //        {
      //          foreach (string stId in standIds)
      //          {
      //            if (stId == string.Empty)
      //              continue;
      //            long standId = Convert.ToInt64(stId);
      //            //find active rollsets and update groove data in all active rollsethistory records
      //            IRepository<VGroovesView4Accumulation> repAccu = uow.Repository<VGroovesView4Accumulation>();
      //            IList<VGroovesView4Accumulation> resultAccu;
      //            resultAccu = repAccu.Query(f => f.StandStatus == (short)PE.Core.Constants.StandStatus.Active &&
      //                                                                            f.GrooveNumber == dc.GrooveNo &&
      //                                                                            f.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Mounted &&
      //                                                                            f.StandId == standId &&
      //                                                                            (f.GrooveStatus == (short)PE.Core.Constants.RollGrooveStatus.Active || f.GrooveStatus == (short)PE.Core.Constants.RollGrooveStatus.Actual))
      //                                                            .Get().ToList();
      //            if (resultAccu == null)
      //            {
      //              //ModuleController.Logger.Error("Can not get data from VGroovesView4Accumulation");
      //              return false;
      //            }
      //            if (resultAccu.Count > 0)
      //            {
      //              foreach (VGroovesView4Accumulation vrec in resultAccu)
      //              {
      //                RollGroovesHistoryDataHandler.AccumulateGrooveData(vrec.RollGroovesHistoryId, (long)weight, ref uow);
      //              }
      //              //ModuleController.Logger.Info("{0} records updated in RollGroovesHistory table", resultAccu.Count);
      //            }
      //            else
      //            {
      //              //ModuleController.Logger.Error("No active grooves for accumulation.");
      //              continue;
      //            }
      //          }
      //        }
      //        else
      //        {
      //          //ModuleController.Logger.Error("Invalid groove number {0). No active grooves for accumulation.", activeGrooveNo);
      //          return false;
      //        }
      //        //uow.SaveChanges();
      //        return true;
      //      }
      //    }
      //    catch (Exception ex)
      //    {
      //      DbExceptionHelper.ProcessException(ex, "UpdateAccummulateData::Database operation failed!", ModuleController.Logger);
      //      return false;
      //    }
      //  }
      //  return true;
      //  //if(dc == null)
      //  //{
      //  //	return false;
      //  //}
      //  //return RollGroovesHistoryDataHandler.UpdateAccummulateData(dc.BilletId, dc.StandIds, dc.GrooveNo, ref uow);
      //}

    }
}
