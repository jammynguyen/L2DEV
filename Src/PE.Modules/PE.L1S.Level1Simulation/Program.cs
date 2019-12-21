using PE.DbEntity.Models;
using PE.L1S.Level1Simulation.Communication;
using PE.L1S.Level1Simulation.Module;
using SMF.Core.Log;
using SMF.Module.Core;
using SMF.Module.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PE.L1S.Level1Simulation
{
  public class Program
  {
    #region members

    private static int _timerRefreshCycle = 1000;
    //private static L1SimulationManager simulation;
    private static L1SimulationFurnace simFurnace;

    #endregion
    #region setup

    private static void Main(string[] args)
    {
      ModuleController.StartModule(ModuleInit, ModuleImplementationInit, PE.Interfaces.Module.Modules.L1Sim.Name);
    }
    private static void ModuleInit()
    {
      try
      {
        ModuleController.ExternalAdapter = new ExternalAdapter(ModuleController.ModuleName);
        //simulation = new L1SimulationManager();
        simFurnace = new L1SimulationFurnace(ModuleController.Logger);
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
        ModuleController.Logger.Info("Params were updated");
      }
      catch (Exception)
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

      //simFurnace.RequestBaseIdForMaterial();
      //if (!simulation.IsRunning)
      //{
      //  simulation.RunAsync();
      //}
    }

    #endregion
  }
}
