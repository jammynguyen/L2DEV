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
    
    public partial class V_L1L3MaterialAssignment
    {
        public long Sorting { get; set; }
        public long MaterialId { get; set; }
        public string MaterialName { get; set; }
        public System.DateTime CreatedTs { get; set; }
        public System.DateTime LastUpdateTs { get; set; }
        public long FKHeatId { get; set; }
        public double Weight { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsDummy { get; set; }
        public Nullable<long> RawMaterialId { get; set; }
        public string RawMaterialName { get; set; }
        public Nullable<short> Status { get; set; }
        public string StatusName { get; set; }
        public long FKWorkOrderId { get; set; }
    }
}
