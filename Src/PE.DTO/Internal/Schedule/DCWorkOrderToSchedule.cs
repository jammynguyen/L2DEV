using PE.DbEntity.Enums;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Schedule
{
  public class DCWorkOrderToSchedule : DataContractBase
  {
    [DataMember]
    public long WorkOrderId { get; set; }

    [DataMember]
    public long ScheduleId { get; set; }

    [DataMember]
    public ScheduleMoveOperator Direction { get; set; }

    [DataMember]
    public short OrderSeq { get; set; }
  }
}
