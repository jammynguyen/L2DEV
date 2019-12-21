using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PE.DTO.Internal.Schedule
{
  public class DCL3MaterialForRawMaterial: SMF.Core.DC.DataContractBase
  {
    [DataMember]
    public short ExternalRawMaterialId { get; set; }

    [DataMember]
    public long NextInScheduleL3Material { get; set; }

    [DataMember]
    public float Weigth { get; set; }

    [DataMember]
    public float Width { get; set; }

    [DataMember]
    public float Thickness { get; set; }

    [DataMember]
    public float Length { get; set; }
  }
}
