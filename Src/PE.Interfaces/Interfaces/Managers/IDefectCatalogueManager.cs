using PE.DTO.Internal.Quality;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers
{
  public interface IDefectCatalogueManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDefectCatalogueAsync(DCDefectCatalogue defectCatalogue);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddDefectCatalogueAsync(DCDefectCatalogue defectCatalogue);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteDefectCatalogueAsync(DCDefectCatalogue defectCatalogue);
  }
}
