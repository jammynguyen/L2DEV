using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.MVHistory
{
  public class DCSample
  {
    /// <summary>
    /// Sample Value
    /// </summary>
    [DataMember]
    public float Value { get; set; }

    /// <summary>
    /// Sample head offset / sample number in case when not material realted
    /// Unit: [m]
    /// </summary>
    [DataMember]
    public float HeadOffset { get; set; }

    /// <summary>
    /// 1 in case when measured value is valid
    /// default: 0
    /// </summary>
    [DataMember]
    public int IsValid { get; set; }

    /// <summary>
    /// Time of offset from first sample
    /// Unit: [ms]
    /// </summary>
    [DataMember]
    public int TimeOffset { get; set; }
  }
}
