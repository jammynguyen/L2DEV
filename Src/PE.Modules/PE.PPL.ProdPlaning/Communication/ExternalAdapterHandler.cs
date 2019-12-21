using Ninject;
using Ninject.Parameters;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.Schedule;
using PE.Interfaces.Managers;
using PE.PPL.Managers;
using PE.PPL.ProdPlaning.Module;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.PPL.ProdPlaning.Communication
{
  internal class ExternalAdapterHandler
  {
    private static readonly IScheduleManager _scheduleManager;

    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IScheduleManager>().To<ScheduleManager>();
        _scheduleManager = kernel.Get<IScheduleManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    internal static async Task<DataContractBase> AddWorkOrderToSchedule(DCWorkOrderToSchedule message)
    {
      return await _scheduleManager.AddWorkOrderToScheduleAsync(message);
    }

    internal static async Task<DataContractBase> RemoveItemFromSchedule(DCWorkOrderToSchedule message)
    {
      return await _scheduleManager.RemoveItemFromScheduleAsync(message);
    }

    internal static async Task<DataContractBase> MoveItemInSchedule(DCWorkOrderToSchedule message)
    {
      return await _scheduleManager.MoveItemInScheduleAsync(message);
    }

    internal static async Task<DataContractBase> AddTestWorkOrderToSchedule(DCTestSchedule dc)
    {
      return await _scheduleManager.AddTestWorkOrderToScheduleAsync(dc);
    }

    internal static async Task<DCL1L3MaterialConnector> RequestL3MaterialAsync(DCFeatureRelatedOperationData data)
    {
      return await _scheduleManager.RequestL3MaterialForRawMaterialAsync(data);
    }
    //internal static async Task<DataContractBase> ProcessTriggerAsync(DCTriggerData dcTriggerData)
    //{
    //  return await _scheduleManager.ProcessTriggerAsync(dcTriggerData);
    //}

    internal static async Task<DataContractBase> RemoveFinishedOrdersFromScheduleAsync(DataContractBase data)
    {
      return await _scheduleManager.RemoveFinishedOrdersFromScheduleAsync(data);
    }
  }
}
