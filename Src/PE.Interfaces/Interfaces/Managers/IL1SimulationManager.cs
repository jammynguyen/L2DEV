using PE.DTO.External.MVHistory;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Interfaces.Managers
{
  public interface IL1SimulationManager
  {
    [FaultContract(typeof(ModuleMessage))]
    void ProcessPEBaseIdTelegam(DCPEBasIdExt dataContract);
  }
}
