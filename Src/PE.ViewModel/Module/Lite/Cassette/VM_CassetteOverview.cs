using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Cassette
{
  public class VM_CassetteOverview : VM_Base
  {
    #region properties
    public virtual long? Id { get; set; }

    [Required]
    [SmfDisplayAttribute(typeof(VM_CassetteOverview), "CassetteName", "NAME_CassetteName")]
    public virtual string CassetteName { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverview), "Status", "NAME_Status")]
    public virtual CassetteStatus Status { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverview), "StatusTxt", "NAME_Status")]
    public virtual string StatusTxt { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverview), "FkCassetteType", "NAME_Type")]
    public virtual long? FkCassetteType { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverview), "TypeName", "NAME_Type")]
    public virtual string CassetteTypeName { get; set; }

    [Required]
    [SmfDisplayAttribute(typeof(VM_CassetteOverview), "NumberOfPositions", "NAME_NumberOfPosition")]
    //[SmfRangeAttribute(typeof(VM_CassetteOverview), "Plane", "RANGE_MIN_CassettePositions", "RANGE_MAX_CassettePositions")]
    [SmfFormatAttribute("FORMAT_CassettePositions")]
    public virtual short? NumberOfPositions { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverview), "StandNo", "NAME_StandNo")]
    public virtual short? StandNo { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverview), "Arrangement", "NAME_Arrangement")]
    public virtual short? Arrangement { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverview), "NewStatus", "NAME_StatusNew")]
    public virtual short? NewStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverview), "NumberOfRolls", "NAME_NumberOfRolls")]
    public virtual short? NumberOfRolls { get; set; }

    #endregion

    #region ctor

    public VM_CassetteOverview()
    {

    }
    public VM_CassetteOverview(V_CassettesOverview c)
    {
      this.Id = c.CassetteId;
      this.CassetteName = c.CassetteName;
      this.Status = (CassetteStatus)c.Status;
      this.FkCassetteType = c.FKCassetteTypeId;
      this.CassetteTypeName = c.CassetteTypeName;
      this.NumberOfPositions = c.NumberOfPositions;
      this.StandNo = c.StandNo;
      this.Arrangement = (short)c.Arrangement;
      this.NewStatus = (short)c.Status;
      this.NumberOfRolls = Convert.ToInt16(c.NumberOfRolls);
      this.StatusTxt = ResourceController.GetCassetteStatusValue(c.Status.ToString());

      UnitConverterHelper.ConvertToLocal<VM_CassetteOverview>(this);
    }

    #endregion
  }
}
