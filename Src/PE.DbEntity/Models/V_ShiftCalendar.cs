//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PE.DbEntity.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_ShiftCalendar
    {
        public long ShiftCalendarId { get; set; }
        public string ShiftCode { get; set; }
        public string CrewName { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public bool IsActualShift { get; set; }
        public System.DateTime PlannedStartTime { get; set; }
        public System.DateTime PlannedEndTime { get; set; }
        public long ShiftDefinitionId { get; set; }
        public long CrewId { get; set; }
        public long Sorting { get; set; }
    }
}
