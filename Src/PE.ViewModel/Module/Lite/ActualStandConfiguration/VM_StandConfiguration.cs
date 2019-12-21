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
  public class VM_StandConfiguration : VM_Base
  {
    #region properties
    public virtual long? Id { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "StandNo", "NAME_StandNo")]
    public virtual short StandNo { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "CassetteId", "NAME_CassetteName")]
    public virtual long? CassetteId { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "CassetteName", "NAME_CassetteName")]
    public virtual string CassetteName { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "CassetteType", "NAME_Name")]
    public virtual long? CassetteType { get; set; }

    //[SmfDisplayAttribute(typeof(VM_StandConfiguration), "Mounted", "NAME_Mounted")]
    //public virtual DateTime? Mounted{ get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "StandStatus", "NAME_Status")]
    public virtual short? StandStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "Arrangement", "NAME_Arrangement")]
    public virtual short? Arrangement { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "StandStatusNew", "NAME_StatusNew")]
    public virtual short? StandStatusNew { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "ArrangementNew", "NAME_ArrangementNew")]
    public virtual short? ArrangementNew { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "StandStatusAct", "NAME_StatusAct")]
    public virtual short? StandStatusAct { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "ArrangementAct", "NAME_ArrangementAct")]
    public virtual short? ArrangementAct { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "NumberofRolls", "NAME_NumberOfRolls")]
    public virtual short? NumberOfRolls { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "StandBlockName", "NAME_StandBlockName")]
    public virtual string StandBlockName { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "StandActivity", "NAME_StandActivity")]
    public virtual short? StandActivity { get; set; }

    public virtual bool IsCalibrated { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandConfiguration), "Position", "NAME_Position")]
    public virtual short? Position { get; set; }
    public virtual int? RollsetCnt { get; set; }

    #endregion
    #region ctor
    public VM_StandConfiguration()
    {
    }
    public VM_StandConfiguration(V_ActualStandConfiguration asc)
    {
      this.Id = asc.StandId;
      this.StandNo = asc.StandNo;
      this.CassetteId = asc.CassetteId;
      this.CassetteName = asc.CassetteName;
      this.CassetteType = asc.FKCassetteTypeId;
      //this.Mounted = asc.Mounted;
      this.StandStatus = (short?)asc.Status;
      if (this.StandStatus == null)
        this.StandStatus = 0;
      this.Arrangement = (short?)asc.Arrangement;
      if (this.Arrangement == null)
        this.Arrangement = 0;
      this.StandStatusNew = (short?)asc.Status;
      this.ArrangementNew = (short?)asc.Arrangement;
      this.StandBlockName = asc.StandZoneName;
      this.StandStatusAct = null;
      this.ArrangementAct = null;
      //this.RollSetStatus = null;
      this.NumberOfRolls = Convert.ToInt16(asc.NumberOfRolls);
      this.StandActivity = (short?)(asc.IsOnLine);
      this.IsCalibrated = asc.IsCalibrated;
      this.Position = asc.Position;
      this.RollsetCnt = asc.RollsetsCnt;


      UnitConverterHelper.ConvertToLocal<VM_StandConfiguration>(this);
    }
    //public VM_StandConfiguration(RLSCassette asc)
    //{
    //  this.Id = asc.  asc.Stand.StandId;
    //  this.StandNo = asc.Stand.StandNo;
    //  this.CassetteId = asc.CassetteId;
    //  this.CassetteName = asc.CassetteName;
    //  this.CassetteType = asc.FkCassetteType;
    //  //this.Mounted = asc.Mounted;
    //  this.StandStatus = (short?)asc.Stand.Status;
    //  if (this.StandStatus == null)
    //    this.StandStatus = 0;
    //  this.Arrangement = (short?)asc.Arrangement;
    //  if (this.Arrangement == null)
    //    this.Arrangement = 0;
    //  this.StandStatusNew = (short?)asc.Stand.Status;
    //  this.ArrangementNew = (short?)asc.Arrangement;
    //  this.StandStatusAct = null;
    //  this.ArrangementAct = null;
    //  //this.RollSetStatus = null;
    //  //  this.NumberOfRolls = Convert.ToInt16(asc.);

    //  UnitConverterHelper.ConvertToLocal<VM_StandConfiguration>(this);
    //}
    #endregion
  }
}
