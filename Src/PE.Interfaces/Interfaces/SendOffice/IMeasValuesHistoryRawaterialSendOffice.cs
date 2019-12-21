using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.Interfaces.Interfaces.SendOffice
{
  public interface IMeasValuesHistoryRawMaterialSendOffice
  {
    Task<SendOfficeResult<DCL1L3MaterialConnector>> SendRequestForL3MaterialAsync(DCFeatureRelatedOperationData data);
    Task<SendOfficeResult<DCProductData>> SendRequestToCreateProductAsync(DCRawMaterialData data);
    Task<SendOfficeResult<DataContractBase>> SendHeadEnterToDLSAsync(DTO.Internal.Delay.DCDelayEvent data);
    Task<SendOfficeResult<DataContractBase>> SendTailLeavesToDLSAsync(DTO.Internal.Delay.DCDelayEvent data);
    Task<SendOfficeResult<DataContractBase>> SendRemoveFinishedOrdersFromScheduleAsync(DataContractBase data);
    Task<SendOfficeResult<DataContractBase>> SendScrapEventAsync(DCL1ScrapData data);





    //Task<SendOfficeResult<DataContractBase>> SendTriggerToWBMAsync(DCTriggerData dcTriggerData);
    //Task<SendOfficeResult<DataContractBase>> SendTriggerToTRKAsync(DCTriggerData dcTriggerData);
    //Task<SendOfficeResult<DataContractBase>> SendTriggerToPPLAsync(DCTriggerData dcTriggerData);
    //Task<SendOfficeResult<DataContractBase>> AnswerL1BasIdRequestDivideAsync(DCPEBasId dcBaseIdMsg);
    //Task<SendOfficeResult<DataContractBase>> AnswerL1BasIdRequestAsync(DCPEBasId dcBaseIdMsg);
  }
}
