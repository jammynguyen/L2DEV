using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.Schedule;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers
{
  public interface IScheduleManager : IManagerBase
  {
		[FaultContract(typeof(ModuleMessage))]
		Task<DataContractBase> AddWorkOrderToScheduleAsync(DCWorkOrderToSchedule dc);

		[FaultContract(typeof(ModuleMessage))]
		Task<DataContractBase> AddTestWorkOrderToScheduleAsync(DCTestSchedule dc);

    [FaultContract(typeof(ModuleMessage))]
		Task<DataContractBase> MoveItemInScheduleAsync(DCWorkOrderToSchedule dc);

		[FaultContract(typeof(ModuleMessage))]
		Task<DataContractBase> RemoveItemFromScheduleAsync(DCWorkOrderToSchedule dc);

		[FaultContract(typeof(ModuleMessage))]
		Task<DCL1L3MaterialConnector> RequestL3MaterialForRawMaterialAsync(DCFeatureRelatedOperationData data);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RemoveFinishedOrdersFromScheduleAsync(DataContractBase message);
  }
}
