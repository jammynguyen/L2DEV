using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.MVHistory
{
  public class DCL1L3MaterialConnector : DataContractBase
  {
    [DataMember]
    public long RawMaterialId { get; set; }

    [DataMember]
    public long PRMMaterialId { get; set; }
  }
}
