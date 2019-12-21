using PE.ZPC.ZebraPrinterConnector.Communication;
using PE.ZPC.ZebraPrinterConnector.Module;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.Module.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PE.ZPC.ZebraPrinterConnector
{
  internal class Program
  {
    private static async Task Main(string[] args)
    {
      await ModuleController.StartModuleAsync();
    }
  }
}
