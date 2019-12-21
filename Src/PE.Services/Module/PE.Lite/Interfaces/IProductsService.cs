using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Products;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IProductsService
  {
    DataSourceResult GetProductsSearchList(ModelStateDictionary modelState, DataSourceRequest request);
    VM_ProductsOverview GetProductsDetails(ModelStateDictionary modelState, long productId);
    Task<Stream> RequestPDFFromZebraWebServiceForHMIAsync(ModelStateDictionary modelState, long productId);

    Task<Stream> GetProductsLabelWithRawMaterialNameAsync(ModelStateDictionary modelState, string rawMaterialName);
  }
}
