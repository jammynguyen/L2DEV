using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.DBAdapter;
using PE.DTO.Internal.System;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.MVHistory;
using PE.DTO.External.MVHistory;
using PE.DTO.Internal.TcpProxy;
using PE.DTO.External.DBAdapter;
using PE.DTO.External.Setup;
using PE.DTO.Internal.Setup;
using PE.DTO.External;

namespace PE.Interfaces.Lite
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IAdapter : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCWorkOrderStatusExt> ExternalProccesWorkOrderMessageAsync(DCL3L2WorkOrderDefinitionExt message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCPEBasIdExt> L1BaseIdRequestAsync(DCL1BasIdRequestExt message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCDefaultExt> L1CutMessageAsync(DCL1CutDataExt message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCPEBasIdExt> L1DivisionMessageAsync(DCL1MaterialDivisionExt message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCDefaultExt> L1ScrapMessageAsync(DCL1ScrapDataExt message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCDefaultExt> L1MeasDataMessageAsync(DCMeasDataExt message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCDefaultExt> L1SampleMeasMessageAsync(DCMeasDataSampleExt message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> SendSetupDataAsync(DCCommonSetupStructure message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> SendSetupDataRequestAsync(DCCommonSetupStructure message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> L1SetupUpdateMessageAsync(DCCommonSetupStructure message);
    
  }
}
