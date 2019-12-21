using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IRawMaterialService
  {
    DataSourceResult GetRawMaterialSearchList(ModelStateDictionary modelState, DataSourceRequest request);
    VM_RawMaterialOverview GetRawMaterialById(long? materialId);

    VM_RawMaterialOverview GetRawMaterialDetails(ModelStateDictionary modelState, long MaterialId);
    VM_RawMaterialMeasurements GetMeasurementDetails(ModelStateDictionary modelState, long MaterialId);
    VM_RawMaterialHistory GetHistoryDetails(ModelStateDictionary modelState, long rawMaterialStepId);
    VM_L3MaterialData GetL3MaterialData(ModelStateDictionary modelState, long measurementId);
    DataSourceResult GetNotAssignmedL3Materials(ModelStateDictionary modelState, DataSourceRequest request);
    Task<VM_RawMaterialOverview> AssignMaterials(ModelStateDictionary modelState, long rawMaterialId, long l3MaterialId);
    Task<VM_RawMaterialOverview> UnassignMaterial(ModelStateDictionary modelState, long rawMaterialId);
    Task<VM_RawMaterialOverview> MarkMaterialAsScrap(ModelStateDictionary modelState, long rawMaterialId);
    DataSourceResult GetMeasurmentsByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request, long RawMaterialId);
    DataSourceResult GetHistoryByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request, long RawMaterialId);

  }
}
