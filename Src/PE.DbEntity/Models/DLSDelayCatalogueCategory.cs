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
    
    public partial class DLSDelayCatalogueCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DLSDelayCatalogueCategory()
        {
            this.DLSDelayCatalogues = new HashSet<DLSDelayCatalogue>();
        }
    
        public long DelayCatalogueCategoryId { get; set; }
        public string DelayCatalogueCategoryCode { get; set; }
        public string DelayCatalogueCategoryName { get; set; }
        public string DelayCatalogueCategoryDescription { get; set; }
        public bool IsDefault { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DLSDelayCatalogue> DLSDelayCatalogues { get; set; }
    }
}
