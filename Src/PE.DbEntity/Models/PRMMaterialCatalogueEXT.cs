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
    
    public partial class PRMMaterialCatalogueEXT
    {
        public long FKMaterialCatalogueId { get; set; }
        public System.DateTime CreatedTs { get; set; }
    
        public virtual PRMMaterialCatalogue PRMMaterialCatalogue { get; set; }
    }
}
