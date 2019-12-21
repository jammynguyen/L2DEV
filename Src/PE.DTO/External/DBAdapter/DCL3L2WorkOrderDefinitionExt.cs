using PE.DTO.Internal.Adapter;
using SMF.Core.DC;
using SMF.Module.Core.Interfaces.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.DBAdapter
{
  public class DCL3L2WorkOrderDefinitionExt : BaseExternalTelegram
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
    public short CommStatus { get; set; }

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
    /// simulation flag  - should be removed
    /// </summary>
    [DataMember]
    public bool AmISimulated { get; set; }


    public override string ToString()
    {
      string output = "Generated WO L3 telegram:\n";
      PropertyInfo[] properties = typeof(DCL3L2WorkOrderDefinitionExt).GetProperties();
      foreach (PropertyInfo prop in properties)
      {
        output += "\t" + prop.Name + ": " + prop.GetValue(this) + "\n";

      }
      return output;
    }
    public override DataContractBase ToInternal(int? telId = null)
    {
      return new DCL3L2WorkOrderDefinition()
      {
        Counter = this.Counter,
        CreatedTs = this.CreatedTs,
        UpdatedTs = this.UpdatedTs,
        CommStatus = (PE.DbEntity.Enums.CommStatus)this.CommStatus,
        WorkOrderName = this.WorkOrderName,
        ProductName = this.ProductName,
        L3Created = this.L3Created,
        CustomerName = this.CustomerName,
        ToBeReadyBefore = this.ToBeReadyBefore,
        TargetOrderWeight = this.TargetOrderWeight,
        TargetOrderWeightMin = this.TargetOrderWeightMin,
        TargetOrderWeightMax = this.TargetOrderWeightMax,
        HeatName = this.HeatName,
        RawMaterialNumber = this.RawMaterialNumber,
        RawMaterialShapeType = this.RawMaterialShapeType.ToString(),
        RawMaterialThickness = this.RawMaterialThickness,
        RawMaterialWidth = this.RawMaterialWidth,
        RawMaterialLength = this.RawMaterialLength,
        RawMaterialWeight = this.RawMaterialWeight,
        RawMaterialType = this.RawMaterialType,
        ExternalSteelGradeCode = this.ExternalSteelGradeCode,
        SteelGradeCode = this.SteelGradeCode,
        NextAggregate = this.NextAggregate,
        OperationCode = this.OperationCode,
        HeatingGroup = this.HeatingGroup,
        QualityPolicy = this.QualityPolicy,
        ExtraLabelInformation = this.ExtraLabelInformation,

        AmISimulated = this.AmISimulated,
      };

    }
  }
}
