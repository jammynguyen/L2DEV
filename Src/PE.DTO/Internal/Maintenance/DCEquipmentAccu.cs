using PE.DbEntity.Enums;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Maintenance
{
  public class DCEquipmentAccu : DataContractBase
  {
    [DataMember]
    public long MaterialWeight
    {
      get;
      set;
    }
  }
}
