using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.System
{
  public class RoleController : BaseController
  {
    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {

      ViewBag.PermissionTypesController = _roleService.GetPermissionTypesController();
      ViewBag.PermissionTypesMenu = _roleService.GetPermissionTypesMenu();
      ViewBag.AccessUnitsMenu = _roleService.GetAccessUnitsMenu();
      ViewBag.AccessUnitsController = _roleService.GetAccessUnitsController();

      base.OnActionExecuting(filterContext);
    }
    #region services

    IRoleService _roleService;

    #endregion
    #region ctor
    public RoleController(IRoleService service)
    {
      _roleService = service;
    }
    #endregion
    #region view interface
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetRoleData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _roleService.GetRolesList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> EditRoleDialog(string id)
    {
      return await PreparePopupActionResultFromVm(() => _roleService.GetRole(ModelState, id), "EditRoleDialog");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> UpdateRole(VM_Role viewModel)
    {
      return await PrepareJsonResultFromVm(() => _roleService.UpdateRole(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddRoleDialog()
    {
      return PartialView("AddRoleDialog");
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> InsertRole(VM_Role viewModel)
    {
      return await PrepareJsonResultFromVm(() => _roleService.InsertRole(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult EditAccessUnitsDialog(string id)
    {
      return PartialView("EditAccessUnitsDialog", id);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> UpdateAccessUnitPermission(string roleRightId, long accessUnitId, string roleId, bool isAssigned, short permission)
    {
      return await PrepareJsonResultFromVm(() => _roleService.UpdateRight(ModelState, roleRightId, accessUnitId, roleId, isAssigned, permission));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> Delete([DataSourceRequest] DataSourceRequest request, VM_StringId viewModel)
    {
      return await PrepareJsonResultFromVm(() => _roleService.DeleteRole(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> GetRightsTypeData(string roleId, short rightsType, [DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _roleService.GetRightsType(ModelState, request, roleId, rightsType));
    }

    #endregion



  }
}