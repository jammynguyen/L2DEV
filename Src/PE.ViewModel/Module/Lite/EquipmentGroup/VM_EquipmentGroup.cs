using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.Resources;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_EquipmentGroup : VM_Base
  {
    public long EquipmentGroupId { get; set; }

    [SmfDisplay(typeof(VM_EquipmentGroup), "EquipmentGroupCode", "NAME_Code")]
    [StringLength(5, MinimumLength = 3, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string EquipmentGroupCode { get; set; }

    [SmfDisplay(typeof(VM_EquipmentGroup), "EquipmentGroupName", "NAME_EquipmentGroup")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string EquipmentGroupName { get; set; }

    [SmfDisplay(typeof(VM_EquipmentGroup), "EquipmentGroupDescription", "NAME_Description")]
    public string EquipmentGroupDescription { get; set; }

    [SmfDisplay(typeof(VM_EquipmentGroup), "Created", "NAME_CreatedTs")]
    public DateTime Created { get; set; }


    public VM_EquipmentGroup()
    {
      this.Created = DateTime.Now;
    }

    public VM_EquipmentGroup(MNTEquipmentGroup p)
    {
      this.EquipmentGroupId = p.EquipmentGroupId;
      this.EquipmentGroupCode = p.EquipmentGroupCode;
      this.EquipmentGroupName = p.EquipmentGroupName;
      this.EquipmentGroupDescription = p.EquipmentGroupDescription;
      this.Created = p.CreatedTs;

      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
