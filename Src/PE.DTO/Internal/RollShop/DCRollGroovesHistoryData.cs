using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.RollShop
{
  public class DCRollGroovesHistoryData : DataContractBase
  {
    [DataMember]
    public long Id
    {
      get;
      set;
    }
    [DataMember]
    public short GrooveNumber
    {
      get;
      set;
    }
    [DataMember]
    public long RollId
    {
      get;
      set;
    }
    [DataMember]
    public long GrooveTemplateId
    {
      get;
      set;
    }
    [DataMember]
    public short Status
    {
      get;
      set;
    }
    [DataMember]
    public double AccWeight
    {
      get;
      set;
    }
    [DataMember]
    public long AccBilletCnt
    {
      get;
      set;
    }
    [DataMember]
    public long RollSetHistoryId
    {
      get;
      set;
    }
    [DataMember]
    public double ActDiameter
    {
      get;
      set;
    }

  }
}
