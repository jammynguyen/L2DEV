using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.Interfaces.Managers;
using SMF.Core.DC;

namespace PE.Interfaces.Interfaces.Managers
{
  public interface ILevel1SimulationManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task StartSimulation();
  }
  //public interface ILevel1SimulationManagerOld : IManagerBase
  //{
  //  [FaultContract(typeof(ModuleMessage))]
  //  Task CallLine();
  //  //Task<DataContractBase> AddWorkOrderToSchedule(DCWorkOrderToSchedule dc);
  //}
}
