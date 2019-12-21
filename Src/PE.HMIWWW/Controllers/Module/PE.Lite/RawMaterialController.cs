using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class RawMaterialController : BaseController
  {
    private readonly IRawMaterialService _materialService;

    public RawMaterialController(IRawMaterialService service)
    {
      _materialService = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult Index()
    {
      ViewBag.RawMaterialStatuses = ListValuesHelper.GetRawMaterialStatusesList();
      return View("~/Views/Module/PE.Lite/RawMaterial/Index.cshtml");
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetRawMaterialSearchList([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _materialService.GetRawMaterialSearchList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> ElementDetails(long RawMaterialId)
    {
      return await PrepareActionResultFromVm(() => _materialService.GetRawMaterialDetails(ModelState, RawMaterialId), "~/Views/Module/PE.Lite/RawMaterial/_RawMaterialBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> MeasurementDetails(long measurementId)
    {
      return await PrepareActionResultFromVm(() => _materialService.GetMeasurementDetails(ModelState, measurementId), "~/Views/Module/PE.Lite/RawMaterial/_MeasurementsBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> HistoryDetails(long rawMaterialStepId)
    {
      return await PrepareActionResultFromVm(() => _materialService.GetHistoryDetails(ModelState, rawMaterialStepId), "~/Views/Module/PE.Lite/RawMaterial/_HistoryBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> L3MaterialAssignment(long RawMaterialId)
    {
      return await PrepareActionResultFromVm(() => _materialService.GetL3MaterialData(ModelState, RawMaterialId), "~/Views/Module/PE.Lite/RawMaterial/_L3MaterialAssignment.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetNotAssignmedL3Materials([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _materialService.GetNotAssignmedL3Materials(ModelState, request));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> AssignMaterials(long RawMaterialId, long l3MaterialId)
    {
      return await TaskPrepareActionResultFromVm(_materialService.AssignMaterials(ModelState, RawMaterialId, l3MaterialId), "~/Views/Module/PE.Lite/RawMaterial/_RawMaterialBody.cshtml");
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> UnassignMaterial(long RawMaterialId)
    {
      return await TaskPrepareActionResultFromVm(_materialService.UnassignMaterial(ModelState, RawMaterialId), "~/Views/Module/PE.Lite/RawMaterial/_RawMaterialBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> MarkMaterialAsScrap(long RawMaterialId)
    {
      return await TaskPrepareActionResultFromVm(_materialService.MarkMaterialAsScrap(ModelState, RawMaterialId), "~/Views/Module/PE.Lite/RawMaterial/_RawMaterialBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetMeasurmentsByRawMaterialId([DataSourceRequest] DataSourceRequest request, long RawMaterialId)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _materialService.GetMeasurmentsByRawMaterialId(ModelState, request, RawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetHistoryByRawMaterialId([DataSourceRequest] DataSourceRequest request, long RawMaterialId)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _materialService.GetHistoryByRawMaterialId(ModelState, request, RawMaterialId));
    }
  }
}
