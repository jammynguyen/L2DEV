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
  public class DCEquipmentGroup : DataContractBase
  {
    [DataMember]
    public long EquipmentGroupId
    {
      get;
      set;
    }
    [DataMember]
    public string EquipmentGroupCode
    {
      get;
      set;
    }
    [DataMember]
    public string EquipmentGroupName
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
  }
}
