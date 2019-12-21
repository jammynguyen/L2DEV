using PE.Common;
using PE.DbEntity.Models;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using PE.DTO.Internal.Schedule;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Managers;
using PE.PRM.Handlers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.PRM.Managers
{
  public class WorkOrderManager : IWorkOrderManager
  {
    #region members

    private readonly IProdManagerWorkOrderSendOffice _sendOffice;

    #endregion
    #region handlers

    private readonly ProductCatalogueHandler _productCatalogueHandler;
    private readonly HeatHandler _heatHandler;
    private readonly SteelgradeHandler _steelgradeHandler;
    private readonly MaterialCatalogueHandler _materialCatalogueHandler;
    private readonly MaterialHandler _materialHandler;
    private readonly WorkOrderHandler _workOrderHandler;
    private readonly ReheatingHandler _reheatingHandler;
    private readonly CustomerHandler _customerHandler;

    #endregion
    #region ctor
    public WorkOrderManager(IProdManagerWorkOrderSendOffice sendOffice)
    {
      _sendOffice = sendOffice;


      _productCatalogueHandler = new ProductCatalogueHandler();
      _heatHandler = new HeatHandler();
      _steelgradeHandler = new SteelgradeHandler();
      _materialCatalogueHandler = new MaterialCatalogueHandler();
      _materialHandler = new MaterialHandler();
      _workOrderHandler = new WorkOrderHandler();
      _reheatingHandler = new ReheatingHandler();
      _customerHandler = new CustomerHandler();
    }

    #endregion

    #region func
    public virtual async Task<DCWorkOrderStatus> ProcessWorkOrderDataAsync(DCL3L2WorkOrderDefinition message)
    {
      DCWorkOrderStatus backMsg = new DCWorkOrderStatus()
      {
        Counter = message.Counter
      };

      if (message.RawMaterialNumber < 1)
        message.RawMaterialNumber = 1;

      double weight = message.TargetOrderWeight / message.RawMaterialNumber;

      // Work order creating
      try
      {
        using (PEContext ctx = new PEContext())
        {


          // Get product catalogue or throw an exception
          PRMProductCatalogue productCatalogue = await _productCatalogueHandler.ProcessGetProductCatalogueByNameForWorkOrderAsync(ctx, message.ProductName, backMsg);

          PRMSteelgrade steelgrade = await _steelgradeHandler.GetSteelgradeForWorkOrderProcessingAsync(ctx, message.SteelGradeCode, backMsg);

          PRMMaterialCatalogue materialCatalogue = await _materialCatalogueHandler.GetMaterialCatalogueByL3TelegramAsync(ctx, steelgrade.SteelgradeId, message);

          PRMHeat heat = await _heatHandler.GetHeatByNameAsync(ctx, message.HeatName);
          // if heat doesn't exist create new one
          if (heat == null)
          {
            heat = await _heatHandler.CreateNewHeatAsync(ctx, message.HeatName, materialCatalogue);
            ctx.PRMHeats.Add(heat);

            NotificationController.Info($"New heat: {heat.HeatName} creating...");
          }

          //if (heat.FKSteelgradeId != steelgrade.SteelgradeId)
          //{
          //  backMsg.BackMessage += "Inconsistent SteelGrade - Heat";
          //  NotificationController.Error("Inconsistent SteelGrade - Heat");
          //  throw new Exception("Inconsistent SteelGrade - Heat ionformation");
          //}

          

          if (materialCatalogue == null)
          {
            materialCatalogue = await _materialCatalogueHandler.CreateMaterialCatalogueAsync(ctx, steelgrade, message);
            NotificationController.Info($"New material catalogue: {materialCatalogue.MaterialCatalogueName} creating...");
          }

          List<PRMMaterial> materials = _materialHandler.CreateMaterials(ctx, materialCatalogue, message.RawMaterialNumber, heat, weight);

          PRMWorkOrder workOrder = await _workOrderHandler.GetWorkOrderByNameAsync(ctx, message.WorkOrderName);

          PRMCustomer customer = await _customerHandler.GetCustomerByNameOrCreateNewAsync(ctx, message.CustomerName, backMsg);

          PRMReheatingGroup reheatingGroup = await _reheatingHandler.GetReheatingByNameForWorkOrderProcessingAsync(ctx, message.HeatingGroup, backMsg);

          if (workOrder != null) // if work order with this name don't exists
          {
            NotificationController.Info($"Work order: {workOrder.WorkOrderName}  exists");
            if (workOrder.WorkOrderStatus <= DbEntity.Enums.OrderStatus.ENUM_Scheduled) // if work order can be updated
            {
              _materialHandler.DeleteOldMaterialsAfterWOUpdate(ctx, workOrder.WorkOrderId);
              workOrder = await _workOrderHandler.UpdateWorkOrderAsync(ctx, productCatalogue.ProductCatalogueId, customer, reheatingGroup.ReheatingGroupId, materialCatalogue, materials, heat, message);
              NotificationController.Info($"Work order: {workOrder.WorkOrderName}  updating...");
            }
            else
            {
              NotificationController.RegisterAlarm(Defs.WorkOrderNotUpdatable, $"Work order [id: {workOrder.WorkOrderId}] status don't allow it to update", workOrder.WorkOrderId);
              NotificationController.Warn($"Work order [id: {workOrder.WorkOrderId}, name: {workOrder.WorkOrderName}] status don't allow it to update");
              backMsg.Status = PE.DbEntity.Enums.CommStatus.ENUM_COMMSTATUS_ValidationError;
              return backMsg;
            }
          }
          else
          {
            workOrder = _workOrderHandler.CreateWorkOrder(ctx,
                                                                                                                    message.AmISimulated,
                                                          message.TargetOrderWeight,
                                                                                                                    materials,
                                                                                                                    heat,
                                                          materialCatalogue,
                                                          productCatalogue.ProductCatalogueId,
                                                                                                                    customer,
                                                                                                                    message.L3Created,
                                                                                                                    message.WorkOrderName,
                                                                                                                    message.ToBeReadyBefore,
                                                                                                                    message.TargetOrderWeightMin,
                                                                                                                    message.TargetOrderWeightMax,
                                                                                                                    message.QualityPolicy,
                                                                                                                    message.ExtraLabelInformation,
                                                          reheatingGroup.ReheatingGroupId,
                                                                                                                    message.NextAggregate,
                                                                                                                    message.OperationCode,
                                                                                                                    message.ExternalSteelGradeCode);
            // workOrder.PRMHeat = heat;
            ctx.PRMWorkOrders.Add(workOrder);
            NotificationController.Info($"Work order: {workOrder.WorkOrderName}  creating...");
          }

          backMsg.Status = PE.DbEntity.Enums.CommStatus.ENUM_COMMSTATUS_OK;
          await ctx.SaveChangesAsync();


          //--------------******************** S I M U L A T I O N ******************** ---------------------------------------
          if (message.AmISimulated == true)
          {
            SendOfficeResult<DataContractBase> result = await _sendOffice.AutoScheduleWorkOrderAsync(new DCWorkOrderToSchedule() { WorkOrderId = workOrder.WorkOrderId });

            if (result.OperationSuccess)
              NotificationController.Info("Forwarding work order to schedule module - success");
            else
              NotificationController.Error("Forwarding work order to schedule module - error. Work order will not be scheduled automatically");
          }


        }
      }
      catch (ArgumentNullException e)
      {
        NotificationController.RegisterAlarm(Defs.ProductCatalogueNotFound, $"Product Catalogue {message.ProductName} not found", message.ProductName);
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        NotificationController.LogException(e);
        backMsg.BackMessage += "Exception";
        backMsg.Status = PE.DbEntity.Enums.CommStatus.ENUM_COMMSTATUS_ProcessingError;
        throw new Exception("ProcessWorkOrderData processing exception", e);
      }
      catch (Exception e)
      {
        NotificationController.RegisterAlarm(Defs.WorkOrderNotCreated, $"Critical error when creating work order [name: {message.WorkOrderName}", message.WorkOrderName);
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        backMsg.BackMessage += "Exception";
        NotificationController.LogException(e);
        backMsg.Status = PE.DbEntity.Enums.CommStatus.ENUM_COMMSTATUS_ProcessingError;
        throw new Exception("ProcessWorkOrderData processing exception", e);
      }

      return backMsg;
    }

    public virtual async Task<DataContractBase> CreateWorkOrderAsync(DCWorkOrder dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          _workOrderHandler.CreateWorkOrderByUser(ctx, dc);

          await ctx.SaveChangesAsync();
          await ModuleController.HmiRefresh(HMIRefreshKeys.Schedule);
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.WorkOrderNotCreated, "Error while updating Work Order Catalogue");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.WorkOrderNotCreated, "Error while updating Work Order Catalogue");
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateWorkOrderAsync(DCWorkOrder dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          PRMWorkOrder workOrder = await _workOrderHandler.GetWorkOrderByIdAsync(ctx, dc.WorkOrderId);
          NotificationController.Info($"Work order: {workOrder.WorkOrderName}  exists");
          //if (workOrder.WorkOrderStatus <= DbEntity.Enums.OrderStatus.ENUM_Scheduled) // if work order can be updated
          //{
            _workOrderHandler.UpdateWorkOrderByUser(ctx, workOrder, dc);

            await ctx.SaveChangesAsync();
            await ModuleController.HmiRefresh(HMIRefreshKeys.Schedule);
          //}
          //else
          //{
          //  result.AddWarningMessage(Defs.WorkOrderNotUpdatable, $"Warning - if work id dc.WorkOrderId {dc.WorkOrderId} can be updated {dc.WorkOrderId}", dc.WorkOrderId);
          //  NotificationController.RegisterAlarm(Defs.WorkOrderNotUpdatable, $"Work order [id: {workOrder.WorkOrderId}] status don't allow it to update", workOrder.WorkOrderId);
          //  NotificationController.Warn($"Work order [id: {workOrder.WorkOrderId}, name: {workOrder.WorkOrderName}] status don't allow it to update");
          //}
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.WorkOrderNotCreated, "Error while updating Work Order Catalogue");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.WorkOrderNotCreated, "Error while updating Work Order Catalogue");
      }

      return result;
    }

    #endregion

    #region private

    #endregion

  }
}
