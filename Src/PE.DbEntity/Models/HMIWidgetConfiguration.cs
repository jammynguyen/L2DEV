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
    
    public partial class HMIWidgetConfiguration
    {
        public long WidgetConfigurationId { get; set; }
        public string WidgetName { get; set; }
        public bool IsActive { get; set; }
        public Nullable<short> OrderSeq { get; set; }
        public string WidgetFileName { get; set; }
        public string FKUserId { get; set; }
    }
}
