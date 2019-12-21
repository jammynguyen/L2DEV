using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.ProdManager
{
  public class DCProductCatalogue : DataContractBase
  {
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public long Id { get; set; }
    [DataMember]
    public double? Length { get; set; }
    [DataMember]
    public double? LengthMax { get; set; }
    [DataMember]
    public double? LengthMin { get; set; }
    [DataMember]
    public double? Width { get; set; }
    [DataMember]
    public double? WidthMax { get; set; }
    [DataMember]
    public double? WidthMin { get; set; }
    [DataMember]
    public double? Thickness { get; set; }
    [DataMember]
    public double? ThicknessMax { get; set; }
    [DataMember]
    public double? ThicknessMin { get; set; }
    [DataMember]
    public double? Weight { get; set; }
    [DataMember]
    public double? WeightMax { get; set; }
    [DataMember]
    public double? WeightMin { get; set; }
    [DataMember]
    public double? Ovality { get; set; }
    [DataMember]
    public double? OvalityMax { get; set; }
    [DataMember]
    public string Description { get; set; }
    [DataMember]
    public string SAPNumber { get; set; }
    [DataMember]
    public string Steelgrade { get; set; }
    [DataMember]
    public string Shape { get; set; }
    [DataMember]
    public string Type { get; set; }
    [DataMember]
    public double? ProfileToleranceMin { get; set; }
    [DataMember]
    public double? ProfileToleranceMax { get; set; }
    [DataMember]
    public double StdGapTime { get; set; }
    [DataMember]
    public double StdRollingTime { get; set; }
    [DataMember]
    public double? StdProductionTime { get; set; }
    [DataMember]
    public double StdMetallicYield { get; set; }
    [DataMember]
    public double StdQualityYield { get; set; }
    [DataMember]
    public double StdUtilizationTime { get; set; }
    [DataMember]
    public double? StdProductivity { get; set; }
    [DataMember]
    public bool Slitting { get; set; }
    [DataMember]
    public double? ExitSpeed { get; set; }
    [DataMember]
    public double? MaxTensile { get; set; }
    [DataMember]
    public double? MaxYieldPoint { get; set; }
    [DataMember]
    public DateTime LastUpdateTs { get; set; }
    [DataMember]
    public long? SteelgradeId { get; set; }
    [DataMember]
    public long ShapeId { get; set; }
    [DataMember]
    public long? TypeId { get; set; }
  }
}
