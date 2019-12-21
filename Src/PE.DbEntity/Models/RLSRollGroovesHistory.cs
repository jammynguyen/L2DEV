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
    
    public partial class RLSRollGroovesHistory
    {
        public long RollGroovesHistoryId { get; set; }
        public System.DateTime CreatedTs { get; set; }
        public System.DateTime LastUpdateTs { get; set; }
        public long FKRollId { get; set; }
        public long FKGrooveTemplateId { get; set; }
        public Nullable<long> FKRollSetHistoryId { get; set; }
        public short GrooveNumber { get; set; }
        public PE.DbEntity.Enums.RollGrooveStatus Status { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Deactivated { get; set; }
        public double AccWeight { get; set; }
        public long AccBilletCnt { get; set; }
        public Nullable<double> ActDiameter { get; set; }
    
        public virtual RLSRoll RLSRoll { get; set; }
        public virtual RLSGrooveTemplate RLSGrooveTemplate { get; set; }
        public virtual RLSRollSetHistory RLSRollSetHistory { get; set; }
    }
}