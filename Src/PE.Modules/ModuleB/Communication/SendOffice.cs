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

namespace PE.ModuleB.Communication
{
	public static class SendOffice
	{
        #region External calls

        public static async Task<SendOfficeResult<DCTestData>> SendMessageToA(DCTestData entryDataContract)
        {
            //prepare target module name and interface
            string targetModuleName = PE.Interfaces.Module.Modules.ModuleA.Name;
            IModuleA client = InterfaceHelper.GetFactoryChannel<IModuleA>(targetModuleName);

            //call method on remote module
            return await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.TestMethodOnModuleA(entryDataContract));
        }

        #endregion
    }
}
