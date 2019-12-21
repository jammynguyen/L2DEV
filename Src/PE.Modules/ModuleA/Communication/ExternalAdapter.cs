
using System.ServiceModel;
using PE.DTO;
using System.Threading.Tasks;
using System;
using PE.DTO.Internal.System;
using PE.ModuleA.Module;
using PE.DTO.Internal;
using SMF.Module.Core;

namespace PE.ModuleA.Communication
{
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
	public class ExternalAdapter : ExternalAdapterBase, PE.Interfaces.IModuleA
	{
    #region ctor
    public ExternalAdapter(string moduleName) : base(moduleName, typeof(PE.Interfaces.IModuleA)) { }
		#endregion
		#region interface
		public async Task<DCTestData> TestMethodOnModuleA(DCTestData message)
		{
			return await HandleExternalMethod(message, () => ExternalAdapterHandler.TestHandlingMethod(message));
		}
    #endregion
  }
}
