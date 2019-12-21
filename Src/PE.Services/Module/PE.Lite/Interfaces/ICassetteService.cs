using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Cassette;
using PE.HMIWWW.ViewModel.Module.Lite.CassetteType;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface ICassetteService
  {
    DataSourceResult GetCassetteList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    Task<VM_Base> InsertCassette(ModelStateDictionary modelState, VM_CassetteOverview viewModel);
    VM_CassetteOverview GetCassette(ModelStateDictionary modelState, long id);
    Task<VM_Base> UpdateCassette(ModelStateDictionary modelState, VM_CassetteOverview viewModel);
    Task<VM_Base> DeleteCassette(ModelStateDictionary modelState, VM_LongId viewModel);
  }
}
