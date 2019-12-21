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
  public class VM_StandGrid : VM_Base
  {
    #region properties
    public long? CassetteId { get; set; }
    public long? StandId { get; set; }
    public short? Position { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandGrid), "StandStatus", "NAME_StandStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short StandStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandGrid), "CassetteName", "NAME_CassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string CassetteName { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandGrid), "CassetteTypeName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String CassetteTypeName { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandGrid), "StandNo", "NAME_StandNo")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short? StandNo { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandGrid), "StandZoneName", "NAME_StandZoneName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string StandZoneName { get; set; }

    [SmfDisplayAttribute(typeof(VM_StandGrid), "ArrangementString", "NAME_Arrangement")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short? Arrangement { get; set; }

    #endregion

    #region ctor
    public VM_StandGrid()
    {

    }

    public VM_StandGrid(V_RSCassettesInStands entity)
    {
      this.CassetteId = entity.CassetteId;
      this.StandId = entity.StandId;
      this.CassetteName = entity.CassetteName;
      this.CassetteTypeName = entity.CassetteTypeName;
      this.StandNo = entity.StandNo;
      this.StandZoneName = entity.StandZoneName;
      this.Position = entity.Position;
      this.StandStatus = entity.Status;
      this.Arrangement = entity.Arrangement;

      UnitConverterHelper.ConvertToLocal<VM_StandGrid>(this);
    }
    #endregion
  }
}
