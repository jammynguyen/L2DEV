using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.MVHistory
{
  public class DCPEBasId: DataContractBase
  {
    /// <summary>
    /// Material id for basic automation
    /// </summary>
    [DataMember]
    public uint BasId { get; set; }

    /// <summary>
    /// Random token sent by L1 in request telegram
    /// </summary>
    [DataMember]
    public int RequestToken { get; set; }

    /// <summary>
    /// divide flag
    /// </summary>
    [DataMember]
    public bool IsDivide { get; set; }
  }
}
