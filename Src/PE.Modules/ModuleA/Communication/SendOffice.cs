using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.Interfaces;
using PE.DTO;
using SMF.Core.Helpers;
using PE.DTO.Internal.System;
using SMF.Core.DC;

namespace PE.ModuleA.Communication
{

	public static class SendOffice
	{
		#region External calls

		public static async Task<SendOfficeResult<DCTestData>> SendMessageToB(DCTestData entryDataContract)
		{
			//prepare target module name and interface
			string targetModuleName = PE.Interfaces.Module.Modules.ModuleB.Name;
			IModuleB client = InterfaceHelper.GetFactoryChannel<IModuleB>(targetModuleName);

			//call method on remote module
			return await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.TestMethodOnModuleB(entryDataContract));
		}
    internal static async Task<SendOfficeResult<DCTestData>> SendMessageToBForWarningAsync(DCTestData entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ModuleB.Name;
      IModuleB client = InterfaceHelper.GetFactoryChannel<IModuleB>(targetModuleName);

      //call method on remote module
      return await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.TestMethodForWarningOnModuleB(entryDataContract));
    }
    #endregion
  }
}
