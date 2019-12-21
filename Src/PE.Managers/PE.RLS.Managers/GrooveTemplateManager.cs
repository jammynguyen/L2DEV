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
  public class GrooveTemplateManager : IGrooveTemplateManager
  {
    private readonly IGrooveTemplateManagerSendOffice _sendOffice;

    private readonly GrooveTemplateHandler _grooveTemplateHandler;


    public GrooveTemplateManager(IGrooveTemplateManagerSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _grooveTemplateHandler = new GrooveTemplateHandler();
    }

    public virtual async Task<DataContractBase> InsertGrooveTemplateAsync(DCGrooveTemplateData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null || (String.IsNullOrEmpty(dc.GrooveTemplateName)))
      {
        NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while inserting Roll Type to DB. RollTypeName is not valid");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while inserting RollType to DB. RollTypeName is not valid");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {

          //RLSRollType rollType = _rollTypeHandler.GetRollTypeFromName(ctx, dc.RollTypeName);
          RLSGrooveTemplate grooveTemplate = new RLSGrooveTemplate();
          if (grooveTemplate != null)
          {
            _grooveTemplateHandler.CreateGrooveTemplate(ctx, ref grooveTemplate, dc);


            ctx.RLSGrooveTemplates.Add(grooveTemplate);


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

    public virtual async Task<DataContractBase> UpdateGrooveTemplateAsync(DCGrooveTemplateData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while updating RollType");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while updating RollType.");
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSGrooveTemplate grooveTemplate = _grooveTemplateHandler.GetGrooveTemplateFromId(ctx, dc.GrooveTemplateId);

          if (grooveTemplate != null)
          {
            _grooveTemplateHandler.UpdateGrooveTemplate(ctx, ref grooveTemplate, dc);
            //grooveTemplate.Angle1 = dc.Angle1;
            //grooveTemplate.Angle2 = dc.Angle2;
            //grooveTemplate.CreatedTs = DateTime.Now;
            //grooveTemplate.D1 = dc.D1;
            //grooveTemplate.D2 = dc.D2;
            //grooveTemplate.GrooveTemplateDescription = dc.Description;
            //grooveTemplate.GrindingProgramName = dc.GrindingProgramName;
            //grooveTemplate.GrooveTemplateName = dc.GrooveTemplateName;
            //grooveTemplate.GrooveTemplateCode = dc.NameShort;
            //grooveTemplate.Plane = dc.GroovePlane;
            //grooveTemplate.R1 = dc.R1;
            //grooveTemplate.R2 = dc.R2;
            //grooveTemplate.R3 = dc.R3;
            ////grooveTemplate.FKShapeId = dc.ShapeId;
            //grooveTemplate.GrooveTemplateCode = "DEF";
            //grooveTemplate.Shape = dc.Shape;
            //grooveTemplate.SpreadFactor = dc.SpreadFactor;
            //grooveTemplate.Status = 1;
            //grooveTemplate.W1 = dc.W1;
            //grooveTemplate.W2 = dc.W2;
            //grooveTemplate.Ds = dc.Ds;
            //grooveTemplate.Dw = dc.Dw;


            await ctx.SaveChangesAsync();

            await ModuleController.HmiRefresh(HMIRefreshKeys.Roll);
          }
        }
      }
      catch(Exception ex)
      {
        NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while updating Groove Template.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while updating GrooveTemplate.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> DeleteGrooveTemplateAsync(DCGrooveTemplateData dc)
    {
      DataContractBase result = new DataContractBase();

      if (dc == null)
      {
        NotificationController.RegisterAlarm(Defs.RollTypeSaveError, "Error while removing RollType.");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.RollTypeSaveError, "Error while removing RollType.");
      }
      try
      {
        using (PEContext ctx = new PEContext())
        {

          RLSGrooveTemplate grooveTemplate = _grooveTemplateHandler.GetGrooveTemplateFromId(ctx, dc.GrooveTemplateId);

          if (grooveTemplate != null)
          {
            ctx.RLSGrooveTemplates.Remove(grooveTemplate);
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
