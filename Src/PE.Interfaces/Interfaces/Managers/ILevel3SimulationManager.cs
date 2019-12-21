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
  public interface ILevel3SimulationManager : IManagerBase
  {
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> AddWorkOrderToSchedule(DCWorkOrderToSchedule dc);
    [FaultContract(typeof(ModuleMessage))]
    Task CreateWorkOrders(int limitWeightMin, int limitWeightMax, int materialMin, int materialMax, int delayBetweenOrders);
  }
}
