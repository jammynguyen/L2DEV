using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class DefectCatalogueController : BaseController
  {
    private readonly IDefectService _service;

    public DefectCatalogueController(IDefectService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult Index()
    {
      ViewBag.DefectCategories = ListValuesHelper.GetDefectCatalogCategoriesList();
      return View("~/Views/Module/PE.Lite/Defect/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> GetDefectCatalogueList([DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = _service.GetDefectCatalogueList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> DefectCatalogueAddPopup( )
    {
      ViewBag.DefectCategories = ListValuesHelper.GetDefectCatalogCategoriesList();
      //ViewBag.CategoryList = _service.GetDefectCategoryList();
      VM_DefectCatalogue result = new VM_DefectCatalogue();
      return await PreparePopupActionResultFromVm(() => result, "~/Views/Module/PE.Lite/Defect/_DefectCatalogueAddPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> DefectCatalogueEditPopup(long id)
    {
      ViewBag.DefectCategories = ListValuesHelper.GetDefectCatalogCategoriesList();
      VM_DefectCatalogue result = _service.GetDelayCatalogue(id);
      return await PreparePopupActionResultFromVm(() => result, "~/Views/Module/PE.Lite/Defect/_DefectCatalogueEditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    [HttpPost]
    public async Task<ActionResult> AddDefectCatalogue(VM_DefectCatalogue defectCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.AddDefectCatalogue(ModelState, defectCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    [HttpPost]
    public async Task<ActionResult> UpdateDefectCatalogue(VM_DefectCatalogue defectCatalogue)
    {   
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.UpdateDefectCatalogue(ModelState, defectCatalogue));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Controller_Defect, RightLevel.Delete)]
    public async Task<JsonResult> DeleteDefect(long itemId)
    {
      VM_DefectCatalogue dCat = new VM_DefectCatalogue()
      {
        DefectCatalogueId = itemId
      };
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteDefectCatalogueAsync(ModelState, dCat));
    }
  }
}
