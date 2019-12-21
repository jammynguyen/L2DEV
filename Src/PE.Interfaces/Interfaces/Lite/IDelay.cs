using PE.DTO.Internal.Delay;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
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
  public interface IDelay : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDelayCatalogueAsync(DCDelayCatalogue delayCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddDelayCatalogueAsync(DCDelayCatalogue dcDelayCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteDelayCatalogueAsync(DCDelayCatalogue dcDelayCatalogue);

    [OperationContract]
    Task<DataContractBase> ProcessHeadEnterAsync(DCDelayEvent dcDelayEvent);

    [OperationContract]
    Task<DataContractBase> ProcessTailLeavesAsync(DCDelayEvent dcDelayEvent);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDelayAsync(DCDelay delay);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DivideDelayAsync(DCDelayToDivide delay);
  }
}
