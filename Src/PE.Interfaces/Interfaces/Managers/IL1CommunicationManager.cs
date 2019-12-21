using PE.DTO.External.MVHistory;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.Setup;
using PE.DTO.Internal.TcpProxy;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers
{
  public interface IL1CommunicationManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DCPEBasId> ProcessL1BaseIdRequestAsync(DCL1BasIdRequest message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessL1CutMessageAsync(DCL1CutData message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessL1ScrapMessageAsync(DCL1ScrapData message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DCPEBasId> ProcessL1DivisionMessageAsync(DCL1MaterialDivision message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessL1MeasDataMessageAsync(DCMeasData message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessL1SampleMeasMessageAsync(DCMeasDataSample message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessSendTelegramSetupByteDataAsync(DCTelegramSetup message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessSendTelegramSetupStructureDataAsync(DCCommonSetupStructure message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessExternalSendSetupDataRequestAsync(DCCommonSetupStructure message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessSetupUpdateMessageAsync(DCCommonSetupStructure message);






    //Task<DataContractBase> ProcessL1BaseIdRequest(DCL1BasIdRequestExt message);
    //Task<DataContractBase> ProcessPEBaseIdRequest(DCPEBasId dcBaseIdMsg);
    //Task<DataContractBase> ProcessL1MaterialDivisionMessage(DCL1MaterialDivisionExt message);
    //Task<DataContractBase> ProcessL1CutDataMessage(DCL1CutDataExt message);
    //Task<DataContractBase> ProcessL1ScrapDataTelegram(DCL1ScrapDataExt message);
    //Task<DataContractBase> SendFinishedSignalToAdapter(DCL1MaterialFinishedExt message);

  }
}
