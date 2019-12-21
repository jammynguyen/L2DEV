using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Signalr;
using SMF.Module.Core;

namespace PE.HMI.Module
{
  public class Worker : BaseWorker
  {
    #region managers

    #endregion
    #region properties

    #endregion
    #region ctor

    #endregion

    public override void ModuleStarted(object sender, ModuleStartEventArgs e)
    {
      SignalRClient.Init();
      base.ModuleStarted(sender, e);
    }
  }
}
