using PE.DbEntity.Enums;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.RollShop
{
  public class DCRollData : DataContractBase
  {
    [DataMember]
    public long? Id
    {
      get;
      set;
    }
    [DataMember]
    public string RollName
    {
      get;
      set;
    }
    [DataMember]
    public RollStatus Status
    {
      get;
      set;
    }
    [DataMember]
    public long? RollTypeId
    {
      get;
      set;
    }
    [DataMember]
    public string RollTypeName
    {
      get;
      set;
    }
    [DataMember]
    public double? ActualDiameter
    {
      get;
      set;
    }
    [DataMember]
    public double? InitialDiameter
    {
      get;
      set;
    }
    [DataMember]
    public double? MinimumDiameter
    {
      get;
      set;
    }
    [DataMember]
    public string Supplier
    {
      get;
      set;
    }
    [DataMember]
    public Int16? GroovesNumber
    {
      get;
      set;
    }
    [DataMember]
    public string RollTypeDescription
    {
      get;
      set;
    }
    [DataMember]
    public double? DiameterMin
    {
      get;
      set;
    }
    [DataMember]
    public double? DiameterMax
    {
      get;
      set;
    }
    [DataMember]
    public double? RoughnessMin
    {
      get;
      set;
    }
    [DataMember]
    public double? RoughnessMax
    {
      get;
      set;
    }
    [DataMember]
    public double? YieldStrengthRef
    {
      get;
      set;
    }
    [DataMember]
    public string SteelgradeRoll
    {
      get;
      set;
    }
    [DataMember]
    public double? Length
    {
      get;
      set;
    }
    [DataMember]
    public string StatusName
    {
      get;
      set;
    }
    [DataMember]
    public string RollSetUpper
    {
      get;
      set;
    }
    [DataMember]
    public string RollSetBottom
    {
      get;
      set;
    }
    [DataMember]
    public string RollSetThird
    {
      get;
      set;
    }
    [DataMember]
    public short IsThirdRoll
    {
      get;
      set;
    }
    [DataMember]
    public string RollSetName
    {
      get;
      set;
    }
    [DataMember]
    public RollScrapReason? ScrapReason
    {
      get;
      set;
    }
    [DataMember]
    public DateTime? ScrapDate
    {
      get;
      set;
    }
    [DataMember]
    public string Description
    {
      get;
      set;
    }
    [DataMember]
    public long? RollsetId
    {
      get;
      set;
    }
    [DataMember]
    public short? Location
    {
      get;
      set;
    }
    [DataMember]
    public short RollType
    {
      get;
      set;
    }
  }
}
