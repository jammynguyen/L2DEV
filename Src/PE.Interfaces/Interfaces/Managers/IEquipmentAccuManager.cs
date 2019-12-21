using PE.DTO.Internal.Maintenance;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers
{
  public interface IEquipmentAccuManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AccumulateEquipmentUsageAsync(DCEquipmentAccu message);

  }
}
