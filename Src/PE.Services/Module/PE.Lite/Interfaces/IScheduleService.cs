using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Schedule;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IScheduleService
  {
    DataSourceResult GetSchedule(ModelStateDictionary modelState, DataSourceRequest request);
    Task<VM_Base> AddWorkOrderToSchedule(ModelStateDictionary modelState, long workOrderId);
    Task<VM_Base> RemoveItemFromSchedule(ModelStateDictionary modelState, long scheduleId, long workOrderId, short orderSeq);
    Task<VM_Base> MoveItemInSchedule(ModelStateDictionary modelState, long scheduleId, long workOrderId, short direction);
    VM_Schedule GetScheduleData(ModelStateDictionary modelState, long scheduleId);
    Task<VM_Base> CreateTestSchedule(ModelStateDictionary modelState, VM_Schedule viewModel);
    VM_WorkOrderSummary PreparePratialForSchedule(ModelStateDictionary modelState);

  }
}
