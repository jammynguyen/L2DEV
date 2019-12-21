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
  public class QualityAssignmentManager : IQualityAssignment
  {
    #region members

    #endregion
    #region handlers

    private readonly QualityAssignmentHandler _qualityHandler;

    #endregion
    #region ctor
    public QualityAssignmentManager()
    {
      _qualityHandler = new QualityAssignmentHandler();
    }
    #endregion

    public virtual async Task<DataContractBase> AssignQualityAsync(DCQualityAssignment message)
    {
      DataContractBase result = new DataContractBase();

      if (message.ProductIds.Count == 0)
        return result;

      try
      {
        using (PEContext ctx = new PEContext())
        {
         
          foreach (long productId in message.ProductIds)
          {
            await _qualityHandler.RemoveCurrentlyAssignedDefectsToProduct(ctx, productId);

            PRMProduct product = await _qualityHandler.GetProductById(ctx, productId);
            product.QualityEnum = message.QualityFlag;
            if (message.DefectCatalogueElementIds != null)
            {
              foreach (long defectId in message.DefectCatalogueElementIds)
              {
                MVHDefect defect = _qualityHandler.CreateDefect(ctx, productId, defectId, message.Remark);
                ctx.MVHDefects.Add(defect);
              }
            }

            await ctx.SaveChangesAsync();
            await ModuleController.HmiRefresh(HMIRefreshKeys.ProductQualityMgm);
          }


          //MVHDefectCatalogue defectCatalogue = await _defectCatalogueHandler.GetDefectCatalogueByIdAsync(ctx, dc.Id);

          //_defectCatalogueHandler.UpdateDefectCatalogue(dc, defectCatalogue);

          //await ctx.SaveChangesAsync();

          //await ModuleController.HmiRefresh(HMIRefreshKeys.DefectCatalogue);
        }
      }
      catch (Exception e)
      {
       NotificationController.RegisterAlarm(Defs.DefectAssignmentFailed, "Error while assigning defects");
       NotificationController.Error("Error while assigning defects " + e.Source);
       throw new ModuleMessageException(ModuleController.ModuleName, Defs.DefectAssignmentFailed, "Error while assigning defects");
      }

      return result;
    }
  }
}
