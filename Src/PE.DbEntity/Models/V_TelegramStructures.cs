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
    
    public partial class V_TelegramStructures
    {
        public long Sort { get; set; }
        public Nullable<long> ElementId { get; set; }
        public Nullable<long> ParentElementId { get; set; }
        public string ElementCode { get; set; }
        public long DataTypeId { get; set; }
        public string DataType { get; set; }
        public Nullable<long> TelegramStructureId { get; set; }
        public string TelegramStructureIndex { get; set; }
        public Nullable<long> RootId { get; set; }
        public Nullable<bool> IsRoot { get; set; }
        public bool IsStructure { get; set; }
        public Nullable<bool> IsHeader { get; set; }
        public string StructureGraph { get; set; }
        public string StructurePath { get; set; }
        public string StructureCode { get; set; }
        public Nullable<int> StructureLevel { get; set; }
        public string OrderSeq { get; set; }
    }
}
