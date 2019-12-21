using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration
{
  public class VM_RollChangeOperation : VM_Base
  {
    #region properties
    public virtual long? Id { get; set; } //StandId

    [SmfDisplayAttribute(typeof(VM_RollChangeOperation), "StandNo", "NAME_StandNo")]
    public virtual short StandNo { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollChangeOperation), "CassetteId", "NAME_CassetteName")]
    public virtual long? CassetteId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollChangeOperation), "CassetteName", "NAME_CassetteName")]
    public virtual string CassetteName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollChangeOperation), "CassetteType", "NAME_Name")]
    public virtual long? CassetteType { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollChangeOperation), "StandStatus", "NAME_Status")]
    public virtual short? StandStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollChangeOperation), "Arrangement", "NAME_Arrangement")]
    public virtual short? Arrangement { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverview), "PositionInCassette", "NAME_PositionInCassette")]
    public virtual short? PositionInCassette { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollChangeOperation), "RollsetToBeDismountedId", "NAME_Rollset4Dismounting")]
    public virtual long? RollsetToBeDismountedId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollChangeOperation), "RollsetToBeDismounted", "NAME_Rollset4Dismounting")]
    public virtual string RollsetToBeDismounted { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollChangeOperation), "Mounted", "NAME_Mounted")]
    public virtual DateTime? Mounted { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollChangeOperation), "RollsetToBeMountedId", "NAME_Rollset4Mounting")]
    public virtual long? RollsetToBeMountedId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollChangeOperation), "RollsetToBeMounted", "NAME_Rollset4Mounting")]
    public virtual string RollsetToBeMounted { get; set; }

    public virtual short? rcAction { get; set; }


    //[SmfDisplayAttribute(typeof(VM_RollChangeOperation), "RollSetName", "NAME_RollSetName")]
    //public virtual string RollSetName { get; set; }



    #endregion
    #region ctor
    public VM_RollChangeOperation()
    {
    }
    public VM_RollChangeOperation(RLSCassette asc, short? position, V_RollsetOverviewNewest rsToBeDismounted, V_RollsetOverviewNewest rsToBeMounted, RollChangeAction rcAction)
    {
      this.Id = asc.RLSStand.StandId;
      this.StandNo = asc.RLSStand.StandNo;
      this.CassetteId = asc.CassetteId;
      this.CassetteName = asc.CassetteName;
      this.CassetteType = asc.RLSCassetteType.Type;
      this.StandStatus = (short?)asc.RLSStand.Status;
      if (this.StandStatus == null)
        this.StandStatus = 0;
      this.Arrangement = (short?)asc.Arrangement;
      if (this.Arrangement == null)
        this.Arrangement = 0;
      this.PositionInCassette = position;
      if (rsToBeMounted != null)
      {
        this.RollsetToBeMountedId = rsToBeMounted.RollSetId;
        //  this.RollsetToBeMounted = rsToBeM

        // old version
        this.RollsetToBeMounted = rsToBeDismounted.RollSetName;
      }
      else
      {
        this.RollsetToBeMountedId = -1;
      }

      if (rsToBeDismounted != null)
      {
        this.RollsetToBeDismountedId = rsToBeDismounted.RollSetId;
        this.Mounted = rsToBeDismounted.Mounted;
        this.RollsetToBeDismounted = rsToBeDismounted.RollSetName;

      }
      else
        this.RollsetToBeDismountedId = -1;

      this.rcAction = (short)rcAction;

      //this.RollSetName = asc.


      UnitConverterHelper.ConvertToLocal<VM_RollChangeOperation>(this);
    }
    #endregion
  }
}
