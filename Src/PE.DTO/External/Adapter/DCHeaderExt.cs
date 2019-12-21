using SMF.Core.DC;
using SMF.Module.Core.Interfaces.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.External.Adapter
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 22)]
  public class DCHeaderExt: BaseExternalTelegram
  {
    /// <summary>
    /// Unique message id
    /// </summary>
    public ushort MessageId;

    /// <summary>
    /// Message Counter
    /// </summary>
    public ushort Counter;

    /// <summary>
    /// Time stamp of sender system
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
    public string TimeStamp;

  }
}
