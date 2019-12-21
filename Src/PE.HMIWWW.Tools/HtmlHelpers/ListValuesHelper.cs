using PE.Common;
using PE.DbEntity;
using PE.DbEntity.Model;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using PE.DbEntity.Enums;
using SMF.Core.Helpers;

namespace PE.HMIWWW.Core.HtmlHelpers
{
  public static class ListValuesHelper
  {
    // ENUM
    public static SelectList GetShiftsTs()
    {
      using (PEContext uow = new PEContext())
      {
        IList<ShiftCalendar> shifts = uow.ShiftCalendars
              .Include(q => q.Crew)
              .OrderByDescending(q => q.IsActualShift)
              .Take(3)
              .ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        foreach (ShiftCalendar lm in shifts)
        {
          resultDictionary.Add((int)lm.ShiftCalendarId, String.Format("{0} - {1} - {2}", lm.PlannedStartTime.ToString("m") + " " + lm.PlannedStartTime.ToString("t"), lm.PlannedEndTime.ToString("m") + " " + lm.PlannedEndTime.ToString("t"), lm.Crew.CrewName));
        }

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;
      }
    }

    public static SelectList GetScheduleStatuses()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(PE.DbEntity.Enums.OrderStatus)))
      {
        resultDictionary.Add((int)item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_OrderStatus_{0}", item.Key)));
      }
      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetCorrectStatus()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(PE.DbEntity.Enums.Correct)))
      {
        resultDictionary.Add((int)item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_CorrectStatus_{0}", item.Key)));
      }
      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetTextFromTransferTableDataReadingStatus()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();
      SelectList tmpSelectList = null;
      try
      {
        foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(PE.DbEntity.Enums.TransferTableDataReadingStatus)))
        {
          resultDictionary.Add((int)item.Key, VM_Resources.ResourceManager.GetString(string.Format("GLOB_TransferTableDataReadingStatus_{0}", item.Key)));
        }
        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.Alarm_ExceptionInViewBag, String.Format("Exception in view bag method: {0}. Exception: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
        LogHelper.LogException(ex, String.Format("Exception in view bag method: {0}. Exception: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message));
      }
      return tmpSelectList;
    }

    public static SelectList GetTextFromProcessingMessageStatus()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();
      SelectList tmpSelectList = null;
      try
      {
        foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(PE.DbEntity.Enums.ProcessingMessageStatus)))
        {
          resultDictionary.Add((int)item.Key, VM_Resources.ResourceManager.GetString(string.Format("GLOB_ProcessingMessage_{0}", item.Key)));
        }
        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.Alarm_ExceptionInViewBag, String.Format("Exception in view bag method: {0}. Exception: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
        LogHelper.LogException(ex, String.Format("Exception in view bag method: {0}. Exception: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message));
      }
      return tmpSelectList;
    }

    public static SelectList GetAlarmTypes()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();
      SelectList tmpSelectList = null;
      try
      {
        foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(SMF.DbEntity.Enums.AlarmType)))
        {
          resultDictionary.Add((int)item.Key, VM_Resources.ResourceManager.GetString(string.Format("GLOB_AlarmType_{0}", item.Key)));
        }
        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.Alarm_ExceptionInViewBag, String.Format("Exception in view bag method: {0}. Exception: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
        LogHelper.LogException(ex, String.Format("Exception in view bag method: {0}. Exception: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message));
      }
      return tmpSelectList;
    }

    public static SelectList GetTelegramNames()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();
      SelectList tmpSelectList = null;
      try
      {
        foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(PE.DbEntity.Enums.TelegramIds)))
        {
          resultDictionary.Add((int)item.Key, VM_Resources.ResourceManager.GetString(string.Format("GLOB_TelegramId_{0}", item.Key)));
        }
        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.Alarm_ExceptionInViewBag, String.Format("Exception in view bag method: {0}. Exception: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
        LogHelper.LogException(ex, String.Format("Exception in view bag method: {0}. Exception: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message));
      }
      return tmpSelectList;
    }

    public static SelectList GetTextFromYesNoStatus()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();
      SelectList tmpSelectList = null;
      try
      {
        foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(PE.DbEntity.Enums.YesNo)))
        {
          resultDictionary.Add((int)item.Key, VM_Resources.ResourceManager.GetString(string.Format("GLOB_NoYes_{0}", item.Key)));
        }
        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.Alarm_ExceptionInViewBag, String.Format("Exception in view bag method: {0}. Exception: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
        LogHelper.LogException(ex, String.Format("Exception in view bag method: {0}. Exception: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message));
      }
      return tmpSelectList;
    }

    public static SelectList GetCrews()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<Crew> crews = ctx.Crews.Where(z => z.CrewName == "A" || z.CrewName == "B" || z.CrewName == "C").ToList();
        return new SelectList(crews, "CrewId", "CrewName");
      }
    }

    public static SelectList GetFeaturesDefinitions()
    {
      throw new NotImplementedException();
      //IList<MVHFeature> features = new List<MVHFeature>();

      //using (PEContext ctx = new PEContext())
      //{

      //  features = ctx.MVHFeatures
      //    .Include(fa => fa.MVHFeaturesEXT).Include(fa => fa.Asset).OrderBy(fa => fa.Asset.OrderSeq).ThenBy(fa => fa.FeatureName)
      //    .ToList();

      //  Dictionary<long, string> outputDictionary = new Dictionary<long, string>();
      //  foreach (MVHFeature f in features)
      //  {
      //    outputDictionary.Add(f.FeatureId, $"{f.Asset.AssetName} - {f.FeatureName}");
      //  }

      //  return new SelectList(outputDictionary, "Key", "Value");
      //}
    }

    public static SelectList GetRollStatus()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(RollStatus)))
      {
        resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_ROLLS_StatusEnum_{0}", item.Key)));
      }


      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");


      return tmpSelectList;
    }

    public static SelectList GetGrooveTemplatesShortList()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<RLSGrooveTemplate> equipmentgroup = ctx.RLSGrooveTemplates.ToList();

        SelectList tmpSelectList = new SelectList(equipmentgroup, "GrooveTemplateId", "GrooveTemplateCode");

        return tmpSelectList;
      }
    }

    public static SelectList GetRollScrapReasons()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(RollScrapReason)))
      {
        resultDictionary.Add((int)item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_ROLLSCRAPREASON_{0}", item.Key)));
      }
      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetRollStatusWithoutUndef()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(RollStatus)))
      {
        if (item.Key != (short)RollStatus.Undefined)
        {
          resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_ROLLS_StatusEnum_{0}", item.Key)));
        }
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetShapeList()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<PRMShape> shapelist = ctx.PRMShapes.ToList();

        SelectList tmpshapeList = new SelectList(shapelist, "ShapeId", "ShapeName");

        return tmpshapeList;
      }
    }

    public static SelectList GetRollWithTypes()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<RLSRollType> products = ctx.RLSRollTypes.OrderBy(o => o.MatchingRollsetType).ThenBy(o => o.RollTypeName).ThenBy(o => o.DiameterMin).ToList();
        return new SelectList(products, "RollTypeId", "RollTypeName");
      }
    }

    public static SelectList GetEquipmentStatuses()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(EquipmentStatus)))
      {
        resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_EquipmentStatus_{0}", item.Key)));
      }
      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      return tmpSelectList;
    }

    public static SelectList GetEquipmentGroupsList()
    {

      using (PEContext ctx = new PEContext())
      {
        IList<MNTEquipmentGroup> equipmentgroup = ctx.MNTEquipmentGroups.OrderBy(o => o.EquipmentGroupCode).ToList();
        SelectList tmpSelectList = new SelectList(equipmentgroup, "EquipmentGroupId", "EquipmentGroupCode");
        return tmpSelectList;
      }
    }


    public static SelectList GetRollSetStatus()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(RollSetStatus)))
      {
        resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_ROLLSETS_StatusEnum_{0}", item.Key)));
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");


      return tmpSelectList;
    }

    public static SelectList GetRollSetStatusShortList()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(RollSetStatus)))
      {
        if (item.Key == (short)RollSetStatus.NotAvailable || item.Key == (short)RollSetStatus.Ready)
          resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_ROLLSETS_StatusEnum_{0}", item.Key)));
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetRollSetHistoryStatus()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(RollSetHistoryStatus)))
      {
        resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_ROLLSETHISTORY_StatusEnum_{0}", item.Key)));
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetRollsetEmpty()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<RLSRollSet> rollsets = ctx.RLSRollSets.Where(f => f.Status == RollSetStatus.Empty).OrderBy(p => p.RollSetName).ToList();

        if (rollsets.Count != 0)
        {
          return new SelectList(rollsets, "RollSetId", "RollSetName");
        }
        else
        {
          return null;
        }
      }
    }
    public static SelectList GetRollsetTypeList()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(RollSetType)))
      {
        if (item.Key == (short)RollSetType.TwoActiveRollsRM || item.Key == (short)RollSetType.TwoActiveRollsIM)
          resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_ROLLSETTYPE_Enum_{0}", item.Key)));
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }
    public static SelectList GetCassetteStatusShortList()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(CassetteStatus)))
      {
        if (item.Key == (short)CassetteStatus.NotAvailable || item.Key == (short)CassetteStatus.Empty || item.Key == (short)CassetteStatus.InRegeneration)
          resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_CASSETTE_StatusEnum_{0}", item.Key)));
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }
    public static SelectList GetCassetteStatus()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(CassetteStatus)))
      {
        resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_CASSETTE_StatusEnum_{0}", item.Key)));
        //resultDictionary.Add(item.Key, item.Value);
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetRollsReadyWithTypes()
    {
      using (PEContext ctx = new PEContext())
      {
        //IList<VRollsWithType> rolls = uow.Repository<VRollsWithType>()
        //                                                    .Query(f => (f.Status == (short)PE.Core.Constants.RollStatus.New || f.Status == (short)PE.Core.Constants.RollStatus.Unassigned))
        //                                                    .OrderBy(o => o.OrderBy(p => p.RollName))
        //                                                    .Get()
        //                                                    .ToList();

        IList<V_RollsWithTypes> rolls = ctx.V_RollsWithTypes.Where(f => (f.Status == (short)RollStatus.New || f.Status == (short)RollStatus.Unassigned)).OrderBy(p => p.RollName).ToList();

        //  newest until testing
        /* var rSet = uow.Repository<VRollsetOverview>().Query().Include(r => r.RollTypeIdUpper).GetSingle();
         var rSet = uow.Repository<VRollsetOverview>().Query().Include(r => r.RollTypeIdUpper).GetSingle();
          IList<VRollsWithType> rolls = uow.Repository<VRollsWithType>()
                                                                              .Query(f => (f.Status == (short)PE.Core.Constants.RollStatus.New || f.Status == (short)PE.Core.Constants.RollStatus.Unassigned) 
                                                                              && (f.Type == rSet.RollTypeIdUpper))

                                                                              .OrderBy(o => o.OrderBy(p => p.RollName))
                                                                              .Get()
                                                                              .ToList();*/

        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();

        foreach (V_RollsWithTypes rl in rolls)
        {
          if (!String.IsNullOrEmpty(rl.RollTypeName))
          {
            resultDictionary.Add(rl.RollId, String.Format("{0} / {1}", rl.RollName, rl.RollTypeName));
          }
          else
          {
            resultDictionary.Add(rl.RollId, String.Format("{0}", rl.RollName));
          }
        }

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        //return new SelectList(cassette, "RollId", "RollName");
        return tmpSelectList;

      }


    }

    public static SelectList GetNumberOfActiveRoll()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(RollSetType)))
      {
        resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_ROLLSETS_Number_of_Active_RollEnum_{0}", item.Key)));
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetCassetteArrangement()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(CassetteArrangement)))
      {
        resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_CassetteArrangement_{0}", item.Key)));
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetCassetteType()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<RLSCassetteType> products = ctx.RLSCassetteTypes.ToList();
        return new SelectList(products, "CassetteTypeId", "CassetteTypeName");
      }
    }

    public static SelectList GetCassetteTypeEnum()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(CassetteType)))
      {
        resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_CASS_Cassette_Type_{0}", item.Key)));
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetRollSetCounter()
    {
      using (PEContext ctx = new PEContext())
      {
        //IList<VCassettesOverview> cassette = uow.Repository<VCassettesOverview>()
        //                                                                            .Query()
        //                                                                            .Get().ToList();

        IList<V_CassettesOverview> cassette = ctx.V_CassettesOverview.ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        foreach (V_CassettesOverview lm in cassette)
        {
          //IList<V_RollsetOverviewNewest> entityList = uow.Repository<VRollsetOverviewNewest>()
          //                                                    .Query(x => (x.CassetteId == lm.CassetteId))
          //                                                    .Get()
          //                                                    .ToList();

          IList<V_RollsetOverviewNewest> entityList = ctx.V_RollsetOverviewNewest.Where(x => x.CassetteId == lm.CassetteId).ToList();
          int rscounter = entityList.Count();

          resultDictionary.Add((int)lm.CassetteId, rscounter.ToString());
        }

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;
      }

    }

    public static SelectList GetRollGrooveStatus()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(RollGrooveStatus)))
      {
        resultDictionary.Add((int)item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_ROLLGROOVES_StatusEnum_{0}", item.Key)));
      }
      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetStandStat()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(StandStatus)))
      {
        resultDictionary.Add((int)item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_StandStatus_{0}", item.Key)));
      }
      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetGrooveTemplatesList()
    {
      using (PEContext ctx = new PEContext())
      {
        //IList<GrooveTemplate> equipmentgroup = uow.Repository<GrooveTemplate>()
        //    .Query()
        //    .Get()
        //    .ToList();

        IList<RLSGrooveTemplate> templateList = ctx.RLSGrooveTemplates.ToList();
        SelectList tmpSelectList = new SelectList(templateList, "GrooveTemplateId", "GrooveTemplateName");

        return tmpSelectList;
      }
    }

    public static SelectList GetRollSetHistoryForId(long rollsetHistoryId)
    {
      using (PEContext ctx = new PEContext())
      {
        SelectList tmpSelectList = null; ;
        //RollSetHistory rollsetOveview = uow.Repository<RollSetHistory>().Query(z => z.RollSetHistoryId == rollsetHistoryId).GetSingle();
        RLSRollSetHistory rollSetOverview = ctx.RLSRollSetHistories.Where(z => z.RollSetHistoryId == rollsetHistoryId).FirstOrDefault();
        if (rollSetOverview != null)
        {
          //IList<RollSetHistory> rollsethistory = uow.Repository<RollSetHistory>().Query(x => (x.FkRollSetId == rollsetOveview.FkRollSetId)).OrderBy(o => o.OrderByDescending(g => g.RollSetHistoryId)).Get().ToList();
          IList<RLSRollSetHistory> rollsethistory = ctx.RLSRollSetHistories.Where(x => x.FKRollSetId == rollSetOverview.FKRollSetId).OrderBy(g => g.RollSetHistoryId).ToList();

          Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

          foreach (RLSRollSetHistory lm in rollsethistory)
          {
            if (lm.Status != RollSetHistoryStatus.Actual)
            {
              resultDictionary.Add((int)lm.RollSetHistoryId, String.Format("{0}", lm.Created));
            }
            else
            {
              resultDictionary.Add((int)lm.RollSetHistoryId, ResxHelper.GetResxByKey("GLOB_ROLLSETHISTORY_StatusEnum_1"));
            }

          }
          tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
        }
        return tmpSelectList;
        //return new SelectList(rollsethistory, "RollSetHistoryId", "Mounted");
      }
    }

    public static SelectList GetRollSetHistory(long rollsetId)
    {
      using (PEContext ctx = new PEContext())
      {
        //IList<RollSetHistory> rollsethistory = uow.Repository<RollSetHistory>().Query(x => (x.FkRollSetId == rollsetId)).OrderBy(o => o.OrderByDescending(g => g.RollSetHistoryId)).Get().ToList();

        IList<RLSRollSetHistory> rollSetHistory = ctx.RLSRollSetHistories.Where(z => z.FKRollSetId == rollsetId).OrderBy(g => g.RollSetHistoryId).ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        foreach (RLSRollSetHistory lm in rollSetHistory)
        {
          if (lm.Status != RollSetHistoryStatus.Actual)
          {
            resultDictionary.Add((int)lm.RollSetHistoryId, String.Format("{0}", lm.Created));
          }
          else
          {
            resultDictionary.Add((int)lm.RollSetHistoryId, ResxHelper.GetResxByKey("GLOB_ROLLSETHISTORY_StatusEnum_1"));
          }

        }

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
        return tmpSelectList;
        //return new SelectList(rollsethistory, "RollSetHistoryId", "Mounted");
      }
    }

    public static SelectList GetCassetteArrangementNoUndefined()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(CassetteArrangement)))
      {
        if (item.Key != (short)CassetteArrangement.Undefined)
        {
          resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_CassetteArrangement_{0}", item.Key)));
        }
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetUnmountedCassettes()
    {
      using (PEContext ctx = new PEContext())
      {
        //IList<VCassettesOverview> cassette = uow.Repository<VCassettesOverview>()
        //                                                                            .Query(x => (x.Status == (short)PE.Core.Constants.CassetteStatus.RollSetInside) || (x.Status == (short)PE.Core.Constants.CassetteStatus.New))
        //                                                                            .Get()
        //                                                                            .OrderBy(p => p.CassetteName)
        //                                                                            .ToList();

        IList<V_CassettesOverview> cassette = ctx.V_CassettesOverview
                                                                            .Where(x => (x.Status == (short)CassetteStatus.RollSetInside) || (x.Status == (short)CassetteStatus.New))
                                                                            .OrderBy(p => p.CassetteName)
                                                                            .ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        resultDictionary.Add(0, /*Resources.Framework.Global.GLOB_Name_CassetteChoose*/"Choose cassette");

        foreach (V_CassettesOverview lm in cassette)
        {
          resultDictionary.Add((int)lm.CassetteId, lm.CassetteName);
        }
        resultDictionary.ToList();

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;

      }
    }

    public static SelectList GetMountedCassettes()
    {
      using (PEContext ctx = new PEContext())
      {
        //IList<VCassettesOverview> cassette = uow.Repository<VCassettesOverview>()
        //                                                                            .Query(x => (x.Status == (short)PE.Core.Constants.CassetteStatus.MountedInStand))
        //                                                                            .Get()
        //                                                                            .OrderBy(p => p.CassetteName)
        //                                                                            .ToList();

        IList<V_CassettesOverview> cassette = ctx.V_CassettesOverview
                                                                            .Where(x => (x.Status == (short)CassetteStatus.MountedInStand))
                                                                            .OrderBy(p => p.CassetteName)
                                                                            .ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        resultDictionary.Add(0, /*Resources.Framework.Global.GLOB_Name_CassetteChoose*/"Choose cassette");

        foreach (V_CassettesOverview lm in cassette)
        {
          resultDictionary.Add((int)lm.CassetteId, lm.CassetteName);
        }
        resultDictionary.ToList();

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;

      }
    }


    public static SelectList GetCassettesReadyForMount()
    {
      using (PEContext ctx = new PEContext())
      {
        //Cassette Cass = uow.Repository<Cassette>().Query().Include(z => z.Stand).Include(z => z.CassetteType).GetSingle();
        RLSCassette cass = ctx.RLSCassettes.Include(z => z.RLSStand).Include(z => z.RLSCassetteType).FirstOrDefault();

        //IList<VCassettesOverview> cassette = uow.Repository<VCassettesOverview>()
        //                                                                            .Query(x => (x.Status == (short)PE.Core.Constants.CassetteStatus.RollSetInside) || (x.Status == (short)PE.Core.Constants.CassetteStatus.Empty))
        //                                                                            .Get()
        //                                                                            .OrderBy(p => p.CassetteName).ThenBy(b => b.CassetteName)
        //                                                                            .ToList();

        IList<V_CassettesOverview> cassette = ctx.V_CassettesOverview
                                                                            .Where(x => (x.Status == (short)CassetteStatus.RollSetInside) || (x.Status == (short)CassetteStatus.Empty))
                                                                            .OrderBy(p => p.CassetteName).ThenBy(b => b.CassetteName)
                                                                            .ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        resultDictionary.Add(0, /*Resources.Framework.Global.GLOB_Name_CassetteChoose*/"Choose cassette");

        foreach (V_CassettesOverview lm in cassette)
        {
          resultDictionary.Add((int)lm.CassetteId, lm.CassetteName);
        }
        resultDictionary.ToList();

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;

      }
    }

    public static SelectList GetRollsetsReadyOnlyWithTypes(short? type = null)
    {
      using (PEContext ctx = new PEContext())
      {
        //IList<VRollsetOverviewNewest> rollsets = uow.Repository<VRollsetOverviewNewest>()
        //                                                                            .Query(f => f.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Ready)
        //                                                                            .OrderBy(o => o.OrderBy(p => p.RollSetName))
        //                                                                            .Get()
        //                                                                            .ToList();

        IList<V_RollsetOverviewNewest> rollsets = ctx.V_RollsetOverviewNewest
                                                                            .Where(f => f.RollSetStatus == (short)RollSetStatus.Ready)
                                                                            .OrderBy(p => p.RollSetName)
                                                                            .ToList();

        if (type != null)
        {
          rollsets = rollsets.Where(x => x.RollSetType == type).ToList();
        }

        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();

        foreach (V_RollsetOverviewNewest rl in rollsets)
        {
          if (!String.IsNullOrEmpty(rl.RollTypeUpper))
          {
            resultDictionary.Add(Convert.ToInt64(rl.RollSetId), String.Format("{0} / {1}", rl.RollSetName, rl.RollTypeUpper));
          }
          else
          {
            resultDictionary.Add(Convert.ToInt64(rl.RollSetId), String.Format("{0}", rl.RollSetName));
          }
        }

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        //return new SelectList(cassette, "RollId", "RollName");
        return tmpSelectList;
      }
    }

    public static SelectList GetCassettesReadyForMountWith2Rolls(/*long standId*/)
    {
      using (PEContext ctx = new PEContext())
      {
        //var stand = uow.Repository<VActualStandConfiguration>().Query(x => x.StandId == standId).Get().FirstOrDefault();
        //if (stand == null)
        //{
        //    throw new NullReferenceException();
        //}
        //IList<VCassettesOverview> cassette = uow.Repository<VCassettesOverview>()
        //                                                                           // .Query(x => (x.Status == (short)PE.Core.Constants.CassetteStatus.RollSetInside) || (x.Status == (short)PE.Core.Constants.CassetteStatus.Empty) && (x.NumberOfRolls == stand.NumberOfRolls))
        //                                                                           .Query(x => ((x.Status == (short)PE.Core.Constants.CassetteStatus.RollSetInside) || (x.Status == (short)PE.Core.Constants.CassetteStatus.Empty)) && (x.NumberOfRolls == (short)PE.Core.Constants.RollSetType.twoActiveRollsRM))
        //                                                                            .Get()
        //                                                                            .OrderBy(p => p.CassetteName).ThenBy(b => b.CassetteName)
        //                                                                            .ToList();

        IList<V_CassettesOverview> cassette = ctx.V_CassettesOverview
                                                                         // .Query(x => (x.Status == (short)PE.Core.Constants.CassetteStatus.RollSetInside) || (x.Status == (short)PE.Core.Constants.CassetteStatus.Empty) && (x.NumberOfRolls == stand.NumberOfRolls))
                                                                         .Where(x => ((x.Status == (short)CassetteStatus.RollSetInside) || (x.Status == (short)CassetteStatus.Empty)) && (x.NumberOfRolls == (short)RollSetType.TwoActiveRollsRM))
                                                                         .OrderBy(p => p.CassetteName).ThenBy(b => b.CassetteName)
                                                                         .ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        resultDictionary.Add(0, /*Resources.Framework.Global.GLOB_Name_CassetteChoose*/"Choose cassette");

        foreach (V_CassettesOverview lm in cassette)
        {
          resultDictionary.Add((int)lm.CassetteId, lm.CassetteName);
        }
        resultDictionary.ToList();

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;

      }
    }

    public static SelectList GetStandActivity()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(StandActivity)))
      {
        resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_STANDS_Stand_Activity_{0}", item.Key)));
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetStandStatNoUndefined()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(StandStatus)))
      {
        if (item.Key != (short)StandStatus.Undefined)
        {
          resultDictionary.Add((int)item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_StandStatus_{0}", item.Key)));
        }
      }
      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetCassetteReadyNewWithInitValue()
    {
      using (PEContext ctx = new PEContext())
      {
        //IList<VCassettesOverview> cassette = uow.Repository<VCassettesOverview>()
        //                                                                            .Query(x => (x.Status == (short)PE.Core.Constants.CassetteStatus.Empty) || (x.Status == (short)PE.Core.Constants.CassetteStatus.New))
        //                                                                            .Get()
        //                                                                            .OrderBy(p => p.CassetteName)
        //                                                                            .ToList();

        IList<V_CassettesOverview> cassette = ctx.V_CassettesOverview.Where(x => (x.Status == (short)CassetteStatus.Empty) || (x.Status == (short)CassetteStatus.New))
                                                                     .OrderBy(p => p.CassetteName)
                                                                     .ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        resultDictionary.Add(0, /*Resources.Framework.Global.GLOB_Name_CassetteChoose*/"Choose cassette");

        foreach (V_CassettesOverview lm in cassette)
        {
          resultDictionary.Add((int)lm.CassetteId, lm.CassetteName);
        }
        resultDictionary.ToList();

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;

      }
    }

    public static SelectList GetAvailableRollsets()
    {
      using (PEContext ctx = new PEContext())
      {
        //IList<VRollsetOverviewNewest> rollSets = uow.Repository<VRollsetOverviewNewest>()
        //                                                                            .Query(x => x.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Ready)
        //                                                                            .Get()
        //                                                                            .OrderBy(p => p.CassetteName)
        //                                                                            .ToList();

        IList<V_RollsetOverviewNewest> rollSets = ctx.V_RollsetOverviewNewest.Where(x => x.RollSetStatus == (short)RollSetStatus.Ready)
                                                                             .OrderBy(p => p.CassetteName)
                                                                             .ToList();
        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        resultDictionary.Add(0, /*Resources.Framework.Global.GLOB_Name_CassetteChoose*/"Choose rollset");

        foreach (V_RollsetOverviewNewest rollset in rollSets)
        {
          resultDictionary.Add((int)rollset.RollSetId, rollset.RollSetName);
        }
        resultDictionary.ToList();

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;

      }
    }

    public static SelectList GetDefectCatalogCategoriesList()
    {

      using (PEContext ctx = new PEContext())
      {
        IList<MVHDefectCatalogueCategory> list = ctx.MVHDefectCatalogueCategories.OrderBy(o => o.DefectCatalogueCategoryCode).ToList();
        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
        if (list != null)
        {
          foreach (MVHDefectCatalogueCategory el in list)
          {
            resultDictionary.Add(el.DefectCatalogueCategoryId, el.DefectCatalogueCategoryCode);
          }
        }
        resultDictionary.ToList();
        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
        return tmpSelectList;
      }
    }

    public static SelectList GetProductQualityList()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(ProductQuality)))
      {
        resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(string.Format("GLOB_ProductQuality_{0}", item.Key)));
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetWorkOrderStatusesList()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(DbEntity.Enums.OrderStatus)))
      {
        resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(item.Value));
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetRawMaterialStatusesList()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(DbEntity.Enums.RawMaterialStatus)))
      {
        resultDictionary.Add(item.Key, ResxHelper.GetResxByKey(item.Value));
      }

      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    
  }
}



