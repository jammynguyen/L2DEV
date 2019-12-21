using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Products;
using PE.HMIWWW.ViewModel.Module.Lite.Tracking;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using System.Data.Entity;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class TrackingService : BaseService, ITrackingService
  {
    public DataSourceResult GetTrackingList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.V_RawMaterialOverview.Where(x => x.DimRawMaterialStatusKey < 8)
          .ToDataSourceResult<V_RawMaterialOverview, VM_TrackingOverview>(request, modelState, x => new VM_TrackingOverview(x));
      }

      return result;
    }

    public VM_TrackingOverview GetTrackingDetails(ModelStateDictionary modelState, long dimRawMaterialKey, long? workOrderId, long? heatId)
    {
      VM_TrackingOverview result = null;

      if (dimRawMaterialKey <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return result;
      }

      using (PEContext ctx = new PEContext())
      {
        V_RawMaterialOverview data = ctx.V_RawMaterialOverview
          .Where(x => x.DimRawMaterialKey == dimRawMaterialKey)
          .Single();

        PRMWorkOrder WOdata = ctx.PRMWorkOrders
         .Include(i => i.PRMMaterialCatalogue)
         .Where(x => x.WorkOrderId == workOrderId)
         .SingleOrDefault();

        PRMHeat Hdata = ctx.PRMHeats
          .Include(i => i.PRMHeatSupplier)
          .Include(i => i.PRMMaterialCatalogue)
          .Include(i => i.PRMMaterialCatalogue.PRMSteelgrade)
          .Where(x => x.HeatId == heatId)
          .SingleOrDefault();

        result = new VM_TrackingOverview(data, WOdata, Hdata);
      }

      return result;
    }
  }
}
