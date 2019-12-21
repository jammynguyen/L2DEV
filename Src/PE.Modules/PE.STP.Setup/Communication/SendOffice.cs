using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using PE.DTO.Internal.Schedule;
using PE.DTO.Internal.Setup;
using PE.DTO.Internal.TcpProxy.Configuration;
using PE.Interfaces.Interfaces.Lite;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Lite;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.STP.Setup.Communication
{
  public class SendOffice : ModuleSendOfficeBase, ISetupTelegramSendOffice
  {
    public async Task<SendOfficeResult<DataContractBase>> SendTelegramSetupDataAsync(DCCommonSetupStructure dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      //call method on remote module
      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.SendSetupDataAsync(dataToSend));
    }
    public async Task<SendOfficeResult<DataContractBase>> SendSetupDataRequestToL1Async(DCCommonSetupStructure dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      //call method on remote module
      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.SendSetupDataRequestAsync(dataToSend));
    }
  }

}
