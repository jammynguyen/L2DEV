using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class ScheduleController : BaseController
  {

    private readonly IScheduleService _service;

    public ScheduleController(IScheduleService service)
    {
      _service = service;
    }

    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.ScheduleStatuses = ListValuesHelper.GetScheduleStatuses();
      base.OnActionExecuting(ctx);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlaning, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Schedule/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlaning, RightLevel.View)]
    public async Task<JsonResult> GetSchedule([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _service.GetSchedule(ModelState, request));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlaning, RightLevel.Update)]
    public async Task<JsonResult> AddWorkOrderToSchedule(long workOrderId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.AddWorkOrderToSchedule(ModelState, workOrderId));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlaning, RightLevel.Update)]
    public async Task<JsonResult> RemoveItemFromSchedule(long scheduleId, long workOrderId, short orderSeq)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.RemoveItemFromSchedule(ModelState, scheduleId, workOrderId, orderSeq));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlaning, RightLevel.Update)]
    public async Task<JsonResult> MoveItemInSchedule(long scheduleId, long workOrderId, short direction)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.MoveItemInSchedule(ModelState, scheduleId, workOrderId, direction));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlaning, RightLevel.Update)]
    public async Task<ActionResult> CloneSchedulePopup(long scheduleId)
    {
      return await PreparePopupActionResultFromVm(() => _service.GetScheduleData(ModelState, scheduleId), "~/Views/Module/PE.Lite/Schedule/_CreateTestSchedulePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlaning, RightLevel.Update)]
    public async Task<ActionResult> CreateTestSchedule(VM_Schedule viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.CreateTestSchedule(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlaning, RightLevel.View)]
    public async Task<ActionResult> PreparePratialForSchedule()
    {
      return await PrepareActionResultFromVm(() => _service.PreparePratialForSchedule(ModelState), "~/Views/Module/PE.Lite/Schedule/_ScheduleSlideScreen.cshtml");
    }
  }
}
