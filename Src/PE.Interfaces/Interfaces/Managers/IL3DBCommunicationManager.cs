using PE.DTO.External.DBAdapter;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers
{
  public interface IL3DBCommunicationManager : IManagerBase
  {
    //Task UpdateTransferTableCommStatusesAsync(DCWorkOrderStatusExt dataToSend);
    [FaultContract(typeof(ModuleMessage))]
    Task TransferDataFromTransferTableToAdapterAsync();
    [FaultContract(typeof(ModuleMessage))]
    Task UpdateWorkOrdesWithTimeoutAsync();
  }
}
