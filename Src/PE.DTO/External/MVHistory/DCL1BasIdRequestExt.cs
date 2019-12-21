using PE.DTO.External.Adapter;
using PE.DTO.Internal.MVHistory;
using SMF.Core.DC;
using SMF.Module.Core.Interfaces.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.External.MVHistory
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCL1BasIdRequestExt: BaseExternalTelegram
  {
    /// <summary>
    /// Common telegram header
    /// </summary>
    public DCHeaderExt Header { get; set; }

    /// <summary>
    /// Random token for return message identification
    /// </summary>
    public ushort RequestToken { get; set; }
    public ushort IsSimnu { get; set; }

    public override DataContractBase ToInternal(int? telId = null)
    {
      return new DCL1BasIdRequest()
      {
        RequestToken = Convert.ToInt32(this.RequestToken),
        IsSimnu = Convert.ToBoolean(this.IsSimnu),
      };
    }

  }
}
