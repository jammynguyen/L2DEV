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
  public interface ICassetteManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertCassetteAsync(DCCassetteData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateCassetteAsync(DCCassetteData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteCassetteAsync(DCCassetteData dc);
  }
}
