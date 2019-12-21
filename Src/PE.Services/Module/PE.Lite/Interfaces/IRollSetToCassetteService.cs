using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Cassette;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IRollsetToCassetteService
  {
    DataSourceResult GetAvailableCassettesList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    DataSourceResult GetAvailableInterCassettesList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request); // List for new view with InterCassette
    DataSourceResult GetAvailableRollSetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    DataSourceResult GetScheduledRollSetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    DataSourceResult GetReadyRollSetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    Task<VM_Base> ConfirmRsReadyForMounting(ModelStateDictionary modelState, VM_LongId viewModel);
    Task<VM_Base> AssembleRollSetAndCassette(ModelStateDictionary modelState, VM_CassetteOverviewWithPositions viewModel);
    VM_CassetteOverviewWithPositions GetCassetteOverviewWithPositions(ModelStateDictionary modelState, long id);
    Task<VM_Base> UnloadRollSet(ModelStateDictionary modelState, VM_LongId viewModel);
    Task<VM_Base> CancelPlan(ModelStateDictionary modelState, VM_LongId viewModel);
    VM_RollsetDisplay GetRollSetDisplay(ModelStateDictionary modelState, long id);
    VM_CassetteOverview GetCassette(ModelStateDictionary modelState, long id);
    VM_RollSetOverview GetRollSet(ModelStateDictionary modelState, long id);

    SelectList GetCassetteRSWithRollsList(long cassetteType);

  }
}
