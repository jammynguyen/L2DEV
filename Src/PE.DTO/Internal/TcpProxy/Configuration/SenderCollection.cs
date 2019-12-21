using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.TcpProxy.Configuration
{
  [ConfigurationCollection(typeof(SenderElement))]
  public class SenderCollection : ConfigurationElementCollection
  {
    protected override ConfigurationElement CreateNewElement()
    {
      return new SenderElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((SenderElement)element).TelegramID;
    }

    public SenderElement this[int index]
    {
      get
      {
        return BaseGet(index) as SenderElement;
      }
    }
  }
}
