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
    
    public partial class MVHTriggersFeature
    {
        public long TriggersFeatureId { get; set; }
        public long FKTriggerId { get; set; }
        public long FKFeatureId { get; set; }
        public string Relations { get; set; }
        public short PassNo { get; set; }
        public short OrderSeq { get; set; }
    
        public virtual MVHTrigger MVHTrigger { get; set; }
        public virtual MVHFeature MVHFeature { get; set; }
    }
}