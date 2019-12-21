using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Delay;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class DelayCatalogueController : BaseController
  {
    private readonly IDelaysService _service;

    public DelayCatalogueController(IDelaysService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Controller_Delays, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/DelaysCatalogue/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Controller_Delays, RightLevel.View)]
    public async Task<ActionResult> GetDelayCatalogueList([DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = _service.GetDelayCatalogueList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Controller_Delays, RightLevel.Update)]
    public async Task<ActionResult> UpdateDelayCatalogueAsync(VM_DelayCatalogue delayCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.UpdateDelayCatalogueAsync(ModelState, delayCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Controller_Delays, RightLevel.Update)]
    public async Task<ActionResult> DelayCatalogueEditPopup(long delayCatalogueId)
    {
      ViewBag.ParentDelay = _service.GetDelayCataloguesForParentSelector();
      ViewBag.CatalogueCategory = _service.GetDelayCategories();
      return await PreparePopupActionResultFromVm(() => _service.GetDelayCatalogue(ModelState, delayCatalogueId), "~/Views/Module/PE.Lite/DelaysCatalogue/_DelayCatalogueEditPopup.cshtml");
    }
  }
}
