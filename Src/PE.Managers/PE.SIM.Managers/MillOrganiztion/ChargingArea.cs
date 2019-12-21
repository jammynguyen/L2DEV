using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.DTO.External.MVHistory;
using PE.DTO.Internal.MVHistory;
using PE.Interfaces.SendOffice;
using PE.L1S.Handlers.MillSetup;
using SMF.Module.Core;
using SMF.Module.Notification;

namespace PE.SIM.Managers.MillOrganiztion
{
  public class ChargingArea : Area
  {
    #region ctor

    public ChargingArea(int capacity, List<MillAssetConfig> assets, Area precedingArea, ISimulationSendOffice sendOffice) : base(capacity, assets, precedingArea, sendOffice)
    {

    }



    #endregion
    #region public methods

    public async Task IntroduceMaterial()
    {
      if (IsLoadingMaterialPossible())
      {
        DCL1BasIdRequestExt dc = new DCL1BasIdRequestExt()
        {
          RequestToken = Convert.ToUInt16(new Random().Next(1, 60000)),
          IsSimnu = 1,
          Header = new DTO.External.Adapter.DCHeaderExt()
          {
            Counter = 1,
            MessageId = 1000,
            TimeStamp = DateTime.Now.ToString(),
          }
        };

        SendOfficeResult<DCPEBasIdExt> result = await _sendOffice.SendL1MaterialIdRequestToAdapterAsync(dc);
        if (result.OperationSuccess)
        {
          NotificationController.Info("Received new BasId: {0}", result.DataConctract.BasId);

          AddMaterial(result.DataConctract.BasId);
        }

        else
          throw new Exception("Id of material cannot be retrived from PE");
      }
    }



    #endregion
  }
}
