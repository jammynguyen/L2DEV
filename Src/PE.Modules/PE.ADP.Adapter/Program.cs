using SMF.Module.Core;
using System.Threading.Tasks;

namespace PE.ADP.Adapter
{
  internal class Program
  {
    private static async Task Main(string[] args)
    {
      await ModuleController.StartModuleAsync();
    }
  }
}
