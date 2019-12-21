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
  public class DCRollSetGrooveSetup : DataContractBase
  {
    [DataMember]
    public long? Id
    {
      get;
      set;
    }
    [DataMember]
    public GrindingTurningAction Action
    {
      get;
      set;
    }
    [DataMember]
    public short? RollSetType
    {
      get;
      set;
    }
    [DataMember]
    public List<SingleGrooveSetup> GrooveSetupList
    {
      get;
      set;
    }

    [DataMember]
    public List<SingleRollDataInfo> RollDataInfoList
    {
      get;
      set;
    }

  }
}
