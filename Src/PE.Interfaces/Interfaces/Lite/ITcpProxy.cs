using PE.DTO.External;
using PE.DTO.External.MVHistory;
using PE.DTO.External.Setup;
using PE.DTO.Internal.TcpProxy.Configuration;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Interfaces.Lite
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface ITcpProxy :IBaseModule
  {
    [OperationContract]
    Task<DataContractBase> ProcessPEResponseWithBaseMaterialIdAsync(DCPEBasIdExt message);

    [OperationContract]
    Task<DCDefaultExt> SendTelegramSetupDataAsync(DCTCPSetpointTelegramEnvelopeExt message);
  }
}
