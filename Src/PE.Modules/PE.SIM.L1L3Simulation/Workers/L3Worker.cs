using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Ninject;
using Ninject.Parameters;
using PE.Interfaces.Interfaces.Managers;
using PE.SIM.L1L3Simulation.Communication;
using PE.SIM.Managers;
using SMF.Core.Log;
using SMF.Module.Core;
using SMF.Module.Limit;
using SMF.Module.Parameter;

namespace PE.SIM.L1L3Simulation.Workers
{
  public static class L3Worker
  {
    #region managers

    private static readonly ILevel3SimulationManager _level3SimulationManager;

    #endregion
    #region properties

    public static int ParameterOrderGeneartionDelay { get; private set; }
    public static int LimitWeightMin { get; set; }
    public static int LimitWeightMax { get; set; }
    public static int LimitMaterialsMin { get; set; }
    public static int LimitMaterialsMax { get; set; }
    #endregion

    #region delegated
    public static StatusDelegate OnStatus { get; private set; }

    #endregion
    #region members

    private static Timer _l3Timer;

    #endregion
    #region ctor

    static L3Worker()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<ILevel3SimulationManager>().To<L3SimulationManager>();
        _level3SimulationManager = kernel.Get<ILevel3SimulationManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
      Init();
      SetupTimer();
    }

    #endregion

    #region public methods
		public static async void Start(StatusDelegate onStatus)
    {
      OnStatus = onStatus;

      try
      {
        List<string> sb = await _level3SimulationManager.CreateWorkOrders(LimitWeightMin, LimitWeightMax, LimitMaterialsMin, LimitMaterialsMax, ParameterOrderGeneartionDelay);
        if (sb != null)
          OnStatus?.Invoke(sb);
      }
      catch (Exception ex)
      {
        //OnStatus?.Invoke(ex);

      }
    }
    #endregion

    #region private methods

    private static async void Init()
    {
      ParameterController.Init("Simulation","");
      LimitController.Init("Simulation", "");

      ParameterOrderGeneartionDelay = ParameterController.GetParameter("SIM_WorkOrderGenerationDelay").ValueInt.GetValueOrDefault();
      LimitMaterialsMin = LimitController.GetLimit("SIM_WorkOrderNumMaterials").LowerValueInt.GetValueOrDefault();
      LimitMaterialsMax = LimitController.GetLimit("SIM_WorkOrderNumMaterials").UpperValueInt.GetValueOrDefault();

      try
      {
        List<string> sb = await _level3SimulationManager.CreateWorkOrders(LimitWeightMin, LimitWeightMax, LimitMaterialsMin, LimitMaterialsMax, ParameterOrderGeneartionDelay);
				if(sb!=null)
        OnStatus?.Invoke(sb);
      }
      catch (Exception ex)
      {
       // OnStatus?.Invoke(ex.ToString());
      }
    }

    private static void SetupTimer()
    {
      _l3Timer = new Timer();
      _l3Timer.AutoReset = true;
      _l3Timer.Interval = 20*1000;
      _l3Timer.Enabled = true;
      _l3Timer.Elapsed += TimerMethod;
      _l3Timer.Start();
    }
    private static async void TimerMethod(object sender, ElapsedEventArgs e)
    {
      try
      {
        List<string> sb = await _level3SimulationManager.CreateWorkOrders(LimitWeightMin, LimitWeightMax, LimitMaterialsMin, LimitMaterialsMax, ParameterOrderGeneartionDelay);
        if (sb != null)
          OnStatus?.Invoke(sb);
      }
      catch (Exception ex)
      {
        //OnStatus?.Invoke(ex.ToString());
      }

    }
    #endregion
  }
}
