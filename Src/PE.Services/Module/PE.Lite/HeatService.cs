using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.Core;
using PE.HMIWWW.Core.Communication;
using SMF.Core.DC;
using PE.DTO.Internal.ProdManager;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class HeatService : BaseService, IHeatService
  {
    public VM_HeatOverview GetHeatDetails(ModelStateDictionary modelState, long heatId)
    {
      VM_HeatOverview result = null;

      if (heatId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      using (PEContext ctx = new PEContext())
      {
        PRMHeat heat = ctx.PRMHeats
                  .Include(i => i.PRMHeatsEXT)
                  .Include(i => i.PRMHeatSupplier)
                  //.Include(i => i.PRMSteelgrade.PRMSteelgradeChemicalComposition)
                  .Include(i => i.PRMMaterials)
                  .Include(i => i.PRMHeatChemAnalysis)
                  .Include(i => i.PRMMaterialCatalogue)
                  .Include(i => i.PRMMaterialCatalogue.PRMSteelgrade)
                  .Include(i => i.PRMMaterialCatalogue.PRMSteelgrade.PRMSteelgradeChemicalComposition)
                  .Where(x => x.HeatId == heatId)
                  .Single();

        result = new VM_HeatOverview(heat);
      }

      return result;
    }

    public DataSourceResult GetHeatOverviewList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {

        result = ctx.V_Heats.ToDataSourceResult<V_Heats, VM_HeatSummary>(request, modelState, data => new VM_HeatSummary(data));
      }

      return result;
    }

    public DataSourceResult GetMaterialsListByHeatId(ModelStateDictionary modelState, DataSourceRequest request, long heatId)
    {
      using (PEContext ctx = new PEContext())
      {
        IQueryable<PRMMaterial> materialsList = ctx.PRMMaterials.Include(i => i.PRMWorkOrder)?.Where(x => x.FKHeatId == heatId).AsQueryable();

        return materialsList.ToDataSourceResult<PRMMaterial, VM_Material>(request, modelState, data => new VM_Material(data));
      }
    }

    public DataSourceResult GetWorkOrderListByHeatId(ModelStateDictionary modelState, DataSourceRequest request, long heatId)
    {
      using (PEContext ctx = new PEContext())
      {
        IQueryable<PRMMaterial> materialsInHeat = ctx.PRMMaterials.Where(x => x.FKHeatId == heatId).AsQueryable();
        IEnumerable<long> workOrderIds = materialsInHeat.Select(x => x.FKWorkOrderId).Distinct().ToList();
        List<PRMWorkOrder> workOrdersList = ctx.PRMWorkOrders.Where(x => x.WorkOrderId == workOrderIds.FirstOrDefault()).ToList();
        //foreach(PRMWorkOrder wo in workOrderList)
        //{
        //  wo.PRMMaterials
        //}
        //workOrdersList.FirstOrDefault().PRMMaterials = ctx.PRMMaterials.Where(x => x.FKWorkOrderId == workOrderIds.FirstOrDefault()).FirstOrDefault();

        return workOrdersList.ToDataSourceResult<PRMWorkOrder, VM_WorkOrderOverview>(request, modelState, data => new VM_WorkOrderOverview(data.WorkOrderId, data.WorkOrderName, data.CreatedTs, data.TargetOrderWeight, data.WorkOrderStatus));
      }
    }

    public async Task<VM_Base> CreateHeat(ModelStateDictionary modelState, VM_Heat heat)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref heat);

      DCHeat dCHeat = new DCHeat()
      {
        HeatName = heat.HeatName,
        FKMaterialCatalogueId = await GetMaterialCatalogueByName(heat.FKMaterialCatalogueId),
        FKHeatSupplierId = heat.FKHeatSupplierId,
        HeatWeightRef = heat.HeatWeightRef,
        IsDummy = heat.IsDummy
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendHeatAsync(dCHeat);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public IList<PRMHeatSupplier> GetSupplierList()
    {
      List<PRMHeatSupplier> result = new List<PRMHeatSupplier>();
      using (PEContext ctx = new PEContext())
      {

        result = ctx.PRMHeatSuppliers.ToList();
      }
      return result;
    }

    public IList<PRMSteelgrade> GetSteelgradeList()
    {
      List<PRMSteelgrade> result = new List<PRMSteelgrade>();
      using (PEContext ctx = new PEContext())
      {

        result = ctx.PRMSteelgrades.ToList();
      }
      return result;
    }

    public IList<PRMMaterialCatalogue> GetMaterialList()
    {
      List<PRMMaterialCatalogue> result = new List<PRMMaterialCatalogue>();
      using (PEContext ctx = new PEContext())
      {

        result = ctx.PRMMaterialCatalogues.ToList();
      }
      return result;
    }
    public IList<VM_HeatSummary> GetHeatsByAnyFeaure(string text)
    {
      IList<VM_HeatSummary> result = new List<VM_HeatSummary>();
      using (PEContext ctx = new PEContext())
      {
        if (!string.IsNullOrEmpty(text))
        {
          result = ctx.V_Heats.Where(p => p.HeatName.Contains(text) || p.SteelgradeName.Contains(text) || p.MaterialCatalogueName.Contains(text) || p.SupplierName.Contains(text)).AsEnumerable()
            .Select(heat => new VM_HeatSummary(heat)).ToList();
        }
      }

      return result;
    }
    public async Task<long> GetMaterialCatalogueByName(string materialCatalogueName)
    {
      using (PEContext ctx = new PEContext())
      {
        return await ctx.PRMMaterialCatalogues.Where(x => x.MaterialCatalogueName.Equals(materialCatalogueName)).Select(x => x.MaterialCatalogueId).FirstAsync();
      }
    }
  }
}
