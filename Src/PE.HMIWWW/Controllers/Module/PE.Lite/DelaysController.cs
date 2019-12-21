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
  public class DelaysController : BaseController
  {
    private readonly IDelaysService _service;

    public DelaysController(IDelaysService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Controller_Delays, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Delays/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Controller_Delays, RightLevel.View)]
    public async Task<ActionResult> GetDelayList([DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = _service.GetDelayList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Controller_Delays, RightLevel.Update)]
    public async Task<ActionResult> UpdateDelayAsync(VM_Delay delay)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.UpdateDelayAsync(ModelState, delay));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Controller_Delays, RightLevel.Update)]
    public async Task<ActionResult> DelayEditPopup(long delayId)
    {
      ViewBag.CatalogueList = _service.GetDelayCatalogue();
      return await PreparePopupActionResultFromVm(() => _service.GetDelay(ModelState, delayId), "~/Views/Module/PE.Lite/Delays/_DelayEditPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Controller_Delays, RightLevel.Update)]
    public async Task<ActionResult> DivideDelayAsync(VM_Delay delay)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DivideDelayAsync(ModelState, delay));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Controller_Delays, RightLevel.Update)]
    public async Task<ActionResult> DelayDividePopup(long delayId)
    {
      return await PreparePopupActionResultFromVm(() => _service.GetDelay(ModelState, delayId), "~/Views/Module/PE.Lite/Delays/_DelayDividePopup.cshtml");
    }
  }
}
