using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.RollShop;
using PE.Interfaces.Lite;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.RLS.RollShop.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, IRollShop
  {
    #region ctor

    public ExternalAdapter() : base(typeof(PE.Interfaces.Lite.IRollShop)) { }

    #endregion

    #region rollManagement
    public async Task<DataContractBase> InsertRollAsync(DCRollData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.InsertRollAsync, message);
    }

    public async Task<DataContractBase> UpdateRollAsync(DCRollData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateRollAsync, message);
    }

    public async Task<DataContractBase> ScrapRollAsync(DCRollData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ScrapRollAsync, message);
    }

    public async Task<DataContractBase> DeleteRollAsync(DCRollData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.DeleteRollAsync, message);
    }
    #endregion

    #region RollType
    public async Task<DataContractBase> InsertRollTypeAsync(DCRollTypeData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.InsertRollTypeAsync, message);
    }

    public async Task<DataContractBase> UpdateRollTypeAsync(DCRollTypeData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateRollTypeAsync, message);
    }

    public async Task<DataContractBase> DeleteRollTypeAsync(DCRollTypeData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.DeleteRollTypeAsync, message);
    }
    #endregion

    #region GrooveTemplate

    public async Task<DataContractBase> InsertGrooveTemplateAsync(DCGrooveTemplateData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.InsertGrooveTemplateAsync, message);
    }

    public async Task<DataContractBase> UpdateGrooveTemplateAsync(DCGrooveTemplateData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateGrooveTemplateAsync, message);
    }

    public async Task<DataContractBase> DeleteGrooveTemplateAsync(DCGrooveTemplateData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.DeleteGrooveTemplateAsync, message);
    }
    #endregion

    #region RollSet

    public async Task<DataContractBase> InsertRollSetAsync(DCRollSetData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.InsertRollSetAsync, message);
    }

    //public async Task<DataContractBase> UpdateRollSetAsync(DCRollSetData message)
    //{
    //  return await HandleIncommingMethod(ExternalAdapterHandler.UpdateRollSetAsync, message);
    //}

    public async Task<DataContractBase> DeleteRollSetAsync(DCRollSetData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.DeleteRollSetAsync, message);
    }

    public async Task<DataContractBase> AssembleRollSetAsync(DCRollSetData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.AssembleRollSetAsync, message);
    }

    public async Task<DataContractBase> ConfirmRollSetStatusAsync(DCRollSetData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ConfirmRollSetStatusAsync, message);
    }

    public async Task<DataContractBase> UpdateRollSetStatusAsync(DCRollSetData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateRollSetStatusAsync, message);
    }

    public async Task<DataContractBase> DisassembleRollSetAsync(DCRollSetData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.DisassembleRollSetAsync, message);
    }
    #endregion

    #region CassetteType
    public async Task<DataContractBase> InsertCassetteTypeAsync(DCCassetteTypeData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.InsertCassetteTypeAsync, message);
    }

    public async Task<DataContractBase> UpdateCassetteTypeAsync(DCCassetteTypeData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateCassetteTypeAsync, message);
    }

    public async Task<DataContractBase> DeleteCassetteTypeAsync(DCCassetteTypeData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.DeleteCassetteTypeAsync, message);
    }
    #endregion

    #region Cassette
    public async Task<DataContractBase> InsertCassetteAsync(DCCassetteData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.InsertCassetteAsync, message);
    }

    public async Task<DataContractBase> UpdateCassetteAsync(DCCassetteData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateCassetteAsync, message);
    }

    public async Task<DataContractBase> DeleteCassetteAsync(DCCassetteData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.DeleteCassetteAsync, message);
    }
    #endregion

    #region RollChange
    public async Task<DataContractBase> RollChangeActionAsync(DCRollChangeOperationData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.RollChangeActionAsync, message);
    }

    public async Task<DataContractBase> RollSetToCassetteAction(DCRollSetToCassetteAction message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.RollSetToCassetteAction, message);
    }
    #endregion

    #region GrindingTurning
    public async Task<DataContractBase> CancelRollSetStatusAsync(DCRollSetData message)
    {
      //  return await HandleIncommingMethod(ExternalAdapterHandler.CancelRollSetStatusAsync, message);
      return new DataContractBase();
    }

    public async Task<DataContractBase> UpdateGroovesToRollSetAsync(DCRollSetGrooveSetup message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateGroovesToRollSetAsync, message);
    }

    public async Task<DataContractBase> UpdateStandConfigurationAsync(DCStandConfigurationData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateStandConfigurationAsync, message);
    }

    public async Task<DataContractBase> UpdateGroovesStatusesAsync(DCRollSetGrooveSetup message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateGroovesStatusesAsync, message);
    }

    public async Task<DataContractBase> AccumulateRollsUsageAsync(DCRollsAccu message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.AccumulateRollsUsageAsync, message);
    }

    
    #endregion
  }
}
