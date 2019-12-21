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
  public class DCRollSetHistoryData : DataContractBase
  {
    [DataMember]
    public long? Id
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
    public long? RollSetId
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
    public RollSetHistoryStatus Status
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
    public short? IsThirdRoll
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
    public double? AccWeightLimit
    {
      get;
      set;
    }
  }
}
