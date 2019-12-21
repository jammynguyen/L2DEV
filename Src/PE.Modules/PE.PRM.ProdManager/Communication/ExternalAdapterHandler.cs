using Ninject;
using PE.Helpers;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.ProdManager;
using PE.Interfaces.Managers;
using PE.PRM.ProdManager.Module;
using PE.PRM.Managers;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.Interfaces.Interfaces.Managers;
using Ninject.Parameters;

namespace PE.PRM.ProdManager.Communication
{
    internal class ExternalAdapterHandler
    {
        private static readonly IWorkOrderManager _workOrderManager;
        private static readonly ICatalogueManager _catalogueManager;
        private static readonly IProductManager _productManager;

        static ExternalAdapterHandler()
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<IWorkOrderManager>().To<WorkOrderManager>();
                kernel.Bind<ICatalogueManager>().To<CatalogueManager>();
                kernel.Bind<IProductManager>().To<ProductManager>();

                _workOrderManager = kernel.Get<IWorkOrderManager>(new ConstructorArgument("sendOffice", new SendOffice()));
                _catalogueManager = kernel.Get<ICatalogueManager>(new ConstructorArgument("sendOffice", new SendOffice()));
                _productManager = kernel.Get<IProductManager>(new ConstructorArgument("sendOffice", new SendOffice()));
            }
        }

        internal static async Task<DCWorkOrderStatus> ProcessWorkOrderDataAsync(DCL3L2WorkOrderDefinition message)
        {
            return await _workOrderManager.ProcessWorkOrderDataAsync(message);
        }

        internal static async Task<DataContractBase> CreateSteelgradeAsync(DCSteelgrade steelgrade)
        {
            return await _catalogueManager.CreateSteelgradeCatalogueAsync(steelgrade);
        }

        internal static async Task<DataContractBase> UpdateSteelgradeAsync(DCSteelgrade steelgrade)
        {
            return await _catalogueManager.UpdateSteelgradeCatalogueAsync(steelgrade);
        }

        internal static async Task<DataContractBase> DeleteSteelgradeAsync(DCSteelgrade steelgrade)
        {
            return await _catalogueManager.DeleteSteelgradeAsync(steelgrade);
        }

        internal static async Task<DataContractBase> CreateWorkOrderAsync(DCWorkOrder workOrder)
        {
            return await _workOrderManager.CreateWorkOrderAsync(workOrder);
        }

        internal static async Task<DataContractBase> UpdateWorkOrderAsync(DCWorkOrder workOrder)
        {
            return await _workOrderManager.UpdateWorkOrderAsync(workOrder);
        }

        internal static async Task<DataContractBase> CreateMaterialAsync(DCMaterial dcMaterial)
        {
            return await _catalogueManager.CreateMaterialAsync(dcMaterial);
        }

        internal static async Task<DataContractBase> UpdateMaterialAsync(DCMaterial dcMaterial)
        {
          return await _catalogueManager.UpdateMaterialAsync(dcMaterial);
        }

        internal static async Task<DataContractBase> CheckMaterialsNumberAsync(DCMaterial dcMaterial)
        {
            return await _catalogueManager.CheckMaterialsNumberAsync(dcMaterial);
        }

        internal static async Task<DataContractBase> CreateHeatAsync(DCHeat heat)
        {
            return await _catalogueManager.CreateHeatAsync(heat);
        }

        internal static async Task<DataContractBase> CreateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue)
        {
            return await _catalogueManager.CreateMaterialCatalogueAsync(billetCatalogue);
        }

        internal static async Task<DataContractBase> UpdateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue)
        {
            return await _catalogueManager.UpdateMaterialCatalogueAsync(billetCatalogue);
        }

        internal static async Task<DataContractBase> DeleteMaterialCatalogueAsync(DCMaterialCatalogue mCat)
        {
            return await _catalogueManager.DeleteMaterialCatalogueAsync(mCat);
        }

        internal static async Task<DataContractBase> CreateProductCatalogueAsync(DCProductCatalogue productCatalogue)
        {
            return await _catalogueManager.CreateProductCatalogueAsync(productCatalogue);
        }

        internal static async Task<DataContractBase> UpdateProductCatalogueAsync(DCProductCatalogue productCatalogue)
        {
            return await _catalogueManager.UpdateProductCatalogueAsync(productCatalogue);
        }

        internal static async Task<DataContractBase> DeleteProductCatalogueAsync(DCProductCatalogue pCat)
        {
            return await _catalogueManager.DeleteProductCatalogueAsync(pCat);
        }

        internal static async Task<DCProductData> MaterialProductionEndAsync(DCRawMaterialData materialData)
        {
            return await _productManager.ProcessMaterialProductionFinishAsync(materialData);
        }

        internal static async Task<DataContractBase> DeleteWorkOrderAsync(DCWorkOrder workOrder)
        {
            return await _catalogueManager.DeleteWorkOrderAsync(workOrder);
        }
    }
}
