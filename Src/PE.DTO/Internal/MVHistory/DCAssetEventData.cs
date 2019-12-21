using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.MVHistory
{
  public class DCAssetEventData :DataContractBase
  {
    /// <summary>
    /// Feature id - defined by PE
    /// </summary>
    [DataMember]
    public long FeatureId { get; set; }

    /// <summary>
    /// Asset Id
    /// </summary>
    [DataMember]
    public long AssetId { get; set; }
    /// <summary>
    /// Feature name
    /// </summary>
    [DataMember]
    public string FeatureName { get; set; }



  }
}
