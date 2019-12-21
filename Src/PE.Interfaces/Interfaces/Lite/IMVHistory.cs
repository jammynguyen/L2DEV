using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using SMF.Module.Core;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PE.Interfaces.Lite
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IMVHistory : IBaseModule
  {
    #region L1

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessScrapMessageAsync(DCL1ScrapData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessCutMessageAsync(DCL1CutData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCPEBasId> ProcessL1BaseIdRequestAsync(DCL1BasIdRequest message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCPEBasId> ProcessDivisionMaterialMessageAsync(DCL1MaterialDivision message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessL1MeasurementAsync(DCMeasData message);


    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessL1SampleMeasurementAsync(DCMeasDataSample message);

    #endregion

    #region HMI

    #endregion

    //[OperationContract]
    //Task<DataContractBase> ChangeMaterialStatusAsync(DCNewMaterialStatus dataToSend);

    //[OperationContract]
    //Task<DataContractBase> ProcessMeasurementParcelAsync(DCMeasDataParcel message);

    //[OperationContract]
    //Task<DataContractBase> ConnectRawMaterialWithL3MaterialAsync(DTO.Internal.MVHistory.DCL1L3MaterialConnector dataToSend);

    //[OperationContract]
    //Task<DataContractBase> ProcessFinishedMessageAsync(DCL1MaterialFinished internalTel);
    //[OperationContract]
    //Task<DataContractBase> ProcessProductionEndAsync(DCMaterialProductionEnd dataToSend);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssignMaterialsAsync(DCMaterialAssign data);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UnassignMaterialAsync(DCMaterialAssign data);
    //[OperationContract]
    //Task<DataContractBase> ConnectRawMaterialWithProductAsync(DCProductData dataToSend);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> MarkMaterialAsScrapAsync(DCMaterialMarkedAsScrap data);
  }
}
