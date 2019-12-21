using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.DTO.Internal.Delay
{
  public class DCDelayToDivide : DataContractBase
  {
    [DataMember]
    public long DelayId { get; set; }
    [DataMember]
    public int DurationOfNewDelay { get; set; }
  }
}
