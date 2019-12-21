using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.Services.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.EventCalendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class EventCalendarService : BaseService, IEventCalendarService
  {
    public IEnumerable<VM_EventCalendar> GetEventCalendarData(long eventId, DateTime date)
    {
      List<VM_EventCalendar> result = new List<VM_EventCalendar>();
      using (PEContext ctx = new PEContext())
      {
        IEnumerable<V_Events> list = ctx.V_Events
          .AsNoTracking()
          .Where(x => x.EventTypeId == eventId && x.Month == date.Month && x.Year == date.Year).AsEnumerable();

        foreach (V_Events item in list)
        {
          result.Add(new VM_EventCalendar(item));
        }
      }
      return result;
    }

    public List<VM_EventList> GetEventTypeList()
    {
      List<VM_EventList> result = new List<VM_EventList>();
      using (PEContext ctx = new PEContext())
      {

        IQueryable<EventType> dbList = ctx.EventTypes.AsQueryable();
        foreach (EventType item in dbList)
        {
          result.Add(new VM_EventList(item));
        }
      }
      return result;
    }

    public List<VM_ShiftDefinition> GetShiftDefinitions()
    {
      List<VM_ShiftDefinition> result = new List<VM_ShiftDefinition>();
      using (PEContext ctx = new PEContext())
      {

        IQueryable<ShiftDefinition> dbList = ctx.ShiftDefinitions.AsQueryable();
        foreach (ShiftDefinition item in dbList)
        {
          result.Add(new VM_ShiftDefinition(item));
        }
      }
      return result;
    }
  }
}
