using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.ConsumptionMonitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class ConsumptionMonitoringController : BaseController
  {
    private readonly IConsumptionMonitoringService _consumptionMonitoringService;

    public ConsumptionMonitoringController(IConsumptionMonitoringService service)
    {
      _consumptionMonitoringService = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/ConsumptionMonitoring/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetFeaturesList([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _consumptionMonitoringService.GetFeaturesList(ModelState, request));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> GetFeatureDetails(long featureId)
    {
      return await PrepareActionResultFromVm(() => _consumptionMonitoringService.GetFeatureDetails(ModelState, featureId), "~/Views/Module/PE.Lite/ConsumptionMonitoring/_MeasurementBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    [HttpPost]
    public async Task<ActionResult> GetMeasurementData([DataSourceRequest]DataSourceRequest request, long featureId, DateTime dateFrom, DateTime dateTo)
    {
      DataSourceResult measurements = _consumptionMonitoringService.GetMeasurementData(ModelState, request, featureId, dateFrom, dateTo);

      return Json(measurements.Data);
    }
  }
}
