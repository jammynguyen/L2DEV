using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.DTO.Internal.Maintenance;
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
using PE.HMIWWW.ViewModel.Module.Lite.Quality;
using PE.HMIWWW.Core.Resources;
using PE.DTO.Internal.Quality;
using PE.DbEntity.Enums;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;
using SMF.Core.Helpers;
using PE.HMIWWW.ViewModel.Module.Lite.Products;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ProductQualityMgmService : BaseService, IProductQualityMgmService
  {
    public DataSourceResult GetProductHistoryList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.V_ProductionHistory
          .ToDataSourceResult<V_ProductionHistory, VM_ProductHistory>(request, modelState, x => new VM_ProductHistory(x));
      }
      return result;
    }

    public async Task<VM_Base> AssignQualityAsync(ModelStateDictionary modelState, long productId, short quality, List<long> defectIds)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      //UnitConverterHelper.ConvertToSi(ref defectCatalogue);

      DCQualityAssignment dcQualityAssignment = new DCQualityAssignment();
      dcQualityAssignment.DefectCatalogueElementIds = defectIds;
      if (dcQualityAssignment.ProductIds is null) dcQualityAssignment.ProductIds = new List<long>();
      dcQualityAssignment.ProductIds.Add(productId);
      dcQualityAssignment.QualityFlag = (ProductQuality)quality;


      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AssignQualityAsync(dcQualityAssignment);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public DataSourceResult GetDefectsList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.MVHDefectCatalogues
          .ToDataSourceResult<MVHDefectCatalogue, VM_DefectCatalogue>(request, modelState, x => new VM_DefectCatalogue(x));
      }
      return result;
    }

    public Dictionary<int, string> GetProductQualityList()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(ProductQuality)))
      {
        resultDictionary.Add(item.Key, Core.HtmlHelpers.ResxHelper.GetResxByKey(string.Format("GLOB_ProductQuality_{0}", item.Key)));
      }

      return resultDictionary;
    }

    public DataSourceResult GetProductDefectsList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request, long productId)
    {
      using (PEContext ctx = new PEContext())
      {
        IQueryable<V_DefectsSummary> productDefectsList = ctx.V_DefectsSummary.Where(x => x.FKProductId == productId).AsQueryable();

        return productDefectsList.ToDataSourceResult<V_DefectsSummary, VM_ProductDefect>(request, modelState, data => new VM_ProductDefect(data));
      }
    }
  }
}
