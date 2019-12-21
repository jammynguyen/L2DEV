using PE.DTO.Internal.Adapter;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Managers;
using SMF.Module.Core;
using SMF.Module.Notification;
using System.Threading.Tasks;

namespace PE.ADP.Managers
{
  public class L3CommunicationManager : IL3CommunicationManager
  {
    #region members

    private readonly IAdapterL3SendOffice _sendOffice;

    #endregion
    public L3CommunicationManager(IAdapterL3SendOffice sendOffice)
    {
      _sendOffice = sendOffice;
    }


    public virtual async Task<DCWorkOrderStatus> ProccesExtWorkOrderMessageAsync(DCL3L2WorkOrderDefinition message)
    {
      // Send to prod manager
      SendOfficeResult<DCWorkOrderStatus> result = await _sendOffice.SendWorkOrderDataAsync(message);
      if (result.OperationSuccess)
      {
        NotificationController.Info("Forwarding WorkOrderData - success");
        NotificationController.Info(result.DataConctract?.GetDataContractLogText());
      }
      else
      {
        result.DataConctract.Status = DbEntity.Enums.CommStatus.ENUM_COMMSTATUS_New;
        result.DataConctract.BackMessage = "PRM not available";
      }

      return result.DataConctract;
    }
  }
}
