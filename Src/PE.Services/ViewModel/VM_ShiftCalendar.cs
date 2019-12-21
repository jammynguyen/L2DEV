using System;
using SMF.HMIWWW.Attributes;
using SMF.DbEntity.Model;
using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.Model;

namespace PE.HMIWWW.Services.ViewModel
{
	public class VM_ShiftCalendar
	{
		public VM_ShiftCalendar()
		{
		}

		public List<VM_ShiftCalendarElement> vmShiftCalendarList { get; set; }
	}
}
