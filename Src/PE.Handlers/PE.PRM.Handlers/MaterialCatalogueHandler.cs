using PE.Common;
using PE.DbEntity.Models;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.ProdManager;
using SMF.Module.Core;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PE.PRM.Handlers
{
  public sealed class MaterialCatalogueHandler
  {
    public async Task<PRMMaterialCatalogue> GetMaterialCatalogueByL3TelegramAsync(PEContext ctx, long steelgradeId, DCL3L2WorkOrderDefinition message)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      PRMMaterialCatalogueType materialCatalogueType = await ctx.PRMMaterialCatalogueTypes.Where(x => x.MaterialCatalogueTypeSymbol.ToLower().Equals(message.RawMaterialType.ToLower())).SingleOrDefaultAsync() ?? await ctx.PRMMaterialCatalogueTypes.Where(x => x.IsDefault == true).SingleAsync();
      PRMShape shape = await ctx.PRMShapes.Where(x => x.ShapeSymbol.ToLower().Equals(message.RawMaterialShapeType.ToLower())).SingleOrDefaultAsync() ?? await ctx.PRMShapes.Where(x => x.IsDefault == true).SingleAsync();

      PRMMaterialCatalogue materialCatalogue = await ctx.PRMMaterialCatalogues.Where(x =>

        x.FKMaterialCatalogueTypeId == materialCatalogueType.MaterialCatalogueTypeId &&
        x.FKShapeId == shape.ShapeId &&
        x.Thickness == message.RawMaterialThickness &&
        x.Width == message.RawMaterialWidth &&
        x.Weight == message.RawMaterialWeight)
      .SingleOrDefaultAsync();

      return materialCatalogue;
    }

    public PRMMaterialCatalogue CreateMaterialCatalogue(DCMaterialCatalogue dc)
    {
      PRMMaterialCatalogue catalogue;
      try
      {
        catalogue = new PRMMaterialCatalogue()
        {

          MaterialCatalogueId = dc.MaterialCatalogueId,
          MaterialCatalogueName = dc.MaterialName,
          CreatedTs = DateTime.Now,
          LastUpdateTs = DateTime.Now,
          Description = dc.Description,
          Length = dc.Length,
          Width = dc.Width,
          Thickness = dc.Thickness,
          Weight = dc.Weight,
          SAPNumber = dc.SAPNumber,
          FKShapeId = dc.ShapeId,
          FKSteelgradeId = dc.SteelgradeId.Value,
          FKMaterialCatalogueTypeId = dc.MaterialCatalogueTypeId,

          // Details
          LengthMin = dc.LengthMin,
          LengthMax = dc.LengthMax,
          WidthMin = dc.WidthMin,
          WidthMax = dc.WidthMax,
          ThicknessMin = dc.ThicknessMin,
          ThicknessMax = dc.ThicknessMax,
          WeightMin = dc.WeightMin,
          WeightMax = dc.WeightMax
        };
        
      }
      catch (Exception)
      {
        throw new Exception() { Source = "CreateMaterialCatalogue - Error during saving" };
      }

      return catalogue;
    }

    public void UpdateMaterialCatalogue(PRMMaterialCatalogue matCatalogue, DCMaterialCatalogue dc)
    {

      try
      {
        if (matCatalogue != null)
        {

          matCatalogue.MaterialCatalogueId = dc.MaterialCatalogueId;
          matCatalogue.MaterialCatalogueName = dc.MaterialName;
          matCatalogue.Description = dc.Description;
          matCatalogue.Length = dc.Length;
          matCatalogue.Width = dc.Width;
          matCatalogue.Thickness = dc.Thickness;
          matCatalogue.Weight = dc.Weight;
          matCatalogue.SAPNumber = dc.SAPNumber;
          matCatalogue.FKShapeId = dc.ShapeId;
          matCatalogue.FKSteelgradeId = dc.SteelgradeId.Value;

          // Details
          matCatalogue.LengthMin = dc.LengthMin;
          matCatalogue.LengthMax = dc.LengthMax;
          matCatalogue.WidthMin = dc.WidthMin;
          matCatalogue.WidthMax = dc.WidthMax;
          matCatalogue.ThicknessMin = dc.ThicknessMin;
          matCatalogue.ThicknessMax = dc.ThicknessMax;
          matCatalogue.WeightMin = dc.WeightMin;
          matCatalogue.WeightMax = dc.WeightMax;
        }
        else
        {
          throw new Exception() { Source = "UpdateMaterialCatalogue - MC not found" };
        }
      }
      catch (Exception)
      {
        throw new Exception() { Source = "UpdateMaterialCatalogue - Error during saving" };
      }
    }

    public async Task<PRMMaterialCatalogue> CreateMaterialCatalogueAsync(PEContext ctx, PRMSteelgrade steelgrade, DCL3L2WorkOrderDefinition message)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      PRMMaterialCatalogue materialCatalogue = new PRMMaterialCatalogue()
      {
        CreatedTs = DateTime.Now,
        LastUpdateTs = DateTime.Now,
        
        MaterialCatalogueName = "Mat_Cat_" + DateTime.Now.ToLongTimeString(),

        PRMMaterialCatalogueType = await ctx.PRMMaterialCatalogueTypes.Where(x=>x.MaterialCatalogueTypeSymbol.ToLower().Equals(message.RawMaterialType.ToLower())).SingleOrDefaultAsync() ?? await ctx.PRMMaterialCatalogueTypes.Where(x => x.IsDefault == true).SingleAsync(),
        PRMShape = await ctx.PRMShapes.Where(x => x.ShapeSymbol.ToLower().Equals(message.RawMaterialShapeType.ToLower())).SingleOrDefaultAsync() ?? await ctx.PRMShapes.Where(x => x.IsDefault == true).SingleAsync(),
        PRMSteelgrade = steelgrade,
        Length = message.RawMaterialLength,
        Thickness = message.RawMaterialThickness,
        Width = message.RawMaterialWidth,
        Weight = message.RawMaterialWeight,
        IsActive = true,

        PRMMaterialCatalogueEXT = new PRMMaterialCatalogueEXT()
        {
          CreatedTs = DateTime.Now
        },
        
      };

      return materialCatalogue;
    }

    public async Task<PRMMaterialCatalogue> GetMaterialCatalogueById(PEContext ctx, long materialCatalogueId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      PRMMaterialCatalogue matCat = ctx.PRMMaterialCatalogues.Find(materialCatalogueId);

      if (matCat == null)
      {
        matCat = await ctx.PRMMaterialCatalogues.Where(x => x.IsDefault == true).SingleAsync();
      }

      return matCat;
    }

    public async Task<PRMMaterialCatalogue> GetMaterialCatalogueByNameAsync(PEContext ctx, string materialName)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      return await ctx.PRMMaterialCatalogues.Where(x => x.MaterialCatalogueName == materialName).SingleAsync();
    }
  }
}

