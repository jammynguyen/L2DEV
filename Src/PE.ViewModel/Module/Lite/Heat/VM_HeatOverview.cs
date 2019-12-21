using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Heat
{
    public class VM_HeatOverview : VM_Base
    {
        [SmfDisplayAttribute(typeof(VM_HeatOverview), "HeatId", "NAME_HeatId")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public long HeatId { get; set; }

        [SmfDisplayAttribute(typeof(VM_HeatOverview), "CreatedTs", "NAME_CreatedTs")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public DateTime? CreatedTs { get; set; }

        [SmfDisplayAttribute(typeof(VM_HeatOverview), "HeatName", "NAME_Name")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string HeatName { get; set; }

        [SmfDisplayAttribute(typeof(VM_HeatOverview), "MaterialCatalogueName", "NAME_MaterialCatalogueRef")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string MaterialCatalogueName { get; set; }

        [SmfDisplayAttribute(typeof(VM_HeatOverview), "SupplierName", "NAME_SupplierName")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string SupplierName { get; set; }

        [SmfDisplayAttribute(typeof(VM_HeatOverview), "IsDummy", "NAME_IsDummy")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool? IsDummy { get; set; }

        [SmfDisplayAttribute(typeof(VM_HeatOverview), "Description", "NAME_SupplierDescription")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string SupplierDescription { get; set; }

        [SmfDisplayAttribute(typeof(VM_HeatOverview), "SteelgradeCode", "NAME_SteelgradeCode")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string SteelgradeCode { get; set; }

        [SmfDisplayAttribute(typeof(VM_HeatOverview), "SteelGradeDensity", "NAME_SteelDensity")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public double? SteelGradeDensity { get; set; }

        [SmfDisplayAttribute(typeof(VM_HeatOverview), "Weigth", "NAME_Weigth")]
        [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
        [SmfUnit("UNIT_Weight")]
        public double Weigth { get; set; }

        [SmfDisplayAttribute(typeof(VM_HeatOverview), "HeatWeightRef", "NAME_HeatWeightRef")]
        [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
        [SmfUnit("UNIT_Weight")]
        public double? HeatWeightRef { get; set; }

        public VM_Steelgrade SC { get; set; }

        public VM_HeatChemAnalysis HC { get; set; }

        public VM_HeatOverview() { }
        public VM_HeatOverview(PRMHeat heat)
        {
            HeatId = heat.HeatId;
            CreatedTs = heat.CreatedTs;
            HeatName = heat.HeatName;
            SupplierName = heat.PRMHeatSupplier?.SupplierName;
            IsDummy = heat.IsDummy;
            SupplierDescription = heat.PRMHeatSupplier?.Description;
            SteelgradeCode = heat.PRMMaterialCatalogue.PRMSteelgrade?.SteelgradeCode;
            SteelGradeDensity = heat.PRMMaterialCatalogue.PRMSteelgrade?.Density;
            HeatWeightRef = heat.HeatWeightRef;
            MaterialCatalogueName = heat.PRMMaterialCatalogue?.MaterialCatalogueName;
            if (heat.PRMMaterialCatalogue != null && heat.PRMMaterialCatalogue.PRMSteelgrade != null)
                SC = new VM_Steelgrade(heat.PRMMaterialCatalogue.PRMSteelgrade);
            if (heat.PRMHeatChemAnalysis != null && heat.PRMHeatChemAnalysis.Count() > 0)
                HC = new VM_HeatChemAnalysis(heat.PRMHeatChemAnalysis.FirstOrDefault());

            UnitConverterHelper.ConvertToLocal<VM_HeatOverview>(this);
        }
    }
}
