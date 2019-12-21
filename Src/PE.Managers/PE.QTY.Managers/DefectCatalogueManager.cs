using PE.Common;
using PE.DbEntity.Models;
using PE.DTO.Internal.Quality;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Managers;
using PE.QTY.Handlers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.QTY.Managers
{
  public class DefectCatalogueManager: IDefectCatalogueManager
  {
    #region members

    #endregion
    #region handlers

    private readonly DefectCatalogueHandler _defectCatalogueHandler;

    #endregion
    #region ctor
    public DefectCatalogueManager()
    {
      _defectCatalogueHandler = new DefectCatalogueHandler();
    }
    #endregion

    public virtual async Task<DataContractBase> AddDefectCatalogueAsync(DCDefectCatalogue dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          //check if there is already a entry with same code
          MVHDefectCatalogue defCat = await _defectCatalogueHandler.GetDefectCatalogueByCodeAsync(ctx, dc.DefectCatalogueCode);

          if (defCat != null)
          {
            //NotificationController.RegisterAlarm(Defs.EqGrpCodeNotUnique, "Error while creating new record in table MNTEquipmentGroups: short group code " + dc.EquipmentGroupCode + " is not unique. ", dc.EquipmentGroupCode);
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.DefectCodeNotUnique, "Code not unique", dc.DefectCatalogueCode);
          }

          ctx.MVHDefectCatalogues.Add(_defectCatalogueHandler.CreateNewDefectCatalogue(dc));

          NotificationController.Info("New defect catalogue: {0} creating...", dc.DefectCatalogueName);
          await ctx.SaveChangesAsync();
          NotificationController.RegisterAlarm(Defs.DefectCatalogCreate, "New entry with code/name " + dc.DefectCatalogueCode + "/" + dc.DefectCatalogueName + " has been created in Defect Catalogue", dc.DefectCatalogueCode);
          await ModuleController.HmiRefresh(HMIRefreshKeys.DefectCatalogue);
        }
      }
      catch (ModuleMessageException ex)
      {
        //NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw (ex);
      }
      catch (Exception e)
      {
        NotificationController.RegisterAlarm(Defs.DefectCatalogGeneralError, "Error while creating Defect Catalogue ", "Error while creating Defect Catalogue");
        NotificationController.Error("Error while creating Defect Catalogue " + e.Source);
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.DefectCatalogGeneralError, "Error while creating Defect Catalogue ", "Error while creating Defect Catalogue");
      }

      return result;
    }


    public virtual async Task<DataContractBase> UpdateDefectCatalogueAsync(DCDefectCatalogue dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          //check if there is already another entry with same code
          MVHDefectCatalogue defCat = await _defectCatalogueHandler.GetDefectCatalogueByCodeAsync(ctx, dc.DefectCatalogueCode, dc.Id);

          if (defCat != null)
          {
            //NotificationController.RegisterAlarm(Defs.EqGrpCodeNotUnique, "Error while creating new record in table MNTEquipmentGroups: short group code " + dc.EquipmentGroupCode + " is not unique. ", dc.EquipmentGroupCode);
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.DefectCodeNotUnique, "Code not unique", dc.DefectCatalogueCode);
          }

          MVHDefectCatalogue defectCatalogue = await _defectCatalogueHandler.GetDefectCatalogueByIdAsync(ctx, dc.Id);
            
          _defectCatalogueHandler.UpdateDefectCatalogue(dc, defectCatalogue);

          await ctx.SaveChangesAsync();
          NotificationController.RegisterAlarm(Defs.DefectCatalogueUpdate, "New record entry with code/name " + dc.DefectCatalogueCode + "/" + dc.DefectCatalogueName + " has been updated", dc.DefectCatalogueCode);

          await ModuleController.HmiRefresh(HMIRefreshKeys.DefectCatalogue);
        }
      }
      catch (ModuleMessageException ex)
      {
        throw (ex);
      }
      catch (Exception e)
      {
        NotificationController.RegisterAlarm(Defs.DefectCatalogGeneralError, "Error while updating Defect Catalogue ", "Error while updating Defect Catalogue ");
        NotificationController.Error("Error while updating Defect Catalogue " + e.Source);
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.DefectCatalogGeneralError, "Error while updating Defect Catalogue ", "Error while updating Defect Catalogue ");
      }

      return result;
    }

  
    public virtual async Task<DataContractBase> DeleteDefectCatalogueAsync(DCDefectCatalogue dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          MVHDefectCatalogue defectCatalogue = await _defectCatalogueHandler.GetDefectCatalogueByIdAsync(ctx, dc.Id, true);
					if (defectCatalogue != null)
          {
						if (defectCatalogue.MVHDefects.Count > 0)
            {
              throw new ModuleMessageException(ModuleController.ModuleName, Defs.DefectCatDeleteNotPossible, "Defect can not be deleted due to dependencies", defectCatalogue.DefectCatalogueCode);
            }
            ctx.MVHDefectCatalogues.Remove(defectCatalogue);
            await ctx.SaveChangesAsync();
						//throw new ModuleMessageException(ModuleController.ModuleName, Defs.DefectCatalogDelete, "Catalog entry has been deleted", dc.DefectCatalogueCode);
						NotificationController.RegisterAlarm(Defs.DefectCatalogDelete, "Defect catalogue entry " + dc.DefectCatalogueCode + "/" + dc.DefectCatalogueName + " has been deleted", defectCatalogue.DefectCatalogueCode);
						await ModuleController.HmiRefresh(HMIRefreshKeys.DefectCatalogue);
          }
					else
          {
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.DefectCatalogGeneralError, "Record not found", "Element not found in Defect Catalogue");
          }

        }
      }
      catch (Exception e)
      {
        NotificationController.RegisterAlarm(Defs.DefectCatalogGeneralError, "Error while deleting Defect Catalogue ", "Error while deleting Defect Catalogue element");
        NotificationController.Error("Error while deleting Defect Catalogue " + e.Source);
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.DefectCatalogGeneralError, "Error while deleting Defect Catalogue element ", "Error while deleting Defect Catalogue element");
      }

      return result;
    }
  }
}
