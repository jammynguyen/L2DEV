using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.Setup;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.Interfaces.Interfaces.SendOffice
{
  public interface ISetupTelegramSendOffice
  {

    Task<SendOfficeResult<DataContractBase>> SendTelegramSetupDataAsync(DCCommonSetupStructure dataToSend);
    Task<SendOfficeResult<DataContractBase>> SendSetupDataRequestToL1Async(DCCommonSetupStructure dataToSend);
  }
}
