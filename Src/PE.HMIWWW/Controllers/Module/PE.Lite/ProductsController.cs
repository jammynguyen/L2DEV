using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class ProductsController :BaseController
  {
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService service)
    {
      _productsService = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Products/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetProductsList([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _productsService.GetProductsSearchList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> ElementDetails(long ProductId)
    {
      return await PrepareActionResultFromVm(() => _productsService.GetProductsDetails(ModelState, ProductId), "~/Views/Module/PE.Lite/Products/_ProductsBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> GetProductsLabel(long ProductId)
    {
      return new FileStreamResult(_productsService.RequestPDFFromZebraWebServiceForHMIAsync(ModelState, ProductId).Result, "image/png");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> GetProductsLabelWithRawMaterialNameAsync(string rawMaterialName)
    {
      return new FileStreamResult(_productsService.GetProductsLabelWithRawMaterialNameAsync(ModelState, rawMaterialName).Result, "image/png");
    }

  }
}
