using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.Schedule;
using SMF.Core.DC;
using SMF.Core.Helpers;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.PPL.ProdPlaning.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, PE.Interfaces.Lite.IProdPlaning
  {
    #region ctor

    public ExternalAdapter() : base(typeof(PE.Interfaces.Lite.IProdPlaning)) { }
    #endregion

    #region hmi

    public async Task<DataContractBase> MoveItemInScheduleAsync(DCWorkOrderToSchedule message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.MoveItemInSchedule, message);
    }

    public async Task<DataContractBase> RemoveItemFromScheduleAsync(DCWorkOrderToSchedule message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.RemoveItemFromSchedule, message);
    }

    public async Task<DataContractBase> AddTestWorkOrderToScheduleAsync(DCTestSchedule dc)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.AddTestWorkOrderToSchedule, dc);
    }


    #endregion

    #region prm

    public async Task<DataContractBase> AddWorkOrderToScheduleAsync(DCWorkOrderToSchedule message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.AddWorkOrderToSchedule, message);
    }

    #endregion

    #region MVH

    public async Task<DCL1L3MaterialConnector> RequestL3MaterialAsync(DCFeatureRelatedOperationData data)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.RequestL3MaterialAsync, data);
    }
    public async Task<DataContractBase> RemoveFinishedOrdersFromScheduleAsync(DataContractBase data)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.RemoveFinishedOrdersFromScheduleAsync, data);
    }

    #endregion




    //protected async Task<S> HandleIncommingMethod2<T, S>(Func<T, Task<S>> externalAdapterHandlerMethod, T message) where T : DataContractBase
    //{
    //  int callLocalNumber = _callNumber++;

    //  if ((message as DataContractBase).Sender == null || (message as DataContractBase).ModuleWarningMessage == null)
    //  {
    //    NotificationController.Info("EXTERNAL ADAPTER({0}): Data contract not initialized! Please verify data contract", callLocalNumber);

    //    if ((message as DataContractBase).Sender == null)
    //    {
    //      (message as DataContractBase).Sender = "Default";
    //    }
    //    else if ((message as DataContractBase).ModuleWarningMessage == null)
    //    {
    //      (message as DataContractBase).ModuleWarningMessage = new ModuleWarningMessage() { ModuleName = "Default" };
    //    }
    //  }

    //  string callingMethodName = GetCallingMethodName();
    //  NotificationController.Info("EXTERNAL ADAPTER({0}): Method \"{1}\" called by module: \"{2}\"", callLocalNumber, callingMethodName, (message as DataContractBase).Sender);
    //  NotificationController.Info("EXTERNAL ADAPTER({0}): {1}", callLocalNumber, (message as DataContractBase).GetDataContractLogText());
    //  //ModuleController.IncommingCall((message as DataContractBase).Sender, callingMethodName);

    //  try
    //  {
    //    S resultDataContract = default(S);
    //    System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

    //    Task<S> resultTask = externalAdapterHandlerMethod(message);
    //    if (resultTask.Wait(SMF.Core.Constants.ModuleMethodTimeout))
    //    {
    //      // task completed within timeout
    //      resultDataContract = await resultTask;
    //    }
    //    else
    //    {
    //      // timeout logic
    //      throw new TimeoutException($"Timeout ({SMF.Core.Constants.ModuleMethodTimeout}[ms]) reached.");
    //    }

    //    watch.Stop();
    //    NotificationController.Info("EXTERNAL ADAPTER({0}): Called method: {1}, EXEC TIME: {2}[ms] - END", callLocalNumber, callingMethodName, watch.ElapsedMilliseconds);
    //    return resultDataContract;
    //  }
    //  catch (SMF.RepositoryPatternExt.ModuleMessageException e)
    //  {
    //    LogHelper.LogException(e);
    //    throw new FaultException<ModuleMessage>(e.ModuleMessage);
    //  }
    //  catch (Exception ex)
    //  {
    //    LogHelper.LogException(ex);

    //    if (ex is AggregateException)
    //    {
    //      Exception exception = (ex as AggregateException).InnerException;

				//	if(exception is ModuleMessageException)
    //      {
    //        throw new FaultException<ModuleMessage>((exception as ModuleMessageException).ModuleMessage);
    //      }
				//	else
    //      {
    //        throw new FaultException(ex.Message);
    //      }
    //    }
				//else
				//	throw new FaultException(ex.Message);
    //  }
    //}
  }
}
