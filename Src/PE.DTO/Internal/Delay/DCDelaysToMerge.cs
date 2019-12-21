using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.DTO.Internal.Delay
{
  public class DCDelaysToMerge : DataContractBase
  {
    [DataMember]
    public long firstDelayId { get; set; }
    [DataMember]
    public long secondDelayId { get; set; }
  }
}
