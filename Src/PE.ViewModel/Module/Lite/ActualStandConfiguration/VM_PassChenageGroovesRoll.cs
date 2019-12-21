using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration
{
  public class VM_PassChangeGroovesRoll : VM_Base
  {
    #region properties
    [SmfUnitAttribute("UNIT_WeightTons")]
    [SmfFormatAttribute("FORMAT_OrderWeight")]
    [SmfDisplayAttribute(typeof(VM_PassChangeGroovesRoll), "AccWeight", "NAME_AccWeight")]
    public double AccWeight { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeGroovesRoll), "AccBilletCnt", "NAME_AccBilletCnt")]
    public long AccBilletCnt { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeGroovesRoll), "AccBilletCntLimit", "NAME_AccBilletCntLimit_ToTable")]
    public virtual long? AccBilletCntLimit { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeGroovesRoll), "GrooveTemplateId", "NAME_GrooveTemplateId")]
    public long GrooveTemplateId { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeGroovesRoll), "GrooveTemplateName", "NAME_GrooveTemplateName")]
    public string GrooveTemplateName { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeGroovesRoll), "GrooveConfirmed", "NAME_GrooveConfirmed")]
    public virtual Boolean GrooveConfirmed { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeGroovesRoll), "GroovesStatus", "NAME_GrooveStatus")]
    public virtual short GrooveStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeGroovesRoll), "GrooveShortName", "NAME_GrooveNameShort")]
    public virtual string GrooveShortName { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeGroovesRoll), "GrooveNumber", "NAME_GrooveNumber")]
    public virtual short GrooveNumber { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeGroovesRoll), "AccWeightRatio", "NAME_AccWeightRatio_ToTable")]
    [SmfFormatAttribute("FORMAT_Percent")]
    [SmfUnitAttribute("UNIT_Percent")]
    public virtual double? AccWeightRatio { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeGroovesRoll), "EstimatedPassChangeDate", "NAME_EstimatedProdDate")]
    public DateTime? EstimatedPassChangeDate { get; set; }

    #endregion
    #region ctor
    public VM_PassChangeGroovesRoll()
    {
    }

    public VM_PassChangeGroovesRoll(V_RollHistory rb, DateTime? estimationDateTime = null)
    {
      this.AccWeight = rb.AccWeight;
      this.AccBilletCnt = rb.AccBilletCnt;
      this.GrooveTemplateId = rb.FKGrooveTemplateId;
      this.GrooveTemplateName = rb.GrooveTemplateName;
      this.GrooveStatus = rb.GrooveStatus;
      this.GrooveShortName = rb.GrooveTemplateName;
      this.AccBilletCntLimit = 0;
      this.GrooveNumber = rb.GrooveNumber;
      this.AccWeightRatio = (rb.AccWeightLimit ?? 0.0) == 0.0 ? 0.0 : AccWeight / rb.AccWeightLimit;
      this.EstimatedPassChangeDate = estimationDateTime;

      UnitConverterHelper.ConvertToLocal<VM_PassChangeGroovesRoll>(this);
    }
    #endregion
  }
}
