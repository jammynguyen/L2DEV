using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
    public interface IWorkOrderService
    {
        DataSourceResult GetWorkOrderOverviewList(ModelStateDictionary modelState, DataSourceRequest request);
        VM_WorkOrderOverview GetWorkOrderDetails(ModelStateDictionary modelState, long workOrderId);
        DataSourceResult GetMaterialsListByWorkOrderId(ModelStateDictionary modelState, DataSourceRequest request, long workOrderId);
        DataSourceResult GetNoScheduledWorkOrderList(ModelStateDictionary modelState, DataSourceRequest request);
        Task<VM_Base> CreateWorkOrder(ModelStateDictionary modelState, VM_WorkOrder workOrder);
        Task<VM_Base> EditWorkOrder(ModelStateDictionary modelState, VM_WorkOrder workOrder);

        IList<PRMHeat> GetHeatList();
        IList<PRMProductCatalogue> GetProductList();
        IList<PRMReheatingGroup> GetReheatingList();
        IList<PRMCustomer> GetCustomerList();
        IList<PRMMaterialCatalogue> GetMaterialList();
        Task<VM_Base> DeleteWorkOrder(ModelStateDictionary modelState, VM_WorkOrderOverview workOrder);
        Task<VM_WorkOrder> GetWorkOrder(long? id);
        Task<long?> GetWorkOrderIdByMaterialId(long id);
        Task<VM_ProductCatalogue> GetProductCatalogueDetails(long productCatalogId);
        Task<double> GetMaterialWeightFromMaterialCatalogue(string heatName);
    
  }
}
