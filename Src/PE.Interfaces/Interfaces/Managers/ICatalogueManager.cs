using PE.DTO.Internal.Delay;
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
    public interface ICatalogueManager : IManagerBase
    {
        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CreateSteelgradeCatalogueAsync(DCSteelgrade steelgrade);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> UpdateSteelgradeCatalogueAsync(DCSteelgrade steelgrade);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> DeleteSteelgradeAsync(DCSteelgrade steelgrade);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CreateHeatAsync(DCHeat heat);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CreateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> UpdateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> DeleteMaterialCatalogueAsync(DCMaterialCatalogue mCat);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CreateProductCatalogueAsync(DCProductCatalogue productCatalogue);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> UpdateProductCatalogueAsync(DCProductCatalogue productCatalogue);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> DeleteProductCatalogueAsync(DCProductCatalogue pCat);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> DeleteWorkOrderAsync(DCWorkOrder workOrder);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CreateMaterialAsync(DCMaterial material);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> UpdateMaterialAsync(DCMaterial material);

        [FaultContract(typeof(ModuleMessage))]
        Task<DataContractBase> CheckMaterialsNumberAsync(DCMaterial material);
    }
}
