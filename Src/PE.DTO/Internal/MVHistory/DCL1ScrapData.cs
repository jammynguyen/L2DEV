using PE.DbEntity.Enums;
using PE.DTO.External.Adapter;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.MVHistory
{
  public class DCL1ScrapData :DataContractBase
  {

    /// <summary>
    /// Billet unique identification
    /// </summary>
    [DataMember]
    public uint BasId { get; set; }

    /// <summary>
    /// Unique location id
    /// </summary>
    [DataMember]
    public long LocationId { get; set; }

    /// <summary>
    /// Type of scrap:
    ///1: can be rolled again
    ///2: cannont be rolled again
    ///3: can/cannot be rolled logic in L2
    /// </summary>
    [DataMember]
    public TypeOfScrap TypeOfScrap { get; set; }
  }
}
