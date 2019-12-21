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
    public class EquipmentAccuHandler
	{
		/// <summary>
		/// Accumulates usage of all equipment being in state of In Operation at a time of call
		/// </summary>
		/// <param name="ctx">DB context for EF</param>
		/// <param name="materialWeight">Weight of processed material [kg]</param>
		/// <returns></returns>
		public async Task AccumulateEquipmentUsage(PEContext ctx, long materialWeight)
		{
			if (ctx == null)
			{
				ctx = new PEContext();
			}
			ctx.MNTEquipments.Where(w => w.EquipmentStatus == DbEntity.Enums.EquipmentStatus.InOperation).ToList().ForEach(a => { a.ActualValue += materialWeight; a.CntMatsProcessed += 1; });
		}
  }
}
