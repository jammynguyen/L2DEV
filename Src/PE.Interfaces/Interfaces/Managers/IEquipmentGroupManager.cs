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
  public interface IEquipmentGroupManager
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateEquipmentGroupAsync(DCEquipmentGroup message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteEquipmentGroupAsync(DCEquipmentGroup message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateEquipmentGroupAsync(DCEquipmentGroup message);
  }
}
