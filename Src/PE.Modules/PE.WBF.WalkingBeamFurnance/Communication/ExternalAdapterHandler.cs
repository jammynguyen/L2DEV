using Ninject;
using PE.DTO.Internal.MVHistory;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.Interfaces.Managers;
using PE.WBF.WalkingBeamFurnance.Module;
using Ninject.Parameters;
using PE.WBF.Managers;

namespace PE.WBF.WalkingBeamFurnance.Communication
{
  internal class ExternalAdapterHandler
  {
    private static readonly IFurnanceTriggersManager _furnanceTriggersManager;

    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IFurnanceTriggersManager>().To<FurnanceTriggersManager>();

        _furnanceTriggersManager = kernel.Get<IFurnanceTriggersManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    internal static async Task<DataContractBase> ProcessTriggerAsync(DCTriggerData dcTriggerData)
    {
      return await _furnanceTriggersManager.ProcessTriggerAsync(dcTriggerData);
    }
  }
}
