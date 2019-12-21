using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.Schedule;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.PPL.Handlers
{
  public sealed class ScheduleHandler
  {
    public long EstimateWorkOrderTime(double targetOrderWeight, double? stdProductivity)
    {
      long planedTimeInS = 0;
      if (stdProductivity != null)
      {
        planedTimeInS = (long)(targetOrderWeight / stdProductivity.Value);
      }
      return planedTimeInS;
    }

    public DateTime GetLatestPlannedEndTime(PEContext ctx)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      return ctx.PPLSchedules.OrderByDescending(x => x.PlannedEndTime).FirstOrDefault()?.PlannedEndTime ?? DateTime.Now;

    }

    public short GetLastOrderSeqNumber(PEContext ctx)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      return ctx.PPLSchedules.OrderByDescending(x => x.OrderSeq).FirstOrDefault()?.OrderSeq ?? 0;
    }

    public void AddItemToSchedule(PEContext ctx, PPLSchedule scheduleItem)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      ctx.PPLSchedules.Add(scheduleItem);
    }

    //public async Task UpdateScheduleItemStatusAsync(PEContext ctx, long scheduleId, OrderStatus status)
    //{
    //  if (ctx == null)
    //  {
    //    ctx = new PEContext();
    //  }

    //  PPLSchedule scheduleItem = await ctx.PPLSchedules.Where(x => x.ScheduleId == scheduleId).SingleOrDefaultAsync();
    //  scheduleItem.ScheduleStatus = status;
    //}

    public void UpdateOrderSeqAfterDelete(PEContext ctx, long orderSeq)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      ctx.PPLSchedules.Where(x => x.OrderSeq > orderSeq).ToList().ForEach(x => x.OrderSeq--);

    }

    public async Task<PPLSchedule> GetScheduleItemByIdAsync(PEContext ctx, long scheduleId, bool deepInclude = false)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      
      return deepInclude==false? 
                   await ctx.PPLSchedules.Where(x => x.ScheduleId == scheduleId).SingleAsync() :
                    await ctx.PPLSchedules
                            .Where(w => w.ScheduleId == scheduleId)
                            .Include(i => i.PRMWorkOrder)
                            .Include(i => i.PRMWorkOrder.PRMMaterials)
                            .Include(i => i.PRMWorkOrder.PRMMaterialCatalogue)
                            .Include(i => i.PRMWorkOrder.PRMProductCatalogue)
                            .Include(i => i.PRMWorkOrder.PRMCustomer)
                            .SingleAsync();
    }

    ///// <summary>
    ///// Function create Tes Schedule - called from HMI.
    ///// </summary>
    ///// <param name="workOrder"></param>
    ///// <param name="dc"></param>
    ///// <returns></returns>
    //public PPLSchedule CreateTestSchedule(PRMWorkOrder workOrder, DCTestSchedule dc)
    //{
    //  if(workOrder is null || dc is null)
    //  {
    //    throw new Exception() { Source = "CreateTestSchedule" };
    //  }

    //  return new PPLSchedule()
    //  {
    //    CreatedTs = DateTime.Now,
    //    LastUpdateTs = DateTime.Now,
    //    PlannedStartTime = DateTime.Now,
    //    PlannedEndTime = DateTime.Now,
    //    PRMWorkOrder = workOrder,
    //    OrderSeq = dc.OrderSeq.GetValueOrDefault(),
    //    PlannedTime = EstimateWorkOrderTime(workOrder.TargetOrderWeight, workOrder?.PRMProductCatalogue?.StdTPH),

    //  };
    //}

    public async Task<PPLSchedule> GetScheduleItemByOrderSeqNumAsync(PEContext ctx, short orderSeq, ScheduleMoveOperator direction = ScheduleMoveOperator.Non)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      return await ctx.PPLSchedules.Where(x => x.OrderSeq == orderSeq + (short)direction).SingleOrDefaultAsync();

    }

    public async Task<PPLSchedule> GetNextScheduleItemByOrderSeqNumAsync(PEContext ctx, short orderSeq, ScheduleMoveOperator direction = ScheduleMoveOperator.Non)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      
      if (direction==ScheduleMoveOperator.Down)
        return await ctx.PPLSchedules.Where(x => x.OrderSeq >= orderSeq + (short)direction).OrderBy(o=>o.OrderSeq).FirstOrDefaultAsync();
      else
        return await ctx.PPLSchedules.Where(x => x.OrderSeq <= orderSeq + (short)direction).OrderByDescending(o=>o.OrderSeq).FirstOrDefaultAsync();

    }

    public async Task DeleteItemByIdAsync(PEContext ctx, long scheduleItemId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      PPLSchedule item = await ctx.PPLSchedules.Where(x => x.ScheduleId == scheduleItemId).SingleAsync();
      ctx.PPLSchedules.Remove(item);
    }
    /// <summary>
    /// Return list of PPLSchedules Ids sorted by OrderSeq
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    public async Task<List<long>> GetScheduleOfWorkOrdersAsync(PEContext ctx)
    {
      if(ctx == null)
      {
        ctx = new PEContext();
      }

      return await ctx.PPLSchedules.OrderBy(o => o.OrderSeq).Select(s => s.FKWorkOrderId).ToListAsync();
    }
    public async Task<PRMMaterial> GetFirstUnasignedMaterialFromSchedule(PEContext ctx, long rawMaterialId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      PRMMaterial material = null;
      MVHRawMaterial mvhMaterial = null;
      mvhMaterial = await ctx.MVHRawMaterials.Where(z => z.RawMaterialId == rawMaterialId).Include(z=>z.PRMMaterial).SingleOrDefaultAsync();
      if (mvhMaterial != null)
      {
        material = mvhMaterial.PRMMaterial;
        if (material != null)
          return material;
      }
      //TODO - if some L3 material is assigned to raw material (rawMaterialId) - return that one
      material =  await ctx.PPLSchedules.OrderBy(x => x.OrderSeq).Join(ctx.PRMMaterials, a => a.FKWorkOrderId, b => b.FKWorkOrderId, (a, b) => b).Where(b => b.IsAssigned == false).FirstOrDefaultAsync();

      return material;
    }

    public async Task RemoveFinishedWorkOrdersFromSchedule(PEContext ctx)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      bool allMaterialsRolled = false;
      List<long> workOrdersToRemove = new List<long>();
      List<PRMMaterial> pRMMaterials = new List<PRMMaterial>(); ;
      List<PPLSchedule> pPLSchedules = ctx.PPLSchedules.ToList();
      int i = 0;
      if(pPLSchedules != null)
      {
				foreach(PPLSchedule element in pPLSchedules)
        {
          //Check all raw materials from work orders
          pRMMaterials = await ctx.PRMMaterials.Include(z => z.MVHRawMaterials).Where(z => z.FKWorkOrderId == element.FKWorkOrderId).ToListAsync();
					if(pRMMaterials != null)
          {
            i = 0;
						foreach(PRMMaterial pRMMaterialelement in pRMMaterials)
            {
              if (pRMMaterialelement.MVHRawMaterials.Any())
              {
                i++;
                if (pRMMaterialelement.MVHRawMaterials.SingleOrDefault().Status >= RawMaterialStatus.ENUM_Rolled)
                  allMaterialsRolled = true;
                else
                {
                  allMaterialsRolled = false;
                  break;
                }

                if (allMaterialsRolled && i == pRMMaterials.Count)
                  if (pRMMaterialelement.FKWorkOrderId > 0)
                    workOrdersToRemove.Add((long)pRMMaterialelement.FKWorkOrderId);
              }
            }
          }
        }
      }

      foreach (long element in workOrdersToRemove)
      {
        PPLSchedule pPLSchedule = await ctx.PPLSchedules.Where(z => z.FKWorkOrderId == element).SingleOrDefaultAsync();
				if(pPLSchedule != null)
					ctx.PPLSchedules.Remove(pPLSchedule);
      }
    }
  }
}
