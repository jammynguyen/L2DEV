using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.Resources;
using PE.DbEntity.Models;
using SMF.HMIWWW.UnitConverter;
using System.Linq;

namespace PE.HMIWWW.ViewModel.Module.Lite.WorkOrder
{
    public class VM_WorkOrder : VM_Base
    {
        [SmfDisplayAttribute(typeof(VM_WorkOrder), "WorkOrderId", "NAME_WorkOrderId")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public long WorkOrderId { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "WorkOrderName", "NAME_Name")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        [SmfRequired]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
        public string WorkOrderName { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "FKHeatIdRef", "NAME_HeatName")]
        [DisplayFormat(HtmlEncode = false, ConvertEmptyStringToNull = true)]
        public string FKHeatIdRef { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "FKProductCatalogueId", "NAME_ProductCatalogue")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        [SmfRequired]
        public long FKProductCatalogueId { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "WorkOrderStatus", "NAME_WorkOrderStatus")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        [SmfRequired]
        public PE.DbEntity.Enums.OrderStatus WorkOrderStatus { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "FKMaterialCatalogueId", "NAME_MaterialCatalogue")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        [SmfRequired]
        public long FKMaterialCatalogueId { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "IsTestOrder", "NAME_IsTestOrder")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsTestOrder { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "TargetOrderWeight", "NAME_Weight")]
        [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
        [SmfUnit("UNIT_Weight")]
        [SmfRequired]
        public double TargetOrderWeight { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "TargetOrderWeightMin", "NAME_TargetOrderWeightMin")]
        [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
        [SmfUnit("UNIT_Weight")]
        public double? TargetOrderWeightMin { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "TargetOrderWeightMax", "NAME_TargetOrderWeightMax")]
        [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
        [SmfUnit("UNIT_Weight")]
        public double? TargetOrderWeightMax { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "CreatedInL3", "NAME_CreatedInL3")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        [SmfRequired]
        public DateTime CreatedInL3 { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "ToBeCompletedBefore", "NAME_ToBeCompletedBefore")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        [SmfRequired]
        public DateTime ToBeCompletedBefore { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "FKCustomerId", "NAME_CustomerName")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public long? FKCustomerId { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "FKReheatingGroupId", "NAME_ReheatingGroupNameLong")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public long? FKReheatingGroupId { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "NextAggregate", "NAME_NextAggregate")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
        public string NextAggregate { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "OperationCode", "NAME_OperationCode")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
        public string OperationCode { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "QualityPolicy", "NAME_QualityPolicy")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
        public string QualityPolicy { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "ExtraLabelInformation", "NAME_ExtraLabelInformation")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
        public string ExtraLabelInformation { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "ExternalSteelgradeCode", "NAME_ExternalSteelgradeCode")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
        public string ExternalSteelgradeCode { get; set; }

        [SmfDisplayAttribute(typeof(VM_WorkOrder), "MaterialNumber", "NAME_MaterialsNumber")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public long? MaterialsNumber { get; set; }

        public VM_WorkOrder() { }

        public VM_WorkOrder(PRMWorkOrder data)
        {
            WorkOrderId = data.WorkOrderId;
            WorkOrderName = data.WorkOrderName;
            FKHeatIdRef = data.PRMMaterials.OfType<PRMMaterial>().FirstOrDefault().PRMHeat?.HeatName;
            FKProductCatalogueId = data.FKProductCatalogueId;
            FKMaterialCatalogueId = data.PRMMaterialCatalogue.MaterialCatalogueId;
            IsTestOrder = data.IsTestOrder;
            TargetOrderWeight = data.TargetOrderWeight;
            TargetOrderWeightMin = data.TargetOrderWeightMin;
            TargetOrderWeightMax = data.TargetOrderWeightMax;
            CreatedInL3 = data.CreatedInL3;
            ToBeCompletedBefore = data.ToBeCompletedBefore;
            FKCustomerId = data.FKCustomerId;
            FKReheatingGroupId = data.FKReheatingGroupId;
            NextAggregate = data.NextAggregate;
            OperationCode = data.OperationCode;
            QualityPolicy = data.QualityPolicy;
            ExtraLabelInformation = data.ExtraLabelInformation;
            ExternalSteelgradeCode = data.ExternalSteelgradeCode;
            WorkOrderStatus = data.WorkOrderStatus;

            UnitConverterHelper.ConvertToLocal<VM_WorkOrder>(this);
        }
    }
}
