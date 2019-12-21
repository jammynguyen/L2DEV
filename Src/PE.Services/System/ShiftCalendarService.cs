using PE.HMIWWW.ViewModel.System;
using PE.Common;
using SMF.DbEntity;
using SMF.DbEntity.Model;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Threading.Tasks;
using SMF.RepositoryPattern.Infrastructure;
using PE.DbEntity.Model;
using PE.DbEntity;
using PE.HMIWWW.Core.HtmlHelpers;
using SMF.Core.Helpers;
using PE.HMIWWW.Core.Resources;
using SMF.Module.Notification;
using PE.HMIWWW.Core;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.ViewModel;
using PE.DbEntity.Models;
using System.Data.Entity;

namespace PE.HMIWWW.Services.System
{
    public interface IShiftCalendarService
		{
			DataSourceResult GetShiftCalendarsList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
			VM_ShiftCalendarElement UpdateShiftCalendarElement(ModelStateDictionary modelState, VM_ShiftCalendarElement viewModel);
			VM_LongId DeleteShiftCalendarElement(ModelStateDictionary modelState, VM_LongId viewModel);
			DataSourceResult GenerateShiftCalendarForNextWeek(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
			VM_ShiftCalendarElement GetShiftCalendarElement(ModelStateDictionary modelState, VM_ShiftCalendarElement viewModel);
			VM_LongId InsertShiftCalendar(ModelStateDictionary modelState, VM_ShiftCalendarElement viewModel);
			DataSourceResult PrepareCrewData(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);

	}
	public class ShiftCalendarService : BaseService, IShiftCalendarService
	{
		#region interface IShiftCalendarService
		public DataSourceResult GetShiftCalendarsList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
		{
			DataSourceResult returnValue = null;

			// VALIDATE ENTRY PARAMETERS
			if (!modelState.IsValid)
			{
				return returnValue;
			}
			//END OF VALIDATION

			//DB OPERATION 
			VM_ShiftCalendar vmodel = new VM_ShiftCalendar();
			List<VM_ShiftCalendarElement> vm = new List<VM_ShiftCalendarElement>();

			using (PEContext uow = new PEContext())
			{
				IList<V_ShiftCalendar> scCollection = uow.V_ShiftCalendar.ToList();
				foreach (V_ShiftCalendar rec in scCollection)
				{
					vm.Add(new VM_ShiftCalendarElement(rec));
				}
				vmodel.vmShiftCalendarList = vm;
			}
			returnValue = vmodel.vmShiftCalendarList.ToDataSourceResult(request, modelState);
			//END OF DB OPERATION

			return returnValue;
		}

		public VM_ShiftCalendarElement UpdateShiftCalendarElement(ModelStateDictionary modelState, VM_ShiftCalendarElement viewModel)
		{
			VM_ShiftCalendarElement returnValueVm = null;

			//VALIDATE ENTRY PARAMETERS
			if (viewModel == null)
			{
				AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
			}
			if (viewModel.CrewId <= 0)
			{
				AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
			}
			if (!modelState.IsValid)
			{
				return returnValueVm;
			}
			//END OF VALIDATION

			//DB OPERATION
			using (PEContext uow = new PEContext())
			{
				ShiftCalendar vShiftCalendar = uow.ShiftCalendars.Where(z => z.ShiftCalendarId == viewModel.ShiftCalendarId).Include(z =>z.ShiftDefinition).Single();
				vShiftCalendar.FKCrewId = viewModel.CrewId;

				vShiftCalendar.PlannedStartTime = vShiftCalendar.PlannedStartTime.Date + this.GetPlannedStartTime(viewModel.ShiftTime);
				vShiftCalendar.PlannedEndTime = vShiftCalendar.PlannedEndTime.Date + this.GetPlannedEndTime(viewModel.ShiftTime);

				if (vShiftCalendar.ShiftDefinition.ShiftDefinitionId != (long)PE.DbEntity.Enums.ShiftTime.Night)
				{
					if (viewModel.ShiftTime == (long)PE.DbEntity.Enums.ShiftTime.Night)
					{
						if (vShiftCalendar.PlannedEndTime.TimeOfDay < vShiftCalendar.PlannedStartTime.TimeOfDay)
							vShiftCalendar.PlannedEndTime = vShiftCalendar.PlannedEndTime.AddDays(1);
						if (vShiftCalendar.PlannedStartTime.TimeOfDay < vShiftCalendar.PlannedEndTime.TimeOfDay)
							vShiftCalendar.PlannedEndTime = vShiftCalendar.PlannedEndTime.AddDays(-1);
					}
				}

				if (vShiftCalendar.ShiftDefinition.ShiftDefinitionId == (long)PE.DbEntity.Enums.ShiftTime.Night)
				{
					if (viewModel.ShiftTime != (long)PE.DbEntity.Enums.ShiftTime.Night)
					{
						if (vShiftCalendar.PlannedEndTime.TimeOfDay < vShiftCalendar.PlannedStartTime.TimeOfDay)
							vShiftCalendar.PlannedEndTime = vShiftCalendar.PlannedEndTime.AddDays(1);
						if (vShiftCalendar.PlannedStartTime.TimeOfDay < vShiftCalendar.PlannedEndTime.TimeOfDay)
							vShiftCalendar.PlannedEndTime = vShiftCalendar.PlannedEndTime.AddDays(-1);
					}
				}
				vShiftCalendar.FKShiftDefinitionId = viewModel.ShiftTime;
				//vShiftCalendar.State = ObjectState.Modified;
				uow.SaveChanges();
			}
			//END OF DB OPERATION

			return viewModel;
		}

