using System.Configuration;

namespace PE.DTO.Internal.TcpProxy.Configuration
{
  public class ListenerElement : ConfigurationElement
  {
    [ConfigurationProperty("telegramid", IsKey = true, IsRequired = true)]
    public int TelegramID
    {
      get { return (int)this["telegramid"]; }
      set { this["telegramid"] = value; }
    }

    [ConfigurationProperty("telLength", IsRequired = true)]
    public int TelegramLength
    {
      get { return (int)this["telLength"]; }
      set { this["telLength"] = value; }
    }

    [ConfigurationProperty("socket", IsRequired = true)]
    public int Socket
    {
      get { return (int)this["socket"]; }
      set { this["socket"] = value; }
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
