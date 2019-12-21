using PE.Common;
using PE.DbEntity.Models;
using PE.DLS.Handlers;
using PE.DTO.Internal.Delay;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Managers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.DLS.Managers
{
  public class DelayCatalogueManager : IDelayCatalogueManager
  {
    #region members

    private readonly IDelayManagerCatalogueSendOffice _sendOffice;

    #endregion
    #region handlers

    private readonly DelayCatalogueHandler _delayCatalogueHandler;

    #endregion
    #region ctor


    public DelayCatalogueManager(IDelayManagerCatalogueSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _delayCatalogueHandler = new DelayCatalogueHandler();
    }
    #endregion
    #region func
    public virtual async Task<DataContractBase> AddDelayCatalogueAsync(DCDelayCatalogue dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          //DLSDelayCatalogue delayCatalogue = _delayCatalogueHandler.GetDelayCatalogueById(ctx, dc.Id);

          DLSDelayCatalogue delayCatalogue = _delayCatalogueHandler.CreateNewDelayCatalogue(ctx, dc);
          ctx.DLSDelayCatalogues.Add(delayCatalogue);

          await ctx.SaveChangesAsync();

          await ModuleController.HmiRefresh(HMIRefreshKeys.DelayCatalogue);
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.AC_DelayCatalogueUpdate, "Error while updating Delay Catalogue ");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.AC_DelayCatalogueUpdate, "Error while updating Delay Catalogue ");

      }

      return result;
    }
    public virtual async Task<DataContractBase> UpdateDelayCatalogueAsync(DCDelayCatalogue dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          DLSDelayCatalogue delayCatalogue = _delayCatalogueHandler.GetDelayCatalogueById(ctx, dc.Id);

          delayCatalogue = _delayCatalogueHandler.UpdateDelayCatalogue(ctx, delayCatalogue, dc);

          await ctx.SaveChangesAsync();

          await ModuleController.HmiRefresh(HMIRefreshKeys.DelayCatalogue);
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.AC_DelayCatalogueUpdate, "Error while updating Delay Catalogue ");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.AC_DelayCatalogueUpdate, "Error while updating Delay Catalogue ");

      }

      return result;
    }
    public virtual async Task<DataContractBase> DeleteDelayCatalogueAsync(DCDelayCatalogue dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          DLSDelayCatalogue delayCatalogue = _delayCatalogueHandler.GetDelayCatalogueById(ctx, dc.Id);

          ctx.DLSDelayCatalogues.Remove(delayCatalogue);
          await ctx.SaveChangesAsync();

          await ModuleController.HmiRefresh(HMIRefreshKeys.DelayCatalogue);
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.AC_DelayCatalogueUpdate, "Error while deleting Delay Catalogue ");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.AC_DelayCatalogueUpdate, "Error while deleting Delay Catalogue ");

      }

      return result;
    }

    #endregion
    #region private
    #endregion
  }
}
