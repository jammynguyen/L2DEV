using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.DTO.Internal.RollShop
{
  public class DCRollSetAccumulationData : DataContractBase
  {
    [DataMember]
    public long BilletId
    {
      get;
      set;
    }

    [DataMember]
    public string StandIds
    {
      get;
      set;
    }

    [DataMember]
    public short GrooveNo
    {
      get;
      set;
    }
  }
}
