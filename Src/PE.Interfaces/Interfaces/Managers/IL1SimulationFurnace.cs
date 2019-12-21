using PE.DTO.External.MVHistory;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Interfaces.Managers
{
  public interface IL1SimulationFurnace
  {
    void ProcessPEBaseIdTelegam(DCPEBasIdExt dataContract);
  }
}
