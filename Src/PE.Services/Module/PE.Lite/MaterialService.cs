using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class MaterialService : BaseService, IMaterialService
  {
    public VM_MaterialOverview GetMaterialById(long? materialId)
    {
      using (PEContext ctx = new PEContext())
      {
        PRMMaterial material = ctx.PRMMaterials.Where(x => x.MaterialId == materialId).SingleOrDefault();

        if (material != null)
        {
          return new VM_MaterialOverview(material);
        }
        else
        {
          return null;
        }
      }
    }

    public DataSourceResult GetMaterialSearchList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      using (PEContext ctx = new PEContext())
      {
        IQueryable<V_MaterialOverview> workOrderList = ctx.V_MaterialOverview.AsQueryable();
        return workOrderList.ToDataSourceResult<V_MaterialOverview, VM_MaterialOverview>(request, modelState, data => new VM_MaterialOverview(data));
      }
    }

    public VM_MaterialOverview GetMaterialDetails(ModelStateDictionary modelState, long MaterialId)
    {
      VM_MaterialOverview result = null;

      if (MaterialId <= 0)
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
        PRMMaterial material = ctx.PRMMaterials
          //.Include(ext => ext)
          .Where(x => x.MaterialId == MaterialId)
          .Include(i => i.PRMHeat)
          .Include(i => i.PRMWorkOrder)
          .Include(i => i.PRMWorkOrder.PRMMaterialCatalogue)
          .Include(i => i.PRMWorkOrder.PRMMaterialCatalogue.PRMSteelgrade)
          .Include(i => i.PRMHeat)
          .Include(i => i.PRMHeat.PRMMaterialCatalogue)
          .Include(i => i.PRMHeat.PRMMaterialCatalogue.PRMSteelgrade)
          .Single();

        result = new VM_MaterialOverview(material);
      }

      return result;
    }
  }
}
