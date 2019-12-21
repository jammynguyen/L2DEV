using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.External.DBAdapter;
using PE.DTO.Internal.DBAdapter;
using SMF.Module.Core;
using SMF.Module.Notification;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;

namespace PE.DBA.Handlers
{
  public class DataTransferHandler
  {

    /// <summary>
    /// Extracts new data from Transfer Table and sets CommStatuses to BeingProcessed.
    /// </summary>
    /// <param name="ctx">PEContext instance</param>
    /// <returns>Collection of DCL3L2WorkOrderDefinitionExt</returns>
    public async Task<IEnumerable<DCL3L2WorkOrderDefinitionExt>> ExtractDataFromTransferTableAsync(PEContext ctx)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      IEnumerable<L3L2WorkOrderDefinition> toBeTransfered = await ctx.L3L2WorkOrderDefinition
                              .Where(x => x.CommStatus == CommStatus.ENUM_COMMSTATUS_New)
                              .OrderBy(x => x.CreatedInL3)
															.Take(2)
                              .ToListAsync();

      List<DCL3L2WorkOrderDefinitionExt> dcList = new List<DCL3L2WorkOrderDefinitionExt>();

      foreach (L3L2WorkOrderDefinition data in toBeTransfered)
      {
        data.CommStatus = CommStatus.ENUM_COMMSTATUS_BeingProcessed;
        data.UpdatedTs = DateTime.Now;
        data.WorkOrderNameStatus = data.CommStatus.ToString();

        NotificationController.Info(string.Format("Preparing WorkOrder {0}.", data.WorkOrderName));

        DCL3L2WorkOrderDefinitionExt dc = null;

        try
        {
          dc = new DCL3L2WorkOrderDefinitionExt()
          {
            Counter = data.CounterId,
            CreatedTs = data.CreatedTs,
            UpdatedTs = data.UpdatedTs,
            CommStatus = (short)data.CommStatus.Value,
            WorkOrderName = data.WorkOrderName,
            ProductName = data.ProductName,
            L3Created = data.CreatedInL3.Value,
            CustomerName = data.CustomerName,
            ToBeReadyBefore = data.ToBeCompletedBefore.Value,
            TargetOrderWeight = data.TargetOrderWeight.Value,
            TargetOrderWeightMin = data.TargetOrderWeightMin,
            TargetOrderWeightMax = data.TargetOrderWeightMax,
            HeatName = data.HeatName,
            RawMaterialNumber = data.RawMaterialNumber.Value,
            RawMaterialShapeType = data.RawMaterialShapeType,
            RawMaterialThickness = data.RawMaterialThickness.Value,
            RawMaterialWidth = data.RawMaterialWidth.Value,
            RawMaterialLength = data.RawMaterialLength.Value,
            RawMaterialWeight = data.RawMaterialWeight.Value,
            RawMaterialType = data.RawMaterialType,
            ExternalSteelGradeCode = data.ExternalSteelgradeCode,
            SteelGradeCode = data.SteelgradeCode,
            NextAggregate = data.NextAggregate,
            OperationCode = data.OperationCode,
            HeatingGroup = data.ReheatingGroupName,
            QualityPolicy = data.QualityPolicy,
            ExtraLabelInformation = data.ExtraLabelInformation,
            AmISimulated =data.WorkOrderName.Contains("Sim_")
          };
        }
        catch (Exception)
        {
          // In case of any errors during creating the DC
          NotificationController.Error($"[INVALID DB DATA] - {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} when creating DC");
        }

        if (dc != null)
        {
          dcList.Add(dc);
        }
      }

      return dcList;
    }

    public async Task UpdateTransferTableCommStatusesAsync(PEContext ctx, DCWorkOrderStatusExt workOrderProcessingResult)
    {
      L3L2WorkOrderDefinition target = await ctx.L3L2WorkOrderDefinition.SingleAsync(x => x.CounterId == workOrderProcessingResult.Counter);
      target.CommStatus = workOrderProcessingResult.Status;
      target.CommMessage = workOrderProcessingResult.BackMsg;
      target.WorkOrderNameStatus = workOrderProcessingResult.Status.ToString();

      NotificationController.Info(string.Format("Status changed for WorkOrder {0}.", target.WorkOrderName));
    }

    public async Task UpdateWorkOrdesWithTimeoutAsync(PEContext ctx)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      IEnumerable<L3L2WorkOrderDefinition> toBeUpdated = await ctx.L3L2WorkOrderDefinition
                  .Where(x => x.CommStatus == CommStatus.ENUM_COMMSTATUS_BeingProcessed && x.UpdatedTs < DbFunctions.AddSeconds(DateTime.Now, -120))
                  .OrderBy(x => x.CreatedInL3)
                  .ToListAsync();


      foreach (L3L2WorkOrderDefinition data in toBeUpdated)
      {
        data.CommStatus = CommStatus.ENUM_COMMSTATUS_New;
        data.UpdatedTs = DateTime.Now;
        data.WorkOrderNameStatus = data.CommStatus.ToString();

        NotificationController.Warn(string.Format("System detected timeout during processing WorkOrder {0}. Preparing data for resend.", data.WorkOrderName));
      }
      return;
    }

  }
}
