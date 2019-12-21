using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IGrooveTemplateService
  {
    DataSourceResult GetGrooveTemplateList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    Task<VM_Base> InsertGrooveTemplate(ModelStateDictionary modelState, VM_GrooveTemplate viewModel);
    VM_GrooveTemplate GetGrooveTemplate(ModelStateDictionary modelState, long id);
    Task<VM_Base> UpdateGrooveTemplate(ModelStateDictionary modelState, VM_GrooveTemplate viewModel);
    Task<VM_Base> DeleteGrooveTemplate(ModelStateDictionary modelState, VM_LongId viewModel);
    //IList<VM_SteelGroup> GetShapeList();
  }
}
