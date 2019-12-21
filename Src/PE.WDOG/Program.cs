using NLog;
using SMF.Core.Log;
using SMF.Wdog.Server;
//using SMF.Wdog.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PE.WDOG
{
	class Program
	{
		private static Logger _logger = LogManager.GetCurrentClassLogger();
		private static WdogServerController _wdogServerController;

		static void Main(string[] args)
		{
			string appGuid = ((GuidAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value.ToString();
			string mutexId = string.Format("Global\\{{{0}}}", appGuid);

			using (var mutex = new Mutex(false, mutexId))
			{
				var allowEveryoneRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow);
				var securitySettings = new MutexSecurity();
				securitySettings.AddAccessRule(allowEveryoneRule);
				mutex.SetAccessControl(securitySettings);
				var hasHandle = false;
				try
				{
					try
					{
						hasHandle = mutex.WaitOne(1000, false);
						if (hasHandle == false)
						{
							Console.WriteLine("Program already running");
							Environment.Exit(0);
						}
					}
					catch (AbandonedMutexException)
					{
						hasHandle = true;
					}
					try
					{
						AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

						LogHelper.AddHeadertext("Wdog");
						_wdogServerController = new WdogServerController();
						_wdogServerController.InitializeWdog();
						Console.WriteLine("Init done");
						//_wdogServerController.StartAll();
						Console.ReadLine();
						_wdogServerController.TerminateAll();
					}
					catch (Exception ex)
					{
						Console.WriteLine("Program start error");
						Console.WriteLine(ex.Message);
					}
				}
				finally
				{
					if (hasHandle)
						mutex.ReleaseMutex();
				}
			}
		}

		private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs ex)
		{
			LogHelper.LogUnhandledException(ex);
			if (ex.IsTerminating)
			{
				_wdogServerController.Stop("");
				Environment.Exit(1);
			}
		}
	}
}
