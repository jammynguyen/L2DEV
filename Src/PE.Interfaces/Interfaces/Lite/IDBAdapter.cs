using PE.DTO.External.DBAdapter;
using PE.DTO.Internal.MVHistory;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Lite
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IDBAdapter : IBaseModule
  {
    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> SendBackMsgToL3(DCWorkOrderStatusExt dataToSend);
  }
}
