using PE.DbEntity.Enums;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Quality
{
  public class DCQualityAssignment : DataContractBase
  {

    [DataMember]
    public List<long> ProductIds
    {
      get;
      set;
    }

    [DataMember]
    public List<long> DefectCatalogueElementIds
    {
      get;
      set;
    }

    [DataMember]
    public PE.DbEntity.Enums.ProductQuality QualityFlag
    {
      get;
      set;
    }
    [DataMember]
    public string Remark
    {
      get;
      set;
    }
  }
}
