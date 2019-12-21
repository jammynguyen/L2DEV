using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.System
{
  public class TestController : BaseController
  {
    #region services

    ITestService _testService;

    #endregion

    #region ctor
    public TestController(ITestService service)
    {
      _testService = service;
    }
    #endregion
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Test, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }


		#region Actions
		[HttpPost]
		[SmfAuthorization(Constants.SmfAuthorization_Controller_Test, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
		public async Task<JsonResult> SendToModuleA()
		{
			return await TaskPrepareJsonResultFromVm<VM_Test, Task<VM_Test>>(() => _testService.SendMessageToModuleA(ModelState));
		}
		//[HttpPost]
		//[SmfAuthorization(Constants.SmfAuthorization_Controller_Test, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
		//public async Task<JsonResult> SendToModuleB()
		//{
		//	return await TaskPrepareJsonResultFromVm<VM_Test, Task<VM_Test>>(() => _testService.SendMessageToModuleB(ModelState));
		//}
		//[HttpPost]
		//[SmfAuthorization(Constants.SmfAuthorization_Controller_Test, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
		//public async Task<JsonResult> SendToModuleAMakeError()
		//{
		//	return await TaskPrepareJsonResultFromVm<VM_Test, Task<VM_Test>>(() => _testService.SendToModuleAMakeError(ModelState));
		//}
		//[HttpPost]
		//[SmfAuthorization(Constants.SmfAuthorization_Controller_Test, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
		//public async Task<JsonResult> SendToModuleAMakeWarnings()
		//{
		//	return await TaskPrepareJsonResultFromVm<VM_Test, Task<VM_Test>>(() => _testService.SendToModuleAMakeWarnings(ModelState));
		//}

		#endregion
	}
}