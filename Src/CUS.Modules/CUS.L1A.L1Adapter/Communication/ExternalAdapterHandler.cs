using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUS.L1A.Managers;
using Ninject;
using Ninject.Parameters;
using PE.DTO.External;
using PE.DTO.External.Setup;
using PE.DTO.Internal.Setup;
using PE.Interfaces.Interfaces.Managers.Custom;
using SMF.Core.DC;

namespace CUS.L1A.L1Adapter.Communication
{
  internal class ExternalAdapterHandler
  {
    #region members

    private static readonly IL1SetupManager _level1SetupManager;
    private static readonly IL1TrackingManager _level1TrackingManager;

    #endregion
    #region ctor

    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IL1SetupManager>().To<L1SetupManager>();
        _level1SetupManager = kernel.Get<IL1SetupManager>(new ConstructorArgument("sendOffice", new SendOffice()));

        kernel.Bind<IL1TrackingManager>().To<L1TrackingManager>();
        _level1TrackingManager = kernel.Get<IL1TrackingManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    #endregion

    internal static async Task<DataContractBase> SendSetupDataToL1AdapterAsync(DCCommonSetupStructure message)
    {
      // return result
      return await _level1SetupManager.SendSetupMessageAsync(message);
    }
    internal static async Task<DataContractBase> SendSetupDataRequestToL1AdapterAsync(DCCommonSetupStructure message)
    {
      // return result
      return await _level1SetupManager.HandleSetupRequestMessageAsync(message);
    }
    
  }
}
