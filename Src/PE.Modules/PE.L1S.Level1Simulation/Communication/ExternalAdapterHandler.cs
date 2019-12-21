using Ninject;
using PE.DTO.External.MVHistory;
using PE.Interfaces.Interfaces.Managers;
using PE.L1S.Level1Simulation.Module;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.L1S.Level1Simulation.Communication
{
  internal class ExternalAdapterHandler
  {
    private static readonly IL1SimulationFurnace _l1SimulationManager;

    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IL1SimulationFurnace>().To<L1SimulationFurnace>();
        _l1SimulationManager = kernel.Get<IL1SimulationFurnace>();
      }
    }


  }
}
