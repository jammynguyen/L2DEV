using PE.DbEntity.Models;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Web.Mvc;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.UnitConverter;
using SMF.Core.DC;
using SMF.Module.Core;
using PE.HMIWWW.Core.Communication;
using SMF.RepositoryPatternExt;
using SMF.Module.Notification;
using System.Reflection;
using PE.DTO.Internal.ProdManager;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using PE.HMIWWW.Core.Resources;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class BilletService : BaseService, IBilletService
  {
    public VM_MaterialCatalogue GetMaterialCatalogue(long id)
    {
      VM_MaterialCatalogue result = null;

      using (PEContext ctx = new PEContext())
      {
        PRMMaterialCatalogue materialCatalogue = ctx.PRMMaterialCatalogues
                                  .Include(x => x.PRMSteelgrade)
                                  .Include(x => x.PRMMaterialCatalogueType)
                                  .SingleOrDefault(x => x.MaterialCatalogueId == id);
        result = materialCatalogue == null ? null : new VM_MaterialCatalogue(materialCatalogue);
      }

      return result;
    }

    public DataSourceResult GetProductCatalogueList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.PRMMaterialCatalogues
                    .Include(x => x.PRMSteelgrade)
                    .Include(x => x.PRMShape)
                    .Include(x => x.PRMMaterialCatalogueType)
                    .ToDataSourceResult<PRMMaterialCatalogue, VM_MaterialCatalogue>(request, modelState, x => new VM_MaterialCatalogue(x));
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

    public async Task<VM_Base> UpdateMaterialCatalogue(ModelStateDictionary modelState, VM_MaterialCatalogue materialCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref materialCatalogue);

      DCMaterialCatalogue dcMaterialCatalogue = new DCMaterialCatalogue()
      {
        MaterialCatalogueId = materialCatalogue.Id,
        MaterialName = materialCatalogue.MaterialName,
        Description = materialCatalogue.Description,
        SteelgradeCode = materialCatalogue.SteelgradeCode,
        MaterialCatalogueTypeId = materialCatalogue.TypeId.GetValueOrDefault(),
        Length = materialCatalogue.Length,
        Width = materialCatalogue.Width,
        Thickness = materialCatalogue.Thickness,
        Weight = materialCatalogue.Weight,
        SAPNumber = materialCatalogue.SAPNumber,
        SteelgradeId = materialCatalogue.SteelgradeId,
        ShapeId = materialCatalogue.ShapeId,



        // Details
        LengthMin = materialCatalogue.LengthMin,
        LengthMax = materialCatalogue.LengthMax,
        WidthMin = materialCatalogue.WidthMin,
        WidthMax = materialCatalogue.WidthMax,
        ThicknessMin = materialCatalogue.ThicknessMin,
        ThicknessMax = materialCatalogue.ThicknessMax,
        WeightMin = materialCatalogue.WeightMin,
        WeightMax = materialCatalogue.WeightMax,
        TypeId = materialCatalogue.TypeId,
      };


      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendMaterialCatalogueAsync(dcMaterialCatalogue);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> CreateMaterialCatalogue(ModelStateDictionary modelState, VM_MaterialCatalogue materialCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref materialCatalogue);

      DCMaterialCatalogue dcMaterialCatalogue = new DCMaterialCatalogue()
      {
        MaterialCatalogueId = materialCatalogue.Id,
        MaterialName = materialCatalogue.MaterialName,
        Description = materialCatalogue.Description,
        SteelgradeCode = materialCatalogue.SteelgradeCode,
        Length = materialCatalogue.Length,
        Width = materialCatalogue.Width,
        Thickness = materialCatalogue.Thickness,
        Weight = materialCatalogue.Weight,
        SAPNumber = materialCatalogue.SAPNumber,
        SteelgradeId = materialCatalogue.SteelgradeId,
        ShapeId = materialCatalogue.ShapeId,



        // Details
        LengthMin = materialCatalogue.LengthMin,
        LengthMax = materialCatalogue.LengthMax,
        WidthMin = materialCatalogue.WidthMin,
        WidthMax = materialCatalogue.WidthMax,
        ThicknessMin = materialCatalogue.ThicknessMin,
        ThicknessMax = materialCatalogue.ThicknessMax,
        WeightMin = materialCatalogue.WeightMin,
        WeightMax = materialCatalogue.WeightMax,
        TypeId = materialCatalogue.TypeId,
        MaterialCatalogueTypeId = materialCatalogue.TypeId == null ? 0 : materialCatalogue.TypeId.Value,
      };


      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendCreateMaterialCatalogueAsync(dcMaterialCatalogue);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public VM_MaterialCatalogue GetBilletDetails(ModelStateDictionary modelState, long Id)
    {
      VM_MaterialCatalogue result = null;

      if (Id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return result;
      }

      using (PEContext ctx = new PEContext())
      {
        PRMMaterialCatalogue data = ctx.PRMMaterialCatalogues
          .Include(x => x.PRMSteelgrade)
          .Include(x => x.PRMShape)
          .Include(x => x.PRMMaterialCatalogueType)
          .Where(x => x.MaterialCatalogueId == Id)
          .SingleOrDefault();

        result = new VM_MaterialCatalogue(data);
      }

      return result;
    }

    public async Task<VM_Base> DeleteMaterialCatalogue(ModelStateDictionary modelState, VM_MaterialCatalogue materialCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref materialCatalogue);

      DCMaterialCatalogue dcMatCatalogue = new DCMaterialCatalogue()
      {
        MaterialCatalogueId = materialCatalogue.Id
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDeleteMaterialCatalogueAsync(dcMatCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
    public IList<VM_MaterialCatalogueType> GetMaterialCatalogueTypeList()
    {
      List<VM_MaterialCatalogueType> result = new List<VM_MaterialCatalogueType>();
      using (PEContext ctx = new PEContext())
      {

        IQueryable<PRMMaterialCatalogueType> dbList = ctx.PRMMaterialCatalogueTypes.AsQueryable();
        foreach (PRMMaterialCatalogueType item in dbList)
        {
          result.Add(new VM_MaterialCatalogueType(item));
        }
      }
      return result;
    }
    public IList<VM_MaterialCatalogue> GetMaterialCataloguesByAnyFeaure(string text)
    {
      IList<VM_MaterialCatalogue> result = new List<VM_MaterialCatalogue>();
      using (PEContext ctx = new PEContext())
      {
        if (!string.IsNullOrEmpty(text))
        {
          result = ctx.PRMMaterialCatalogues
            .Include(m => m.PRMSteelgrade)
            .Where(m => m.MaterialCatalogueName.Contains(text) || m.PRMSteelgrade.SteelgradeName.Contains(text) ).AsEnumerable()
            .Select(mc => new VM_MaterialCatalogue(mc)).ToList();
        }
      }

      return result;
    }
  }
}
