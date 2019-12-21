using PE.DbEntity;
using PE.DbEntity.Model;
using PE.DbEntity.Models;
using SMF.Module.Core;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.Helpers
{
	public static class Helpers
	{
		public static ShiftCalendar GetActualWorkingShiftCalendar()
		{ 
			ShiftCalendar shiftCalendar = null;
			try
			{
				using (PEContext uow = new PEContext())
				{
					shiftCalendar = uow.ShiftCalendars.Where(z => z.IsActualShift == true).Single();
				}
			}
			catch (Exception ex)
			{
				DbExceptionHelper.ProcessException(ex, "GetActualWorkingShiftCalendar::Database operation failed!");
			}
			return shiftCalendar;
		}
	}
}
