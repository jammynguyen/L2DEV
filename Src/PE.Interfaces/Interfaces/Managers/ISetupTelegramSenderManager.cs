using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.Setup;
using SMF.Core.DC;

namespace PE.Interfaces.Interfaces.Managers
{
  public interface ISetupTelegramsSenderManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessForceSendTelegramSetupAsync(DCTelegramSetupId message);
  }
}
