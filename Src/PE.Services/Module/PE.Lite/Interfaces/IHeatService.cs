using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IHeatService
  {
    VM_HeatOverview GetHeatDetails(ModelStateDictionary modelState, long heatId);
    DataSourceResult GetHeatOverviewList(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetMaterialsListByHeatId(ModelStateDictionary modelState, DataSourceRequest request, long heatId);
    DataSourceResult GetWorkOrderListByHeatId(ModelStateDictionary modelState, DataSourceRequest request, long heatId);
    Task<VM_Base> CreateHeat(ModelStateDictionary modelState, VM_Heat heat);
    IList<PRMHeatSupplier> GetSupplierList();
    IList<PRMSteelgrade> GetSteelgradeList();
    IList<PRMMaterialCatalogue> GetMaterialList();
    IList<VM_HeatSummary> GetHeatsByAnyFeaure(string text);
  }
}
