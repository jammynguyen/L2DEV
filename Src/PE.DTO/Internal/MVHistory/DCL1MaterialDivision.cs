using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.MVHistory
{
  public class DCL1MaterialDivision: DataContractBase
  {
    /// <summary>
    /// Billet basic automation ID
    /// </summary>
    [DataMember]
    public uint ParentBasId { get; set; }

    /// <summary>
    /// Random token for return message identification
    /// </summary>
    [DataMember]
    public int RequestToken { get; set; }

    /// <summary>
    /// Length of new material
    /// Unit meters
    /// </summary>
    [DataMember]
    public float NewMaterialLength { get; set; }

    /// <summary>
    /// 1st cut = 1
    /// 2nd cut = 2
    /// ...
    /// </summary>
    [DataMember]
    public int CutNumberInParent { get; set; }

    /// <summary>
    /// Unique location Id
    /// </summary>
    [DataMember]
    public int LocationId { get; set; }
  }
}
