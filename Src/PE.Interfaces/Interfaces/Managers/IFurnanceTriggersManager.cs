using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.MVHistory;
using System.ServiceModel;

namespace PE.Interfaces.Managers
{
  public interface IFurnanceTriggersManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessTriggerAsync(DCTriggerData message);
  }
}
