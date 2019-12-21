using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IProductService
  {
    DataSourceResult GetProductCatalogueList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    VM_ProductCatalogue GetProductCatalogue(long id);
    Task<VM_Base> CreateProductCatalogue(ModelStateDictionary modelState, VM_ProductCatalogue productCatalogue);
    Task<VM_Base> UpdateProductCatalogue(ModelStateDictionary modelState, VM_ProductCatalogue productCatalogue);
    Task<VM_Base> DeleteProductCatalogue(ModelStateDictionary modelState, VM_ProductCatalogue productCatalogue);
    IList<VM_Steelgrade> GetSteelgradeList();
    IList<VM_Shape> GetShapeList();
    IList<VM_ProductCatalogueType> GetProductCatalogueTypeList();
    VM_ProductCatalogue GetProductDetails(ModelStateDictionary modelState, long id);
  }
}
