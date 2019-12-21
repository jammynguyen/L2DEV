using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette
{
  public class VM_RollSetShort : VM_Base
  {
    #region properties
    [SmfDisplayAttribute(typeof(VM_RollSetShort), "RollSetName", "NAME_RollSetName")]
    public virtual long? Id { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetShort), "RollSetStatus", "NAME_RollsetStatus")]
    public virtual RollSetStatus? RollSetStatus { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetShort), "RollSetName", "NAME_RollSetName")]
    public virtual string RollSetName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetShort), "RollSetType", "NAME_RollSetType")]
    public virtual short RollSetType { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetShort), "RollType", "NAME_RollTypeBottom")]
    public virtual string RollType { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_RollSetShort), "PositionInCassette", "NAME_PositionInCassette")]
    public virtual short? PositionInCassette { get; set; }

    public virtual string RollsetCombinedInfo { get; set; }
    #endregion
    #region ctor
    public VM_RollSetShort()
    {
    }

    public VM_RollSetShort(short position)
    {
      this.Id = null;
      this.RollSetStatus = DbEntity.Enums.RollSetStatus.Undefined;
      this.RollSetName = "";
      this.RollType = "";
      this.PositionInCassette = null;
      this.RollsetCombinedInfo = "";
      UnitConverterHelper.ConvertToLocal<VM_RollSetShort>(this);
    }

    public VM_RollSetShort(V_RollsetOverviewNewest rs)
    {
      this.Id = rs.RollSetId;
      this.RollSetStatus = (RollSetStatus)rs.RollSetStatus;
      this.RollSetName = rs.RollSetName;
      this.RollSetType = rs.RollSetType;

      this.RollType = rs.RollTypeUpper;
      this.PositionInCassette = rs.PositionInCassette;
      this.RollsetCombinedInfo = "";
      UnitConverterHelper.ConvertToLocal<VM_RollSetShort>(this);
    }
    #endregion
  }
}
