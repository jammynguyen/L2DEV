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
    
    public partial class RLSCassetteType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RLSCassetteType()
        {
            this.RLSCassettes = new HashSet<RLSCassette>();
        }
    
        public long CassetteTypeId { get; set; }
        public System.DateTime CreatedTs { get; set; }
        public System.DateTime LastUpdateTs { get; set; }
        public string CassetteTypeName { get; set; }
        public string CassetteTypeDescription { get; set; }
        public Nullable<short> NumberOfRolls { get; set; }
        public Nullable<short> Type { get; set; }
        public bool IsInterCassette { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RLSCassette> RLSCassettes { get; set; }
    }
}
