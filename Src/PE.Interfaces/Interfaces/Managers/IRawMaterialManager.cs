using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using PE.DTO.Internal.Schedule;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers
{
  public interface IRawMaterialManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessScrapMessageAsync(DCL1ScrapData message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessCutDataMessageAsync(DCL1CutData message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DCPEBasId> ProcessL1BaseIdRequestAsync(DCL1BasIdRequest message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssignL3MaterialPropertiesForRawMaterialAsync(DCL3MaterialForRawMaterial message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DCPEBasId> ProcessDivisionMaterialMessageAsync(DCL1MaterialDivision message);
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> ProcessFinishedMessageAsync(DCL1MaterialFinished message);
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> ChangeMaterialStatusAsync(DCNewMaterialStatus message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssignMaterialsAsync(DCMaterialAssign message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UnassignMaterialAsync(DCMaterialAssign message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> MarkMaterialAsScrapAsync(DCMaterialMarkedAsScrap message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> MaterialStateOperations(DCMeasData message);
  }
}
