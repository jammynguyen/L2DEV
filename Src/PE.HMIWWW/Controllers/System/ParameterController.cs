using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.System
{
    public class ParameterController : BaseController
	{
		#region services

		IParameterService _parameterService;

		#endregion
		#region ctor
		public ParameterController(IParameterService service)
		{
			_parameterService = service;
		}

		#endregion
		#region view interface

		[SmfAuthorization(Constants.SmfAuthorization_Controller_Parameter, Constants.SmfAuthorization_Module_System, RightLevel.View)]
		public ActionResult Index()
		{
			return View();
		}

		[SmfAuthorization(Constants.SmfAuthorization_Controller_Parameter, Constants.SmfAuthorization_Module_System, RightLevel.View)]
		public async Task<JsonResult> ParameterData([DataSourceRequest]DataSourceRequest request)
		{
            return await PrepareJsonResultFromDataSourceResult(() => _parameterService.GetParameters(ModelState, request));
        }

    [AcceptVerbs(HttpVerbs.Post)]
		[SmfAuthorization(Constants.SmfAuthorization_Controller_Parameter, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
		public async Task<JsonResult> Update(VM_Parameter viewModel)
		{
            return await PrepareJsonResultFromVm(() => _parameterService.UpdateParameter(ModelState, viewModel));
        }

		#endregion
	}	
}