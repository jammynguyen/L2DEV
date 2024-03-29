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
    
    public partial class DaysOfYear
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DaysOfYear()
        {
            this.ShiftCalendars = new HashSet<ShiftCalendar>();
        }
    
        public long DaysOfYearId { get; set; }
        public System.DateTime DateDay { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Day { get; set; }
        public Nullable<int> WeekNo { get; set; }
        public Nullable<bool> IsWeekend { get; set; }
        public Nullable<int> Quarter { get; set; }
        public string MonthName { get; set; }
        public string WeekDayName { get; set; }
        public int HalfOfYear { get; set; }
        public string DateANSI { get; set; }
        public string DateUS { get; set; }
        public string DateUK { get; set; }
        public string DateDE { get; set; }
        public string DateIT { get; set; }
        public string DateISO { get; set; }
        public Nullable<System.DateTime> FirstOfMonth { get; set; }
        public Nullable<System.DateTime> LastOfMonth { get; set; }
        public Nullable<System.DateTime> FirstOfYear { get; set; }
        public Nullable<System.DateTime> LastOfYear { get; set; }
        public Nullable<int> ISOWeekNumber { get; set; }
        public Nullable<int> WeekNumber { get; set; }
        public Nullable<int> YearNumber { get; set; }
        public Nullable<int> MonthNumber { get; set; }
        public Nullable<int> DayYearNumber { get; set; }
        public Nullable<int> DayNumber { get; set; }
        public Nullable<int> WeekDayNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShiftCalendar> ShiftCalendars { get; set; }
    }
}
