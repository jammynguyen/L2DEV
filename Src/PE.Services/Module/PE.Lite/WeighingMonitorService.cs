using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.WeighingMonitor;

namespace PE.HMIWWW.Services.Module
{
  public class WeighingMonitorService : BaseService, IWeighingMonitorService
  {
    public VM_WeighingOverview GetWeighingMonitorOverview(ModelStateDictionary modelState, DataSourceRequest request)
    {
      using (PEContext ctx = new PEContext())
      {
        //IQueryable<PRMWorkOrder> workOrderList = ctx.PRMWorkOrders.AsQueryable();

        List<V_RawMaterialOverview> rawMaterialsBeforeWeightList = ctx.V_RawMaterialOverview.Where(x => x.RawMaterialStatus <= 7).OrderByDescending(o => o.Sorting).AsQueryable().Take(2).ToList();

        List<V_RawMaterialOverview> rawMaterialsAfterWeightList = ctx.V_RawMaterialOverview.Where(x => x.RawMaterialStatus >= 8 && x.Asset29Weight != null).OrderBy(o => o.Sorting).AsQueryable().Skip(1).Take(2).ToList();

        V_RawMaterialOverview rawMaterialOnWeight = ctx.V_RawMaterialOverview.Where(x => x.Asset29Weight != null).OrderBy(o => o.Sorting).First();

        VM_WeighingOverview weighingOverview = new VM_WeighingOverview(rawMaterialsBeforeWeightList, rawMaterialOnWeight, rawMaterialsAfterWeightList);

        return new VM_WeighingOverview();
      }
    }

    public DataSourceResult GetRawMaterialsBeforeWeightList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.V_RawMaterialOverview
                        .Where(x => x.RawMaterialStatus <= 7)
                        .OrderByDescending(o => o.Sorting)
                        .Take(4)
                        .ToDataSourceResult<V_RawMaterialOverview, VM_RawMaterialOverview>(request, modelState, x => new VM_RawMaterialOverview(x));
      }

      return result;
    }

    public DataSourceResult GetRawMaterialsAfterWeightList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.V_RawMaterialOverview
                        .Where(x => x.RawMaterialStatus >= 8 && x.Asset29Weight != null)
                        .OrderBy(o => o.Sorting)
                        .Skip(1)
                        .Take(4)
                        .ToDataSourceResult<V_RawMaterialOverview, VM_RawMaterialOverview>(request, modelState, x => new VM_RawMaterialOverview(x));
      }

      return result;
    }

    public VM_RawMaterialOverview GetRawMaterialOnWeight(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      VM_RawMaterialOverview result = null;

      using (PEContext ctx = new PEContext())
      {
        V_RawMaterialOverview rawMaterial = ctx.V_RawMaterialOverview
                                                    .Where(x => x.Asset29Weight != null)
                                                    .OrderBy(o => o.Sorting)
                                                    .First();
        result = new VM_RawMaterialOverview(rawMaterial);
      }

      return result;
    }
  }
}
