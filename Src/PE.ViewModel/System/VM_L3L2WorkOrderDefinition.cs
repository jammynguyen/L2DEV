using PE.DbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.ComponentModel.DataAnnotations;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_L3L2WorkOrderDefinition : VM_Base
    {
        #region properties
        [Editable(false)]
        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "CounterId", "NAME_CounterId")]
        public virtual long CounterId { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "CreatedTs", "NAME_CreatedTs")]
        public virtual DateTime CreatedTs { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "UpdatedTs", "NAME_UpdatedTs")]
        public virtual DateTime UpdatedTs { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "CommStatus", "NAME_CommStatus")]
        public virtual string CommStatus { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "WorkOrderName", "NAME_WorkOrderName")]
        public virtual String WorkOrderName { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "ProductName", "NAME_ProductName")]
        public virtual String ProductName { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "CreatedInL3", "NAME_CreatedInL3")]
        public virtual DateTime? CreatedInL3 { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "ToBeCompletedBefore", "NAME_ToBeCompletedBefore")]
        public virtual DateTime? ToBeCompletedBefore { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "CustomerName", "NAME_CustomerName")]
        public virtual String CustomerName { get; set; }

        [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "TargetOrderWeight", "NAME_Weight")]
        [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
        [SmfUnit("UNIT_Weight")]
        public double? TargetOrderWeight { get; set; }

        [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "TargetOrderWeightMin", "NAME_Min")]
        [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
        [SmfUnit("UNIT_Weight")]
        public double? TargetOrderWeightMin { get; set; }

        [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "TargetOrderWeightMax", "NAME_Max")]
        [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
        [SmfUnit("UNIT_Weight")]
        public double? TargetOrderWeightMax { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "HeatName", "NAME_HeatName")]
        public virtual String HeatName { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "RawMaterialNumber", "NAME_RawMaterialNumber")]
        public virtual Int32? RawMaterialNumber { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "RawMaterialShapeType", "NAME_RawMaterialShapeType")]
        public virtual String RawMaterialShapeType { get; set; }

        [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "RawMaterialThickness", "NAME_RawMaterialThickness")]
        [SmfFormat("FORMAT_BilletThickness", NullDisplayText = "-")]
        [SmfUnit("UNIT_BilletThickness")]
        public double? RawMaterialThickness { get; set; }

        [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "RawMaterialWidth", "NAME_RawMaterialWidth")]
        [SmfFormat("FORMAT_BilletWidth", NullDisplayText = "-")]
        [SmfUnit("UNIT_BilletWidth")]
        public double? RawMaterialWidth { get; set; }

        [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "RawMaterialLength", "NAME_RawMaterialLength")]
        [SmfFormat("FORMAT_BilletLength", NullDisplayText = "-")]
        [SmfUnit("UNIT_BilletLength")]
        public double? RawMaterialLength { get; set; }

        [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "RawMaterialWeight", "NAME_RawMaterialWeight")]
        [SmfFormat("FORMAT_BilletWeight", NullDisplayText = "-")]
        [SmfUnit("UNIT_BilletWeight")]
        public double? RawMaterialWeight { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "SteelgradeCode", "NAME_SteelgradeCode")]
        public virtual String SteelgradeCode { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "ExternalSteelgradeCode", "NAME_ExternalSteelgradeCode")]
        public virtual String ExternalSteelgradeCode { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "RawMaterialType", "NAME_RawMaterialType")]
        public virtual String RawMaterialType { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "NextAggregate", "NAME_NextAggregate")]
        public virtual String NextAggregate { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "OperationCode", "NAME_OperationCode")]
        public virtual String OperationCode { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "ReheatingGroupName", "NAME_ReheatingGroupName")]
        public virtual String ReheatingGroupName { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "QualityPolicy", "NAME_QualityPolicy")]
        public virtual String QualityPolicy { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "ExtraLabelInformation", "NAME_ExtraLabelInformation")]
        public virtual String ExtraLabelInformation { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "ValidationCheck", "NAME_ValidationCheck")]
        public virtual String ValidationCheck { get; set; }

        [SmfDisplayAttribute(typeof(VM_L3L2WorkOrderDefinition), "WorkOrderNameStatus", "NAME_WorkOrderNameStatus")]
        public virtual String WorkOrderNameStatus { get; set; }
        #endregion

        #region ctor
        public VM_L3L2WorkOrderDefinition(){}

        public VM_L3L2WorkOrderDefinition(L3L2WorkOrderDefinition l3L2WorkOrderDefinition)
        {
            this.CounterId = l3L2WorkOrderDefinition.CounterId;
            this.CreatedTs = l3L2WorkOrderDefinition.CreatedTs;
            this.UpdatedTs = l3L2WorkOrderDefinition.UpdatedTs;
            this.CommStatus = ResxHelper.GetResxByKey(l3L2WorkOrderDefinition.CommStatus.ToString());
            this.WorkOrderName = l3L2WorkOrderDefinition.WorkOrderName;
            this.ProductName = l3L2WorkOrderDefinition.ProductName;
            this.CreatedInL3 = l3L2WorkOrderDefinition.CreatedInL3;
            this.ToBeCompletedBefore = l3L2WorkOrderDefinition.ToBeCompletedBefore;
            this.CustomerName = l3L2WorkOrderDefinition.CustomerName;
            this.TargetOrderWeight = l3L2WorkOrderDefinition.TargetOrderWeight;
            this.TargetOrderWeightMin = l3L2WorkOrderDefinition.TargetOrderWeightMin;
            this.TargetOrderWeightMax = l3L2WorkOrderDefinition.TargetOrderWeightMax;
            this.HeatName = l3L2WorkOrderDefinition.HeatName;
            this.RawMaterialNumber = l3L2WorkOrderDefinition.RawMaterialNumber;
            this.RawMaterialShapeType = l3L2WorkOrderDefinition.RawMaterialShapeType;
            this.RawMaterialThickness = l3L2WorkOrderDefinition.RawMaterialThickness;
            this.RawMaterialWidth = l3L2WorkOrderDefinition.RawMaterialWidth;
            this.RawMaterialLength = l3L2WorkOrderDefinition.RawMaterialLength;
            this.RawMaterialWeight = l3L2WorkOrderDefinition.RawMaterialWeight;
            this.SteelgradeCode = l3L2WorkOrderDefinition.SteelgradeCode;
            this.ExternalSteelgradeCode = l3L2WorkOrderDefinition.ExternalSteelgradeCode;
            this.RawMaterialType = l3L2WorkOrderDefinition.RawMaterialType;
            this.NextAggregate = l3L2WorkOrderDefinition.NextAggregate;
            this.OperationCode = l3L2WorkOrderDefinition.OperationCode;
            this.ReheatingGroupName = l3L2WorkOrderDefinition.ReheatingGroupName;
            this.QualityPolicy = l3L2WorkOrderDefinition.QualityPolicy;
            this.ExtraLabelInformation = l3L2WorkOrderDefinition.ExtraLabelInformation;
            this.ValidationCheck = l3L2WorkOrderDefinition.ValidationCheck;
            this.WorkOrderNameStatus = l3L2WorkOrderDefinition.WorkOrderNameStatus;

            UnitConverterHelper.ConvertToLocal(this);
        }
        #endregion
    }
}
