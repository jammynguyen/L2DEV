using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Feature
{
  public class VM_Feature : VM_Base
  {
    [SmfDisplayAttribute(typeof(VM_Feature), "FeatureId", "NAME_FeatureId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long FeatureId { get; set; }


        [SmfDisplayAttribute(typeof(VM_Feature), "FeatureCode", "NAME_FeatureCode")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public int FeatureCode { get; set; }

        [SmfDisplayAttribute(typeof(VM_Feature), "FeatureName", "NAME_FeatureName")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string FeatureName { get; set; }


        [SmfDisplayAttribute(typeof(VM_Feature), "IsNewProcessingStep", "NAME_IsNewProcessingStep")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsNewProcessingStep { get; set; }


        [SmfDisplayAttribute(typeof(VM_Feature), "FkAssetId", "NAME_FkAssetId")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public long FkAssetId { get; set; }

        [SmfDisplayAttribute(typeof(VM_Feature), "IsMaterialRelated", "NAME_IsMaterialRelated")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsMaterialRelated { get; set; }


        [SmfDisplayAttribute(typeof(VM_Feature), "IsLengthRelated", "NAME_IsLengthRelated")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsLengthRelated { get; set; }


        [SmfDisplayAttribute(typeof(VM_Feature), "IsActive", "NAME_IsActive")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsActive { get; set; }


        [SmfDisplayAttribute(typeof(VM_Feature), "IsTrigger", "NAME_IsTrigger")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsTrigger { get; set; }


        public VM_Feature(MVHFeature table)
        {
            FkAssetId = table.FKAssetId;
            FeatureCode = table.FeatureCode;
            FeatureName = table.FeatureName;
            IsNewProcessingStep = table.IsNewProcessingStep;
            FeatureId = table.FeatureId;
            IsMaterialRelated = table.IsMaterialRelated;
            IsLengthRelated = table.IsLengthRelated;
            IsActive = table.IsActive;
            IsTrigger = table.IsTrigger;

        }

        public VM_Feature()
        {

        }


    }
}
