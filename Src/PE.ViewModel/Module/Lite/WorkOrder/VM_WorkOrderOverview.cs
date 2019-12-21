using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PE.HMIWWW.ViewModel.Module.Lite.WorkOrder
{
  public class VM_WorkOrderOverview : VM_Base
  {
    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "WorkOrderId", "NAME_WorkOrderId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? WorkOrderId { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "LastUpdateTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "WorkOrderStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "WorkOrderName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "IsTestOrder", "NAME_IsTestOrder")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsTestOrder { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "TargetOrderWeight", "NAME_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? TargetOrderWeight { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "FKProductCatalogueId", "NAME_FKProductCatalogueId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKProductCatalogueId { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "CreatedInL3", "NAME_CreatedInL3")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedInL3 { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "ToBeCompletedBefore", "NAME_ToBeCompletedBefore")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? ToBeCompletedBefore { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "TargetOrderWeightMin", "NAME_TargetOrderWeightMin")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? TargetOrderWeightMin { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "TargetOrderWeightMax", "NAME_TargetOrderWeightMax")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? TargetOrderWeightMax { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "FKCustomerId", "NAME_FKCustomerId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKCustomerId { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "FKReheatingGroupId", "NAME_FKReheatingGroupId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKReheatingGroupId { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "NextAggregate", "NAME_NextAggregate")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string NextAggregate { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "Shape", "NAME_Shape")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Shape { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "OperationCode", "NAME_OperationCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string OperationCode { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "QualityPolicy", "NAME_QualityPolicy")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string QualityPolicy { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "ExtraLabelInformation", "NAME_ExtraLabelInformation")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ExtraLabelInformation { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "SteelGrade", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Steelgrade { get; set; }


    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "IsScheduled", "NAME_IsScheduled")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsScheduled { get; set; }

    public long? FKHeatIdRef { get; set; }
    public long? FKMaterialCatalogueId { get; set; }

    public VM_MaterialCatalogue MC { get; set; }

    public VM_ProductCatalogue PC { get; set; }

    public VM_Steelgrade SC { get; set; }

    public VM_HeatOverview Heat { get; set; }

    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "MetallicYield", "NAME_MetallicYield")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MetallicYield { get; set; }
    [SmfDisplayAttribute(typeof(VM_WorkOrderOverview), "Completion", "NAME_Completion")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double Completion { get; set; }


    public VM_WorkOrderOverview() { }
    public VM_WorkOrderOverview(PRMWorkOrder order)
    {
      WorkOrderId = order.WorkOrderId;
      CreatedTs = order.CreatedTs;
      LastUpdateTs = order.LastUpdateTs;
      WorkOrderStatus = ResxHelper.GetResxByKey(order.WorkOrderStatus.ToString());
      WorkOrderName = order.WorkOrderName;
      IsTestOrder = order.IsTestOrder;
      TargetOrderWeight = order.TargetOrderWeight;
      FKProductCatalogueId = order.FKProductCatalogueId;
      CreatedInL3 = order.CreatedInL3;
      ToBeCompletedBefore = order.ToBeCompletedBefore;
      TargetOrderWeightMin = order.TargetOrderWeightMin;
      TargetOrderWeightMax = order.TargetOrderWeightMax;
      FKCustomerId = order.FKCustomerId;
      FKReheatingGroupId = order.FKReheatingGroupId;
      NextAggregate = order.NextAggregate;
      OperationCode = order.OperationCode;
      QualityPolicy = order.QualityPolicy;
      ExtraLabelInformation = order.ExtraLabelInformation;
      if (order.PRMMaterials.OfType<PRMMaterial>().FirstOrDefault()!= null && order.PRMMaterials.OfType<PRMMaterial>().FirstOrDefault().PRMHeat != null)
      {
        Heat = new VM_HeatOverview(order.PRMMaterials.OfType<PRMMaterial>().FirstOrDefault().PRMHeat);
        HeatName = Heat.HeatName;
        FKHeatIdRef = Heat.HeatId;
      }
      
      Steelgrade = order.PRMMaterialCatalogue.PRMSteelgrade.SteelgradeCode;
      
      FKMaterialCatalogueId = order.PRMMaterialCatalogue?.MaterialCatalogueId;
      Shape = order.PRMMaterialCatalogue?.PRMShape?.ShapeName;

      if (order.PRMMaterialCatalogue != null)
        MC = new VM_MaterialCatalogue(order.PRMMaterialCatalogue);

      if (order.PRMProductCatalogue != null)
        PC = new VM_ProductCatalogue(order.PRMProductCatalogue);

      if (order.PRMProductCatalogue?.PRMSteelgrade != null)
        SC = new VM_Steelgrade(order.PRMProductCatalogue.PRMSteelgrade);

      UnitConverterHelper.ConvertToLocal<VM_WorkOrderOverview>(this);
    }

    public VM_WorkOrderOverview(PRMWorkOrder order, double? metallicYield, double completion)
    {
      WorkOrderId = order.WorkOrderId;
      CreatedTs = order.CreatedTs;
      LastUpdateTs = order.LastUpdateTs;
      WorkOrderStatus = ResxHelper.GetResxByKey(order.WorkOrderStatus.ToString());
      WorkOrderName = order.WorkOrderName;
      IsTestOrder = order.IsTestOrder;
      TargetOrderWeight = order.TargetOrderWeight;
      FKProductCatalogueId = order.FKProductCatalogueId;
      CreatedInL3 = order.CreatedInL3;
      ToBeCompletedBefore = order.ToBeCompletedBefore;
      TargetOrderWeightMin = order.TargetOrderWeightMin;
      TargetOrderWeightMax = order.TargetOrderWeightMax;
      FKCustomerId = order.FKCustomerId;
      FKReheatingGroupId = order.FKReheatingGroupId;
      NextAggregate = order.NextAggregate;
      OperationCode = order.OperationCode;
      QualityPolicy = order.QualityPolicy;
      ExtraLabelInformation = order.ExtraLabelInformation;
      Heat = new VM_HeatOverview(order.PRMMaterials.OfType<PRMMaterial>().FirstOrDefault().PRMHeat);
      HeatName = Heat.HeatName;
      Steelgrade = order.PRMMaterialCatalogue.PRMSteelgrade.SteelgradeCode;
      FKHeatIdRef = Heat.HeatId;
      FKMaterialCatalogueId = order.PRMMaterialCatalogue?.MaterialCatalogueId;
      MetallicYield = metallicYield;
      Completion = completion;

      if (order.PRMMaterialCatalogue != null)
        MC = new VM_MaterialCatalogue(order.PRMMaterialCatalogue);

      if (order.PRMProductCatalogue != null)
        PC = new VM_ProductCatalogue(order.PRMProductCatalogue);

      if (order.PRMProductCatalogue?.PRMSteelgrade != null)
        SC = new VM_Steelgrade(order.PRMProductCatalogue.PRMSteelgrade);


      UnitConverterHelper.ConvertToLocal<VM_WorkOrderOverview>(this);
    }

    public VM_WorkOrderOverview(long workOrderId, OrderStatus workOrderStatus, string workOrderName, DateTime? toBeCompletedBefore)
    {
      WorkOrderId = workOrderId;
      WorkOrderName = workOrderName;
      ToBeCompletedBefore = toBeCompletedBefore;
      WorkOrderStatus = ResxHelper.GetResxByKey(workOrderStatus.ToString());

      UnitConverterHelper.ConvertToLocal<VM_WorkOrderOverview>(this);
    }

    public VM_WorkOrderOverview(long workOrderId, string workOrderName, DateTime createdTs, double targetOrderWeight, PE.DbEntity.Enums.OrderStatus workOrderStatus)
    {
      WorkOrderId = workOrderId;
      WorkOrderName = workOrderName;
      CreatedTs = createdTs;
      TargetOrderWeight = targetOrderWeight;
      WorkOrderStatus = ResxHelper.GetResxByKey(workOrderStatus.ToString());

      UnitConverterHelper.ConvertToLocal<VM_WorkOrderOverview>(this);
    }
  }
}
