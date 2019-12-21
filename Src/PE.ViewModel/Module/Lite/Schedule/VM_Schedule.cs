using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Schedule
{
  public class VM_Schedule : VM_Base
  {
    #region props
    [SmfDisplayAttribute(typeof(VM_Schedule), "ScheduleId", "NAME_ScheduleId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? ScheduleId { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "FKWorkOrderId", "NAME_FKWorkOrderId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKWorkOrderId { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "OrderSeq", "NAME_OrderSeq")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? OrderSeq { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "ScheduleStatus", "NAME_ScheduleStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? ScheduleStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "PlannedWeight", "NAME_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? PlannedWeight { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "PlannedTime", "NAME_PlannedDuration")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string PlannedTime { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "StartTime", "NAME_StartTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? StartTime { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "EndTime", "NAME_EndTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? EndTime { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "PlannedStartTime", "NAME_StartTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? PlannedStartTime { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "PlannedEndTime", "NAME_PlannedEndTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? PlannedEndTime { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "FKAssetId", "NAME_FKAssetId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKAssetId { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "NoOfmaterials", "NAME_NoOfmaterials")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? NoOfmaterials { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "MaterialsNo", "NAME_MaterialsNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsNo { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "IsTest", "NAME_IsTest")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsTest { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "ProductCatalogueName", "PS_ProductCatalogueName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueName { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "BilletCatalogueName", "NAME_BilletCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string BilletCatalogueName { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "Steelgrade", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Steelgrade { get; set; }

    [SmfDisplayAttribute(typeof(VM_Schedule), "BilletsToBeRolled", "NAME_BilletsToBeRolled")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string BilletsToBeRolled { get; set; }

    public int? WorkOrderStatus { get; set; }
    #endregion

    #region ctor
    public VM_Schedule(PPLSchedule schedule)
    {
      ScheduleId = schedule.ScheduleId;
      FKWorkOrderId = schedule.FKWorkOrderId;
      OrderSeq = schedule.OrderSeq;
      PlannedTime = TimeSpan.FromSeconds(schedule.PlannedTime).ToString();
      PlannedStartTime = schedule.PlannedStartTime;
      PlannedEndTime = schedule.PlannedEndTime;
      CreatedTs = schedule.CreatedTs;
      PlannedWeight = schedule.PRMWorkOrder?.TargetOrderWeight;
      HeatName = schedule.PRMWorkOrder.PRMMaterials.OfType<PRMMaterial>().FirstOrDefault().PRMHeat.HeatName;
      IsTest = schedule.PRMWorkOrder?.IsTestOrder;
      ProductCatalogueName = schedule.PRMWorkOrder?.PRMProductCatalogue?.ProductCatalogueName;
      BilletCatalogueName = schedule.PRMWorkOrder?.PRMMaterialCatalogue?.MaterialCatalogueName;
      Steelgrade = schedule.PRMWorkOrder.PRMMaterials.OfType<PRMMaterial>().FirstOrDefault().PRMHeat.PRMMaterialCatalogue.PRMSteelgrade?.SteelgradeCode;

      WorkOrderName = schedule?.PRMWorkOrder?.WorkOrderName;
      MaterialsNo = schedule?.PRMWorkOrder?.PRMMaterials?.Count;
      WorkOrderStatus = Convert.ToInt32(schedule?.PRMWorkOrder?.WorkOrderStatus);

      UnitConverterHelper.ConvertToLocal<VM_Schedule>(this);
    }

    public VM_Schedule()
    {

    }
    #endregion
  }
}
