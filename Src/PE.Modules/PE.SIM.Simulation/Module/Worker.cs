using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Ninject;
using Ninject.Parameters;
using PE.Interfaces.Interfaces.Managers;
using PE.SIM.Managers;
using PE.SIM.Simulation.Communication;
using SMF.Module.Core;
using SMF.Module.Limit;
using SMF.Module.Notification;
using SMF.Module.Parameter;

namespace PE.SIM.Simulation.Module
{
  public class Worker:BaseWorker
  {
    #region managers

    private  readonly ILevel1SimulationManager _level1SimulationManager;
    private  readonly ILevel3SimulationManager _level3SimulationManager;

    #endregion
    #region properties

    public static int ParameterOrderGeneartionDelay { get; private set; }
    public static int LimitWeightMin { get; set; }
    public static int LimitWeightMax { get; set; }
    public static int LimitMaterialsMin { get; set; }
    public static int LimitMaterialsMax { get; set; }
    #endregion
    #region members
		private static int timerCycle=0;
    #endregion
    #region ctor

    public Worker()
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
    #region module calls

    public override async void TimerMethod(object sender, ElapsedEventArgs e)
    {
      try
      {
        if (timerCycle % 20 == 0)
          await _level3SimulationManager.CreateWorkOrders(LimitWeightMin, LimitWeightMax, LimitMaterialsMin, LimitMaterialsMax, ParameterOrderGeneartionDelay);
        timerCycle++;
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
    }
    public override async void ModuleInitialized(object sender, ModuleInitEventArgs e)
    {
      ParameterOrderGeneartionDelay = ParameterController.GetParameter("SIM_WorkOrderGenerationDelay").ValueInt.GetValueOrDefault();
      LimitMaterialsMin = LimitController.GetLimit("SIM_WorkOrderNumMaterials").LowerValueInt.GetValueOrDefault();
      LimitMaterialsMax = LimitController.GetLimit("SIM_WorkOrderNumMaterials").UpperValueInt.GetValueOrDefault();

      try
      {
        await _level3SimulationManager.CreateWorkOrders(LimitWeightMin, LimitWeightMax, LimitMaterialsMin, LimitMaterialsMax, ParameterOrderGeneartionDelay);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
    }
    public override async void ModuleStarted(object sender, ModuleStartEventArgs e)
    {
      await _level1SimulationManager.StartSimulation();
    }

    #endregion
  }
}
