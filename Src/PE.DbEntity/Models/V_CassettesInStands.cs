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
    
    public partial class V_CassettesInStands
    {
        public long CassetteTypeId { get; set; }
        public long CassetteId { get; set; }
        public long StandId { get; set; }
        public string CassetteTypeName { get; set; }
        public string CassetteTypeDescription { get; set; }
        public Nullable<short> Type { get; set; }
        public bool IsInterCassette { get; set; }
        public short Arrangement { get; set; }
        public string CassetteName { get; set; }
        public short NumberOfPositions { get; set; }
        public short CassetteStatus { get; set; }
        public short StandNo { get; set; }
        public Nullable<short> NumberOfRolls { get; set; }
        public string StandZoneName { get; set; }
        public Nullable<short> IsOnLine { get; set; }
        public bool IsCalibrated { get; set; }
        public Nullable<short> Position { get; set; }
        public short StandStatus { get; set; }
        public long RollSetId { get; set; }
        public long RollSetHistoryId { get; set; }
        public short RollsetStatus { get; set; }
        public short RollSetType { get; set; }
        public string RollSetName { get; set; }
        public string RollSetDescription { get; set; }
        public Nullable<System.DateTime> Mounted { get; set; }
        public Nullable<System.DateTime> Dismounted { get; set; }
        public short RollsetHistoryStatus { get; set; }
    }
}
