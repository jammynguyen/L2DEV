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

namespace PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration
{
  public class VM_RollSetOverviewRollChange : VM_Base
  {
    #region properties
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollSetName", "NAME_RollSetName")]
    public virtual long Id { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollSetStatus", "NAME_RollsetStatus")]
    public virtual short RollSetStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollSetStatusNew", "NAME_RollsetStatusNew")]
    public virtual short RollSetStatusNew { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollSetType", "NAME_RollsetType")]
    public virtual short RollSetType { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollSetName", "NAME_RollSetName")]
    public virtual string RollSetName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "UpperRollId", "NAME_RollUpperName")]
    public virtual long? UpperRollId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "DiameterUpper", "NAME_DiameterUpper")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    public virtual double? UpperDiameter { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollNameUpper", "NAME_RollUpperName")]
    public virtual string RollNameUpper { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollTypeUpper", "NAME_RollTypeUpper")]
    public virtual string RollTypeUpper { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollTypeIdUpper", "NAME_RollTypeUpper")]
    public virtual long? RollTypeIdUpper { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollNameBottomId", "NAME_RollBottomName")]
    public virtual long? BottomRollId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "DiameterBottom", "NAME_DiameterBottom")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    public virtual double? BottomDiameter { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollNameBottom", "NAME_RollBottomName")]
    public virtual string RollNameBottom { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollTypeBottom", "NAME_RollTypeBottom")]
    public virtual string RollTypeBottom { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollTypeIdBottom", "NAME_RollTypeBottom")]
    public virtual long? RollTypeIdBottom { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollNameThirdId", "NAME_RollThirdName")]
    public virtual long? ThirdRollId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "DiameterThird", "NAME_DiameterThird")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    public virtual double? ThirdDiameter { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollNameThird", "NAME_RollThirdName")]
    public virtual string RollNameThird { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollTypeThird", "NAME_RollTypeThird")]
    public virtual string RollTypeThird { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollTypeIdThird", "NAME_RollTypeThird")]
    public virtual long? RollTypeIdThird { get; set; }


    public virtual long? RollSetHistoryId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollSetHistoryStatus", "NAME_RollsetHistoryStatus")]
    public virtual short RollSetHistoryStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "CassetteName", "NAME_CassetteName")]
    public virtual string CassetteName { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "PositionInCassette", "NAME_PositionInCassette")]
    public virtual short? PositionInCassette { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "StandNo", "NAME_StandNo")]
    public virtual short? StandNo { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "Mounted", "NAME_Mounted")]
    public virtual DateTime? Mounted { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "Dismounted", "NAME_Dismounted")]
    public virtual DateTime? Dismounted { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollStatusName", "NAME_RollsetStatus")]
    public virtual string RollStatusName { get; set; }

    //public virtual short? CassetteStatusId { get; set; }

    //public virtual string CassetteStatusName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "CassetteId", "NAME_CassetteName")]
    //[Editable(true)] 
    public virtual long? CassetteId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "RollSetStatusActual", "NAME_RollSetStatusActual")]
    public virtual string RollSetStatusActual { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "CassetteStatus", "NAME_Status")]
    public virtual short CassetteStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "EstRollChangeTime", "NAME_EstRollChangeTime")]
    public virtual DateTime? EstRollChangeTime { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewRollChange), "StandStatus", "NAME_StandStatus")]
    public virtual short? InstalledInStandStatus { get; set; }

    #endregion
    #region ctor
    public VM_RollSetOverviewRollChange()
    {
    }

    public VM_RollSetOverviewRollChange(short position)
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
      this.CassetteName = "";
      this.PositionInCassette = position;
      this.StandNo = null;
      this.Mounted = null;
      this.Dismounted = null;
      this.RollSetStatusActual = "";
      this.CassetteStatus = 0;
      this.RollTypeIdUpper = null;
      this.RollTypeIdBottom = null;
      this.EstRollChangeTime = null;
      this.InstalledInStandStatus = null;

      UnitConverterHelper.ConvertToLocal<VM_RollSetOverviewRollChange>(this);

    }

    public VM_RollSetOverviewRollChange(V_RollsetOverviewNewest rs, short? standStatus)
    {
      this.Id = Convert.ToInt64(rs.RollSetId);
      this.RollSetStatus = (short)rs.RollSetStatus;
      this.RollSetStatusNew = (short)rs.RollSetStatus;
      this.RollSetType = Convert.ToInt16(rs.RollSetType);
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
      this.RollSetHistoryStatus = Convert.ToInt16(rs.RollSetHistoryStatus);
      this.CassetteName = rs.CassetteName;
      this.PositionInCassette = rs.PositionInCassette;
      this.StandNo = rs.StandNo;
      this.Mounted = rs.Mounted;
      this.Dismounted = rs.Dismounted;
      this.RollSetStatusActual = "";
      this.RollTypeIdUpper = rs.RollTypeIdUpper;
      this.RollTypeIdBottom = rs.RollTypeIdBottom;
      this.InstalledInStandStatus = standStatus;
      this.EstRollChangeTime = null;

      //condition used for proper ViewBag.CassReadyNew with CassetteId selection when rs.CassetteId is null
      if ((rs.RollSetStatus == (short)DbEntity.Enums.RollSetStatus.Ready) || (rs.RollSetStatus == (short)DbEntity.Enums.RollSetStatus.NotAvailable))
      {
        this.CassetteId = 0;
      }
      else
      {
        this.CassetteId = rs.CassetteId;
      }
      //this.CassetteName = (rs.CassetteName != null) ? CassetteName: "";
      this.CassetteName = rs.CassetteName;
      if(rs.CassetteStatus!=null) this.CassetteStatus = (short)(rs.CassetteStatus);
      UnitConverterHelper.ConvertToLocal<VM_RollSetOverviewRollChange>(this);
    }
    #endregion
  }

}
