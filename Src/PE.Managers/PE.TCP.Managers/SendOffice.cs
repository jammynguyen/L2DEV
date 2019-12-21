using PE.DTO.External.MVHistory;
using PE.DTO.Internal.TcpProxy;
using PE.Interfaces.Lite;
using SMF.Module.Core;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PE.TCP.Managers
{
  public static class SendOffice
  {

    public static async Task SendMeasDataSampleToAdapter(DCMeasDataSampleExt tel)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      //call method on remote module
      await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessMeasurementSample(tel));
    }

    public static async Task SendMeasDataToAdapter(DCMeasDataExt tel)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      //call method on remote module
      await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessMeasurementNonSample(tel));
    }

    public static async Task SendScrapDataToAdapter(DCL1ScrapDataExt tel)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      //call method on remote module
      await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessL1ScrapDataTelegram(tel));
    }

    public static async Task SendMaterialDivisionToAdapter(DCL1MaterialDivisionExt tel)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      //call method on remote module
      await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessL1MaterialDivisionMessage(tel));
    }

    public static async Task SendCutDataToAdapter(DCL1CutDataExt tel)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      //call method on remote module
      await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessL1CutDataMessage(tel));
    }

    public static async Task SendBasIdRequestToAdapter(DCL1BasIdRequestExt tel)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      //call method on remote module
      await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.L1BaseIdRequest(tel));
    }

    internal static async Task SendFinishedSignalToAdapter(DCL1MaterialFinishedExt tel)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      //call method on remote module
      await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.SendFinishedSignalToAdapter(tel));
    }
  }
}
