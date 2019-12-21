using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.EventCalendar
{
  public class VM_EventList : VM_Base
  {
    public long EventType { get; set; }
    public string EventCode { get; set; }
    public string EventName { get; set; }
    public string EventColor { get; set; }
    public string EventIcon { get; set; }
    public bool Editable { get; set; }
    public string EditLink { get; set; }
    public VM_EventList(EventType entity)
    {
      this.EventType = entity.EventTypeId;
      this.EventName = entity.EventTypeName;
      this.EventCode = entity.EventTypeCode;
      this.EventColor = entity.HMIColor;
      this.EventIcon = entity.HMIIcon;
      this.Editable = entity.IsEditable;
      this.EditLink = entity.HMILink;
    }
  }
}
