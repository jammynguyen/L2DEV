using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration
{
  public class VM_SwappingCassettes : VM_Base
  {
    #region properties
    public virtual long? Id { get; set; }

    [SmfDisplayAttribute(typeof(VM_SwappingCassettes), "StandNo", "NAME_StandNo")]
    public virtual short StandNo { get; set; }

    [SmfDisplayAttribute(typeof(VM_SwappingCassettes), "CassetteId", "NAME_CassetteName")]
    public virtual long? CassetteId { get; set; }

    [SmfDisplayAttribute(typeof(VM_SwappingCassettes), "CassetteName", "NAME_CassetteName")]
    public virtual string CassetteName { get; set; }

    [SmfDisplayAttribute(typeof(VM_SwappingCassettes), "CassetteType", "NAME_Name")]
    public virtual long? CassetteType { get; set; }

    [SmfDisplayAttribute(typeof(VM_SwappingCassettes), "StandStatus", "NAME_Status")]
    public virtual short? StandStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_SwappingCassettes), "Arrangement", "NAME_Arrangement")]
    public virtual short? Arrangement { get; set; }


    [SmfDisplayAttribute(typeof(VM_SwappingCassettes), "NumberOfPositions", "NAME_NumberOfPosition")]
    //[SmfRangeAttribute(typeof(VM_SwappingCassettes), "Plane", "RANGE_MIN_CassettePositions", "RANGE_MAX_CassettePositions")]
    [SmfFormatAttribute("FORMAT_CassettePositions")]
    public virtual short NumberOfPositions { get; set; }

    public virtual List<VM_RollSetShort> RollSetss { get; set; }

    [SmfDisplayAttribute(typeof(VM_SwappingCassettes), "StandStatusNew", "NAME_StatusNew")]
    public virtual short? StandStatusNew { get; set; }

    [SmfDisplayAttribute(typeof(VM_SwappingCassettes), "ArrangementNew", "NAME_ArrangementNew")]
    public virtual short? ArrangementNew { get; set; }

    [SmfDisplayAttribute(typeof(VM_SwappingCassettes), "NewCassetteId", "NAME_CassetteName")]
    public virtual long? NewCassetteId { get; set; }

    [SmfDisplayAttribute(typeof(VM_SwappingCassettes), "NewCassetteType", "NAME_Name")]
    public virtual long? NewCassetteType { get; set; }

    [SmfDisplayAttribute(typeof(V_RollsetOverviewNewest), "RollSetName", "NAME_Name")]
    public virtual string RollSetName { get; set; }

    #endregion
    #region ctor
    public VM_SwappingCassettes()
    {
    }
    public VM_SwappingCassettes(RLSCassette asc, IList<V_RollsetOverviewNewest> rollsets)
    {
      this.Id = asc.RLSStand.StandId;
      this.StandNo = asc.RLSStand.StandNo;
      this.CassetteId = asc.CassetteId;
      this.CassetteName = asc.CassetteName;
      this.CassetteType = asc.FKCassetteTypeId;
      this.StandStatus = (short?)asc.RLSStand.Status;
      if (this.StandStatus == null)
        this.StandStatus = 0;
      this.Arrangement = (short?)asc.Arrangement;
      if (this.Arrangement == null)
        this.Arrangement = 0;
      this.StandStatusNew = 0;
      this.ArrangementNew = 0;
      //this.RollSetStatus = null;
      this.NumberOfPositions = asc.NumberOfPositions;


      bool rollsetFound = false;
      this.RollSetss = new List<VM_RollSetShort>();
      for (int i = 1; i <= this.NumberOfPositions; i++)
      {
        rollsetFound = false;
        foreach (V_RollsetOverviewNewest v in rollsets)
        {
          if (v.PositionInCassette == i)
          {
            rollsetFound = true;
            this.RollSetss.Add(new VM_RollSetShort(v));
            break;
          }
        }
        if (!rollsetFound)
        {
          //add empty row
          VM_RollSetShort rov = new VM_RollSetShort((short)i);
          this.RollSetss.Add(rov);
        }
      }

    }
    #endregion
  }
}
