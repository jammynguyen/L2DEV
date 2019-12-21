using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  [SmfAuthorization(Constants.SmfAuthorization_Controller_EventCalendar, Constants.SmfAuthorization_Module_EventCalendar, RightLevel.View)]
  public class EventCalendarController : BaseController
  {
    IEventCalendarService _eventCalendarService;
    public EventCalendarController(IEventCalendarService service)
    {
      _eventCalendarService = service;
    }

    // GET: EventCalendar
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/EventCalendar/Index.cshtml",_eventCalendarService.GetShiftDefinitions());
    }

    public JsonResult EventCalendarData(long eventId,DateTime date)
    {
      IEnumerable<ViewModel.Module.Lite.EventCalendar.VM_EventCalendar> result = _eventCalendarService.GetEventCalendarData(eventId, date);
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    public JsonResult GetEventTypeList()
    {
      return Json(_eventCalendarService.GetEventTypeList(), JsonRequestBehavior.AllowGet);
    }
  }
}
