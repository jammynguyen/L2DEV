using Kendo.Mvc.UI;
using PE.Core;
using PE.DbEntity.Model;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using PE.HMIWWW.ViewModel.System;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module
{
  public class RollSetManagementController : BaseController
  {

    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.RollSetStatus = ListValuesHelper.GetRollSetStatus();
      ViewBag.RollSetStatusShort = ListValuesHelper.GetRollSetStatusShortList();
      ViewBag.RollSetHistoryStatus = ListValuesHelper.GetRollSetHistoryStatus();
      ViewBag.RollsAvail = ListValuesHelper.GetRollsReadyWithTypes();
      ViewBag.RollsetAvail = ListValuesHelper.GetRollsetEmpty();
      ViewBag.RollsetTypes = ListValuesHelper.GetRollsetTypeList();
      ViewBag.CassStatus = ListValuesHelper.GetCassetteStatus();
      ViewBag.RSHistStatus = ListValuesHelper.GetRollSetHistoryStatus();

      base.OnActionExecuting(ctx);
    }
    #region services

    IRollSetManagementService _rollsetManagementService;

    #endregion

    #region ctor
    public RollSetManagementController(IRollSetManagementService service)
    {
      _rollsetManagementService = service;
    }
    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/RollSetManagement/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddRollSetDialog()
    {
      return PartialView("~/Views/Module/PE.Lite/RollSetManagement/_RollSetManagementAddPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> AssembleRollSetDialog(long id)
    {
      //long rollsetType = param;
      //if (rollsetType == (long)PE.Core.Constants.RollSetType.twoActiveRollsIM || rollsetType == (long)PE.Core.Constants.RollSetType.twoActiveRollsRM)
        return await PreparePopupActionResultFromVm(() => _rollsetManagementService.GetRollSet(ModelState, id), "~/Views/Module/PE.Lite/RollSetManagement/_RollSetManagementAssemblePopup.cshtml");
      //else
      //  return await PreparePopupActionResultFromVm(() => _rollsetManagementService.GetRollSet(ModelState, id), "AssembleExtendedRollSetDialog");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> DisassembleRollSetDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _rollsetManagementService.GetRollSet(ModelState, id), "~/Views/Module/PE.Lite/RollSetManagement/_RollSetManagementDisassemblePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> EditRollSetDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _rollsetManagementService.GetRollSet(ModelState, id), "~/Views/Module/PE.Lite/RollSetManagement/_RollSetManagementEditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Details(long id)
    {
      return PartialView("_Details", _rollsetManagementService.GetRollSet(ModelState, id));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetRollsetData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _rollsetManagementService.GetRollSetList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetScheduledRollsetData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _rollsetManagementService.GetScheduledRollSetList(ModelState, request));
    }

    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public JsonResult GetRolls(string RollSetId)
    {
      return Json(RollSetManagementService.GetRolls(RollSetId), JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public JsonResult GetGrooveTemplates()
    {
      return Json(RollSetManagementService.GetGrooveTemplates(), JsonRequestBehavior.AllowGet);
    }

    public static List<RLSRollSet> GetEmptyRollsetList()
    {
      return RollSetManagementService.GetEmptyRollsetList();
    }

    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public JsonResult GetRollsWithMaterials(string UpperRollId)
    {
      return Json(RollSetManagementService.GetRollsWithMaterials(UpperRollId), JsonRequestBehavior.AllowGet);
    }

    #region Actions
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> InsertRollSet(VM_RollSetOverviewFull viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.InsertRollSet(ModelState, viewModel));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> DeleteRollSet(VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.DeleteRollSet(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> UpdateRollSetStatus(VM_RollSetOverviewFull viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.UpdateRollSetStatus(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> AssembleRollSet(VM_RollSetOverviewFull viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.AssembleRollSet(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> DisassembleRollSet(VM_RollSetOverviewFull viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.DisassembleRollSet(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> ConfirmAssembleRollSet(VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.ConfirmRollSetStatus(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> CancelAssembleRollSet(VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.CancelRollSetStatus(ModelState, viewModel));
    }

    #endregion
  }
}
