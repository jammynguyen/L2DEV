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
    
    public partial class ShiftDefinition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShiftDefinition()
        {
            this.ShiftCalendars = new HashSet<ShiftCalendar>();
        }
    
        public long ShiftDefinitionId { get; set; }
        public System.DateTime CreatedTs { get; set; }
        public System.DateTime LastUpdateTs { get; set; }
        public string ShiftCode { get; set; }
        public System.TimeSpan DefaultStartTime { get; set; }
        public System.TimeSpan DefaultEndTime { get; set; }
        public bool ShiftEndsNextDay { get; set; }
        public bool ShiftStartsPreviousDay { get; set; }
        public long NextShiftDefinitionId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShiftCalendar> ShiftCalendars { get; set; }
    }
}