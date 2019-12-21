using PE.DTO.Internal.System;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PE.Interfaces
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IModuleB : IBaseModule
  {
    [OperationContract]
    Task<DCTestData> TestMethodOnModuleB(DCTestData message);
    [OperationContract]
    Task<DCTestData> TestMethodForWarningOnModuleB(DCTestData entryDataContract);
  }
}
