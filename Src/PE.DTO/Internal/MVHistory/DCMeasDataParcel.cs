using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMF.Core.DC;
using System.Runtime.Serialization;

namespace PE.DTO.Internal.MVHistory
{
  [DataContract]
  public class DCMeasDataParcel : DataContractBase
  {
    public DCMeasDataParcel()
    {

    }
    public DCMeasDataParcel(DCMeasDataSample dcMeasurement)
    {
      this.Measurements = new List<DCMeasDataSample>()
            {
              dcMeasurement,
            };
    }

    /// <summary>
    /// List of DCMeasurement objects <see cref="DCMeasurement"/>
    /// </summary>
    [DataMember]
    public List<DCMeasDataSample> Measurements { get; set; }
  }
}
