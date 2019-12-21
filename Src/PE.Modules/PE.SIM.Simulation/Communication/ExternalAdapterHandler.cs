using Ninject;
using Ninject.Parameters;
using PE.Interfaces.Interfaces.Managers;
using PE.SIM.Managers;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.SIM.Simulation.Communication
{
  internal class ExternalAdapterHandler
  {
    #region members

    private static readonly ILevel1SimulationManager _level1SimulationManager;
    private static readonly ILevel3SimulationManager _level3SimulationManager;

    #endregion
    #region ctor

    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<ILevel1SimulationManager>().To<L1SimulationManager>();
        kernel.Bind<ILevel3SimulationManager>().To<L3SimulationManager>();
        _level1SimulationManager = kernel.Get<ILevel1SimulationManager>(new ConstructorArgument("sendOffice", new SendOffice()));
        _level3SimulationManager = kernel.Get<ILevel3SimulationManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    #endregion
  }
}
