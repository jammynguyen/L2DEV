using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.System;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using Kendo.Mvc.UI;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class GrindingTurningController : BaseController
  {
    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.RSetStatus = ListValuesHelper.GetRollSetStatus();
      ViewBag.RSetStatusShort = ListValuesHelper.GetRollSetStatusShortList();
      ViewBag.RSetHistoryStatus = ListValuesHelper.GetRollSetHistoryStatus();
      ViewBag.RollsAvail = ListValuesHelper.GetRollsReadyWithTypes();
      ViewBag.RollsetAvail = ListValuesHelper.GetRollsetEmpty();
      ViewBag.GroovesList = ListValuesHelper.GetGrooveTemplatesList();
      ViewBag.GrooveListShorts = ListValuesHelper.GetGrooveTemplatesShortList();
      ViewBag.RollsetTypes = ListValuesHelper.GetRollsetTypeList();

      base.OnActionExecuting(ctx);
    }
    #region services

    IGrindingTurningService _grindingTurningService;

    #endregion

    #region ctor
    public GrindingTurningController(IGrindingTurningService service)
    {
      _grindingTurningService = service;
    }
    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/GrindingTurning/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddCustomerDialog()
    {
      return PartialView("AddCustomerDialog");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> ConfigRollSetDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/GrindingTurning/_GrindingTurningConfigRollSetPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> ConfirmRollSetDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/GrindingTurning/_GrindingTurningConfirmRollSetPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> TurningInfoPopupDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/GrindingTurning/_GrindingTurningInfoPopup.cshtml");
    }
    public async Task<ActionResult> TurningForConfirmInfoPopupDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/GrindingTurning/_GrindingTurningScheduledInfoPopup.cshtml");
    }
    //public async Task<ActionResult> TurningForKocksInfoPopupDialog(long id)
    //{
    //  return await PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetDisplay(ModelState, id), "TurningForKocksInfoPopupDialog");
    //}
    //public async Task<ActionResult> TurningForConfirmForKocksInfoPopupDialog(long id)
    //{
    //  return await PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetDisplay(ModelState, id), "TurningForConfirmForKocksInfoPopupDialog");
    //}

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> RollSetHistoryPopupDialog(long id)
    {
      ViewBag.RSetHistory = ListValuesHelper.GetRollSetHistory(id);
      return await PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetHistoryActual(ModelState, id), "~/Views/Module/PE.Lite/GrindingTurning/_GrindingTurningHistoryRollSetPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    //public async Task<ActionResult> RollSetHistoryForKocksPopupDialog(long id)
    //{
    //  ViewBag.RSetHistory = ListValuesHelper.GetRollSetHistory(id);
    //  return await PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetHistoryActual(ModelState, id), "RollSetHistoryForKocksPopupDialog");
    //}

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<ActionResult> GetRollSetHistoryById(long id)
    {
      ViewBag.RSetHistory = ListValuesHelper.GetRollSetHistoryForId(id);
      return await PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetHistoryById(ModelState, id), "~/Views/Module/PE.Lite/GrindingTurning/_GrindingTurningHistoryRollSetPopup.cshtml");
    }
    //public async Task<ActionResult> GetRollSetHistoryForKocksById(long id)
    //{
    //  ViewBag.RSetHistory = ListValuesHelper.GetRollSetHistoryForId(id);
    //  return await PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetHistoryById(ModelState, id), "RollSetHistoryForKocksPopupDialog");
    //}

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetPlannedRollsetData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _grindingTurningService.GetPlannedRollsetList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetScheduledRollsetData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _grindingTurningService.GetScheduledRollsetList(ModelState, request));
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    //public async Task<ActionResult> ConfigRollSetForKocksDialog(long id)
    //{
    //  return await PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetDisplay(ModelState, id), "ConfigRollSetForKocksDialog");
    //}
    #region Actions
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> ConfigRollSetSubmit(VM_RollsetDisplay viewModel, string submit)
    {
      //if (viewModel.RollSetType == (short)PE.Core.Constants.RollSetType.twoActiveRollsIM || viewModel.RollSetType == (short)PE.Core.Constants.RollSetType.twoActiveRollsRM)
      //{
        if (submit == Core.Resources.VM_Resources.GLOB_Form_Button_Add)
        {
          return await PreparePopupActionResultFromVm(() => _grindingTurningService.AddGroovesToRollSetDisplay(ModelState, viewModel), "~/Views/Module/PE.Lite/GrindingTurning/_GrindingTurningConfigRollSetPopup.cshtml");
        }
        else if (submit == Core.Resources.VM_Resources.GLOB_Form_Button_Delete)
        {
          return await PreparePopupActionResultFromVm(() => _grindingTurningService.RemoveGroovesToRollSetDisplay(ModelState, viewModel), "~/Views/Module/PE.Lite/GrindingTurning/_GrindingTurningConfigRollSetPopup.cshtml");
        }
        else if (submit == Core.Resources.VM_Resources.GLOB_Form_Button_Save)
        {
          return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grindingTurningService.UpdateGroovesToRollSetDisplay(ModelState, viewModel));
        }
        return await PreparePopupActionResultFromVm(() => _grindingTurningService.AddGroovesToRollSetDisplay(ModelState, viewModel), "~/Views/Module/PE.Lite/GrindingTurning/_GrindingTurningConfigRollSetPopup.cshtml");
      //}
      //else
      //{
      //  if (submit == @PE.HMIWWW.Core.Resources.VM_Resources.GLOB_Form_Button_Add)
      //  {
      //    return await PreparePopupActionResultFromVm(() => _grindingTurningService.AddGroovesToRollSetForKocksDisplay(ModelState, viewModel), "ConfigRollSetForKocksDialog");
      //  }
      //  else if (submit == @PE.HMIWWW.Core.Resources.VM_Resources.GLOB_Form_Button_Delete)
      //  {
      //    return await PreparePopupActionResultFromVm(() => _grindingTurningService.RemoveGroovesToRollSetForKocksDisplay(ModelState, viewModel), "ConfigRollSetForKocksDialog");
      //  }
      //  else if (submit == @PE.HMIWWW.Core.Resources.VM_Resources.GLOB_Form_Button_Save)
      //  {
      //    return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grindingTurningService.UpdateGroovesToRollSetForKocksDisplay(ModelState, viewModel));
      //  }
      //  return await PreparePopupActionResultFromVm(() => _grindingTurningService.AddGroovesToRollSetDisplay(ModelState, viewModel), "ConfigRollSetForKocksDialog");
      //}
    }


    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> ConfirmRollSetSubmit(VM_RollsetDisplay viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grindingTurningService.ConfirmUpdateGroovesToRollSetDisplay(ModelState, viewModel));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> CancelRollset(VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grindingTurningService.CancelRollSetStatus(ModelState, viewModel));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> ConfirmRollset(VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grindingTurningService.ConfirmRollSetStatus(ModelState, viewModel));
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    //public async Task<ActionResult> ConfirmRollSetForKocksDialog(long id)
    //{
    //  return await PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetDisplay(ModelState, id), "ConfirmRollSetForKocksDialog");
    //}

    //[AcceptVerbs(HttpVerbs.Post)]
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    //public async Task<JsonResult> ConfirmRollsetForKocks(VM_LongId viewModel)
    //{
    //  return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grindingTurningService.ConfirmRollSetForKocksStatus(ModelState, viewModel));
    //}
    #endregion
  }
}
