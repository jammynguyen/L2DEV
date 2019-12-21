using System;
using SMF.HMIWWW.Attributes;
using SMF.DbEntity.Model;
using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.Resources;
using PE.DbEntity.Model;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollsManagement
{
  public class VM_RollsWithTypes : VM_Base
  {
    #region properties
    public virtual long? Id { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "RollName", "NAME_RollName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollName { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "Status", "NAME_RollStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short Status { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "RollTypeId", "NAME_RollTypeName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? RollTypeId { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "RollTypeName", "NAME_RollTypeName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollTypeName { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "ActualDiameter", "NAME_DiameterActual")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormatAttribute("FORMAT_SmallDiameter")]
    //[SmfRangeAttribute(typeof(VM_RollsWithTypes), "ActualDiameter", "RANGE_MIN_Diameter", "RANGE_MAX_Diameter")]
    [SmfUnitAttribute("UNIT_SmallDiameter")]
    public virtual double? ActualDiameter { get; set; }
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "InitialDiameter", "NAME_DiameterInitial")]
    [SmfFormatAttribute("FORMAT_SmallDiameter")]
    //[SmfRangeAttribute(typeof(VM_RollsWithTypes), "InitialDiameter", "RANGE_MIN_Diameter", "RANGE_MAX_Diameter")]
    [SmfUnitAttribute("UNIT_SmallDiameter")]
    public virtual double? InitialDiameter { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "MinimumDiameter", "NAME_DiameterMinium")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    //[SmfRangeAttribute(typeof(VM_RollsWithTypes), "MinimumDiameter", "RANGE_MIN_Diameter", "RANGE_MAX_Diameter")]
    [SmfUnitAttribute("UNIT_SmallDiameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? MinimumDiameter { get; set; }
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "Supplier", "NAME_Supplier")]
    //[Required()]
    public virtual string Supplier { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "GroovesNumber", "NAME_RollGroovesNumber")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? GroovesNumber { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "RollTypeDescription", "NAME_DescriptionType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollTypeDescription { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "DiameterMin", "NAME_DiameterMinium")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormatAttribute("FORMAT_Diameter")]
    //[SmfRangeAttribute(typeof(VM_RollsWithTypes), "DiameterMin", "RANGE_MIN_Diameter", "RANGE_MAX_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    public virtual double? DiameterMin { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "DiameterMax", "NAME_DiameterMaximum")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormatAttribute("FORMAT_SmallDiameter")]
    //[SmfRangeAttribute(typeof(VM_RollsWithTypes), "DiameterMax", "RANGE_MIN_Diameter", "RANGE_MAX_Diameter")]
    [SmfUnitAttribute("UNIT_SmallDiameter")]
    public virtual double? DiameterMax { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "RoughnessMin", "NAME_RoughnessMinimum")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormatAttribute("FORMAT_Roughness")]
    public virtual double? RoughnessMin { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "RoughnessMax", "NAME_RoughnessMaximum")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormatAttribute("FORMAT_Roughness")]
    public virtual double? RoughnessMax { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "YieldStrengthRef", "NAME_YieldStrengthRef")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormatAttribute("FORMAT_YieldStrengthRef")]
    public virtual double? YieldStrengthRef { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "SteelgradeRoll", "NAME_SteelGradeName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string SteelgradeRoll { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "Length", "NAME_Length")]
    [SmfFormatAttribute("FORMAT_Length")]
    //[SmfRangeAttribute(typeof(VM_RollsWithTypes), "Length", "RANGE_MIN_RollLength", "RANGE_MAX_RollLength")]
    [SmfUnitAttribute("UNIT_RollLength")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? Length { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "StatusName", "NAME_RollStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string StatusName { get; set; }

    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "RollSetUpper", "NAME_RollSetUpper")]
    public virtual string RollSetUpper { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "RollSetBottom", "NAME_RollSetBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetBottom { get; set; }

    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "RollSetThird", "NAME_RollSetThird")]
    public virtual string RollSetThird { get; set; }

    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "RollSetName", "NAME_RollSetName")]
    public virtual string RollSetName { get; set; }
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "ScrapReason", "NAME_RollScrapReason")]
    public virtual RollScrapReason? ScrapReason { get; set; }
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "ScrapDate", "NAME_ScrapDate")]
    public virtual DateTime? ScrapDate { get; set; }
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfDisplayAttribute(typeof(VM_RollsWithTypes), "Description", "NAME_Description")]
    public virtual string Description { get; set; }
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? RollsetId { get; set; }

    //[SmfDisplayAttribute(typeof(VM_RollsWithTypes), "Adjust", "NAME_Adjust")]
    //public virtual short? Adjust { get; set; }

    #endregion
    #region ctor
    public VM_RollsWithTypes()
    {
      this.Status = (short)RollStatus.New;
    }
    public VM_RollsWithTypes(V_RollsWithTypes rt)
    {
      this.Id = rt.RollId;
      this.RollName = rt.RollName;
      this.Status = rt.Status;
      this.RollTypeId = rt.RollTypeId;
      this.RollTypeName = rt.RollTypeName;
      this.ActualDiameter = rt.ActualDiameter;
      this.InitialDiameter = rt.InitialDiameter;
      this.MinimumDiameter = rt.MinimumDiameter;
      this.Supplier = rt.Supplier;
      this.GroovesNumber = rt.GroovesNumber;
      this.RollTypeDescription = rt.RollTypeDescription;
      this.DiameterMin = rt.DiameterMin ?? 0;
      this.DiameterMax = rt.DiameterMax ?? 0;
      this.RoughnessMin = rt.RoughnessMin ?? 0;
      this.RoughnessMax = rt.RoughnessMax ?? 0;
      this.YieldStrengthRef = rt.YieldStrengthRef ?? 0;
      this.SteelgradeRoll = rt.SteelgradeRoll;
      this.Length = rt.Length;
      this.RollSetUpper = rt.RollSetUpper;
      this.RollSetBottom = rt.RollSetBottom;
      this.RollSetThird = rt.RollSetThird;

      if (rt.RollSetUpper != null)
      {
        this.RollSetName = rt.RollSetUpper;
        this.RollsetId = rt.RollSetIdUpper;
      }
      else if (rt.RollSetBottom != null)
      {
        this.RollSetName = rt.RollSetBottom;
        this.RollsetId = rt.RollSetIdBottom;
      }
      else if (rt.RollSetThird != null)
      {
        this.RollSetName = rt.RollSetThird;
        this.RollsetId = rt.RollSetIdThird;
      }


      this.ScrapReason = (rt.ScrapReason != null) ? (RollScrapReason)rt.ScrapReason : RollScrapReason.Other;
      this.ScrapDate = rt.ScrapDate;


      UnitConverterHelper.ConvertToLocal<VM_RollsWithTypes>(this);
    }
    #endregion
  }



}
