using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.RLS.RollShop.Communication;
using PE.RLS.RollShop.Module;
using SMF.Module.Core;

namespace PE.RLS.RollShop
{
  internal class Program
  {
    private static async Task Main(string[] args)
    {
      await ModuleController.StartModuleAsync();
    }
  }
}
