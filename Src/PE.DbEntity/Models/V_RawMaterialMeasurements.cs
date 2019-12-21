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
    
    public partial class V_RawMaterialMeasurements
    {
        public Nullable<long> RawMaterialId { get; set; }
        public long MeasurementId { get; set; }
        public long FeatureId { get; set; }
        public long AssetId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public int FeatureCode { get; set; }
        public string FeatureName { get; set; }
        public string AssetName { get; set; }
        public string ParentAssetName { get; set; }
        public short PassNo { get; set; }
        public bool IsLastPass { get; set; }
        public bool IsValid { get; set; }
        public System.DateTime MeasurementTime { get; set; }
        public Nullable<double> MeasurementValueMin { get; set; }
        public double MeasurementValueAvg { get; set; }
        public Nullable<double> MeasurementValueMax { get; set; }
        public string UnitSymbol { get; set; }
        public Nullable<long> FKHeatId { get; set; }
        public string WorkOrderName { get; set; }
        public string RawMaterialName { get; set; }
        public Nullable<long> FKWorkOrderId { get; set; }
    }
}