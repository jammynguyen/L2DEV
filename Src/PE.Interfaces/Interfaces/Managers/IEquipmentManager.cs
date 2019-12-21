using PE.DTO.Internal.Maintenance;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers
{
  public interface IEquipmentManager
  {

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

  }
}
