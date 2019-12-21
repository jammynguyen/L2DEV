using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Parameters;
using PE.DTO.Internal.RollShop;
using PE.Interfaces.Interfaces.Lite;
using PE.Interfaces.Interfaces.Managers;
using PE.Interfaces.Managers;
using PE.RLS.Managers;
using SMF.Core.DC;

namespace PE.RLS.RollShop.Communication
{
  internal class ExternalAdapterHandler
  {
    private static readonly IRollManager _rollManager;
    private static readonly IRollTypeManager _rollTypeManager;
    private static readonly IGrooveTemplateManager _grooveTemplateManager;
    private static readonly IRollSetManager _rollSetManager;
    private static readonly ICassetteTypeManager _cassetteTypeManager;
    private static readonly ICassetteManager _cassetteManager;
    private static readonly IRollChangeManager _rollChangeManager;
    private static readonly IRollSetHistoryManager _rollSetHistoryManager;
    private static readonly ICustomRollGroovesManager _customRollGroovesManager;

    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IRollManager>().To<RollManager>();
        kernel.Bind<IRollTypeManager>().To<RollTypeManager>();
        kernel.Bind<IGrooveTemplateManager>().To<GrooveTemplateManager>();
        kernel.Bind<IRollSetManager>().To<RollSetManager>();
        kernel.Bind<ICassetteTypeManager>().To<CassetteTypeManager>();
        kernel.Bind<ICassetteManager>().To<CassetteManager>();
        kernel.Bind<IRollChangeManager>().To<RollChangeManager>();
        kernel.Bind<IRollSetHistoryManager>().To<RollSetHistoryManager>();
        kernel.Bind<ICustomRollGroovesManager>().To<CustomRollGroovesManager>();

