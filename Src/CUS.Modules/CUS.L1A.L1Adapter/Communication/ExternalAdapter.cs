using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.External;
using PE.DTO.External.Setup;
using PE.DTO.Internal.Setup;
using PE.Interfaces.Interfaces.Custom;
using SMF.Core.DC;
using SMF.Module.Core;

namespace CUS.L1A.L1Adapter.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, IL1Adapter
  {
    #region ctor

    public ExternalAdapter() : base(typeof(IL1Adapter)) { }

    public async Task<DataContractBase> SendSetupDataRequestToL1AdapterAsync(DCCommonSetupStructure dataToSend)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.SendSetupDataRequestToL1AdapterAsync, dataToSend);
    }

    #endregion

    public async Task<DataContractBase> SendSetupDataToL1AdapterAsync(DCCommonSetupStructure dataToSend)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.SendSetupDataToL1AdapterAsync, dataToSend);
    }
  }
}
