using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.External;
using PE.DTO.External.Setup;
using PE.DTO.Internal.Setup;
using PE.Interfaces.Managers;
using SMF.Core.DC;

namespace PE.Interfaces.Interfaces.Managers.Custom
{
  public interface IL1SetupManager : IManagerBase
  {
    Task<DataContractBase> SendSetupMessageAsync(DCCommonSetupStructure message);
    Task<DataContractBase> HandleSetupRequestMessageAsync(DCCommonSetupStructure message);
    
  }
}
