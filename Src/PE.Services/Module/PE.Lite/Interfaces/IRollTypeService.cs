using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.RollType;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IRollTypeService
  {
    DataSourceResult GetRollTypeList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    Task<VM_Base> InsertRollType(ModelStateDictionary modelState, VM_RollType viewModel);
    VM_RollType GetRollType(ModelStateDictionary modelState, long id);
    Task<VM_Base> UpdateRollType(ModelStateDictionary modelState, VM_RollType viewModel);
    Task<VM_Base> DeleteRollType(ModelStateDictionary modelState, VM_LongId viewModel);
  }
}
