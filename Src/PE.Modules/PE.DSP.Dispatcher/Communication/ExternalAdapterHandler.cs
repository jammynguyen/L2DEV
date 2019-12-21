using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using PE.DTO.Internal.Setup;
using PE.Interfaces.Managers;
using SMF.Core.DC;
using PE.DTO.Internal.TcpProxy;
using Ninject.Parameters;
using PE.DSP.Managers;
using PE.DTO.Internal.MVHistory;

namespace PE.DSP.Dispatcher.Communication
{
  internal class ExternalAdapterHandler
  {
    private static readonly IDispatcherManager _dispatcherManager;

    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IDispatcherManager>().To<DispatcherManager>();
        _dispatcherManager = kernel.Get<IDispatcherManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    internal static async Task<DataContractBase> ProcessAssetEventAsync(DCMeasData dcAssetEvent)
    {
      return await _dispatcherManager.ProcessAssetEventAsync(dcAssetEvent);
    }

    internal static async Task<DataContractBase> ProcessScrapEventAsync(DCL1ScrapData dcScrapEvent)
    {
      return await _dispatcherManager.ProcessScrapEventAsync(dcScrapEvent);
    }
  }
}
