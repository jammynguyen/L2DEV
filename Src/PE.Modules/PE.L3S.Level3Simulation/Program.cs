using PE.Lite.L3Sim.Module;
using SMF.Core.Log;
using SMF.Module.Core;
using SMF.Module.Parameter;
using System;
using System.Timers;

namespace PE.Lite.L3Sim
{
  public class Program
  {
    #region members

    private static int _timerRefreshCycle = 4000;
    private static WorkOrderSimulator _workOrderSimulator;
    private static readonly string _moduleName = PE.Interfaces.Module.Modules.L3Sim.Name;

    #endregion
    #region setup

    private static void Main(string[] args)
    {
      ModuleController.StartModule(ModuleInit, ModuleImplementationInit, _moduleName, ModuleController.GetSmfConnectionString());
    }
    private static void ModuleInit()
    {
      try
      {
        _workOrderSimulator = new WorkOrderSimulator();
        ModuleController.SetupTimer(_timerRefreshCycle, true, TimerMethod);
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ex);
      }

      // events
      ModuleController.ModuleStarted += ModuleController_ModuleStarted;
      ModuleController.ModuleInitialized += ModuleController_ModuleInitialized;
      ModuleController.ModuleClosed += ModuleController_ModuleClosed;
      ModuleController.ParametersChanged += ModuleController_ParametersChanged;
    }

    private static void ModuleImplementationInit()
    {
      try
      {
        ModuleController.StartTimer();
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ex);
      }
    }

    #endregion
    #region module events

    private static void ModuleController_ParametersChanged(object sender, ParametersChangedEventArgs e)
    {
      try
      {
        _workOrderSimulator.InitParamsAndLimits();
        ModuleController.Logger.Info("Params were updated");
      }
      catch(Exception)
      {
        ModuleController.Logger.Error("Error while params update");
      }
    }

    private static void ModuleController_ModuleClosed(object sender, ModuleCloseEventArgs e)
    {
      try
      {
        ModuleController.Logger.Info("Module Closed");
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ex);
      }
    }
    private static void ModuleController_ModuleInitialized(object sender, ModuleInitEventArgs e)
    {
      try
      {
        ModuleController.Logger.Info("Module Initialized");
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ex);
      }

    }
    private static void ModuleController_ModuleStarted(object sender, ModuleStartEventArgs e)
    {
      try
      {
        ModuleController.Logger.Info("Module Started");
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ex);
      }
    }
    private static void TimerMethod(object sender, ElapsedEventArgs e)
    {
      _workOrderSimulator.Run();
    }

    #endregion
  }
}
