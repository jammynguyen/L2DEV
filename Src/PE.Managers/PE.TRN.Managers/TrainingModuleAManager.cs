using PE.DbEntity.Models;
using PE.DTO.Internal.System;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.TRN.Managers
{
	public class TrainingModuleAManager : ManagerRoot
	{
		public virtual async Task<DCTestData> GetItemsFromModuleA(DCTestData dc)
		{
			DCTestData result = new DCTestData();

			try
			{
				using (PEContext ctx = new PEContext())
				{
					//var scheduleItem = _scheduleHandler.GetScheduleItemById(ctx, dc.ScheduleId);
					//var scheduleItemToSwap = _scheduleHandler.GetScheduleItemByOrderSeqNum(ctx, scheduleItem.OrderSeq, dc.Direction);

					//// Save changes
					//await ctx.SaveChangesAsync();

					//ModuleController.HmiRefresh(HMIRefreshKeys.Schedule);
				}
			}
			catch (Exception)
			{
				//NotificationController.RegisterAlarm(Defs.DeleteItemFromSchedule, $"Critical error when deleting item from schedule [scheduleId: {dc.ScheduleId}, workOrderId: {dc.WorkOrderId}]", dc.ScheduleId, dc.WorkOrderId);
				//ModuleController.Logger.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
				//throw new ModuleMessageException(ModuleController.ModuleName, Defs.DeleteItemFromSchedule, $"Critical error when deleting item from schedule[scheduleId: { dc.ScheduleId }, workOrderId: { dc.WorkOrderId}]", dc.ScheduleId, dc.WorkOrderId);
			}

			return result;
		}
	}
}
