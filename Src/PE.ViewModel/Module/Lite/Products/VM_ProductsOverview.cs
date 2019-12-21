using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Quality;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Products
{
  public class VM_ProductsOverview : VM_Base
  {
    [SmfDisplay(typeof(VM_ProductsOverview), "ProductId", "NAME_ProductId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? ProductId { get; set; }

    [SmfDisplay(typeof(VM_ProductsOverview), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_ProductsOverview), "ProductName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public string ProductName { get; set; }

    [SmfDisplay(typeof(VM_ProductsOverview), "IsAssigned", "NAME_IsAssigned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsAssigned { get; set; }

    [SmfDisplay(typeof(VM_ProductsOverview), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_ProductsOverview), "Weight", "NAME_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double Weight { get; set; }

    [SmfDisplay(typeof(VM_ProductsOverview), "ProductCatalogueName", "NAME_ProductCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueName { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaterialCatalogueName", "NAME_MaterialCatalogueName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialCatalogueName { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "WorkOrderCreated", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? WorkOrderCreated { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "ProductCatalogueDescription", "NAME_Description")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueDescription { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "ExitSpeed", "NAME_ExitSpeed")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double ExitSpeed { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "Length", "NAME_Length")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double Length { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "ProductCatalogueLength", "NAME_Length")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? ProductCatalogueLength { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "ProductCatalogueLengthMin", "NAME_Length")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? ProductCatalogueLengthMin { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "ProductCatalogueLengthMax", "NAME_Length")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? ProductCatalogueLengthMax { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaxOvality", "NAME_MaxOvality")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaxOvality { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaxTensile", "NAME_MaxTensile")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaxTensile { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaxYieldPoint", "NAME_MaxYieldPoint")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaxYieldPoint { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "Ovality", "NAME_Ovality")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Ovality { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "ProfileToleranceMin", "NAME_ProfileToleranceMin")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? ProfileToleranceMin { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "ProfileToleranceMax", "NAME_ProfileToleranceMax")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? ProfileToleranceMax { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "StdGapTime", "NAME_StdGapTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? StdGapTime { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "StdMetallicYield", "NAME_StdMetallicYield")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? StdMetallicYield { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "StdProductionTime", "NAME_StdProductionTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? StdProductionTime { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "StdProductivity", "NAME_StdProductivity")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double StdProductivity { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "StdQualityYield", "NAME_StdQualityYield")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double StdQualityYield { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "StdRollingTime", "NAME_StdRollingTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double StdRollingTime { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "StdUtilizationTime", "NAME_StdUtilizationTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double StdUtilizationTime { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "Thickness", "NAME_Thickness")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Thickness { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "Thickness", "NAME_ThicknessMin")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? ThicknessMin { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "ThicknessMax", "NAME_ThicknessMax")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? ThicknessMax { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "ProductCatalogueWeight", "NAME_Thickness")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? ProductCatalogueWeight { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "WeightMin", "NAME_ThicknessMin")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? WeightMin { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "WeightMax", "NAME_ThicknessMax")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? WeightMax { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "Thickness", "NAME_Thickness")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? Width { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "Thickness", "NAME_ThicknessMin")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? WidthMin { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "ThicknessMax", "NAME_ThicknessMax")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? WidthMax { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaterialCatalogueLength", "NAME_Length")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaterialCatalogueLength { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaterialCatalogueLengthMin", "NAME_Length")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaterialCatalogueLengthMin { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaterialCatalogueLengthMax", "NAME_Length")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaterialCatalogueLengthMax { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaterialThickness", "NAME_Thickness")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaterialCatalogueThickness { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaterialThickness", "NAME_ThicknessMin")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaterialCatalogueThicknessMin { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaterialThicknessMax", "NAME_ThicknessMax")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaterialCatalogueThicknessMax { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaterialWeight", "NAME_Thickness")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaterialCatalogueWeight { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaterialWeightMin", "NAME_ThicknessMin")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaterialCatalogueWeightMin { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaterialWeightMax", "NAME_ThicknessMax")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaterialCatalogueWeightMax { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaterialThickness", "NAME_Thickness")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaterialWidth { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaterialThickness", "NAME_ThicknessMin")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaterialCatalogueWidthMin { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "MaterialThicknessMax", "NAME_ThicknessMax")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MaterialCatalogueWidthMax { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "HeatCreated", "NAME_HeatCreated")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? HeatCreated { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "HeatSteelgradeCode", "NAME_HeatSteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatSteelgradeCode { get; set; }
    [SmfDisplay(typeof(VM_ProductsOverview), "HeatSteelgradeDescription", "NAME_HeatSteelgradeDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatSteelgradeDescription { get; set; }
    public VM_ProductCatalogue PC { get; set; }
    public VM_MaterialCatalogue BC { get; set; }
    public VM_WorkOrderOverview WorkOrderOverview { get; set; }
    public VM_RawMaterialFacts Rmf { get; set; }
    public VM_HeatOverview HeatOverview { get; set; }
    public PE.DbEntity.Enums.ProductQuality QualityEnum { get; set; }
    public int QualityEnumKey { get; set; }

    public VM_ProductsOverview() { }

    public VM_ProductsOverview(PRMProduct data)
    {
      ProductId = data.ProductId;
      CreatedTs = data.CreatedTs;
      ProductName = data.ProductName;
      IsAssigned = data.IsAssigned;
      Weight = data.Weight;
      QualityEnum = data.QualityEnum;
      QualityEnumKey = (int)data.QualityEnum;

			if(data.MVHRawMaterials != null)
      {

      }
      if (data.PRMWorkOrder != null)
      {
        WorkOrderOverview  = new VM_WorkOrderOverview(data.PRMWorkOrder);
        WorkOrderCreated = data.PRMWorkOrder.CreatedTs;
        WorkOrderName = data.PRMWorkOrder?.WorkOrderName;
        if (data.PRMWorkOrder.PRMProductCatalogue != null)
        {
          PC = new VM_ProductCatalogue(data.PRMWorkOrder.PRMProductCatalogue);
        }
        if (data.PRMWorkOrder.PRMMaterialCatalogue != null)
        {
          BC = new VM_MaterialCatalogue(data.PRMWorkOrder.PRMMaterialCatalogue);
        }
        if (data.PRMWorkOrder.PRMMaterials != null)
        {
          HeatOverview = new VM_HeatOverview(data.PRMWorkOrder.PRMMaterials.OfType<PRMMaterial>().FirstOrDefault().PRMHeat);
          HeatName = HeatOverview.HeatName;
          HeatCreated = HeatOverview.CreatedTs;
          HeatSteelgradeCode = HeatOverview.SteelgradeCode;
          HeatSteelgradeDescription = HeatOverview.SC.Description;
        }
      }
    }
  }
}
