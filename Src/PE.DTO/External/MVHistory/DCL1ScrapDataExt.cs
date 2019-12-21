using PE.DTO.External.Adapter;
using SMF.Core.DC;
using System.Runtime.Serialization;
using System;
using System.Runtime.InteropServices;
using SMF.Module.Core.Interfaces.Telegram;
using PE.DTO.Internal.MVHistory;

namespace PE.DTO.External.MVHistory
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCL1ScrapDataExt : BaseExternalTelegram
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

    /// <summary>
    /// Type of scrap:
    ///1: can be rolled again
    ///2: cannont be rolled again
    ///3: can/cannot be rolled logic in L2
    /// </summary>
    public ushort TypeOfScrap { get; set; }

    public override DataContractBase ToInternal(int? telId = null)
    {
      return new DCL1ScrapData()
      {
        BasId = this.BasId,
        LocationId = Convert.ToInt64(this.LocationId),
        TypeOfScrap = (DbEntity.Enums.TypeOfScrap)Convert.ToInt32(this.TypeOfScrap)
      };

    }
  }
}
