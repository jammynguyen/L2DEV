using PE.DTO.Internal.Setup;
using PE.Interfaces.Managers;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers
{
	public interface ISetupTelegramsManager : IManagerBase
	{
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessSendTelegramSetupAsync(DCTelegramSetupId message);
    [FaultContract(typeof(ModuleMessage))]
		Task<DataContractBase> UpdateTelegramValueAsync(DCTelegramSetupValue message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateNewVersionOfTelegramAsync(DCTelegramSetupId message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSetupAsync(DCTelegramSetupId message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RequestUpdateSetupFromL1Async(DCTelegramSetupId message);
    Task<DataContractBase> UpdateSetupFromL1Async(DCCommonSetupStructure message);
  }
}
