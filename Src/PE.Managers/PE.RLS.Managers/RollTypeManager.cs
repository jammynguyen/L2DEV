using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.Common;
using PE.DbEntity.Models;
using PE.DTO.Internal.RollShop;
using PE.Interfaces.Interfaces.Managers;
using PE.Interfaces.Interfaces.SendOffice;
using PE.RLS.Handlers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;

namespace PE.RLS.Managers
{
  public class RollTypeManager : IRollTypeManager
  {
    private readonly IRollTypeManagerSendOffice _sendOffice;

    private readonly RollTypeHandler _rollTypeHandler;


    public RollTypeManager(IRollTypeManagerSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _rollTypeHandler = new RollTypeHandler();
    }

    public virtual async Task<DataContractBase> InsertRollTypeAsync(DCRollTypeData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null || (String.IsNullOrEmpty(dc.RollTypeName)))
      {
        NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while inserting Roll Type to DB. RollTypeName is not valid");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while inserting RollType to DB. RollTypeName is not valid");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSRollType duplicatedRollType = _rollTypeHandler.GetRollTypeFromName(ctx, dc.RollTypeName);

          if(duplicatedRollType!=null)
          {
            NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while inserting Roll Type to DB. RollTypeName is not valid");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while inserting RollType to DB. RollTypeName is not valid");
          }
          RLSRollType rollType = new RLSRollType();
          if (rollType != null)
          {
            
            //rollType.AccBilletCntLimit = dc.AccBilletCntLimit;
            //rollType.AccWeightLimit = dc.AccWeightLimit;
            //rollType.ChokeType = dc.ChokeType;
            //rollType.CreatedTs = DateTime.Now;
            //rollType.LastUpdateTs = DateTime.Now;
            //rollType.DiameterMax = dc.DiameterMax;
            //rollType.DiameterMin = dc.DiameterMin;
            //rollType.DrawingName = dc.DrawingName;
            //rollType.Length = dc.Length;
            //rollType.RollTypeDescription = dc.RollTypeDescription;
            //rollType.RollTypeName = dc.RollTypeName;
            //rollType.RoughnessMax = dc.RoughnessMax;
            //rollType.RoughnessMin = dc.RoughnessMin;
            //rollType.SteelgradeRoll = dc.SteelgradeRoll;
            //rollType.YieldStrengthRef = dc.YieldStrengthRef;
            _rollTypeHandler.CreateRollType(ctx, ref rollType, dc);

            ctx.RLSRollTypes.Add(rollType);
            
            await ctx.SaveChangesAsync();

            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
          //else
          //{
          //  NotificationController.RegisterAlarm(Defs.RollTypeAlreadyExist, "Error while inserting RollType to DB. Roll Type with specified name already exists in DB");
          //  NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          //  throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeAlreadyExist, "Error while inserting Roll Type to DB. Roll Type with specified name already exists in DB");
          //}
        }
      }
      catch(Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while inserting Roll Type to DB.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while inserting Roll Type to DB.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateRollTypeAsync(DCRollTypeData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc==null)
      {
        NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while updating RollType");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while updating RollType.");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSRollType rollType = _rollTypeHandler.GetRollTypeFromId(ctx, dc.Id);

          if (rollType != null)
          {
            //rollType.AccBilletCntLimit = dc.AccBilletCntLimit;
            //rollType.AccWeightLimit = dc.AccWeightLimit;
            //rollType.ChokeType = dc.ChokeType;
            //rollType.CreatedTs = DateTime.Now;
            //rollType.DiameterMax = dc.DiameterMax;
            //rollType.DiameterMin = dc.DiameterMin;
            //rollType.DrawingName = dc.DrawingName;
            //rollType.Length = dc.Length;
            //rollType.RollTypeDescription = dc.RollTypeDescription;
            //rollType.RollTypeName = dc.RollTypeName;
            //rollType.RoughnessMax = dc.RoughnessMax;
            //rollType.RoughnessMin = dc.RoughnessMin;
            //rollType.SteelgradeRoll = dc.SteelgradeRoll;
            //rollType.YieldStrengthRef = dc.YieldStrengthRef;

            _rollTypeHandler.UpdateRollType(ctx, ref rollType, dc);

            await ctx.SaveChangesAsync();

            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while updating RollType.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while updating RollType.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> DeleteRollTypeAsync(DCRollTypeData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc==null)
      {
        NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while removing RollType.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while removing RollType.");
      }
      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSRollType rollType = _rollTypeHandler.GetRollTypeFromId(ctx, dc.Id);

          if (rollType != null)
          {
            ctx.RLSRollTypes.Remove(rollType);
            await ctx.SaveChangesAsync();
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while removing RollType.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while removing RollType.");
      }

      return result;
    }

  }
}
