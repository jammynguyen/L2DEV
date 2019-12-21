using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PE.DTO.Internal.ProdManager
{
  public class DCRawMaterialData : SMF.Core.DC.DataContractBase
  {
    [DataMember]
    public long RawMaterialId { get; set; }

    [DataMember]
    public long? FKMaterialId { get; set; }

    [DataMember]
    public double OverallWeight { get; set; }
  }
}
