
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using SMF.Module.Core;
using SMF.Watchdog.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Services.System
{
  public interface IWatchdogService
  {
    VM_Base Stop(ModelStateDictionary modelState, string moduleName);
    VM_Base Kill(ModelStateDictionary modelState, string moduleName);
    VM_Base SetProcessUnderWd(ModelStateDictionary modelState, string moduleName);
    VM_Base UnSetProcessUnderWd(ModelStateDictionary modelState, string moduleName);
    VM_Base Initialize(ModelStateDictionary modelState, string moduleName);
  }
  public class WatchdogService : BaseService, IWatchdogService
  {
    public VM_Base Initialize(ModelStateDictionary modelState, String moduleName)
    {
      VM_Base returnValueVm = new VM_Base();
      IWdog client = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      client = WatchdogInterfaceHelper.GetWdogFactoryChannel<IWdog>("");

      Task<Message> myTask = client.Initialize(moduleName);
      Message message = myTask.Result;

      WatchdogInterfaceHelper.CloseChannel((IClientChannel)client);

      if (message != null && message.MessageId == -1)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("NoCommunicationWithWatchdog"));
      }

      return returnValueVm;
    }
    public VM_Base Kill(ModelStateDictionary modelState, string moduleName)
    {
      VM_Base returnValueVm = new VM_Base();
      IWdog client = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      client = WatchdogInterfaceHelper.GetWdogFactoryChannel<IWdog>("");

      Task<Message> myTask = client.Kill(moduleName);
      Message message = myTask.Result;

      WatchdogInterfaceHelper.CloseChannel((IClientChannel)client);

      if (message != null && message.MessageId == -1)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("NoCommunicationWithWatchdog"));
      }

      return returnValueVm;
    }
    public VM_Base Stop(ModelStateDictionary modelState, String moduleName)
    {
      VM_Base returnValueVm = new VM_Base();
      IWdog client = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      client = WatchdogInterfaceHelper.GetWdogFactoryChannel<IWdog>("");

      Task<Message> myTask = client.Stop(moduleName);
      myTask.ConfigureAwait(false);
      Message message = myTask.Result;

      WatchdogInterfaceHelper.CloseChannel((IClientChannel)client);

      if (message != null && message.MessageId == -1)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("NoCommunicationWithWatchdog"));
      }

      return returnValueVm;
    }
    public VM_Base SetProcessUnderWd(ModelStateDictionary modelState, String moduleName)
    {
      VM_Base returnValueVm = new VM_Base();
      IWdog client = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      client = WatchdogInterfaceHelper.GetWdogFactoryChannel<IWdog>("");

      Task<Message> myTask = client.SetProcessUnderWd(moduleName);
      Message message = myTask.Result;

      WatchdogInterfaceHelper.CloseChannel((IClientChannel)client);

      if (message != null && message.MessageId == -1)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("NoCommunicationWithWatchdog"));
      }

      return returnValueVm;
    }
    public VM_Base UnSetProcessUnderWd(ModelStateDictionary modelState, String moduleName)
    {
      VM_Base returnValueVm = new VM_Base();
      IWdog client = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      client = WatchdogInterfaceHelper.GetWdogFactoryChannel<IWdog>("");

      Task<Message> myTask = client.UnSetProcessUnderWd(moduleName);
      Message message = myTask.Result;

      WatchdogInterfaceHelper.CloseChannel((IClientChannel)client);

      if (message != null && message.MessageId == -1)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("NoCommunicationWithWatchdog"));
      }

      return returnValueVm;
    }
  }
}
