using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
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
    public class EquipmentController : BaseController
    {
			private readonly IEquipmentService _service;

			public EquipmentController(IEquipmentService service)
			{
				_service = service;
			}

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    public ActionResult Index()
    {
      ViewBag.EquipmentStatus = ListValuesHelper.GetEquipmentStatuses();
      return View("~/Views/Module/PE.Lite/Equipment/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    public async Task<ActionResult> GetEquipmentList([DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = _service.GetEquipmentList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> EquipmentEditPopupAsync(long id)
    {
      //ViewBag.EquipmentGroupList = _service.GetEquipmentGroupList();
      ViewBag.EquipmentGroupList = ListValuesHelper.GetEquipmentGroupsList();
      ViewBag.EquipmentStatus = ListValuesHelper.GetEquipmentStatuses();
      return await PreparePopupActionResultFromVm(() => _service.GetEquipment(id), "~/Views/Module/PE.Lite/Equipment/_EquipmentEditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> EquipmentClonePopupAsync(long id)
    {
      //ViewBag.EquipmentGroupList = _service.GetEquipmentGroupList();
      ViewBag.EquipmentGroupList = ListValuesHelper.GetEquipmentGroupsList();
      ViewBag.EquipmentStatus = ListValuesHelper.GetEquipmentStatuses();
      return await PreparePopupActionResultFromVm(() => _service.GetEquipment(id), "~/Views/Module/PE.Lite/Equipment/_EquipmentClonePopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> UpdateEquipmentAsync(VM_Equipment vm)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.UpdateEquipment(ModelState, vm));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> CreateEquipmentAsync(VM_Equipment vm)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.CreateEquipment(ModelState, vm));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> CloneEquipmentAsync(VM_Equipment vm)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.CloneEquipment(ModelState, vm));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> DeleteEquipmentAsync(long id)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteEquipment(id));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> EquipmentCreatePopupAsync()
    {
      //ViewBag.EquipmentGroupList = _service.GetEquipmentGroupList();
      ViewBag.EquipmentGroupList = ListValuesHelper.GetEquipmentGroupsList();
      return await PreparePopupActionResultFromVm(() => _service.GetEmptyEquipment(), "~/Views/Module/PE.Lite/Equipment/_EquipmentCreatePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    public async Task<ActionResult> ShowEquipmentHistory(long id)
    {
      ViewBag.EquipmentStatus = ListValuesHelper.GetEquipmentStatuses();
      //return await PrepareActionResultFromVm(() => _heatService.GetHeatDetails(ModelState, id), "~/Views/Module/PE.Lite/Heat/_HeatBody.cshtml");
      return await PreparePopupActionResultFromVm(() => _service.GetEquipmentHistory(id), "~/Views/Module/PE.Lite/Equipment/_EquipmentHistoryPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    public async Task<ActionResult> GetEquipmentHistory([DataSourceRequest]DataSourceRequest request, long id)
    {
      ViewBag.EquipmentStatus = ListValuesHelper.GetEquipmentStatuses();
      DataSourceResult result = _service.GetEquipmentHistoryList(id, ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    //public async Task<ActionResult> EquipmentHistory(long id)
    //{
    //  return await PrepareActionResultFromVm(() => _service.GetEquipmentHistory(ModelState, id), "~/Views/Module/PE.Lite/Heat/_HeatBody.cshtml");
    //}
  }
}
