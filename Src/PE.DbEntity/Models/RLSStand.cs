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
    
    public partial class RLSStand
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RLSStand()
        {
            this.RLSCassettes = new HashSet<RLSCassette>();
        }
    
        public long StandId { get; set; }
        public System.DateTime CreatedTs { get; set; }
        public System.DateTime LastUpdateTs { get; set; }
        public short Status { get; set; }
        public short StandNo { get; set; }
        public Nullable<short> NumberOfRolls { get; set; }
        public string StandZoneName { get; set; }
        public Nullable<short> IsOnLine { get; set; }
        public bool IsCalibrated { get; set; }
        public Nullable<short> Position { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RLSCassette> RLSCassettes { get; set; }
    }
}
