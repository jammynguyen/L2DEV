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
  public interface IL1ConsumptionSendOffice
  {
    Task<SendOfficeResult<DCDefaultExt>> SendL1AggregatedMeasDataToAdapterAsync(DCMeasDataExt dataToSend);
  }
}
