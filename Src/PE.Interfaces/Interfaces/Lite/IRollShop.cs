using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.RollShop;

using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.Interfaces.Lite
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IRollShop : IBaseModule
  {
    /////////////
    ///ROLLS MANAGEMENT
    /////////////
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertRollAsync(DCRollData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateRollAsync(DCRollData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ScrapRollAsync(DCRollData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteRollAsync(DCRollData message);


    /////////////
    ///RollType
    /////////////
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertRollTypeAsync(DCRollTypeData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateRollTypeAsync(DCRollTypeData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteRollTypeAsync(DCRollTypeData message);


    /////////////
    ///GrooveType
    /////////////
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertGrooveTemplateAsync(DCGrooveTemplateData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateGrooveTemplateAsync(DCGrooveTemplateData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteGrooveTemplateAsync(DCGrooveTemplateData message);

    /////////////
    ///RollSet
    /////////////
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertRollSetAsync(DCRollSetData data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssembleRollSetAsync(DCRollSetData data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateRollSetStatusAsync(DCRollSetData data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ConfirmRollSetStatusAsync(DCRollSetData data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DisassembleRollSetAsync(DCRollSetData data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteRollSetAsync(DCRollSetData data);

    /////////////
    ///CassetteType
    /////////////
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertCassetteTypeAsync(DCCassetteTypeData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateCassetteTypeAsync(DCCassetteTypeData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteCassetteTypeAsync(DCCassetteTypeData message);

    /////////////
    ///Cassette
    /////////////
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertCassetteAsync(DCCassetteData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateCassetteAsync(DCCassetteData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteCassetteAsync(DCCassetteData message);


    ////////////
    ///RollChange
    ////////////
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RollChangeActionAsync(DCRollChangeOperationData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RollSetToCassetteAction(DCRollSetToCassetteAction message);

    ////////////
    ///GrindingTurning
    ////////////
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateGroovesToRollSetAsync(DCRollSetGrooveSetup message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CancelRollSetStatusAsync(DCRollSetData message);


    ////////////
    ///ActualStandConfiguration
    ////////////
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateGroovesStatusesAsync(DCRollSetGrooveSetup data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateStandConfigurationAsync(DCStandConfigurationData data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AccumulateRollsUsageAsync(DCRollsAccu message);
  }
}