        _rollManager = kernel.Get<IRollManager>(new ConstructorArgument("sendOffice", new SendOffice()));
        _rollTypeManager = kernel.Get<IRollTypeManager>(new ConstructorArgument("sendOffice", new SendOffice()));
        _grooveTemplateManager = kernel.Get<IGrooveTemplateManager>(new ConstructorArgument("sendOffice", new SendOffice()));
        _rollSetManager = kernel.Get<IRollSetManager>(new ConstructorArgument("sendOffice", new SendOffice()));
        _cassetteTypeManager = kernel.Get<ICassetteTypeManager>(new ConstructorArgument("sendOffice", new SendOffice()));
        _cassetteManager = kernel.Get<ICassetteManager>(new ConstructorArgument("sendOffice", new SendOffice()));
        _rollChangeManager = kernel.Get<IRollChangeManager>(new ConstructorArgument("sendOffice", new SendOffice()));
        _rollSetHistoryManager = kernel.Get<IRollSetHistoryManager>(new ConstructorArgument("sendOffice", new SendOffice()));
        _customRollGroovesManager = kernel.Get<ICustomRollGroovesManager>(new ConstructorArgument("sendOffice", new SendOffice()));

      }
    }

    internal static async Task<DataContractBase> InsertRollAsync(DCRollData message)
    {
      return await _rollManager.InsertRollAsync(message);
    }

    internal static async Task<DataContractBase> UpdateRollAsync(DCRollData message)
    {
      return await _rollManager.UpdateRollAsync(message);
    }

    internal static async Task<DataContractBase> ScrapRollAsync(DCRollData message)
    {
      return await _rollManager.ScrapRollAsync(message);
    }

    internal static async Task<DataContractBase> DeleteRollAsync(DCRollData message)
    {
      return await _rollManager.DeleteRollAsync(message);
    }

    internal static async Task<DataContractBase> UpdateStandConfigurationAsync(DCStandConfigurationData message)
    {
      return await _rollManager.UpdateStandConfigurationAsync(message);
    }

    internal static async Task<DataContractBase> InsertRollTypeAsync(DCRollTypeData message)
    {
      return await _rollTypeManager.InsertRollTypeAsync(message);
    }

    internal static async Task<DataContractBase> UpdateRollTypeAsync(DCRollTypeData message)
    {
      return await _rollTypeManager.UpdateRollTypeAsync(message);
    }

    internal static async Task<DataContractBase> DeleteRollTypeAsync(DCRollTypeData message)
    {
      return await _rollTypeManager.DeleteRollTypeAsync(message);
    }

    internal static async Task<DataContractBase> InsertGrooveTemplateAsync(DCGrooveTemplateData message)
    {
      return await _grooveTemplateManager.InsertGrooveTemplateAsync(message);
    }

    internal static async Task<DataContractBase> UpdateGrooveTemplateAsync(DCGrooveTemplateData message)
    {
      return await _grooveTemplateManager.UpdateGrooveTemplateAsync(message);
    }

    internal static async Task<DataContractBase> DeleteGrooveTemplateAsync(DCGrooveTemplateData message)
    {
      return await _grooveTemplateManager.DeleteGrooveTemplateAsync(message);
    }

    internal static async Task<DataContractBase> InsertRollSetAsync(DCRollSetData message)
    {
      return await _rollSetManager.InsertRollSetAsync(message);
    }

    //internal static async Task<DataContractBase> UpdateRollSetAsync(DCRollSetData message)
    //{
    //  return await _rollSetManager.UpdateRollSetAsync(message);
    //}

    internal static async Task<DataContractBase> AssembleRollSetAsync(DCRollSetData message)
    {
      return await _rollSetManager.AssembleRollSetAsync(message);
    }

    internal static async Task<DataContractBase> DeleteRollSetAsync(DCRollSetData message)
    {
      return await _rollSetManager.DeleteRollSetAsync(message);
    }

    internal static async Task<DataContractBase> DisassembleRollSetAsync(DCRollSetData message)
    {
      return await _rollSetManager.DisassembleRollSetAsync(message);
    }

    internal static async Task<DataContractBase> UpdateRollSetStatusAsync(DCRollSetData message)
    {
      return await _rollSetManager.UpdateRollSetStatusAsync(message);
    }

    internal static async Task<DataContractBase> ConfirmRollSetStatusAsync(DCRollSetData message)
    {
      return await _rollSetManager.ConfirmRollSetStatusAsync(message);
    }

    internal static async Task<DataContractBase> InsertCassetteTypeAsync(DCCassetteTypeData message)
    {
      return await _cassetteTypeManager.InsertCassetteTypeAsync(message);
    }

    internal static async Task<DataContractBase> UpdateCassetteTypeAsync(DCCassetteTypeData message)
    {
      return await _cassetteTypeManager.UpdateCassetteTypeAsync(message);
    }

    internal static async Task<DataContractBase> DeleteCassetteTypeAsync(DCCassetteTypeData message)
    {
      return await _cassetteTypeManager.DeleteCassetteTypeAsync(message);
    }

    internal static async Task<DataContractBase> InsertCassetteAsync(DCCassetteData message)
    {
      return await _cassetteManager.InsertCassetteAsync(message);
    }

    internal static async Task<DataContractBase> UpdateCassetteAsync(DCCassetteData message)
    {
      return await _cassetteManager.UpdateCassetteAsync(message);
    }

    internal static async Task<DataContractBase> DeleteCassetteAsync(DCCassetteData message)
    {
      return await _cassetteManager.DeleteCassetteAsync(message);
    }

    internal static async Task<DataContractBase> RollChangeActionAsync(DCRollChangeOperationData message)
    {
      return await _rollChangeManager.RollChangeActionAsync(message);
    }

    internal static async Task<DataContractBase> RollSetToCassetteAction(DCRollSetToCassetteAction message)
    {
      return await _rollChangeManager.RollSetToCassetteAction(message);
    }

    internal static async Task<DataContractBase> UpdateGroovesToRollSetAsync(DCRollSetGrooveSetup message)
    {
      return await _rollSetHistoryManager.UpdateGroovesToRollSetAsync(message);
    }

    internal static async Task<DataContractBase> UpdateGroovesStatusesAsync(DCRollSetGrooveSetup message)
    {
      return await _customRollGroovesManager.SelectActiveGrooves(message);
    }

    internal static async Task<DataContractBase> AccumulateRollsUsageAsync(DCRollsAccu message)
    {
      return await _customRollGroovesManager.AccumulateRollsUsageAsync(message);
    }

    
  }
}
