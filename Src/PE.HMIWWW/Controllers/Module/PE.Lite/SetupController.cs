using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
	[SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]

	public class SetupController : BaseController
	{
		private readonly ISetupService _setupService;

		public SetupController(ISetupService service)
		{
			_setupService = service;
		}

		public ActionResult Index()
		{
			return View("~/Views/Module/PE.Lite/Setup/Index.cshtml");
		}

		[SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
		public async Task<JsonResult> GetTelegramsSearchGridData([DataSourceRequest] DataSourceRequest request)
		{
			return await PrepareJsonResultFromDataSourceResult(() => _setupService.GetTelegramsSearchGridData(ModelState, request));
		}

		public async Task<ActionResult> ElementDetails(long? telegramId)
		{
			VM_Telegrams model = _setupService.FindTelegram((long)telegramId);
				return PartialView("~/Views/Module/PE.Lite/Setup/_SetupBody.cshtml", model);

		}

		public async Task<JsonResult> GetTelegramLineData([DataSourceRequest] DataSourceRequest request, long telegramId)
		{
			return await PrepareJsonResultFromDataSourceResult(() => _setupService.GetTelegramLineData(ModelState, request, telegramId));
		}

		[SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
		public async Task<ActionResult> UpdateValues(VM_TelegramStructure model)
		{
			// VM_TelegramStructure model = new VM_TelegramStructure { ElementId = id, Value = values };
			return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _setupService.UpdateValue(ModelState, model));
		}
		public async Task<ActionResult> SendTelegram(long telegramId)
		{
			// VM_TelegramStructure model = new VM_TelegramStructure { ElementId = id, Value = values };
			return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _setupService.SendTelegram(ModelState, telegramId));
		}
    public async Task<ActionResult> CreateNewVersion(long? telegramId)
    {
      // VM_TelegramStructure model = new VM_TelegramStructure { ElementId = id, Value = values };
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _setupService.CreateNewVersion(ModelState, (long)telegramId));
    }
    public async Task<ActionResult> DeleteSetup(long? telegramId)
    {
      // VM_TelegramStructure model = new VM_TelegramStructure { ElementId = id, Value = values };
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _setupService.DeleteSetup(ModelState, (long)telegramId));
    }

  }
}
