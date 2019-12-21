using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.ProdManager
{
  public class DCSteelgrade: DataContractBase
  {
    [DataMember]
    public long Id { get; set; }
    [DataMember]
    public string SteelgradeCode { get; set; }
    [DataMember]
    public string Description { get; set; }
    [DataMember]
    public double? Density { get; set; }
    [DataMember]
    public double? ExpThermCoef { get; set; }
    [DataMember]
    public bool? IsDefault { get; set; }
    [DataMember]
    public string QualitySpecification { get; set; }
    [DataMember]
    public string CommercialGroup { get; set; }
    [DataMember]
    public string CustomerUseCode { get; set; }
    [DataMember]
    public string CustomerUseDescription { get; set; }
    [DataMember]
    public string GroupCode { get; set; }
    [DataMember]
    public string GroupDescription { get; set; }
    [DataMember]
    public long? FkSteelGroup { get; set; }
    [DataMember]
    public double? OvenRecipeTemperature { get; set; }
    [DataMember]
    public double? OvenRecipePosition { get; set; }
    [DataMember]
    public double? OvenRecipeTemperatureTollSup { get; set; }
    [DataMember]
    public double? OvenRecipeTemperatureTollInf { get; set; }
    [DataMember]
    public int? OvenRecipeDurationMin { get; set; }
    [DataMember]
    public int? OvenRecipeDurationMax { get; set; }

    [DataMember]
    public double? FeMax { get; set; }
    [DataMember]
    public double? FeMin { get; set; }
    [DataMember]
    public double? CMax { get; set; }
    [DataMember]
    public double? CMin { get; set; }
    [DataMember]
    public double? MnMax { get; set; }
    [DataMember]
    public double? MnMin { get; set; }
    [DataMember]
    public double? CrMax { get; set; }
    [DataMember]
    public double? CrMin { get; set; }
    [DataMember]
    public double? MoMax { get; set; }
    [DataMember]
    public double? MoMin { get; set; }
    [DataMember]
    public double? VMax { get; set; }
    [DataMember]
    public double? VMin { get; set; }
    [DataMember]
    public double? NiMax { get; set; }
    [DataMember]
    public double? NiMin { get; set; }
    [DataMember]
    public double? CoMax { get; set; }
    [DataMember]
    public double? CoMin { get; set; }
    [DataMember]
    public double? SiMax { get; set; }
    [DataMember]
    public double? SiMin { get; set; }
    [DataMember]
    public double? PMax { get; set; }
    [DataMember]
    public double? PMin { get; set; }
    [DataMember]
    public double? SMax { get; set; }
    [DataMember]
    public double? SMin { get; set; }
    [DataMember]
    public double? CuMax { get; set; }
    [DataMember]
    public double? CuMin { get; set; }
    [DataMember]
    public double? NbMax { get; set; }
    [DataMember]
    public double? NbMin { get; set; }
    [DataMember]
    public double? AlMax { get; set; }
    [DataMember]
    public double? AlMin { get; set; }
    [DataMember]
    public double? NMax { get; set; }
    [DataMember]
    public double? NMin { get; set; }
    [DataMember]
    public double? CaMax { get; set; }
    [DataMember]
    public double? CaMin { get; set; }
    [DataMember]
    public double? BMax { get; set; }
    [DataMember]
    public double? BMin { get; set; }
    [DataMember]
    public double? TiMax { get; set; }
    [DataMember]
    public double? TiMin { get; set; }
    [DataMember]
    public double? SnMax { get; set; }
    [DataMember]
    public double? SnMin { get; set; }
    [DataMember]
    public double? OMax { get; set; }
    [DataMember]
    public double? OMin { get; set; }
    [DataMember]
    public double? HMax { get; set; }
    [DataMember]
    public double? HMin { get; set; }
    [DataMember]
    public double? WMax { get; set; }
    [DataMember]
    public double? WMin { get; set; }
    [DataMember]
    public double? PbMax { get; set; }
    [DataMember]
    public double? PbMin { get; set; }
    [DataMember]
    public double? ZnMax { get; set; }
    [DataMember]
    public double? ZnMin { get; set; }
    [DataMember]
    public double? AsMax { get; set; }
    [DataMember]
    public double? AsMin { get; set; }
    [DataMember]
    public double? MgMax { get; set; }
    [DataMember]
    public double? MgMin { get; set; }
    [DataMember]
    public double? SbMax { get; set; }
    [DataMember]
    public double? SbMin { get; set; }
    [DataMember]
    public double? BiMax { get; set; }
    [DataMember]
    public double? BiMin { get; set; }
    [DataMember]
    public double? TaMax { get; set; }
    [DataMember]
    public double? TaMin { get; set; }
    [DataMember]
    public double? ZrMax { get; set; }
    [DataMember]
    public double? ZrMin { get; set; }
    [DataMember]
    public double? CeMax { get; set; }
    [DataMember]
    public double? CeMin { get; set; }
    [DataMember]
    public double? TeMax { get; set; }
    [DataMember]
    public double? TeMin { get; set; }
  }
}
