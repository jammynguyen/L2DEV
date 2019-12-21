using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.DTO.Internal.DBAdapter;
using PE.Interfaces.Interfaces.Managers;
using PE.Interfaces.SendOffice;
using PE.L3S.Handlers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;

namespace PE.SIM.Managers
{
  public class L3SimulationManager: ILevel3SimulationManager
  {
    #region members

    private ISimulationSendOffice _sendOffice;
    private bool _isActuallyRunning;

    #endregion
    #region handlers

    private L3SimulationHandler _l3SimulationHandler;

    #endregion
    #region ctor
    public L3SimulationManager(ISimulationSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _l3SimulationHandler = new L3SimulationHandler();
     
    }


    #endregion
    #region interface

    public async Task CreateWorkOrders(int limitWeightMin, int limitWeightMax, int materialMin, int materialMax, int delayBetweenOrders)
    {
      //short s = RandNoMaterialsInWorkOrder(materialMin, materialMax);

      if (delayBetweenOrders < 1000)
        delayBetweenOrders = 1000;
    
      if (!_isActuallyRunning)
      {
        
        _isActuallyRunning = true;
        try
        {

          using (PEContext ctx = new PEContext())
          {
            bool scheduleIsEmpty = await _l3SimulationHandler.CheckIfNewOrdersHaveToBeGeneratedAsync(ctx);
            if (scheduleIsEmpty)
            {
              NotificationController.Info("Schedule is EMPTY - new orders will be created");

              int noOfOrders = new Random().Next(5, 10);
              string shapeType = await _l3SimulationHandler.GetRandomMaterialTypeAsync(ctx);

              NotificationController.Info(String.Format("-----------------------------------------------"));
              NotificationController.Info(String.Format("Generating {0} orders, Raw material shape: {1} ", noOfOrders, shapeType));
              NotificationController.Info(String.Format("-----------------------------------------------"));


              for (int i = 0; i < noOfOrders; i++)
              {
                L3L2WorkOrderDefinition workOrder = null;

                workOrder = _l3SimulationHandler.CreateL3WorkOrder(ctx, shapeType, limitWeightMin, limitWeightMax, RandNoMaterialsInWorkOrder(materialMin, materialMax));

                NotificationController.Info(String.Format("Generated WorkOrder [{0} of {1}]: Name: {2}, no of materials: {3}, product: {4}", i, noOfOrders, workOrder.WorkOrderName, workOrder.RawMaterialNumber, workOrder.RawMaterialShapeType));

                _l3SimulationHandler.StoreL3WorkOrderInTransferTable(ctx, workOrder);

                await ctx.SaveChangesAsync();

                await Task.Delay(delayBetweenOrders);
              }
            }
						else
              NotificationController.Info("Some work orders are available in schedule, generation of new not necessary.");
          }

        }
        catch (DbEntityValidationException e)
        {
          foreach (DbEntityValidationResult eve in e.EntityValidationErrors)
          {
            NotificationController.Error( string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
            foreach (DbValidationError ve in eve.ValidationErrors)
            {
              NotificationController.Error(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
            }
          }
          throw;
        }
        catch (Exception e)
        {
          throw new Exception("Internal error", e);
        }
        finally { _isActuallyRunning = false; }
      }
    }
    
		#endregion
    #region private methods

    private static short RandNoMaterialsInWorkOrder(int materialMin, int materialMax)
    {
      return (short)new Random().Next(materialMin, materialMax);
    }

    #endregion
  }
}
