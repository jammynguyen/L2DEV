using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using PE.SIM.Simulation.Communication;
using PE.SIM.Simulation.Module;
using SMF.Module.Core;
using SMF.Module.Notification;

namespace PE.SIM.Simulation
{
  class Program
  {
    private static async Task Main(string[] args)
    {
      await ModuleController.StartModuleAsync(1000);
    }
  }
}
