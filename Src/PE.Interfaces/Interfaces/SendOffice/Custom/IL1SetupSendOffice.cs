using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.Setup;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.Interfaces.Interfaces.SendOffice.Custom
{
  public interface IL1SetupSendOffice
  {
    Task<SendOfficeResult<DataContractBase>> SendL1SetupToAdapterAsync(DCCommonSetupStructure dataToSend);
  }
}
