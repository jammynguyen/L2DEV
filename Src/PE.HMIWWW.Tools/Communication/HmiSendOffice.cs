using NLog;
using PE.Interfaces;
using PE.DTO.Internal.Delay;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using PE.DTO.Internal.Schedule;
using PE.DTO.Internal.Maintenance;
using PE.DTO.Internal.System;
using PE.DTO.Internal.Quality;
using PE.Interfaces.Lite;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.ServiceModel;
using System.Threading.Tasks;
using PE.Interfaces.Interfaces.Lite;
using PE.DTO.Internal.ZebraPrinter;
using System.IO;
using PE.DTO.Internal.Setup;
using System.Diagnostics;
using PE.Common;

using PE.DTO.Internal.RollShop;

namespace PE.HMIWWW.Core.Communication
{
	public class HmiSendOffice : HmiSendOfficeBase
	{
    private static Logger Logger;
    public static readonly int Timeout = 4000;
    static HmiSendOffice()
    {
      Logger = LogManager.GetLogger("WWW");
    }

    #region setup

    public static async Task<SendOfficeResult<DataContractBase>> SendTelegramAsync(DCTelegramSetupId dataToSend)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.SendTelegramSetupAsync(dataToSend));
    }
    public static async Task<SendOfficeResult<DataContractBase>> UpdateTelegramValueAsync(DCTelegramSetupValue dataToSend)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateTelegramValueAsync(dataToSend));
    }
    public static async Task<SendOfficeResult<DataContractBase>> CreateNewVersionOfTelegramAsync(DCTelegramSetupId dataToSend)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.CreateNewVersionOfTelegramAsync(dataToSend));
    }

    public static async Task<SendOfficeResult<DataContractBase>> DeleteSetupAsync(DCTelegramSetupId dataToSend)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteSetupAsync(dataToSend));
    }

    #endregion

    #region delay

    public static async Task<SendOfficeResult<DataContractBase>> SendDelayCatalogueAsync(DCDelayCatalogue dcDelayCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Delay.Name;
      IDelay client = InterfaceHelper.GetFactoryChannel<IDelay>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateDelayCatalogueAsync(dcDelayCatalogue));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendAddDelayCatalogueAsync(DCDelayCatalogue dcDelayCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Delay.Name;
      IDelay client = InterfaceHelper.GetFactoryChannel<IDelay>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.AddDelayCatalogueAsync(dcDelayCatalogue));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendDeleteDelayCatalogueAsync(DCDelayCatalogue dcDelayCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Delay.Name;
      IDelay client = InterfaceHelper.GetFactoryChannel<IDelay>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteDelayCatalogueAsync(dcDelayCatalogue));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendDelayAsync(DCDelay dcDelay)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Delay.Name;
      IDelay client = InterfaceHelper.GetFactoryChannel<IDelay>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateDelayAsync(dcDelay));
    }
    public static async Task<SendOfficeResult<DataContractBase>> DivideDelayAsync(DCDelayToDivide dcDelaytoDivide)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Delay.Name;
      IDelay client = InterfaceHelper.GetFactoryChannel<IDelay>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DivideDelayAsync(dcDelaytoDivide));
    }

    #endregion

    #region schedule

    public static async Task<SendOfficeResult<DataContractBase>> MoveItemInScheduleAsync(DCWorkOrderToSchedule data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdPlaning.Name;
      IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);

      //call method on remote module
      return await HandleSendMethod((IClientChannel)client, targetModuleName, () => client.MoveItemInScheduleAsync(data), true, true);
    }


    //public static async Task<SendOfficeResult<T>> HandleSendMethod2<T>(IClientChannel client, string moduleName, Func<Task<T>> methodOnClientInterface, bool throwException = false, bool isFromHmi = false) where T : new()
    //{
    //  Stopwatch watch = null;
    //  T resultDataContract = new T();
    //  bool success = true;
    //  long executionTime = 0;
    //  ModuleMessage moduleResult = null;
    //  int callLocalNumber = _callNumber++;

    //  //if (!isFromHmi)
    //  //  NotificationController.Info("SEND OFFICE({0}): Calling \"{1}.{2}()\"", callLocalNumber, moduleName, _callingMethodName);


    //  watch = System.Diagnostics.Stopwatch.StartNew();

    //  try
    //  {
    //    if (client.State != CommunicationState.Opened)
    //      throw new Exception("Client not available");

    //    Task<T> resultTask = default(Task<T>);

    //    //watch = System.Diagnostics.Stopwatch.StartNew();

    //    resultTask = methodOnClientInterface();


    //    if (resultTask.Wait(4000))
    //    {
    //      // task completed within timeout
    //      resultDataContract = await resultTask;

    //      if (resultDataContract == null)
    //      {
    //        //if (!isFromHmi)
    //        //  NotificationController.Error("SEND OFFICE({0}): No data received, data contract is null", callLocalNumber);
    //      }
    //    }
    //    else
    //    {
    //      // timeout logic
    //      throw new TimeoutException();
    //    }
    //  }


    //  catch (Exception ex)
    //  {
    //    success = false;
    //    Exception innerExeptionToBeAttached = ex;

    //    if (ex is TimeoutException)
    //    {
    //      moduleResult = new ModuleMessage("S016", $"Timeout during external module call ({4000}[ms])", moduleName);
    //    }
    //    if (ex is AggregateException)
    //    {
    //      (ex as AggregateException).Handle((x) =>
    //      {
    //        if (x is FaultException<ModuleMessage>)
    //        {
    //          moduleResult = (x as FaultException<ModuleMessage>).Detail;
    //          innerExeptionToBeAttached = x.InnerException; //ovewrite
    //          return true;
    //        }
    //        else if (x.InnerException is FaultException<ModuleMessage>)
    //        {
    //          moduleResult = (x.InnerException as FaultException<ModuleMessage>).Detail;
    //          innerExeptionToBeAttached = x.InnerException; //ovewrite
    //          return true;
    //        }
    //        else if (x.InnerException is FaultException)
    //        {
    //          FaultException fe = x.InnerException as FaultException;

    //          moduleResult = new ModuleMessage("S017", "Internal module error", moduleName);
    //          innerExeptionToBeAttached = x.InnerException; //ovewrite
    //          return true;
    //        }
    //        else if (x.InnerException is AggregateException)
    //        {

    //          return true;
    //        }
    //        else
    //        {
    //          moduleResult = new ModuleMessage("S017", "Internal module error", moduleName);
    //          return true;
    //        }
    //      });
    //    }


    //    if (client != null)
    //      InterfaceHelper.AbortChannel((IClientChannel)client);

    //    Exception exceptionToBeThrown = new SMF.RepositoryPatternExt.ModuleMessageException(moduleResult, innerExeptionToBeAttached);


    //    if (throwException)
    //      throw exceptionToBeThrown;
    //    else
    //    {
    //      //if (!isFromHmi)
    //      //  LogHelper.LogException(exceptionToBeThrown, "SEND OFFICE({0}): exception during calling \"{1}.{2}()\"", callLocalNumber, moduleName, _callingMethodName);
    //    }

    //    resultDataContract = new T();
    //  }
    //  finally
    //  {
    //    //if (!isFromHmi)
    //    //  ModuleController.OutgoingCall(moduleName, _callingMethodName);
    //  }

    //  if (client != null)
    //    InterfaceHelper.CloseChannel(client);

    //  executionTime = watch.ElapsedMilliseconds;
    //  watch.Stop();

    //  //if (success)
    //  //{
    //  //  if (!isFromHmi)
    //  //    NotificationController.Info("SEND OFFICE({0}): result: {1} after calling \"{2}.{3}()\" - EXEC TIME {4}[ms]", callLocalNumber, success, moduleName, _callingMethodName, executionTime);
    //  //}
    //  //else
    //  //{
    //  //  if (!isFromHmi)
    //  //    NotificationController.Error("SEND OFFICE({0}): result: {1} after calling \"{2}.{3}()\" - EXEC TIME {4}[ms]", callLocalNumber, success, moduleName, _callingMethodName, executionTime);
    //  //}

    //  return new SendOfficeResult<T>(resultDataContract, success, moduleResult);
    //}



    public static async Task<SendOfficeResult<DataContractBase>> AddTestScheduleAsync(DCTestSchedule dc)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdPlaning.Name;
      IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);

      //call method on remote module
      return await HandleSendMethod((IClientChannel)client, targetModuleName, () => client.AddTestWorkOrderToScheduleAsync(dc));
    }
    public static async Task<SendOfficeResult<DataContractBase>> AddWorkOrderToScheduleAsync(DCWorkOrderToSchedule data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdPlaning.Name;
      IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.AddWorkOrderToScheduleAsync(data));
    }
    public static async Task<SendOfficeResult<DataContractBase>> RemoveItemFromScheduleAsync(DCWorkOrderToSchedule data)
    {

      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdPlaning.Name;
      IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);

        //call method on remote module
        return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.RemoveItemFromScheduleAsync(data));
      
    }

    #endregion

    #region prodManager

    public static async Task<SendOfficeResult<DataContractBase>> SendCreateSteelgradeAsync(DCSteelgrade dcSteelgrade)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.CreateSteelgradeAsync(dcSteelgrade));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendSteelgradeAsync(DCSteelgrade dcSteelgrade)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateSteelgradeAsync(dcSteelgrade));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendDeleteSteelgradeAsync(DCSteelgrade dcSteelgrade)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteSteelgradeAsync(dcSteelgrade));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendWorkOrderAsync(DCWorkOrder dcWorkOrder)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      if (dcWorkOrder.WorkOrderId == 0)
      {
        return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.CreateWorkOrderAsync(dcWorkOrder));
      }
      else
      {
        return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateWorkOrderAsync(dcWorkOrder));
      }
    }

    public static async Task<SendOfficeResult<DataContractBase>> SendMaterialAsync(DCMaterial dcMaterial)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      if(dcMaterial.MaterialId == 0)
      {
        return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.CreateMaterialAsync(dcMaterial));
      }
      else
      {
        return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateMaterialAsync(dcMaterial));
      }
    }

    public static async Task<SendOfficeResult<DataContractBase>> DeleteWorkOrderAsync(DCWorkOrder dcWorkOrder)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteWorkOrderAsync(dcWorkOrder));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendHeatAsync(DCHeat dcHeat)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.CreateHeatAsync(dcHeat));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendCreateMaterialCatalogueAsync(DCMaterialCatalogue dcMaterialCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.CreateMaterialCatalogueAsync(dcMaterialCatalogue));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendMaterialCatalogueAsync(DCMaterialCatalogue dcMaterialCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateMaterialCatalogueAsync(dcMaterialCatalogue));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendDeleteMaterialCatalogueAsync(DCMaterialCatalogue dcMaterialCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteMaterialCatalogueAsync(dcMaterialCatalogue));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendCreateProductCatalogueAsync(DCProductCatalogue dcProductCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.CreateProductCatalogueAsync(dcProductCatalogue));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendProductCatalogueAsync(DCProductCatalogue dcProductCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateProductCatalogueAsync(dcProductCatalogue));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendDeleteProductCatalogueAsync(DCProductCatalogue dcProductCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteProductCatalogueAsync(dcProductCatalogue));
    }

    #endregion

    #region measured values history
    public static async Task<SendOfficeResult<DataContractBase>> AssignMaterials(DCMaterialAssign data)
    {

      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.AssignMaterialsAsync(data));
    }
    public static async Task<SendOfficeResult<DataContractBase>> UnassignMaterial(DCMaterialAssign dataToSend)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UnassignMaterialAsync(dataToSend));
    }
    public static async Task<SendOfficeResult<DataContractBase>> MarkMaterialAsScrap(DCMaterialMarkedAsScrap dataToSend)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.MarkMaterialAsScrapAsync(dataToSend));
    }

    #endregion

    #region zebra

    public static async Task<SendOfficeResult<DCZebraResponse>> RequestPDFFromZebraAsync(DCZebraRequest dataToSend)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ZebraPrinter.Name;
      IZebraPC client = InterfaceHelper.GetFactoryChannel<IZebraPC>(targetModuleName);

      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.RequestPDFForHMIAsync(dataToSend));
		}

    #endregion

    #region Rollshop

    public static async Task<SendOfficeResult<DataContractBase>> InsertRollAsync(DCRollData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.InsertRollAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> UpdateRollAsync(DCRollData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateRollAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> ScrapRollAsync(DCRollData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.ScrapRollAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> DeleteRollAsync(DCRollData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteRollAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> InsertRollTypeAsync(DCRollTypeData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.InsertRollTypeAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> UpdateRollTypeAsync(DCRollTypeData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateRollTypeAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> DeleteRollTypeAsync(DCRollTypeData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteRollTypeAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> InsertGrooveTemplateAsync(DCGrooveTemplateData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.InsertGrooveTemplateAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> UpdateGrooveTemplateAsync(DCGrooveTemplateData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateGrooveTemplateAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> DeleteGrooveTemplateAsync(DCGrooveTemplateData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteGrooveTemplateAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> InsertRollSetAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.InsertRollSetAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> AssembleRollSetAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.AssembleRollSetAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> UpdateRollSetStatusAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.UpdateRollSetStatusAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> ConfirmRollSetStatusAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.ConfirmRollSetStatusAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> DisassembleRollSetAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.DisassembleRollSetAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> DeleteRollSetAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.DeleteRollSetAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> InsertCassetteTypeAsync(DCCassetteTypeData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.InsertCassetteTypeAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> UpdateCassetteTypeAsync(DCCassetteTypeData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateCassetteTypeAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> DeleteCassetteTypeAsync(DCCassetteTypeData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteCassetteTypeAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> InsertCassetteAsync(DCCassetteData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.InsertCassetteAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> UpdateCassetteAsync(DCCassetteData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateCassetteAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> DeleteCassetteAsync(DCCassetteData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteCassetteAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> RollChangeActionAsync(DCRollChangeOperationData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.RollChangeActionAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> RollSetToCassetteAction(DCRollSetToCassetteAction data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.RollSetToCassetteAction(data));
    }


    public static async Task<SendOfficeResult<DataContractBase>> CancelRollSetStatusAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.CancelRollSetStatusAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> UpdateGroovesToRollSetAsync(DCRollSetGrooveSetup data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateGroovesToRollSetAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> UpdateGroovesStatusesAsync(DCRollSetGrooveSetup data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateGroovesStatusesAsync(data));
    }

    public static async Task<SendOfficeResult<DataContractBase>> UpdateStandConfigurationAsync(DCStandConfigurationData data)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateStandConfigurationAsync(data));
    }


    #endregion

    public static async Task<SendOfficeResult<DCTestData>> SendMessageToA(DCTestData entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.ModuleA.Name;
      IModuleA client = InterfaceHelper.GetFactoryChannel<IModuleA>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.TestMethodOnModuleA(entryDataContract));
    }

    #region Maintenance

    public static async Task<SendOfficeResult<DataContractBase>> SendEquipmentGroupCreateRequest(DCEquipmentGroup entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.CreateEquipmentGroupAsync(entryDataContract));
    }

    public static async Task<SendOfficeResult<DataContractBase>> SendEquipmentGroupUpdateRequest(DCEquipmentGroup entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateEquipmentGroupAsync(entryDataContract));
    }

    public static async Task<SendOfficeResult<DataContractBase>> SendEquipmentGroupDeleteRequest(DCEquipmentGroup entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteEquipmentGroupAsync(entryDataContract));
    }

    public static async Task<SendOfficeResult<DataContractBase>> SendEquipmentCreateRequest(DCEquipment entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.CreateEquipmentAsync(entryDataContract));
    }

    public static async Task<SendOfficeResult<DataContractBase>> SendEquipmentUpdateRequest(DCEquipment entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateEquipmentAsync(entryDataContract));
    }

    public static async Task<SendOfficeResult<DataContractBase>> SendEquipmentDeleteRequest(DCEquipment entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteEquipmentAsync(entryDataContract));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendEquipmentCloneRequest(DCEquipment entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.CloneEquipmentAsync(entryDataContract));
    }

    #endregion

    #region Quality

    public static async Task<SendOfficeResult<DataContractBase>> SendAddDefectCatalogue(DCDefectCatalogue dcDefectCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.AddDefectCatalogueAsync(dcDefectCatalogue));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendDefectCatalogue(DCDefectCatalogue dcDefectCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.UpdateDefectCatalogueAsync(dcDefectCatalogue));
    }
    public static async Task<SendOfficeResult<DataContractBase>> SendDeleteDefectCatalogue(DCDefectCatalogue dcDefectCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.DeleteDefectCatalogueAsync(dcDefectCatalogue));
    }

    public static async Task<SendOfficeResult<DataContractBase>> AssignQualityAsync(DCQualityAssignment dCQualityAssignment)
    {
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return await HandleHMISendMethod((IClientChannel)client, targetModuleName, () => client.AssignQualityAsync(dCQualityAssignment));
    }

    

    #endregion
  }
}
