using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollType
{
  public class VM_RollType : VM_Base
  {
    #region properties
    public virtual long? Id { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollType), "RollTypeName", "NAME_RollTypeName")]
    public virtual String RollTypeName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollType), "RollDescription", "NAME_Description")]
    public virtual String RollTypeDescription { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollType), "DiameterMin", "NAME_DiameterMinium")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    //[SmfRangeAttribute(typeof(VM_RollType), "DiameterMin", "RANGE_MIN_Diameter", "RANGE_MAX_Diameter")]
    [SmfUnitAttribute("UNIT_SmallDiameter")]
    public virtual double? DiameterMin { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollType), "DiameterMax", "NAME_DiameterMaximum")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    //[SmfRangeAttribute(typeof(VM_RollType), "DiameterMax", "RANGE_MIN_Diameter", "RANGE_MAX_Diameter")]
    [SmfUnitAttribute("UNIT_SmallDiameter")]
    public virtual double? DiameterMax { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollType), "RoughnessMin", "NAME_RoughnessMinimum")]
    [SmfFormatAttribute("FORMAT_Roughness")]
    public virtual double? RoughnessMin { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollType), "RoughnessMax", "NAME_RoughnessMaximum")]
    [SmfFormatAttribute("FORMAT_Roughness")]
    public virtual double? RoughnessMax { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollType), "YieldStrengthRef", "NAME_YieldStrengthRef")]
    [SmfFormatAttribute("FORMAT_YieldStrengthRef")]
    public virtual double? YieldStrengthRef { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollType), "SteelgradeRoll", "NAME_SteelGradeName")]
    public virtual String SteelgradeRoll { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollType), "Length", "NAME_Length")]
    [SmfFormatAttribute("FORMAT_Length")]
    public virtual double? Length { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollType), "DrawingName", "NAME_DrawingName")]
    public virtual String DrawingName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollType), "ChokeType", "NAME_ChokeType")]
    public virtual String ChokeType { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollType), "AccBilletCntLimit", "NAME_AccBilletCntLimit")]
    [SmfFormat("FORMAT_DefaultLong")]
    public virtual long? AccBilletCntLimit { get; set; }


    [SmfDisplayAttribute(typeof(VM_RollType), "Adjust", "NAME_Adjust")]
    public virtual short? Adjust { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollType), "MatchingRollsetType", "NAME_MatchingRollsetType")]
    public virtual short? MatchingRollsetType { get; set; }

    public virtual bool IsInUse { get; set; }



    #endregion
    #region ctor
    public VM_RollType()
    {
    }
    public VM_RollType(RLSRollType rt)
    {
      this.Id = rt.RollTypeId;
      this.RollTypeName = rt.RollTypeName;
      this.RollTypeDescription = rt.RollTypeDescription;
      this.DiameterMin = rt.DiameterMin;
      this.DiameterMax = rt.DiameterMax;
      this.RoughnessMin = rt.RoughnessMin;
      this.RoughnessMax = rt.RoughnessMax;
      this.YieldStrengthRef = rt.YieldStrengthRef;
      this.SteelgradeRoll = rt.SteelgradeRoll;
      this.Length = rt.Length;
      this.DrawingName = rt.DrawingName;
      this.ChokeType = rt.ChokeType;
      this.AccBilletCntLimit = rt.AccBilletCntLimit;
      this.MatchingRollsetType = rt.MatchingRollsetType;
      // this.Adjust = Convert.ToInt16(rt.Adjust);

      UnitConverterHelper.ConvertToLocal<VM_RollType>(this);
    }
    #endregion
  }
}
