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
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.WeighingMonitor;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
    public class WeighingMonitorController : BaseController
  {
    private readonly IWeighingMonitorService _service;
    public WeighingMonitorController(IWeighingMonitorService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WeighingMonitor, Constants.SmfAuthorization_Module_WeighingMonitor, RightLevel.View)]
    public ActionResult Index()
    {
      DataSourceRequest request = new DataSourceRequest();
      //VM_WeighingOverview weighingMonitordata = _service.GetWeighingMonitorOverview(ModelState, request);
      VM_RawMaterialOverview rawMaterialOnScale = _service.GetRawMaterialOnWeight(ModelState, request);
      return View("~/Views/Module/PE.Lite/WeighingMonitor/Index.cshtml", rawMaterialOnScale);
      //return View("~/Views/Module/PE.Lite/WeighingMonitorService/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WeighingMonitor, Constants.SmfAuthorization_Module_WeighingMonitor, RightLevel.View)]
    public async Task<JsonResult> GetRawMaterialsBeforeWeightList([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _service.GetRawMaterialsBeforeWeightList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WeighingMonitor, Constants.SmfAuthorization_Module_WeighingMonitor, RightLevel.View)]
    public async Task<JsonResult> GetRawMaterialsAfterWeightList([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _service.GetRawMaterialsAfterWeightList(ModelState, request));
    }
  }
}
