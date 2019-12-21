using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.Interfaces.Managers;

namespace PE.Interfaces.Interfaces.Managers.Custom
{
  public interface IL1ConsumptionManager : IManagerBase
  {
    Task ConnectToOpcServerAsync();
    Task InitCommunicationAsync();
    Task ReadMeasurements();
  }
}
