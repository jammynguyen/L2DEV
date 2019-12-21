using System;
using System.ServiceModel;
using System.Threading.Tasks;
using PE.DTO.External;
using PE.DTO.External.MVHistory;
using PE.DTO.Internal.DBAdapter;
using PE.Interfaces.Lite;
using PE.Interfaces.SendOffice;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.SIM.Simulation.Communication
{
  public class SendOffice : ModuleSendOfficeBase, ISimulationSendOffice
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
  }
}
