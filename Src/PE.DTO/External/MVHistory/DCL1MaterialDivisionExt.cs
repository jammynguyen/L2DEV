using PE.DTO.External.Adapter;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SMF.Module.Core.Interfaces.Telegram;
using System.Runtime.InteropServices;
using PE.DTO.Internal.MVHistory;

namespace PE.DTO.External.MVHistory
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCL1MaterialDivisionExt: BaseExternalTelegram
  {

    /// <summary>
    /// Common header telegram
    /// </summary>
    public DCHeaderExt Header { get; set; }

    /// <summary>
    /// Billet basic automation ID
    /// </summary>
    public uint ParentBasId { get; set; }

    /// <summary>
    /// Random token for return message identification
    /// </summary>
    public ushort RequestToken { get; set; }

    /// <summary>
    /// Length of new material
    /// Unit meters
    /// </summary>
    public float NewMaterialLength { get; set; }

    /// <summary>
    /// 1st cut = 1
    /// 2nd cut = 2
    /// ...
    /// </summary>
    public ushort CutNumberInParent { get; set; }

    /// <summary>
    /// Unique location Id
    /// </summary>
    public ushort LocationId { get; set; }

    public override DataContractBase ToInternal(int? telId = null)
    {
      return new DCL1MaterialDivision()
      {
        CutNumberInParent = Convert.ToInt16(this.CutNumberInParent),
        LocationId = Convert.ToInt16(this.LocationId),
        NewMaterialLength = this.NewMaterialLength,
        ParentBasId = this.ParentBasId,
        RequestToken = Convert.ToInt32(this.RequestToken)
      };
    }
  }
}
