using PE.DTO.Internal.Quality;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace PE.QTY.Quality.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, PE.Interfaces.Lite.IQuality
  {
    #region ctor
    public ExternalAdapter() : base(typeof(PE.Interfaces.Lite.IQuality)) { }
    #endregion

    #region HMI
    public async Task<DataContractBase> UpdateDefectCatalogueAsync(DCDefectCatalogue dc)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateDefectCatalogueAsync, dc);
    }
    public async Task<DataContractBase> AddDefectCatalogueAsync(DCDefectCatalogue dc)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.AddDefectCatalogueAsync, dc);
    }

    public async Task<DataContractBase> DeleteDefectCatalogueAsync(DCDefectCatalogue dc)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.DeleteDefectCatalogueAsync, dc);
    }

    #endregion

    #region General functions
    public async Task<DataContractBase> AssignQualityAsync(DCQualityAssignment dc)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.AssignQualityAsync, dc);
    }

    #endregion

  }
}
