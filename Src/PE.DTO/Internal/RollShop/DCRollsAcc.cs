using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.DTO.Internal.RollShop
{
  public class DCRollsAccu : DataContractBase
  {
    [DataMember]
    public double MaterialWeight
    {
      get;
      set;
    }
  }
}
