using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.RollShop;
using PE.Interfaces.Managers;
using SMF.Core.DC;

namespace PE.Interfaces.Interfaces.Managers
{
  public interface IRollChangeManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RollChangeActionAsync(DCRollChangeOperationData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RollSetToCassetteAction(DCRollSetToCassetteAction dc);
  }
}
