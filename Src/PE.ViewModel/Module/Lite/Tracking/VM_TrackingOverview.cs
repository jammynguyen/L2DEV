using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Products;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Tracking
{
  public class VM_TrackingOverview : VM_Base
  {
    #region prop
    [SmfDisplay(typeof(VM_TrackingOverview), "RawMaterialLastUpdated", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime RawMaterialLastUpdated { get; set; }
    [SmfDisplay(typeof(VM_TrackingOverview), "RawMaterialLastWeight", "NAME_CurrentWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? RawMaterialLastWeight { get; set; }
    [SmfDisplay(typeof(VM_TrackingOverview), "RawMaterialInitialWeight", "NAME_RawInitialWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? RawMaterialInitialWeight { get; set; }
    [SmfDisplay(typeof(VM_TrackingOverview), "RawMaterialLastLength", "NAME_CurrentLength")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Length")]
    [SmfFormat("FORMAT_Length")]
    public double? RawMaterialLastLength { get; set; }
    [SmfDisplay(typeof(VM_TrackingOverview), "RawMaterialInitialLength", "NAME_RawInitialLength")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Length")]
    [SmfFormat("FORMAT_Length")]
    public double? RawMaterialInitialLength { get; set; }
    public long? Sorting { get; set; }

    [SmfDisplay(typeof(VM_TrackingOverview), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }
    [SmfDisplay(typeof(VM_TrackingOverview), "ParentAssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParentAssetName { get; set; }
    [SmfDisplay(typeof(VM_TrackingOverview), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }
    [SmfDisplay(typeof(VM_TrackingOverview), "ProductCatalogueName", "NAME_ProductCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueName { get; set; }
    [SmfDisplay(typeof(VM_TrackingOverview), "SteelGroupName", "NAME_SteelGroupName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelGroupName { get; set; }
    [SmfDisplay(typeof(VM_TrackingOverview), "SteelgradeName", "NAME_SteelGradeName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }
    [SmfDisplay(typeof(VM_TrackingOverview), "MaterialName", "NAME_L3MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }
    [SmfDisplay(typeof(VM_TrackingOverview), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }
    [SmfDisplay(typeof(VM_TrackingOverview), "MaterialCatalogueName", "NAME_MaterialCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialCatalogueName { get; set; }
    
    public long? DimWorkOrderKey { get; set; }
    public long? DimHeatKey { get; set; }
    public long? DimProductKey { get; set; }
    public short DimRawMaterialStatusKey { get; set; }
    public long DimRawMaterialKey { get; set; }
    public long? AssetId { get; set; }
    public long? DimLastAssetKey { get; set; }

    public VM_WorkOrderOverview WO { get; set; }
    public VM_HeatOverview H { get; set; }
    public VM_ProductsOverview P { get; set; }
    public VM_Asset A { get; set; }
    #endregion

    #region ctor
    public VM_TrackingOverview(V_RawMaterialOverview data)
    {
      RawMaterialLastUpdated = data.RawMaterialLastUpdated;
      RawMaterialLastWeight = data.RawMaterialLastWeight;
      RawMaterialLastLength = data.RawMaterialLastLength;
      Sorting = data.Sorting;

      SteelGroupName = data.SteelGroupName;
      SteelgradeName = data.SteelgradeName;
      MaterialName = data.RawMaterialName;
      WorkOrderName = data.WorkOrderName;
      ProductCatalogueName = data.ProductCatalogueName;
      HeatName = data.HeatName;
      ParentAssetName = data.ParentAssetName;

      DimWorkOrderKey = data.DimWorkOrderKey;
      DimHeatKey = data.DimHeatKey;
      DimLastAssetKey = data.DimLastAssetKey;
      DimRawMaterialStatusKey = data.DimRawMaterialStatusKey;
      DimRawMaterialKey = data.DimRawMaterialKey;
      DimProductKey = data.DimProductKey;
      AssetId = data.DimLastAssetKey;

      UnitConverterHelper.ConvertToLocal<VM_TrackingOverview>(this);
    }

    public VM_TrackingOverview(V_RawMaterialOverview data, PRMWorkOrder workOrder, PRMHeat heat)
    {
      RawMaterialLastUpdated = data.RawMaterialLastUpdated;
      RawMaterialLastWeight = data.RawMaterialLastWeight;
      RawMaterialInitialWeight = data.RawMaterialInitialWeight;
      RawMaterialLastLength = data.RawMaterialLastLength;
      RawMaterialInitialLength = data.RawMaterialInitialLength;

      SteelGroupName = data.SteelGroupName;
      SteelgradeName = data.SteelgradeName;
      MaterialCatalogueName = data.MaterialCatalogueName;
      MaterialName = data.MaterialName;
      WorkOrderName = data.WorkOrderName;
      ProductCatalogueName = data.ProductCatalogueName;
      AssetName = data.AssetName;
      HeatName = data.HeatName;

      if (workOrder != null)
        WO = new VM_WorkOrderOverview(workOrder);
      else WO = new VM_WorkOrderOverview();
      if (heat != null)
        H = new VM_HeatOverview(heat);
      else H = new VM_HeatOverview();

      UnitConverterHelper.ConvertToLocal<VM_TrackingOverview>(this);
    }
    #endregion
  }
}
