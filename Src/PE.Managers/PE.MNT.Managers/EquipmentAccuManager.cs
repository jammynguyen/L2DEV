using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.Interfaces.Managers;
using PE.DTO.Internal.Maintenance;
using SMF.Core.DC;
using PE.MNT.Handlers;
using PE.DbEntity.Models;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using SMF.Module.Core;
using PE.Common;

namespace PE.MNT.Managers
{
    public class EquipmentAccuManager : IEquipmentAccuManager
    {
		#region members

		#endregion
		#region handlers

		private readonly EquipmentAccuHandler eqAccuHandler;

		#endregion

		public EquipmentAccuManager()
    {
      //no send office
      eqAccuHandler = new EquipmentAccuHandler();
    }

    public virtual async Task<DataContractBase> AccumulateEquipmentUsageAsync(DCEquipmentAccu message)
    {
      DataContractBase result = new DataContractBase();

			try
			{
				using (PEContext ctx = new PEContext())
				{
          await eqAccuHandler.AccumulateEquipmentUsage(ctx, message.MaterialWeight);
          await ctx.SaveChangesAsync();
          NotificationController.Info("All equipment elements being In Operation are updated with processed weight of {0} kg", message.MaterialWeight);
          await ModuleController.HmiRefresh(HMIRefreshKeys.Equipment);
        }
      }
			catch
			{
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
      }

			return result;
    }
  }
}
