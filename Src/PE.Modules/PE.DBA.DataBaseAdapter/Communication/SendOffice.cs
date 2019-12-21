using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DBA.Managers;
using PE.DTO.External.DBAdapter;
using PE.DTO.Internal.DBAdapter;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Lite;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Core.Interfaces.Telegram;

namespace PE.DBA.DataBaseAdapter.Communication
{
  public class SendOffice : ModuleSendOfficeBase, IDbAdapterSendOffice
  {
    public async Task<SendOfficeResult<DCWorkOrderStatusExt>> SendWorkOrderDataToAdapterAsync(DCL3L2WorkOrderDefinitionExt dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ExternalProccesWorkOrderMessageAsync(dataToSend));
    }
  }
}
