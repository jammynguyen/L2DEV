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
using PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class RollsetToCassetteController : BaseController
  {
    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      //ViewBag.CassStatus = ListValuesHelper.GetCassetteStatus();
      //ViewBag.CassReadyNew = ListValuesHelper.GetCassetteReadyNew();
      ViewBag.Arrang = ListValuesHelper.GetCassetteArrangement();
      ViewBag.RollTypes = ListValuesHelper.GetRollWithTypes();
      ViewBag.RSetHistoryStatus = ListValuesHelper.GetRollSetHistoryStatus();
      ViewBag.RSetStatus = ListValuesHelper.GetRollSetStatus();
      ViewBag.CassReadyNew = ListValuesHelper.GetCassetteReadyNewWithInitValue();
      ViewBag.CassStatus = ListValuesHelper.GetCassetteStatus();
      ViewBag.CassType = ListValuesHelper.GetCassetteType();
      //   ViewBag.InterCassType
      ViewBag.AvailableRollsets = ListValuesHelper.GetAvailableRollsets();
      ViewBag.CassetteType = ListValuesHelper.GetCassetteType();
      ViewBag.CassetteStatus = ListValuesHelper.GetCassetteStatus();
      ViewBag.NumberOfRolls = ListValuesHelper.GetNumberOfActiveRoll();
      base.OnActionExecuting(ctx);
    }
    #region services

    IRollsetToCassetteService _rollsetToCassetteService;

    #endregion

    #region ctor
    public RollsetToCassetteController(IRollsetToCassetteService service)
    {
      _rollsetToCassetteService = service;
    }
    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/RollSetToCassette/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> AssembleCassetteAndRollsetDialog(long id)
    {
      ViewBag.AvailableRollSetsForGivenCassette = _rollsetToCassetteService.GetCassetteRSWithRollsList(id);

      return await PreparePopupActionResultFromVm(() => _rollsetToCassetteService.GetCassetteOverviewWithPositions(ModelState, id), "~/Views/Module/PE.Lite/RollSetToCassette/_RollSetToCassetteAssemblePopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> CassetteInfoPopupDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _rollsetToCassetteService.GetCassette(ModelState, id), "~/Views/Module/PE.Lite/RollSetToCassette/_RollSetToCassetteInfoCassettePopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> RollSetInfoPopupDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _rollsetToCassetteService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/RollSetToCassette/_RollSetToCassetteInfoRollSetPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult _DetailsCassette(long id)
    {
      return PartialView("~/Views/Module/PE.Lite/RollSetToCassette/_RollSetToCassetteDetailsCassette.cshtml", _rollsetToCassetteService.GetCassette(ModelState, id));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult _DetailsRollSet(long id)
    {
      return PartialView("~/Views/Module/PE.Lite/RollSetToCassette/_RollSetToCassetteDetailsRollSet.cshtml", _rollsetToCassetteService.GetRollSet(ModelState, id));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetAvailableCassettesData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _rollsetToCassetteService.GetAvailableCassettesList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetAvailableInterCassettesData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _rollsetToCassetteService.GetAvailableInterCassettesList(ModelState, request));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetAvailableRollSetData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _rollsetToCassetteService.GetAvailableRollSetList(ModelState, request));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetScheduledRollSetData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _rollsetToCassetteService.GetScheduledRollSetList(ModelState, request));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetReadyRollSetData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _rollsetToCassetteService.GetReadyRollSetList(ModelState, request));
    }
    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public JsonResult GetCassetteRSWith3RollsList([DataSourceRequest] DataSourceRequest request, long CassetteType, short RollsetType)
    {
      return Json(RollsetToCassetteService.GetCassetteRSWith3RollsList(CassetteType), JsonRequestBehavior.AllowGet);
    }


    #region Actions
    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> ConfirmRsReadyForMounting(VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetToCassetteService.ConfirmRsReadyForMounting(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> AssembleRollSetAndCassette(VM_CassetteOverviewWithPositions viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetToCassetteService.AssembleRollSetAndCassette(ModelState, viewModel));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> UnloadRollset(VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetToCassetteService.UnloadRollSet(ModelState, viewModel));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> CancelPlan(VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetToCassetteService.CancelPlan(ModelState, viewModel));
    }
    #endregion
  }
}
