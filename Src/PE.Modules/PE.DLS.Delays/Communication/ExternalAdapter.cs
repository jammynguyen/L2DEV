using PE.DTO.Internal.Delay;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.DLS.Delays.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, PE.Interfaces.Lite.IDelay
  {
    #region ctor
    public ExternalAdapter() : base(typeof(PE.Interfaces.Lite.IDelay)) { }
    #endregion

    #region HMI

    public async Task<DataContractBase> AddDelayCatalogueAsync(DCDelayCatalogue delayCatalogue)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.AddDelayCatalogueAsync, delayCatalogue);
    }
    public async Task<DataContractBase> UpdateDelayCatalogueAsync(DCDelayCatalogue delayCatalogue)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateDelayCatalogueAsync, delayCatalogue);
    }
    public async Task<DataContractBase> UpdateDelayAsync(DCDelay delay)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateDelayAsync, delay);
    }
    public async Task<DataContractBase> DeleteDelayCatalogueAsync(DCDelayCatalogue delayCatalogue)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.DeleteDelayCatalogueAsync, delayCatalogue);
    }

    public async Task<DataContractBase> DivideDelayAsync(DCDelayToDivide delayToDivide)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.DivideDelayAsync, delayToDivide);
    }

    #endregion

    #region MVH

    public async Task<DataContractBase> ProcessHeadEnterAsync(DCDelayEvent dcDelayEvent)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ProcessHeadEnterAsync, dcDelayEvent);
    }

    public async Task<DataContractBase> ProcessTailLeavesAsync(DCDelayEvent dcDelayEvent)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ProcessTailLeavesAsync, dcDelayEvent);
    }

    #endregion

  }
}
