using PE.DbEntity.Models;
using PE.DTO.Internal.Delay;
using PE.DTO.Internal.ProdManager;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PE.DLS.Handlers
{
  public sealed class DelayCatalogueHandler
  {
    public DLSDelayCatalogue CreateNewDelayCatalogue(PEContext ctx, DCDelayCatalogue dc)
    {
      if (ctx == null) ctx = new PEContext();

      DLSDelayCatalogue delayCatalogue = new DLSDelayCatalogue();
      if (delayCatalogue != null)
      {
        delayCatalogue.DelayCatalogueName = dc.DelayName;
        delayCatalogue.CreatedTs = DateTime.Now;
        delayCatalogue.LastUpdateTs = DateTime.Now;
        delayCatalogue.StdDelayTime = dc.StdDelayTime.GetValueOrDefault();
        delayCatalogue.DelayCatalogueDescription = dc.DelayDescription;
        delayCatalogue.DelayCatalogueCode = dc.DelayCode;
        delayCatalogue.FKDelayCatalogueCategoryId = (long)dc.FKDelayCategoryId;
        delayCatalogue.ParentDelayCatalogueId = dc.ParentDelayCatalogueId;
      }
      else
      {
        throw new Exception("Catalogue cannot be updated - does not exists");
      }

      return delayCatalogue;
    }
    public DLSDelayCatalogue UpdateDelayCatalogue(PEContext ctx, DLSDelayCatalogue delayCatalogue, DCDelayCatalogue dc)
    {
      if (ctx == null) ctx = new PEContext();
      
      if (delayCatalogue != null)
      {

        delayCatalogue.DelayCatalogueName = dc.DelayName;
        delayCatalogue.StdDelayTime = dc.StdDelayTime.GetValueOrDefault();
        delayCatalogue.DelayCatalogueDescription = dc.DelayDescription;
        delayCatalogue.DelayCatalogueCode = dc.DelayCode;
        delayCatalogue.FKDelayCatalogueCategoryId = (long)dc.FKDelayCategoryId;
        delayCatalogue.ParentDelayCatalogueId = dc.ParentDelayCatalogueId;
      }
      else
      {
        throw new Exception("Catalogue cannot be updated - does not exists");
      }

      return delayCatalogue;
    }

    public DLSDelayCatalogue GetDelayCatalogueById(PEContext ctx, long delayCatalogueId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      DLSDelayCatalogue delayCat = ctx.DLSDelayCatalogues.Where(x => x.DelayCatalogueId == delayCatalogueId).Include(i => i.DLSDelayCatalogueCategory).Single();

      return delayCat;
    }
  }
}
