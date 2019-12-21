using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMF.Module.Core;

namespace CUS.L1A.L1Adapter
{
  class Program
  {
    private static async Task Main(string[] args)
    {
      await ModuleController.StartModuleAsync(1000);
    }
  }
}
