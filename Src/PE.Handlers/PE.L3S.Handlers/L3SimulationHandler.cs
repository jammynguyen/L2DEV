using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.DBAdapter;

namespace PE.L3S.Handlers
{
  public sealed class L3SimulationHandler
  {
    #region public methods
    /// <summary>
    /// verify if schedule is empty
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    public async Task<bool> CheckIfNewOrdersHaveToBeGeneratedAsync(PEContext ctx)
    {
      IEnumerable<long> workOrdersInSchedule = await ctx.PPLSchedules.Select(s => s.FKWorkOrderId).ToListAsync();
      int numberOfMaterials = 0;

      foreach (long workOrder in workOrdersInSchedule)
      {
        numberOfMaterials += await ctx.PRMMaterials.Where(w => w.FKWorkOrderId == workOrder && w.IsAssigned == false).CountAsync();
        if (numberOfMaterials > 2)
          return false;
      }
	
      return true;
    }
    public async Task<string> GetRandomMaterialTypeAsync(PEContext ctx)
    {
      List<PRMShape> maierialType = await ctx.PRMShapes.ToListAsync();

      int index = new Random().Next(maierialType.Count - 1);

      return maierialType[index].ShapeSymbol;
    }
    public L3L2WorkOrderDefinition CreateL3WorkOrder(PEContext ctx, string shapeType, int limitWeightMin, int limitWeightMax,  short noOfManterials)
    {
      if (noOfManterials < 1)
        noOfManterials = 1;


      double[] dimensions = new double[] {0.12, 0.14, 0.15, 0.2 };
      double[] lengths = new double[] { 0.9, 1, 1.1, 1.2 };
      String[] productNames = new String[] { "Default Product", "Customer Product" , "Other"};
      String[] steelGrades = new String[] { "SC", "SGC", "SG" };

      double materialDimension = dimensions[new Random().Next(0, dimensions.Length-1)];
      double length = lengths[new Random().Next(0, lengths.Length - 1)];
      string productName = productNames[new Random().Next(0, productNames.Length-1)];
      string steelGrade = steelGrades[new Random().Next(0, steelGrades.Length-1)];
      double billetWeight = materialDimension* materialDimension* length*7800;
      double orderWeight = billetWeight * noOfManterials * 0.9;


      if (orderWeight < 1)
        orderWeight = 1;

      return new L3L2WorkOrderDefinition()
      {
        CreatedTs = DateTime.Now,
        UpdatedTs = DateTime.Now.AddSeconds(-30),
        CommStatus = DbEntity.Enums.CommStatus.ENUM_COMMSTATUS_New,
        WorkOrderName = "Sim_WO_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
        ProductName = productName,
        CreatedInL3 = DateTime.Now.AddMinutes(-1),
        CustomerName = String.Format("Customer Name {0}", DateTime.Now.Minute),
        ToBeCompletedBefore = DateTime.Now.AddDays(10),
        TargetOrderWeight = orderWeight,
        TargetOrderWeightMin = orderWeight - orderWeight * 0.05,
        TargetOrderWeightMax = orderWeight + orderWeight * 0.05,
        HeatName = ConstructHeatName(steelGrade),
        RawMaterialNumber = noOfManterials,
        RawMaterialShapeType = (DateTime.Now.Second % 2 == 0) ? "RS" : "O",
        RawMaterialThickness = materialDimension,
        RawMaterialWidth = materialDimension,
        RawMaterialLength = length,
        RawMaterialWeight = billetWeight,
        RawMaterialType = (DateTime.Now.Second % 2 == 0) ? "BB" : "RCB",
        ExternalSteelgradeCode = "External steel grade code (to be printed)",
        NextAggregate = "Rolling Mill",
        OperationCode = "Cust. Code",
        ReheatingGroupName = (DateTime.Now.Second % 2 == 0) ? "EFG" : "SomeNotExisting",
        QualityPolicy = "Customer's information about quality policy",
        ExtraLabelInformation = "Additional information to be printed",
        SteelgradeCode = steelGrade
      };
    }
    public void StoreL3WorkOrderInTransferTable(PEContext ctx, L3L2WorkOrderDefinition workOrderData)
    {
      if (ctx == null) { ctx = new PEContext(); }

      ctx.L3L2WorkOrderDefinition.Add(workOrderData);
    }
    
		#endregion
    #region private helpers

    private string ConstructHeatName(string steelGrade)
    {
      return "Heat_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "_" + DateTime.Now.Hour+"_"+ steelGrade;
    }

    #endregion
  }
}
