using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.Core;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.RollChange;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class RollChangeController : BaseController
  {
    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      //ViewBag.StandId = _rollChangeService.GetFirstStandId();
      ViewBag.MountedRollSetStatusNo = (short)RollSetStatus.Mounted;
      ViewBag.GrooveStatusText = ListValuesHelper.GetRollGrooveStatus();
      ViewBag.RollsetStatusText = ListValuesHelper.GetRollSetStatus();
      ViewBag.ArrangementText = ListValuesHelper.GetCassetteArrangement();
      ViewBag.StandStatusText = ListValuesHelper.GetStandStat();
      base.OnActionExecuting(ctx);
    }

    #region Services
    IRollChangeService _rollChangeService;
    #endregion

    #region Ctor
    public RollChangeController(IRollChangeService service)
    {
      _rollChangeService = service;
    }
    #endregion

    #region Database Operations

    #region View Methods

    // GET: RollChange
    public ActionResult Index()
    {
      return View();
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_RollChangeKocks, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult ConfirmationDialog(short? id, long? param, long? param2, short? param3, short? param4)
    {
      short? operationType = id;
      long? rollsetId = param;
      long? mountedRollsetId = param2;
      short? position = param3;
      short? standNo = param4;
      return PartialView("ConfirmationForRmAndImDialog", _rollChangeService.BuildVMConfirmationForRmAndIm(operationType, rollsetId, mountedRollsetId, position, standNo));
    }

    #endregion

    #region Data Grid Methods

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetStandGridData([DataSourceRequest]DataSourceRequest request, long firstStand, long lastStand)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _rollChangeService.GetStandGridData(ModelState, request, firstStand, lastStand));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetRollsetGridData([DataSourceRequest]DataSourceRequest request, long standNo)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _rollChangeService.GetRollsetGridData(ModelState, request, standNo));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetRollGroovesGridData([DataSourceRequest]DataSourceRequest request, long rollSetId, long standNo)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _rollChangeService.GetRollGroovesGridData(ModelState, request, rollSetId, standNo));
    }

    #endregion

    #region Actions

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public JsonResult GetCassettesInfoRmIm()
    {
      List<V_RSCassettesInStands> foundStands = _rollChangeService.GetCassettesInfoRmIm();
      if (foundStands.Count() > 0)
      {
        return Json(foundStands, JsonRequestBehavior.AllowGet);
      }
      return Json("", JsonRequestBehavior.AllowGet);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public JsonResult GetAvailableRollsets(short standNo)
    {
      List<V_RollsetOverviewNewest> foundRollsets = _rollChangeService.GetAvailableRollsets(standNo);
      if (foundRollsets.Count() > 0)
      {
        return Json(CreateReturnRollsetObject(foundRollsets), JsonRequestBehavior.AllowGet);
      }
      return Json("", JsonRequestBehavior.AllowGet);
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_RollChangeKocks, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> PostConfiguration(VM_ConfirmationForRmAndIm viewModel)
    {
      viewModel.Position = 1; //override, is filled with stando!
      switch (viewModel.OperationType)
      {
        case (short)RollChangeAction.SwapRollSetOnly:
          {
            return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollChangeService.SwapRollSet(ModelState, viewModel));
          }
        case (short)RollChangeAction.SwapCassette:
          {
            //TODO
            break;
          }
        case (short)RollChangeAction.MountRollSetOnly:
          {
            return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollChangeService.MountRollset(ModelState, viewModel));
          }
        case (short)RollChangeAction.DismountRollSetOnly:
          {
            return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollChangeService.DismountRollset(ModelState, viewModel));
          }
        case (short)RollChangeAction.DismountWithCassette:
          {
            break;
          }
        case (short)RollChangeAction.MountWithCassette:
          {
            break;
          }
      }
      return null;
    }

    #endregion

    #endregion

    #region Helper Methods

    private List<ReturnRollset> CreateReturnRollsetObject(List<V_RollsetOverviewNewest> foundRollsets)
    {
      List<ReturnRollset> rollsets = new List<ReturnRollset>();
      foreach (V_RollsetOverviewNewest item in foundRollsets)
      {
        long rollId = _rollChangeService.GetRollId(item) ?? 0;
        short groovesNumber = _rollChangeService.GetGrooveNumber(rollId);
        rollsets.Add(new ReturnRollset
        {
          RollSetType = item.RollSetType,
          RollSetName = item.RollSetName,
          RollSetId = item.RollSetId,
          GroovesNumber = groovesNumber
        });
      }
      return rollsets;
    }

    #endregion

    #region Structs

    struct ReturnRollset
    {
      public short RollSetType { get; set; }
      public string RollSetName { get; set; }
      public long RollSetId { get; set; }
      public short GroovesNumber { get; set; }
    }

    #endregion
  }
}
