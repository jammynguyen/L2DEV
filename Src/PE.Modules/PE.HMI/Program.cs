using PE.HMI.Communication;
using PE.HMIWWW.Core.Signalr;
using SMF.Core.Helpers;
using SMF.HMI.Module;
using SMF.Module.Core;
using SMF.Module.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PE.HMI
{
	class Program
	{
    private static async Task Main(string[] args)
    {
      await ModuleController.StartModuleAsync();
    }
	}
}
