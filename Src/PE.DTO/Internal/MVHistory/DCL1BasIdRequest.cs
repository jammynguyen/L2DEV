using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.MVHistory
{
  public class DCL1BasIdRequest: DataContractBase
  {
    /// <summary>
    /// Random token for return message identification
    /// </summary>
    [DataMember]
    public int RequestToken { get; set; }
    [DataMember]
    public bool IsSimnu { get; set; }
  }
}
