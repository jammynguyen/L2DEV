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
using PE.HMIWWW.ViewModel.Module.Lite.CassetteType;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class CassetteTypeController : BaseController
  {

    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.NumberOfRolls = ListValuesHelper.GetNumberOfActiveRoll();  // <-

      base.OnActionExecuting(ctx);
    }

    #region services

    ICassetteTypeService _cassetteTypeService;

    #endregion

    #region ctor
    public CassetteTypeController(ICassetteTypeService service)
    {
      _cassetteTypeService = service;
    }
    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/CassetteType/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddCassetteTypeDialog()
    {
      return PartialView("~/Views/Module/PE.Lite/CassetteType/_CassetteTypeAddPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> EditCassetteTypeDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _cassetteTypeService.GetCassetteType(ModelState, id), "~/Views/Module/PE.Lite/CassetteType/_CassetteTypeEditPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Details(long id)
    {
      return PartialView("_Details", _cassetteTypeService.GetCassetteType(ModelState, id));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetCassetteTypeData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _cassetteTypeService.GetCassetteTypeList(ModelState, request));
    }

    #region Actions
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> InsertCassetteType(VM_CassetteType viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _cassetteTypeService.InsertCassetteType(ModelState, viewModel));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> DeleteCassetteType(VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _cassetteTypeService.DeleteCassetteType(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> UpdateCassetteType(VM_CassetteType viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _cassetteTypeService.UpdateCassetteType(ModelState, viewModel));
    }
    #endregion
  }
}
