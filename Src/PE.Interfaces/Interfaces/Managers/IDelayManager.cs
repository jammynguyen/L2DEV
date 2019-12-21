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
  public interface IDelayManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessHeadEnterAsync(DCDelayEvent dcDelayEvent);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessTailLeavesAsync(DCDelayEvent dcDelayEvent);

    Task<bool> UpdateCurrentDelayStatusAsync();

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDelayAsync(DCDelay delay);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DivideDelayAsync(DCDelayToDivide delayToDivide);
  }
}
