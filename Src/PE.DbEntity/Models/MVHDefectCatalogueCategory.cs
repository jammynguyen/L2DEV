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
    
    public partial class MVHDefectCatalogueCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MVHDefectCatalogueCategory()
        {
            this.MVHDefectCatalogues = new HashSet<MVHDefectCatalogue>();
        }
    
        public long DefectCatalogueCategoryId { get; set; }
        public string DefectCatalogueCategoryCode { get; set; }
        public string DefectCatalogueCategoryName { get; set; }
        public string DefectCatalogueCategoryDescription { get; set; }
        public bool IsDefault { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MVHDefectCatalogue> MVHDefectCatalogues { get; set; }
    }
}