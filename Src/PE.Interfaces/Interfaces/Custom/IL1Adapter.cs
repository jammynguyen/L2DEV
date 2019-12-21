using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.External;
using PE.DTO.External.Setup;
using PE.DTO.Internal.Setup;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.Interfaces.Interfaces.Custom
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IL1Adapter : IBaseModule
  {
    [OperationContract]
    Task<DataContractBase> SendSetupDataToL1AdapterAsync(DCCommonSetupStructure dataToSend);
    [OperationContract]
    Task<DataContractBase> SendSetupDataRequestToL1AdapterAsync(DCCommonSetupStructure dataToSend);
    
  }
}
