using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.DTO.Internal.RollShop
{
  public class DCCassetteTypeData : DataContractBase
  {
    [DataMember]
    public long? Id
    {
      get;
      set;
    }
    [DataMember]
    public string CassetteTypeName
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
    public short? NumberOfRolls
    {
      get; set;
    }
  }
}
