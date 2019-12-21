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
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IRollSetManagementService
  {
    DataSourceResult GetRollSetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    DataSourceResult GetScheduledRollSetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    Task<VM_Base> InsertRollSet(ModelStateDictionary modelState, VM_RollSetOverviewFull viewModel);
    VM_RollSetOverviewFull GetRollSet(ModelStateDictionary modelState, long id);
    Task<VM_Base> AssembleRollSet(ModelStateDictionary modelState, VM_RollSetOverviewFull viewModel);
    Task<VM_Base> UpdateRollSetStatus(ModelStateDictionary modelState, VM_RollSetOverviewFull viewModel);
    Task<VM_Base> ConfirmRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel);
    Task<VM_Base> CancelRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel);
    Task<VM_Base> DisassembleRollSet(ModelStateDictionary modelState, VM_RollSetOverviewFull viewModel);
    Task<VM_Base> DeleteRollSet(ModelStateDictionary modelState, VM_LongId viewModel);
    VM_RollsetDisplay GetRollSetDisplay(ModelStateDictionary modelState, long id);

  }
}
