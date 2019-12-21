using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Schedule
{
  public class DCTestSchedule :DataContractBase
  {
    [DataMember]
    public long? ScheduleId;
    [DataMember]
    public short? OrderSeq { get; set; }
    [DataMember]
    public short? ScheduleStatus { get; set; }
    [DataMember]
    public double? PlannedWeight { get; set; }
    [DataMember]
    public long? PlannedTime { get; set; }
    [DataMember]
    public DateTime? StartTime { get; set; }
    [DataMember]
    public DateTime? EndTime { get; set; }
    [DataMember]
    public DateTime? PlannedStartTime { get; set; }
    [DataMember]
    public DateTime? PlannedEndTime { get; set; }
    [DataMember]
    public long? NoOfmaterials { get; set; }
  }
}
