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
using PE.HMIWWW.Services.Module.PE.Lite;
using PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;


namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class ActualStandConfigurationController : BaseController
  {
    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.StandStat = ListValuesHelper.GetStandStat();
      ViewBag.Arrang = ListValuesHelper.GetCassetteArrangement();
      ViewBag.ArrangNoUndefined = ListValuesHelper.GetCassetteArrangementNoUndefined();
      ViewBag.CassetteType = ListValuesHelper.GetCassetteType();
      ViewBag.CassetteStatus = ListValuesHelper.GetCassetteStatus();
      ViewBag.RSetStatus = ListValuesHelper.GetRollSetStatus();
      ViewBag.GrooveStatus = ListValuesHelper.GetRollGrooveStatus();
      ViewBag.UnmountedCassettes = ListValuesHelper.GetUnmountedCassettes();
      ViewBag.MountedCassettes = ListValuesHelper.GetMountedCassettes();
      ViewBag.AvailableCassettes = ListValuesHelper.GetCassettesReadyForMount();
      ViewBag.AvailableCassettesWith2Rolls = ListValuesHelper.GetCassettesReadyForMountWith2Rolls();
      //ViewBag.AvailableCassettesWith3Rolls = ListValuesHelper.GetCassettesReadyForMountWithNoOfRolls();
      ViewBag.StandStatNoUndefined = ListValuesHelper.GetStandStatNoUndefined();
      ViewBag.AvailableRollSets = ListValuesHelper.GetRollsetsReadyOnlyWithTypes();
      ViewBag.NumberOfRolls = ListValuesHelper.GetNumberOfActiveRoll();
      ViewBag.StandActivity = ListValuesHelper.GetStandActivity();
      ViewBag.IsThirdActive = ListValuesHelper.GetNumberOfActiveRoll();
      ViewBag.RSHistStatus = ListValuesHelper.GetRollSetHistoryStatus();
      ViewBag.RollsetTypes = ListValuesHelper.GetRollsetTypeList();
      //  ViewBag.StandBlockName = ListValuesHelper.GetStandBlockName();


      base.OnActionExecuting(ctx);
    }
    #region services

    IActualStandsConfigurationService _actualStandsConfigurationService;

    #endregion

    #region ctor



    public ActualStandConfigurationController(IActualStandsConfigurationService service)
    {
      _actualStandsConfigurationService = service;
    }
    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/ActualStandConfiguration/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddStandConfigurationDialog()
    {
      return PartialView("~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationAddStandPopup.cshtml", new VM_StandConfiguration());
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> RollSetInfoPopupDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationRollSetInfoPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> RenderNewCassetteDetails(long CassetteId, long StandId)
    {
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetCassetteWithPositions(ModelState, CassetteId, StandId), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationNewCassetteDetailsPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> CassetteInfoPopupDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetCassette(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationCassetteInfoPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> EditStandConfigurationDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetStandConfiguration(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationEditPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> MountCassetteDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetStandConfiguration(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationMountCassettePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> MountEmptyCassetteDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetStandConfiguration(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationMountEmptyCassettePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> DismountCassetteDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetStandConfiguration(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationDismountCassettePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> MountRollSetDialog(long id, short? param)
    {
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetRollChangeOperation(ModelState, id, param), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationMountRollSetPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> DismountRollSetDialog(long id, short? param)
    {
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetRollChangeOperationForRollSet(ModelState, id, param), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationDismountRollSetPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> SwapStandDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetStandConfiguration(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationSwapStandPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> SwapCassetteDialog(long id)
    {
      //  ViewBag.AvailableCassettes = ListValuesHelper.GetCassettesReadyForMount(id); //standId
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetSwapCassette(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationSwapCassettePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> SwapRollSetDialog(long id, short param)
    {
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetRollChangeOperation(ModelState, id, param), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationSwapRollSetPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> PassChangeGrooveDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationPassChangeGroovePopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetStandConfigurationCatalogueData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _actualStandsConfigurationService.GetStandConfigurationList(ModelState, request));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetPassChangeActualData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _actualStandsConfigurationService.GetPassChangeActualList(ModelState, request));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetCassetteRollSetsData([DataSourceRequest]DataSourceRequest request, long? cassetteId, short? standStatus)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _actualStandsConfigurationService.GetCassetteRollSetsList(ModelState, request, cassetteId, standStatus));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetRollSetInCassetteList([DataSourceRequest]DataSourceRequest request, long? cassetteId)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _actualStandsConfigurationService.GetRollSetInCassetteList(ModelState, request, (long)cassetteId));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetRollSetGroovesData([DataSourceRequest]DataSourceRequest request, long rollsetHistoryId, short? standStatus) // rollsetHistoryId,
    {
      return await PrepareJsonResultFromDataSourceResult(() => _actualStandsConfigurationService.GetRollSetGroovesList(ModelState, request, rollsetHistoryId, standStatus));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> AssembleCassetteAndRollsetDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetCassetteOverviewWithPositions(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_ActualStandConfigurationAssembleCassettePopup.cshtml");
    }


    //[SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    //public async Task<JsonResult> AssembleRollSetAndCassette(VM_CassetteOverviewWithPositions viewModel)
    //{
    //  return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.AssembleRollSetAndCassette(ModelState, viewModel));
    //}


    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Customer, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    //public async Task<JsonResult> GetCassetteRSWith3RollsList([DataSourceRequest]DataSourceRequest request, short? cassetteType)
    //{
    //    return await PrepareJsonResultFromDataSourceResult(() => _actualStandsConfigurationService.GetCassetteRSWith3RollsList(ModelState, request, (short)cassetteType));
    //}

    //[HttpGet]
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    //public JsonResult GetCassetteRSWith3RollsList(short CassetteType)
    //{
    //  return Json(RollsetToCassetteService.GetCassetteRSWith3RollsList(CassetteType), JsonRequestBehavior.AllowGet);
    //}

    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public JsonResult GetCassetteToStandList([DataSourceRequest] DataSourceRequest request, long StandId)
    {
      return Json(ActualStandsConfigurationService.GetCassetteToStandList(StandId), JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public JsonResult GetEmptyCassetteToStandList([DataSourceRequest] DataSourceRequest request, long StandId)
    {
      return Json(ActualStandsConfigurationService.GetEmptyCassetteToStandList(StandId), JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public JsonResult GetCassetteToStandList_test([DataSourceRequest] DataSourceRequest request, long StandId)
    {
      return Json(ActualStandsConfigurationService.GetCassetteToStandList(StandId), JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public JsonResult GetAvailableRollsets(short type)
    {
      return Json(ListValuesHelper.GetRollsetsReadyOnlyWithTypes(type), JsonRequestBehavior.AllowGet);
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    //public async Task<ActionResult> PassChangeGrooveForKocksDialog(long id)
    //{
    //  return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetRollSetDisplay(ModelState, id), "PassChangeGrooveForKocksDialog");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    //public async Task<ActionResult> RollSetInfoPopupForKocksDialog(long id)
    //{
    //  return await PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetRollSetDisplay(ModelState, id), "RollSetInfoPopupForKocksDialog");
    //}

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> UpdateStandConfiguration(VM_StandConfiguration viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.UpdateStandConfiguration(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> MountCassette(VM_StandConfiguration viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.MountCassette(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> MountRollSet(VM_RollChangeOperation viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.MountRollSet(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> SwapRollSet(VM_RollChangeOperation viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.SwapRollSet(ModelState, viewModel));
    }
    #region Actions

    #region Grooves

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> ConfigRollSetSubmit(VM_RollsetDisplay viewModel, string submit)
    {
      //submit = "";
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.UpdateGroovesToRollSetDisplay(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> DismountRollSet(VM_RollChangeOperation viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.DismountRollSet(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> DismountCassette(VM_StandConfiguration viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.DismountCassette(ModelState, viewModel));
    }


    #endregion

    #endregion
  }
}
