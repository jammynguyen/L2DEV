using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.ProdManager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PE.PRM.Handlers
{
    public sealed class WorkOrderHandler
    {
        public PRMWorkOrder CreateWorkOrder(PEContext ctx,
          bool isTest,
          double? targetOrderWeight,
          ICollection<PRMMaterial> materials,
          PRMHeat heat,
          PRMMaterialCatalogue materialCatalogue,
          long productCatalogue,
          PRMCustomer customer,
          DateTime? l3Created = null,
          string workOrderName = null,
          DateTime? toBeCompletedBefore = null,
          double? targetOrderWeightMin = null,
          double? targetOrderWeightMax = null,
          string qualityPolicy = null,
          string extraLabelInformation = null,
          long? reheatingGroupId = null,
          string nextAggregate = null,
          string operationCode = null,
          string externalSteelgradeCode = null
          )
        {
            if (ctx == null) { ctx = new PEContext(); }

            if (isTest)
            {
                workOrderName = workOrderName ?? "TestOrder" + DateTime.Now.ToString("yyyyMMddHHmmss");
                targetOrderWeight = targetOrderWeight ?? 1000;
            }
            else
            {
                if (workOrderName == null)
                    throw new Exception("Work order name must be set");
                if (targetOrderWeight == null)
                    throw new Exception("Order weight must be set");
            }

            PRMWorkOrder workOrder = new PRMWorkOrder()
            {
                WorkOrderStatus = DbEntity.Enums.OrderStatus.ENUM_NewOrIncomplete,
                IsTestOrder = isTest,
                CreatedTs = DateTime.Now,
                LastUpdateTs = DateTime.Now,
                CreatedInL3 = l3Created ?? DateTime.Now,
                WorkOrderName = workOrderName,
                ToBeCompletedBefore = toBeCompletedBefore ?? DateTime.Now,
                TargetOrderWeight = (double)targetOrderWeight,
                TargetOrderWeightMin = targetOrderWeightMin,
                TargetOrderWeightMax = targetOrderWeightMax,
                FKProductCatalogueId = productCatalogue,
                QualityPolicy = qualityPolicy,
                ExtraLabelInformation = extraLabelInformation,
                FKReheatingGroupId = reheatingGroupId,
                PRMCustomer = customer,
                NextAggregate = nextAggregate,
                OperationCode = operationCode,
                ExternalSteelgradeCode = externalSteelgradeCode,
                PRMMaterialCatalogue = materialCatalogue,
                PRMMaterials = materials,

                PRMWorkOrdersEXT = new PRMWorkOrdersEXT()
                {
                    CreatedTs = DateTime.Now,
                },
            };

            return workOrder;
        }



        ////////////////  /// <summary>
        ////////////////  /// Create new work order and workorder ext
        ////////////////  /// </summary>
        ////////////////  /// <param name="ctx"></param>
        ////////////////  /// <param name="productCatalogue"></param>
        ////////////////  /// <param name="workOrderName"></param>
        ////////////////  /// <param name="toBeReadyBefore"></param>
        ////////////////  /// <param name="targetOrderWeight"></param>
        ////////////////  /// <param name="targetOrderWeightMin"></param>
        ////////////////  /// <param name="targetOrderWeightMax"></param>
        ////////////////  /// <param name="qualityPolicy"></param>
        ////////////////  /// <param name="extraLabelInformation"></param>
        ////////////////  /// <param name="customerId"></param>
        ////////////////  /// <param name="reheatingGroup"></param>
        ////////////////  /// <returns></returns>
        ////////////////  public PRMWorkOrder CreateWorkOrder(PEContext ctx, long productCatalogue, PRMCustomer customer, long reheatingGroup, PRMMaterialCatalogue materialCatalogue, List<PRMMaterial> materials, PRMHeat heat, DCL3L2WorkOrderDefinition message)
        ////////////////{
        ////////////////  if (ctx == null) { ctx = new PEContext(); }

        ////////////////  PRMWorkOrder workOrder = new PRMWorkOrder()
        ////////////////  {
        ////////////////    WorkOrderStatus = DbEntity.Enums.OrderStatus.ENUM_NewOrIncomplete,
        ////////////////    CreatedTs = DateTime.Now,
        ////////////////    LastUpdateTs = DateTime.Now,
        ////////////////    CreatedInL3 = message.L3Created,
        ////////////////    WorkOrderName = message.WorkOrderName,
        ////////////////    ToBeCompletedBefore = message.ToBeReadyBefore,
        ////////////////    TargetOrderWeight = message.TargetOrderWeight,
        ////////////////    TargetOrderWeightMin = message.TargetOrderWeightMin,
        ////////////////    TargetOrderWeightMax = message.TargetOrderWeightMax,
        ////////////////    FKProductCatalogueId = productCatalogue,
        ////////////////    QualityPolicy = message.QualityPolicy,
        ////////////////    ExtraLabelInformation = message.ExtraLabelInformation,
        ////////////////    FKReheatingGroupId = reheatingGroup,
        ////////////////    PRMCustomer = customer,
        ////////////////    NextAggregate = message.NextAggregate,
        ////////////////    OperationCode = message.OperationCode,
        ////////////////    ExternalSteelgradeCode = message.ExternalSteelGradeCode,


        ////////////////    PRMWorkOrdersEXT = new PRMWorkOrdersEXT()
        ////////////////    {
        ////////////////      CreatedTs = DateTime.Now,
        ////////////////    },
        ////////////////    PRMMaterialCatalogue = materialCatalogue,
        ////////////////    PRMMaterials = materials,
        ////////////////    PRMHeat = heat

        ////////////////  };

        ////////////////  return workOrder;
        ////////////////}

        ////////////////public PRMWorkOrder CreateTestWorkOrder(PEContext ctx, long productCatalogue, PRMCustomer customer, long? reheatingGroup, PRMMaterialCatalogue materialCatalogue, List<PRMMaterial> materials, PRMHeat heat,  double? weight)
        ////////////////{
        ////////////////  return new PRMWorkOrder()
        ////////////////  {
        ////////////////    WorkOrderStatus = DbEntity.Enums.OrderStatus.ENUM_Scheduled,
        ////////////////    CreatedTs = DateTime.Now,
        ////////////////    LastUpdateTs = DateTime.Now,
        ////////////////    CreatedInL3 = DateTime.Now,
        ////////////////    WorkOrderName = "TestWorkOrder" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute,
        ////////////////    ToBeCompletedBefore = DateTime.Now,
        ////////////////    TargetOrderWeight = (double)weight,
        ////////////////    TargetOrderWeightMin = weight,
        ////////////////    TargetOrderWeightMax = weight,
        ////////////////    FKProductCatalogueId = productCatalogue,
        ////////////////    QualityPolicy = "",
        ////////////////    ExtraLabelInformation = "Test",
        ////////////////    FKReheatingGroupId = reheatingGroup,
        ////////////////    PRMCustomer = customer,
        ////////////////    NextAggregate = "",
        ////////////////    OperationCode = "Test order",
        ////////////////    ExternalSteelgradeCode = "Test",


        ////////////////    PRMWorkOrdersEXT = new PRMWorkOrdersEXT()
        ////////////////    {
        ////////////////      CreatedTs = DateTime.Now,
        ////////////////    },
        ////////////////    PRMMaterialCatalogue = materialCatalogue,
        ////////////////    PRMMaterials = materials,
        ////////////////    PRMHeat = heat,
        ////////////////IsTestOrder = true
        ////////////////  };
        ////////////////}

        /// <summary>
        /// Return whole work order, find by id with ext and materials included
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        public async Task<PRMWorkOrder> GetWorkOrderByIdAsync(PEContext ctx, long workOrderId)
        {
            if (ctx == null) { ctx = new PEContext(); }

            return await ctx.PRMWorkOrders
              .Include(x => x.PRMWorkOrdersEXT)
              .Include(x => x.PRMMaterials)
              .Include(x => x.PRMProductCatalogue)
              .Where(x => x.WorkOrderId == workOrderId).SingleAsync();

        }

        public void CreateWorkOrderByUser(PEContext ctx, DCWorkOrder dc)
        {
            if (ctx == null) { ctx = new PEContext(); }

            PRMHeat heat = ctx.PRMHeats.Where(x => x.HeatId ==  dc.FKHeatIdRef).Include(x => x.PRMMaterialCatalogue).Single();


            PRMWorkOrder workOrder = new PRMWorkOrder()
            {
                CreatedTs = DateTime.Now,
                LastUpdateTs = DateTime.Now,
                WorkOrderStatus = dc.WorkOrderStatus.HasValue ? dc.WorkOrderStatus.Value : OrderStatus.ENUM_NewOrIncomplete,
                WorkOrderName = dc.WorkOrderName,
                IsTestOrder = dc.IsTestOrder,
                TargetOrderWeight = dc.TargetOrderWeight,
                TargetOrderWeightMin = dc.TargetOrderWeightMin,
                TargetOrderWeightMax = dc.TargetOrderWeightMax,
                CreatedInL3 = dc.CreatedInL3,
                ToBeCompletedBefore = dc.ToBeCompletedBefore,
                NextAggregate = dc.NextAggregate,
                OperationCode = dc.OperationCode,
                QualityPolicy = dc.QualityPolicy,
                ExtraLabelInformation = dc.ExtraLabelInformation,
                ExternalSteelgradeCode = dc.ExternalSteelgradeCode,
                FKProductCatalogueId = dc.FKProductCatalogueId,
                FKMaterialCatalogueId = heat.PRMMaterialCatalogue.MaterialCatalogueId,
                FKCustomerId = dc.FKCustomerId,
                FKReheatingGroupId = dc.FKReheatingGroupId
            };
            
            ctx.PRMWorkOrders.Add(workOrder);
        }

        public void UpdateWorkOrderByUser(PEContext ctx, PRMWorkOrder workOrder, DCWorkOrder dc)
        {
            if (ctx == null) { ctx = new PEContext(); }

            try
            {
                if (workOrder != null)
                {
                    workOrder.LastUpdateTs = DateTime.Now;
                    workOrder.WorkOrderName = dc.WorkOrderName;
                    workOrder.IsTestOrder = dc.IsTestOrder;
                    workOrder.TargetOrderWeight = dc.TargetOrderWeight;
                    workOrder.TargetOrderWeightMin = dc.TargetOrderWeightMin;
                    workOrder.TargetOrderWeightMax = dc.TargetOrderWeightMax;
                    workOrder.ToBeCompletedBefore = dc.ToBeCompletedBefore;
                    workOrder.NextAggregate = dc.NextAggregate;
                    workOrder.OperationCode = dc.OperationCode;
                    workOrder.QualityPolicy = dc.QualityPolicy;
                    workOrder.ExtraLabelInformation = dc.ExtraLabelInformation;
                    workOrder.ExternalSteelgradeCode = dc.ExternalSteelgradeCode;
                    workOrder.FKProductCatalogueId = dc.FKProductCatalogueId;
                    workOrder.FKCustomerId = dc.FKCustomerId;
                    workOrder.FKReheatingGroupId = dc.FKReheatingGroupId;
                    if (dc.WorkOrderStatus.HasValue) workOrder.WorkOrderStatus = dc.WorkOrderStatus.Value;
                }
                else
                {
                    throw new Exception() { Source = "Update Work Order - Work Order not found" };
                }
            }
            catch (Exception)
            {
                throw new Exception() { Source = "Update Work Order - error during saving" };
            }
        }

        /// <summary>
        /// True if work order with this name exist
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="workOrderName"></param>
        /// <returns></returns>
        public async Task<PRMWorkOrder> GetWorkOrderByNameAsync(PEContext ctx, string workOrderName)
        {
            if (ctx == null) { ctx = new PEContext(); }

            return await ctx.PRMWorkOrders.Where(x => x.WorkOrderName.ToLower().Equals(workOrderName.ToLower())).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Update work order properties
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="workOrderName"></param>
        /// <param name="toBeReadyBefore"></param>
        /// <param name="targetOrderWeight"></param>
        /// <param name="targetOrderWeightMin"></param>
        /// <param name="targetOrderWeightMax"></param>
        public async Task<PRMWorkOrder> UpdateWorkOrderAsync(PEContext ctx, long productCatalogue, PRMCustomer customer, long reheatingGroup, PRMMaterialCatalogue matCatalogue, List<PRMMaterial> materials, PRMHeat heat, DCL3L2WorkOrderDefinition message)
        {
            if (ctx == null) { ctx = new PEContext(); }

            PRMWorkOrder workOrder = await ctx.PRMWorkOrders.Where(x => x.WorkOrderName.ToLower().Equals(message.WorkOrderName.ToLower())).SingleAsync();
            workOrder.ToBeCompletedBefore = message.ToBeReadyBefore;
            workOrder.TargetOrderWeight = message.TargetOrderWeight;
            workOrder.TargetOrderWeightMin = message.TargetOrderWeightMin;
            workOrder.TargetOrderWeightMax = message.TargetOrderWeightMax;
            workOrder.FKProductCatalogueId = productCatalogue;
            workOrder.QualityPolicy = message.QualityPolicy;
            workOrder.ExtraLabelInformation = message.ExtraLabelInformation;
            workOrder.PRMCustomer = customer;
            workOrder.PRMMaterialCatalogue = matCatalogue;
            workOrder.FKReheatingGroupId = reheatingGroup;
            workOrder.PRMMaterials = materials;

            return workOrder;
        }

        /// <summary>
        /// Update work order status by his name
        /// Be sure if work order exist before run this function (IsWorkOrderExist())
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="workOrderName"></param>
        /// <param name="status"></param>
        public async Task UpdateWorkOrderStatusAsync(PEContext ctx, long workOrderId, OrderStatus status, bool isScheduled, DateTime? scheduledTime = null)
        {
            if (ctx == null) { ctx = new PEContext(); }

            PRMWorkOrder workOrder = await ctx.PRMWorkOrders.Where(x => x.WorkOrderId == workOrderId).SingleAsync();
            workOrder.WorkOrderStatus = status;
        }

        public async Task<long> WorkOrderIdByNameAsync(PEContext ctx, string workOrderName)
        {
            if (ctx == null) { ctx = new PEContext(); }

            PRMWorkOrder workOrder = await ctx.PRMWorkOrders.Where(x => x.WorkOrderName.ToLower().Equals(workOrderName.ToLower())).SingleOrDefaultAsync();

            return workOrder.WorkOrderId;
        }

        //public async Task<PRMMaterial> FindFirstUnassignedMaterialInWorkOrderAsync(PEContext ctx, long workOrderId)
        //{
        //  if (ctx == null)
        //  {
        //    ctx = new PEContext();
        //  }

        //  return await ctx.PRMMaterials
        //                        .Where(w =>
        //                                      w.FKWorkOrderId == workOrderId &&
        //                                      w.IsAssigned == false)
        //                        .Take(1)
        //                        .FirstOrDefaultAsync();
        //}
    }
}
