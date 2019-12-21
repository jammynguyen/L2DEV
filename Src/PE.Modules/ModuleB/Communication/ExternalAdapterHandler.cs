using PE.DTO.Internal.System;
using PE.ModuleB.Module;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.ModuleB.Communication
{
	public static class ExternalAdapterHandler
	{
		public static DCTestData TestHandlingMethod(DCTestData data)
		{
			DCTestData retMessage = new DCTestData();

			if (TestHandler.IncommingMessageHandler(data.TestItem))
				retMessage.TestItem = "Method return state OK";
			else
				retMessage.TestItem = "Method return state ERROR";

			return retMessage;
		}

    internal static DCTestData TestHandlingMethodForWarning(DCTestData message)
    {
      DCTestData retMessage = new DCTestData();
      retMessage.AddWarningMessage(ModuleBDefs.AlarmCode_IdError,"module b warning",123);
      return retMessage;
    }
  }
}
