using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.Resources;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_MaterialCatalogue: VM_Base
  {
    public long Id { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "Name", "NAME_Name")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfRequired]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "Description", "NAME_Description")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Description { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "SteelgradeCode", "NAME_Steelgrade")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "SteelgradeName", "NAME_SteelgradeName")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "Length", "NAME_Length")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-")]
    [SmfUnit("UNIT_Length")]
    [SmfRange(typeof(VM_MaterialCatalogue), "Length", "BilletLength")]
    [SmfRequired]
    public double? Length { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "Width", "NAME_Width")]
    [SmfFormat("FORMAT_Width", NullDisplayText = "-")]
    [SmfUnit("UNIT_Width")]
    [SmfRange(typeof(VM_MaterialCatalogue), "Width", "BilletWidth")]
    [SmfRequired]
    public double Width { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "Thickness", "NAME_Thickness")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-")]
    [SmfUnit("UNIT_Diameter")]
    [SmfRange(typeof(VM_MaterialCatalogue), "Thickness", "BilletThickness")]
    [SmfRequired]
    public double Thickness { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "Weight", "NAME_Weight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    [SmfRange(typeof(VM_MaterialCatalogue), "Weight", "BilletWeight")]
    [SmfRequired]
    public double Weight { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "SAPNumber", "NAME_SapNumber")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SAPNumber { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "Shape", "NAME_Shape")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Shape { get; set; }

    // Details
    [SmfDisplay(typeof(VM_MaterialCatalogue), "LengthMin", "NAME_LengthMin")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "LengthMin", "BilletLength")]
    [SmfUnit("UNIT_Length")]
    [SmfRequired]
    public double? LengthMin { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "LengthMax", "NAME_LengthMax")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "LengthMax", "BilletLength")]
    [SmfUnit("UNIT_Length")]
    public double? LengthMax { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "WidthMin", "NAME_WidthMin")]
    [SmfFormat("FORMAT_Width", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "WidthMin", "BilletWidth")]
    [SmfUnit("UNIT_Width")]
    public double? WidthMin { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "WidthMax", "NAME_WidthMax")]
    [SmfFormat("FORMAT_Width", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "WidthMax", "BilletWidth")]
    [SmfUnit("UNIT_Width")]
    public double? WidthMax { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "ThicknessMin", "NAME_ThicknessMin")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "ThicknessMin", "BilletThickness")]
    [SmfUnit("UNIT_Diameter")]
    public double? ThicknessMin { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "ThicknessMax", "NAME_ThicknessMax")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "ThicknessMax", "BilletThickness")]
    [SmfUnit("UNIT_Diameter")]
    public double? ThicknessMax { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "Type", "NAME_Type")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Type { get; set; }


    [SmfDisplay(typeof(VM_MaterialCatalogue), "WeightMin", "NAME_WeightMin")]
    [SmfFormat("FORMAT_WeightMin", NullDisplayText = "-", DataFormatString = "{0:0.###}")]
    [SmfRange(typeof(VM_MaterialCatalogue), "WeightMin", "BilletWeight")]
    [SmfUnit("UNIT_Weight")]
    public double? WeightMin { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "WeightMax", "NAME_WeightMax")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-", DataFormatString = "{0:0.###}")]
    [SmfRange(typeof(VM_MaterialCatalogue), "WeightMax", "BilletWeight")]
    [SmfUnit("UNIT_Weight")]
    public double? WeightMax { get; set; }
    public long? SteelgradeId { get; set; }
    [SmfDisplay(typeof(VM_MaterialCatalogue), "ShapeId", "NAME_Shape")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long ShapeId { get; set; }

    public long? TypeId { get; set; }

    public VM_MaterialCatalogue()
    {

    }

    public VM_MaterialCatalogue(PRMMaterialCatalogue p)
    {
      Id = p.MaterialCatalogueId;
      MaterialName = p.MaterialCatalogueName;
      Description = p.Description;
      SteelgradeCode = p.PRMSteelgrade?.SteelgradeCode;
      SteelgradeName = p.PRMSteelgrade?.SteelgradeName;
      Shape = p.PRMShape?.ShapeSymbol;
      Length = p.Length;
      Width = p.Width;
      Thickness = p.Thickness;
      Weight = p.Weight;
      SAPNumber = p.SAPNumber;
      SteelgradeId = p.FKSteelgradeId;
      ShapeId = p.FKShapeId;

      // Details
      LengthMin = p.LengthMin;
      LengthMax = p.LengthMax;
      WidthMin = p.WidthMin;
      WidthMax = p.WidthMax;
      ThicknessMin = p.ThicknessMin;
      ThicknessMax = p.ThicknessMax;
      WeightMin = p.WeightMin;
      WeightMax = p.WeightMax;
      Type = p.PRMMaterialCatalogueType?.MaterialCatalogueTypeSymbol;
      TypeId = p.FKMaterialCatalogueTypeId;


      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
