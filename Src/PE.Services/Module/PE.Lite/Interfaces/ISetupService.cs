using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Setup;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
	public interface ISetupService
	{
		DataSourceResult GetTelegramsSearchGridData(ModelStateDictionary modelState, DataSourceRequest request);
		DataSourceResult GetTelegramLineData(ModelStateDictionary modelState, DataSourceRequest request, long telegramId);
		Task<VM_Base> UpdateValue(ModelStateDictionary modelState, VM_TelegramStructure model);
		Task<VM_Base> SendTelegram(ModelStateDictionary modelState, long telegramId);
    Task<VM_Base> CreateNewVersion(ModelStateDictionary modelState, long telegramId);
    Task<VM_Base> DeleteSetup(ModelStateDictionary modelState, long telegramId);
    VM_Telegrams FindTelegram(long telegramId);

	}
	
}
