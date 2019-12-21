using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.External.DBAdapter;
using PE.DTO.Internal.Adapter;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.Interfaces.Interfaces.SendOffice
{
  public interface IAdapterL3SendOffice
  {
    Task<SendOfficeResult<DCWorkOrderStatus>> SendWorkOrderDataAsync(DCL3L2WorkOrderDefinition dataToSend);
  }
}
