using PE.DTO.External;
using PE.DTO.External.DBAdapter;
using PE.DTO.External.MVHistory;
using PE.DTO.External.Setup;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.Setup;
using PE.Interfaces.Interfaces.Custom;
using PE.Interfaces.Interfaces.Lite;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Lite;
using SMF.Core;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PE.ADP.Adapter.Communication
{
  public class SendOffice : ModuleSendOfficeBase, IAdapterL3SendOffice, IAdapterL1SendOffice
  {
    #region L3 communication

    public async Task<SendOfficeResult<DCWorkOrderStatus>> SendWorkOrderDataAsync(DCL3L2WorkOrderDefinition dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessWorkOrderDataAsync(dataToSend));
    }

    #endregion
    #region L1 communication

    public async Task<SendOfficeResult<DCPEBasId>> SendL1BaseIdRequestAsync(DCL1BasIdRequest dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      //call method on remote module
      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessL1BaseIdRequestAsync(dataToSend));
    }

    public async Task<SendOfficeResult<DataContractBase>> SendL1CutMessageAsync(DCL1CutData dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      //call method on remote module
      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessCutMessageAsync(dataToSend));
    }

    public async Task<SendOfficeResult<DataContractBase>> SendL1ScrapMessageAsync(DCL1ScrapData dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      //call method on remote module
      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessScrapMessageAsync(dataToSend));
    }

    public async Task<SendOfficeResult<DCPEBasId>> SendL1DivisionMessageAsync(DCL1MaterialDivision dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      //call method on remote module
      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessDivisionMaterialMessageAsync(dataToSend));
    }

    public async Task<SendOfficeResult<DataContractBase>> SendL1MeasDataMessageAsync(DCMeasData dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessL1MeasurementAsync(dataToSend));
    }

    public async Task<SendOfficeResult<DataContractBase>> SendL1SampleMeasMessageAsync(DCMeasDataSample dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessL1SampleMeasurementAsync(dataToSend));

    }
    #endregion
    #region L1 Send

    public async Task<SendOfficeResult<DCDefaultExt>> SendSetupDataToL1TCPAsync(DCTCPSetpointTelegramEnvelopeExt dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.TcpProxy.Name;
      ITcpProxy client = InterfaceHelper.GetFactoryChannel<ITcpProxy>(targetModuleName);


      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.SendTelegramSetupDataAsync(dataToSend));
    }

    public async Task<SendOfficeResult<DataContractBase>> SendSetupDataToL1AdapterAsync(DCCommonSetupStructure dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.L1Adapter.Name;
      IL1Adapter client = InterfaceHelper.GetFactoryChannel<IL1Adapter>(targetModuleName);


      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.SendSetupDataToL1AdapterAsync(dataToSend));
    }

    public async Task<SendOfficeResult<DataContractBase>> SendSetupUpdateRequestToL1AdapterAsync(DCCommonSetupStructure dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.L1Adapter.Name;
      IL1Adapter client = InterfaceHelper.GetFactoryChannel<IL1Adapter>(targetModuleName);


      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.SendSetupDataRequestToL1AdapterAsync(dataToSend));
    }

    public async Task<SendOfficeResult<DataContractBase>> SendSetupUpdateAsync(DCCommonSetupStructure dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);


      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.UpdateSetupFromL1Async(dataToSend));
    }

    #endregion
  }
}
