using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Ninject;
using Ninject.Parameters;
using PE.DBA.DataBaseAdapter.Communication;
using PE.DBA.Managers;
using PE.Interfaces.Managers;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.Module.Parameter;

namespace PE.DBA.DataBaseAdapter.Module
{
  public class Worker:BaseWorker
  {
    #region managers

    private readonly IL3DBCommunicationManager _L3DBCommunicationManager;

    #endregion
    #region properties

    public static int ParameterOrderGeneartionDelay { get; private set; }
    public static int LimitWeightMin { get; set; }
    public static int LimitWeightMax { get; set; }
    public static int LimitMaterialsMin { get; set; }
    public static int LimitMaterialsMax { get; set; }
    #endregion
    #region ctor

    public Worker()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IL3DBCommunicationManager>().To<L3DBCommunicationManager>();
        _L3DBCommunicationManager = kernel.Get<IL3DBCommunicationManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    #endregion
    #region module calls

		public override async void TimerMethod(object sender, ElapsedEventArgs e)
    {
      try
      {
        await _L3DBCommunicationManager.TransferDataFromTransferTableToAdapterAsync();
        await _L3DBCommunicationManager.UpdateWorkOrdesWithTimeoutAsync();
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
    }
    public override void ModuleInitialized(object sender, ModuleInitEventArgs e)
    {
      int timerRefreshCycle = ParameterController.GetParameter("DBA_WoProcessingCycle").ValueInt.GetValueOrDefault() * 1000;
      // uncoment this ModuleController.SetTimerInterval(timerRefreshCycle);
      base.ModuleInitialized(sender, e);
    }
    public override async void ModuleStarted(object sender, ModuleStartEventArgs e)
    {
      await _L3DBCommunicationManager.TransferDataFromTransferTableToAdapterAsync();
      await _L3DBCommunicationManager.UpdateWorkOrdesWithTimeoutAsync();
      base.ModuleStarted(sender, e);
    }

    #endregion
  }
}
