using PE.DTO.Internal.MVHistory;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Lite;
using PE.WBF.Managers;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.WBF.WalkingBeamFurnance.Communication
{
  public class SendOffice : ModuleSendOfficeBase, IFurnaceProcessTriggersSendOffice
  {
    //public async Task<SendOfficeResult<DataContractBase>> RequestFirstInScheduleMaterial(DCTriggerData data)
    //{
    //  string targetModuleName = PE.Interfaces.Module.Modules.ProdPlaning.Name;
    //  IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);

    //  return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.RequestNextMaterialAsync(data));
    //}
    //public Task<SendOfficeResult<DataContractBase>> RequestFirstInScheduleMaterial(DCTriggerData data)
    //{
    //  throw new NotImplementedException();
    //}
  }
}
