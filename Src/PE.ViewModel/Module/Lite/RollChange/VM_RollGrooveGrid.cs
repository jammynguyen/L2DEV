using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollChange
{
  public class VM_RollGrooveGrid : VM_Base
  {
    #region properties
    public long? RollId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollGrooveGrid), "GrooveNumber", "NAME_GrooveNumberShort")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short GrooveNumber { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollGrooveGrid), "GrooveTemplateName", "NAME_GrooveTemplateNameShort")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string GrooveTemplateName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollGrooveGrid), "AccWeight", "NAME_AccWeightShort")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfUnitAttribute("UNIT_WeightTons")]
    [SmfFormatAttribute("FORMAT_Weight")]
    public double? AccWeight { get; set; }

    //[SmfDisplayAttribute(typeof(VM_RollGrooveGrid), "AccWeightLimit", "NAME_AccWeightLimitShort")]
    //[DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    //[SmfUnitAttribute("UNIT_WeightTons")]
    //[SmfFormatAttribute("FORMAT_Weight")]
    //public double? AccWeightLimit { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollGrooveGrid), "AccWeightRatio", "NAME_AccWeightRatio_ToTable")]
    [SmfFormatAttribute("FORMAT_Percent")]
    [SmfUnitAttribute("UNIT_Percent")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? AccWeightRatio { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollGrooveGrid), "StatusString", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short GrooveStatus { get; set; }
    #endregion

    #region ctor
    public VM_RollGrooveGrid()
    {

    }

    public VM_RollGrooveGrid(V_RollHistoryPerGroove entity)
    {
      this.RollId = entity.RollId;
      this.GrooveNumber = entity.GrooveNumber;
      this.GrooveTemplateName = entity.GrooveTemplateName;
      this.AccWeight = entity.AccWeight;
      //this.AccWeightLimit = entity.AccWeightLimit ?? 0;
      this.GrooveStatus = entity.GrooveStatus;
      this.AccWeightRatio = this.AccWeightRatio = (entity.AccWeightLimit ?? 0.0) == 0.0 ? 0.0 : AccWeight / entity.AccWeightLimit; ;

      UnitConverterHelper.ConvertToLocal<VM_RollGrooveGrid>(this);
    }
    #endregion

  }
}
