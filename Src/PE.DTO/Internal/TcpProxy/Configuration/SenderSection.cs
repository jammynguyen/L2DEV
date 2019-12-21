using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.TcpProxy.Configuration
{
  public class SenderSection : ConfigurationSection
  {
    [ConfigurationProperty("Senders", IsDefaultCollection = true)]
    public SenderCollection Senders
    {
      get { return (SenderCollection)this["Senders"]; }
      set { this["Senders"] = value; }
    }
  }
}
