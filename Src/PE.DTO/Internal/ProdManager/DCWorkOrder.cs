using System;
using System.Runtime.Serialization;
using PE.DbEntity.Enums;
using SMF.Core.DC;

namespace PE.DTO.Internal.ProdManager
{
  public class DCWorkOrder : DataContractBase
  {
    [DataMember]
    public long WorkOrderId { get; set; }
    [DataMember]
    public string WorkOrderName { get; set; }
    [DataMember]
    public bool IsTestOrder { get; set; }
    [DataMember]
    public double TargetOrderWeight { get; set; }
    [DataMember]
    public double? TargetOrderWeightMin { get; set; }
    [DataMember]
    public double? TargetOrderWeightMax { get; set; }
    [DataMember]
    public DateTime CreatedInL3 { get; set; }
    [DataMember]
    public DateTime ToBeCompletedBefore { get; set; }
    [DataMember]
    public string NextAggregate { get; set; }
    [DataMember]
    public string OperationCode { get; set; }
    [DataMember]
    public string QualityPolicy { get; set; }
    [DataMember]
    public string ExtraLabelInformation { get; set; }
    [DataMember]
    public string ExternalSteelgradeCode { get; set; }

    [DataMember]
    public long? FKHeatIdRef { get; set; }
    [DataMember]
    public long FKProductCatalogueId { get; set; }
    [DataMember]
    public long FKMaterialCatalogueId { get; set; }
    [DataMember]
    public long? FKCustomerId { get; set; }
    [DataMember]
    public long? FKReheatingGroupId { get; set; }
    [DataMember]
    public long? MaterialsNumber { get; set; }
    [DataMember]
    public OrderStatus? WorkOrderStatus { get; set; }
  }
}
