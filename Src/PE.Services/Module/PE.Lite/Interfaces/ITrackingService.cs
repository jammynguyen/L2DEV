using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Products;
using PE.HMIWWW.ViewModel.Module.Lite.Tracking;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface ITrackingService
  {
    DataSourceResult GetTrackingList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    VM_TrackingOverview GetTrackingDetails(ModelStateDictionary modelState, long dimRawMaterialKey, long? workOrderId, long? heatId);
  }
}
