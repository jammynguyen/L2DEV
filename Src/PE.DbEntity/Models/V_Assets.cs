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
    
    public partial class V_Assets
    {
        public long Seq { get; set; }
        public long AssetId { get; set; }
        public Nullable<long> ParentAssetId { get; set; }
        public string AssetName { get; set; }
        public string AssetDescription { get; set; }
        public string AssetCode { get; set; }
        public bool IsCheckpoint { get; set; }
        public short AssetOrderSeq { get; set; }
        public string Levels { get; set; }
        public string Path { get; set; }
        public Nullable<short> TimeIn { get; set; }
    }
}