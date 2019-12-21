using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.Delay;
using PE.DTO.Internal.ProdManager;
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
    public interface IProdManager : IBaseModule
    {
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DCWorkOrderStatus> ProcessWorkOrderDataAsync(DCL3L2WorkOrderDefinition dataToSend);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CreateSteelgradeAsync(DCSteelgrade steelgrade);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> UpdateSteelgradeAsync(DCSteelgrade steelgrade);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> DeleteSteelgradeAsync(DCSteelgrade steelgradeId);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CreateWorkOrderAsync(DCWorkOrder workOrder);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> UpdateWorkOrderAsync(DCWorkOrder workOrder);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CreateMaterialAsync(DCMaterial dcMaterial);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> UpdateMaterialAsync(DCMaterial dcMaterial);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CheckMaterialsNumberAsync(DCMaterial dcMaterial);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CreateHeatAsync(DCHeat heat);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CreateProductCatalogueAsync(DCProductCatalogue productCatalogue);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> UpdateProductCatalogueAsync(DCProductCatalogue productCatalogue);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> DeleteProductCatalogueAsync(DCProductCatalogue pCatId);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CreateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> UpdateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> DeleteMaterialCatalogueAsync(DCMaterialCatalogue mCatId);
        //[OperationContract]
        //[FaultContract(typeof(ModuleMessage))]
        //Task<DataContractBase> ProcessRequestFirstScheduleMaterialAsync(DCWorkOrdersList dCWorkOrdersList);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DCProductData> ProcessProductionEndAsync(DCRawMaterialData materialData);
        [OperationContract]
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> DeleteWorkOrderAsync(DCWorkOrder workOrder);
    }
}
