using PE.DTO.Internal.MVHistory;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Lite;
using PE.TRK.Managers;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.TRK.Tracking.Communication
{
  internal class SendOffice : ModuleSendOfficeBase, ITrackingProcessTriggerSendOffice
  {
    //public async Task<SendOfficeResult<DataContractBase>> ChangeMaterialStatus(DCNewMaterialStatus dataToSend)
    //{
    //  string targetModuleName = PE.Interfaces.Module.Modules.MvHistory.Name;
    //  IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

    //  return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ChangeMaterialStatusAsync(dataToSend));
    //}
    //public async Task<SendOfficeResult<DataContractBase>> CreateProductAfterProductionEnd(DCMaterialProductionEnd dataToSend)
    //{
    //  string targetModuleName = PE.Interfaces.Module.Modules.MvHistory.Name;
    //  IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

    //  return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessProductionEndAsync(dataToSend));
    //}
  }
}
