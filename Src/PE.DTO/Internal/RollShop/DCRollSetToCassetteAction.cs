using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Enums;
using SMF.Core.DC;

namespace PE.DTO.Internal.RollShop
{
  public class DCRollSetToCassetteAction : DataContractBase
  {
    [DataMember]
    public long? CassetteId
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
    public RollSetCassetteAction? Action
    {
      get;
      set;
    }
    [DataMember]
    public short? Postion
    {
      get;
      set;
    }

    [DataMember]
    public List<long?> RollSetIdList
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

  }
}
