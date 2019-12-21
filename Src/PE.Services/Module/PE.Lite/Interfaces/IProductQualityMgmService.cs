using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Maintenance;
using PE.HMIWWW.ViewModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IProductQualityMgmService
  {
    DataSourceResult GetProductHistoryList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    //Task<VM_Base> UpdateProductQuality(ModelStateDictionary modelState, VM_Equipment cat);

    Task<VM_Base> AssignQualityAsync(ModelStateDictionary modelState, long productId, short quality, List<long> defectIds);
    DataSourceResult GetDefectsList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);

    DataSourceResult GetProductDefectsList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, long productId);
    Dictionary<int, string> GetProductQualityList();
  }
}
