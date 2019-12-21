using System;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using PE.HMIWWW.Core.Resources;


namespace PE.HMIWWW.ViewModel.Module.Lite.Heat
{
    public class VM_Heat : VM_Base
    {
        [SmfDisplayAttribute(typeof(VM_Heat), "HeatId", "NAME_HeatId")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public long HeatId { get; set; }

        [SmfDisplayAttribute(typeof(VM_Heat), "HeatName", "NAME_Name")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        [SmfRequired]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
        public string HeatName { get; set; }

        [SmfDisplayAttribute(typeof(VM_Heat), "FKMaterialCatalogueIdRef", "NAME_MaterialCatalogue")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        [SmfRequired]
        public string FKMaterialCatalogueId { get; set; }

        [SmfDisplayAttribute(typeof(VM_Heat), "FKHeatSupplierId", "NAME_Supplier")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public long? FKHeatSupplierId { get; set; }

        [SmfDisplayAttribute(typeof(VM_Heat), "HeatWeightRef", "NAME_HeatWeight")]
        [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
        [SmfUnit("UNIT_Weight")]
        public double? HeatWeightRef { get; set; }

        [SmfDisplayAttribute(typeof(VM_Heat), "IsDummy", "NAME_IsDummy")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool? IsDummy { get; set; }

        public VM_Heat() { }
    }
}
