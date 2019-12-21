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
  public sealed class QualityAssignmentHandler
  {
    public async Task<PRMProduct> GetProductById(PEContext ctx, long id)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      return await ctx.PRMProducts.Where(x => x.ProductId==id).SingleAsync();
    }

    public async Task RemoveCurrentlyAssignedDefectsToProduct(PEContext ctx, long productid)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      List<MVHDefect> oldDefectList = ctx.MVHDefects.Where(x => x.FKProductId==productid).ToList();
      if (oldDefectList != null)
      {
        foreach (MVHDefect defect in oldDefectList)
        {
          ctx.MVHDefects.Remove(defect);
        }
      }
      return;
    }


    public MVHDefect CreateDefect(PEContext ctx, long productId, long defectId, string remark)
    {
      MVHDefect defect = new MVHDefect
      {
        CreatedTs = DateTime.Now,
        FKProductId = productId,
        FKDefectCatalogueId = defectId,
        DefectDescription = remark
      };
      return defect;
    }
  }
}
