using PE.HMIWWW.ViewModel.System;
using System;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using PE.DbEntity.Model;
using PE.DbEntity;
using SMF.Module.Core;
using PE.Interfaces;
using SMF.Core.DC;
using PE.HMIWWW.Core.Resources;
using System.ServiceModel;
using PE.HMIWWW.Core.ViewModel;
using SMF.Core.Helpers;
using PE.DTO.Internal.System;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Communication;
using System.Security.Claims;
using PE.HMIWWW.Core.Service;
using SMF.RepositoryPatternExt;

namespace PE.HMIWWW.Services.System
{
  public interface ITestService
  {
		Task<VM_Test> SendMessageToModuleA(ModelStateDictionary modelState);
		//Task<VM_Test> SendMessageToModuleB(ModelStateDictionary modelState);
		//Task<VM_Test> SendToModuleAMakeError(ModelStateDictionary modelState);
		//Task<VM_Test> SendToModuleAMakeWarnings(ModelStateDictionary modelState);
	}
  public class TestService : BaseService, ITestService
  {
		#region interface IExtTelArchiveService
		public async Task<VM_Test> SendMessageToModuleA(ModelStateDictionary modelState)
		{
			VM_Test returnVM = new VM_Test();

			//VALIDATE ENTRY PARAMETERS
			if (!modelState.IsValid)
			{
				return returnVM;
			}
			//END OF VALIDATION

			DCTestData entryDataContract = new DCTestData();

			entryDataContract.TestItem = "Telegram from HMI";
			// ClaimsPrincipal.Current.Identity.Name;

		 Task<SendOfficeResult<DCTestData>> taskOnRemoteModule = HmiSendOffice.SendMessageToA(entryDataContract);


			SendOfficeResult<DCTestData> sendOfficeResult = null;
			if (await Task.WhenAny(taskOnRemoteModule, Task.Delay(2000)) == taskOnRemoteModule)
			{
				// task completed within timeout
				sendOfficeResult = await taskOnRemoteModule;

				//use the result from "SendOffice"
				if (sendOfficeResult != null)
				{
					if (sendOfficeResult.OperationSuccess)
						returnVM = new VM_Test(sendOfficeResult.DataConctract.TestItem);
					else
						throw new Exception("Error in module A (service calls)");
				}
			}
			else
			{
				// timeout logic
				throw new Exception("Timeout!!");
			}
			return returnVM;
		}

		//public async Task<VM_Test> SendMessageToModuleB(ModelStateDictionary modelState)
		//{
		//	VM_Test returnVM = new VM_Test();

		//	//VALIDATE ENTRY PARAMETERS
		//	if (!modelState.IsValid)
		//	{
		//		return returnVM;
		//	}
		//	//END OF VALIDATION

		//	DCTestData entryDataContract = new DCTestData();
		//	InitDataContract(entryDataContract); //HMI INITIATOR INIT

		//	entryDataContract.TestItem = "Telegram from HMI";

		//	//Task<SendOfficeResult<DCTestData>> taskOnRemoteModule = HmiSendOffice.SendMessageToB(entryDataContract);


		//	//SendOfficeResult<DCTestData> sendOfficeResult = null;
		//	//if (await Task.WhenAny(taskOnRemoteModule, Task.Delay(2000)) == taskOnRemoteModule)
		//	//{
		//	//  // task completed within timeout
		//	//  sendOfficeResult = await taskOnRemoteModule;

		//	//  //use the result from "SendOffice"
		//	//  if (sendOfficeResult != null)
		//	//  {
		//	//    if (sendOfficeResult.OperationSuccess)
		//	//      returnVM = new VM_Test(sendOfficeResult.DataConctract.TestItem);
		//	//    else
		//	//      throw new Exception("Error in module A");
		//	//  }
		//	//}
		//	//else
		//	//{
		//	//  // timeout logic
		//	//  throw new Exception("Timeout!!");
		//	//}
		//	return returnVM;

		//}

		//public async Task<VM_Test> SendToModuleAMakeError(ModelStateDictionary modelState)
		//{
		//	VM_Test returnVM = new VM_Test();

		//	DCTestData entryDataContract = new DCTestData();
		//	InitDataContract(entryDataContract); //HMI INITIATOR INIT

		//	entryDataContract.TestItem = "Telegram from HMI";

		//	Task<SendOfficeResult<DCTestData>> taskOnRemote = HmiSendOffice.SendToModuleAMakeError(entryDataContract);
		//	SendOfficeResult<DCTestData> result = null;
		//	if (await Task.WhenAny(taskOnRemote) == taskOnRemote)
		//	{
		//		result = await taskOnRemote;
		//		if (result.OperationSuccess)
		//		{
		//			if (result.DataConctract != null)
		//			{
		//				HandleModuleWarningMessage(result.DataConctract, ref modelState);
		//			}
		//			// Null data
		//			else throw new ArgumentNullException();
		//		}
		//		// Operation failure
		//		else if (result.ModuleMessage != null)
		//		{
		//			throw new ModuleMessageException(result.ModuleMessage);
		//		}
		//		else throw new Exception();
		//	}
		//	// Timeout
		//	else throw new Exception();

		//	return returnVM;
		//}

		//public async Task<VM_Test> SendToModuleAMakeWarnings(ModelStateDictionary modelState)
		//{
		//	VM_Test returnVM = new VM_Test();

		//	DCTestData entryDataContract = new DCTestData();
		//	InitDataContract(entryDataContract); //HMI INITIATOR INIT

		//	entryDataContract.TestItem = "Telegram from HMI";

		//	Task<SendOfficeResult<DCTestData>> taskOnRemote = HmiSendOffice.SendToModuleAMakeWarnings(entryDataContract);
		//	SendOfficeResult<DCTestData> result = null;
		//	if (await Task.WhenAny(taskOnRemote) == taskOnRemote)
		//	{
		//		result = await taskOnRemote;
		//		if (result.OperationSuccess)
		//		{
		//			if (result.DataConctract != null)
		//			{
		//				returnVM.Text = result.DataConctract.TestItem;
		//				HandleModuleWarningMessage(result.DataConctract, ref modelState);
		//			}
		//			// Null data
		//			else throw new ArgumentNullException();
		//		}
		//		// Operation failure
		//		else if (result.ModuleMessage != null)
		//		{
		//			throw new ModuleMessageException(result.ModuleMessage);
		//		}
		//		else throw new Exception();
		//	}
		//	// Timeout
		//	else throw new Exception();

		//	return returnVM;
		//}
		#endregion

		#region private methods

		#endregion

	}
}