		public VM_LongId DeleteShiftCalendarElement(ModelStateDictionary modelState, VM_LongId viewModel)
		{
			VM_LongId returnValueVm = null;
			
			//VALIDATE ENTRY PARAMETERS
			if (viewModel == null)
			{
				AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
			}
			if (viewModel.Id <= 0)
			{
				AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
			}
			if (!modelState.IsValid)
			{
				return returnValueVm;
			}
			//END OF VALIDATION

			//DB OPERATION
			using (PEContext uow = new PEContext())
			{
				ShiftCalendar shiftCalendar = uow.ShiftCalendars.Where(z => z.ShiftCalendarId == viewModel.Id).Single();
				if (shiftCalendar != null)
				{
					uow.ShiftCalendars.Remove(shiftCalendar);
					uow.SaveChanges();
					returnValueVm = new VM_LongId(viewModel.Id);
				}
			}
			//END OF DB OPERATION

			return returnValueVm;
		}

		public DataSourceResult GenerateShiftCalendarForNextWeek(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
		{
			DataSourceResult result = null;

			// VALIDATE ENTRY PARAMETERS
			if (!modelState.IsValid)
			{
				return result;
			}
			//END OF VALIDATION


			//DB OPERATION 
			DateTime LastDateTime, DateTimeYesterday;
			List<VM_ShiftCalendarElement> vm = new List<VM_ShiftCalendarElement>();
			using (PEContext uow = new PEContext())
			{
				//check if something exists in shift calendars records for next week
				List<ShiftCalendar> ShiftCalendarElementsInNextWeek = uow.ShiftCalendars.Where(z => z.PlannedStartTime > DateTime.Now).ToList();
				if (ShiftCalendarElementsInNextWeek.Count == 0)
				{
					//if not exists 
					//create 7 like in previous week
					LastDateTime = DateTime.Now.AddDays(-7);
					DateTimeYesterday = DateTime.Now.AddDays(-1);
					List<ShiftCalendar> ShiftCalendarElementsInPrevoiusWeek = uow.ShiftCalendars.Where(z => (z.PlannedStartTime >= LastDateTime && z.PlannedStartTime < DateTimeYesterday)).ToList();

					bool IsBreakInShiftCalendar = !this.CheckShiftCalendarsData(ShiftCalendarElementsInPrevoiusWeek);

					if (ShiftCalendarElementsInPrevoiusWeek.Count > 0)
					{
						foreach (ShiftCalendar ShiftCalendarElement in ShiftCalendarElementsInPrevoiusWeek)
						{
							ShiftCalendar ShiftCalendarNew = ShiftCalendarElement;
							ShiftCalendarNew.PlannedStartTime = ShiftCalendarNew.PlannedStartTime.AddDays(7);
							ShiftCalendarNew.PlannedEndTime = ShiftCalendarNew.PlannedEndTime.AddDays(7);
							//ShiftCalendarNew.State = ObjectState.Added;
							uow.ShiftCalendars.Add(ShiftCalendarNew);
							uow.SaveChanges();

							result = ShiftCalendarElementsInPrevoiusWeek.ToDataSourceResult<ShiftCalendar, VM_ShiftCalendarElement>(request, modelState, data => new VM_ShiftCalendarElement(data));
						}
					}
					else
					{
						NotificationController.RegisterAlarm(CommonAlarmDefs.Alarm_CannotGenerateShiftCalendarsNoPreviousWeekRecords, String.Format("Cannot generate new shift calendar records because prevoius shifts not exist."));
						throw new Exception(ResourceController.GetResourceTextByResourceKey("GLOB_CannotGenerateShiftCalendarPrevRecsNotExist"));
					}
					if (IsBreakInShiftCalendar)
					{
						NotificationController.RegisterAlarm(CommonAlarmDefs.Alarm_GenerateShiftCalendarsWithGaps, String.Format("Break exists in previous week. New elements in shift calendar added with gaps. {0}", " "), " ");
						throw new Exception(ResourceController.GetResourceTextByResourceKey("GLOB_GenerateShiftCalendarBreakExists"));
					}
				}
				else
				{
					NotificationController.RegisterAlarm(CommonAlarmDefs.Alarm_CannotGenerateShiftCalendarsExistNextWeekRecords, String.Format("Cannot generate new shift calendar records because next shifts exist."));
					throw new Exception(ResourceController.GetResourceTextByResourceKey("GLOB_CannotGenerateShiftCalendarNextRecsExist"));
				}
			}
			//END OF DB OPERATION

			return result;
		}

		public VM_ShiftCalendarElement GetShiftCalendarElement(ModelStateDictionary modelState,VM_ShiftCalendarElement viewModel)
		{
			VM_ShiftCalendarElement returnValueVm = new VM_ShiftCalendarElement();

			//VALIDATE ENTRY PARAMETERS
			if (viewModel == null)
			{
				AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
			}
			if (viewModel.ShiftCalendarId <= 0)
			{
				AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
			}
			if (!modelState.IsValid)
			{
				return returnValueVm;
			}
			//END OF VALIDATION

			//DB OPERATION

			using (PEContext uow = new PEContext())
			{
				V_ShiftCalendar shiftCalendar = uow.V_ShiftCalendar.Where(z => z.ShiftCalendarId == viewModel.ShiftCalendarId).Single();
				if (shiftCalendar != null)
				{
					returnValueVm = new VM_ShiftCalendarElement(shiftCalendar);
					return returnValueVm;
				}
				else
					return viewModel;
			}
			// END OF DB OPERATION 


		}

		public VM_LongId InsertShiftCalendar(ModelStateDictionary modelState, VM_ShiftCalendarElement viewModel)
		{
			VM_LongId returnValueVm = null;
			//VALIDATE ENTRY PARAMETERS
			if (viewModel == null)
			{
				AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
			}
			if (!modelState.IsValid)
			{
				return returnValueVm;
			}
			//END OF VALIDATION 

			//DB OPERATION
			using (PEContext uow = new PEContext())
			{

				ShiftCalendar shiftCalendar = new ShiftCalendar();
				shiftCalendar.PlannedStartTime = viewModel.Start.Date + this.GetPlannedStartTime(viewModel.ShiftTime);

        DaysOfYear doy = uow.DaysOfYears.Where(x => x.DateDay == shiftCalendar.PlannedStartTime.Date).Single();

        shiftCalendar.PlannedEndTime = viewModel.End.Date + this.GetPlannedEndTime(viewModel.ShiftTime);
				if (viewModel.ShiftTime == (long)PE.DbEntity.Enums.ShiftTime.Night)
					shiftCalendar.PlannedEndTime = shiftCalendar.PlannedEndTime.AddDays(1);
				shiftCalendar.CreatedTs = DateTime.Now;
				shiftCalendar.LastUpdateTs = DateTime.Now;
				//shiftCalendar.State = ObjectState.Added;
				shiftCalendar.FKCrewId = viewModel.CrewId;
				shiftCalendar.FKShiftDefinitionId = viewModel.ShiftTime;
        shiftCalendar.FKDaysOfYear = doy.DaysOfYearId;
        //shiftCalendar.FKDaysOfYear = 2087;
        uow.ShiftCalendars.Add(shiftCalendar);
				uow.SaveChanges();
				returnValueVm = new VM_LongId(shiftCalendar.ShiftCalendarId);
			}
			//END OF DB OPERATION

			return returnValueVm;
		}

		public DataSourceResult PrepareCrewData(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
		{
			DataSourceResult returnVm = null;
			List<VM_ScheduleItems> scheduleItems = new List<VM_ScheduleItems>();
			List<Crew> crews;

			using (PEContext uow = new PEContext())
			{
				crews = uow.Crews.ToList();
			}

			foreach (Crew c in crews)
			{
				VM_ScheduleItems tmp = new VM_ScheduleItems(c.CrewId, c.CrewName, "#" + ModuloColor.GetColor(c.CrewId));
				tmp.Name = c.CrewName;
				tmp.Id = c.CrewId;
				tmp.Color = "#" + ModuloColor.GetColor(c.CrewId);
				scheduleItems.Add(tmp);
			}
			returnVm = scheduleItems.ToDataSourceResult(request, modelState);
			return returnVm;
		}

		#endregion

		#region public methods
		public static List<ShiftDefinition> GetShiftDefinitionsList()
		{
			List<ShiftDefinition> shiftDefinitions = null;
			try
			{
				using (PEContext ctx = new PEContext())
				{
					shiftDefinitions = ctx.ShiftDefinitions.ToList();
					foreach(ShiftDefinition element in shiftDefinitions)
					{
						element.ShiftCode = VM_Resources.ResourceManager.GetString(string.Format("GLOB_ShiftType_{0}", element.ShiftDefinitionId));
					}
				}
			}
			catch (Exception ex)
			{
				DbExceptionResult result = DbExceptionHelper.ProcessException(ex, "GetShiftDefinitionsList::Database operation failed!", null);
			}
			return shiftDefinitions;
		}
		#endregion

		#region private methods
		private TimeSpan GetPlannedStartTime(long id)
		{
			TimeSpan plannedStartTime = new TimeSpan();
			
			using (PEContext uow = new PEContext())
			{
				ShiftDefinition shiftDefinition = uow.ShiftDefinitions.Where(z => z.ShiftDefinitionId == id).Single();
				if (shiftDefinition != null)
				{
					plannedStartTime = shiftDefinition.DefaultStartTime;
				}
			}
			return plannedStartTime;
		}
		private TimeSpan GetPlannedEndTime(long id)
		{
			TimeSpan plannedEndTime = new TimeSpan();
			using (PEContext uow = new PEContext())
			{
				ShiftDefinition shiftDefinition = uow.ShiftDefinitions.Where(z => z.ShiftDefinitionId == id).Single();
				if (shiftDefinition != null)
				{
					plannedEndTime = shiftDefinition.DefaultEndTime;
				}
			}
			return plannedEndTime;
		}

		/// <summary>
		/// Checking shift calendar elements are existing in previous week. 
		/// </summary>
		/// <param name="shiftCalendarElementsInPrevoiusWeek">Shift calendar list in previous week.</param>
		/// <returns>Return true where shift calendar elements existed for previous week, if some missing, return false.</returns>
		private bool CheckShiftCalendarsData(List<ShiftCalendar> shiftCalendarElementsInPrevoiusWeek)
		{
			bool result = true;

			DateTime LastValidDayElement = DateTime.Now.AddDays(-1);        // Take yesterday,
			DateTime FirstValidDayElement = DateTime.Now.AddDays(-7);				// take day from one week ago,
																																			// and build list days between them.
			List<DateTime> ValidListOfDays = new List<DateTime>();					// Fist of valid days in whole last week,
																																			// fill rest of days between ranges.
			for (DateTime dt = FirstValidDayElement; dt <= LastValidDayElement; dt = dt.AddDays(1))
			{
				ValidListOfDays.Add(dt);
			}
			ValidListOfDays.OrderBy(z => z.Date);														// And sort by date ascending.
			List<ShiftCalendar> ListOfGroupedDays = new List<ShiftCalendar>();

			var GroupedElements = shiftCalendarElementsInPrevoiusWeek.GroupBy(u => u.PlannedStartTime.Date)
																															 .Select(grp => new { PlannedStartTime = grp.Key, shiftCalendarElementsInPrevoiusWeek = grp.ToList() })
																															 .ToList();
																																			
			foreach(DateTime element in ValidListOfDays)													// Check if elements of valid days list existing in grouped elements list.
			{
				try
				{
					var something = GroupedElements.First(z => z.PlannedStartTime.Date == element.Date); 
				}
				catch
				{
					result = false;																							//If something not exists in list, set result to false.
				}
			}
			return result;
		}
		#endregion
	}
}
