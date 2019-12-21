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

namespace PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette
{
  public class VM_CassetteOverviewWithPositions : VM_Base
  {
    #region properties
    public virtual long? Id { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverviewWithPositions), "CassetteName", "NAME_CassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string CassetteName { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverviewWithPositions), "Status", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? Status { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverviewWithPositions), "FkCassetteType", "NAME_Type")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? FkCassetteType { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverviewWithPositions), "TypeName", "NAME_Type")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string TypeName { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverviewWithPositions), "NumberOfPositions", "NAME_NumberOfPosition")]
    //[SmfRangeAttribute(typeof(VM_CassetteOverviewWithPositions), "Plane", "RANGE_MIN_CassettePositions", "RANGE_MAX_CassettePositions")]
    [SmfFormatAttribute("FORMAT_CassettePositions")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short NumberOfPositions { get; set; }

    public virtual List<VM_RollSetShort> RollSetss { get; set; }

    //for cassette mounting purposes
    public virtual long? StandId { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverviewWithPositions), "Arrangement", "NAME_Arrangement")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? Arrangement { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverviewWithPositions), "StandStatus", "NAME_StandStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? StandStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteOverviewWithPositions), "NumberOfRolls", "NAME_NumberOfRolls")]
    public virtual short? NumberOfRolls { get; set; }

    #endregion
    #region ctor
    public VM_CassetteOverviewWithPositions()
    {
    }
    public VM_CassetteOverviewWithPositions(V_CassettesOverview c, IList<V_RollsetOverviewNewest> rollsets)
    {
      this.Id = c.CassetteId;
      this.CassetteName = c.CassetteName;
      this.Status = (short)c.Status;
      this.FkCassetteType = c.FKCassetteTypeId;
      this.TypeName = c.CassetteTypeName;
      this.NumberOfPositions = c.NumberOfPositions;
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
      this.StandStatus = 0;
      this.Arrangement = 0;
      this.StandId = c.StandId;
      this.NumberOfRolls = Convert.ToInt16(c.NumberOfRolls);

      UnitConverterHelper.ConvertToLocal<VM_CassetteOverviewWithPositions>(this);

    }
    #endregion
  }
}
