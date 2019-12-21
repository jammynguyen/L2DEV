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
  public class CassetteTypeManager : ICassetteTypeManager
  {
    private readonly ICassetteTypeManagerSendOffice _sendOffice;

    private readonly CassetteTypeHandler _cassetteTypeHandler;


    public CassetteTypeManager(ICassetteTypeManagerSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _cassetteTypeHandler = new CassetteTypeHandler();
    }

    public virtual async Task<DataContractBase> InsertCassetteTypeAsync(DCCassetteTypeData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null || (String.IsNullOrEmpty(dc.CassetteTypeName)))
      {
        NotificationController.RegisterAlarm(Defs.CassetteTypeSaveError, "Error while inserting Cassette Type to DB. CassetteTypeName is not valid");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteTypeSaveError, "Error while inserting CassetteType to DB. CassetteTypeName is not valid");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSCassetteType cassetteType = _cassetteTypeHandler.GetCassetteTypeFromName(ctx, dc.CassetteTypeName);

          if (cassetteType == null)
          {
            cassetteType = new RLSCassetteType();

            _cassetteTypeHandler.CreateCassetteType(ctx, ref cassetteType, dc);
            //cassetteType = new RLSCassetteType
            //{
            //  CassetteTypeName = dc.CassetteTypeName,
            //  CreatedTs = DateTime.Now,
            //  LastUpdateTs = DateTime.Now,
            //  CassetteTypeDescription = dc.Description,
            //  NumberOfRolls = dc.NumberOfRolls
            //};

            ctx.RLSCassetteTypes.Add(cassetteType);


            await ctx.SaveChangesAsync();

            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
          //else
          //{
          //  NotificationController.RegisterAlarm(Defs.CassetteTypeAlreadyExist, "Error while inserting CassetteType to DB. Cassette Type with specified name already exists in DB");
          //  NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          //  throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteTypeAlreadyExist, "Error while inserting Cassette Type to DB. Cassette Type with specified name already exists in DB");
          //}
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.CassetteTypeSaveError, "Error while inserting Cassette Type to DB.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteTypeSaveError, "Error while inserting Cassette Type to DB.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateCassetteTypeAsync(DCCassetteTypeData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.CassetteTypeSaveError, "Error while updating CassetteType");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteTypeSaveError, "Error while updating CassetteType.");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSCassetteType cassetteType = _cassetteTypeHandler.GetCassetteTypeFromId(ctx, dc.Id);

          if (cassetteType != null)
          {
            _cassetteTypeHandler.UpdateCassetteType(ctx, ref cassetteType, dc);
            //cassetteType.CassetteTypeName = dc.CassetteTypeName;
            //cassetteType.LastUpdateTs = DateTime.Now;
            //cassetteType.CassetteTypeDescription = dc.Description;
            //cassetteType.NumberOfRolls = dc.NumberOfRolls;

            await ctx.SaveChangesAsync();

            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.CassetteTypeSaveError, "Error while updating CassetteType.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteTypeSaveError, "Error while updating CassetteType.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> DeleteCassetteTypeAsync(DCCassetteTypeData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.CassetteTypeSaveError, "Error while removing CassetteType.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteTypeSaveError, "Error while removing CassetteType.");
      }
      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSCassetteType cassetteType = _cassetteTypeHandler.GetCassetteTypeFromId(ctx, dc.Id);

          if (cassetteType != null)
          {
            ctx.RLSCassetteTypes.Remove(cassetteType);
            await ctx.SaveChangesAsync();
            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
        }
      }
      catch
      {
        NotificationController.RegisterAlarm(Defs.CassetteTypeSaveError, "Error while removing CassetteType.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.CassetteTypeSaveError, "Error while removing CassetteType.");
      }

      return result;
    }

  }
}
