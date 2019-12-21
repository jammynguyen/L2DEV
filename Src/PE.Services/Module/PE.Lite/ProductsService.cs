using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Products;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using PE.DTO.Internal.ZebraPrinter;
using SMF.Module.Core;
using PE.HMIWWW.Core.Communication;
using System.Threading.Tasks;
using System;
using SMF.RepositoryPatternExt;
using SMF.Module.Notification;
using System.Reflection;
using System.Text;
using System.Drawing;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ProductsService : BaseService, IProductsService
  {
    public VM_ProductsOverview GetProductsDetails(ModelStateDictionary modelState, long productId)
    {
      VM_ProductsOverview result = null;

      if (productId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      using (PEContext ctx = new PEContext())
      {
        PRMProduct product = ctx.PRMProducts
          .Where(x => x.ProductId == productId)
          .Include(i => i.MVHRawMaterials)
          .Include(i => i.PRMWorkOrder)
          .Include(i => i.PRMWorkOrder.PRMMaterials)
          .Include(i => i.PRMWorkOrder.PRMProductCatalogue)
          .Include(i => i.PRMWorkOrder.PRMMaterialCatalogue)
          .Include(i => i.PRMWorkOrder.PRMMaterialCatalogue.PRMSteelgrade)
          .Single();

        PRMHeat heat = ctx.PRMMaterials.Where(x => x.FKWorkOrderId == product.FKWorkOrderId).Select(x => x.PRMHeat).Include(x => x.PRMHeatSupplier).Include(x => x.PRMMaterialCatalogue).Include(x => x.PRMMaterialCatalogue.PRMSteelgrade).Include(x => x.PRMHeatChemAnalysis).FirstOrDefault();
        product.PRMWorkOrder.PRMMaterials.FirstOrDefault().PRMHeat = heat;

        result = new VM_ProductsOverview(product);
      }

      return result;
    }

    public DataSourceResult GetProductsSearchList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      using (PEContext ctx = new PEContext())
      {
        IQueryable<V_Products> workOrderList = ctx.V_Products.AsQueryable();
        return workOrderList.ToDataSourceResult<V_Products, VM_ProductsList>(request, modelState, data => new VM_ProductsList(data));
      }
    }

    public async Task<Stream> RequestPDFFromZebraWebServiceForHMIAsync(ModelStateDictionary modelState, long productId=1)
    {
			//PREPARE EXIT OBJECT
			Stream returnStream = default(Stream);

			//VALIDATE ENTRY PARAMETERS
			if (!modelState.IsValid)
			{
				return returnStream;
			}

			if (productId < 1)
			{
				return returnStream;
			}

			//END OF VALIDATION

			DCZebraRequest entryDataContract = new DCZebraRequest();

			entryDataContract.ProductId = productId;

			SendOfficeResult<DCZebraResponse> sendOfficeResult = await HmiSendOffice.RequestPDFFromZebraAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

			if (sendOfficeResult.OperationSuccess)
				returnStream = new MemoryStream(sendOfficeResult.DataConctract.Picture);
			

			return returnStream;
    }

    public async Task<Stream> GetProductsLabelWithRawMaterialNameAsync(ModelStateDictionary modelState, string rawMaterialName)
    {
      //PREPARE EXIT OBJECT
      Stream returnStream = default(Stream);
      V_ProductOverview product = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnStream;
      }

      if (rawMaterialName.Length < 1)
      {
        return returnStream;
      }

      using (PEContext ctx = new PEContext())
      {
         product = ctx.V_ProductOverview
          .Where(x => x.RawMaterialName == rawMaterialName)
          .Single();
      }

      //END OF VALIDATION

      DCZebraRequest entryDataContract = new DCZebraRequest();

      entryDataContract.ProductId = product.ProductId;

      SendOfficeResult<DCZebraResponse> sendOfficeResult = await HmiSendOffice.RequestPDFFromZebraAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      if (sendOfficeResult.OperationSuccess)
        returnStream = new MemoryStream(sendOfficeResult.DataConctract.Picture);


      return returnStream;
    }
  }
}
