using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.MVHistory
{
  public class DCTriggerData: DataContractBase
  {
    /// <summary>
    ///TriggerEnumType
    /// </summary>
    [DataMember]
    public DbEntity.Enums.TriggerType triggerType { get; set; }

    /// <summary>
    ///Triggerd material Id
    /// </summary>
    [DataMember]
    public long materialId { get; set; }

  }
}
