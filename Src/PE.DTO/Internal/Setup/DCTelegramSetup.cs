using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Setup
{
	public class DCTelegramSetup : DataContractBase
	{
    [DataMember]
    public byte[] DataStream { get; set; }
    [DataMember]
    public int TelegramId { get; set; }
    [DataMember]
    public short Port { get; set; }
    [DataMember]
    public string IpAddress { get; set; }
  }
}
