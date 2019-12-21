using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.GrindingTurning;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IGrindingTurningService
  {
    DataSourceResult GetPlannedRollsetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    DataSourceResult GetScheduledRollsetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    VM_RollsetDisplay GetRollSetDisplay(ModelStateDictionary modelState, long id);
    VM_RollsetDisplay AddGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVM);
    VM_RollSetTurningHistory GetRollSetHistoryActual(ModelStateDictionary modelState, long id);
    VM_RollSetTurningHistory GetRollSetHistoryById(ModelStateDictionary modelState, long id);
    VM_RollsetDisplay RemoveGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVM);
    //VM_RollsetDisplay AddGroovesToRollSetForKocksDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVM);
    //VM_RollsetDisplay RemoveGroovesToRollSetForKocksDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVM);

    Task<VM_Base> UpdateGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVM);
    //Task<VM_Base> UpdateGroovesToRollSetForKocksDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVM);
    Task<VM_Base> ConfirmRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel);
    Task<VM_Base> CancelRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel);
    Task<VM_Base> ConfirmUpdateGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVM);
    //Task<VM_Base> ConfirmRollSetForKocksStatus(ModelStateDictionary modelState, VM_LongId viewModel);
  }
}
