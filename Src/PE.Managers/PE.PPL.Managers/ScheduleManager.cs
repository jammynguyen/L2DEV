using PE.Common;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using PE.DTO.Internal.Schedule;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Managers;
using PE.PPL.Handlers;
using PE.PRM.Handlers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PE.PPL.Managers
{
  public class ScheduleManager: IScheduleManager
  {
    #region members

    private readonly IProductionPlanningSendOffice _sendOffice;

    #endregion
    #region handlers

    private readonly ScheduleHandler _scheduleHandler;
    private readonly WorkOrderHandler _workOrderHandler;
    private readonly HeatHandler _heatHandler;
    private readonly MaterialHandler _materialHandler;

    #endregion
    #region ctor
    public ScheduleManager(IProductionPlanningSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _scheduleHandler = new ScheduleHandler();
      _workOrderHandler = new WorkOrderHandler();
      _heatHandler = new HeatHandler();
      _materialHandler = new MaterialHandler();
    }
    #endregion

    #region func
    public virtual async Task<DataContractBase>  AddWorkOrderToScheduleAsync(DCWorkOrderToSchedule dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          PRMWorkOrder workOrder = await _workOrderHandler.GetWorkOrderByIdAsync(ctx, dc.WorkOrderId);
          long plannedTime = _scheduleHandler.EstimateWorkOrderTime(workOrder.TargetOrderWeight, workOrder?.PRMProductCatalogue?.StdProductivity);
          DateTime plannedStartTime = _scheduleHandler.GetLatestPlannedEndTime(ctx);

          PPLSchedule scheduleItem = new PPLSchedule()
          {
            CreatedTs = DateTime.Now,
						LastUpdateTs = DateTime.Now,
            FKWorkOrderId = dc.WorkOrderId,
            OrderSeq = Convert.ToInt16(_scheduleHandler.GetLastOrderSeqNumber(ctx) + 1),
            PlannedTime = plannedTime,
            PlannedStartTime = plannedStartTime,
            PlannedEndTime = plannedStartTime.Add(TimeSpan.FromSeconds(plannedTime)),
          };

          _scheduleHandler.AddItemToSchedule(ctx, scheduleItem);
          await _workOrderHandler.UpdateWorkOrderStatusAsync(ctx, workOrder.WorkOrderId, OrderStatus.ENUM_Scheduled, true, DateTime.Now);
          await ctx.SaveChangesAsync();
          await ModuleController.HmiRefresh(HMIRefreshKeys.Schedule);
        }
      }
      catch (OverflowException)
      {
        NotificationController.RegisterAlarm(Defs.ToManyItemsInSchedule, $"Critical error when adding work order [id:{dc.WorkOrderId}] to schedule", dc.WorkOrderId);
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.ToManyItemsInSchedule, $"Critical error when adding work order [id:{dc.WorkOrderId}] to schedule", dc.WorkOrderId);
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.WorkOrderNotAddedToSchedule, $"Critical error when adding work order [id:{dc.WorkOrderId}] to schedule", dc.WorkOrderId);
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.WorkOrderNotAddedToSchedule, $"Critical error when adding work order [id:{dc.WorkOrderId}] to schedule", dc.WorkOrderId);
      }

      return result;
    }

    public virtual async Task<DataContractBase> AddTestWorkOrderToScheduleAsync(DCTestSchedule dc)
    {
      DataContractBase result = new DataContractBase();
      PEContext ctx = new PEContext();

      try
      {
        if (dc.NoOfmaterials == null || (dc.NoOfmaterials.HasValue && dc.NoOfmaterials <= 0))
        {
          throw new ArgumentException();
        }

        PPLSchedule schedule = await _scheduleHandler.GetScheduleItemByIdAsync(ctx, dc.ScheduleId.Value, true);

        PRMHeat heat = _heatHandler.CreateTestHeat(schedule?.PRMWorkOrder.PRMMaterials.OfType<PRMMaterial>().SingleOrDefault().PRMHeat);

        ctx.PRMHeats.Add(heat);

        double weight = (double)(dc.PlannedWeight / dc.NoOfmaterials);

        List<PRMMaterial> materials = _materialHandler.CreateTestMaterials(ctx, schedule.PRMWorkOrder.PRMMaterialCatalogue, (short)dc.NoOfmaterials, heat, weight);

        PRMWorkOrder workOrder = _workOrderHandler.CreateWorkOrder(ctx,
                                                                    true,
                                                                    dc.PlannedWeight,
                                                                    materials,
                                                                    heat,
                                                                    schedule.PRMWorkOrder.PRMMaterialCatalogue,
                                                                    schedule.PRMWorkOrder.FKProductCatalogueId,
                                                                    schedule.PRMWorkOrder.PRMCustomer,
                                                                    DateTime.Now,
                                                                    null,
                                                                    DateTime.Now,
                                                                    dc.PlannedWeight,
                                                                    dc.PlannedWeight,
                                                                    "Test",
                                                                    "Test",
                                                                    schedule.PRMWorkOrder.FKReheatingGroupId,
                                                                    "Test",
                                                                    "Test",
                                                                    "Test"
                                                                    );
					
					
        ctx.PRMWorkOrders.Add(workOrder);

        long plannedTime = _scheduleHandler.EstimateWorkOrderTime(workOrder.TargetOrderWeight, workOrder?.PRMProductCatalogue?.StdProductivity);
        DateTime plannedStartTime = _scheduleHandler.GetLatestPlannedEndTime(ctx);

        PPLSchedule scheduleItem = new PPLSchedule()
        {
          CreatedTs = DateTime.Now,
          LastUpdateTs = DateTime.Now,
					PRMWorkOrder = workOrder,
          OrderSeq = Convert.ToInt16(_scheduleHandler.GetLastOrderSeqNumber(ctx) + 1),
          PlannedTime = plannedTime,
          PlannedStartTime = plannedStartTime,
          PlannedEndTime = plannedStartTime.Add(TimeSpan.FromSeconds(plannedTime)),
        };

        _scheduleHandler.AddItemToSchedule(ctx, scheduleItem);

        await ctx.SaveChangesAsync();
        await ModuleController.HmiRefresh(HMIRefreshKeys.Schedule);
      }
      catch (ArgumentException)
      {
        NotificationController.RegisterAlarm(Defs.ImproperNumberOfMaterials, "Given number of materials in impropper");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed - number of test materials less or equal to 0");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.ImproperNumberOfMaterials, "Given number of materials in impropper");
      }
      catch (DbEntityException)
      {
        NotificationController.RegisterAlarm(Defs.SavingError, "Critical error appears while test work order was beeing saved into DB - changes not saved");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.SavingError, "Critical error appears while test work order was beeing saved into DB - changes not saved");
      }
      catch (Exception e)
      {
        switch (e.Source)
        {
          case "CreateTestHeat":
            {
              NotificationController.Error(e.Source + " error");
              break;
            }
          case "CreateTestMaterial":
          case "CreateTestSchedule":
            {
              NotificationController.Error(e.Source + " missing argument Heat or WorkOrder or DCTestSchedule");
              break;
            }
        }

        NotificationController.RegisterAlarm(Defs.CreateTestWorkOrder, "Error during processing create test work order function - unexpected error may happend");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CreateTestWorkOrder, "Failed during processing create test work order function - unexpected error may happend");
      }
      return result;
    }

    public virtual async Task<DataContractBase> MoveItemInScheduleAsync(DCWorkOrderToSchedule dc)
    {
      DataContractBase result = new DataContractBase();

			//throw new Exception("Test text");
			//throw new ModuleMessageException(ModuleController.ModuleName, Defs.DeleteItemFromSchedule, $"Critical error when deleting item from schedule[scheduleId: { dc.ScheduleId }, workOrderId: { dc.WorkOrderId}]", dc.ScheduleId, dc.WorkOrderId);
			//result.AddWarningMessage(Defs.NoItemToSwap, $"Warning - can't swap items. No item to change position near position {22}", 222);

			try
			{
        using (PEContext ctx = new PEContext())
        {
          PPLSchedule scheduleItem = await _scheduleHandler.GetScheduleItemByIdAsync(ctx, dc.ScheduleId);
          PPLSchedule scheduleItemToSwap = await _scheduleHandler.GetNextScheduleItemByOrderSeqNumAsync(ctx, scheduleItem.OrderSeq, dc.Direction);
          if (scheduleItemToSwap == null)
          {
            result.AddWarningMessage(Defs.NoItemToSwap, $"Warning - can't swap items. No item to change position near position {scheduleItem.OrderSeq}", scheduleItem.OrderSeq);
          }
          else
          {
            ctx.PPLSchedules.Remove(scheduleItem);
            ctx.PPLSchedules.Remove(scheduleItemToSwap);
            await ctx.SaveChangesAsync();

            short tmp = scheduleItemToSwap.OrderSeq;
            scheduleItemToSwap.OrderSeq = scheduleItem.OrderSeq;
            scheduleItem.OrderSeq = tmp;
            //---
            // planned time change
            if (dc.Direction == ScheduleMoveOperator.Up)
            {
              scheduleItem.PlannedStartTime = scheduleItemToSwap.PlannedStartTime;
              scheduleItem.PlannedEndTime = scheduleItem.PlannedStartTime.Add(TimeSpan.FromSeconds(scheduleItem.PlannedTime));
              scheduleItemToSwap.PlannedStartTime = scheduleItem.PlannedEndTime;
              scheduleItemToSwap.PlannedEndTime = scheduleItemToSwap.PlannedStartTime.Add(TimeSpan.FromSeconds(scheduleItemToSwap.PlannedTime));
            }
            else
            {
              scheduleItemToSwap.PlannedStartTime = scheduleItem.PlannedStartTime;
              scheduleItemToSwap.PlannedEndTime = scheduleItemToSwap.PlannedStartTime.Add(TimeSpan.FromSeconds(scheduleItemToSwap.PlannedTime));
              scheduleItem.PlannedStartTime = scheduleItem.PlannedEndTime;
              scheduleItem.PlannedEndTime = scheduleItem.PlannedStartTime.Add(TimeSpan.FromSeconds(scheduleItem.PlannedTime));
            }
          }

          ctx.PPLSchedules.Add(scheduleItem);
          ctx.PPLSchedules.Add(scheduleItemToSwap);


          await ctx.SaveChangesAsync();

          await ModuleController.HmiRefresh(HMIRefreshKeys.Schedule);
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.MoveItemsInScheduleError, $"Critical error when moving items in schedule [scheduleId: {dc.ScheduleId}, workOrderId: {dc.WorkOrderId}]", dc.ScheduleId, dc.WorkOrderId);
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
				throw new ModuleMessageException(ModuleController.ModuleName, Defs.MoveItemsInScheduleError, $"Critical error when moving items in schedule [scheduleId: { dc.ScheduleId }, workOrderId: { dc.WorkOrderId}]", dc.ScheduleId, dc.WorkOrderId);
      }

      return result;
    }

    public virtual async Task<DataContractBase> RemoveItemFromScheduleAsync(DCWorkOrderToSchedule dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          await _scheduleHandler.DeleteItemByIdAsync(ctx, dc.ScheduleId);
          await ctx.SaveChangesAsync();
          _scheduleHandler.UpdateOrderSeqAfterDelete(ctx, dc.OrderSeq);
          await _workOrderHandler.UpdateWorkOrderStatusAsync(ctx, dc.WorkOrderId, OrderStatus.ENUM_Cancelled, false);

          await ctx.SaveChangesAsync();
          await ModuleController.HmiRefresh(HMIRefreshKeys.Schedule);
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.DeleteItemFromSchedule, $"Critical error when deleting item from schedule [scheduleId: {dc.ScheduleId}, workOrderId: {dc.WorkOrderId}]", dc.ScheduleId, dc.WorkOrderId);
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.DeleteItemFromSchedule, $"Critical error when deleting item from schedule[scheduleId: { dc.ScheduleId }, workOrderId: { dc.WorkOrderId}]", dc.ScheduleId, dc.WorkOrderId);
      }

      return result;
    }
    //public virtual async Task<DataContractBase> ProcessTriggerAsync(DCTriggerData message)
    //{
    //  DataContractBase result = new DataContractBase();

    //  Console.ForegroundColor = ConsoleColor.Blue;
    //  Console.WriteLine($"Trigger {message.triggerType.ToString()} for material {message.materialId}");
    //  Console.ResetColor();

    //  switch (message.triggerType)
    //  {
    //    case TriggerType.Charge:
    //      {
    //        await Task.Delay(1);
    //        break;
    //      }
    //    case TriggerType.UnCharge:
    //      {
    //        throw new NotImplementedException();
    //      }
    //    case TriggerType.Discharge:
    //      {
    //        break;
    //      }
    //    case TriggerType.UnDischarge:
    //      {
    //        throw new NotImplementedException();
    //      }
    //    default:
    //      {
    //        NotificationController.Info("Trigger not registered for this module");
    //        break;
    //      }
    //  }


    //  return result;
    //}

    /// <summary>
    /// Processing of Request about next material in schedule when <see cref=PE.DbEntity.Enums.TriggerType.Charge"/> happend
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public virtual async Task<DCL1L3MaterialConnector> RequestL3MaterialForRawMaterialAsync(DCFeatureRelatedOperationData data)
    {
      DCL1L3MaterialConnector result = null;

      try
      {
        using (PEContext ctx = new PEContext())
        {
          PRMMaterial nextMaterial = await _scheduleHandler.GetFirstUnasignedMaterialFromSchedule(ctx, data.RawMaterialId);
          await ctx.SaveChangesAsync();

          if (nextMaterial != null)
          {
            NotificationController.Info($"Next L3 material to be assigned to RawMaterial: {nextMaterial.MaterialName}({nextMaterial.MaterialId})");
            result = new DCL1L3MaterialConnector() { PRMMaterialId = nextMaterial.MaterialId, RawMaterialId = data.RawMaterialId };
          }
          else
            NotificationController.Error("Forwarding RequestFirstMaterialInProductionScheduleAsync - error");
        }
      }
      catch (InvalidOperationException)
      {
        NotificationController.RegisterAlarm(Defs.EmptySchedule, $"GetScheduleOfWorkOrders failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.RequestFirstMaterialInProductionSchedule, $"RequestFirstMaterialInProductionSchedule thrown unexpected error");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }

      return result;

    }

    public virtual async Task<DataContractBase> RemoveFinishedOrdersFromScheduleAsync(DataContractBase message)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          await _scheduleHandler.RemoveFinishedWorkOrdersFromSchedule(ctx);
          await ctx.SaveChangesAsync();
        }
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.RequestFirstMaterialInProductionSchedule, $"RemoveFinishedOrdersFromScheduleAsync thrown unexpected error");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }
      return result;
    }

    #endregion

    #region private

    #endregion
  }
}
