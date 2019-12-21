using PE.DTO.Internal.Setup;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.STP.Setup.Communication
{
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
	public class ExternalAdapter : ExternalAdapterBase, PE.Interfaces.Lite.ISetup
	{
		#region ctor

		public ExternalAdapter() : base(typeof(PE.Interfaces.Lite.ISetup)) { }

		#endregion

		public async Task<DataContractBase> SendTelegramSetupAsync(DCTelegramSetupId message)
		{
			return await HandleIncommingMethod(ExternalAdapterHandler.ForceSendTelegramSetupAsync,message);
		}

		public async Task<DataContractBase> UpdateTelegramValueAsync(DCTelegramSetupValue message)
		{
			return await HandleIncommingMethod(ExternalAdapterHandler.UpdateTelegramValueAsync,message);

		}

    public async Task<DataContractBase> CreateNewVersionOfTelegramAsync(DCTelegramSetupId message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.CreateNewVersionOfTelegramAsync, message);

    }
    public async Task<DataContractBase> DeleteSetupAsync(DCTelegramSetupId message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.DeleteSetupAsync, message);
    }
		/// <summary>
    /// this metod can be called from HMI in order to request Setup update from L1. 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<DataContractBase> RequestUpdateSetupFromL1Async(DCTelegramSetupId message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.RequestUpdateSetupFromL1Async, message);
    }

    public async Task<DataContractBase> UpdateSetupFromL1Async(DCCommonSetupStructure message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateSetupFromL1Async, message);
    }
  }
}
