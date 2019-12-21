using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.DBAdapter;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers
{
  public interface IL3CommunicationManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DCWorkOrderStatus> ProccesExtWorkOrderMessageAsync(DCL3L2WorkOrderDefinition message);
  }
}
