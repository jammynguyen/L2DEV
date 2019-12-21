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
  public interface IRollSetManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertRollSetAsync(DCRollSetData dc);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> UpdateRollSetAsync(DCRollSetData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssembleRollSetAsync(DCRollSetData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DisassembleRollSetAsync(DCRollSetData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteRollSetAsync(DCRollSetData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateRollSetStatusAsync(DCRollSetData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ConfirmRollSetStatusAsync(DCRollSetData dc);
  }
}
