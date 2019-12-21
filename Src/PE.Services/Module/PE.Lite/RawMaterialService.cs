using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using SMF.Module.Core;
using SMF.Core.DC;
using PE.HMIWWW.Core.Communication;
using PE.DTO.Internal.MVHistory;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class RawMaterialService : BaseService, IRawMaterialService
  {
    public VM_RawMaterialOverview GetRawMaterialById(long? materialId)
    {
      using (PEContext ctx = new PEContext())
      {
        MVHRawMaterial material = ctx.MVHRawMaterials.Where(x => x.RawMaterialId == materialId).Include(i => i.MVHRawMaterial1).SingleOrDefault();

        if (material != null)
        {
          return new VM_RawMaterialOverview(material);
        }
        else
        {
          return null;
        }
      }
    }

    public DataSourceResult GetRawMaterialSearchList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      using (PEContext ctx = new PEContext())
      {
        IQueryable<V_RawMaterialList> list = ctx.V_RawMaterialList.AsQueryable();

        return list.ToDataSourceResult<V_RawMaterialList, VM_RawMaterialList>(request, modelState, data => new VM_RawMaterialList(data));
      }
    }

    public DataSourceResult GetNotAssignmedL3Materials(ModelStateDictionary modelState, DataSourceRequest request)
    {
      using (PEContext ctx = new PEContext())
      {
        List<long?> attachedMaterials = ctx.MVHRawMaterials.Where(x => x.FKMaterialId != null).Select(x => x.FKMaterialId).ToList();
        IQueryable<PRMMaterial> notAttachedMaterials = ctx.PRMMaterials.Where(x => !attachedMaterials.Contains(x.MaterialId)).Include(x => x.PRMWorkOrder).AsQueryable();

        return notAttachedMaterials.ToDataSourceResult<PRMMaterial, VM_MaterialOverview>(request, modelState, data => new VM_MaterialOverview(data));
      }
    }

    public VM_RawMaterialOverview GetRawMaterialDetails(ModelStateDictionary modelState, long RawMaterialId)
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
          .Where(x => x.RawMaterialId == RawMaterialId)
          .Include(i => i.MVHRawMaterial1)
          .Single();

        //V_RawMaterialOverwiew rmf = ctx.V_RawMaterialOverwiew.Where(w => w.DimRawMaterialKey == RawMaterialId).Single();
        V_RawMaterialMeasurements measurements = ctx.V_RawMaterialMeasurements.Where(w => w.RawMaterialId == RawMaterialId).FirstOrDefault();
        V_RawMaterialHistory history = ctx.V_RawMaterialHistory.Where(w => w.RawMaterialId == RawMaterialId).FirstOrDefault();

        result = new VM_RawMaterialOverview(material);
      }

      return result;
    }

    public VM_RawMaterialMeasurements GetMeasurementDetails(ModelStateDictionary modelState, long measurementId)
    {
      VM_RawMaterialMeasurements result = null;

      if (measurementId <= 0)
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
        V_RawMaterialMeasurements data = ctx.V_RawMaterialMeasurements
          .Where(x => x.MeasurementId == measurementId)
          .FirstOrDefault();

        result = new VM_RawMaterialMeasurements(data);
      }

      return result;
    }

    public VM_RawMaterialHistory GetHistoryDetails(ModelStateDictionary modelState, long rawMaterialStepId)
    {
      VM_RawMaterialHistory result = null;

      if (rawMaterialStepId <= 0)
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
        V_RawMaterialHistory data = ctx.V_RawMaterialHistory
          .Where(x => x.RawMaterialStepId == rawMaterialStepId)
          .FirstOrDefault();

        result = new VM_RawMaterialHistory(data);
      }

      return result;
    }

    public VM_L3MaterialData GetL3MaterialData(ModelStateDictionary modelState, long rawMaterialId)
    {
      VM_L3MaterialData result = null;

      if (!modelState.IsValid)
      {
        return result;
      }

      using (PEContext ctx = new PEContext())
      {
        long? materialId = ctx.MVHRawMaterials.Where(y => y.RawMaterialId == rawMaterialId).Select(x => x.FKMaterialId).FirstOrDefault();
        if (materialId != null)
        {
          result = new VM_L3MaterialData(ctx.PRMMaterials.Where(x => x.MaterialId == materialId).Include(y => y.PRMWorkOrder).Include(y => y.PRMWorkOrder.PRMMaterialCatalogue).Include(y => y.PRMWorkOrder.PRMMaterialCatalogue.PRMSteelgrade).FirstOrDefault());
        }
        else
        {
          return result;
        }
      }

      return result;
    }

    public async Task<VM_RawMaterialOverview> AssignMaterials(ModelStateDictionary modelState, long rawMaterialId, long l3MaterialId)
    {

      VM_RawMaterialOverview result = null;

      if (rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      DCMaterialAssign dataToSend = new DCMaterialAssign()
      {
        RawMaterialId = rawMaterialId,
        L3MaterialId = l3MaterialId
      };


      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AssignMaterials(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);



      using (PEContext ctx = new PEContext())
      {
        MVHRawMaterial material = ctx.MVHRawMaterials
          //.Include(ext => ext)
          .Where(x => x.RawMaterialId == rawMaterialId)
          .Single();

        result = new VM_RawMaterialOverview(material, true);
      }

      return result;
    }

    public async Task<VM_RawMaterialOverview> UnassignMaterial(ModelStateDictionary modelState, long rawMaterialId)
    {

      VM_RawMaterialOverview result = null;

      if (rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      DCMaterialAssign dataToSend = new DCMaterialAssign()
      {
        RawMaterialId = rawMaterialId,
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UnassignMaterial(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);



      using (PEContext ctx = new PEContext())
      {
        MVHRawMaterial material = ctx.MVHRawMaterials
          //.Include(ext => ext)
          .Where(x => x.RawMaterialId == rawMaterialId)
          .Single();

        result = new VM_RawMaterialOverview(material, true);
      }

      return result;
    }

    public async Task<VM_RawMaterialOverview> MarkMaterialAsScrap(ModelStateDictionary modelState, long rawMaterialId)
    {
      VM_RawMaterialOverview result = null;

      if (rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      DCMaterialMarkedAsScrap dataToSend = new DCMaterialMarkedAsScrap()
      {
        RawMaterialId = rawMaterialId,
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.MarkMaterialAsScrap(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      using (PEContext ctx = new PEContext())
      {
        MVHRawMaterial material = ctx.MVHRawMaterials
          //.Include(ext => ext)
          .Where(x => x.RawMaterialId == rawMaterialId)
          .Single();

        result = new VM_RawMaterialOverview(material, false);
      }

      return result;
    }

    public DataSourceResult GetMeasurmentsByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request, long RawMaterialId)
    {
      using (PEContext ctx = new PEContext())
      {
        IQueryable<V_RawMaterialMeasurements> list = ctx.V_RawMaterialMeasurements.Where(x => x.RawMaterialId == RawMaterialId).AsQueryable();

        return list.ToDataSourceResult<V_RawMaterialMeasurements, VM_RawMaterialMeasurements>(request, modelState, data => new VM_RawMaterialMeasurements(data));
      }
    }

    public DataSourceResult GetHistoryByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request, long RawMaterialId)
    {
      using (PEContext ctx = new PEContext())
      {
        IQueryable<V_RawMaterialHistory> list = ctx.V_RawMaterialHistory.Where(x => x.RawMaterialId == RawMaterialId).AsQueryable();

        V_Assets asset = new V_Assets();

        return list.ToDataSourceResult<V_RawMaterialHistory, VM_RawMaterialHistory>(request, modelState, data => new VM_RawMaterialHistory(data));
      }
    }
  }
}
