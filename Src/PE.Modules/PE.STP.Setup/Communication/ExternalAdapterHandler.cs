using Ninject;
using Ninject.Parameters;
using PE.DTO.Internal.Setup;
using PE.Interfaces.Interfaces.Managers;
using PE.Interfaces.Managers;
using PE.STP.Managers;
using PE.STP.Setup.Module;
using SMF.Core.DC;
using System;
using System.Threading.Tasks;

namespace PE.STP.Setup.Communication
{
	internal class ExternalAdapterHandler
	{
		private static readonly ISetupTelegramsManager _setupTelegramsManager;

		static ExternalAdapterHandler()
		{
			using (IKernel kernel = new StandardKernel())
			{
				kernel.Bind<ISetupTelegramsManager>().To<SetupTelegramsManager>();

        _setupTelegramsManager = kernel.Get<ISetupTelegramsManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
		}

		internal static async Task<DataContractBase> ForceSendTelegramSetupAsync(DCTelegramSetupId message)
		{
			return await _setupTelegramsManager.ProcessSendTelegramSetupAsync(message);
		}
		internal static async Task<DataContractBase> UpdateTelegramValueAsync(DCTelegramSetupValue message)
		{
			return await _setupTelegramsManager.UpdateTelegramValueAsync(message);
		}

    internal static async Task<DataContractBase> CreateNewVersionOfTelegramAsync(DCTelegramSetupId message)
    {
      return await _setupTelegramsManager.CreateNewVersionOfTelegramAsync(message);
    }
    internal static async Task<DataContractBase> DeleteSetupAsync(DCTelegramSetupId message)
    {
      return await _setupTelegramsManager.DeleteSetupAsync(message);
    }
    internal static async Task<DataContractBase> RequestUpdateSetupFromL1Async(DCTelegramSetupId message)
    {
      return await _setupTelegramsManager.RequestUpdateSetupFromL1Async(message);
    }

    internal static async Task<DataContractBase> UpdateSetupFromL1Async(DCCommonSetupStructure message)
    {
      return await _setupTelegramsManager.UpdateSetupFromL1Async(message);
    }
  }
}
