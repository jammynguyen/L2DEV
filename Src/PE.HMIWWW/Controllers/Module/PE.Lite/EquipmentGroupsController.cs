using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
    public class EquipmentGroupsController : BaseController
    {
			private readonly IEquipmentGroupsService _service;

			public EquipmentGroupsController(IEquipmentGroupsService service)
			{
				_service = service;
			}

    [SmfAuthorization(Constants.SmfAuthorization_Controller_EquipmentGroups, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/EquipmentGroup/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_EquipmentGroups, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    public async Task<ActionResult> GetEquipmentGroupsList([DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = _service.GetEquipmentGroupsList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_EquipmentGroups, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> EquipmentGroupsEditPopupAsync(long id)
    {
      return await PreparePopupActionResultFromVm(() => _service.GetEquipmentGroup(id), "~/Views/Module/PE.Lite/EquipmentGroup/_EquipmentGroupsEditPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_EquipmentGroups, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> UpdateEquipmentGroupAsync(VM_EquipmentGroup vm)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.UpdateEquipmentGroup(ModelState, vm));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_EquipmentGroups, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> CreateEquipmentGroupAsync(VM_EquipmentGroup vm)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.CreateEquipmentGroup(ModelState, vm));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_EquipmentGroups, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> DeleteEquipmentGroupAsync(long id)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteEquipmentGroup(id));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_EquipmentGroups, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> EquipmentGroupCreatePopup()
    {
      return await PreparePopupActionResultFromVm(() => _service.GetEmptyEquipmentGroup(), "~/Views/Module/PE.Lite/EquipmentGroup/_EquipmentGroupsCreatePopup.cshtml");
      //return PartialView("~/Views/Module/PE.Lite/EquipmentGroup/_EquipmentGroupsCreatePopup.cshtml");
    }
  }
}
