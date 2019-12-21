using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.System
{
  public class ViewsStatisticsController : BaseController
	{
		#region services

		IViewsStaticsService _viewsStatisticsService;

		#endregion

		public ViewsStatisticsController(IViewsStaticsService service)
		{
			_viewsStatisticsService = service;
		}

		[SmfAuthorization(Constants.SmfAuthorization_Controller_ViewsStatistics, Constants.SmfAuthorization_Module_System, RightLevel.View)]
		public ActionResult Index()
		{
			return View();
		}

		[SmfAuthorization(Constants.SmfAuthorization_Controller_ViewsStatistics, Constants.SmfAuthorization_Module_System, RightLevel.View)]
		public async Task<JsonResult> ViewsStatisticsData([DataSourceRequest]DataSourceRequest request)
		{
			return await PrepareJsonResultFromDataSourceResult(() => _viewsStatisticsService.GetViewsStatisticsList(ModelState, request));
		}

	}	
}