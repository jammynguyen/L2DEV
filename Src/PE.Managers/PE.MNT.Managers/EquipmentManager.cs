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
    public class EquipmentManager : IEquipmentManager
    {
		#region members

		#endregion
		#region handlers

		private readonly EquipmentHandler eqHandler;

		#endregion

		public EquipmentManager ()
    {
      //no send office
      eqHandler = new EquipmentHandler();

		}

    public virtual async Task<DataContractBase> CreateEquipmentAsync(DCEquipment message)
    {
      DataContractBase result = new DataContractBase();

			try
			{
				using (PEContext ctx = new PEContext())
				{
          MNTEquipment eq = new MNTEquipment();
					eq.CreatedTs = DateTime.Now;
          eq.LastUpdateTs = DateTime.Now;
          eq.AccumulationType = 0; //dummy
          eq.ActualValue = 0;
          eq.AlarmValue = (long?)message.AlarmValue;
          eq.CntMatsProcessed = 0;
          eq.EquipmentCode = message.EquipmentCode;
          eq.EquipmentName = message.EquipmentName;
          eq.EquipmentStatus = DbEntity.Enums.EquipmentStatus.InStock;
          eq.EquipmentDescription = message.EquipmentDescription;
          eq.FKEquipmentGroupId = message.EquipmentGroupId;
          eq.IsActive = true;
          eq.ServiceExpires = message.ServiceExpires;
          eq.WarningValue = (long?)message.WarningValue;

          await eqHandler.CreateEquipmentAsync(ctx, eq);
          await ctx.SaveChangesAsync();
          NotificationController.RegisterAlarm(Defs.EqElementCreated, "New record in table MNTEquipments: name " + eq.EquipmentName + ", id " + eq.EquipmentId, eq.EquipmentName);
          await ModuleController.HmiRefresh(HMIRefreshKeys.Equipment);
        }
			}
      catch (ModuleMessageException ex)
      {
        throw (ex);
      }
      catch
			{
        NotificationController.RegisterAlarm(Defs.EqElementGeneralError, "Error occured while creating new equipment element", "Error occured while creating new equipment element");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqElementGeneralError, "Error occured while creating equipment element ", "Error occured while creating equipment element");
      }

			return result;
    }

    public virtual async Task<DataContractBase> DeleteEquipmentAsync(DCEquipment message)
    {

      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          MNTEquipment eq = await eqHandler.GetEquipmentById(ctx, message.EquipmentId);
          if (eq != null)
          {
            await eqHandler.DeleteEquipmentAsync(ctx, eq);
            await ctx.SaveChangesAsync();
            NotificationController.RegisterAlarm(Defs.EqElementDeleted, "Equipments element (and its history)" + eq.EquipmentName + " has been deleted", eq.EquipmentName);
            await ModuleController.HmiRefresh(HMIRefreshKeys.Equipment);
          }
					else
          {
            NotificationController.Error("Equipment with Id {0} not found. Delete operation failed.", message.EquipmentId);
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqElementGeneralError, "Error occured while deleting equipment element", "Error occured while deleting equipment element");
          }
        }
      }
      catch (ModuleMessageException ex)
      {
        throw (ex);
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.EqElementGeneralError, "Error occured while deleting equipment element", "Error occured while deleting equipment element");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqElementGeneralError, "Error occured while deleting equipment element ", "Error occured while deleting equipment element");
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateEquipmentAsync(DCEquipment message)
    {
      DataContractBase result = new DataContractBase();

			try
			{
				using (PEContext ctx = new PEContext())
				{
          MNTEquipment eqElement = await eqHandler.GetEquipmentById(ctx, message.EquipmentId);
          if (eqElement == null)
          {
            NotificationController.Error("Equipment with Id {0} not found. Update operation failed.", message.EquipmentId);
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqElementGeneralError, "Error occured while updating equipment element", "Error occured while updating equipment element");
          }
					//compare if status is different, if true: create new eqipment history record with actual data before factual update
					if (eqElement.EquipmentStatus != message.EqStatus)
          {
            await eqHandler.CreateEquipmentHistoryRecordAsync(ctx, eqElement, message.Remark);
          }
          //reset counters if new status is In Operation and previous was not In Operatio = equipment is put online
          bool statusChanged = false;
          DbEntity.Enums.EquipmentStatus prevStatus = eqElement.EquipmentStatus;
          if (eqElement.EquipmentStatus != DbEntity.Enums.EquipmentStatus.InOperation && message.EqStatus == DbEntity.Enums.EquipmentStatus.InOperation)
          {
            eqElement.CntMatsProcessed = 0;
            eqElement.ActualValue = 0;
            statusChanged = true;
          }
          eqElement.EquipmentCode = message.EquipmentCode;
          eqElement.EquipmentName = message.EquipmentName;
          eqElement.EquipmentDescription = message.EquipmentDescription;
          eqElement.AlarmValue = (long?)message.AlarmValue;
          eqElement.WarningValue = (long?)message.WarningValue;
          eqElement.ServiceExpires = message.ServiceExpires;
          eqElement.FKEquipmentGroupId = message.EquipmentGroupId;
          eqElement.EquipmentStatus = message.EqStatus ?? eqElement.EquipmentStatus;
          eqElement.LastUpdateTs = DateTime.Now;
          await ctx.SaveChangesAsync();
					if (statusChanged)
          {
            NotificationController.RegisterAlarm(Defs.EqElementChangedStatus, String.Format("Equipment element {0} changed status from {1} to {2}", eqElement.EquipmentName, eqElement.EquipmentStatus, prevStatus), eqElement.EquipmentName, eqElement.EquipmentStatus);
          }
					else
          {
            NotificationController.RegisterAlarm(Defs.EqElementUpdated, String.Format("Equipment element {0} has been updated", eqElement.EquipmentName), eqElement.EquipmentStatus);
          }
          
          await ModuleController.HmiRefresh(HMIRefreshKeys.Equipment);
        }
			}
      catch (ModuleMessageException ex)
      {
        throw (ex);
      }
      catch
			{
        NotificationController.RegisterAlarm(Defs.EqElementGeneralError, "Error occured while updating equipment element", "Error occured while updating equipment element");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqElementGeneralError, "Error occured while updating equipment element", "Error occured while updating equipment element");

      }

			return result;
    }

    public virtual async Task<DataContractBase> CloneEquipmentAsync(DCEquipment message)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          MNTEquipment eqOriginal = await eqHandler.GetEquipmentById(ctx, message.EquipmentId);
          if (eqOriginal == null)
          {
            NotificationController.Error("Equipment with Id {0} not found. Update operation failed.", message.EquipmentId);
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqElementGeneralError, "Error occured while updating equipment element", "Error occured while updating equipment element");
          }
          MNTEquipment eq = new MNTEquipment();
          eq.CreatedTs = DateTime.Now;
          eq.LastUpdateTs = DateTime.Now;
          eq.AccumulationType = 0; //dummy
          eq.ActualValue = 0;
          eq.AlarmValue = (long?)message.AlarmValue;
          eq.CntMatsProcessed = 0;
          eq.EquipmentCode = message.EquipmentCode;
          eq.EquipmentName = message.EquipmentName;
          eq.EquipmentStatus = DbEntity.Enums.EquipmentStatus.InStock;
          eq.EquipmentDescription = message.EquipmentDescription;
          eq.FKEquipmentGroupId = eqOriginal.FKEquipmentGroupId;
          eq.WarningValue = (long?)message.WarningValue;
          eq.IsActive = true;
          //eq.ServiceExpires = message.ServiceExpires;

          await eqHandler.CreateEquipmentAsync(ctx, eq);
          await ctx.SaveChangesAsync();
          NotificationController.RegisterAlarm(Defs.EqElementCreated, "New record in table MNTEquipments: name " + eq.EquipmentName + ", id " + eq.EquipmentId, eq.EquipmentName);
          await ModuleController.HmiRefresh(HMIRefreshKeys.Equipment);
        }
      }
      catch (ModuleMessageException ex)
      {
        throw (ex);
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.EqGrpGeneralError, "Error occured while cloning equipment element", "Error occured while cloning new  equipment element");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqGrpGeneralError, "Error occured while cloning equipment element", "Error occured while cloning new equipment element");
      }

      return result;
    }
  }
}
