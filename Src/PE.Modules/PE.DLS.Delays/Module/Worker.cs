using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Ninject;
using Ninject.Parameters;
using PE.DLS.Delays.Communication;
using PE.DLS.Managers;
using PE.Interfaces.Managers;
using SMF.Core.Helpers;
using SMF.Module.Core;
using SMF.Module.Notification;

namespace PE.DLS.Delay.Module
{
  public class Worker:BaseWorker
  {
    #region managers

    private static readonly IDelayManager _delayManager;

    #endregion
    #region properties

    #endregion
    #region ctor

    static Worker()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IDelayManager>().To<DelayManager>();
        _delayManager = kernel.Get<IDelayManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    #endregion
    #region module calls

    public override async void TimerMethod(object sender, ElapsedEventArgs e)
    {
      try
      {
        await ExternalAdapterHandler.UpdateCurrentDelayStatusAsync();
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
    }

    #endregion
  }
}
