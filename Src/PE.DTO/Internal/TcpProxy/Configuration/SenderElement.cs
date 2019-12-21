using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.TcpProxy.Configuration
{
  public class SenderElement : ConfigurationElement
  {
    [ConfigurationProperty("telegramid", IsKey = true, IsRequired = true)]
    public int TelegramID
    {
      get { return (int)this["telegramid"]; }
      set { this["telegramid"] = value; }
    }

    [ConfigurationProperty("customerTelId", IsRequired = true)]
    public int TelegramIDCustomer
    {
      get { return (int)this["customerTelId"]; }
      set { this["customerTelId"] = value; }
    }

    [ConfigurationProperty("socket", IsRequired = true)]
    public int Socket
    {
      get { return (int)this["socket"]; }
      set { this["socket"] = value; }
    }

    [ConfigurationProperty("ip", IsRequired = true, DefaultValue = "dummy description")]
    public string IP
    {
      get { return (string)this["ip"]; }
      set { this["ip"] = value; }
    }

    [ConfigurationProperty("telLength", IsRequired = false, DefaultValue = -1)]
    public int TelegramLength
    {
      get { return (int)this["telLength"]; }
      set { this["telLength"] = value; }
    }

    [ConfigurationProperty("descr", IsRequired = true, DefaultValue = "dummy description")]
    public string Description
    {
      get { return (string)this["descr"]; }
      set { this["descr"] = value; }
    }

    [ConfigurationProperty("alive", IsRequired = true)]
    public int Alive
    {
      get { return (int)this["alive"]; }
      set { this["alive"] = value; }
    }

    [ConfigurationProperty("alivecycle", IsRequired = false, DefaultValue = -1)]
    public int AliveCycle
    {
      get { return (int)this["alivecycle"]; }
      set { this["alivecycle"] = value; }
    }

    [ConfigurationProperty("aliveId", IsRequired = false, DefaultValue = -1)]
    public int AliveID
    {
      get { return (int)this["aliveId"]; }
      set { this["aliveId"] = value; }
    }

    [ConfigurationProperty("aliveOffset", IsRequired = false, DefaultValue = -1)]
    public int AliveOffset
    {
      get { return (int)this["aliveOffset"]; }
      set { this["aliveOffset"] = value; }
    }

    [ConfigurationProperty("aliveLen", IsRequired = false, DefaultValue = -1)]
    public int AliveLength
    {
      get { return (int)this["aliveLen"]; }
      set { this["aliveLen"] = value; }
    }
  }
}
