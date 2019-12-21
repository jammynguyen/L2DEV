using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialList : VM_Base
  {
    #region prop
    public long Sorting { get; set; }
    public long Id { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialOverview), "MaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialOverview), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime CreatedTs { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialOverview), "LastUpdateTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime LastUpdateTs { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialOverview), "MaterialStatus", "NAME_MaterialStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short Status { get; set; }
    //[SmfDisplay(typeof(VM_RawMaterialOverview), "Status", "NAME_Status")]
    //[DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    //public string StatusName { get; set; }
    #endregion

    #region ctor
    public VM_RawMaterialList(V_RawMaterialList data)
    {
      Sorting = data.Sorting;
      Id = data.RawMaterialId;
      MaterialName = data.MaterialName;
      CreatedTs = data.CreatedTs;
      LastUpdateTs = data.LastUpdateTs;
      Status = data.Status;
      //StatusName = data.StatusName;
    }
    #endregion
  }
}
