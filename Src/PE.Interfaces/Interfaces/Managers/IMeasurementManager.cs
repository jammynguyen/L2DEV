using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.MVHistory;
using SMF.Core.DC;

namespace PE.Interfaces.Managers
{
  public interface IMeasurementManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessMeasurementAsync<T>(T message) where T : DCMeasData;
  }
}
