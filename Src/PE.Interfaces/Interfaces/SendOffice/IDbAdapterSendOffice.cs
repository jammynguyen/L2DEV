using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.External.DBAdapter;
using PE.DTO.Internal.DBAdapter;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.Interfaces.Interfaces.SendOffice
{
  public interface IDbAdapterSendOffice
  {
    Task<SendOfficeResult<DCWorkOrderStatusExt>> SendWorkOrderDataToAdapterAsync(DCL3L2WorkOrderDefinitionExt dataToSend);
  }
}
