using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class HeatController : BaseController
  {
    private readonly IHeatService _heatService;

    public HeatController(IHeatService service)
    {
      _heatService = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Heat/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> ElementDetails(long heatId)
    {
      return await PrepareActionResultFromVm(() => _heatService.GetHeatDetails(ModelState, heatId), "~/Views/Module/PE.Lite/Heat/_HeatBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetHeatSearchList([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _heatService.GetHeatOverviewList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetMaterialsListByHeatId([DataSourceRequest] DataSourceRequest request, long heatId)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _heatService.GetMaterialsListByHeatId(ModelState, request, heatId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetWorkOrderListByHeatId([DataSourceRequest] DataSourceRequest request, long heatId)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _heatService.GetWorkOrderListByHeatId(ModelState, request, heatId));
    }


    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> CreateHeat(VM_Heat heat)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _heatService.CreateHeat(ModelState, heat));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> HeatCreatePopup()
    {
      ViewBag.SupplierList = _heatService.GetSupplierList();
      ViewBag.MaterialList = _heatService.GetMaterialList();
      return PartialView("~/Views/Module/PE.Lite/Heat/_HeatCreatePopup.cshtml");
    }

    public JsonResult ServerFiltering_GetHeats(string text)
    {
      IList<VM_HeatSummary> heats = _heatService.GetHeatsByAnyFeaure(text);

      return Json(heats, JsonRequestBehavior.AllowGet);
    }
  }
}
