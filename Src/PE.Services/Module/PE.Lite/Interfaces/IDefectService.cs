using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IDefectService
  {
    VM_DefectCatalogue GetDelayCatalogue(long id);
    DataSourceResult GetDefectCatalogueList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    Task<VM_Base> AddDefectCatalogue(ModelStateDictionary modelState, VM_DefectCatalogue defectCatalogue);
    Task<VM_Base> UpdateDefectCatalogue(ModelStateDictionary modelState, VM_DefectCatalogue defectCatalogue);
    Task<VM_Base> DeleteDefectCatalogueAsync(ModelStateDictionary modelState, VM_DefectCatalogue defectCatalogue);
    IList<VM_DefectCatalogueCategory> GetDefectCategoryList();
  }
}
