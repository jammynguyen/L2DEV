using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using PE.DbEntity.Enums;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.UnitConverter;
using SMF.Core.DC;
using SMF.Module.Core;
using PE.HMIWWW.Core.Communication;
using PE.DTO.Internal.ProdManager;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
    public class WorkOrderService : BaseService, IWorkOrderService
    {

        public DataSourceResult GetWorkOrderOverviewList(ModelStateDictionary modelState, DataSourceRequest request)
        {
            using (PEContext ctx = new PEContext())
            {
                IQueryable<V_WorkOrderSummary> workOrderList = ctx.V_WorkOrderSummary.AsQueryable();
                return workOrderList.ToDataSourceResult<V_WorkOrderSummary, VM_WorkOrderSummary>(request, modelState, data => new VM_WorkOrderSummary(data));
            }
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
                  .Include(i => i.PRMMaterialCatalogue.PRMSteelgrade)
                  .Include(i => i.PRMMaterialCatalogue.PRMMaterialCatalogueType)
                  .Include(i => i.PRMProductCatalogue)
                  .Include(i => i.PRMProductCatalogue.PRMShape)
                  .Include(i => i.PRMMaterialCatalogue.PRMShape)
                  .Include(i => i.PRMMaterialCatalogue.PRMSteelgrade)
                  .Include(i => i.PRMMaterialCatalogue.PRMSteelgrade.PRMSteelgradeChemicalComposition)
                  .Include(i => i.PRMProductCatalogue.PRMProductCatalogueType)
                  .Include(i => i.PRMProductCatalogue.PRMSteelgrade.PRMSteelgradeChemicalComposition)
                  .Where(x => x.WorkOrderId == workOrderId)
                  .Single();

                  workOrder.PRMMaterials = ctx.PRMMaterials.Where(x => x.FKWorkOrderId == workOrderId).Include(x => x.PRMHeat).ToList();

                double? metallicYield = ctx.V_WorkOrderSummary.Where(x => x.WorkOrderId == workOrderId).Select(x => x.MetallicYield).FirstOrDefault();
                double completion = ctx.V_WorkOrderSummary.Where(x => x.WorkOrderId == workOrderId).Select(x => x.Completion).FirstOrDefault();
                result = new VM_WorkOrderOverview(workOrder, metallicYield, completion);
            }

            return result;
        }

        public DataSourceResult GetMaterialsListByWorkOrderId(ModelStateDictionary modelState, DataSourceRequest request, long workOrderId)
        {
            using (PEContext ctx = new PEContext())
            {
                //List<long> List = ctx.PRMMaterials.Where(x => x.FKWorkOrderId == workOrderId).Select(s => s.MaterialId).ToList();
                //V_L1L3MaterialAssignment assignments = ctx.V_L1L3MaterialAssignment.Where(i => List.Contains(i.MaterialId)).FirstOrDefault();
                IQueryable<V_L1L3MaterialAssignment> materialsList = ctx.V_L1L3MaterialAssignment.Where(x => x.FKWorkOrderId == workOrderId).AsQueryable();
                return materialsList.ToDataSourceResult<V_L1L3MaterialAssignment, VM_L1L3MaterialAssignment>(request, modelState, data => new VM_L1L3MaterialAssignment(data));
            }
        }

        public DataSourceResult GetNoScheduledWorkOrderList(ModelStateDictionary modelState, DataSourceRequest request)
        {
            using (PEContext ctx = new PEContext())
            {
                IQueryable<PRMWorkOrder> workOrderList = ctx.PRMWorkOrders.Where(x => x.WorkOrderStatus != OrderStatus.ENUM_Scheduled)
                                                                          .Include(i => i.PRMMaterialCatalogue)
                                                                          .AsQueryable();
                workOrderList.Select(x => x.PRMMaterialCatalogue).Include(i => i.PRMSteelgrade).AsQueryable();
                                                                          

                return workOrderList.ToDataSourceResult<PRMWorkOrder, VM_WorkOrderOverview>(request, modelState, data => new VM_WorkOrderOverview(data));
            }
        }

        public async Task<VM_Base> CreateWorkOrder(ModelStateDictionary modelState, VM_WorkOrder workOrder)
        {
            VM_Base result = new VM_Base();

            if (!modelState.IsValid)
            {
                return result;
            }

            UnitConverterHelper.ConvertToSi(ref workOrder);

            DCWorkOrder dcWorkOrder = new DCWorkOrder()
            {
                WorkOrderName = workOrder.WorkOrderName,
                IsTestOrder = workOrder.IsTestOrder,
                TargetOrderWeight = workOrder.TargetOrderWeight,
                TargetOrderWeightMin = workOrder.TargetOrderWeightMin,
                TargetOrderWeightMax = workOrder.TargetOrderWeightMax,
                CreatedInL3 = DateTime.Now,//workOrder.CreatedInL3,
                ToBeCompletedBefore = workOrder.ToBeCompletedBefore,
                NextAggregate = workOrder.NextAggregate,
                OperationCode = workOrder.OperationCode,
                QualityPolicy = workOrder.QualityPolicy,
                ExtraLabelInformation = workOrder.ExtraLabelInformation,
                ExternalSteelgradeCode = workOrder.ExternalSteelgradeCode,
                FKHeatIdRef = await GetHeatByName(workOrder.FKHeatIdRef),
                FKProductCatalogueId = workOrder.FKProductCatalogueId,
                FKMaterialCatalogueId = workOrder.FKMaterialCatalogueId,
                FKCustomerId = workOrder.FKCustomerId,
                FKReheatingGroupId = workOrder.FKReheatingGroupId,
                MaterialsNumber = workOrder.MaterialsNumber.Value,
                WorkOrderStatus = workOrder.WorkOrderStatus
            };

            //request data from module
            SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendWorkOrderAsync(dcWorkOrder);

            //handle warning information
            HandleWarnings(sendOfficeResult, ref modelState);

            if (workOrder.MaterialsNumber.HasValue && workOrder.FKHeatIdRef != null)
            {
                long? heatID = await GetHeatByName(workOrder.FKHeatIdRef);
                DCMaterial dcMaterial = new DCMaterial()
                {
                    FKWorkOrderIdRef = workOrder.WorkOrderName,
                    Weight = await GetCatalogMaterialWeight(heatID.Value),
                    MaterialsNumber = workOrder.MaterialsNumber,
                    FKHeatId = heatID.Value
                };
                SendOfficeResult<DataContractBase> sendOfficeResultForMaterial = await HmiSendOffice.SendMaterialAsync(dcMaterial);
                HandleWarnings(sendOfficeResultForMaterial, ref modelState);
            }
            //return view model
            return result;
        }

        public async Task<VM_Base> EditWorkOrder(ModelStateDictionary modelState, VM_WorkOrder workOrder)
        {
            VM_Base result = new VM_Base();

            if (!modelState.IsValid)
            {
                return result;
            }

            UnitConverterHelper.ConvertToSi(ref workOrder);

            DCWorkOrder dcWorkOrder = new DCWorkOrder()
            {
                WorkOrderId = workOrder.WorkOrderId,
                WorkOrderName = workOrder.WorkOrderName,
                IsTestOrder = workOrder.IsTestOrder,
                TargetOrderWeight = workOrder.TargetOrderWeight,
                TargetOrderWeightMin = workOrder.TargetOrderWeightMin,
                TargetOrderWeightMax = workOrder.TargetOrderWeightMax,
                CreatedInL3 = workOrder.CreatedInL3,
                ToBeCompletedBefore = workOrder.ToBeCompletedBefore,
                NextAggregate = workOrder.NextAggregate,
                OperationCode = workOrder.OperationCode,
                QualityPolicy = workOrder.QualityPolicy,
                ExtraLabelInformation = workOrder.ExtraLabelInformation,
                ExternalSteelgradeCode = workOrder.ExternalSteelgradeCode,
                FKHeatIdRef = await GetHeatByName(workOrder.FKHeatIdRef),
                FKProductCatalogueId = workOrder.FKProductCatalogueId,
                FKMaterialCatalogueId = workOrder.FKMaterialCatalogueId,
                FKCustomerId = workOrder.FKCustomerId,
                FKReheatingGroupId = workOrder.FKReheatingGroupId,
                MaterialsNumber = workOrder.MaterialsNumber.Value,
                WorkOrderStatus = workOrder.WorkOrderStatus
            };

            SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendWorkOrderAsync(dcWorkOrder);

            HandleWarnings(sendOfficeResult, ref modelState);

            if (workOrder.MaterialsNumber.HasValue && workOrder.FKHeatIdRef != null)
            {
                long? heatID = await GetHeatByName(workOrder.FKHeatIdRef);
                DCMaterial dcMaterial = new DCMaterial()
                {
                    //MaterialId is used only to pass number of materials assigned to work order
                    MaterialId = Convert.ToInt64(workOrder.MaterialsNumber),
                    FKWorkOrderIdRef = workOrder.WorkOrderName,
                    MaterialsNumber = workOrder.MaterialsNumber,
                    FKHeatId = heatID.Value,
                    Weight = await GetCatalogMaterialWeight(heatID.Value),
                };
                SendOfficeResult<DataContractBase> sendOfficeResultForMaterial = await HmiSendOffice.SendMaterialAsync(dcMaterial);
                HandleWarnings(sendOfficeResultForMaterial, ref modelState);
            }

            return result;
        }

        public async Task<VM_WorkOrder> GetWorkOrder(long? id)
        {
            VM_WorkOrder result = null;

            using (PEContext ctx = new PEContext())
            {
                PRMWorkOrder workOrder = await ctx.PRMWorkOrders
                //.Include(x => x.PRMMaterials)
                .Include(x => x.PRMMaterialCatalogue)
                .SingleOrDefaultAsync(x => x.WorkOrderId == id);
                
                workOrder.PRMMaterials = ctx.PRMMaterials.Where(x => x.FKWorkOrderId == id).Include(x => x.PRMHeat).ToList();
                result = workOrder == null ? null : new VM_WorkOrder(workOrder);
                result.MaterialsNumber = await ctx.PRMMaterials.Where(x => x.FKWorkOrderId == id).CountAsync();
            }

            return result;
        }

        public async Task<double> GetCatalogMaterialWeight(long heatId)
        {
          double MaterialWeightFromMaterialCatalogue;
          using (PEContext ctx = new PEContext())
          {
            MaterialWeightFromMaterialCatalogue = await ctx.PRMHeats.Where(x => x.HeatId == heatId).Include(x => x.PRMMaterialCatalogue).Select(x => x.PRMMaterialCatalogue.Weight).SingleOrDefaultAsync();
          }

          return MaterialWeightFromMaterialCatalogue;
        }

    public IList<PRMHeat> GetHeatList()
        {
            List<PRMHeat> result = new List<PRMHeat>();
            using (PEContext ctx = new PEContext())
            {
                PRMHeat emptyHeat = new PRMHeat
                {
                    HeatName = "",
                };
                result = ctx.PRMHeats.ToList();
                result.Add(emptyHeat);
            }
            return result;
        }

        public IList<PRMProductCatalogue> GetProductList()
        {
            List<PRMProductCatalogue> result = new List<PRMProductCatalogue>();
            using (PEContext ctx = new PEContext())
            {

                result = ctx.PRMProductCatalogues.ToList();
            }
            return result;
        }

        public IList<PRMReheatingGroup> GetReheatingList()
        {
            List<PRMReheatingGroup> result = new List<PRMReheatingGroup>();
            using (PEContext ctx = new PEContext())
            {

                result = ctx.PRMReheatingGroups.ToList();
            }
            return result;
        }

        public IList<PRMCustomer> GetCustomerList()
        {
            List<PRMCustomer> result = new List<PRMCustomer>();
            using (PEContext ctx = new PEContext())
            {

                result = ctx.PRMCustomers.ToList();
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

        public async Task<VM_Base> DeleteWorkOrder(ModelStateDictionary modelState, VM_WorkOrderOverview workOrder)
        {
            VM_Base result = new VM_Base();

            if (!modelState.IsValid)
                return result;

            UnitConverterHelper.ConvertToSi(ref workOrder);

            DCWorkOrder entryDataContract = new DCWorkOrder
            {
                WorkOrderName = workOrder.WorkOrderName
            };

            SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteWorkOrderAsync(entryDataContract);

            //handle warning information
            HandleWarnings(sendOfficeResult, ref modelState);

            //return view model
            return result;
        }

        public async Task<long?> GetHeatByName(string heatName)
        {
            long? id;
            if (heatName == null)
                id = null;
            else
            {
                using (PEContext ctx = new PEContext())
                {
                    id = await ctx.PRMHeats.Where(x => x.HeatName.Equals(heatName)).Select(x => x.HeatId).FirstAsync();
                }
            }
            return id;
        }

        public async Task<long?> GetWorkOrderIdByMaterialId(long id)
        {
            long? workOrderId = 0;
            using (PEContext ctx = new PEContext())
            {
                workOrderId = await ctx.PRMMaterials.Where(x => x.MaterialId == id).Select(x => x.FKWorkOrderId).FirstAsync();
            }
            return workOrderId;
        }
        public async Task<VM_ProductCatalogue> GetProductCatalogueDetails(long productCatalogId)
        {
          VM_ProductCatalogue result = null;
          using (PEContext ctx = new PEContext())
          {
            PRMProductCatalogue data = ctx.PRMProductCatalogues
              .Include(i => i.PRMShape)
              .Include(i => i.PRMProductCatalogueType)
              .Include(i => i.PRMSteelgrade)
              .Where(w => w.ProductCatalogueId == productCatalogId)
              .SingleOrDefault();

            result = new VM_ProductCatalogue(data);
          }
          return result;
        }
        public async Task<double> GetMaterialWeightFromMaterialCatalogue(string heatName)
        {
          VM_MaterialCatalogue matcat = new VM_MaterialCatalogue();
          using (PEContext ctx = new PEContext())
          {
            PRMHeat heat = ctx.PRMHeats
              .Include(i => i.PRMMaterialCatalogue)
              .Where(w => w.HeatName == heatName)
              .FirstOrDefault();
              
               matcat = new VM_MaterialCatalogue(heat.PRMMaterialCatalogue);
          }

          return matcat.Weight;
        }
  }
}
