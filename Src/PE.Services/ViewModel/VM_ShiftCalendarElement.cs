using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using System;

namespace PE.HMIWWW.Services.ViewModel
{
  public class VM_ShiftCalendarElement : VM_Base, ISchedulerEvent
  {
    public VM_ShiftCalendarElement()
    {

    }
    public VM_ShiftCalendarElement(V_ShiftCalendar rec)
    {
      ShiftCalendarId = rec.ShiftCalendarId;
      ShiftCode = rec.ShiftCode;
			if (rec.PlannedStartTime <= DateTime.Now && rec.PlannedEndTime >= DateTime.Now)
				IsActualShift = true;
			else
				IsActualShift = false;
      //if (rec.IsActualShift == null || rec.IsActualShift == false)
      //{
      //  IsActualShift = false;
      //}
      //else
      //{
      //  IsActualShift = true;
      //}

      ShiftDefinitionId = rec.ShiftDefinitionId;
      CrewId = rec.CrewId;
      //no idea what is this
      //if (rec.IsActualWorkingShift == null || rec.IsActualWorkingShift == false)
      //{
      //  IsActualWorkingShift = false;
      //}
      //else
      //{
      //  IsActualWorkingShift = true;
      //}

      switch (ShiftCode)
      {
        case "Day": { ShiftTime = (long)PE.DbEntity.Enums.ShiftTime.Day; break; }
        case "Afternoon": { ShiftTime = (long)PE.DbEntity.Enums.ShiftTime.Afternoon; break; }
        case "Night": { ShiftTime = (long)PE.DbEntity.Enums.ShiftTime.Night; break; }
      }

      //IScheduler interdace implementation
      Start = rec.PlannedStartTime;
      End = rec.PlannedEndTime;
      CrewName = rec.CrewName;
      Title = rec.CrewName;
      this.IsAllDay = false;
    }

    public VM_ShiftCalendarElement(ShiftCalendar rec)
    {
      ShiftCalendarId = rec.ShiftCalendarId;
      IsActualShift = rec.IsActualShift;
     

      switch (ShiftCode)
      {
        case "Day": { ShiftTime = (long)PE.DbEntity.Enums.ShiftTime.Day; break; }
        case "Afternoon": { ShiftTime = (long)PE.DbEntity.Enums.ShiftTime.Afternoon; break; }
        case "Night": { ShiftTime = (long)PE.DbEntity.Enums.ShiftTime.Night; break; }
      }

      //IScheduler interdace implementation
      Start = rec.PlannedStartTime;
      End = rec.PlannedEndTime;
      this.IsAllDay = false;
    }


    public long? ShiftCalendarId { get; set; } // ShiftCalendarId
    public string ShiftCode { get; set; } // ShiftCode
    public string CrewName { get; set; } // CrewName
    [SmfDisplayAttribute(typeof(VM_ShiftCalendarElement), "IsActualShift", "NAME_IsActiveShift")]
    public bool IsActualShift { get; set; } // IsActualShift
    public long ShiftDefinitionId { get; set; } // ShiftDefinitionId
    [SmfDisplayAttribute(typeof(VM_ShiftCalendarElement), "CrewId", "NAME_Shift")]
    public long CrewId { get; set; } // CrewId
    [SmfDisplayAttribute(typeof(VM_ShiftCalendarElement), "IsActualWorkingShift", "NAME_IsWorkingShift")]
    public bool IsActualWorkingShift { get; set; } // IsActualWorkingShift
    [SmfDisplayAttribute(typeof(VM_ShiftCalendarElement), "ShiftTime", "NAME_ShiftTime")]
    public long ShiftTime { get; set; }

    public long DaysOfYearId { get; set; }

    public string Description { get; set; }


    //IScheduler interdace implementation
    public string EndTimezone { get; set; }
    public bool IsAllDay { get; set; }
    [SmfDisplayAttribute(typeof(VM_ShiftCalendarElement), "Start", "NAME_From")]
    public DateTime Start { get; set; } // DelayStart
    [SmfDisplayAttribute(typeof(VM_ShiftCalendarElement), "End", "NAME_To")]
    public DateTime End { get; set; } // DelayEnd
    public string RecurrenceException { get; set; }
    public string RecurrenceRule { get; set; }
    public string StartTimezone { get; set; }
    public string Title { get; set; }



  }
  //public class VM_ShiftCalendarElementList : List<VM_ShiftCalendarElement>
  //{
  //	public VM_ShiftCalendarElementList()
  //	{
  //	}
  //	//public VM_DADelayElementList(IList<VDelaysSummaryPivotData> dbClass)
  //	//{
  //	//	foreach (VDelaysSummaryPivotData item in dbClass)
  //	//	{
  //	//		this.Add(new VM_DADelayElement(item));
  //	//	}
  //	//}
  //}
}
