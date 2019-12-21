using PE.DbEntity.Enums;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.MVHistory
{
  public class DCNewMaterialStatus : DataContractBase
  {
    [DataMember]
    public long MaterialId { get; set; }

    [DataMember]
    public RawMaterialStatus Status { get; set; }
  }
}
