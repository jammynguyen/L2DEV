using Kendo.Mvc.UI;
using PE.HMIWWW.Services.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.EventCalendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IEventCalendarService
  {
    IEnumerable<VM_EventCalendar> GetEventCalendarData(long eventId,DateTime date);
    List<VM_EventList> GetEventTypeList();
    List<VM_ShiftDefinition> GetShiftDefinitions();
  }
}
