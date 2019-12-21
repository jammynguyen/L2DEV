using PE.DbEntity.Models;
using System;
using System.Data.Entity;
using System.Collections;
using System.Linq;

namespace PE.PRM.Handlers
{
  public sealed class ProductHandler
  {
    /// <summary>
    /// Create Product from given RawMaterial and connected with it PRMMaterial
    /// </summary>
    /// <param name="rawMaterial"></param>
    /// <param name="material"></param>
    /// <param name="overallWeight"></param>
    /// <returns></returns>
    public PRMProduct CreateProduct(long rawMaterialId, PRMMaterial material, double overallWeight)
    {
      PRMProduct product = new PRMProduct()
      {
        CreatedTs = DateTime.Now,
        LastUpdateTs = DateTime.Now,
        IsDummy = false,
        ProductName = "P_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
        //FKWorkOrderId = material.FKWorkOrderId,
        //FKHeatId = material.FKHeatId,
        //FKProductCatalogueIdRef = material.PRMWorkOrder.FKProductCatalogueId,
        FKWorkOrderId = material.FKWorkOrderId,
        Weight = overallWeight,
        IsAssigned = true,

        PRMProductsEXT = new PRMProductsEXT()
        {
          CreatedTs = DateTime.Now,
          
        }
        
      };

      return product;
    }
  }
}
