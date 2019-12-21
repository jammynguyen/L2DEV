using PE.DTO.External.MVHistory;
using PE.DTO.Internal.MVHistory;
using PE.Interfaces.Lite;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.L1S.Level1Simulation.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, IL1Simulation
  {

    #region ctor
    public ExternalAdapter(string moduleName) : base(moduleName, typeof(PE.Interfaces.Lite.IL1Simulation)) { }
    #endregion

  }
}
