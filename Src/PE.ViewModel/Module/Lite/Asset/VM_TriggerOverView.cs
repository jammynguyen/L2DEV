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

namespace PE.HMIWWW.ViewModel.Module.Lite.Asset
{
    public class VM_TriggerOverView : VM_Base
    {
        [SmfDisplayAttribute(typeof(VM_TriggerOverView), "TriggerCode", "NAME_TriggerCode")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string TriggerCode { get; set; }

        [SmfDisplayAttribute(typeof(VM_TriggerOverView), "TriggerName", "NAME_TriggerName")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string TriggerName { get; set; }

        [SmfDisplayAttribute(typeof(VM_TriggerOverView), "AssetCode", "NAME_AssetCode")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string AssetCode { get; set; }

        [SmfDisplayAttribute(typeof(VM_TriggerOverView), "AssetName", "NAME_AssetName")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string AssetName { get; set; }

        [SmfDisplayAttribute(typeof(VM_TriggerOverView), "FeatureCode", "NAME_FeatureCode")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public int FeatureCode { get; set; }

        [SmfDisplayAttribute(typeof(VM_TriggerOverView), "FeatureName", "NAME_FeatureName")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string FeatureName { get; set; }


        [SmfDisplayAttribute(typeof(VM_TriggerOverView), "IsActive", "NAME_IsActive")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsActive { get; set; }





        public VM_TriggerOverView(V_TriggerOverview table)
        {
            TriggerCode = table.TriggerCode;
            TriggerName = table.TriggerName;
            AssetCode = table.AssetCode;
            AssetName = table.AssetName;
            FeatureCode = table.FeatureCode;
            FeatureName = table.FeatureName;
            IsActive = table.IsActive;
        }

        public VM_TriggerOverView(string triggerCode,string triggerName,bool isActive)
        {
            TriggerCode = triggerCode;
            TriggerName = triggerName;
            IsActive = isActive;
        }


        public VM_TriggerOverView()
        {


        }



    }
}
