using PE.Interfaces.DC.Internal.System;
using SMF.Module.Core;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.Handlers.Test
{
  public static class TestHandler
  {
    #region Constants
    private const int SimulatedExecutionTime = 5000;
    #endregion

    public static DCTestData DoSomething(DCTestData message)
    {
      DCTestData result = new DCTestData()
      {
        TestItem = $"Pong: {message.TestItem}"
      };
      ModuleController.InitDataContract(result);

      Task.Delay(SimulatedExecutionTime);

      return result;
    }
  }
}
