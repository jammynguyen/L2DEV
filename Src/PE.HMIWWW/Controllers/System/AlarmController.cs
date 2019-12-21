using Kendo.Mvc.UI;
using PE.Services.System;
using PE.HMIWWW.Core.Controllers;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using System.Web.Mvc;
using PE.HMIWWW.ViewModel.System;
using System.Threading.Tasks;
using PE.HMIWWW.Core.HtmlHelpers;

namespace PE.HMIWWW.Controllers
{
  public class AlarmController : BaseController
  {
    #region services

    IAlarmsService _alarmsService;

    #endregion

    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.YesNo = ListValuesHelper.GetTextFromYesNoStatus();
      ViewBag.AlarmTypes = ListValuesHelper.GetAlarmTypes();

      base.OnActionExecuting(ctx);
    }
    #region ctor
    public AlarmController(IAlarmsService alarmsService)
    {
      _alarmsService = alarmsService;
    }

    #endregion
    #region interface

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_Alarms, RightLevel.View)]
    public ActionResult Index(string owner = null, bool? toConfirm = null)
    {
      VM_AlarmSelection alarmSelection = null;
      if (owner != null || toConfirm != null)
      {
        alarmSelection = new VM_AlarmSelection(owner, toConfirm);
      }
      return View("Index", alarmSelection);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_Alarms, RightLevel.View)]
    public async Task<JsonResult> GetAlarmData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _alarmsService.GetAlarmList(ModelState, request, GetCultureName()));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_Alarms, RightLevel.View)]
    public async Task<JsonResult> GetLastAlarmData([DataSourceRequest]DataSourceRequest request)
    {
      request.Page = 1;
      return await PrepareJsonResultFromDataSourceResult(() => _alarmsService.GetAlarmList(ModelState, request, GetCultureName()));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_Alarms, RightLevel.View)]
    public async Task<JsonResult> GetLastShortAlarmData([DataSourceRequest]DataSourceRequest request)
    {
      request.Page = 1;
      return await PrepareJsonResultFromDataSourceResult(() => _alarmsService.GetShortAlarmList(ModelState, request, GetCultureName()));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_Alarms, RightLevel.Update)]
    public async Task<JsonResult> Confirm(long alarmId)
    {
      return await PrepareJsonResultFromVm(() => _alarmsService.Confirm(ModelState, alarmId, _userName));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_Alarms, RightLevel.View)]
    public async Task<ActionResult> DetailsDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _alarmsService.GetDetails(ModelState, id), "DetailsDialog");
    }

    #endregion

  }
}