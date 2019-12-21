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
  public class DCSampleExt : BaseExternalTelegram
  {
    /// <summary>
    /// Sample Value
    /// </summary>
    public float Value { get; set; }

    /// <summary>
    /// Sample head offset / sample number in case when not material realted
    /// Unit: [m]
    /// </summary>
    public float HeadOffset { get; set; }

    /// <summary>
    /// 1 in case when measured value is valid
    /// default: 0
    /// </summary>
    public ushort IsValid { get; set; }

    /// <summary>
    /// Time of offset from first sample
    /// Unit: [ms]
    /// </summary>
    public ushort TimeOffset { get; set; }
  }
}
