using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.TcpProxy.Configuration
{
  [ConfigurationCollection(typeof(ListenerElement))]
  public class ListenerCollection : ConfigurationElementCollection
  {
    protected override ConfigurationElement CreateNewElement()
    {
      return new ListenerElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((ListenerElement)element).TelegramID;
    }

    public ListenerElement this[int index]
    {
      get
      {
        return BaseGet(index) as ListenerElement;
      }
    }
  }
}
