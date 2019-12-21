using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.Module.Lite.RollsManagement;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IRollsManagementService
  {
    DataSourceResult GetRollsList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    Task<VM_Base> InsertRoll(ModelStateDictionary modelState, VM_RollsWithTypes viewModel);
    VM_RollsWithTypes GetRoll(ModelStateDictionary modelState, long id);
    Task<VM_Base> UpdateRoll(ModelStateDictionary modelState, VM_RollsWithTypes viewModel);
    Task<VM_Base> ScrapRoll(ModelStateDictionary modelState, VM_RollsWithTypes viewModel);
    Task<VM_Base> DeleteRoll(ModelStateDictionary modelState, VM_LongId viewModel);
    VM_RollsetDisplay GetRollSetDisplay(ModelStateDictionary modelState, long id);
  }
}
