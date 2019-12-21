using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.DBAdapter;
using PE.Interfaces.Handlers;
using PE.L3S.Handlers;
using PE.Lite.L3Sim.Communication;
using SMF.Module.Core;
using SMF.Module.Limit;
using SMF.Module.Parameter;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PE.Lite.L3Sim.Module
{
  public sealed class WorkOrderSimulator
  {
    #region members
    private DCL3L2WorkOrderDefinitionExt workOrderMessage { get; set; }
    private DateTime simulationSendingWorkOrderTime { get; set; }
    private int shiftTime { get; set; }
    private PEContext ctx { get; set; }
    private MaterialShapeType materialType { get; set; }
    private int numberOfGeneratedWorkOrderTelegrams { get; set; }
    private int limitWeightMin { get; set; }
    private int limitWeightMax { get; set; }
    private int limitMaterialsMin { get; set; }
    private int limitMaterialsMax { get; set; }

    private int shapeTypeChangeBy { get; set; }
    private int geneartionDelayTime { get; set; }
    private int updateNotGenerateWoBy { get; set; }
    private bool isActuallyRunning { get; set; }

    private readonly IL3WorkOrderSimulatorHanlder _l3WorkOrderSimulatorHanlder;
    #endregion

    #region ctor
    public WorkOrderSimulator()
    {
      ctx = new PEContext();
      workOrderMessage = new DCL3L2WorkOrderDefinitionExt();
      simulationSendingWorkOrderTime = DateTime.Now;
      isActuallyRunning = false;

      numberOfGeneratedWorkOrderTelegrams = 1;

      InitParamsAndLimits();
      SetNewRawMaterialType();
      _l3WorkOrderSimulatorHanlder = new L3WorkOrderSimulatorHanlder();
    }

    internal void InitParamsAndLimits()
    {
      shapeTypeChangeBy = ParameterController.GetParameter("SIM_L3ShapeTypeChangeBy").ValueInt.GetValueOrDefault();
      geneartionDelayTime = ParameterController.GetParameter("SIM_L3DelayPerWOTime").ValueInt.GetValueOrDefault();
      updateNotGenerateWoBy = ParameterController.GetParameter("SIM_L3UpdateNotChangeBy").ValueInt.GetValueOrDefault();
      shiftTime = ParameterController.GetParameter("SIM_L3WorkOrderTimerSender").ValueInt.GetValueOrDefault();
      limitWeightMin = LimitController.GetLimit("SIM_L3WorkOrderWeight").LowerValueInt.GetValueOrDefault();
      limitWeightMax = LimitController.GetLimit("SIM_L3WorkOrderWeight").UpperValueInt.GetValueOrDefault();
      limitMaterialsMin = LimitController.GetLimit("SIM_L3WorkOrderNumMaterials").LowerValueInt.GetValueOrDefault();
      limitMaterialsMax = LimitController.GetLimit("SIM_L3WorkOrderNumMaterials").UpperValueInt.GetValueOrDefault();
    }
    #endregion

    /// <summary>
    /// Every <see cref=">shapeTypeChangeBy"/> MaterialShapeType have to be changed
    /// </summary>
    private void SetNewRawMaterialType()
    {
      if (numberOfGeneratedWorkOrderTelegrams % shapeTypeChangeBy == 0)
      {
        Array array = Enum.GetValues(typeof(MaterialShapeType));
        materialType = (MaterialShapeType)array.GetValue(new Random().Next(array.Length));
      }
    }

    /// <summary>
    /// Generation of telegram main function
    /// </summary>
    public async void Run()
    {
      if (DateTime.Now > simulationSendingWorkOrderTime && !isActuallyRunning)
      {
        isActuallyRunning = true;
        bool isNeededScheduledWorkOrderNeeded = true;
        System.Collections.Generic.List<long> workOrdersInSchedule = ctx.PPLSchedules.Where(w => w.ScheduleStatus == OrderStatus.ENUM_Scheduled).Select(s => s.FKWorkOrderId).ToList();
        foreach (long workOrder in workOrdersInSchedule)
        {
          if (ctx.PRMMaterials.Where(w => w.FKWorkOrderId == workOrder && w.IsAssigned == false).Any())
          {
            isNeededScheduledWorkOrderNeeded = false;
            break;
          }
        }

        if (isNeededScheduledWorkOrderNeeded)
        //if (ctx.PRMMaterials.Where(w => w.MaterialStatus == RawMaterialStatus.Unassigned).Count() <= 1)
        {
          simulationSendingWorkOrderTime.AddSeconds(shiftTime);
          //As PO demand every updateNotGenerateWoBy.Value telegram will be sended prev generated with changed CommStatus
          if (numberOfGeneratedWorkOrderTelegrams % updateNotGenerateWoBy != 0)
          {
            PrepareWorkOrderMessage();
          }
          else
          {
            workOrderMessage.CommStatus = (short)CommStatus.ENUM_COMMSTATUS_ProcessingError;
          }

          //sending to ADP
          SendOffice.SendWorkOrderDataToPRM(workOrderMessage);

          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine();
          Console.WriteLine(workOrderMessage.ToString());
          Console.WriteLine();
          Console.ResetColor();

          //reset of iteration counter
          if (numberOfGeneratedWorkOrderTelegrams == updateNotGenerateWoBy * shapeTypeChangeBy)
          {
            numberOfGeneratedWorkOrderTelegrams = 1;
          }
          else
          {
            numberOfGeneratedWorkOrderTelegrams++;
          }
          await Task.Delay(geneartionDelayTime);
          isActuallyRunning = false; 
        }
      }
    }
    /// <summary>
    /// Prepare Ext  telegram 
    /// </summary>
    private void PrepareWorkOrderMessage()
    {

      SetNewRawMaterialType();


      try
      {
        workOrderMessage = _l3WorkOrderSimulatorHanlder.CreateSimulationWorkOrderMessage(ctx, RandWorkOrderWeight(), limitMaterialsMin, limitMaterialsMax, materialType);
        ModuleController.InitDataContract(workOrderMessage);
      }
      catch (Exception e)
      {

        switch (e.Source)
        {
          case "ConstructHeatName":
          case "GetDefaultCustomerName":
          case "GetDefaultProductCatalogueName":
            {
              ModuleController.Logger.Error("Error in L3WorkOrderSimylationHandler - " + e.Source + " " + e.Message);
              break;
            }
        }
      }
    }

    /// <summary>
    /// Helper to rand WO weight from given limits
    /// </summary>
    /// <returns></returns>
    private int RandWorkOrderWeight()
    {
      return new Random().Next(limitWeightMin, limitWeightMax);
    }


  }
}
