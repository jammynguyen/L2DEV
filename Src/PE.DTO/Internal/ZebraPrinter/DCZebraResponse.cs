using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.ZebraPrinter
{
  public class DCZebraResponse : DataContractBase
  {
    [DataMember]
    public byte[] Picture { get; set; }
  }
}
