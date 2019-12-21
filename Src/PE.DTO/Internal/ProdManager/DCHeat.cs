using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.DTO.Internal.ProdManager
{
    public class DCHeat : DataContractBase
    {
        [DataMember]
        public long HeatId { get; set; }

        [DataMember]
        public string HeatName { get; set; }

        [DataMember]
        public long FKMaterialCatalogueId { get; set; }

        [DataMember]
        public long? FKHeatSupplierId { get; set; }

        [DataMember]
        public double? HeatWeightRef { get; set; }

        [DataMember]
        public bool? IsDummy { get; set; }
    }
}
