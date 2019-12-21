using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.DTO.Internal.Setup;
using PE.Interfaces.Interfaces.Managers;
using PE.Interfaces.Interfaces.SendOffice;
using PE.STP.Handlers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;

namespace PE.STP.Managers
{
  public class SetupTelegramBinaryManager : ISetupTelegramsSenderManager
  {
    #region members

    private readonly ISetupTelegramBinarySendOffice _sendOffice;

    #endregion
    #region handlers

    private readonly SetupTelegramBinaryHandler _setupTelegramsBinaryHandler;
    private readonly SetupTelegramsHandler _setupTelegramsHandler;

    #endregion
    #region ctor

    public SetupTelegramBinaryManager(ISetupTelegramBinarySendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _setupTelegramsBinaryHandler = new SetupTelegramBinaryHandler();
      _setupTelegramsHandler = new SetupTelegramsHandler();
    }

    #endregion
    public virtual async Task<DataContractBase> ProcessForceSendTelegramSetupAsync(DCTelegramSetupId message)
    {
      DataContractBase result = new DataContractBase();

      using (PEContext ctx = new PEContext())
      {
        try
        {
          SendOfficeResult<DataContractBase> communicationResult = null;

          System.Collections.Generic.List<V_TelegramValues> data = _setupTelegramsHandler.GetSetupTelegramStructureBySetupTelegramId(ctx, message.TelegramId);

          byte[] byteData = _setupTelegramsBinaryHandler.ConvertTelegramSetupDataToByteArray(ctx, data);
          string ipAddress= _setupTelegramsHandler.GetIpAddress(data);
          short port= _setupTelegramsHandler.GetPortNumber(data);

          communicationResult = await _sendOffice.SendTelegramSetupByteDataAsync(new DCTelegramSetup() { DataStream = byteData, TelegramId = message.TelegramId, IpAddress = ipAddress, Port=port });

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
  }
}
