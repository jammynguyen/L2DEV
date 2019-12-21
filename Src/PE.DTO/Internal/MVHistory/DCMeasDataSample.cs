using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.MVHistory
{
  public class DCMeasDataSample : DCMeasData
  {

    /// <summary>
    /// Date and time of first sample
    /// </summary>
    [DataMember]
    public DateTime DateTimeOfFirstSample { get; set; }

    /// <summary>
    /// Number of samples sent in Samples array
    /// </summary>
    [DataMember]
    public int NumberOfSamples { get; set; }

    /// <summary>
    /// Array of measured values. Max sixe (x ) has to be agreed with L1 and be used for all messages
    /// </summary>
    [DataMember]
    public List<DCSample> Samples { get; set; }
  }
}
