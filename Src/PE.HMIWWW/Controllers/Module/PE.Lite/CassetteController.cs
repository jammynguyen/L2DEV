using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Cassette;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class CassetteController : BaseController
  {
    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.CassStatusShortList = ListValuesHelper.GetCassetteStatusShortList();
      ViewBag.CassStatus = ListValuesHelper.GetCassetteStatus();
      ViewBag.CassArrangement = ListValuesHelper.GetCassetteArrangement();
      ViewBag.CassType = ListValuesHelper.GetCassetteType();
      ViewBag.RsCounter = ListValuesHelper.GetRollSetCounter();
      ViewBag.RSetStatus = ListValuesHelper.GetRollSetStatus();
      ViewBag.NumberOfRolls = ListValuesHelper.GetNumberOfActiveRoll();
      ViewBag.CassetteType = ListValuesHelper.GetCassetteTypeEnum();

      base.OnActionExecuting(ctx);
    }
    #region services

    ICassetteService _cassetteService;

    #endregion

    #region ctor
    public CassetteController(ICassetteService service)
    {
      _cassetteService = service;
    }
    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Cassette/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddCassetteDialog()
    {
      return PartialView("~/Views/Module/PE.Lite/Cassette/_CassetteAddPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> EditCassetteDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _cassetteService.GetCassette(ModelState, id), "~/Views/Module/PE.Lite/Cassette/_CassetteEditPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Details(long id)
    {
      return PartialView("_Details", _cassetteService.GetCassette(ModelState, id));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetCassetteData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _cassetteService.GetCassetteList(ModelState, request));
    }

    #region Actions
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> InsertCassette(VM_CassetteOverview viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _cassetteService.InsertCassette(ModelState, viewModel));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> DeleteCassette(VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _cassetteService.DeleteCassette(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> UpdateCassette(VM_CassetteOverview viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _cassetteService.UpdateCassette(ModelState, viewModel));
    }
    #endregion
  }
}
