using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Delay
{
  public class DCDelayCatalogue : DataContractBase
  {
    [DataMember]
    public long Id { get; set; }
    [DataMember]
    public string DelayName { get; set; }
    [DataMember]
    public double? StdDelayTime { get; set; }
    [DataMember]
    public bool IsActive { get; set; }
    [DataMember]
    public bool IsDefault { get; set; }
    [DataMember]
    public string DelayDescription { get; set; }
    [DataMember]
    public string DelayCode { get; set; }
    [DataMember]
    public string DelayCategory { get; set; } 
    [DataMember]
    public double? FKDelayCategoryId { get; set; }
    [DataMember]
    public long? ParentDelayCatalogueId { get; set; }
  }
}
