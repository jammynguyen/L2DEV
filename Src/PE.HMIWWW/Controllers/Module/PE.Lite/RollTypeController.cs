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
using PE.HMIWWW.ViewModel.Module.Lite.RollType;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class RollTypeController : BaseController
  {
    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      //ViewBag.AdjustRoll = ListValuesHelper.GetAdjustRoll();
      ViewBag.RollsetTypes = ListValuesHelper.GetRollsetTypeList();

      base.OnActionExecuting(ctx);
    }
    #region services

    IRollTypeService _rollTypeService;

    #endregion

    #region ctor
    public RollTypeController(IRollTypeService service)
    {
      _rollTypeService = service;
    }
    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/RollType/Index.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddRollTypeDialog()
    {
      return PartialView("~/Views/Module/PE.Lite/RollType/_RollTypeAddPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> EditRollTypeDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _rollTypeService.GetRollType(ModelState, id), "~/Views/Module/PE.Lite/RollType/_RollTypeEditPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Details(long id)
    {
      return PartialView("_Details", _rollTypeService.GetRollType(ModelState, id));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetRollTypeData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _rollTypeService.GetRollTypeList(ModelState, request));
    }

    #region Actions
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> InsertRollType(VM_RollType viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollTypeService.InsertRollType(ModelState, viewModel));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> DeleteRollType(VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollTypeService.DeleteRollType(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> UpdateRollType(VM_RollType viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollTypeService.UpdateRollType(ModelState, viewModel));
    }
    #endregion
  }
}
