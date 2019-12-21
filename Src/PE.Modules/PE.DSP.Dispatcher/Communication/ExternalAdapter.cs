using PE.DTO.Internal.MVHistory;
using SMF.Core.DC;
using SMF.Module.Core;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PE.DSP.Dispatcher.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, Interfaces.Lite.IDispatcher
  {
    #region ctor
    public ExternalAdapter() : base( typeof(PE.Interfaces.Lite.IDispatcher)) { }
    #endregion

    public async Task<DataContractBase> AssetEventAsync(DCMeasData dcAssetEvent)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ProcessAssetEventAsync, dcAssetEvent);
    }

    public async Task<DataContractBase> ScrapEventAsync(DCL1ScrapData dcScrapEvent)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ProcessScrapEventAsync, dcScrapEvent);
    }
  }
}
