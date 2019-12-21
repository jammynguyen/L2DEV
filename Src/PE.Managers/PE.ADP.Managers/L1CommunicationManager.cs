using SMF.Core.DC;
using System.Threading.Tasks;
using SMF.Module.Core;
using PE.DTO.Internal.MVHistory;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Managers;
using PE.DTO.Internal.Setup;
using PE.DTO.External.Setup;
using PE.DTO.External.MVHistory;
using SMF.Module.Notification;
using PE.DTO.External;

namespace PE.ADP.Managers
{
  public class L1CommunicationManager : IL1CommunicationManager
  {
    #region members

    private IAdapterL1SendOffice _sendOffice;

    #endregion

    public L1CommunicationManager(IAdapterL1SendOffice sendOffice)
    {
      _sendOffice = sendOffice;
    }


    public async Task<DCPEBasId> ProcessL1BaseIdRequestAsync(DCL1BasIdRequest message)
    {
      SendOfficeResult<DCPEBasId> result = await _sendOffice.SendL1BaseIdRequestAsync(message);
      if (result.OperationSuccess)
        NotificationController.Info("Forwarding L1BaseIdRequest - success");
      else
        NotificationController.Error("Forwarding L1BaseIdRequest - error");
      return (result).DataConctract;
    }

    public async Task<DataContractBase> ProcessL1CutMessageAsync(DCL1CutData message)
    {
      SendOfficeResult<DataContractBase> result = await _sendOffice.SendL1CutMessageAsync(message);
      if (result.OperationSuccess)
        NotificationController.Info("Forwarding L1CutMessage - success");
      else
        NotificationController.Error("Forwarding L1CutMessage - error");
      return (result).DataConctract;
    }

    public async Task<DataContractBase> ProcessL1ScrapMessageAsync(DCL1ScrapData message)
    {
      SendOfficeResult<DataContractBase> result = await _sendOffice.SendL1ScrapMessageAsync(message);
      if (result.OperationSuccess)
        NotificationController.Info("Forwarding L1ScrapMessage - success");
      else
        NotificationController.Error("Forwarding L1ScrapMessage - error");
      return (result).DataConctract;
    }

    public async Task<DCPEBasId> ProcessL1DivisionMessageAsync(DCL1MaterialDivision message)
    {
      SendOfficeResult<DCPEBasId> result = await _sendOffice.SendL1DivisionMessageAsync(message);
      if (result.OperationSuccess)
        NotificationController.Info("Forwarding L1DivisionMessage - success");
      else
        NotificationController.Error("Forwarding L1DivisionMessage - error");
      return (result).DataConctract;
    }

    public async Task<DataContractBase> ProcessL1MeasDataMessageAsync(DCMeasData message)
    {
      SendOfficeResult<DataContractBase> result = await _sendOffice.SendL1MeasDataMessageAsync(message);
      if (result.OperationSuccess)
        NotificationController.Info("Forwarding L1MeasDataMessage - success");
      else
        NotificationController.Error("Forwarding L1MeasDataMessage - error");
      return (result).DataConctract;
    }

    public async Task<DataContractBase> ProcessL1SampleMeasMessageAsync(DCMeasDataSample message)
    {
      SendOfficeResult<DataContractBase> result = await _sendOffice.SendL1SampleMeasMessageAsync(message);
      if (result.OperationSuccess)
        NotificationController.Info("Forwarding L1SampleMeasMessage - success");
      else
        NotificationController.Error("Forwarding L1SampleMeasMessage - error");
      return (result).DataConctract;
    }

    public async Task<DataContractBase> ProcessSendTelegramSetupByteDataAsync(DCTelegramSetup message)
    {
      DCL1TelegramSetupExt externalTelegram = new DCL1TelegramSetupExt();
      externalTelegram.ToExternal(message);

      SendOfficeResult<DCDefaultExt> result = null;

      DCTCPSetpointTelegramEnvelopeExt envelope = new DCTCPSetpointTelegramEnvelopeExt();
      envelope.Port = message.Port;
      envelope.IpAddress = message.IpAddress;
      envelope.TelegramToBeSend = externalTelegram;

      result = await _sendOffice.SendSetupDataToL1TCPAsync(envelope);


      if (result.OperationSuccess)
        NotificationController.Info("Forwarding Setup message to L1 - success");
      else
        NotificationController.Error("Forwarding Setup message to L1  - error");

      return new DataContractBase();
    }

    public async Task<DataContractBase> ProcessSendTelegramSetupStructureDataAsync(DCCommonSetupStructure message)
    {
			// in case when it is necessary it is possible to pass internal datacontract outside of PE environment
      SendOfficeResult<DataContractBase> result = await _sendOffice.SendSetupDataToL1AdapterAsync(message);

      if (result.OperationSuccess)
        NotificationController.Info("Forwarding Setup message to L1 - success");
      else
        NotificationController.Error("Forwarding Setup message to L1  - error");

      return result.DataConctract;
    }

    public async Task<DataContractBase> ProcessExternalSendSetupDataRequestAsync(DCCommonSetupStructure message)
    {
      // in case when it is necessary it is possible to pass internal datacontract outside of PE environment
      SendOfficeResult<DataContractBase> result = await _sendOffice.SendSetupUpdateRequestToL1AdapterAsync(message);

      if (result.OperationSuccess)
        NotificationController.Info("Forwarding Setup request message to L1 - success");
      else
        NotificationController.Error("Forwarding Setup request message to L1  - error");

      return result.DataConctract;
    }

    public async Task<DataContractBase> ProcessSetupUpdateMessageAsync(DCCommonSetupStructure message)
    {
      // in case when it is necessary it is possible to pass internal datacontract outside of PE environment
      SendOfficeResult<DataContractBase> result = await _sendOffice.SendSetupUpdateAsync(message);

      if (result.OperationSuccess)
        NotificationController.Info("Forwarding Setup request message to L1 - success");
      else
        NotificationController.Error("Forwarding Setup request message to L1  - error");

      return result.DataConctract;
    }
  }
}
