using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.Common
{
	public class CommonAlarmDefs
	{
		//system alarms/notifications
		//SF moved to SMF
		public static string Alarm_CannotGenerateShiftCalendarsNoPreviousWeekRecords = "S010";
		public static string Alarm_CannotGenerateShiftCalendarsExistNextWeekRecords = "S011";
		public static string Alarm_GenerateShiftCalendarsWithGaps = "S012";
		public static string Alarm_ExceptionInViewBag = "S014";
		public static string Alarm_InvalidParameter = "S015"; //module name, parameter name, description


	}
}
