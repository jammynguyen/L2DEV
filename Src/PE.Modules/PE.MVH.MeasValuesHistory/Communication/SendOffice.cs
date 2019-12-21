using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Lite;
using PE.MVH.Managers;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.MVH.MeasValuesHistory.Communication
{
  internal class SendOffice : ModuleSendOfficeBase, IMeasValuesHistoryDefectSendOffice, IMeasValuesHistoryMeasurementSendOffice, IMeasValuesHistoryRawMaterialSendOffice
  {
    public async Task<SendOfficeResult<DCL1L3MaterialConnector>> SendRequestForL3MaterialAsync(DCFeatureRelatedOperationData data)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.ProdPlaning.Name;
      IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);
      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.RequestL3MaterialAsync(data));
    }
    public async Task<SendOfficeResult<DCProductData>> SendRequestToCreateProductAsync(DTO.Internal.ProdManager.DCRawMaterialData data)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessProductionEndAsync(data));
    }

    public async Task<SendOfficeResult<DataContractBase>> SendHeadEnterToDLSAsync(DTO.Internal.Delay.DCDelayEvent data)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Delay.Name;
      IDelay client = InterfaceHelper.GetFactoryChannel<IDelay>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessHeadEnterAsync(data));
    }

    public async Task<SendOfficeResult<DataContractBase>> SendTailLeavesToDLSAsync(DTO.Internal.Delay.DCDelayEvent data)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Delay.Name;
      IDelay client = InterfaceHelper.GetFactoryChannel<IDelay>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessTailLeavesAsync(data));
    }

    public async Task<SendOfficeResult<DataContractBase>> SendRemoveFinishedOrdersFromScheduleAsync(DataContractBase data)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.ProdPlaning.Name;
      IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);
      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.RemoveFinishedOrdersFromScheduleAsync(data));
    }
    public async Task<SendOfficeResult<DataContractBase>> SendAssetEventAsync(DCMeasData data)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Dispatcher.Name;
      IDispatcher client = InterfaceHelper.GetFactoryChannel<IDispatcher>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.AssetEventAsync(data));
    }
    public async Task<SendOfficeResult<DataContractBase>> SendScrapEventAsync(DCL1ScrapData data)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Dispatcher.Name;
      IDispatcher client = InterfaceHelper.GetFactoryChannel<IDispatcher>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ScrapEventAsync(data));
    }







    //public async Task<SendOfficeResult<DataContractBase>> SendTriggerToWBMAsync(DCTriggerData dcTriggerData)
    //{
    //  string targetModuleName = PE.Interfaces.Module.Modules.WalkingBeamFurnance.Name;
    //  IWalkingBeamFurnance client = InterfaceHelper.GetFactoryChannel<IWalkingBeamFurnance>(targetModuleName);

    //  //call method on remote module
    //  if ((client as IClientChannel).State == CommunicationState.Opened)
    //  {
    //    return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessTriggerAsync(dcTriggerData));
    //  }
    //  else
    //  {
    //    DataContractBase returnValue = new DataContractBase();

    //    return new SendOfficeResult<DataContractBase>(returnValue, false);
    //  }
    //}

    //public async Task<SendOfficeResult<DataContractBase>> SendTriggerToTRKAsync(DCTriggerData dcTriggerData)
    //{
    //  string targetModuleName = PE.Interfaces.Module.Modules.Tracking.Name;
    //  ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

    //  //call method on remote module
    //  if ((client as IClientChannel).State == CommunicationState.Opened)
    //  {
    //    return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessTriggerAsync(dcTriggerData));
    //  }
    //  else
    //  {
    //    DataContractBase returnValue = new DataContractBase();

    //    return new SendOfficeResult<DataContractBase>(returnValue, false);
    //  }
    //}

    //public async Task<SendOfficeResult<DataContractBase>> SendTriggerToDLSAsync(DCTriggerData dcTriggerData)
    //{
    //  string targetModuleName = PE.Interfaces.Module.Modules.Delay.Name;
    //  IDelay client = InterfaceHelper.GetFactoryChannel<IDelay>(targetModuleName);

    //  if ((client as IClientChannel).State == CommunicationState.Opened)
    //  {
    //    return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessTrigger(dcTriggerData));
    //  }
    //  else
    //  {
    //    DataContractBase returnValue = new DataContractBase();

    //    return new SendOfficeResult<DataContractBase>(returnValue, false);
    //  }
    //}
    //public async Task<SendOfficeResult<DataContractBase>> SendTriggerToPPLAsync(DCTriggerData dcTriggerData)
    //{
    //  string targetModuleName = PE.Interfaces.Module.Modules.ProdPlaning.Name;
    //  IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);
    //  return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessTrigger(dcTriggerData));

    //}




  }
}
