using PE.DLS.Delay.Module;
using PE.DLS.Delays.Communication;
using SMF.Core.Helpers;
using SMF.Module.Core;
using SMF.Module.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PE.DLS.Delays
{
  class Program
  {
    private static async Task Main(string[] args)
    {
      await ModuleController.StartModuleAsync();
    }
  }
}
