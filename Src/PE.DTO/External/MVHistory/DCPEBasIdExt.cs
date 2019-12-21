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
  public class DCPEBasIdExt : BaseExternalTelegram
  {

    /// <summary>
    /// Common telegram Header
    /// </summary>
    public DCHeaderExt Header { get; set; }

    /// <summary>
    /// Material id for basic automation
    /// </summary>
    public uint BasId { get; set; }

    /// <summary>
    /// Random token sent by L1 in request telegram
    /// </summary>
    public ushort RequestToken { get; set; }

    public override int ToExternal(DataContractBase dc)
    {
      RequestToken = Convert.ToUInt16((dc as DCPEBasId).RequestToken);
      BasId = (dc as DCPEBasId).BasId;
      Header = new DCHeaderExt()
      {
        TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
        Counter = (dc as DCPEBasId).IsDivide == true ? Convert.ToUInt16(2) : Convert.ToUInt16(0),
        MessageId = 2000,
      };
      return 0;
    }
  }
}
