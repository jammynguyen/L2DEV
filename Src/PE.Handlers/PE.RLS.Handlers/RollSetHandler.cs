using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.RollShop;

namespace PE.RLS.Handlers
{
  public sealed class RollSetHandler
  {
    public RLSRollSet GetRollSetFromName(PEContext ctx, string rollSetName)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSRollSet rollSet = ctx.RLSRollSets.Where(x => x.RollSetName == rollSetName).FirstOrDefault();
      return rollSet;
    }

    public RLSRollSet GetRollSetFromId(PEContext ctx, long? rollSetId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSRollSet rollSet = ctx.RLSRollSets.Where(x => x.RollSetId == rollSetId).FirstOrDefault();
      return rollSet;
    }

    public void UpdateRollSet(PEContext ctx, ref RLSRollSet rollSet, RLSRollSet dc)
    {
      rollSet.LastUpdateTs = DateTime.Now;
      rollSet.RollSetDescription = dc.RollSetDescription;
      rollSet.FKUpperRollId = dc.FKUpperRollId;
      rollSet.FKBottomRollId = dc.FKBottomRollId;
      rollSet.RollSetName = dc.RollSetName;
      rollSet.RollSetType = dc.RollSetType;
      rollSet.Status = dc.Status;
    }

    public void CreateRollSet(PEContext ctx, ref RLSRollSet rollSet, DCRollSetData dc)
    {
      rollSet = new RLSRollSet();
      rollSet.Created = DateTime.Now;
      rollSet.CreatedTs = DateTime.Now;
      rollSet.LastUpdateTs = DateTime.Now;
      rollSet.RollSetDescription = dc.Description;
      rollSet.RollSetName = dc.RollSetName;
      rollSet.Status = (RollSetStatus)dc.RollSetStatus;
      rollSet.RollSetType = dc.RollSetType;
    }

    public List<V_RollsetOverviewNewest> GetRollSetFromCassette(PEContext ctx, long? cassetteId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      List<V_RollsetOverviewNewest> rollSetList = ctx.V_RollsetOverviewNewest.Where(x => x.CassetteId == cassetteId).ToList();
      return rollSetList;
    }


  }
}
