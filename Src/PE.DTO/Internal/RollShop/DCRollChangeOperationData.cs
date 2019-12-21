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
  public class DCRollChangeOperationData : DataContractBase
  {
    [DataMember]
    public long? StandId
    {
      get;
      set;
    }
    [DataMember]
    public short? Action
    {
      get;
      set;
    }
    [DataMember]
    public long? DismountedRollSet
    {
      get;
      set;
    }
    [DataMember]
    public long? MountedRollSet
    {
      get;
      set;
    }
    [DataMember]
    public short? Position
    {
      get;
      set;
    }

    [DataMember]
    public CassetteArrangement? Arrangement
    {
      get;
      set;
    }
    [DataMember]
    public CassetteArrangement? ArrangementNew
    {
      get;
      set;
    }

    [DataMember]
    public StandStatus? StandStatus
    {
      get;
      set;
    }

    [DataMember]
    public long? MountedCassette
    {
      get;
      set;
    }

    [DataMember]
    public long? DismountedCassette
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
    public string CassetteNameNew
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
    public long? InterCassId
    {
      get;
      set;
    }
  }
}
