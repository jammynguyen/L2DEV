using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using PE.ADP.Adapter.Module;
using PE.ADP.Managers;
using PE.Helpers;
using PE.DTO.External.MVHistory;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.DBAdapter;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.Setup;
using PE.Interfaces.Managers;
using SMF.Core.DC;
using PE.DTO.Internal.TcpProxy;
using Ninject.Parameters;

namespace PE.ADP.Adapter.Communication
{
  internal class ExternalAdapterHandler
  {
    private static readonly IL3CommunicationManager _l3CommunicationManager;
    private static readonly IL1CommunicationManager _l1CommunicationManager;

    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IL3CommunicationManager>().To<L3CommunicationManager>();
        kernel.Bind<IL1CommunicationManager>().To<L1CommunicationManager>();
        _l3CommunicationManager = kernel.Get<IL3CommunicationManager>(new ConstructorArgument("sendOffice", new SendOffice()));
        _l1CommunicationManager = kernel.Get<IL1CommunicationManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    //L3 methods
    internal static async Task<DCWorkOrderStatus> ExternalProccesWorkOrderMessageAsync(DCL3L2WorkOrderDefinition message)
    {
      // return result
      return await _l3CommunicationManager.ProccesExtWorkOrderMessageAsync(message);
    }
    //L1 methods
    internal static async Task<DCPEBasId> ExternalProcessL1BaseIdRequestAsync(DCL1BasIdRequest message)
    {
      // return result
      return await _l1CommunicationManager.ProcessL1BaseIdRequestAsync(message);
    }
    internal static async Task<DataContractBase> ExternalProcessL1CutMessageAsync(DCL1CutData message)
    {
      // return result
      return await _l1CommunicationManager.ProcessL1CutMessageAsync(message);
    }
    internal static async Task<DCPEBasId> ExternalProcessL1DivisionMessageAsync(DCL1MaterialDivision message)
    {
      // return result
      return await _l1CommunicationManager.ProcessL1DivisionMessageAsync(message);
    }
    internal static async Task<DataContractBase> ExternalProcessL1MeasDataMessageAsync(DCMeasData message)
    {
      // return result
      return await _l1CommunicationManager.ProcessL1MeasDataMessageAsync(message);
    }
    internal static async Task<DataContractBase> ExternalProcessL1SampleMeasMessageAsync(DCMeasDataSample message)
    {
      // return result
      return await _l1CommunicationManager.ProcessL1SampleMeasMessageAsync(message);
    }
    internal static async Task<DataContractBase> ExternalProcessL1ScrapMessageAsync(DCL1ScrapData message)
    {
      // return result
      return await _l1CommunicationManager.ProcessL1ScrapMessageAsync(message);
    }
    //internal static async Task<DataContractBase> ExternalSendTelegramSetupByteDataAsync(DCTelegramSetup message)
    //{
    //  // return result
    //  return await _l1CommunicationManager.ProcessSendTelegramSetupByteDataAsync(message);
    //}

    internal static async Task<DataContractBase> ExternalSendTelegramSetupDataAsync(DCCommonSetupStructure message)
    {
      // return result
			// create new manager in case when binary serialized data have to be sent to L1
      return await _l1CommunicationManager.ProcessSendTelegramSetupStructureDataAsync(message);
    }
    internal static async Task<DataContractBase> ExternalSendSetupDataRequestAsync(DCCommonSetupStructure message)
    {
      // return result
      // create new manager in case when binary serialized data have to be sent to L1
      return await _l1CommunicationManager.ProcessExternalSendSetupDataRequestAsync(message);
    }

    internal static async Task<DataContractBase> SendSetupUpdateMessageAsync(DCCommonSetupStructure message)
    {
      return await _l1CommunicationManager.ProcessSetupUpdateMessageAsync(message);
    }
  }
}
