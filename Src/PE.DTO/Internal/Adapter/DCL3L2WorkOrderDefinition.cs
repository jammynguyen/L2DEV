using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Adapter
{
  public class DCL3L2WorkOrderDefinition : DataContractBase
  {
    /// <summary>
    /// Primary Key in transfer table
    /// </summary>
    [DataMember]
    public long Counter { get; set; }

    /// <summary>
    /// Time stamp when record has been inserted to transfer table
    /// </summary>
    [DataMember]
    public DateTime CreatedTs { get; set; }

    /// <summary>
    /// Time stamp when record has been updated in transfer table (initially null)
    /// </summary>
    [DataMember]
    public DateTime? UpdatedTs { get; set; }

    /// <summary>
    /// 0 - new record, 1 - processed and OK, 2 - processed and Error, 3 - processed and rejected
    /// </summary>
    [DataMember]
    public PE.DbEntity.Enums.CommStatus CommStatus { get; set; }

    /// <summary>
    /// Unique work order name
    /// </summary>
    [DataMember]
    public string WorkOrderName { get; set; }

    /// <summary>
    ///  unique product name, reference to Product Catalogue
    /// </summary>
    [DataMember]
    public string ProductName { get; set; }

    /// <summary>
    /// Date and time  when work order has been created in L3
    /// </summary>
    [DataMember]
    public DateTime L3Created { get; set; }

    /// <summary>
    /// Customer name
    /// </summary>
    [DataMember]
    public string CustomerName { get; set; }

    /// <summary>
    /// Deadline for completing of the production
    /// </summary>
    [DataMember]
    public DateTime ToBeReadyBefore { get; set; }

    /// <summary>
    /// Target total weight of the products
    /// </summary>
    [DataMember]
    public double TargetOrderWeight { get; set; }

    /// <summary>
    /// Minimum total weight of the products
    /// </summary>
    [DataMember]
    public double? TargetOrderWeightMin { get; set; }

    /// <summary>
    /// Minimum total weight of the products
    /// </summary>
    [DataMember]
    public double? TargetOrderWeightMax { get; set; }

    /// <summary>
    /// Heat name
    /// </summary>
    [DataMember]
    public string HeatName { get; set; }

    /// <summary>
    /// Number of associated raw materials (related to Heat) to be used
    /// </summary>
    [DataMember]
    public short RawMaterialNumber { get; set; }

    /// <summary>
    /// S - Square, B - Billet, R - Round, F - Flat
    /// </summary>
    [DataMember]
    public string RawMaterialShapeType { get; set; }

    /// <summary>
    /// Raw material thickness
    /// </summary>
    [DataMember]
    public double RawMaterialThickness { get; set; }

    /// <summary>
    /// Raw material width
    /// </summary>
    [DataMember]
    public double RawMaterialWidth { get; set; }

    /// <summary>
    /// Standard raw material length
    /// </summary>
    [DataMember]
    public double? RawMaterialLength { get; set; }

    /// <summary>
    /// Standard raw material weight
    /// </summary>
    [DataMember]
    public double RawMaterialWeight { get; set; }

    /// <summary>
    /// Type defined by customer, free text
    /// </summary>
    [DataMember]
    public string RawMaterialType { get; set; }

    /// <summary>
    /// External steel grade code (to be printed on label etc.)
    /// </summary>
    [DataMember]
    public string ExternalSteelGradeCode { get; set; }

    /// <summary>
    /// steel grade code
    /// </summary>
    [DataMember]
    public string SteelGradeCode { get; set; }

    /// <summary>
    /// Next aggregate to process output materials
    /// </summary>
    [DataMember]
    public string NextAggregate { get; set; }

    /// <summary>
    /// work order operation code
    /// </summary>
    [DataMember]
    public string OperationCode { get; set; }

    /// <summary>
    /// Customer's information about heating group
    /// </summary>
    [DataMember]
    public string HeatingGroup { get; set; }

    /// <summary>
    /// Customer's information about quality policy
    /// </summary>
    [DataMember]
    public string QualityPolicy { get; set; }

    /// <summary>
    /// Additional information to be printed on product labels
    /// </summary>
    [DataMember]
    public string ExtraLabelInformation { get; set; }

    /// <summary>
    /// temp flag to simulator L3 - can and should be removed
    /// </summary>
    [DataMember]
    public bool AmISimulated { get; set; }

  }
}
