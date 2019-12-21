using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.ProdManager
{
  public class DCMaterialCatalogue :DataContractBase
  {
    [DataMember]
    public long MaterialCatalogueId { get; set; }
    [DataMember]
    public string MaterialName { get; set; }
    [DataMember]
    public string Description { get; set; }
    [DataMember]
    public string SteelgradeCode { get; set; }
    [DataMember]
    public long MaterialCatalogueTypeId { get; set; }
    [DataMember]
    public double? Length { get; set; }
    [DataMember]
    public double Width { get; set; }
    [DataMember]
    public double Thickness { get; set; }
    [DataMember]
    public double Weight { get; set; }
    [DataMember]
    public string SAPNumber { get; set; }
    [DataMember]
    public long? SteelgradeId { get; set; }
    [DataMember]
    public long ShapeId { get; set; }

    // Details
    [DataMember]
    public double? LengthMin { get; set; }
    [DataMember]
    public double? LengthMax { get; set; }
    [DataMember]
    public double? WidthMin { get; set; }
    [DataMember]
    public double? WidthMax { get; set; }
    [DataMember]
    public double? ThicknessMin { get; set; }
    [DataMember]
    public double? ThicknessMax { get; set; }
    [DataMember]
    public double? WeightMin { get; set; }
    [DataMember]
    public double? WeightMax { get; set; }

    [DataMember]
    public long? TypeId { get; set; }
  }
}
