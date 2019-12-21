using PE.DbEntity.Models;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Furnace;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class FurnaceService : BaseService, IFurnaceService
  {
    public IEnumerable<VM_Furnace> GetMaterialInFurnace()
    {
      using(PEContext ctx = new PEContext())
      {
        return ctx.V_MaterialsInFurnace.ToList().Select(x => new VM_Furnace(x)).OrderBy(x=>x.Sorting);
      }
    }

    public VM_RawMaterialOverview GetMaterialDetails(ModelStateDictionary modelState, long RawMaterialId)
    {
      VM_RawMaterialOverview result = null;

      if (RawMaterialId <= 0)
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
        MVHRawMaterial material = ctx.MVHRawMaterials
          //.Include(ext => ext)
          .Where(x => x.RawMaterialId == RawMaterialId)
          .Include(i => i.MVHRawMaterial1)
          .Single();

        result = new VM_RawMaterialOverview(material);
      }

      return result;
    }

    public VM_WorkOrderOverview GetWorkOrderDetails(ModelStateDictionary modelState, long workOrderId)
    {
      VM_WorkOrderOverview result = null;

      if (workOrderId <= 0)
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
        PRMWorkOrder workOrder = ctx.PRMWorkOrders
          .Include(ext => ext.PRMWorkOrdersEXT)
          .Include(i => i.PRMMaterials)
          .Include(i => i.PRMMaterialCatalogue)
          .Include(i => i.PRMMaterialCatalogue.PRMSteelgrade)
          .Where(x => x.WorkOrderId == workOrderId)
          .Single();

        result = new VM_WorkOrderOverview(workOrder);
      }

      return result;
    }
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
                  .Include(i => i.PRMHeatChemAnalysis)
                  .Include(i => i.PRMHeatSupplier)
                  .Include(i => i.PRMMaterialCatalogue)
                  .Include(i => i.PRMMaterialCatalogue.PRMSteelgrade)
                  //.Include(i => i.PRMSteelgrade)
                  .Where(x => x.HeatId == heatId)
                  .Single();

        result = new VM_HeatOverview(heat);
      }

      return result;
    }
  }
}
