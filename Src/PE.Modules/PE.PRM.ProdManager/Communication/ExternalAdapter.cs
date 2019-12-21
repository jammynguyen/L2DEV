using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.Delay;
using PE.DTO.Internal.ProdManager;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.PRM.ProdManager.Communication
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public class ExternalAdapter : ExternalAdapterBase, PE.Interfaces.Lite.IProdManager
    {
        #region ctor

        public ExternalAdapter() : base(typeof(PE.Interfaces.Lite.IProdManager))
        {

        }

        #endregion

        #region HMI

        public async Task<DataContractBase> CreateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.CreateMaterialCatalogueAsync, billetCatalogue);
        }

        public async Task<DataContractBase> UpdateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.UpdateMaterialCatalogueAsync, billetCatalogue);
        }

        public async Task<DataContractBase> DeleteMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.DeleteMaterialCatalogueAsync, billetCatalogue);
        }

        public async Task<DataContractBase> CreateProductCatalogueAsync(DCProductCatalogue productCatalogue)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.CreateProductCatalogueAsync, productCatalogue);
        }

        public async Task<DataContractBase> UpdateProductCatalogueAsync(DCProductCatalogue productCatalogue)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.UpdateProductCatalogueAsync, productCatalogue);
        }

        public async Task<DataContractBase> DeleteProductCatalogueAsync(DCProductCatalogue productCatalogue)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.DeleteProductCatalogueAsync, productCatalogue);
        }

        public async Task<DataContractBase> CreateSteelgradeAsync(DCSteelgrade steelgrade)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.CreateSteelgradeAsync, steelgrade);
        }

        public async Task<DataContractBase> UpdateSteelgradeAsync(DCSteelgrade steelgrade)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.UpdateSteelgradeAsync, steelgrade);
        }

        public async Task<DataContractBase> DeleteSteelgradeAsync(DCSteelgrade steelgrade)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.DeleteSteelgradeAsync, steelgrade);
        }

        public async Task<DataContractBase> CreateWorkOrderAsync(DCWorkOrder workOrder)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.CreateWorkOrderAsync, workOrder);
        }
        public async Task<DataContractBase> UpdateWorkOrderAsync(DCWorkOrder workOrder)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.UpdateWorkOrderAsync, workOrder);
        }

        public async Task<DataContractBase> CreateMaterialAsync(DCMaterial dcMaterial)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.CreateMaterialAsync, dcMaterial);
        }
        public async Task<DataContractBase> UpdateMaterialAsync(DCMaterial dcMaterial)
        {
          return await HandleIncommingMethod(ExternalAdapterHandler.UpdateMaterialAsync, dcMaterial);
        }

    public async Task<DataContractBase> CheckMaterialsNumberAsync(DCMaterial dcMaterial)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.CheckMaterialsNumberAsync, dcMaterial);
        }

        public async Task<DataContractBase> CreateHeatAsync(DCHeat heat)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.CreateHeatAsync, heat);
        }

        #endregion

        #region L3

        public async Task<DCWorkOrderStatus> ProcessWorkOrderDataAsync(DCL3L2WorkOrderDefinition message)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.ProcessWorkOrderDataAsync, message);
        }

        #endregion

        #region MVH - internal system calls

        public async Task<DCProductData> ProcessProductionEndAsync(DCRawMaterialData materialData)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.MaterialProductionEndAsync, materialData);
        }

        public async Task<DataContractBase> DeleteWorkOrderAsync(DCWorkOrder workOrder)
        {
            return await HandleIncommingMethod(ExternalAdapterHandler.DeleteWorkOrderAsync, workOrder);
        }
        #endregion
    }
}
