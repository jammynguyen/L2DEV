using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.Maintenance;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.Interfaces.Lite
{

  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IMaintenance : IBaseModule
  {
    #region Equipment Groups Processing
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateEquipmentGroupAsync(DCEquipmentGroup message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteEquipmentGroupAsync(DCEquipmentGroup message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateEquipmentGroupAsync(DCEquipmentGroup message);
    #endregion

    #region Equipment processing

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateEquipmentAsync(DCEquipment message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteEquipmentAsync(DCEquipment message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateEquipmentAsync(DCEquipment message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CloneEquipmentAsync(DCEquipment message);

    #endregion

    #region Equipment usage accumulation

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AccumulateEquipmentUsageAsync(DCEquipmentAccu dc);

    //TODO add trigger from tracking after material is produced

    #endregion

  }
}
