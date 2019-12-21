using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement
{
  public class VM_RollSetOverview : VM_Base
  {
    #region properties
    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollSetName", "NAME_RollSetName")]
    public virtual long? Id { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollSetStatus", "NAME_RollsetStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? RollSetStatus { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollSetStatusTxt", "NAME_RollsetStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetStatusTxt { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollSetStatusNew", "NAME_RollsetStatusNew")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? RollSetStatusNew { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollSetType", "NAME_RollsetType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? RollSetType { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollSetTypeTxt", "NAME_RollsetType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetTypeTxt { get; set; }


    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollSetName", "NAME_RollSetName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollNameUpper", "NAME_RollUpperName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? UpperRollId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "DiameterUpper", "NAME_DiameterUpper")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? UpperDiameter { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollNameUpper", "NAME_RollUpperName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollNameUpper { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollTypeUpper", "NAME_RollTypeUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollTypeUpper { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollTypeUpper", "NAME_RollTypeUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? RollTypeIdUpper { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollNameBottom", "NAME_RollBottomName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? BottomRollId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "DiameterBottom", "NAME_DiameterBottom")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? BottomDiameter { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollNameBottom", "NAME_RollBottomName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollNameBottom { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollTypeBottom", "NAME_RollTypeBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollTypeBottom { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollTypeIdBottom", "NAME_RollTypeBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? RollTypeIdBottom { get; set; }

    public virtual long? RollSetHistoryId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollSetHistoryStatus", "NAME_RollsetHistoryStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? RollSetHistoryStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "CassetteName", "NAME_CassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string CassetteName { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "PositionInCassette", "NAME_PositionInCassette")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? PositionInCassette { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "StandNo", "NAME_StandNo")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? StandNo { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "Mounted", "NAME_Mounted")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual DateTime? Mounted { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "Dismounted", "NAME_Dismounted")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual DateTime? Dismounted { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollStatusName", "NAME_RollsetStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollStatusName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "CassetteId", "NAME_CassetteName")]
    //[Editable(true)] 
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? CassetteId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollSetStatusActual", "NAME_RollSetStatusActual")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetStatusActual { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "CassetteStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? CassetteStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "ThirdRollId", "NAME_ThirdRollId")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? ThirdRollId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "DiameterThird", "NAME_DiameterThird")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? ThirdDiameter { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollNameThird", "NAME_RollThirdName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollNameThird { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollTypeThird", "NAME_RollTypeThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollTypeThird { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "RollTypeIdThird", "NAME_RollTypeIdThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? RollTypeIdThird { get; set; }

    //[SmfDisplayAttribute(typeof(VM_RollSetOverview), "IsThirdRoll", "NAME_IsThirdRoll")]
    //public virtual short? IsThirdRoll { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "InterCassetteId", "NAME_InterCassetteName")]
    //[Editable(true)] 
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? InterCassetteId { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "InterCassetteName", "NAME_InterCassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string InterCassetteName { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "InternalCassetteStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? InternalCassetteStatus { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "GrooveTemplateName", "NAME_GrooveTemplate")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string GrooveTemplateName { get; set; }

    #endregion
    #region ctor
    public VM_RollSetOverview()
    {
    }

    public VM_RollSetOverview(V_RollsetOverviewNewest rs, V_RollHistoryPerGroove gr = null)  // new
    {
      this.Id = rs.RollSetId;
      this.RollSetStatus = (short)rs.RollSetStatus;
      this.RollSetStatusNew = (short)rs.RollSetStatus;
      this.RollSetType = rs.RollSetType;
      this.RollSetName = rs.RollSetName;
      //if (this.InterCassetteId != null)

      this.UpperRollId = rs.RollIdUpper;
      this.UpperDiameter = rs.DiameterUpper;
      this.RollNameUpper = rs.RollNameUpper;
      this.RollTypeUpper = rs.RollTypeUpper;
      this.BottomRollId = rs.RollIdBottom;
      this.BottomDiameter = rs.DiameterBottom;
      this.RollNameBottom = rs.RollNameBottom;
      this.RollTypeBottom = rs.RollTypeBottom;
      this.RollSetHistoryId = rs.RollSetHistoryId;
      this.RollSetHistoryStatus = rs.RollSetHistoryStatus;
      this.CassetteName = rs.CassetteName;

      this.PositionInCassette = rs.PositionInCassette;
      this.StandNo = rs.StandNo;
      this.Mounted = rs.Mounted;
      this.Dismounted = rs.Dismounted;
      this.RollSetStatusActual = "";
      this.RollTypeIdUpper = rs.RollTypeIdUpper;
      this.RollTypeIdBottom = rs.RollTypeIdBottom;
      this.ThirdRollId = rs.RollIdThird;
      this.ThirdDiameter = rs.DiameterThird;
      this.RollNameThird = rs.RollNameThird;
      this.RollTypeThird = rs.RollTypeThird;
      this.RollTypeIdThird = rs.RollTypeIdThird;


      this.CassetteId = rs.CassetteId;

      //this.CassetteName = (rs.CassetteName != null) ? CassetteName: "";
      this.CassetteName = rs.CassetteName;
      this.CassetteStatus = rs.CassetteStatus;
      this.RollSetStatusTxt = ResourceController.GetRollSetStatusValue(this.RollSetStatus.ToString());
      this.RollSetTypeTxt = ResourceController.GetRollSetTypeValue(this.RollSetType.ToString());
      if (gr != null)
        this.GrooveTemplateName = gr.GrooveTemplateName;


      UnitConverterHelper.ConvertToLocal<VM_RollSetOverview>(this);

    }

    //public VM_RollSetOverview(VRollsetOverviewForIntercassette rs)
    //{
    //  this.Id = rs.RollSetId;
    //  this.RollSetStatus = (short)rs.RollSetStatus;
    //  this.RollSetStatusNew = (short)rs.RollSetStatus;
    //  this.RollSetType = rs.RollSetType;
    //  this.RollSetName = rs.RollSetName;
    //  this.UpperRollId = rs.RollIdUpper;
    //  this.UpperDiameter = rs.DiameterUpper;
    //  this.RollNameUpper = rs.RollNameUpper;
    //  this.RollTypeUpper = rs.RollTypeUpper;
    //  this.BottomRollId = rs.RollIdBottom;
    //  this.BottomDiameter = rs.DiameterBottom;
    //  this.RollNameBottom = rs.RollNameBottom;
    //  this.RollTypeBottom = rs.RollTypeBottom;
    //  this.RollSetHistoryId = rs.RollSetHistoryId;
    //  this.RollSetHistoryStatus = rs.RollSetHistoryStatus;
    //  this.CassetteName = rs.CassetteName;

    //  this.PositionInCassette = rs.PositionInCassette;
    //  this.StandNo = rs.StandNo;
    //  this.Mounted = rs.Mounted;
    //  this.Dismounted = rs.Dismounted;
    //  this.RollSetStatusActual = "";
    //  this.RollTypeIdUpper = rs.RollTypeIdUpper;
    //  this.RollTypeIdBottom = rs.RollTypeIdBottom;
    //  this.ThirdRollId = rs.RollIdThird;
    //  this.ThirdDiameter = rs.DiameterThird;
    //  this.RollNameThird = rs.RollNameThird;
    //  this.RollTypeThird = rs.RollTypeThird;
    //  this.RollTypeIdThird = rs.RollTypeIdThird;

    //  this.InterCassetteId = rs.FkInterCassetteId;
    //  this.CassetteId = rs.CassetteId;

    //  this.InterCassetteId = rs.FkInterCassetteId;
    //  this.CassetteName = rs.CassetteName;
    //  this.CassetteStatus = rs.CassetteStatus;
    //  this.InterCassetteName = rs.InternalCassetteName;
    //  this.InternalCassetteStatus = rs.InternalCasseteStatus;
    //  this.RollSetStatusTxt = ResourceController.GetRollSetStatusValue(this.RollSetStatus.ToString());
    //  this.RollSetTypeTxt = ResourceController.GetRollSetTypeValue(this.RollSetType.ToString());

    //  UnitConverterHelper.ConvertToLocal<VM_RollSetOverview>(this);
    //}
    #endregion
  }
}
