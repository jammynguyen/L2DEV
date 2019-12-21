using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Delay
{
  public class DCDelay : DataContractBase
  {
    [DataMember]
    public long Id { get; set; }

    [DataMember]
    public DateTime? LastUpdateTs { get; set; }

    [DataMember]
    public DateTime? CreatedTs { get; set; }

    [DataMember]
    public DateTime DelayStart { get; set; }

    [DataMember]
    public DateTime? DelayEnd { get; set; }

    [DataMember]
    public string Comment { get; set; }

    [DataMember]
    public bool IsPlanned { get; set; }

    [DataMember]
    public long FkDelayCatalogue { get; set; }
  }
}
