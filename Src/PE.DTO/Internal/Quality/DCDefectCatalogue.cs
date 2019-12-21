using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Quality
{
  public class DCDefectCatalogue : DataContractBase
  {
    [DataMember]
    public long Id { get; set; }
    [DataMember]
    public string DefectCatalogueName { get; set; }
    [DataMember]
    public string DefectCatalogueCode { get; set; }
    [DataMember]
    public string DefectCatalogueCategory { get; set; }
    [DataMember]
    public string DefectCatalogueDescription { get; set; }
    [DataMember]
    public bool IsDefault { get; set; }
    [DataMember]
    public long FkDelayCatalogueCategoryId { get; set; }
  }
}
