using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.Quality;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.Interfaces.Lite
{

  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IQuality : IBaseModule
  {
    #region Defect Catalogue
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDefectCatalogueAsync(DCDefectCatalogue defectCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddDefectCatalogueAsync(DCDefectCatalogue defectCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteDefectCatalogueAsync(DCDefectCatalogue defectCatalogue);
    #endregion

    #region Defect assignment

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssignQualityAsync(DCQualityAssignment message);

    #endregion

  }
}
