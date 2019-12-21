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
using SMF.Module.Notification;

namespace PE.DLS.Handlers
{
  public sealed class DelayHandler
  {
    public DLSDelay UpdateDelay(PEContext ctx, DLSDelay delay, DCDelay dc)
    {
      if (ctx == null) ctx = new PEContext();

      if (delay != null)
      {
        delay.DelayStart = dc.DelayStart;
        delay.DelayEnd = dc.DelayEnd;
        delay.FKDelayCatalogueId = dc.FkDelayCatalogue;
        delay.IsPlanned = dc.IsPlanned;
        delay.UserComment = dc.Comment;
      }
      else
      {
        throw new Exception("Delay cannot be updated - does not exists");
      }
      return delay;
    }

    public DLSDelay GetDelayById(PEContext ctx, long delayId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      DLSDelay delay = ctx.DLSDelays.Where(x => x.DelayId == delayId).Single();

      return delay;
    }

    public async Task<long> GetDefaultDefectCatalogueId(PEContext ctx)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      long delayCatalogueId =  (long) await ctx.DLSDelayCatalogues.Where(x => x.IsDefault == true).Select(s=>s.FKDelayCatalogueCategoryId).SingleAsync();

      return delayCatalogueId;
    }

    public async Task<DLSDelay> GetLastDelay(PEContext ctx)
    {
      DLSDelay lastDelay;
      if (ctx == null)
      {
        ctx = new PEContext();
      }
            
      lastDelay = await ctx.DLSDelays.OrderByDescending(x => x.DelayStart).FirstAsync();      
      
      return lastDelay;
    }

  }
}
