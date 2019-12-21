using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.DTO.Internal.Setup
{
  public class DCCommonSetupStructure : DataContractBase
  {
    [DataMember]
    public int TelegramId { get; set; }
    [DataMember]
    public short Port { get; set; }
    [DataMember]
    public string IpAddress { get; set; }


    [DataMember]
    public List<SetupProperty> SetupProperties { get; set; }
  }

  public class SetupProperty
  {
    [DataMember]
    public long PropertyId { get; set; }
    public string PropertyName { get; set; }
    public string PropertyValue { get; set; }
    public string PropertyType { get; set; }
  }
}
