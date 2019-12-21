using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace PE.HMIWWW.Services.System
{
  public interface IHomeService
  {
    //Task<VM_LongId> ModuleOperationRequest(ModelStateDictionary modelState, VM_LongId viewModel);
  }
  public class HomeService : BaseService, IHomeService
  {
    //public async Task<VM_LongId> ModuleOperationRequest(ModelStateDictionary modelState, VM_LongId viewModel)
    //{
    //  VM_LongId returnVM = new VM_LongId();

    //  //VALIDATE ENTRY PARAMETERS
    //  if (!modelState.IsValid)
    //  {
    //    return returnVM;
    //  }
    //  //END OF VALIDATION

    //  DCHmiOperation entryDataContract = new DCHmiOperation();
    //  InitDataContract(entryDataContract); //HMI INITIATOR INIT
    //  entryDataContract.Operation = (int)viewModel.Id;

    //  Task<bool> taskOnRemoteModule = HmiSendOffice.ModuleOperationrequest(entryDataContract);


    //  bool sendOfficeResult = false;
    //  if (await Task.WhenAny(taskOnRemoteModule, Task.Delay(2000)) == taskOnRemoteModule)
    //  {
    //    // task completed within timeout
    //    sendOfficeResult = await taskOnRemoteModule;

    //    //use the result from "SendOffice"
    //      if (sendOfficeResult)
    //        returnVM.Id = viewModel.Id;
    //      else
    //        throw new Exception("Error in module");
    //  }
    //  else
    //  {
    //    // timeout logic
    //    throw new Exception("Timeout!!");
    //  }
    //  return returnVM;
    //}
  }
}
