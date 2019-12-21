using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.Maintenance;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using PE.DTO.Internal.RollShop;
using PE.DTO.Internal.Schedule;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Lite;
using PE.PRM.Managers;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.PRM.ProdManager.Communication
{
  public class SendOffice : ModuleSendOfficeBase, IProdManagerCatalogueSendOffice, IProdManagerProductSendOffice, IProdManagerWorkOrderSendOffice
  {
    //public async Task<SendOfficeResult<DataContractBase>> ConnectRawMaterialWithProductAsync(DCProductData dataToSend)
    //{
    //  string targetModuleName = PE.Interfaces.Module.Modules.MvHistory.Name;
    //  IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

    //  return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.ConnectRawMaterialWithProductAsync(dataToSend));
    //}

    public async Task<SendOfficeResult<DataContractBase>> AutoScheduleWorkOrderAsync(DCWorkOrderToSchedule dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.ProdPlaning.Name;
      IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);

      //call method on remote module
      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.AddWorkOrderToScheduleAsync(dataToSend));
    }

    public async Task<SendOfficeResult<DataContractBase>> AccumulateRollsUsageAsync(DCRollsAccu dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.AccumulateRollsUsageAsync(dataToSend));
    }

    public async Task<SendOfficeResult<DataContractBase>> AccumulateEquipmentUsageAsync(DCEquipmentAccu dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return await HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.AccumulateEquipmentUsageAsync(dataToSend));
    }
  }

}
