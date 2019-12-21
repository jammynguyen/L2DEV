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
  public sealed class RollGroovesHistoryHandler
  {
    public List<RLSRollGroovesHistory> GetRollGrooveHistory(PEContext ctx, long rollSetHistoryId, long rollId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      List<RLSRollGroovesHistory> result = ctx.RLSRollGroovesHistories.Where(f => f.FKRollSetHistoryId == rollSetHistoryId && f.FKRollId == rollId).ToList();

      return result;
    }

    public List<RLSRollGroovesHistory> GetRollGrooveHistoryFromRollSetHistoryId(PEContext ctx, long rollSetHistoryId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      List<RLSRollGroovesHistory> result = ctx.RLSRollGroovesHistories.Where(f => f.FKRollSetHistoryId == rollSetHistoryId).ToList();

      return result;
    }

    public void InactiveAllGrooves(PEContext ctx, ref List<RLSRollGroovesHistory> rollGrooVeHistoryList)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      foreach (RLSRollGroovesHistory item in rollGrooVeHistoryList)
      {
        if (item.Status == RollGrooveStatus.Active)
        {
          item.Status = RollGrooveStatus.Actual;
        }
      }
    }

    public RLSRollGroovesHistory CreateRollGrooveHistory(PEContext ctx, DCRollGroovesHistoryData dc)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSRollGroovesHistory rollGroovesHistory = new RLSRollGroovesHistory();
      rollGroovesHistory.Created = DateTime.Now;
      rollGroovesHistory.CreatedTs = DateTime.Now;
      rollGroovesHistory.LastUpdateTs = DateTime.Now;
      rollGroovesHistory.FKRollSetHistoryId = dc.RollSetHistoryId;
      rollGroovesHistory.GrooveNumber = dc.GrooveNumber;
      rollGroovesHistory.FKGrooveTemplateId = dc.GrooveTemplateId;
      rollGroovesHistory.FKRollId = dc.RollId;
      rollGroovesHistory.Status = (RollGrooveStatus)dc.Status;
      rollGroovesHistory.AccBilletCnt = 0;
      rollGroovesHistory.AccWeight = 0.0;
      rollGroovesHistory.Created = DateTime.Now;
      rollGroovesHistory.ActDiameter = dc.ActDiameter;

      return rollGroovesHistory;
    }

    public RLSRollGroovesHistory GetRollGrooveHistoryForStatusUpdate(PEContext ctx, long rollSetHistoryId, long rollId, long grooveNumber)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSRollGroovesHistory result = ctx.RLSRollGroovesHistories.Where(f => f.FKRollSetHistoryId == rollSetHistoryId && f.FKRollId == rollId && f.GrooveNumber==grooveNumber && f.Deactivated==null).FirstOrDefault();

      return result;
    }

    public void UpdateRollGrooveHistoryStatus(PEContext ctx, ref RLSRollGroovesHistory rollGrooveHistory, RollGrooveStatus newStatus)
    {
      rollGrooveHistory.Status = newStatus;
    }

    public List<RLSRollGroovesHistory> GetActiveRollGrooveHistory(PEContext ctx)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      List<long> rollGroovesHistoryIds = ctx.V_GroovesView4Accumulation.Where(x => x.GrooveStatus == (short)RollGrooveStatus.Active).Select(s => s.RollGroovesHistoryId).ToList();

      List<RLSRollGroovesHistory> result = ctx.RLSRollGroovesHistories.Where(f => rollGroovesHistoryIds.Contains(f.RollGroovesHistoryId)).ToList();

      return result;
    }

    public async Task UpdateActiveRollGrooveHistoryWithProductWeight(PEContext ctx, double materialWeight)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      List<long> rollGroovesHistoryIds = ctx.V_GroovesView4Accumulation.Where(x => x.GrooveStatus == (short)RollGrooveStatus.Active).Select(s => s.RollGroovesHistoryId).ToList();

      ctx.RLSRollGroovesHistories.Where(f => rollGroovesHistoryIds.Contains(f.RollGroovesHistoryId)).ToList().ForEach(a => { a.AccWeight += materialWeight; a.AccBilletCnt += 1; });

      return;
    }

  }
}
