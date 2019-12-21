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
  public sealed class RollSetHistoryHandler
  {
    public RLSRollSetHistory GetRollSetHistoryFromId(PEContext ctx, long? rollSetHistoryId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSRollSetHistory rollSetHistory = ctx.RLSRollSetHistories.Where(x => x.RollSetHistoryId == rollSetHistoryId).FirstOrDefault();
      return rollSetHistory;
    }

    public RLSRollSetHistory GetRollSetHistoryFromRollSetId(PEContext ctx, long? rollSetId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSRollSetHistory rollSetHistory = ctx.RLSRollSetHistories.Where(x => x.FKRollSetId == rollSetId).First();
      return rollSetHistory;
    }

    public List<RLSRollSetHistory> GetRollSetHistoryRecordList(PEContext ctx, long rollSetId, RollSetHistoryStatus status)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      List<RLSRollSetHistory> RollSetHistory = ctx.RLSRollSetHistories.Where(z => z.FKRollSetId == rollSetId && z.Status == status).ToList();
      return RollSetHistory ?? null;

    }

    public List<RLSRollSetHistory> GetRollSetHistoryRecordListFromRollSetId(PEContext ctx, long rollSetId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      List<RLSRollSetHistory> RollSetHistory = ctx.RLSRollSetHistories.Where(z => z.FKRollSetId == rollSetId).ToList();
      return RollSetHistory ?? null;

    }



    public RLSRollSetHistory GetRollSetHistory(PEContext ctx, long rollSetId, RollSetHistoryStatus status)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      RLSRollSetHistory RollSetHistory = ctx.RLSRollSetHistories.Where(z => z.FKRollSetId == rollSetId && z.Status == status).FirstOrDefault();

      return RollSetHistory ?? null;
    }

    public List<RLSRollSetHistory> GetRollSetHistoryList(PEContext ctx, long rollSetId, RollSetHistoryStatus status)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      List<RLSRollSetHistory> RollSetHistory = ctx.RLSRollSetHistories.Where(z => z.FKRollSetId == rollSetId && z.Status == status).ToList();

      return RollSetHistory ?? null;
    }


    public List<V_RollsetInCassettes> GetInstalledRollsetInCassette(PEContext ctx, long cassetteId, short positionInCassette, RollSetHistoryStatus rshStatus = RollSetHistoryStatus.Actual)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      List<V_RollsetInCassettes> list = ctx.V_RollsetInCassettes.Where(z => z.FKCassetteId == cassetteId && z.PositionInCassette == positionInCassette && z.RollSetHistoryStatus == (short)rshStatus).ToList();

      return list;
    }

    public RLSRollSetHistory GetPlannedOrActualRollSetHistory(PEContext ctx, long rollSetId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSRollSetHistory RollSetHistory = ctx.RLSRollSetHistories.Where(z => z.FKRollSetId == rollSetId && (z.Status == RollSetHistoryStatus.Actual || z.Status == RollSetHistoryStatus.Planned)).FirstOrDefault();

      return RollSetHistory ?? null;
    }

    public void UpdateRollSetHistory(PEContext ctx, ref RLSRollSetHistory rollSetHistory, DCRollSetHistoryData dc)
    {
      //rollSetHistory.CreatedTs = DateTime.Now;
      //rollSetHistory.Created = DateTime.Now;
      //rollSetHistory.LastUpdateTs = DateTime.Now;
      //rollSetHistory.FKRollSetId = (long)dc.RollSetId;
      //rollSetHistory.Dismounted = dc.Dismounted;
      //rollSetHistory.Mounted = dc.Mounted;
      //rollSetHistory.PositionInCassette = dc.PositionInCassette;
      //rollSetHistory.Status = RollSetHistoryStatus.Actual;
      //rollSetHistory.FKCassetteId = dc.CassetteId;

      rollSetHistory.CreatedTs = DateTime.Now;
      rollSetHistory.Created = DateTime.Now;
      rollSetHistory.LastUpdateTs = DateTime.Now;
      
      //if (dc.CassetteId != null) rollSetHistory.FKCassetteId = dc.CassetteId;
      rollSetHistory.FKCassetteId = dc.CassetteId;
      //rollSetHistory.LastUpdateTs = DateTime.Now;
      rollSetHistory.FKRollSetId = (long)dc.RollSetId;
      if (dc.InterCassetteId != null) rollSetHistory.FKRollSetId = (long)dc.RollSetId;
      if (dc.Dismounted != null) rollSetHistory.Dismounted = dc.Dismounted;
      if (dc.Mounted != null) rollSetHistory.Mounted = dc.Mounted;
      if (dc.PositionInCassette != null) rollSetHistory.PositionInCassette = dc.PositionInCassette;
      if (dc.Status != null) rollSetHistory.Status = dc.Status;
      if (dc.AccWeightLimit != null) rollSetHistory.AccWeightLimit = dc.AccWeightLimit ?? 0.0;

    }

    public void CreateRollSetHistory(PEContext ctx, ref RLSRollSetHistory rollSetHistory, DCRollSetHistoryData dc)
    {
      rollSetHistory.CreatedTs = DateTime.Now;
      rollSetHistory.Created = DateTime.Now;
      rollSetHistory.LastUpdateTs = DateTime.Now;
      rollSetHistory.FKRollSetId = (long)dc.RollSetId;
      rollSetHistory.Dismounted = dc.Dismounted;
      rollSetHistory.Mounted = dc.Mounted;
      rollSetHistory.PositionInCassette = dc.PositionInCassette;
      rollSetHistory.Status = RollSetHistoryStatus.Actual;
      rollSetHistory.FKCassetteId = dc.CassetteId;
    }


    //public RLSRollSetHistory CreateRollSetHistory(PEContext ctx, DCRollSetHistoryData dc)
    //{
    //  if (ctx == null)
    //  {
    //    ctx = new PEContext();
    //  }

    //}
  }
}
