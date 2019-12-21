using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.RollChange;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IRollChangeService
  {
    VM_ConfirmationForRmAndIm BuildVMConfirmationForRmAndIm(short? operationType, long? rollsetId, long? mountedRollsetId, short? position, short? standNo);
    DataSourceResult GetStandGridData(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, long firstStand, long lastStand);
    DataSourceResult GetRollsetGridData(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, long standNo);
    DataSourceResult GetRollGroovesGridData(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, long rollSetId, long standNo);
    List<V_RSCassettesInStands> GetCassettesInfoRmIm();
    List<V_RollsetOverviewNewest> GetAvailableRollsets(short standNo);
    V_RollsetOverviewNewest GetRollSetDetails(long rollSetId);
    Task<VM_Base> SwapRollSet(ModelStateDictionary modelState, VM_ConfirmationForRmAndIm viewModel);
    Task<VM_Base> DismountRollset(ModelStateDictionary modelState, VM_ConfirmationForRmAndIm viewModel);
    Task<VM_Base> MountRollset(ModelStateDictionary modelState, VM_ConfirmationForRmAndIm viewModel);
    short GetGrooveNumber(long rollId);
    long? GetRollId(V_RollsetOverviewNewest rollSet);
  }
}
