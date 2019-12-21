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
  public class VM_HeatSummary : VM_Base
  {
    [SmfDisplayAttribute(typeof(VM_HeatSummary), "HeatId", "NAME_HeatId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long HeatId { get; set; }

    [SmfDisplayAttribute(typeof(VM_HeatSummary), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplayAttribute(typeof(VM_HeatSummary), "HeatName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplayAttribute(typeof(VM_HeatSummary), "MaterialCatalogueName", "NAME_MaterialCatalogueRef")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialCatalogueName { get; set; }

    [SmfDisplayAttribute(typeof(VM_HeatSummary), "IsDummy", "NAME_IsDummy")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplayAttribute(typeof(VM_HeatSummary), "SteelgradeCode", "NAME_SteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    [SmfDisplayAttribute(typeof(VM_HeatSummary), "SteelgradeName", "NAME_SteelGradeName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }

    [SmfDisplayAttribute(typeof(VM_HeatSummary), "SteelGradeDensity", "NAME_SteelDensity")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? SteelGradeDensity { get; set; }

    [SmfDisplayAttribute(typeof(VM_HeatSummary), "Weigth", "NAME_Weigth")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double Weigth { get; set; }

    [SmfDisplayAttribute(typeof(VM_HeatSummary), "HeatWeightRef", "NAME_HeatWeightRef")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? HeatWeightRef { get; set; }

    public VM_HeatSummary() { }
    public VM_HeatSummary(V_Heats heat)
    {
      HeatId = heat.HeatId;
      CreatedTs = heat.CreatedTs;
      HeatName = heat.HeatName;
      IsDummy = heat.IsDummy;
      SteelgradeCode = heat.SteelgradeCode;
      SteelgradeName = heat.SteelgradeName;
      SteelGradeDensity = heat.Density;
      HeatWeightRef = heat.HeatWeightRef;
      MaterialCatalogueName = heat.MaterialCatalogueName;

      UnitConverterHelper.ConvertToLocal<VM_HeatSummary>(this);
    }
  }
}
