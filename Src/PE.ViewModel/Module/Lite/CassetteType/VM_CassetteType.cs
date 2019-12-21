using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.CassetteType
{
  public class VM_CassetteType : VM_Base
  {
    #region properties
    public virtual long? Id { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteType), "TypeName", "NAME_Name")]
    public virtual String CassetteTypeName { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteType), "CreatedTs", "NAME_DateTimeCreated")]
    public virtual DateTime CreatedTs { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteType), "LastUpdateTs", "NAME_DateTimeLastUpdate")]
    public virtual DateTime LastUpdateTs { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteType), "Description", "NAME_Description")]
    public virtual String Description { get; set; }

    [SmfDisplayAttribute(typeof(VM_CassetteType), "NumberOfRolls", "NAME_NumberOfRolls")]
    public virtual short? NumberOfRolls { get; set; }

    #endregion
    #region ctor
    public VM_CassetteType()
    {
    }
    public VM_CassetteType(RLSCassetteType cassetType)
    {
      this.Id = cassetType.CassetteTypeId;
      this.CassetteTypeName = cassetType.CassetteTypeName;
      this.Description = cassetType.CassetteTypeDescription;
      this.NumberOfRolls = Convert.ToInt16(cassetType.NumberOfRolls);

      UnitConverterHelper.ConvertToLocal<VM_CassetteType>(this);
    }
    #endregion
  }
}
