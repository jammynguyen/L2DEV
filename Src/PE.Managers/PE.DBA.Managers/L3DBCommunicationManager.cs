using PE.Common;
using PE.DBA.Handlers;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.External.DBAdapter;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Managers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.DBA.Managers
{
  public class L3DBCommunicationManager : IL3DBCommunicationManager
  {
    #region members

    private readonly IDbAdapterSendOffice _sendOffice;

    #endregion
    #region handlers

    private readonly DataTransferHandler _dataTransferHandler;

    #endregion
    #region ctor
    public L3DBCommunicationManager(IDbAdapterSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _dataTransferHandler = new DataTransferHandler();
    }


    #endregion
    #region interface

    public virtual async Task TransferDataFromTransferTableToAdapterAsync()
    {
      using (PEContext ctx = new PEContext())
      {
        try
        {
          IEnumerable<DTO.Internal.DBAdapter.DCL3L2WorkOrderDefinitionExt> dcList = await _dataTransferHandler.ExtractDataFromTransferTableAsync(ctx);

          foreach (DTO.Internal.DBAdapter.DCL3L2WorkOrderDefinitionExt dc in dcList)
          {
            // Send to adapter

            SendOfficeResult<DCWorkOrderStatusExt> result = await _sendOffice.SendWorkOrderDataToAdapterAsync(dc);

            if (result.OperationSuccess)
            {
              NotificationController.Info(String.Format("Work Order: {0} sent to adapter successfully", dc.WorkOrderName));

              try
              {
                await _dataTransferHandler.UpdateTransferTableCommStatusesAsync(ctx, result.DataConctract);
              }
              catch { }
            }         
            else
              NotificationController.Error("Error during sending Work Order to adapter");


          }
        }
        catch (Exception e)
        {
          NotificationController.RegisterAlarm(Defs.AC_TransferDataFromTransferTableToAdapter, "Error of processing L3 Transfer Table");
          NotificationController.Error($"Error while transfering data from TransferTable to ADP {e.Source}");
        }
        await ctx.SaveChangesAsync();
      }
      await ModuleController.HmiRefresh(HMIRefreshKeys.L3TransferTable);
    }
    public virtual async Task UpdateWorkOrdesWithTimeoutAsync()
    {
      using (PEContext ctx = new PEContext())
      {
        try
        {
          await _dataTransferHandler.UpdateWorkOrdesWithTimeoutAsync(ctx);
          await ctx.SaveChangesAsync();
        }
        catch (Exception e)
        {
          NotificationController.RegisterAlarm(Defs.DBA_Timeout, "Error while updating WorkOrders with timeout in transfer table");
          NotificationController.Error($"Error while updating WorkOrders with timeout in transfer table {e.Source}");
        }
      }
      await ModuleController.HmiRefresh(HMIRefreshKeys.L3TransferTable);
      return;
    }
    //public async Task UpdateTransferTableCommStatusesAsync(DCWorkOrderStatusExt dataToSend)
    //{
    //  using (PEContext ctx = new PEContext())
    //  {
    //    try
    //    {
    //      await _dataTransferHandler.UpdateTransferTableCommStatusesAsync(ctx, dataToSend);
    //      await ctx.SaveChangesAsync();
    //    }
    //    catch (Exception e)
    //    {
    //      NotificationController.RegisterAlarm(Defs.DBA_Timeout, "Error while updating WorkOrders with timeout in transfer table");
    //      NotificationController.Error($"Error while updating WorkOrders with timeout in transfer table {e.Source}");
    //      throw new Exception("Error while updating WorkOrders with timeout in transfer table", e);
    //    }
    //  }
    //}

    #endregion
  }
}
