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
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class TrackingController : BaseController
  {
    #region members
    private readonly ITrackingService _service;
    #endregion

    #region ctor
    public TrackingController(ITrackingService service)
    {
      _service = service;
    }
    #endregion

    #region funcs
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Tracking, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Tracking/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetTrackingList([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _service.GetTrackingList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Tracking, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> ElementDetails(long dimRawMaterialKey, long? workOrderId, long? heatId)
    {
      return await PrepareActionResultFromVm(() => _service.GetTrackingDetails(ModelState, dimRawMaterialKey, workOrderId, heatId), "~/Views/Module/PE.Lite/Tracking/_TrackingBody.cshtml");
    }
    #endregion
  }
}
