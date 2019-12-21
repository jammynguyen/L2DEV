using PE.DbEntity.Enums;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using PE.Interfaces.Interfaces.Managers;
using PE.Interfaces.Interfaces.SendOffice;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.TRK.Managers
{
  public class TrackingTriggersManager: ITrackingTriggersManager
  {
    #region members

    private readonly ITrackingProcessTriggerSendOffice _sendOffice;

    #endregion
    #region handlers

    #endregion
    #region ctor
    public TrackingTriggersManager(ITrackingProcessTriggerSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
    }
    #endregion

    ///// <summary>
    ///// Function process triggers registered for TRK module.
    ///// </summary>
    ///// <param name="message"></param>
    ///// <returns></returns>
    //public virtual async Task<DataContractBase> ProcessTrigger(DCTriggerData message)
    //{
    //  DataContractBase result = new DataContractBase();

    //  Console.ForegroundColor = ConsoleColor.Blue;
    //  Console.WriteLine($"Trigger {message.triggerType.ToString()} for material {message.materialId}");
    //  Console.ResetColor();

    //  switch (message.triggerType)
    //  {
    //    case TriggerType.MarkAsFinished:
    //      {
    //        //await _sendOffice.ChangeMaterialStatus(new DCNewMaterialStatus() { MaterialId = message.materialId, Status = RawMaterialStatus.ENUM_Rolled });
    //        break;
    //      }
    //    case TriggerType.ProdEnd_Bar:
    //    case TriggerType.ProdEnd_WireRod:
    //      {
    //        //await _sendOffice.CreateProductAfterProductionEnd(new DCMaterialProductionEnd() { RawMaterialId = message.materialId });
    //        break;
    //      }
    //    case TriggerType.Scrap:
    //      {

    //        break;
    //      }
    //    default:
    //      {
    //        NotificationController.Info("Trigger not registered for this module");
    //        break;
    //      }
    //  }


    //  return result;
    //}
  }
}
