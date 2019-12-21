using Kendo.Mvc.UI;
using PE.Core;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class WorkOrderController : BaseController
  {
    private readonly IWorkOrderService _service;
    public WorkOrderController(IWorkOrderService service)
    {
      _service = service;
    }

    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.WorkOrderStatuses = ListValuesHelper.GetScheduleStatuses();
      base.OnActionExecuting(ctx);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/WorkOrder/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetWorkOrderOverviewList([DataSourceRequest] DataSourceRequest request)
    {
      ViewBag.WorkOrderStatuses = ListValuesHelper.GetWorkOrderStatusesList();
      return await PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrderOverviewList(ModelState, request));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> ElementDetails(long workOrderId)
    {
      return await PrepareActionResultFromVm(() => _service.GetWorkOrderDetails(ModelState, workOrderId), "~/Views/Module/PE.Lite/WorkOrder/_WorkOrderBody.cshtml");
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetMaterialsListByWorkOrderId([DataSourceRequest] DataSourceRequest request, long workOrderId)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _service.GetMaterialsListByWorkOrderId(ModelState, request, workOrderId));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetNoScheduledWorkOrderList([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _service.GetNoScheduledWorkOrderList(ModelState, request));
    }


    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> CreateWorkOrder(VM_WorkOrder workOrder)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.CreateWorkOrder(ModelState, workOrder));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> WorkOrderCreatePopup()
    {
      ViewBag.WorkOrderStatuses = new SelectList(ListValuesHelper.GetWorkOrderStatusesList().Where(x => x.Value != "0").ToList(), "Value", "Text");
      ViewBag.ProductList = _service.GetProductList();
      ViewBag.ReheatingList = _service.GetReheatingList();
      ViewBag.CustomerList = _service.GetCustomerList();
      return PartialView("~/Views/Module/PE.Lite/WorkOrder/_WorkOrderCreatePopup.cshtml");
    }


    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> EditWorkOrder(VM_WorkOrder workOrder)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.EditWorkOrder(ModelState, workOrder));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> WorkOrderEditPopup(long id, bool byMaterial = false)
    {
      long? workOrderId = !byMaterial ? id : await _service.GetWorkOrderIdByMaterialId(id);

      ViewBag.WorkOrderStatuses = new SelectList(ListValuesHelper.GetWorkOrderStatusesList().Where(x => x.Value != "0").ToList(), "Value", "Text");
      ViewBag.ProductList = _service.GetProductList();
      ViewBag.ReheatingList = _service.GetReheatingList();
      ViewBag.CustomerList = _service.GetCustomerList();
      return PartialView("~/Views/Module/PE.Lite/WorkOrder/_WorkOrderEditPopup.cshtml", await _service.GetWorkOrder(workOrderId));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Delete)]
    public async Task<JsonResult> DeleteWorkOrder(long workOrderId)
    {
      VM_WorkOrderOverview workOrder = _service.GetWorkOrderDetails(ModelState, workOrderId);
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteWorkOrder(ModelState, workOrder));
    }

    public async Task<JsonResult> GetProductCatalogDetails(long productCatalogId)
    {
      VM_ProductCatalogue result = await _service.GetProductCatalogueDetails(productCatalogId);
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    public async Task<JsonResult> GetMaterialWeight(string heatName)
    {
      double result = await _service.GetMaterialWeightFromMaterialCatalogue(heatName);
      return Json(result, JsonRequestBehavior.AllowGet);
    }
  }

}
