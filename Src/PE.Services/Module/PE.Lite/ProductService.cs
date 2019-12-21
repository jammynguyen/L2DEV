using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.DTO.Internal.ProdManager;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using PE.HMIWWW.Core.Resources;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ProductService : BaseService, IProductService
  {
    public VM_ProductCatalogue GetProductCatalogue(long id)
    {
      VM_ProductCatalogue result = null;

      using (PEContext ctx = new PEContext())
      {
        PRMProductCatalogue productCatalogue = ctx.PRMProductCatalogues
          .Include(i => i.PRMShape)
          .Include(i => i.PRMProductCatalogueType)
          .Include(i=>i.PRMSteelgrade)
          .SingleOrDefault(x => x.ProductCatalogueId == id);
        result = productCatalogue == null ? null : new VM_ProductCatalogue(productCatalogue);
      }

        return result;
    }

    public DataSourceResult GetProductCatalogueList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.PRMProductCatalogues
          .Include(i => i.PRMShape)
          .Include(i=>i.PRMProductCatalogueType)
          .Include(i => i.PRMSteelgrade)
          .ToDataSourceResult<PRMProductCatalogue, VM_ProductCatalogue>(request, modelState, x => new VM_ProductCatalogue(x));
      }

      return result;
    }

    public IList<VM_Steelgrade> GetSteelgradeList()
    {
      List<VM_Steelgrade> result = new List<VM_Steelgrade>();
      using (PEContext ctx = new PEContext())
      {

        IQueryable<PRMSteelgrade> dbList = ctx.PRMSteelgrades.AsQueryable();
        foreach (PRMSteelgrade item in dbList)
        {
          result.Add(new VM_Steelgrade(item));
        }
      }
      return result;
    }

    public IList<VM_Shape> GetShapeList()
    {
      List<VM_Shape> result = new List<VM_Shape>();
      using (PEContext ctx = new PEContext())
      {

        IQueryable<PRMShape> dbList = ctx.PRMShapes.AsQueryable();
        foreach (PRMShape item in dbList)
        {
          result.Add(new VM_Shape(item));
        }
      }
      return result;
    }
    

    public IList<VM_ProductCatalogueType> GetProductCatalogueTypeList()
    {
      List<VM_ProductCatalogueType> result = new List<VM_ProductCatalogueType>();
      using (PEContext ctx = new PEContext())
      {

        IQueryable<PRMProductCatalogueType> dbList = ctx.PRMProductCatalogueTypes.AsQueryable();
        foreach (PRMProductCatalogueType item in dbList)
        {
          result.Add(new VM_ProductCatalogueType(item));
        }
      }
      return result;
    }

    public async Task<VM_Base> CreateProductCatalogue(ModelStateDictionary modelState, VM_ProductCatalogue productCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref productCatalogue);

      DCProductCatalogue dcProductCatalogue = new DCProductCatalogue()
      {
        Id = productCatalogue.Id,
        Name = productCatalogue.Name,
        Weight = productCatalogue.Weight,
        WeightMax = productCatalogue.WeightMax,
        WeightMin = productCatalogue.WeightMin,
        LastUpdateTs = productCatalogue.LastUpdateTs,
        Length = productCatalogue.Length,
        LengthMax = productCatalogue.LengthMax,
        LengthMin = productCatalogue.LengthMin,
        Width = productCatalogue.Width,
        WidthMax = productCatalogue.WidthMax,
        WidthMin = productCatalogue.WidthMin,
        Thickness = productCatalogue.Thickness,
        ThicknessMax = productCatalogue.ThicknessMax,
        ThicknessMin = productCatalogue.ThicknessMin,
        MaxTensile = productCatalogue.MaxTensile,
        MaxYieldPoint = productCatalogue.MaxYieldPoint,
        StdGapTime = productCatalogue.StdGapTime,
        StdMetallicYield = productCatalogue.StdMetallicYield,
        StdProductionTime = productCatalogue.StdProductionTime,
        StdQualityYield = productCatalogue.StdQualityYield,
        StdRollingTime = productCatalogue.StdRollingTime,
        StdProductivity = productCatalogue.StdProductivity,
        StdUtilizationTime = productCatalogue.StdUtilizationTime,
        ProfileToleranceMax = productCatalogue.ProfileToleranceMax,
        ProfileToleranceMin = productCatalogue.ProfileToleranceMin,
        SAPNumber = productCatalogue.SAPNumber,
        Slitting = productCatalogue.Slitting,
        Ovality = productCatalogue.Ovality,
        OvalityMax = productCatalogue.OvalityMax,
        Description = productCatalogue.Description,
        ExitSpeed = productCatalogue.ExitSpeed,
        SteelgradeId = productCatalogue.SteelgradeId,
        ShapeId = productCatalogue.ShapeId,
        TypeId = productCatalogue.TypeId,

        //Steelgrade = productCatalogue.Steelgrade,
        //Shape = productCatalogue.Shape,
        //Type = productCatalogue.Type,
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendCreateProductCatalogueAsync(dcProductCatalogue);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> UpdateProductCatalogue(ModelStateDictionary modelState, VM_ProductCatalogue productCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref productCatalogue);

      DCProductCatalogue dcProductCatalogue = new DCProductCatalogue()
      {
        Id = productCatalogue.Id,
        Name = productCatalogue.Name,
        Weight = productCatalogue.Weight,
        WeightMax = productCatalogue.WeightMax,
        WeightMin = productCatalogue.WeightMin,
        LastUpdateTs = productCatalogue.LastUpdateTs,
        Length = productCatalogue.Length,
        LengthMax = productCatalogue.LengthMax,
        LengthMin = productCatalogue.LengthMin,
        Width = productCatalogue.Width,
        WidthMax = productCatalogue.WidthMax,
        WidthMin = productCatalogue.WidthMin,
        Thickness = productCatalogue.Thickness,
        ThicknessMax = productCatalogue.ThicknessMax,
        ThicknessMin = productCatalogue.ThicknessMin,
        MaxTensile = productCatalogue.MaxTensile,
        MaxYieldPoint = productCatalogue.MaxYieldPoint,
        StdGapTime = productCatalogue.StdGapTime,
        StdMetallicYield = productCatalogue.StdMetallicYield,
        StdProductionTime = productCatalogue.StdProductionTime,
        StdQualityYield = productCatalogue.StdQualityYield,
        StdRollingTime = productCatalogue.StdRollingTime,
        StdProductivity = productCatalogue.StdProductivity,
        StdUtilizationTime = productCatalogue.StdUtilizationTime,
        ProfileToleranceMax = productCatalogue.ProfileToleranceMax,
        ProfileToleranceMin = productCatalogue.ProfileToleranceMin,
        SAPNumber = productCatalogue.SAPNumber,
        Slitting = productCatalogue.Slitting,
        Ovality = productCatalogue.Ovality,
        OvalityMax = productCatalogue.OvalityMax,
        Description = productCatalogue.Description,
        ExitSpeed = productCatalogue.ExitSpeed,
        SteelgradeId = productCatalogue.SteelgradeId,
        ShapeId = productCatalogue.ShapeId,
        TypeId = productCatalogue.TypeId,

        //Steelgrade = productCatalogue.Steelgrade,
        //Shape = productCatalogue.Shape,
        //Type = productCatalogue.Type,
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendProductCatalogueAsync(dcProductCatalogue);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DeleteProductCatalogue(ModelStateDictionary modelState, VM_ProductCatalogue productCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref productCatalogue);

      DCProductCatalogue dcProdCatalogue = new DCProductCatalogue()
      {
        Id = productCatalogue.Id
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDeleteProductCatalogueAsync(dcProdCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_ProductCatalogue GetProductDetails(ModelStateDictionary modelState, long id)
    {
      VM_ProductCatalogue result = null;

      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      using (PEContext ctx = new PEContext())
      {
        PRMProductCatalogue data = ctx.PRMProductCatalogues
          .Include(i => i.PRMShape)
          .Include(i => i.PRMProductCatalogueType)
          .Include(i => i.PRMSteelgrade)
          .Where(w => w.ProductCatalogueId == id)
          .SingleOrDefault();

        result = new VM_ProductCatalogue(data);
      }

      return result;
    }
  }
}
