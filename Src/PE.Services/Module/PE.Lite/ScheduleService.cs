using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Schedule;
using PE.DTO.Internal.Schedule;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ScheduleService : BaseService, IScheduleService
  {
    public DataSourceResult GetSchedule(ModelStateDictionary modelState, DataSourceRequest request)
    {
      using (PEContext ctx = new PEContext())
      {
        IQueryable<V_WorkOrderSummary> scheduleList = ctx.V_WorkOrderSummary.Where(x => x.ScheduleId != null).OrderByDescending(x => x.ScheduleOrderSeq).AsQueryable();

        return scheduleList.ToDataSourceResult<V_WorkOrderSummary, VM_WorkOrderSummary>(request, modelState, data => new VM_WorkOrderSummary(data));
      }
    }

    public async Task<VM_Base> AddWorkOrderToSchedule(ModelStateDictionary modelState, long workOrderId)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      //prepare data to send
      DCWorkOrderToSchedule dataToSend = new DCWorkOrderToSchedule()
      {
        WorkOrderId = workOrderId
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AddWorkOrderToScheduleAsync(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;

    }

    public async Task<VM_Base> RemoveItemFromSchedule(ModelStateDictionary modelState, long scheduleId, long workOrderId, short orderSeq)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      //prepare data to send
      DCWorkOrderToSchedule dataToSend = new DCWorkOrderToSchedule()
      {
        WorkOrderId = workOrderId,
        ScheduleId = scheduleId,
        OrderSeq = orderSeq
      };


      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RemoveItemFromScheduleAsync(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> MoveItemInSchedule(ModelStateDictionary modelState, long scheduleId, long workOrderId, short direction)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      //prepare data to send
      DCWorkOrderToSchedule dataToSend = new DCWorkOrderToSchedule()
      {
        ScheduleId = scheduleId,
        Direction = (ScheduleMoveOperator)direction,
        WorkOrderId = workOrderId
      };


      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.MoveItemInScheduleAsync(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_Schedule GetScheduleData(ModelStateDictionary modelState, long scheduleId)
    {
      VM_Schedule result = null;

      if (scheduleId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      using (PEContext ctx = new PEContext())
      {
        PPLSchedule schedule = ctx.PPLSchedules.Where(w => w.ScheduleId == scheduleId)
          .Include(i => i.PRMWorkOrder)
          .Include(l => l.PRMWorkOrder.PRMMaterials)
          .Single();

        result = new VM_Schedule(schedule);
        result.PlannedWeight = schedule.PRMWorkOrder.TargetOrderWeight;
        result.NoOfmaterials = schedule.PRMWorkOrder.PRMMaterials.Count;
      }


      return result;
    }

    public async Task<VM_Base> CreateTestSchedule(ModelStateDictionary modelState, VM_Schedule viewModel)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      //prepare data to send
      UnitConverterHelper.ConvertToSi<VM_Schedule>(ref viewModel);

      DCTestSchedule dataToSend = new DCTestSchedule()
      {
        ScheduleId = viewModel.ScheduleId,
        OrderSeq = viewModel.OrderSeq,
        ScheduleStatus = viewModel.ScheduleStatus,
        PlannedWeight = viewModel.PlannedWeight,
        //Todo PlannedTime = viewModel.PlannedTime?.Ticks,
        StartTime = viewModel.StartTime,
        EndTime = viewModel.EndTime,
        PlannedStartTime = viewModel.PlannedStartTime,
        PlannedEndTime = viewModel.PlannedEndTime,
        NoOfmaterials = viewModel.NoOfmaterials
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AddTestScheduleAsync(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;

    }

    public VM_WorkOrderSummary PreparePratialForSchedule(ModelStateDictionary modelState)
    {
      VM_WorkOrderSummary result = null;
      return result;
    }
  }
}
