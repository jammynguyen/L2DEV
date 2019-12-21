using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IEquipmentGroupsService
  {
    VM_EquipmentGroup GetEquipmentGroup(long id);
    VM_EquipmentGroup GetEmptyEquipmentGroup();
    DataSourceResult GetEquipmentGroupsList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    Task<VM_Base> UpdateEquipmentGroup(ModelStateDictionary modelState, VM_EquipmentGroup cat);
    Task<VM_Base> CreateEquipmentGroup(ModelStateDictionary modelState, VM_EquipmentGroup cat);
    Task<VM_Base> DeleteEquipmentGroup(long id);
  }
}
