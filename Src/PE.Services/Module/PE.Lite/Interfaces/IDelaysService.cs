using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Delay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IDelaysService
  {
    VM_DelayCatalogue GetDelayCatalogue(ModelStateDictionary modelState, long id);
    DataSourceResult GetDelayCatalogueList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    Task<VM_Base> AddDelayCatalogueAsync(ModelStateDictionary modelState, VM_DelayCatalogue delayCatalogue);
    Task<VM_Base> UpdateDelayCatalogueAsync(ModelStateDictionary modelState, VM_DelayCatalogue delayCatalogue);
    Task<VM_Base> DeleteDelayCatalogueAsync(ModelStateDictionary modelState, VM_DelayCatalogue delayCatalogue);
    IList<VM_DelayCatalogueCategory> GetDelayCategories();

    IList<VM_DelayCatalogue> GetDelayCataloguesForParentSelector();

    VM_Delay GetDelay(ModelStateDictionary modelState, long id);
    DataSourceResult GetDelayList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    Task<VM_Base> UpdateDelayAsync(ModelStateDictionary modelState, VM_Delay delay);
    IList<VM_DelayCatalogue> GetDelayCatalogue();
    Task<VM_Base> DivideDelayAsync(ModelStateDictionary modelState, VM_Delay delay);
  }
}
