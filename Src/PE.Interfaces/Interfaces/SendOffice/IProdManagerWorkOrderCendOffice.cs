using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.Maintenance;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.RollShop;
using PE.DTO.Internal.Schedule;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.Interfaces.Interfaces.SendOffice
{
  public interface IProdManagerWorkOrderSendOffice
  {
    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<SendOfficeResult<DataContractBase>> ConnectRawMaterialWithL3MaterialAsync(DCL1L3MaterialConnector dataToSend);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<SendOfficeResult<DataContractBase>> AutoScheduleWorkOrderAsync(DCWorkOrderToSchedule dataToSend);


  }
}
