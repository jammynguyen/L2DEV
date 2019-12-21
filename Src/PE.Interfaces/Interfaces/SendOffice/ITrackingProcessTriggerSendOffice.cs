using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.MVHistory;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.Interfaces.Interfaces.SendOffice
{
  public interface ITrackingProcessTriggerSendOffice
  {
    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<SendOfficeResult<DataContractBase>> ChangeMaterialStatus(DCNewMaterialStatus dataToSend);

    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<SendOfficeResult<DataContractBase>> CreateProductAfterProductionEnd(DCMaterialProductionEnd dataToSend);
  }
}
