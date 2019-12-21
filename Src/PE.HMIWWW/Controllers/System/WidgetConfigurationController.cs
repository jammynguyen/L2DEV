using Kendo.Mvc.UI;
using PE.Core;
using PE.DbEntity.Model;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.System
{
  public class WidgetConfigurationController : BaseController
	{
		protected override void OnActionExecuting(ActionExecutingContext ctx)
		{

			base.OnActionExecuting(ctx);

			ViewBag.UserId = ViewBag.User_Id;
		}
		#region services

		IWidgetConfigurationService _widgetService;

		#endregion

		public WidgetConfigurationController(IWidgetConfigurationService service)
		{
			_widgetService = service;
		}

		[SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations, Constants.SmfAuthorization_Module_System, RightLevel.View)]
		public ActionResult Index()
		{
			return View();
		}

		[SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
		public ActionResult AddWidgetConfigurationDialog()
		{
			return PartialView("AddWidgetConfigurationDialog");
		}

		[SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
		public async Task<ActionResult> EditWidgetConfigurationDialog(long id)
		{
			return await PreparePopupActionResultFromVm(() => _widgetService.GetWidgetConfiguration(ModelState, id), "EditWidgetConfigurationDialog");
		}

		public static List<VM_WidgetConfigurations> GetVMWidgetConfigurationsList(string userId)
		{
			return WidgetConfigurationService.GetVMWidgetConfigurationsList(userId);
		}
		public PartialViewResult _Widgets()
		{
			return PartialView("~/Views/Shared/Widget/_Widgets.cshtml", GetVMWidgetConfigurationsList(ViewBag.User_Id));
		}
		#region actions
		[AcceptVerbs(HttpVerbs.Post)]
		[SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
		public async Task<JsonResult> InsertWidgetConfiguration(VM_WidgetConfigurations viewModel)
		{
			return await PrepareJsonResultFromVm(() => _widgetService.InsertWidgetConfiguration(ModelState, viewModel));
		}

		[SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
		public async Task<JsonResult> UpdateWidgetConfiguration(VM_WidgetConfigurations viewModel)
		{
			return await PrepareJsonResultFromVm(() => _widgetService.UpdateWidgetConfiguration(ModelState, viewModel));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
		public async Task<JsonResult> Delete([DataSourceRequest] DataSourceRequest request, VM_LongId viewModel)
		{
			return await PrepareJsonResultFromVm(() => _widgetService.DeleteWidgetConfiguration(ModelState, viewModel));
		}

        [AcceptVerbs(HttpVerbs.Post)]
        [SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
        public async Task<JsonResult> GetWidgetConfigurationsData([DataSourceRequest]DataSourceRequest request)
        {
            string UserId = ViewBag.UserId;
            return await PrepareJsonResultFromDataSourceResult(() => _widgetService.GetWidgetConfigurationsData(ModelState, request, UserId));
        }
        #endregion
    }
}