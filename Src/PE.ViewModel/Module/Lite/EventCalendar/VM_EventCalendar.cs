using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.EventCalendar
{
  public class VM_EventCalendar : VM_Base
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }


    public long? EventType { get; set; }
    public string EventTypeName { get; set; }
    public string CrewName { get; set; }
    public long? Id { get; set; }
    public long Color { get; set; }

    public VM_EventCalendar(V_Events entity)
    {
      this.Title = entity.EventName;
      this.Description = entity.EventDescription;
      this.EventType = entity.EventTypeId;
      this.EventTypeName = entity.EventTypeName;
      this.Start = (DateTime)entity.EventTimeFrom;
      this.End = entity.EventTimeTo ?? DateTime.Now;
      this.Id = entity.EventInternalId;
      this.Color = entity.ColorCheck ?? 1;
    }
   
  }
}
