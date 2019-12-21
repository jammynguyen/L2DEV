using PE.DTO.External.Adapter;
using SMF.Core.DC;
using System.Runtime.Serialization;
using System;
using System.Runtime.InteropServices;
using SMF.Module.Core.Interfaces.Telegram;

namespace PE.DTO.Internal.MVHistory
{
  public class DCL1MaterialFinished : DataContractBase
  {
    /// <summary>
    /// Billet unique identification
    /// </summary>
    [DataMember]
    public uint BasId { get; set; }

    /// <summary>
    /// Unique location id
    /// </summary>
    [DataMember]
    public long LocationId { get; set; }
  }
}
