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
    public class EquipmentGroupManager : IEquipmentGroupManager
    {
		#region members

		#endregion
		#region handlers

		private readonly EquipmentGroupsHandler eqGrpHandler;

		#endregion

		public EquipmentGroupManager ()
    {
			//no send office
			eqGrpHandler = new EquipmentGroupsHandler();

		}

    public virtual async Task<DataContractBase> CreateEquipmentGroupAsync(DCEquipmentGroup dc)
    {
      DataContractBase result = new DataContractBase();

			try
			{
				using (PEContext ctx = new PEContext())
				{
					MNTEquipmentGroup grp = await eqGrpHandler.GetEquipmentGroupByCode(ctx, dc.EquipmentGroupCode);

					if (grp != null)
					{
						//NotificationController.RegisterAlarm(Defs.EqGrpCodeNotUnique, "Error while creating new record in table MNTEquipmentGroups: short group code " + dc.EquipmentGroupCode + " is not unique. ", dc.EquipmentGroupCode);
						throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqGrpCodeNotUnique, "Code not unique", dc.EquipmentGroupCode);
					}
					MNTEquipmentGroup egrp = new MNTEquipmentGroup()
					{
						CreatedTs = DateTime.Now,
						LastUpdateTs = DateTime.Now,
						EquipmentGroupCode = dc.EquipmentGroupCode,
						EquipmentGroupDescription = dc.Description,
						EquipmentGroupName = dc.EquipmentGroupName,
						IsDefault = false
					};
					await eqGrpHandler.CreateEquipmentGroupAsync(ctx, egrp);
					await ctx.SaveChangesAsync();
					NotificationController.RegisterAlarm(Defs.EqGrpCreated, "New record in table MNTEquipmentGroups: group code " + dc.EquipmentGroupCode, dc.EquipmentGroupCode);
					await ModuleController.HmiRefresh(HMIRefreshKeys.EquipmentGroups);
				}
			}
      catch (ModuleMessageException ex)
      {
        //NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw (ex);
        //throw new ModuleMessageException(ex.ModuleMessage.ModuleName, ex.ModuleMessage.ErrorCode, ex.ModuleMessage.ShortDescription, );
      }
      catch
			{
				NotificationController.RegisterAlarm(Defs.EqGrpGeneralError, "Error occured while creating new  equipment group", dc.EquipmentGroupCode);
				NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
				throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqGrpGeneralError, "Error occured while creating new  equipment group ");

			}

			return result;
    }

    public virtual async Task<DataContractBase> DeleteEquipmentGroupAsync(DCEquipmentGroup message)
    {
			DataContractBase result = new DataContractBase();

			try
			{
				using (PEContext ctx = new PEContext())
				{
					//check if equipment exist
					MNTEquipmentGroup grp = await eqGrpHandler.GetEquipmentGroupById(ctx, message.EquipmentGroupId, includeEquipment: true);

					if (grp == null)
					{
						throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqGrpGeneralError, "Record not found", "Record not found");
          }
					//check if there is any equipment assigned to that group
					if (grp.MNTEquipments != null && grp.MNTEquipments.Count > 0)
					{
						throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqGrpDeleteNotPossible, "Equipment group is not empty. Delete operation aborted.", grp.EquipmentGroupCode);
					}

					await eqGrpHandler.DeleteEquipmentGroup(ctx, grp);

					await ctx.SaveChangesAsync();
					NotificationController.RegisterAlarm(Defs.EqGrpDeleted, "Maintenance group with code " + grp.EquipmentGroupCode + " has been deleted", grp.EquipmentGroupCode);
					await ModuleController.HmiRefresh(HMIRefreshKeys.EquipmentGroups);
				}
			}
      catch (ModuleMessageException ex)
      {
        //NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw (ex);
      }
      catch
			{
				NotificationController.RegisterAlarm(Defs.EqGrpGeneralError, "Error occured while deleting equipment group", "Error occured while deleting equipment group");
				NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
				throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqGrpGeneralError, "Error occured while deleting equipment group ");
			}

			return result;
		}

    public virtual async Task<DataContractBase> UpdateEquipmentGroupAsync(DCEquipmentGroup message)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
				{
					//get record
					MNTEquipmentGroup grp = await eqGrpHandler.GetEquipmentGroupById(ctx, message.EquipmentGroupId);

					if (grp == null)
					{
						//record not found
						throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqGrpGeneralError, "Record with ID " + message.EquipmentGroupId + " not found");
					}
					//check if there equipment group code is unique
					MNTEquipmentGroup grp2 = await eqGrpHandler.GetEquipmentGroupByCode(ctx, message.EquipmentGroupCode, id: message.EquipmentGroupId);
        if (grp2 != null)
        {
          throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqGrpCodeNotUnique, "Error occured while updating equipment group. There is another group with same code.", message.EquipmentGroupCode);
          //throw new ModuleMessageException(ModuleController.ModuleName, Defs.TestWO, "Error occured while updating equipment group. There is another group with same code.", "Brenton Tarrant got you");
          //throw new ModuleMessageException(ModuleController.ModuleName, "PPL002", $"Critical error when adding work order [id:{12345}] to schedule", 12345);
        }

					grp.EquipmentGroupCode = message.EquipmentGroupCode;
					grp.EquipmentGroupName = message.EquipmentGroupName;
					grp.EquipmentGroupDescription = message.Description;
					grp.LastUpdateTs = DateTime.Now;
					await ctx.SaveChangesAsync();

					await ModuleController.HmiRefresh(HMIRefreshKeys.EquipmentGroups);
				}
      }
      catch (ModuleMessageException ex)
      {
        //NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw (ex);
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.EqGrpGeneralError, "Error occured while update of equipment group", "Error occured while update of equipment group");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.EqGrpGeneralError, "Error occured while updating equipment group", "Error occured while update of equipment group");

      }

      return result;
    }
  }
}
