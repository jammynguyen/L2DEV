using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Product
{
  public class VM_ProductCatalogue : VM_Base
  {
    public long Id { get; set; }
    [SmfFormat("GLOB_ShortDateTime_FORMAT")]
    public DateTime LastUpdateTs { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "Name", "NAME_Name")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfRequired]
    public string Name { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "Length", "NAME_Length")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-")]
    [SmfRange(typeof(VM_ProductCatalogue), "Length", "ProductLength")]
    [SmfUnit("UNIT_Length")]
    public double? Length { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "LengthMax", "NAME_LengthMax")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-")]
    [SmfRange(typeof(VM_ProductCatalogue), "LengthMax", "ProductLength")]
    [SmfUnit("UNIT_Length")]
    public double? LengthMax { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "LengthMin", "NAME_LengthMin")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-")]
    [SmfRange(typeof(VM_ProductCatalogue), "LengthMin", "ProductLength")]
    [SmfUnit("UNIT_Length")]
    public double? LengthMin { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "Width", "NAME_Width")]
    [SmfFormat("FORMAT_Width", NullDisplayText = "-")]
    [SmfRange(typeof(VM_ProductCatalogue), "Width", "ProductWidth")]
    [SmfUnit("UNIT_Width")]
    public double? Width { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "WidthMax", "NAME_WidthMax")]
    [SmfFormat("FORMAT_Width", NullDisplayText = "-")]
    [SmfRange(typeof(VM_ProductCatalogue), "WidthMax", "ProductWidth")]
    [SmfUnit("UNIT_Width")]
    public double? WidthMax { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "WidthMin", "NAME_WidthMin")]
    [SmfFormat("FORMAT_Width", NullDisplayText = "-")]
    [SmfRange(typeof(VM_ProductCatalogue), "WidthMin", "ProductWidth")]
    [SmfUnit("UNIT_Width")]
    public double? WidthMin { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "Thickness", "NAME_Thickness")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-")]
    [SmfRange(typeof(VM_ProductCatalogue), "Thickness", "ProductThickness")]
    [SmfUnit("UNIT_Diameter")]
    public double? Thickness { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "ThicknessMax", "NAME_ThicknessMax")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-")]
    [SmfRange(typeof(VM_ProductCatalogue), "ThicknessMax", "ProductThickness")]
    [SmfUnit("UNIT_Diameter")]
    public double? ThicknessMax { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "ThicknessMin", "NAME_ThicknessMin")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-")]
    [SmfRange(typeof(VM_ProductCatalogue), "ThicknessMin", "ProductThickness")]
    [SmfUnit("UNIT_Diameter")]
    public double? ThicknessMin { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "Weight", "NAME_Weight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfRange(typeof(VM_ProductCatalogue), "Weight", "ProductWeight")]
    [SmfUnit("UNIT_Weight")]
    public double? Weight { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "WeightMax", "NAME_WeightMax")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfRange(typeof(VM_ProductCatalogue), "WeightMax", "ProductWeight")]
    [SmfUnit("UNIT_Weight")]
    public double? WeightMax { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "WeightMin", "NAME_WeightMin")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfRange(typeof(VM_ProductCatalogue), "WeightMin", "ProductWeight")]
    [SmfUnit("UNIT_Weight")]
    public double? WeightMin { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "Ovality", "NAME_Ovality")]
    [SmfFormat("FORMAT_Ovality", NullDisplayText = "-")]
    public double? Ovality { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "OvalityMax", "NAME_MaxOvality")]
    [SmfFormat("FORMAT_Ovality", NullDisplayText = "-")]
    public double? OvalityMax { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "Description", "NAME_Description")]
    public string Description { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "SAPNumber", "NAME_SapNumber")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string SAPNumber { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "Steelgrade", "NAME_Steelgrade")]
    public string Steelgrade { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "Shape", "NAME_Shape")]
    public string Shape { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "Type", "NAME_Type")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Type { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "ProfileToleranceMin", "NAME_ProfileToleranceMin")]
    [SmfFormat("FORMAT_ProfileToleranceMin", NullDisplayText = "-")]
    public double? ProfileToleranceMin { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "ProfileToleranceMax", "NAME_ProfileToleranceMax")]
    [SmfFormat("FORMAT_ProfileToleranceMax", NullDisplayText = "-")]
    public double? ProfileToleranceMax { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "StdGapTime", "NAME_StdGapTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Second")]
    [SmfRequired]
    public double StdGapTime { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "StdRollingTime", "NAME_StdRollingTime")]
    [SmfUnit("UNIT_Second")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double StdRollingTime { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "StdProductionTime", "NAME_StdProductionTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Second")]
    public double? StdProductionTime { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "StdMetallicYield", "NAME_StdMetallicYield")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public double StdMetallicYield { get; set; }
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfDisplay(typeof(VM_ProductCatalogue), "StdQualityYield", "NAME_StdQualityYield")]
    [SmfRequired]
    public double StdQualityYield { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "StdUtilizationTime", "NAME_StdUtilizationTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Second")]
    [SmfRequired]
    public double StdUtilizationTime { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "StdTPH", "NAME_StdTph")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false, DataFormatString = "{0:0.###}")]
    [SmfRequired]
    public double? StdProductivity { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "Slitting", "NAME_Slitting")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool Slitting { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "ExitSpeed", "NAME_ExitSpeed")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false, DataFormatString = "{0:0.##}")]
    [SmfRequired]
    [SmfUnit("UNIT_Speed")]
    public double? ExitSpeed { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "MaxTensile", "NAME_MaxTensile")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaxTensile { get; set; }
    [SmfDisplay(typeof(VM_ProductCatalogue), "MaxYieldPoint", "NAME_MaxYieldPoint")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaxYieldPoint { get; set; }

    public long? SteelgradeId { get; set; }
    public long ShapeId { get; set; }
    public long? TypeId { get; set; }


    public VM_ProductCatalogue() { }

    public VM_ProductCatalogue(PRMProductCatalogue p)
    {
      Id = p.ProductCatalogueId;
      Weight = p.Weight;
      WeightMax = p.WeightMax;
      WeightMin = p.WeightMin;
      LastUpdateTs = p.LastUpdateTs;
      Name = p.ProductCatalogueName;
      Length = p.Length;
      LengthMax = p.LengthMax;
      LengthMin = p.LengthMin;
      Width = p.Width;
      WidthMax = p.WidthMax;
      WidthMin = p.WidthMin;
      Thickness = p.Thickness;
      ThicknessMax = p.ThicknessMax;
      ThicknessMin = p.ThicknessMin;
      MaxTensile = p.MaxTensile;
      MaxYieldPoint = p.MaxYieldPoint;
      StdGapTime = p.StdGapTime;
      StdMetallicYield = p.StdMetallicYield;
      StdProductionTime = p.StdProductionTime;
      StdQualityYield = p.StdQualityYield;
      StdRollingTime = p.StdRollingTime;
      StdProductivity = p.StdProductivity;
      StdUtilizationTime = p.StdUtilizationTime;
      ProfileToleranceMax = p.ProfileToleranceMax;
      ProfileToleranceMin = p.ProfileToleranceMin;
      SAPNumber = p.SAPNumber;
      Slitting = p.Slitting;
      Ovality = p.Ovality;
      OvalityMax = p.MaxOvality;
      Description = p.Description;
      ExitSpeed = p.ExitSpeed;
      Steelgrade = p.PRMSteelgrade?.SteelgradeCode;
      Shape = p.PRMShape?.ShapeName;
      ShapeId = p.FKShapeId;
      SteelgradeId = p.FKSteelgradeId;
      TypeId = p.FKProductCatalogueTypeId;
      Type = p.PRMProductCatalogueType?.ProductCatalogueTypeSymbol;

      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
