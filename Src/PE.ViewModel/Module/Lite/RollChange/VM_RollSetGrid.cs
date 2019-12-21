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
  public class VM_RollsetGrid : VM_Base
  {
    #region properties
    public long? RollsetId { get; set; }
    public long? RollIdUpper { get; set; }
    public long? RollIdBottom { get; set; }
    public long? RollIdThird { get; set; }
    public long? RollTypeIdUpper { get; set; }
    public long? RollTypeIdBottom { get; set; }
    public long? RollTypeIdThird { get; set; }
    public long? CassetteId { get; set; }
    public long? InterCassetteId { get; set; }
    public long? RollSetHistoryId { get; set; }
    public long? StandId { get; set; }
    public short? PositionInCassette { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "RollSetStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short RollSetStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "RollSetType", "NAME_Type")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short RollSetType { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "RollSetName", "NAME_RollSetName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string RollSetName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "RollNameUpper", "NAME_RollNameUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string RollNameUpper { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "RollNameBottom", "NAME_RollNameBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string RollNameBottom { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "RollNameThird", "NAME_RollNameThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string RollNameThird { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "DiameterUpper", "NAME_DiameterUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    public double? DiameterUpper { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "DiameterBottom", "NAME_DiameterBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    public double? DiameterBottom { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "DiameterThird", "NAME_DiameterThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    public double? DiameterThird { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "RollTypeUpper", "NAME_RollTypeUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string RollTypeUpper { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "RollTypeBottom", "NAME_RollTypeBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string RollTypeBottom { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "RollTypeThird", "NAME_RollTypeThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string RollTypeThird { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "StandStatus", "NAME_StandStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short StandStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "RollSetHistoryStatus", "NAME_RollSetHistoryStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short RollSetHistoryStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "CassetteName", "NAME_CassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string CassetteName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "InterCassetteName", "NAME_InterCassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string InterCassetteName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "CassetteStatus", "NAME_CassetteStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short? CassetteStatus { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "StandNo", "NAME_StandNo")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short? StandNo { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "Mounted", "NAME_Mounted")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public DateTime? Mounted { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollsetGrid), "Dismounted", "NAME_Dismounted")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public DateTime? Dismounted { get; set; }

    #endregion

    #region ctor
    public VM_RollsetGrid()
    {

    }

    public VM_RollsetGrid(V_RollsetOverviewNewest entity)
    {
      this.CassetteId = entity.CassetteId;
      this.StandId = entity.StandId;
      this.CassetteName = entity.CassetteName;
      this.StandNo = entity.StandNo;
      this.StandStatus = entity.Status;
      this.CassetteName = entity.CassetteName;
      this.CassetteStatus = entity.CassetteStatus;
      this.RollsetId = entity.RollSetId;
      this.RollSetName = entity.RollSetName;
      this.RollSetHistoryId = entity.RollSetHistoryId;
      this.RollSetHistoryStatus = entity.RollSetHistoryStatus;
      this.RollSetStatus = entity.RollSetStatus;
      this.RollSetType = entity.RollSetType;
      this.RollTypeUpper = entity.RollTypeUpper;
      this.RollTypeBottom = entity.RollTypeBottom;
      this.RollTypeThird = entity.RollTypeThird;
      this.RollTypeIdUpper = entity.RollTypeIdUpper;
      this.RollTypeIdBottom = entity.RollTypeIdBottom;
      this.RollTypeIdThird = entity.RollTypeIdThird;
      this.RollNameUpper = entity.RollNameUpper;
      this.RollNameBottom = entity.RollNameBottom;
      this.RollNameThird = entity.RollNameThird;
      this.RollIdUpper = entity.RollIdUpper;
      this.RollIdBottom = entity.RollIdBottom;
      this.RollIdThird = entity.RollIdThird;
      this.PositionInCassette = entity.PositionInCassette;
      this.Mounted = entity.Mounted;
      this.Dismounted = entity.Dismounted;
      this.DiameterUpper = entity.DiameterUpper;
      this.DiameterBottom = entity.DiameterBottom;
      this.DiameterThird = entity.DiameterThird;

      UnitConverterHelper.ConvertToLocal<VM_RollsetGrid>(this);
    }

    #endregion

  }
}
