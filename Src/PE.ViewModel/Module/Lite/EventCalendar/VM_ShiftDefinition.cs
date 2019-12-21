using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.EventCalendar
{
  public class VM_ShiftDefinition:VM_Base
  {
    public string ShiftCode { get; set; }
    public TimeSpan ShiftStart { get; set; }
    public TimeSpan ShiftEnd { get; set; }
    public bool EndNextDay { get; set; }
    public VM_ShiftDefinition(ShiftDefinition entity)
    {
      this.ShiftCode = entity.ShiftCode;
      this.ShiftStart = entity.DefaultStartTime;
      this.ShiftEnd = entity.DefaultEndTime;
      this.EndNextDay = entity.ShiftEndsNextDay;
    }
  }
}
