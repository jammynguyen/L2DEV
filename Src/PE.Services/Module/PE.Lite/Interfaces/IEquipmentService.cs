using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Maintenance;
using PE.HMIWWW.ViewModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IEquipmentService
  {
    VM_Equipment GetEquipment(long id);
    VM_Equipment GetEmptyEquipment();
    DataSourceResult GetEquipmentList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    Task<VM_Base> UpdateEquipment(ModelStateDictionary modelState, VM_Equipment cat);
    Task<VM_Base> CreateEquipment(ModelStateDictionary modelState, VM_Equipment cat);
    Task<VM_Base> CloneEquipment(ModelStateDictionary modelState, VM_Equipment cat);
    Task<VM_Base> DeleteEquipment(long id);
    DataSourceResult GetEquipmentHistoryList(long id, ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    IList<VM_EquipmentGroup> GetEquipmentGroupList();
    VM_LongId GetEquipmentHistory(long id);
  }
}
