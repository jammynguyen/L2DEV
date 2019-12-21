using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.ProdManager;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers
{
    public interface IWorkOrderManager : IManagerBase
    {
        [FaultContract(typeof(ModuleMessage))]
        Task<DCWorkOrderStatus> ProcessWorkOrderDataAsync(DCL3L2WorkOrderDefinition message);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CreateWorkOrderAsync(DCWorkOrder workOrder);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> UpdateWorkOrderAsync(DCWorkOrder workOrder);

        //[FaultContract(typeof(ModuleMessage))]
        //Task<DataContractBase> FindFirstFreeScheduledMaterialAsync(DCWorkOrdersList dCWorkOrdersList);
    }
}
