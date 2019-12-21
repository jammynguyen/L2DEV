using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.WorkOrder
{
  public class VM_WorkOrderSummary : VM_Base
  {
    #region props
    public long Sorting { get; set; }
    public long WorkOrderId { get; set; }
    public long? ScheduleId { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "WorkOrderName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "IsTestOrder", "NAME_IsTestOrder")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsTestOrder { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "IsPlanned", "NAME_IsPlanned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int IsPlanned { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "IsProducedNow", "NAME_IsProducedNow")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int IsProducedNow { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "WorkOrderStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short WorkOrderStatus { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "ProductCatalogueName", "NAME_ProductCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueName { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "MaterialCatalogueName", "NAME_MaterialCatalogueName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialCatalogueName { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "SteelgradeCode", "NAME_SteelGradeName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "CreatedInL3", "NAME_CreatedInL3")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime CreatedInL3 { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "ToBeCompletedBefore", "NAME_ToBeCompletedBefore")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime ToBeCompletedBefore { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "PlannedStartTime", "NAME_PlannedStartTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? PlannedStartTime { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "PlannedEndTime", "NAME_PlannedEndTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? PlannedEndTime { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "ProductionStart", "NAME_DateTimeProductionStarted")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? ProductionStart { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "ProductionEnd", "NAME_DateTimeProductionFinished")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? ProductionEnd { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "TargetOrderWeight", "NAME_TargetOrderWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double TargetOrderWeight { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "ProductsWeight", "NAME_ProductWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double ProductsWeight { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "MaterialsWeight", "NAME_RawMaterialWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? MaterialsWeight { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "MetallicYield", "NAME_MetallicYield")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MetallicYield { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "Completion", "NAME_Completion")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double Completion { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "ProductsNumber", "NAME_ProductNumber")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int ProductsNumber { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "ProductsNumber", "NAME_MaterialsPlanned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsPlanned { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "MaterialsAssigned", "NAME_Assigned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsAssigned { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "MaterialsCharged", "NAME_Charged")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsCharged { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "MaterialsDischarged", "NAME_Discharged")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsDischarged { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "MaterialsRolled", "NAME_Rolled")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsRolled { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "MaterialsRejected", "NAME_Rejected")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsRejected { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "MaterialsScrapped", "NAME_Scrapped")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsScrapped { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "Progress", "NAME_Progress")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Progress { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderSummary), "ScheduleOrderSeq", "NAME_Sequence")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? ScheduleOrderSeq { get; set; }
    #endregion

    #region ctor
    public VM_WorkOrderSummary(V_WorkOrderSummary data)
    {
      Sorting = data.Sorting;
      WorkOrderId = data.WorkOrderId;
      ScheduleId = data.ScheduleId;
      WorkOrderName = data.WorkOrderName;
      IsTestOrder = data.IsTestOrder;
      IsPlanned = data.IsPlanned;
      IsProducedNow = data.IsProducedNow;
      WorkOrderStatus = data.WorkOrderStatus;
      ProductCatalogueName = data.ProductCatalogueName;
      MaterialCatalogueName = data.MaterialCatalogueName;
      HeatName = data.HeatName;
      SteelgradeCode = data.SteelgradeCode;
      CreatedInL3 = data.CreatedInL3;
      ToBeCompletedBefore = data.ToBeCompletedBefore;
      PlannedStartTime = data.PlannedStartTime;
      PlannedEndTime = data.PlannedEndTime;
      ProductionStart = data.ProductionStart;
      ProductionEnd = data.ProductionEnd;
      TargetOrderWeight = data.TargetOrderWeight;
      ProductsWeight = data.ProductsWeight;
      MetallicYield = data.MetallicYield;
      Completion = data.Completion;
      ProductsNumber = data.ProductsNumber;
      MaterialsPlanned = data.MaterialsPlanned;
      MaterialsAssigned = data.MaterialsAssigned;
      MaterialsCharged = data.MaterialsCharged;
      MaterialsDischarged = data.MaterialsDischarged;
      MaterialsRolled = data.MaterialsRolled;
      MaterialsRejected = data.MaterialsRejected;
      MaterialsScrapped = data.MaterialsScrapped;
      ScheduleOrderSeq = data.ScheduleOrderSeq;
      MaterialsWeight = data.MaterialsWeight;

      Progress = data.MaterialsAssigned + "/" + data.MaterialsPlanned;
      UnitConverterHelper.ConvertToLocal<VM_WorkOrderSummary>(this);
    }

    public VM_WorkOrderSummary(double? metallicYield)
    {
      MetallicYield = metallicYield;
    }
    #endregion
  }
}
