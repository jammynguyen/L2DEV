using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class GrooveTemplateController : BaseController
  {

    #region services

    IGrooveTemplateService _grooveTemplateService;

    #endregion
    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.ShapeList = ListValuesHelper.GetShapeList();
      base.OnActionExecuting(ctx);
    }
    #region ctor
    public GrooveTemplateController(IGrooveTemplateService service)
    {
      _grooveTemplateService = service;
    }
    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/GrooveTemplate/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddGrooveTemplateDialog()
    {
      return PartialView("~/Views/Module/PE.Lite/GrooveTemplate/_GrooveTemplateAddPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> EditGrooveTemplateDialog(long id)
    {
      //ViewBag.ShapeList = _grooveTemplateService.GetShapeList();
      return await PreparePopupActionResultFromVm(() => _grooveTemplateService.GetGrooveTemplate(ModelState, id), "~/Views/Module/PE.Lite/GrooveTemplate/_GrooveTemplateEditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Details(long id)
    {
      return PartialView("_Details", _grooveTemplateService.GetGrooveTemplate(ModelState, id));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetGrooveTemplatesData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _grooveTemplateService.GetGrooveTemplateList(ModelState, request));
    }




    #region Actions
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> InsertGrooveTemplate(VM_GrooveTemplate viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grooveTemplateService.InsertGrooveTemplate(ModelState, viewModel));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> DeleteGrooveTemplate(VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grooveTemplateService.DeleteGrooveTemplate(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> UpdateGrooveTemplate(VM_GrooveTemplate viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grooveTemplateService.UpdateGrooveTemplate(ModelState, viewModel));
    }
    #endregion
  }
}
