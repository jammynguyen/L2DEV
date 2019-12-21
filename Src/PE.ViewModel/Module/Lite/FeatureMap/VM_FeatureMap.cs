using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PE.HMIWWW.ViewModel.Module.Lite.FeatureMap
{
  public class VM_FeatureMap : VM_Base
  {
    [SmfDisplayAttribute(typeof(VM_FeatureMap), "SeqNo", "NAME_SeqNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? SeqNo { get; set; }

    [SmfDisplayAttribute(typeof(VM_FeatureMap), "AssetCode", "NAME_AssetCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetCode { get; set; }

    [SmfDisplayAttribute(typeof(VM_FeatureMap), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }


        [SmfDisplayAttribute(typeof(VM_FeatureMap), "IsCheckpoint", "NAME_IsCheckpoint")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsCheckpoint { get; set; }

        [SmfDisplayAttribute(typeof(VM_FeatureMap), "FeatureCode", "NAME_FeatureCode")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public int FeatureCode { get; set; }

        [SmfDisplayAttribute(typeof(VM_FeatureMap), "FeatureName", "NAME_FeatureName")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string FeatureName { get; set; }


        [SmfDisplayAttribute(typeof(VM_FeatureMap), "IsNewProcessingStep", "NAME_IsNewProcessingStep")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsNewProcessingStep { get; set; }


        [SmfDisplayAttribute(typeof(VM_FeatureMap), "UnitSymbol", "NAME_UnitSymbol")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string UnitSymbol { get; set; }

        [SmfDisplayAttribute(typeof(VM_FeatureMap), "DataTypeName", "NAME_DataTypeName")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string DataTypeName { get; set; }

        [SmfDisplayAttribute(typeof(VM_FeatureMap), "IsMaterialRelated", "NAME_IsMaterialRelated")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsMaterialRelated { get; set; }


        [SmfDisplayAttribute(typeof(VM_FeatureMap), "IsLengthRelated", "NAME_IsLengthRelated")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsLengthRelated { get; set; }


        [SmfDisplayAttribute(typeof(VM_FeatureMap), "IsActive", "NAME_IsActive")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsActive { get; set; }


        [SmfDisplayAttribute(typeof(VM_FeatureMap), "IsTrigger", "NAME_IsTrigger")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsTrigger { get; set; }

        public VM_FeatureMap(V_FeaturesMap table)
        {
            SeqNo = table.SeqNo;
            AssetCode = table.AssetCode;
            AssetName = table.AssetName;
            IsCheckpoint = table.IsCheckpoint;
            FeatureCode = table.FeatureCode;
            FeatureName = table.FeatureName;
            IsNewProcessingStep = table.IsNewProcessingStep;
            UnitSymbol = table.UnitSymbol;
            DataTypeName = table.DataTypeName;
            IsMaterialRelated = table.IsMaterialRelated;
            IsLengthRelated = table.IsLengthRelated;
            IsActive = table.IsActive;
            IsTrigger = table.IsTrigger;
        }


  }
}
