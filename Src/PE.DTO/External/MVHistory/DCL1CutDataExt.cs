using PE.DTO.External.Adapter;
using SMF.Core.DC;
using SMF.Module.Core.Interfaces.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using PE.DTO.Internal.MVHistory;
using PE.DbEntity.Enums;

namespace PE.DTO.External.MVHistory
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCL1CutDataExt :BaseExternalTelegram
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
    /// Type of cut:
    /// 6: head cut
    /// 8: tail cut
    /// ??: sample cut
    /// </summary>
    public ushort TypeOfCut { get; set; }

    /// <summary>
    /// Length of cut
    /// </summary>
    public float CutLength { get; set; }

    /// <summary>
    /// Unique location Id
    /// </summary>
    public ushort LocationId { get; set; }

    public override DataContractBase ToInternal(int? telId = null)
    {
      return new DCL1CutData()
      {
        CutLength = this.CutLength,
        BasId = this.BasId,
        TypeOfCut = (TypeOfCut)this.TypeOfCut,
        LocationId = Convert.ToInt64(this.LocationId)
      };
    }
  }
}
