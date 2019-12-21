using PE.DTO.Internal.Setup;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Lite
{
	[ServiceContract(SessionMode = SessionMode.Allowed)]
	public interface ISetup : IBaseModule
	{
		[OperationContract]
		[FaultContract(typeof(ModuleMessage))]
		Task<DataContractBase> SendTelegramSetupAsync(DCTelegramSetupId dcData);

		[OperationContract]
		[FaultContract(typeof(ModuleMessage))]
		Task<DataContractBase> UpdateTelegramValueAsync(DCTelegramSetupValue dcData);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateNewVersionOfTelegramAsync(DCTelegramSetupId dcData);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSetupAsync(DCTelegramSetupId dcData);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateSetupFromL1Async(DCCommonSetupStructure dcData);
    
  }
}
