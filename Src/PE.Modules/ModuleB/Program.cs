using PE.ModuleB.Communication;
using PE.ModuleB.Module;
using SMF.Core.Log;
using SMF.Module.Core;
using SMF.Module.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PE.ModuleB
{
	class Program
	{
		#region members

		//private static SignalRClient _signalRClient;
		private static int _timerRefreshCycle = 10000;

		#endregion
		#region setup

		private static void Main(string[] args)
		{
			ModuleController.StartModule(ModuleInit, ModuleImplementationInit, "ModuleB");
		}
		private static void ModuleInit()
		{
			try
			{
				ModuleController.ExternalAdapter = new ExternalAdapter(ModuleController.ModuleName);
				//_signalRClient = new SignalRClient();
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
				//ModuleController.HmiRefresh("HMI");
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
           Task t = TestHandler.SendResults();

            //NotificationController.AlarmAsync("S008", "test alarm only", cnt++);
        }

		#endregion
	}
}
