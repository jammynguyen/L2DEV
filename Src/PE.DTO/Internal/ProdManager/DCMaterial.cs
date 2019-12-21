using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.DTO.Internal.ProdManager
{
  public class DCMaterial : DataContractBase
  {
    [DataMember]
    public long MaterialId { get; set; }
    [DataMember]
    public string FKWorkOrderIdRef { get; set; }
    [DataMember]
    public double Weight { get; set; }
    [DataMember]
    public long? MaterialsNumber { get; set; }
    [DataMember]
    public long? FKHeatId { get; set; }
  }
}
