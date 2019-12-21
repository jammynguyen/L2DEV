using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Adapter
{
  public class DCWorkOrderStatus : DataContractBase
  {
    [DataMember]
    public long Counter { get; set; }

    [DataMember]
    public PE.DbEntity.Enums.CommStatus Status { get; set; }

    [DataMember]
    public string BackMessage { get; set; }
  }
}
