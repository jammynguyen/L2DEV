using PE.DbEntity.Models;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.ProdManager;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PE.PRM.Handlers
{
  public sealed class ProductCatalogueHandler
  {
    /// <summary>
    /// Return product catalogue by his name or return default if not exist ( optional )
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="productName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<PRMProductCatalogue> GetProductCatalogueByNameAsync(PEContext ctx, string productName, bool getDefault = false)
    {
      if (ctx == null) { ctx = new PEContext(); }

      PRMProductCatalogue productCatalogue = await ctx.PRMProductCatalogues.Where(x => x.ProductCatalogueName.ToLower().Equals(productName.ToLower())).SingleOrDefaultAsync();

      if (productCatalogue == null && getDefault == true)
      {
        productCatalogue = await ctx.PRMProductCatalogues.Where(x => x.IsDefault == true).SingleAsync();
      }

      return productCatalogue;
    }

    /// <summary>
    /// Return product catalogue by his name or return default if not exist ( optional )
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="productName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<PRMProductCatalogue> GetProductCatalogueByIdAsync(PEContext ctx, long productId, bool getDefault = false)
    {
      if (ctx == null) { ctx = new PEContext(); }

      PRMProductCatalogue productCatalogue = await ctx.PRMProductCatalogues.Where(x => x.ProductCatalogueId==productId).SingleOrDefaultAsync();

      if (productCatalogue == null && getDefault == true)
      {
        productCatalogue = await ctx.PRMProductCatalogues.Where(x => x.IsDefault == true).SingleAsync();
      }

      return productCatalogue;
    }

    /// <summary>
    /// On Work Order creation it is needed to get existing Product Catalogue matching this in transfer table,
    /// If PC not exist, then throw an exception to stop processing, throw an alarm and send back msg to TT
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="productName"></param>
    /// <param name="backMsg"></param>
    /// <returns></returns>
    public async Task<PRMProductCatalogue> ProcessGetProductCatalogueByNameForWorkOrderAsync(PEContext ctx, string productName, DCWorkOrderStatus backMsg)
    {
      PRMProductCatalogue productCatalogue = await GetProductCatalogueByNameAsync(ctx, productName);

      if(productCatalogue == null)
      {
        backMsg.BackMessage += " PRMProductCatalogue - not found in db;";
        throw new Exception();
      }
      return productCatalogue;
    }

    public PRMProductCatalogue CreateProductCatalogue(DCProductCatalogue dc)
    {
      PRMProductCatalogue productCatalogue;
      try
      {
        productCatalogue = new PRMProductCatalogue();
        productCatalogue.ProductCatalogueName = dc.Name;
        productCatalogue.CreatedTs = DateTime.Now;
        productCatalogue.LastUpdateTs = DateTime.Now;

        productCatalogue.Length = dc.Length.GetValueOrDefault();
        productCatalogue.LengthMax = dc.LengthMax.GetValueOrDefault();
        productCatalogue.LengthMin = dc.LengthMin.GetValueOrDefault();
        productCatalogue.Width = dc.Width;
        productCatalogue.WidthMax = dc.WidthMax;
        productCatalogue.WidthMin = dc.WidthMin;
        productCatalogue.Thickness = dc.Thickness;
        productCatalogue.ThicknessMax = dc.ThicknessMax;
        productCatalogue.ThicknessMin = dc.ThicknessMin;
        productCatalogue.Weight = dc.Weight;
        productCatalogue.WeightMax = dc.WeightMax;
        productCatalogue.WeightMin = dc.WeightMin;
        productCatalogue.ProfileToleranceMax = dc.ProfileToleranceMax;
        productCatalogue.ProfileToleranceMin = dc.ProfileToleranceMin;
        productCatalogue.Ovality = dc.Ovality;
        productCatalogue.MaxOvality = dc.OvalityMax;
        productCatalogue.MaxYieldPoint = dc.MaxYieldPoint;
        productCatalogue.Description = dc.Description;
        productCatalogue.SAPNumber = dc.SAPNumber;
        productCatalogue.Slitting = dc.Slitting;
        productCatalogue.StdGapTime = dc.StdGapTime;
        productCatalogue.StdMetallicYield = dc.StdMetallicYield;
        productCatalogue.StdProductionTime = dc.StdProductionTime;
        productCatalogue.StdQualityYield = dc.StdQualityYield;
        productCatalogue.StdRollingTime = dc.StdRollingTime;
        productCatalogue.StdProductivity = dc.StdProductivity.Value;
        productCatalogue.StdUtilizationTime = dc.StdUtilizationTime;
        productCatalogue.FKSteelgradeId = dc.SteelgradeId.Value;
        productCatalogue.FKShapeId = dc.ShapeId;
        productCatalogue.FKProductCatalogueTypeId = (long)dc.TypeId;
      }
      catch (Exception)
      {
        throw new Exception() { Source = "CreateMaterialCatalogue - Error during saving" };
      }
      return productCatalogue;
    }

    public void UpdateProductCatalogue(PRMProductCatalogue productCatalogue, DCProductCatalogue dc)
    {

      try
      {
        if (productCatalogue != null)
        {
          productCatalogue.ProductCatalogueName = dc.Name;

          productCatalogue.Length = dc.Length.GetValueOrDefault();
          productCatalogue.LengthMax = dc.LengthMax.GetValueOrDefault();
          productCatalogue.LengthMin = dc.LengthMin.GetValueOrDefault();
          productCatalogue.Width = dc.Width;
          productCatalogue.WidthMax = dc.WidthMax;
          productCatalogue.WidthMin = dc.WidthMin;
          productCatalogue.Thickness = dc.Thickness;
          productCatalogue.ThicknessMax = dc.ThicknessMax;
          productCatalogue.ThicknessMin = dc.ThicknessMin;
          productCatalogue.Weight = dc.Weight;
          productCatalogue.WeightMax = dc.WeightMax;
          productCatalogue.WeightMin = dc.WeightMin;
          productCatalogue.ProfileToleranceMax = dc.ProfileToleranceMax;
          productCatalogue.ProfileToleranceMin = dc.ProfileToleranceMin;
          productCatalogue.Ovality = dc.Ovality;
          productCatalogue.MaxOvality = dc.OvalityMax;
          productCatalogue.MaxYieldPoint = dc.MaxYieldPoint;
          productCatalogue.Description = dc.Description;
          productCatalogue.SAPNumber = dc.SAPNumber;
          productCatalogue.Slitting = dc.Slitting;
          productCatalogue.StdGapTime = dc.StdGapTime;
          productCatalogue.StdMetallicYield = dc.StdMetallicYield;
          productCatalogue.StdProductionTime = dc.StdProductionTime;
          productCatalogue.StdQualityYield = dc.StdQualityYield;
          productCatalogue.StdRollingTime = dc.StdRollingTime;
          productCatalogue.StdProductivity = dc.StdProductivity.Value;
          productCatalogue.StdUtilizationTime = dc.StdUtilizationTime;
          productCatalogue.FKSteelgradeId = dc.SteelgradeId.Value;
          productCatalogue.FKShapeId = dc.ShapeId;
          productCatalogue.FKProductCatalogueTypeId = (long)dc.TypeId;



        }
        else
        {
          throw new Exception() { Source = "UpdateMaterialCatalogue - PC not found" };
        }
      }
      catch (Exception)
      {
        throw new Exception() { Source = "UpdateMaterialCatalogue - Error during saving" };
      }
    }
  }
}
