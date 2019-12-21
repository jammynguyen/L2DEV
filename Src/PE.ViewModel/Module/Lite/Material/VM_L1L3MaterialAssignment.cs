using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_L1L3MaterialAssignment : VM_Base
  {
    #region props
    [SmfDisplayAttribute(typeof(VM_L1L3MaterialAssignment), "MaterialId", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? MaterialId { get; set; }

    [SmfDisplayAttribute(typeof(VM_L1L3MaterialAssignment), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplayAttribute(typeof(VM_L1L3MaterialAssignment), "LastUpdateTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplayAttribute(typeof(VM_L1L3MaterialAssignment), "IsDummy", "NAME_IsDummyMaterial")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplayAttribute(typeof(VM_L1L3MaterialAssignment), "MaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }

    [SmfDisplayAttribute(typeof(VM_L1L3MaterialAssignment), "FKHeatId", "NAME_FKHeatId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long FKHeatId { get; set; }

    [SmfDisplayAttribute(typeof(VM_L1L3MaterialAssignment), "FKWorkOrderId", "NAME_FKWorkOrderId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKWorkOrderId { get; set; }

    [SmfDisplayAttribute(typeof(VM_L1L3MaterialAssignment), "StatusName", "NAME_Status")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string StatusName { get; set; }
    #endregion

    #region ctor
    public VM_L1L3MaterialAssignment(V_L1L3MaterialAssignment data)
    {
      MaterialId = data.MaterialId;
      CreatedTs = data.CreatedTs;
      LastUpdateTs = data.LastUpdateTs;
      IsDummy = data.IsDummy;
      MaterialName = data.MaterialName;
      FKHeatId = data.FKHeatId;
      FKWorkOrderId = data.FKWorkOrderId;
      StatusName = data.StatusName;
    }
    #endregion
  }
}
