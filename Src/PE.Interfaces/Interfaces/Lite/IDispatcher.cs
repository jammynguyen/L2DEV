using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.MVHistory;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.Interfaces.Lite
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IDispatcher : IBaseModule
  {
    [OperationContract]
    Task<DataContractBase> AssetEventAsync(DCMeasData message);
    [OperationContract]
    Task<DataContractBase> ScrapEventAsync(DCL1ScrapData steelgrade);
  }
}
