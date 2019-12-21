
using PE.DTO.Internal.TcpProxy.Configuration;
using PE.TCP.Managers;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.TCP.Managers
{
    public class TcpCommunicationListenersManager: ManagerRoot
    {
    public static ListenerSection ConfigListeners
    {
      get;
      private set;
    }

    #region ctor
    public TcpCommunicationListenersManager() : base()
    {
      ConfigListeners = (ListenerSection)ConfigurationManager.GetSection("ListenerSection");

      if (ConfigListeners == null)
      {
        ModuleController.Logger.Error("Can not read listeners from configuration file! Application aborted.");
        return;
      }
      if (!InitializeListeners())
      {
        return;
      }

    }
    #endregion

    /// <summary>
    /// Start AsyncSocketListener for each configured listener
    /// </summary>
    /// <returns></returns>
    public virtual bool InitializeListeners()
    {
      foreach (ListenerElement el in ConfigListeners.Listeners)
      {
        AysncSocketListener listener = new AysncSocketListener(el.TelegramID, el.TelegramLength, el.Socket, el.Description, el.Alive, el.AliveCycle, el.AliveID, el.AliveLength, el.AliveOffset);
        Task.Run(() => listener.StartListening());
      }
      return true;
    }



  }
}
