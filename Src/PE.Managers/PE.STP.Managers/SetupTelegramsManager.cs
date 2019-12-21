using PE.DbEntity.Models;
using PE.DTO.Internal.Setup;
using PE.DTO.Internal.TcpProxy.Configuration;
using PE.Interfaces.Managers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using System;
using System.Threading.Tasks;
using PE.STP.Handlers;
using PE.Interfaces.Interfaces.SendOffice;

namespace PE.STP.Managers
{
  public class SetupTelegramsManager : ISetupTelegramsManager
  {

    #region members

    private readonly ISetupTelegramSendOffice _sendOffice;

    #endregion
    #region handlers

    private readonly SetupTelegramsHandler _setupTelegramsHandler;

    #endregion
    #region ctor

    public SetupTelegramsManager(ISetupTelegramSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _setupTelegramsHandler = new SetupTelegramsHandler();
    }

    #endregion
    public virtual async Task<DataContractBase> ProcessSendTelegramSetupAsync(DCTelegramSetupId message)
    {
      DataContractBase result = new DataContractBase();

      using (PEContext ctx = new PEContext())
      {
        try
        {
          SendOfficeResult<DataContractBase> communicationResult = null;

          System.Collections.Generic.List<V_TelegramValues> data = _setupTelegramsHandler.GetSetupTelegramStructureBySetupTelegramId(ctx, message.TelegramId);


          DCCommonSetupStructure dataContract = _setupTelegramsHandler.ConvertTelegramSetupDataToTransferObject(ctx, data, message.TelegramId);

          communicationResult = await _sendOffice.SendTelegramSetupDataAsync(dataContract);

          if (communicationResult.OperationSuccess)
          {
            NotificationController.Info($"Setup data (telegram ID: {message.TelegramId}) sent");
          }

          else
            NotificationController.Error($"Error during sending setup data (telegram ID: {message.TelegramId})");

        }
        catch (ArgumentOutOfRangeException)
        {
          NotificationController.RegisterAlarm(Defs.EmptyTelegramValuesList, $"Given list of structure telegram values is empty - telegram not exist in db");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        }
        catch (ArgumentException)
        {
          NotificationController.RegisterAlarm(Defs.NotExistingTelegram, $"Wrong telegram id {message.TelegramId}", message.TelegramId);
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        }
        catch (Exception)
        {
          NotificationController.RegisterAlarm(Defs.TelegramSetupError, $"Critical error when processing {message.TelegramId} to send", message.TelegramId);
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        }
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateTelegramValueAsync(DCTelegramSetupValue message)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          STPTelegramValue telegramValue = _setupTelegramsHandler.UpdateTelegramValue(ctx, message);
          await ctx.SaveChangesAsync();
        }
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.TelegramSetupError, $"Critical error when processing {message.TelegramId} to update values", message.TelegramId);
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
      }

      return result;
    }

    public virtual async Task<DataContractBase> CreateNewVersionOfTelegramAsync(DCTelegramSetupId message)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          STPTelegram telegramValue = _setupTelegramsHandler.CreateNewVersionOfTelegram(ctx, message);
          await ctx.SaveChangesAsync();
        }
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.TelegramSetupError, $"Critical error when processing {message.TelegramId} to crete new version", message.TelegramId);
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
      }

      return result;
    }

    public virtual async Task<DataContractBase> DeleteSetupAsync(DCTelegramSetupId message)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          //STPTelegram telegramValue = _setupTelegramsHandler.CreateNewVersionOfTelegram(ctx, message);
          //await ctx.SaveChangesAsync();
        }
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.TelegramSetupError, $"Critical error when processing {message.TelegramId} to crete new version", message.TelegramId);
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
      }

      return result;
    }

    public virtual async Task<DataContractBase> RequestUpdateSetupFromL1Async(DCTelegramSetupId message)
    {
      DataContractBase result = new DataContractBase();

      using (PEContext ctx = new PEContext())
      {
        try
        {
          SendOfficeResult<DataContractBase> communicationResult = null;

          System.Collections.Generic.List<V_TelegramValues> data = _setupTelegramsHandler.GetSetupTelegramStructureBySetupTelegramId(ctx, message.TelegramId);


          DCCommonSetupStructure dataContract = _setupTelegramsHandler.ConvertTelegramSetupDataToTransferObject(ctx, data, message.TelegramId, true);

          communicationResult = await _sendOffice.SendSetupDataRequestToL1Async(dataContract);

          if (communicationResult.OperationSuccess)
          {
            NotificationController.Info($"L1 setup data request (telegram ID: {message.TelegramId}) sent");
          }

          else
            NotificationController.Error($"Error during sending L1 setup data request (telegram ID: {message.TelegramId})");

        }
        catch (ArgumentOutOfRangeException)
        {
          NotificationController.RegisterAlarm(Defs.EmptyTelegramValuesList, $"Given list of structure telegram values is empty - telegram not exist in db");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        }
        catch (ArgumentException)
        {
          NotificationController.RegisterAlarm(Defs.NotExistingTelegram, $"Wrong telegram id {message.TelegramId}", message.TelegramId);
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        }
        catch (Exception)
        {
          NotificationController.RegisterAlarm(Defs.TelegramSetupError, $"Critical error when processing {message.TelegramId} to send", message.TelegramId);
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        }
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateSetupFromL1Async(DCCommonSetupStructure message)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        using (PEContext ctx = new PEContext())
        {
          STPTelegram telegramValue = _setupTelegramsHandler.UpdateSetupFromL1(ctx, message);
          await ctx.SaveChangesAsync();
					//add hmi refresh
        }
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.TelegramSetupError, $"Critical error when processing {message.TelegramId} to crete new version", message.TelegramId);
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
      }

      return result;
    }
  }
}
