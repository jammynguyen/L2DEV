using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class BilletCatalogueController : BaseController
  {
    private readonly IBilletService _service;

    public BilletCatalogueController(IBilletService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Billet/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetMaterialCatalogueList([DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = _service.GetProductCatalogueList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> EditMaterialCataloguePopup(long id)
    {
      ViewBag.SteelgradeList = _service.GetSteelgradeList();
      ViewBag.ShapeList = _service.GetShapeList();
      ViewBag.TypeList = _service.GetMaterialCatalogueTypeList();
      VM_MaterialCatalogue result = _service.GetMaterialCatalogue(id);
      return await PreparePopupActionResultFromVm(() => result, "~/Views/Module/PE.Lite/Billet/_MaterialCatalogueEditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> UpdateMaterialCatalogue(VM_MaterialCatalogue materialCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.UpdateMaterialCatalogue(ModelState, materialCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> CreateMaterialCataloguePopup( )
    {
      ViewBag.SteelgradeList = _service.GetSteelgradeList();
      ViewBag.ShapeList = _service.GetShapeList();
      ViewBag.TypeList = _service.GetMaterialCatalogueTypeList();
      VM_MaterialCatalogue result = new VM_MaterialCatalogue();
      return await PreparePopupActionResultFromVm(() => result, "~/Views/Module/PE.Lite/Billet/_MaterialCatalogueCreatePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> CreateMaterialCatalogue(VM_MaterialCatalogue materialCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.CreateMaterialCatalogue(ModelState, materialCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> ElementDetails(long id)
    {
      return await PrepareActionResultFromVm(() => _service.GetBilletDetails(ModelState, id), "~/Views/Module/PE.Lite/Billet/_BilletBody.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Product, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Delete)]
    public async Task<JsonResult> DeleteMaterialCatalogue(long itemId)
    {
      VM_MaterialCatalogue materialCat = new VM_MaterialCatalogue()
      {
        Id = itemId
      };
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteMaterialCatalogue(ModelState, materialCat));
    }

    public JsonResult ServerFiltering_GetMaterialCatalogues(string text)
    {
      IList<VM_MaterialCatalogue> materialCatalogues = _service.GetMaterialCataloguesByAnyFeaure(text);

      return Json(materialCatalogues, JsonRequestBehavior.AllowGet);
    }
    

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    //public async Task<JsonResult> GetWorkOrderListById([DataSourceRequest] DataSourceRequest request, long id)
    //{
    //  return await PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrderListById(ModelState, request, id));
    //}
  }
}
