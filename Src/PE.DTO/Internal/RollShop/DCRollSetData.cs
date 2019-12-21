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
  public class DCRollSetData : DataContractBase
  {
    [DataMember]
    public long? Id
    {
      get;
      set;
    }
    [DataMember]
    public RollSetStatus? RollSetStatus
    {
      get;
      set;
    }
    [DataMember]
    public RollSetStatus? RollSetStatusNew
    {
      get;
      set;
    }
    [DataMember]
    public RollSetType RollSetType
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
    public long? UpperRollId
    {
      get;
      set;
    }
    [DataMember]
    public double? UpperDiameter
    {
      get;
      set;
    }
    [DataMember]
    public string RollNameUpper
    {
      get;
      set;
    }
    [DataMember]
    public string RollTypeUpper
    {
      get;
      set;
    }
    [DataMember]
    public long? RollTypeIdUpper
    {
      get;
      set;
    }
    [DataMember]
    public long? BottomRollId
    {
      get;
      set;
    }
    [DataMember]
    public double? BottomDiameter
    {
      get;
      set;
    }
    [DataMember]
    public string RollNameBottom
    {
      get;
      set;
    }
    [DataMember]
    public string RollTypeBottom
    {
      get;
      set;
    }
    [DataMember]
    public long? RollTypeIdBottom
    {
      get;
      set;
    }
    [DataMember]
    public long? ThirdRollId
    {
      get;
      set;
    }
    [DataMember]
    public double? ThirdDiameter
    {
      get;
      set;
    }
    [DataMember]
    public string RollNameThird
    {
      get;
      set;
    }
    [DataMember]
    public string RollTypeThird
    {
      get;
      set;
    }
    [DataMember]
    public long? RollTypeIdThird
    {
      get;
      set;
    }
    [DataMember]
    public short? IsThirdRoll
    {
      get;
      set;
    }
    [DataMember]
    public long? RollSetHistoryId
    {
      get;
      set;
    }
    [DataMember]
    public short RollSetHistoryStatus
    {
      get;
      set;
    }
    [DataMember]
    public string CassetteName
    {
      get;
      set;
    }
    [DataMember]
    public short? PositionInCassette
    {
      get;
      set;
    }
    [DataMember]
    public short? StandNo
    {
      get;
      set;
    }
    [DataMember]
    public DateTime? Mounted
    {
      get;
      set;
    }
    [DataMember]
    public DateTime? Dismounted
    {
      get;
      set;
    }
    [DataMember]
    public string RollStatusName
    {
      get;
      set;
    }
    [DataMember]
    public long? CassetteId
    {
      get;
      set;
    }

    [DataMember]
    public long? InterCassetteId
    {
      get;
      set;
    }

    [DataMember]
    public string RollSetStatusActual
    {
      get;
      set;
    }
    [DataMember]
    public short CassetteStatus
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
    public long? GrooveTemplateId
    {
      get;
      set;
    }
  }
}
