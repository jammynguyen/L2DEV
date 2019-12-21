using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SMF.Module.Core.Interfaces.Telegram;

namespace PE.DTO.External.Setup
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCTCPSetpointTelegramEnvelopeExt : BaseExternalTelegram
  {
    [DataMember]
    public short Port { get; set; }
    [DataMember]
    public string IpAddress { get; set; }
    [DataMember]
    public BaseExternalTelegram TelegramToBeSend { get; set; }
  }
}
