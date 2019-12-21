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
using PE.DbEntity.Enums;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement
{
  public class VM_RollSetOverviewFull : VM_Base
  {
    #region properties
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollSetName", "NAME_RollSetName")]
    public virtual long? Id { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollSetStatus", "NAME_RollsetStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual RollSetStatus RollSetStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollSetStatusNew", "NAME_RollsetStatusNew")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? RollSetStatusNew { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollSetType", "NAME_RollsetType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual RollSetType RollSetType { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollSetName", "NAME_RollSetName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollNameUpper", "NAME_RollUpperName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? UpperRollId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "UpperDiameter", "NAME_DiameterUpper")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? UpperDiameter { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollNameUpper", "NAME_RollUpperName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollNameUpper { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollTypeUpper", "NAME_RollTypeUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollTypeUpper { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollTypeUpper", "NAME_RollTypeUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? RollTypeIdUpper { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollNameBottom", "NAME_RollBottomName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? BottomRollId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "BottomDiameter", "NAME_DiameterBottom")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? BottomDiameter { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollNameBottom", "NAME_RollBottomName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollNameBottom { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollTypeBottom", "NAME_RollTypeBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollTypeBottom { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollTypeIdBottom", "NAME_RollTypeBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? RollTypeIdBottom { get; set; }

    public virtual long? RollSetHistoryId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollSetHistoryStatus", "NAME_RollsetHistoryStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? RollSetHistoryStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "CassetteName", "NAME_CassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string CassetteName { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "PositionInCassette", "NAME_PositionInCassette")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? PositionInCassette { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "StandNo", "NAME_StandNo")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? StandNo { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "Mounted", "NAME_Mounted")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual DateTime? Mounted { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "Dismounted", "NAME_Dismounted")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual DateTime? Dismounted { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollStatusName", "NAME_RollsetStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollStatusName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "CassetteId", "NAME_CassetteName")]
    //[Editable(true)] 
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? CassetteId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollSetStatusActual", "NAME_RollSetStatusActual")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetStatusActual { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "CassetteStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? CassetteStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "ThirdRollId", "NAME_ThirdRollId")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? ThirdRollId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "ThirdDiameter", "NAME_DiameterThird")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? ThirdDiameter { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollNameThird", "NAME_RollThirdName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollNameThird { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollTypeThird", "NAME_RollTypeThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollTypeThird { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollTypeIdThird", "NAME_RollTypeIdThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? RollTypeIdThird { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "IsThirdRoll", "NAME_IsThirdRoll")]
    public virtual short? IsThirdRoll { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "InterCassetteId", "NAME_InterCassetteName")]
    //[Editable(true)] 
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? InterCassetteId { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "InterCassetteName", "NAME_InterCassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string InterCassetteName { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "InternalCassetteStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? InternalCassetteStatus { get; set; }
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "GrooveTemplateId", "NAME_GrooveTemplate")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? GrooveTemplateId { get; set; }

    #endregion
    #region ctor
    public VM_RollSetOverviewFull()
    {
    }

    public VM_RollSetOverviewFull(short position)
    {
      this.Id = -1;
      this.RollSetStatus = 0;
      this.RollSetStatusNew = 0;
      this.RollSetType = 0;
      this.RollSetName = "";
      this.UpperRollId = null;
      this.UpperDiameter = null;
      this.RollNameUpper = "";
      this.RollTypeUpper = "";
      this.BottomRollId = null;
      this.BottomDiameter = null;
      this.RollNameBottom = "";
      this.RollTypeBottom = "";
      this.RollSetHistoryId = null;
      this.RollSetHistoryStatus = 0;
      this.CassetteId = 0;
      this.CassetteName = "";
      this.PositionInCassette = position;
      this.StandNo = null;
      this.Mounted = null;
      this.Dismounted = null;
      this.RollSetStatusActual = "";
      this.CassetteStatus = 0;
      this.RollTypeIdUpper = null;
      this.RollTypeIdBottom = null;
      this.ThirdRollId = null;
      this.ThirdDiameter = null;
      this.RollNameThird = "";
      this.RollTypeThird = "";
      this.RollTypeIdThird = null;
      this.IsThirdRoll = null;   // 0 = RM , 1 = IM, 2 = K500, 3 = K370
      this.InterCassetteId = null;

      UnitConverterHelper.ConvertToLocal<VM_RollSetOverviewFull>(this);
    }

    public VM_RollSetOverviewFull(V_RollsetOverviewNewest rs, V_CassettesOverview cass)  // new
    {
      this.Id = rs.RollSetId;
      this.RollSetStatus = (RollSetStatus)rs.RollSetStatus;
      this.RollSetStatusNew = (short)rs.RollSetStatus;
      this.RollSetType = (RollSetType)rs.RollSetType;
      this.RollSetName = rs.RollSetName;

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
      this.IsThirdRoll = Convert.ToInt16(rs.IsThirdRoll);  // <-- with null

      //condition used for proper ViewBag.CassReadyNew with CassetteId selection when rs.CassetteId is null
      if ((rs.RollSetStatus == (short)PE.DbEntity.Enums.RollSetStatus.Ready) || (rs.RollSetStatus == (short)PE.DbEntity.Enums.RollSetStatus.NotAvailable))
      {
        this.CassetteId = 0;
      }

      else
      {
        this.CassetteId = rs.CassetteId;
      }


      //this.CassetteName = (rs.CassetteName != null) ? CassetteName: "";
      this.CassetteName = rs.CassetteName;
      this.CassetteStatus = (short)(rs.CassetteStatus);


      UnitConverterHelper.ConvertToLocal<VM_RollSetOverviewFull>(this);
    }

    public VM_RollSetOverviewFull(V_RollsetOverviewNewest rs)
    {
      this.Id = rs.RollSetId;
      this.RollSetStatus = (RollSetStatus)rs.RollSetStatus;
      this.RollSetStatusNew = (short)rs.RollSetStatus;
      this.RollSetType = (RollSetType)rs.RollSetType;
      this.RollSetName = rs.RollSetName;

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
      this.IsThirdRoll = Convert.ToInt16(rs.IsThirdRoll);  // <-- with null

      //condition used for proper ViewBag.CassReadyNew with CassetteId selection when rs.CassetteId is null
      if ((rs.RollSetStatus == (short)PE.DbEntity.Enums.RollSetStatus.Ready) || (rs.RollSetStatus == (short)PE.DbEntity.Enums.RollSetStatus.NotAvailable))
      {
        this.CassetteId = 0;
      }

      else
      {
        this.CassetteId = rs.CassetteId;
      }


      //this.CassetteName = (rs.CassetteName != null) ? CassetteName: "";
      this.CassetteName = rs.CassetteName;
      this.CassetteStatus = (rs.CassetteStatus);
      this.GrooveTemplateId = 1;

      UnitConverterHelper.ConvertToLocal<VM_RollSetOverviewFull>(this);
    }

    #endregion
  }
}
