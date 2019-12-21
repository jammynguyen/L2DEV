using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.Services.System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.System
{
	public class ServiceController : BaseController
	{
		#region services

		IAccessUnitsService _accessUnitsService;  

		#endregion

		#region ctor
		public ServiceController(IAccessUnitsService accessUnitsService)
		{
			_accessUnitsService = accessUnitsService;
		}
		#endregion

		#region interface
		[SmfAuthorization(Constants.SmfAuthorization_Controller_Service, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
		public ActionResult Index()
		{
			return View();
		}

		[SmfAuthorization(Constants.SmfAuthorization_Controller_Service, Constants.SmfAuthorization_Module_System, RightLevel.View)]
		public async Task<JsonResult> UserRightsPopulate([DataSourceRequest]DataSourceRequest request)
		{
			return await PrepareJsonResultFromDataSourceResult(() => _accessUnitsService.UserRightsPopulate(ModelState, request));
		}

		[SmfAuthorization(Constants.SmfAuthorization_Controller_Service, Constants.SmfAuthorization_Module_System, RightLevel.View)]
		public async Task<JsonResult> GetExistedAccessUnits([DataSourceRequest]DataSourceRequest request)
		{
			return await PrepareJsonResultFromDataSourceResult(() => _accessUnitsService.GetExistedAccessUnits(ModelState, request));
		}


		#endregion
	}
}
