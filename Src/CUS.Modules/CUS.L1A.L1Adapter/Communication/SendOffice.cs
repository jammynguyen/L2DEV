using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.External;
using PE.DTO.External.MVHistory;
using PE.DTO.Internal.Setup;
using PE.Interfaces.Interfaces.SendOffice.Custom;
using PE.Interfaces.Lite;
using SMF.Core.DC;
using SMF.Module.Core;

namespace CUS.L1A.L1Adapter.Communication
{
  public class SendOffice : ModuleSendOfficeBase, IL1SetupSendOffice, IL1TrackingSendOffice, IL1ConsumptionSendOffice
  {
    public async Task<SendOfficeResult<DCPEBasIdExt>> SendL1MaterialIdRequestToAdapterAsync(DCL1BasIdRequestExt dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.L1BaseIdRequestAsync(dataToSend));
    }
    public async Task<SendOfficeResult<DCDefaultExt>> SendL1CutDataToAdapterAsync(DCL1CutDataExt dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.L1CutMessageAsync(dataToSend)); ;
    }
    public async Task<SendOfficeResult<DCPEBasIdExt>> SendL1DivisionToAdapterAsync(DCL1MaterialDivisionExt dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.L1DivisionMessageAsync(dataToSend)); ;
    }
    public async Task<SendOfficeResult<DCDefaultExt>> SendL1ScrapInfoToAdapterAsync(DCL1ScrapDataExt dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.L1ScrapMessageAsync(dataToSend)); ;
    }
    public async Task<SendOfficeResult<DCDefaultExt>> SendL1AggregatedMeasDataToAdapterAsync(DCMeasDataExt dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.L1MeasDataMessageAsync(dataToSend)); ;
    }
    public async Task<SendOfficeResult<DCDefaultExt>> SendL1SampleMeasDataToAdapterAsync(DCMeasDataSampleExt dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.L1SampleMeasMessageAsync(dataToSend)); ;
    }
    public async Task<SendOfficeResult<DataContractBase>> SendL1SetupToAdapterAsync(DCCommonSetupStructure dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.L1SetupUpdateMessageAsync(dataToSend)); ;
    }
  }
}
