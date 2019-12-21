using PE.DTO.Internal.MVHistory;
using PE.Interfaces.Lite;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.TRK.Tracking.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter: ExternalAdapterBase, ITracking
  {
    #region ctor

    public ExternalAdapter() : base(typeof(ITracking)) { }

    #endregion

    //public async Task<DataContractBase> ProcessTriggerAsync(DCTriggerData dcTriggerData)
    //{
    //  return await HandleIncommingMethod(ExternalAdapterHandler.ProcessTriggerAsync, dcTriggerData);
    //}

  }
}
