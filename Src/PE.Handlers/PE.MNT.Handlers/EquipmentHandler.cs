using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using System.Data.Entity;
using PE.DTO.Internal.Maintenance;
using SMF.Core.DC;

namespace PE.MNT.Handlers
{
    public class EquipmentHandler
    {
    /// <summary>
    /// Returns Equipment record or null if not found
    /// </summary>
    /// <param name="ctx">DB context for EF</param>
    /// <param name="id">Equipment group ID (PK)</param>
    /// <returns></returns>
    public async Task<MNTEquipment> GetEquipmentById(PEContext ctx, long id)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      return await ctx.MNTEquipments.Where(x => x.EquipmentId.Equals(id)).SingleAsync();
    }

    /// <summary>
    /// Created new equipment and returns it.
    /// </summary>
    /// <param name="ctx">DB context</param>
    /// <param name="item">Equipment element</param>
    /// <returns></returns>
    public async Task CreateEquipmentAsync(PEContext ctx, MNTEquipment item)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      ctx.MNTEquipments.Add(item);
    }

    /// <summary>
    /// Deletes equipment and all its history
    /// </summary>
    /// <param name="ctx">DB context</param>
    /// <param name="item">MNTEquipment object representing record to be deleted from DB</param>
    /// <returns></returns>
    public async Task DeleteEquipmentAsync(PEContext ctx, MNTEquipment item)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      ctx.MNTEquipments.Remove(item);  //deletes all history entries from MNTEquipmentHistory because of on delete cascade rule
    }


		/// <summary>
		/// Creates EquipmentHistoryRecord based on given Equipment element
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="item">MNTEquipment object representing actual equipment element</param>
		/// <returns></returns>
		public async Task CreateEquipmentHistoryRecordAsync(PEContext ctx, MNTEquipment item, string remark)
		{
			if (ctx == null)
			{
				ctx = new PEContext();
			}
			MNTEquipmentHistory hist = new MNTEquipmentHistory();
			hist.FKEquipmentId = item.EquipmentId;
			hist.EquipmentStatus = item.EquipmentStatus;
			hist.MaterialsProcessed = item.CntMatsProcessed;
			hist.WeightProcessed = item.ActualValue;
			hist.CreatedTs = DateTime.Now;
			hist.LastUpdateTs = DateTime.Now;
			hist.Remark = remark;
			ctx.MNTEquipmentHistories.Add(hist);
		}


	}
}
