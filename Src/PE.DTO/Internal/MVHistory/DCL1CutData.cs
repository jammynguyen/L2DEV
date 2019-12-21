using PE.DbEntity.Enums;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.MVHistory
{
  public class DCL1CutData :DataContractBase
  {
    /// <summary>
    /// Billet unique identification
    /// </summary>
    [DataMember]
    public uint BasId { get; set; }

    /// <summary>
    /// Type of cut:
    /// 6: head cut
    /// 8: tail cut
    /// ??: sample cut
    /// </summary>
    [DataMember]
    public TypeOfCut TypeOfCut { get; set; }

    /// <summary>
    /// Length of cut
    /// </summary>
    [DataMember]
    public float CutLength { get; set; }

    /// <summary>
    /// Unique location Id
    /// </summary>
    [DataMember]
    public long LocationId { get; set; }
  }
}
