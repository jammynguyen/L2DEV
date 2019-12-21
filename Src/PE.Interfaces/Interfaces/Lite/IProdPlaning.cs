using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.Schedule;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Lite
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IProdPlaning : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddWorkOrderToScheduleAsync(DCWorkOrderToSchedule data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RemoveItemFromScheduleAsync(DCWorkOrderToSchedule data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
		//[System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
		//[System.ServiceModel.ServiceKnownTypeAttribute(typeof(ModuleMessage))]
		Task<DataContractBase> MoveItemInScheduleAsync(DCWorkOrderToSchedule data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddTestWorkOrderToScheduleAsync(DCTestSchedule dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
		Task<DCL1L3MaterialConnector> RequestL3MaterialAsync(DCFeatureRelatedOperationData data);


    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RemoveFinishedOrdersFromScheduleAsync(DataContractBase data);
    
  }
}
