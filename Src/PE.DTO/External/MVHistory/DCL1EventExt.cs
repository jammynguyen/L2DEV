using PE.DTO.External.Adapter;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using SMF.Module.Core.Interfaces.Telegram;

namespace PE.DTO.External.MVHistory
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCL1EventExt: BaseExternalTelegram
  {
    /// <summary>
    /// Common header telegram
    /// </summary>
    public DCHeaderExt Header { get; set; }

    /// <summary>
    /// Billet unique identification
    /// </summary>
    public uint BasId { get; set; }

    /// <summary>
    /// Unique location Id
    /// </summary>
    public ushort LocationId { get; set; }

    /// <summary>
    /// Pass number [1 - n]
    /// </summary>
    public ushort PassNumber { get; set; }

    /// <summary>
    /// 1 in case of last pass,
    /// default 1
    /// </summary>
    public byte IsLastPass { get; set; }

    /// <summary>
    /// 1 in case of head-tail change
    /// default: 0
    /// </summary>
    public byte IsReversed { get; set; }
  }
}
