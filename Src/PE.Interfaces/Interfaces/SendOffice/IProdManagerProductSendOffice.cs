using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.Maintenance;
using PE.DTO.Internal.ProdManager;
using PE.DTO.Internal.RollShop;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.Interfaces.Interfaces.SendOffice
{
  public interface IProdManagerProductSendOffice
  {
    //Task<SendOfficeResult<DataContractBase>> ConnectRawMaterialWithProductAsync(DCProductData dataToSend);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<SendOfficeResult<DataContractBase>> AccumulateRollsUsageAsync(DCRollsAccu dataToSend);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<SendOfficeResult<DataContractBase>> AccumulateEquipmentUsageAsync(DCEquipmentAccu dataToSend);
  }
}
