using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.MVHistory;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.Interfaces.Interfaces.SendOffice
{
  public interface IMeasValuesHistoryMeasurementSendOffice
  {
    Task<SendOfficeResult<DataContractBase>> SendAssetEventAsync(DCMeasData data);
    //Task<SendOfficeResult<DataContractBase>> SendTriggerToWBMAsync(DCTriggerData dcTriggerData);
    //Task<SendOfficeResult<DataContractBase>> SendTriggerToTRKAsync(DCTriggerData dcTriggerData);
    //Task<SendOfficeResult<DataContractBase>> SendTriggerToDLSAsync(DCTriggerData dcTriggerData);
    //Task<SendOfficeResult<DataContractBase>> SendTriggerToPPLAsync(DCTriggerData dcTriggerData);

  }
}
