using SMF.Core.DC;
using SMF.Module.Core.Interfaces.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.DBAdapter
{
  public class L2L3WorkOrderReportExt : BaseExternalTelegram
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
    /// total number of products (coils or bundles)
    /// </summary>
    [DataMember]
    public short ProductsNumber { get; set; }

    /// <summary>
    /// total weight of all products
    /// </summary>
    [DataMember]
    public int TotalProductsWeight { get; set; }

    /// <summary>
    /// total weight of all raw materials
    /// </summary>
    [DataMember]
    public int TotalRawMaterialWeight { get; set; }

    /// <summary>
    /// number of all planned raw materials
    /// </summary>
    [DataMember]
    public short RawMaterialIsPlaned { get; set; }

    /// <summary>
    /// number of all discharged raw materials
    /// </summary>
    [DataMember]
    public short RawMaterialsDischarged { get; set; }

    /// <summary>
    /// number of all rolled raw materials
    /// </summary>
    [DataMember]
    public short RawMaterialsRolled { get; set; }

    /// <summary>
    /// number of all scrapped raw materials
    /// </summary>
    [DataMember]
    public short RawMaterialsScrapped { get; set; }

    /// <summary>
    /// number of all rejected raw materials
    /// </summary>
    [DataMember]
    public short RawMaterialsRejected { get; set; }

    /// <summary>
    /// metallic yield
    /// </summary>
    [DataMember]
    public double MetallicYield { get; set; }

    /// <summary>
    /// 1 - normal end of production, 2 - operator manually set status to Finished, 3 - operator manually sets order status to Cancelled
    /// </summary>
    [DataMember]
    public short EventType { get; set; }

    /// <summary>
    /// Timestamp when first raw material has been charged to furnace
    /// </summary>
    [DataMember]
    public DateTime? ProductionStart { get; set; }

    /// <summary>
    /// Timestamp when last product of the work order has been created
    /// </summary>
    [DataMember]
    public DateTime? ProductionEnd { get; set; }
  }
}
