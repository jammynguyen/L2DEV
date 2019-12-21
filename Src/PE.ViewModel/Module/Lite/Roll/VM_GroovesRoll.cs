using System;
using SMF.HMIWWW.Attributes;
using SMF.DbEntity.Model;
using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.Resources;
using PE.DbEntity.Model;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.Models;

namespace PE.HMIWWW.ViewModel.Module
{
  public class VM_GroovesRoll : VM_Base
  {
    #region properties

    [SmfUnitAttribute("UNIT_WeightTons")]
    [SmfFormatAttribute("FORMAT_Weight")]
    [SmfDisplayAttribute(typeof(VM_GroovesRoll), "AccWeight", "NAME_AccWeight")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public double? AccWeight { get; set; }

    //[SmfUnitAttribute("UNIT_WeightTons")]
    //[SmfFormatAttribute("FORMAT_Weight")]
    //[SmfDisplayAttribute(typeof(VM_GroovesRoll), "AccWeightLimit", "NAME_AccWeightLimit_ToTable")]
    //[DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    //public virtual double? AccWeightLimit { get; set; }

    [SmfDisplayAttribute(typeof(VM_GroovesRoll), "AccBilletCnt", "NAME_AccBilletCnt")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public long? AccBilletCnt { get; set; }

    [SmfDisplayAttribute(typeof(VM_GroovesRoll), "AccBilletCntLimit", "NAME_AccBilletCntLimit_ToTable")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? AccBilletCntLimit { get; set; }

    [SmfDisplayAttribute(typeof(VM_GroovesRoll), "GrooveTemplateId", "NAME_GrooveTemplateId")]
    public long? GrooveTemplateId { get; set; }

    [SmfDisplayAttribute(typeof(VM_GroovesRoll), "GrooveTemplateName", "NAME_GrooveTemplateName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string GrooveTemplateName { get; set; }

    [SmfDisplayAttribute(typeof(VM_GroovesRoll), "GrooveConfirmed", "NAME_GrooveConfirmed")]
    public virtual Boolean GrooveConfirmed { get; set; }

    [SmfDisplayAttribute(typeof(VM_GroovesRoll), "GroovesStatus", "NAME_GrooveStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? GrooveStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_GroovesRoll), "GrooveShortName", "NAME_GrooveNameShort")]
    public virtual string GrooveShortName { get; set; }

    [SmfDisplayAttribute(typeof(VM_GroovesRoll), "GrooveNumber", "NAME_GrooveNumber")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? GrooveNumber { get; set; }

    #endregion

    #region ctor
    public VM_GroovesRoll()
    {

    }
    public VM_GroovesRoll(V_RollHistory rb)
    {
      //  change from Krzysiek - compensate after lunch after presentation for costumer
      AccWeight = rb.AccWeight;
      AccBilletCnt = rb.AccBilletCnt;
      GrooveTemplateId = rb.FKGrooveTemplateId;
      GrooveTemplateName = rb.GrooveTemplateName;
      GrooveStatus = rb.GrooveStatus;
      GrooveShortName = rb.GrooveTemplateName;
      GrooveNumber = rb.GrooveNumber;
      AccBilletCntLimit = 0;
      //AccWeightLimit = rb.AccWeightLimit;
      UnitConverterHelper.ConvertToLocal<VM_GroovesRoll>(this);
    }

    #endregion
  }
}
