using PE.DbEntity.Models;
using PE.DTO.Internal.ZebraPrinter;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMF.RepositoryPatternExt;
using PE.Interfaces.Interfaces.Managers;
using PE.ZPC.Handlers;
using PE.Interfaces.Interfaces.SendOffice;

namespace PE.ZPC.Managers
{
  public class ZebraConnectionManager : IZebraConnectionManager
  {
    #region members

    private readonly IZebraManagerSendOffice _sendOffice;

    #endregion
    #region handlers

    private readonly ZebraConnectionHandler _zebraConnectionHandler;

    #endregion
    #region ctor
    public ZebraConnectionManager(IZebraManagerSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _zebraConnectionHandler = new ZebraConnectionHandler();
    }

    #endregion


    public virtual async Task<DCZebraResponse> RequestPDFFromZebraWebServiceForHMIAsync(DCZebraRequest dcZebraRequest)
    {
      DCZebraResponse retMessage = new DCZebraResponse();

      using (PEContext ctx = new PEContext())
      {
        try
        {
          retMessage.Picture = await _zebraConnectionHandler.RequestPngFromZebraWebServiceForHMIAsync(ctx, dcZebraRequest.ProductId);
        }
        catch
        {
          NotificationController.RegisterAlarm(Defs.FailedToGenerateLabel, "Error while requesting Zebra Render from WebService");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          throw new ModuleMessageException(ModuleController.ModuleName, Defs.FailedToGenerateLabel, "Error while requesting Zebra Render from WebService");
        }
      }
      return retMessage;
    }
  }
}
