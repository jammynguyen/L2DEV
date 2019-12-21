using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;
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
using PE.DTO.Internal.Quality;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class DefectService : BaseService, IDefectService
  {
    public DataSourceResult GetDefectCatalogueList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.MVHDefectCatalogues
                    .Include(x => x.MVHDefectCatalogueCategory)
                    .ToDataSourceResult(request, modelState, x => new VM_DefectCatalogue(x));
      }

      return result;
    }

    public VM_DefectCatalogue GetDelayCatalogue(long id)
    {
      VM_DefectCatalogue result = null;

      using (PEContext ctx = new PEContext())
      {
        MVHDefectCatalogue defectCatalogue = ctx.MVHDefectCatalogues
                                             .Include(x => x.MVHDefectCatalogueCategory)
                                             .SingleOrDefault(x => x.DefectCatalogueId == id);
        result = defectCatalogue == null ? null : new VM_DefectCatalogue(defectCatalogue);
      }

      return result;
    }

    public IList<VM_DefectCatalogueCategory> GetDefectCategoryList()
    {
      List<VM_DefectCatalogueCategory> result = new List<VM_DefectCatalogueCategory>();
      using (PEContext ctx = new PEContext())
      {

        IQueryable<MVHDefectCatalogueCategory> dbList = ctx.MVHDefectCatalogueCategories.AsQueryable();
        foreach (MVHDefectCatalogueCategory item in dbList)
        {
          result.Add(new VM_DefectCatalogueCategory(item));
        }
      }
      return result;
    }

    public async Task<VM_Base> UpdateDefectCatalogue(ModelStateDictionary modelState, VM_DefectCatalogue defectCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref defectCatalogue);

      DCDefectCatalogue dcDefectCatalogue = new DCDefectCatalogue()
      {
        Id = defectCatalogue.DefectCatalogueId,
				DefectCatalogueCode = defectCatalogue.DefectCatalogueCode,
        DefectCatalogueName = defectCatalogue.DefectCatalogueName,
        DefectCatalogueDescription = defectCatalogue.DefectCatalogueDescription,
        IsDefault = defectCatalogue.IsDefault,
        FkDelayCatalogueCategoryId = defectCatalogue.DefectCatalogueCategoryId
      };


      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDefectCatalogue(dcDefectCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> AddDefectCatalogue(ModelStateDictionary modelState, VM_DefectCatalogue defectCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref defectCatalogue);

      DCDefectCatalogue dcDefectCatalogue = new DCDefectCatalogue()
      {
        Id = 0,
        DefectCatalogueName = defectCatalogue.DefectCatalogueName,
        DefectCatalogueCode = defectCatalogue.DefectCatalogueCode,
        DefectCatalogueDescription = defectCatalogue.DefectCatalogueDescription,
        IsDefault = defectCatalogue.IsDefault,
        FkDelayCatalogueCategoryId = defectCatalogue.DefectCatalogueCategoryId
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendAddDefectCatalogue(dcDefectCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteDefectCatalogueAsync(ModelStateDictionary modelState, VM_DefectCatalogue defectCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref defectCatalogue);

      DCDefectCatalogue dcDefectCatalogue = new DCDefectCatalogue()
      {
        Id = defectCatalogue.DefectCatalogueId
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDeleteDefectCatalogue(dcDefectCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

  }
}
