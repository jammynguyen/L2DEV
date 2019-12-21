using PE.ModuleA.Module;
using PE.DTO.Internal.System;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMF.RepositoryPatternExt;

namespace PE.ModuleA.Communication
{
	public static class ExternalAdapterHandler
	{
		public static DCTestData TestHandlingMethod(DCTestData data)
		{
      DCTestData retMessage = new DCTestData(); //TestHandler.DoSomething(data);
			retMessage.TestItem = "response from module A";
			retMessage.AddWarningMessage(ModuleADefs.AlarmCode_IdError, "some warning from module A", 321);
			//throw new ModuleMessageException(ModuleController.ModuleName, "PPL002", $"Critical error when adding work order [id:{111}] to schedule", 222);
			return retMessage;
		}
  }
}
