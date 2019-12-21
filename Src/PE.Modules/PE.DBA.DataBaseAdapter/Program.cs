using SMF.Module.Core;
using System.Threading.Tasks;

namespace PE.DBA.DataBaseAdapter
{
  internal class Program
  {
    private static async Task Main(string[] args)
    {
      await ModuleController.StartModuleAsync(1000);
    }
  }
}
