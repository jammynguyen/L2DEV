using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMF.Module.Core;


namespace PE.QTY.Quality
{
  class Program
  {
    static async Task Main(string[] args)
    {
      await ModuleController.StartModuleAsync();
    }
  }
}
