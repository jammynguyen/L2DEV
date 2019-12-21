using System;
using SMF.HMIWWW.Attributes;
using SMF.DbEntity.Model;
using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.Resources;
using PE.DbEntity.Model;
using SMF.HMIWWW.UnitConverter;
using System.Reflection;
using System.Resources;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay
{
  public class VM_RollsetDisplay : VM_Base
  {


    #region properties
    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "RollSetId", "NAME_RollSetName")]
    public virtual long RollSetId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "RollSetName", "NAME_RollSetName")]
    public virtual string RollSetName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "RollSetStatus", "NAME_RollsetStatus")]
    public virtual RollSetStatus RollSetStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "RollSetType", "NAME_RollsetType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? RollSetType { get; set; }

    public virtual long RollSetHistoryId { get; set; }

    public virtual long? ActualUpperRollId { get; set; }

    public virtual long? ActualBottomRollId { get; set; }

    public virtual long? ActualThirdRollId { get; set; }

    public virtual List<VM_GroovesRoll> GrooveActualRollUpper { get; set; }

    public virtual List<VM_GroovesRoll> GrooveActualRollBottom { get; set; }

    public virtual List<VM_GroovesRoll> GrooveActualRollThird { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "ActualUpperRollName", "NAME_RollUpperName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string ActualUpperRollName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "ActualBottomRollName", "NAME_RollBottomName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string ActualBottomRollName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "ActualThirdRollName", "NAME_RollThirdName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string ActualThirdRollName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "ActualUpperDiameter", "NAME_DiameterUpper")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? ActualUpperDiameter { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "ActualBottomDiameter", "NAME_DiameterBottom")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? ActualBottomDiameter { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "ActualThirdDiameter", "NAME_DiameterThird")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? ActualThirdDiameter { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "NewUpperDiameter", "NAME_NewDiameter")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? NewUpperDiameter { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "NewBottomDiameter", "NAME_NewDiameter")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? NewBottomDiameter { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "NewThirdDiameter", "NAME_NewDiameter")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? NewThirdDiameter { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "AccWeight", "NAME_AccWeight")]
    [SmfUnitAttribute("UNIT_Weight")]
    [SmfFormatAttribute("FORMAT_Weight")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? AccWeight { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "AccBilletCnt", "NAME_AccBilletCnt")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? AccBilletCnt { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "AccBilletCntLimit", "NAME_AccBilletCntLimit_ToTable")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? AccBilletCntLimit { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "GrooveTemplateName", "NAME_GrooveTemplateName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string GrooveTemplateName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "GrooveNewTemplate", "NAME_GrooveActualTemplate")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string GrooveActualTemplate { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetDisplay), "StandNo", "NAME_StandNo")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? StandNumber { get; set; }

    #endregion
    #region ctor
    public VM_RollsetDisplay()
    {
    }

    // For RM & IM
    public VM_RollsetDisplay(V_RollsetOverviewNewest rso, IList<V_RollHistory> upperGroove, IList<V_RollHistory> bottomGroove)
    {
      this.RollSetId = Convert.ToInt64(rso.RollSetId);
      this.RollSetName = rso.RollSetName;
      this.RollSetStatus = (RollSetStatus)rso.RollSetStatus;
      this.RollSetType = rso.RollSetType;
      this.RollSetHistoryId = (short)rso.RollSetHistoryId;
      this.ActualUpperRollId = rso.RollIdUpper;
      this.ActualBottomRollId = rso.RollIdBottom;

      this.ActualUpperRollName = rso.RollNameUpper;
      this.ActualBottomRollName = rso.RollNameBottom;
      this.ActualUpperDiameter = rso.DiameterUpper;
      this.ActualBottomDiameter = rso.DiameterBottom;

      this.StandNumber = rso.StandNo;
      this.GrooveTemplateName = "";
      this.GrooveActualTemplate = "";
      this.AccBilletCnt = 0;
      this.AccBilletCntLimit = 0;
      this.AccWeight = 0;
      this.GrooveActualRollUpper = new List<VM_GroovesRoll>();
      this.GrooveActualRollBottom = new List<VM_GroovesRoll>();

      this.NewUpperDiameter = rso.DiameterUpper;
      this.NewBottomDiameter = rso.DiameterBottom;

      foreach (V_RollHistory element in upperGroove)
      {
        this.GrooveActualRollUpper.Add(new VM_GroovesRoll(element));
      }
      foreach (V_RollHistory element in bottomGroove)
      {
        this.GrooveActualRollBottom.Add(new VM_GroovesRoll(element));
      }

      UnitConverterHelper.ConvertToLocal<VM_RollsetDisplay>(this);
    }

    // For Kocks
    public VM_RollsetDisplay(V_RollsetOverviewNewest rso, IList<V_RollHistory> upperGroove, IList<V_RollHistory> bottomGroove, IList<V_RollHistory> thirdGroove)
    {
      this.RollSetId = Convert.ToInt64(rso.RollSetId);
      this.RollSetName = rso.RollSetName;
      this.RollSetStatus = (RollSetStatus)rso.RollSetStatus;
      this.RollSetType = rso.RollSetType;
      this.RollSetHistoryId = (short)rso.RollSetHistoryId;
      this.ActualUpperRollId = rso.RollIdUpper;
      this.ActualBottomRollId = rso.RollIdBottom;
      this.ActualThirdRollId = rso.RollIdThird;
      this.ActualUpperRollName = rso.RollNameUpper;
      this.ActualBottomRollName = rso.RollNameBottom;
      this.ActualThirdRollName = rso.RollNameThird;
      this.ActualUpperDiameter = rso.DiameterUpper;
      this.ActualBottomDiameter = rso.DiameterBottom;
      this.ActualThirdDiameter = rso.DiameterThird;
      this.StandNumber = rso.StandNo;
      this.GrooveTemplateName = "";
      this.GrooveActualTemplate = "";
      this.AccBilletCnt = 0;
      this.AccBilletCntLimit = 0;
      this.AccWeight = 0;
      this.NewUpperDiameter = rso.DiameterUpper;
      this.NewBottomDiameter = rso.DiameterBottom;
      this.NewThirdDiameter = rso.DiameterThird;
      this.GrooveActualRollUpper = new List<VM_GroovesRoll>();
      this.GrooveActualRollBottom = new List<VM_GroovesRoll>();
      this.GrooveActualRollThird = new List<VM_GroovesRoll>();
      foreach (V_RollHistory element in upperGroove)
      {
        this.GrooveActualRollUpper.Add(new VM_GroovesRoll(element));
      }
      foreach (V_RollHistory element in bottomGroove)
      {
        this.GrooveActualRollBottom.Add(new VM_GroovesRoll(element));
      }
      foreach (V_RollHistory element in thirdGroove)
      {
        this.GrooveActualRollThird.Add(new VM_GroovesRoll(element));
      }

      UnitConverterHelper.ConvertToLocal<VM_RollsetDisplay>(this);
    }
    #endregion
  }
}
