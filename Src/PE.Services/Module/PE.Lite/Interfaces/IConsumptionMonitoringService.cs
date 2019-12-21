using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.ConsumptionMonitoring;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IConsumptionMonitoringService
  {
    DataSourceResult GetFeaturesList(ModelStateDictionary modelState, DataSourceRequest request);

    VM_Feature GetFeatureDetails(ModelStateDictionary modelState, long featureId);

    DataSourceResult GetMeasurementData(ModelStateDictionary modelState, DataSourceRequest request, long featureId, DateTime dateFrom, DateTime dateTo);
  }
}
