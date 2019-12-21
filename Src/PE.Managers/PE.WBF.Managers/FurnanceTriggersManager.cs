using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.MVHistory;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Managers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PE.WBF.Managers
{
  public class FurnanceTriggersManager: IFurnanceTriggersManager
  {
    #region members

    private readonly IFurnaceProcessTriggersSendOffice _sendOffice;

    #endregion
    #region handlers


    #endregion
    #region ctor
    public FurnanceTriggersManager(IFurnaceProcessTriggersSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
    }

    #endregion

    /// <summary>
    /// Function process Triggers registered for WBF module
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public virtual async Task<DataContractBase> ProcessTriggerAsync(DCTriggerData message)
    {
      DataContractBase result = new DataContractBase();

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.WriteLine($"Trigger {message.triggerType.ToString()} for material {message.materialId}");
      Console.ResetColor();

      switch (message.triggerType)
      {
        case TriggerType.Charge:
          {
            //in case of charge it is needed to connect 1st material in schedule with charged Raw Material
            //await _sendOffice.RequestFirstInScheduleMaterial(message);
            await Task.Delay(1);
            break;
          }
        case TriggerType.UnCharge:
          {
            throw new NotImplementedException();
          }
        case TriggerType.Discharge:
          {
            break;
          }
        case TriggerType.UnDischarge:
          {
            throw new NotImplementedException();
          }
        default:
          {
            NotificationController.Info("Trigger not registered for this module");
            break;
          }
      }


      return result;
    }
  }
}
