using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.ProdManager;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Lite;
using PE.PPL.Managers;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.PPL.ProdPlaning.Communication
{
  public class SendOffice : ModuleSendOfficeBase, IProductionPlanningSendOffice
  {
    //public async Task<SendOfficeResult<DataContractBase>> RequestAboutFirstUnassignedMaterialFromWorkOrdersListAsync(DCWorkOrdersList dCWorkOrdersList)
    //{
    //  string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
    //  IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

    //  return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessRequestFirstScheduleMaterialAsync(dCWorkOrdersList));

    //}
  }
}
