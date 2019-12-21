using Ninject;
using Ninject.Parameters;
using PE.DTO.Internal.Quality;
using PE.Interfaces.Lite;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.Interfaces.Managers;
using PE.QTY.Managers;

namespace PE.QTY.Quality.Communication
{
  internal static class ExternalAdapterHandler
  {
		private static readonly IDefectCatalogueManager _defectCatalogueManager;
		private static readonly IQualityAssignment _qualityManager;

    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IDefectCatalogueManager>().To<DefectCatalogueManager>();
				_defectCatalogueManager = kernel.Get<IDefectCatalogueManager>(new ConstructorArgument("sendOffice", new SendOffice()));

        kernel.Bind<IQualityAssignment>().To<QualityAssignmentManager>();
				_qualityManager = kernel.Get<IQualityAssignment>(new ConstructorArgument("sendOffice", new SendOffice()));

      }
    }

		internal static async Task<DataContractBase> AddDefectCatalogueAsync(DCDefectCatalogue defectCatalogue)
		{
			return await _defectCatalogueManager.AddDefectCatalogueAsync(defectCatalogue);
		}

		internal static async Task<DataContractBase> UpdateDefectCatalogueAsync(DCDefectCatalogue defectCatalogue)
		{
			return await _defectCatalogueManager.UpdateDefectCatalogueAsync(defectCatalogue);
		}

		internal static async Task<DataContractBase> DeleteDefectCatalogueAsync(DCDefectCatalogue defectCatalogue)
		{
			return await _defectCatalogueManager.DeleteDefectCatalogueAsync(defectCatalogue);
		}
    internal static async Task<DataContractBase> AssignQualityAsync(DCQualityAssignment message)
    {
      return await _qualityManager.AssignQualityAsync(message);
    }
  }
}
