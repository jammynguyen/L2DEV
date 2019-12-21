using PE.Interfaces.Lite;
using PE.TCP.Managers;
using PE.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.TCP.TcpProxy.Module
{
  public class TcpCommunicationListenersManagerExt : TcpCommunicationListenersManager, ITcpCommunicationListeners
  {
    public TcpCommunicationListenersManagerExt() : base()
    {

    }
  }
}
