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
  public interface ICassetteTypeManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertCassetteTypeAsync(DCCassetteTypeData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateCassetteTypeAsync(DCCassetteTypeData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteCassetteTypeAsync(DCCassetteTypeData dc);
  }
}
