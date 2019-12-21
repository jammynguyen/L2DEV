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
  public interface IRollManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertRollAsync(DCRollData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateRollAsync(DCRollData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ScrapRollAsync(DCRollData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteRollAsync(DCRollData dc);


    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateStandConfigurationAsync(DCStandConfigurationData dc);
  }
}
