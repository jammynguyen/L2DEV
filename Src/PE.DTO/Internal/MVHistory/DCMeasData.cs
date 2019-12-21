using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.MVHistory
{
  public class DCMeasData :DataContractBase
  {
    /// <summary>
    /// Billet unique identification
    /// </summary>
    [DataMember]
    public uint BasId { get; set; }

    /// <summary>
    /// Pass number [1 - n]
    /// </summary>
    [DataMember]
    public int PassNumber { get; set; }

    /// <summary>
    /// Feature id - defined by PE
    /// </summary>
    //[DataMember]
    //public int BaseFeature { get; set; }

    /// <summary>
    /// 1 in case when measured value is valid
    /// default 0
    /// </summary>
    [DataMember]
    public int Valid { get; set; }

    /// <summary>
    /// Min value
    /// </summary>
    [DataMember]
    public float Min { get; set; }

    /// <summary>
    /// Avg value
    /// </summary>
    [DataMember]
    public float Avg { get; set; }

    /// <summary>
    /// Max value
    /// </summary>
    [DataMember]
    public float Max { get; set; }

    /// <summary>
    /// IsReversedPass
    /// </summary>
    [DataMember]
    public bool IsReversed { get; set; }

    /// <summary>
    /// IsLast pass
    /// </summary>
    [DataMember]
    public bool IsLastPass { get; set; }

    /// <summary>
    /// Event code defined in PE
    /// </summary>
    [DataMember]
    public int EventCode { get; set; }

  }
}
