using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DSP.Dispatcher.Communication;
using PE.DSP.Dispatcher.Module;
using SMF.Module.Core;
using SMF.Module.Notification;

namespace PE.DSP.Dispatcher
{
  internal class Program
  {
    private static async Task Main(string[] args)
    {
      await ModuleController.StartModuleAsync();
    }
  }
}
