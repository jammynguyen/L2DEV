using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PE.HMIWWW.Core.HtmlHelpers;
using System.Text;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.HMIWWW.Core.Authorization;
using PE.Core;

namespace PE.HMIWWW.Controllers.System
{
  public class WatchdogController : BaseController
  {
    #region services

    private IWatchdogService _service;

    #endregion
    #region ctor
    public WatchdogController(IWatchdogService service)
    {
      _service = service;
    }

    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Watchdog, Constants.SmfAuthorization_Controller_Watchdog, RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Watchdog, Constants.SmfAuthorization_Controller_Watchdog, RightLevel.Delete)]
    public async Task<JsonResult> Kill(string moduleName)
    {
      return await PrepareJsonResultFromVm(() => _service.Kill(ModelState, moduleName));
    }
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Watchdog, Constants.SmfAuthorization_Controller_Watchdog, RightLevel.Delete)]
    public async Task<JsonResult> Stop(string moduleName)
    {
      return await PrepareJsonResultFromVm(() => _service.Stop(ModelState, moduleName));
    }
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Watchdog, Constants.SmfAuthorization_Controller_Watchdog, RightLevel.Delete)]
    public async Task<JsonResult> Initialize(string moduleName)
    {
      return await PrepareJsonResultFromVm(() => _service.Initialize(ModelState, moduleName));
    }
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Watchdog, Constants.SmfAuthorization_Controller_Watchdog, RightLevel.Delete)]
    public async Task<JsonResult> SetProcessUnderWd(string moduleName)
    {
      return await PrepareJsonResultFromVm(() => _service.SetProcessUnderWd(ModelState, moduleName));
    }
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Watchdog, Constants.SmfAuthorization_Controller_Watchdog, RightLevel.Delete)]
    public async Task<JsonResult> UnSetProcessUnderWd(string moduleName)
    {
      return await PrepareJsonResultFromVm(() => _service.UnSetProcessUnderWd(ModelState, moduleName));
    }

  }
}