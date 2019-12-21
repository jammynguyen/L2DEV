using PE.DTO.Internal.DBAdapter;
using PE.Interfaces.Lite;
using SMF.Core.DC;
using SMF.Module.Core;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PE.Lite.L3Sim.Communication
{
  internal class SendOffice
  {
    internal static async Task<SendOfficeResult<DataContractBase>> SendWorkOrderDataToPRM(DCL3L2WorkOrderDefinitionExt dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      //call method on remote module
      return await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.ProccesExtWorkOrderMessage(dataToSend));
    }
  }
}
