using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.Module.Lite.RollsManagement;
using PE.HMIWWW.ViewModel.System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module
{
  public class RollsManagementController : BaseController
  {
    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.RollTypes = ListValuesHelper.GetRollWithTypes();
      ViewBag.RollStatus = ListValuesHelper.GetRollStatus();
      //ViewBag.RollStatusNoUndef = ListValuesHelper.GetRollStatusWithoutUndef();
      ViewBag.RollScrapReasons = ListValuesHelper.GetRollScrapReasons();
      //ViewBag.GrooveListShorts = ListValuesHelper.GetGrooveTemplatesShortList();

      base.OnActionExecuting(ctx);
    }
    #region services

    IRollsManagementService _rollsManagementService;

    #endregion

    #region ctor
    public RollsManagementController(IRollsManagementService service)
    {
      _rollsManagementService = service;
    }
    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/RollsManagement/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddRollDialog()
    {
      return PartialView("~/Views/Module/PE.Lite/RollsManagement/_RollsManagementAddPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> EditRollDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _rollsManagementService.GetRoll(ModelState, id), "~/Views/Module/PE.Lite/RollsManagement/_RollsManagementEditPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> RollSetDetails(long id)
    {
      return await PreparePopupActionResultFromVm(() => _rollsManagementService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/RollsManagement/_RollsManagementRollDetails.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> ScrapRollDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _rollsManagementService.GetRoll(ModelState, id), "~/Views/Module/PE.Lite/RollsManagement/_RollsManagementScrapPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Details(long id)
    {
      return PartialView("_Details", _rollsManagementService.GetRoll(ModelState, id));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetRollData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _rollsManagementService.GetRollsList(ModelState, request));
    }

    #region Actions
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> InsertRoll(VM_RollsWithTypes viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsManagementService.InsertRoll(ModelState, viewModel));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> DeleteRoll(VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsManagementService.DeleteRoll(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> UpdateRoll(VM_RollsWithTypes viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsManagementService.UpdateRoll(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> ScrapRoll(VM_RollsWithTypes viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsManagementService.ScrapRoll(ModelState, viewModel));
    }
    #endregion
  }
}
