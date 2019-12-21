using PE.DTO.Internal.TcpProxy.Configuration;
using System.Configuration;
using SMF.Module.Core;
using System.Threading.Tasks;
using PE.TCP.Managers.SenderTable;
using SMF.Core.Helpers;

namespace PE.TCP.Managers
{
  public class TcpCommunicationSendersManager : ManagerRoot
  {
    #region properties
    public static SenderSection ConfigSenders
    {
      get;
      private set;
    }

    public static SendersTable AllSenderInstances
    {
      get;
      private set;
    }
    #endregion Properties
    #region ctor
    public TcpCommunicationSendersManager() : base()
    {
      ConfigSenders = (SenderSection)ConfigurationManager.GetSection("SenderSection");

      if (ConfigSenders == null)
      {
        ModuleController.Logger.Error("Can not read senders from configuration file! Application aborted.");
        return;
      }
      if (!InitializeSenders())
      {
        return;
      }

    }
    #endregion

    /// <summary>
    /// Start AsyncSocketListener for each configured sender
    /// </summary>
    /// <returns></returns>
    public virtual bool InitializeSenders()
    {
      AllSenderInstances = new SendersTable(ConfigSenders.Senders.Count);
      foreach (SenderElement el in ConfigSenders.Senders)
      {
        AsyncSocketSender sender = new AsyncSocketSender(el.TelegramID, el.TelegramIDCustomer, el.TelegramLength, el.IP, el.Socket, el.Description, el.Alive, el.AliveCycle, el.AliveID, el.AliveLength, el.AliveOffset);  
        AllSenderInstances.AddSender(sender, el.TelegramID, el.TelegramIDCustomer);
      }
      return true;
    }

    /// <summary>
    /// After processing L1ExternalIdRequest it is needed to send response with this generated id by TCP 
    /// </summary>
    /// <param name="message"></param>
    public virtual void SendBaseIdResponse(DTO.External.MVHistory.DCPEBasIdExt message)
    {
      AsyncSocketSender sender = AllSenderInstances.GetSenderByTelId(message.Header.MessageId);
      if (sender != null)
      {
        sender.OpenConnection();
        sender.Send(ObjectDump.RawSerialize(message));
      }
    }
  }
}
