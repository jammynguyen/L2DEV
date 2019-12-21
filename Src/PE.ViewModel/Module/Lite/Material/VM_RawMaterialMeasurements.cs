using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialMeasurements : VM_Base
  {
    #region props
    public long? RawMaterialId { get; set; }
    public long MeasurementId { get; set; }
    public long FeatureId { get; set; }
    [SmfDisplay(typeof(V_RawMaterialMeasurements), "FeatureName", "NAME_FeatureName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string FeatureName { get; set; }
    [SmfDisplay(typeof(V_RawMaterialMeasurements), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }
    [SmfDisplay(typeof(V_RawMaterialMeasurements), "ParentAssetName", "NAME_ParentAssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParentAssetName { get; set; }
    [SmfDisplay(typeof(V_RawMaterialMeasurements), "PassNo", "NAME_PassNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short PassNo { get; set; }
    [SmfDisplay(typeof(V_RawMaterialMeasurements), "IsLastPass", "NAME_Last")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsLastPass { get; set; }
    [SmfDisplay(typeof(V_RawMaterialMeasurements), "IsValid", "NAME_Valid")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsValid { get; set; }
    [SmfDisplay(typeof(V_RawMaterialMeasurements), "MeasurementTime", "NAME_MeasurementTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime MeasurementTime { get; set; }
    [SmfDisplay(typeof(V_RawMaterialMeasurements), "MeasurementValueMin", "NAME_Min")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MeasurementValueMin { get; set; }
    [SmfDisplay(typeof(V_RawMaterialMeasurements), "MeasurementValueMax", "NAME_Max")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MeasurementValueMax { get; set; }
    [SmfDisplay(typeof(V_RawMaterialMeasurements), "UnitSymbol", "NAME_Unit")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string UnitSymbol { get; set; }
    [SmfDisplay(typeof(V_RawMaterialMeasurements), "Average", "NAME_Average")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MeasurementValueAvg { get; set; }
    #endregion

    #region ctor
    public VM_RawMaterialMeasurements() { }

    public VM_RawMaterialMeasurements(V_RawMaterialMeasurements data)
    {
      RawMaterialId = data.RawMaterialId;
      MeasurementId = data.MeasurementId;
      FeatureId = data.FeatureId;
      FeatureName = data.FeatureName;
      AssetName = data.AssetName;
      ParentAssetName = data.ParentAssetName;
      PassNo = data.PassNo;
      IsLastPass = data.IsLastPass;
      IsValid = data.IsValid;
      MeasurementTime = data.MeasurementTime;
      MeasurementValueMin = data.MeasurementValueMin;
      MeasurementValueMax = data.MeasurementValueMax;
      UnitSymbol = data.UnitSymbol;
      MeasurementValueAvg = data.MeasurementValueAvg;

      UnitConverterHelper.ConvertToLocal(this);
    }
    #endregion
  }

}
