using System.ServiceModel;
using SMF.Module.Core;
using System.Threading.Tasks;
using PE.DTO.Internal.System;

namespace PE.ModuleB.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, PE.Interfaces.IModuleB
  {
    #region ctor

    public ExternalAdapter(string moduleName) : base(moduleName, typeof(PE.Interfaces.IModuleB)) { }


    #endregion
    #region interface
    public async Task<DCTestData> TestMethodOnModuleB(DCTestData message)
    {
      return await HandleExternalMethod(message, () => ExternalAdapterHandler.TestHandlingMethod(message));
    }
    public async Task<DCTestData> TestMethodForWarningOnModuleB(DCTestData message)
    {
      return await HandleExternalMethod(message, () => ExternalAdapterHandler.TestHandlingMethodForWarning(message));
    }
    #endregion
  }
}
