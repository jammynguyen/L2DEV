using PE.DTO.Internal.Delay;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers
{
  public interface IDelayCatalogueManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddDelayCatalogueAsync(DCDelayCatalogue delayCatalogue);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDelayCatalogueAsync(DCDelayCatalogue delayCatalogue);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteDelayCatalogueAsync(DCDelayCatalogue delayCatalogue);
  }
}
