using PE.DTO.Internal.Delay;
using PE.DTO.Internal.MVHistory;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers
{
  public interface IDispatcherManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessAssetEventAsync(DCMeasData message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessScrapEventAsync(DCL1ScrapData message);
  }
}
