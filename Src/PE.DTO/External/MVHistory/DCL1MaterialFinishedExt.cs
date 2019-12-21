using PE.DTO.External.Adapter;
using SMF.Core.DC;
using System.Runtime.Serialization;
using System;
using System.Runtime.InteropServices;
using SMF.Module.Core.Interfaces.Telegram;

namespace PE.DTO.External.MVHistory
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCL1MaterialFinishedExt : BaseExternalTelegram
  {
    /// <summary>
    /// Common telegram header
    /// </summary>
    public DCHeaderExt Header { get; set; }

    /// <summary>
    /// Billet unique identification
    /// </summary>
    public uint BasId { get; set; }

    /// <summary>
    /// Unique location id
    /// </summary>
    public uint LocationId { get; set; }
  }
}
