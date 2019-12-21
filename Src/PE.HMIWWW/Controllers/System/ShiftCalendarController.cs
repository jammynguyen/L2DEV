using Kendo.Mvc.UI;
using PE.Core;
using PE.DbEntity.Model;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.Services.ViewModel;
using PE.HMIWWW.ViewModel.System;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.System
{

  public class ShiftCalendarController : BaseController
  {

    #region services

    IShiftCalendarService _shiftCalendarService;

    #endregion

    #region ctor
    public ShiftCalendarController(IShiftCalendarService service)
    {
      _shiftCalendarService = service;
    }
    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> ShiftCalendarData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _shiftCalendarService.GetShiftCalendarsList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> UpdateShiftCalendarElement(VM_ShiftCalendarElement viewModel)
    {
      return await PrepareJsonResultFromVm(() => _shiftCalendarService.UpdateShiftCalendarElement(ModelState, viewModel));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> DeleteShiftCalendarElement([DataSourceRequest] DataSourceRequest request, VM_LongId viewModel)
    {
      return await PrepareJsonResultFromVm(() => _shiftCalendarService.DeleteShiftCalendarElement(ModelState, viewModel));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> GenerateShiftCalendarsForNextWeek([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _shiftCalendarService.GenerateShiftCalendarForNextWeek(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> EditShiftCalendarDialog(VM_LongId viewModel)
    {
      return await PreparePopupActionResultFromVm(() => _shiftCalendarService.GetShiftCalendarElement(ModelState, new VM_ShiftCalendarElement { ShiftCalendarId = viewModel.Id }), "EditShiftCalendarDialog");
    }

    public static List<ShiftDefinition> GetShiftDefinitionsList()
    {
      return ShiftCalendarService.GetShiftDefinitionsList();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddShiftCalendarDialog(VM_StringId viewModel)
    {
      VM_ShiftCalendarElement vModel = new VM_ShiftCalendarElement();
      vModel.Start = DateTime.Parse(viewModel.Id);
      vModel.End = DateTime.Parse(viewModel.Id);

      return PartialView("AddShiftCalendarDialog", vModel);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> InsertShiftCalendar(VM_ShiftCalendarElement viewModel)
    {
      return await PrepareJsonResultFromVm(() => _shiftCalendarService.InsertShiftCalendar(ModelState, viewModel));
    }
    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> PrepareCrewData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _shiftCalendarService.PrepareCrewData(ModelState, request));
    }
  }
}