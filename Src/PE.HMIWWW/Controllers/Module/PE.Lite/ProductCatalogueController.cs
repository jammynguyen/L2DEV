using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class ProductCatalogueController : BaseController
  {
    private readonly IProductService _service;

    public ProductCatalogueController(IProductService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Product, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Product/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Product, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> GetProductCatalogueList([DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = _service.GetProductCatalogueList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Product, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public JsonResult GetSteelgradeList()
    {
      return Json(_service.GetSteelgradeList(), JsonRequestBehavior.AllowGet);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Product, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public JsonResult GetShapeList()
    {
      return Json(_service.GetShapeList(), JsonRequestBehavior.AllowGet);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Product, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> ProductCatalogueCreatePopup()
    {
      ViewBag.SteelgradeList = _service.GetSteelgradeList();
      ViewBag.ShapeList = _service.GetShapeList();
      ViewBag.TypeList = _service.GetProductCatalogueTypeList();
      VM_ProductCatalogue result = new VM_ProductCatalogue();
      return await PreparePopupActionResultFromVm(() => result, "~/Views/Module/PE.Lite/Product/_ProductCatalogueCreatePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Product, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    [HttpPost]
    public async Task<ActionResult> CreateProductCatalogue(VM_ProductCatalogue productCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.CreateProductCatalogue(ModelState, productCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Product, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> ProductCatalogueEditPopup (long id)
    {
      ViewBag.SteelgradeList = _service.GetSteelgradeList();
      ViewBag.ShapeList = _service.GetShapeList();
      ViewBag.TypeList = _service.GetProductCatalogueTypeList();
      VM_ProductCatalogue result = _service.GetProductCatalogue(id);
      return await PreparePopupActionResultFromVm(() => result, "~/Views/Module/PE.Lite/Product/_ProductCatalogueEditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Product, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    [HttpPost]
    public async Task<ActionResult> UpdateMaterialCatalogue(VM_ProductCatalogue productCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.UpdateProductCatalogue(ModelState, productCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Product, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> ElementDetails(long id)
    {
      return await PrepareActionResultFromVm(() => _service.GetProductDetails(ModelState, id), "~/Views/Module/PE.Lite/Product/_ProductBody.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Product, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Delete)]
    public async Task<JsonResult> DeleteProductCatalogue(long itemId)
    {
      VM_ProductCatalogue productCat = new VM_ProductCatalogue()
      {
        Id = itemId
      };
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteProductCatalogue(ModelState, productCat));
    }

  }
}
