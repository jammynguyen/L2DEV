using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.ProdManager
{
  public class DCWorkOrdersList: DataContractBase
  {
    [DataMember]
    public List<long> WorkOrdersList { get; set; }

    [DataMember]
    public long RawMaterialId { get; set; }
  }
}
