using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using CUS.L1A.L1Adapter.Communication;
using CUS.L1A.Managers;
using Ninject;
using Ninject.Parameters;
using PE.Interfaces.Interfaces.Managers.Custom;
using SMF.Module.Core;
using SMF.Module.Notification;

namespace CUS.L1A.L1Adapter.Module
{
  public class Worker : BaseWorker
  {
    #region managers

    private readonly IL1SetupManager _level1SetupManager;
    private readonly IL1TrackingManager _level1TrackingManager;
    private readonly IL1ConsumptionManager _level1ConsumptionManager;

    #endregion
    #region properties

    #endregion
    #region members

    private uint _cycle=0;

    #endregion
    #region ctor

    public Worker()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IL1SetupManager>().To<L1SetupManager>();
        _level1SetupManager = kernel.Get<IL1SetupManager>(new ConstructorArgument("sendOffice", new SendOffice()));

        kernel.Bind<IL1TrackingManager>().To<L1TrackingManager>();
        _level1TrackingManager = kernel.Get<IL1TrackingManager>(new ConstructorArgument("sendOffice", new SendOffice()));

        kernel.Bind<IL1ConsumptionManager>().To<L1ConsumptionManager>();
        _level1ConsumptionManager = kernel.Get<IL1ConsumptionManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    #endregion
    #region module calls

    public override async void TimerMethod(object sender, ElapsedEventArgs e)
    {

      if (_cycle % 1==0)
      {
        //try
        //   {
        //     await _level1TrackingManager.PrintState();
        //     NotificationController.Info("------------------------------------------------------");
        //   }
        //   catch { }
      }
      if (_cycle % 60 == 0)
      {
        try
        {
          NotificationController.Info("---[consumption measurements--------------------------");
          await _level1ConsumptionManager.ReadMeasurements();
          NotificationController.Info("---[end of consumption measurements-------------------");
        }
        catch { }
      }
      _cycle++;
    }
    public override async void ModuleInitialized(object sender, ModuleInitEventArgs e)
    {

    }
    public override async void ModuleStarted(object sender, ModuleStartEventArgs e)
    {
      await _level1TrackingManager.ConnectToOpcServerAsync();
      await _level1TrackingManager.InitCommunicationAsync();

      await _level1ConsumptionManager.ConnectToOpcServerAsync();
      await _level1ConsumptionManager.InitCommunicationAsync();
    }

    #endregion
  }
}
