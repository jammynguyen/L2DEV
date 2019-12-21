using PE.DbEntity.Models;
using PE.DTO.Internal.Quality;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.QTY.Handlers
{
  public sealed class DefectCatalogueHandler
  {
    public void UpdateDefectCatalogue(DCDefectCatalogue dc, MVHDefectCatalogue defectCatalogue)
    {

      try
      {
        if (dc != null)
        {

          defectCatalogue.DefectCatalogueName = dc.DefectCatalogueName;
					defectCatalogue.DefectCatalogueCode = dc.DefectCatalogueCode;
          defectCatalogue.DefectCatalogueDescription = dc.DefectCatalogueDescription;
          //defectCatalogue.IsDefault = dc.IsDefault;
          defectCatalogue.FKDefectCatalogueCategoryId = dc.FkDelayCatalogueCategoryId;

        }
        else
        {
          throw new Exception() { Source = "UpdateDefectCatalogue - DC not found" };
        }
      }
      catch (Exception)
      {
        throw new Exception() { Source = "UpdateDefectCatalogue - Error during saving" };
      }
    }
    public MVHDefectCatalogue CreateNewDefectCatalogue(DCDefectCatalogue dc)
    {
      MVHDefectCatalogue defectCatalogue;
      try
      {
        if (dc != null)
        {
          defectCatalogue = new MVHDefectCatalogue();
          defectCatalogue.DefectCatalogueName = dc.DefectCatalogueName;
          defectCatalogue.DefectCatalogueCode = dc.DefectCatalogueCode;
          defectCatalogue.DefectCatalogueDescription = dc.DefectCatalogueDescription;
          defectCatalogue.IsDefault = dc.IsDefault;
          defectCatalogue.FKDefectCatalogueCategoryId = dc.FkDelayCatalogueCategoryId;
          defectCatalogue.CreatedTs = DateTime.Now;
          defectCatalogue.LastUpdateTs = DateTime.Now;
          //defectCatalogue.MVHDefectCatalogueCategory = null;
          defectCatalogue.IsActive = true;
          defectCatalogue.IsDefault = false;
          
        }
        else
        {
          throw new Exception() { Source = "AddDefectCatalogue - DC not found" };
        }
      }
      catch (Exception)
      {
        throw new Exception() { Source = "AddDefectCatalogue - Error during creation" };
      }
      return defectCatalogue;
    }

    public async Task<MVHDefectCatalogue> GetDefectCatalogueByIdAsync(PEContext ctx, long defectCatalogueId, bool includeDefects = false )
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      if (!includeDefects)
      {
       return await ctx.MVHDefectCatalogues.FindAsync(defectCatalogueId);
      }
      else
      {
        return await ctx.MVHDefectCatalogues.Include(i => i.MVHDefects).Where(x=>x.DefectCatalogueId == defectCatalogueId).SingleOrDefaultAsync();
      }
    }


		/// <summary>
    /// Returns default  catalogue entry with given code or null
    /// Optionally if id is supplied looks for entry with given code and other than supplied id
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="code"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<MVHDefectCatalogue> GetDefectCatalogueByCodeAsync(PEContext ctx, string code, long? id = null)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      if (id == null)
      {
        return await ctx.MVHDefectCatalogues.Where(x => x.DefectCatalogueCode.ToLower().Equals(code.ToLower())).SingleOrDefaultAsync();
      }
      else //search with exclused id
      {
        return await ctx.MVHDefectCatalogues.Where(x => x.DefectCatalogueCode.ToLower().Equals(code.ToLower()) && x.DefectCatalogueId != id).SingleOrDefaultAsync();
      }
    }
  }
}
