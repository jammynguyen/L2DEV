using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.TcpProxy.Configuration
{
  public class ListenerSection : ConfigurationSection
  {
    [ConfigurationProperty("Listeners", IsDefaultCollection = true)]
    public ListenerCollection Listeners
    {
      get { return (ListenerCollection)this["Listeners"]; }
      set { this["Listeners"] = value; }
    }
  }
}
