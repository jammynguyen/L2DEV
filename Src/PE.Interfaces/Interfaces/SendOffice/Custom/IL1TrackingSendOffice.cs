using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.External;
using PE.DTO.External.MVHistory;
using PE.DTO.Internal.DBAdapter;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.Interfaces.Interfaces.SendOffice.Custom
{
  public interface IL1TrackingSendOffice
  {
    Task<SendOfficeResult<DCPEBasIdExt>> SendL1MaterialIdRequestToAdapterAsync(DCL1BasIdRequestExt dataToSend);
    Task<SendOfficeResult<DCDefaultExt>> SendL1CutDataToAdapterAsync(DCL1CutDataExt dataToSend);
    Task<SendOfficeResult<DCPEBasIdExt>> SendL1DivisionToAdapterAsync(DCL1MaterialDivisionExt dataToSend);
    Task<SendOfficeResult<DCDefaultExt>> SendL1ScrapInfoToAdapterAsync(DCL1ScrapDataExt dataToSend);
    Task<SendOfficeResult<DCDefaultExt>> SendL1AggregatedMeasDataToAdapterAsync(DCMeasDataExt dataToSend);
  }
}
