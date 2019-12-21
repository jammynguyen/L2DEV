using Ninject;
using Ninject.Parameters;
using PE.DLS.Delays.Module;
using PE.DLS.Managers;
using PE.DTO.Internal.Delay;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using PE.Interfaces.Managers;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.DLS.Delays.Communication
{
  internal static class ExternalAdapterHandler
  {
    private static readonly IDelayCatalogueManager _delayCatalogueManager;
    private static readonly IDelayManager _delayManager;

    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IDelayCatalogueManager>().To<DelayCatalogueManager>();
        kernel.Bind<IDelayManager>().To<DelayManager>();


        _delayCatalogueManager = kernel.Get<IDelayCatalogueManager>(new ConstructorArgument("sendOffice", new SendOffice()));
        _delayManager = kernel.Get<IDelayManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    internal static async Task<DataContractBase> AddDelayCatalogueAsync(DCDelayCatalogue delayCatalogue)
    {
      return await _delayCatalogueManager.AddDelayCatalogueAsync(delayCatalogue);
    }

    internal static async Task<DataContractBase> UpdateDelayCatalogueAsync(DCDelayCatalogue delayCatalogue)
    {
      return await _delayCatalogueManager.UpdateDelayCatalogueAsync(delayCatalogue);
    }

    internal static async Task<DataContractBase> DeleteDelayCatalogueAsync(DCDelayCatalogue delayCatalogue)
    {
      return await _delayCatalogueManager.DeleteDelayCatalogueAsync(delayCatalogue);
    }

    internal static async Task<DataContractBase> ProcessHeadEnterAsync(DCDelayEvent dcDelayEvent)
    {
      return await _delayManager.ProcessHeadEnterAsync(dcDelayEvent);
    }

    internal static async Task<DataContractBase> ProcessTailLeavesAsync(DCDelayEvent dcDelayEvent)
    {
      return await _delayManager.ProcessTailLeavesAsync(dcDelayEvent);
    }

    internal static async Task<DataContractBase> UpdateDelayAsync(DCDelay delay)
    {
      return await _delayManager.UpdateDelayAsync(delay);
    }

    internal static async Task<DataContractBase> DivideDelayAsync(DCDelayToDivide delayToDivide)
    {
      return await _delayManager.DivideDelayAsync(delayToDivide);
    }

    internal static async Task<bool> UpdateCurrentDelayStatusAsync()
    {
      return await _delayManager.UpdateCurrentDelayStatusAsync();
    }
  }
}
