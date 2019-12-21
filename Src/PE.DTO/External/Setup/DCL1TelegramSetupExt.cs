using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SMF.Module.Core.Interfaces.Telegram;
using System.Runtime.InteropServices;
using PE.DTO.Internal.Setup;
using PE.DTO.External.Adapter;

namespace PE.DTO.External.Setup
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCL1TelegramSetupExt : BaseExternalTelegram
  {
    public DCHeaderExt Header { get; set; }
    public byte[] DataStream { get; set; }

    public override int ToExternal(DataContractBase dc)
    {
      Header = new DCHeaderExt()
      {
        TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
        MessageId = (ushort)(dc as DCTelegramSetup).TelegramId,
        Counter = 0 //to be filled by sender
      };

      DataStream = (dc as DCTelegramSetup).DataStream;

      return 0;
    }
  }
}
