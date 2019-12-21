using Ninject;
using Ninject.Parameters;
using PE.DTO.Internal.MVHistory;
using PE.Interfaces.Interfaces.Managers;
using PE.TRK.Managers;
using PE.TRK.Tracking.Module;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.TRK.Tracking.Communication
{
  internal class ExternalAdapterHandler
  {
    private static readonly ITrackingTriggersManager _trackingTriggersManager;

    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<ITrackingTriggersManager>().To<TrackingTriggersManager>();

        _trackingTriggersManager = kernel.Get<ITrackingTriggersManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    //internal static async Task<DataContractBase> ProcessTriggerAsync(DCTriggerData dcTriggerData)
    //{
    //  return await _trackingTriggersManager.ProcessTrigger(dcTriggerData);
    //}
  }
}
