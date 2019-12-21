using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUS.L1A.Handlers;
using PE.DTO.Internal.Setup;
using PE.Interfaces.Interfaces.Managers.Custom;
using PE.Interfaces.Interfaces.SendOffice.Custom;
using PE.L1S.Handlers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;

namespace CUS.L1A.Managers
{
  public class L1SetupManager : IL1SetupManager
  {
    #region members

    private static IL1SetupSendOffice _sendOffice;

    #endregion
    #region handlers

    private static AssetHandler _assetHandler;
    private static SetupStructureHandler _setupStructureHandler;
    private static OPCHandler _opcHandler;

    #endregion
    #region ctor
    public L1SetupManager(IL1SetupSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _assetHandler = new AssetHandler();
      _setupStructureHandler = new SetupStructureHandler();
      _opcHandler = new OPCHandler();
    }

    #endregion
    #region setup sending

    public async Task<DataContractBase> SendSetupMessageAsync(DCCommonSetupStructure message)
    {
      switch (message.TelegramId)
      {
        // your telegrams defined in db decoded by ID
        case 1:
          {
            return await HandleTelegram1(message);
          }
        case 2:
          {
            return await HandleTelegram2(message);
          }
        default:
          {
            throw new Exception($"Telegram: {message.TelegramId} not implemented");
          }
      }
    }
    private async Task<DataContractBase> HandleTelegram2(DCCommonSetupStructure message)
    {
      foreach (SetupProperty element in message.SetupProperties)
      {
        NotificationController.Info("Sending telegram: {0}, property: {1}, value: {2}", message.TelegramId, element.PropertyName, element.PropertyValue);
        switch (element.PropertyType)
        {
          case "BOOL":
            {
              bool value = _setupStructureHandler.GetBoolValue(element);
              // send to L1
              await _opcHandler.Send(element.PropertyName, value);
              break;
            }
          case "INT":
            {
              short value = _setupStructureHandler.GetIntValue(element);
              // send to L1
              await _opcHandler.Send(element.PropertyName, value);
              break;
            }
          case "REAL":
            {
              float value = _setupStructureHandler.GetRealValue(element);
              // send to L1
              await _opcHandler.Send(element.PropertyName, value);
              break;
            }
          case "STRING":
            {
              byte[] value = _setupStructureHandler.GetStringValue(element);
              // send to L1
              await _opcHandler.Send(element.PropertyName, value);
              break;
            }
          case "DINT":
            {
              int value = _setupStructureHandler.GetDintValue(element);
              // send to L1
              await _opcHandler.Send(element.PropertyName, value);
              break;
            }
          default:
            {
              throw new Exception($"Type: {element.PropertyType} not implemented");
            }
        }
      }
      return new DataContractBase();
    }
    private async Task<DataContractBase> HandleTelegram1(DCCommonSetupStructure message)
    {
      foreach (SetupProperty element in message.SetupProperties)
      {
        NotificationController.Info("Sending telegram: {0}, property: {1}, value: {2}", message.TelegramId, element.PropertyName, element.PropertyValue);
        switch (element.PropertyType)
        {
          case "BOOL":
            {
              bool value = _setupStructureHandler.GetBoolValue(element);
              // send to L1
              await _opcHandler.Send(element.PropertyName, value);
              break;
            }
          case "INT":
            {
              short value = _setupStructureHandler.GetIntValue(element);
              // send to L1
              await _opcHandler.Send(element.PropertyName, value);
              break;
            }
          case "REAL":
            {
              float value = _setupStructureHandler.GetRealValue(element);
              // send to L1
              await _opcHandler.Send(element.PropertyName, value);
              break;
            }
          case "STRING":
            {
              byte[] value = _setupStructureHandler.GetStringValue(element);
              // send to L1
              await _opcHandler.Send(element.PropertyName, value);
              break;
            }
          case "DINT":
            {
              int value = _setupStructureHandler.GetDintValue(element);
              // send to L1
              await _opcHandler.Send(element.PropertyName, value);
              break;
            }
          default:
            {
              throw new Exception($"Type: {element.PropertyType} not implemented");
            }
        }
      }
      return new DataContractBase();
    }

    #endregion
    #region setup request

    public async Task<DataContractBase> HandleSetupRequestMessageAsync(DCCommonSetupStructure message)
    {
      await HandleSetupRequestAsync(message);
      return new DataContractBase();
    }
    private async Task HandleSetupRequestAsync(DCCommonSetupStructure message)
    {
      Task task = new Task(delegate {  RequestSetupUpdate(message); });
      task.Start();

      await Task.Delay(0);
    }

    private async void RequestSetupUpdate(DCCommonSetupStructure message)
    {
      HandleRequestOfTelegram(message);

     
      SendOfficeResult<DataContractBase> result = await _sendOffice.SendL1SetupToAdapterAsync(message);

      if (result.OperationSuccess)
        NotificationController.Info("Forwarding Setup request message to L1 - success");
      else
        NotificationController.Error("Forwarding Setup request message to L1  - error");
    }

    private async void HandleRequestOfTelegram(DCCommonSetupStructure message)
    {
      foreach (SetupProperty element in message.SetupProperties)
      {
        NotificationController.Info("Collecting telegram: {0}, property: {1}, value: {2}", message.TelegramId, element.PropertyName, element.PropertyValue);
        switch (element.PropertyType)
        {
          case "BOOL":
            {
              _setupStructureHandler.UpdateValue<bool>(element, await _opcHandler.Get<bool>(element.PropertyName));

              break;
            }
          case "INT":
            {
              _setupStructureHandler.UpdateValue<Int16>(element, await _opcHandler.Get<Int16>(element.PropertyName));

              break;
            }
          case "REAL":
            {
              _setupStructureHandler.UpdateValue<float>(element, await _opcHandler.Get<float>(element.PropertyName));

              break;
            }
          case "STRING":
            {
              _setupStructureHandler.UpdateValue<byte[]>(element, await _opcHandler.Get<byte[]>(element.PropertyName));
              break;
            }
          case "DINT":
            {
              _setupStructureHandler.UpdateValue<int>(element, await _opcHandler.Get<int>(element.PropertyName));
              break;
            }
          default:
            {
              throw new Exception($"Type: {element.PropertyType} not implemented");
            }
        }
      }
    }

    #endregion


  }
}
