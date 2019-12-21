using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.WeighingMonitor;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IWeighingMonitorService
  {
    VM_WeighingOverview GetWeighingMonitorOverview(ModelStateDictionary modelState, DataSourceRequest request);
    VM_RawMaterialOverview GetRawMaterialOnWeight(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetRawMaterialsBeforeWeightList(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetRawMaterialsAfterWeightList(ModelStateDictionary modelState, DataSourceRequest request);

  }
}
