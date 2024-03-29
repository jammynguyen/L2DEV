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
    
    public partial class Crew
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Crew()
        {
            this.Crews1 = new HashSet<Crew>();
            this.ShiftCalendars = new HashSet<ShiftCalendar>();
        }
    
        public long CrewId { get; set; }
        public System.DateTime CreatedTs { get; set; }
        public System.DateTime LastUpdateTs { get; set; }
        public string CrewName { get; set; }
        public string Description { get; set; }
        public string LeaderName { get; set; }
        public Nullable<short> DfltCrewSize { get; set; }
        public Nullable<short> OrderSeq { get; set; }
        public Nullable<long> NextCrewId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Crew> Crews1 { get; set; }
        public virtual Crew Crew1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShiftCalendar> ShiftCalendars { get; set; }
    }
}
