using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Delay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.UnitConverter;
using SMF.Core.DC;
using SMF.Module.Core;
using PE.HMIWWW.Core.Communication;
using SMF.RepositoryPatternExt;
using System.Reflection;
using SMF.Module.Notification;
using PE.DTO.Internal.Delay;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class DelaysService : BaseService, IDelaysService
  {
    public VM_DelayCatalogue GetDelayCatalogue(ModelStateDictionary modelState, long id)
    {
      VM_DelayCatalogue result = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      using (PEContext ctx = new PEContext())
      {
        DLSDelayCatalogue delay = ctx.DLSDelayCatalogues                      
                       .Include(x => x.DLSDelayCatalogueCategory)
                       .SingleOrDefault(x => x.DelayCatalogueId == id);
        result = delay == null ? null : new VM_DelayCatalogue(delay);
      }

      return result;
    }

    public DataSourceResult GetDelayCatalogueList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.DLSDelayCatalogues
                    .Include(x => x.DLSDelayCatalogueCategory)
                    .Include(x=>x.DLSDelayCatalogue2)
                    .ToDataSourceResult<DLSDelayCatalogue, VM_DelayCatalogue>(request, modelState, x => new VM_DelayCatalogue(x));
      }

      return result;
    }

    public IList<VM_DelayCatalogueCategory> GetDelayCategories()
    {
      List<VM_DelayCatalogueCategory> result = new List<VM_DelayCatalogueCategory>();
      using (PEContext ctx = new PEContext())
      {

        IQueryable<DLSDelayCatalogueCategory> dbList = ctx.DLSDelayCatalogueCategories.AsQueryable();
        foreach (DLSDelayCatalogueCategory item in dbList)
        {
          result.Add(new VM_DelayCatalogueCategory(item));
        }
      }
      return result;
    }

    public IList<VM_DelayCatalogue> GetDelayCataloguesForParentSelector()
    {
      List<VM_DelayCatalogue> result = new List<VM_DelayCatalogue>();
      using (PEContext ctx = new PEContext())
      {

        IQueryable<DLSDelayCatalogue> dbList = ctx.DLSDelayCatalogues.AsQueryable();
        foreach (DLSDelayCatalogue item in dbList)
        {
          result.Add(new VM_DelayCatalogue(item));
        }
      }
      return result;
    }

    public async Task<VM_Base> UpdateDelayCatalogueAsync(ModelStateDictionary modelState, VM_DelayCatalogue delayCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref delayCatalogue);

      DCDelayCatalogue dcDelayCatalogue = new DCDelayCatalogue()
      {
        Id = delayCatalogue.Id,
        DelayName = delayCatalogue.DelayName,
        StdDelayTime = delayCatalogue.StdDelayTime,
        DelayCategory = delayCatalogue.DelayCategory,
        DelayDescription = delayCatalogue.DelayDescription,
        DelayCode = delayCatalogue.DelayCode,
        IsActive = delayCatalogue.IsActive,
        IsDefault = delayCatalogue.IsDefault,
        FKDelayCategoryId = delayCatalogue.FkDelayCatalogueCategoryId,
        ParentDelayCatalogueId = delayCatalogue.ParentDelayCatalogueId
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDelayCatalogueAsync(dcDelayCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;

    }

    public async Task<VM_Base> AddDelayCatalogueAsync(ModelStateDictionary modelState, VM_DelayCatalogue delayCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref delayCatalogue);

      DCDelayCatalogue dcDelayCatalogue = new DCDelayCatalogue()
      {
        Id = delayCatalogue.Id,
        DelayName = delayCatalogue.DelayName,
        StdDelayTime = delayCatalogue.StdDelayTime,
        DelayCategory = delayCatalogue.DelayCategory,
        DelayDescription = delayCatalogue.DelayDescription,
        DelayCode = delayCatalogue.DelayCode,
        IsActive = delayCatalogue.IsActive,
        IsDefault = delayCatalogue.IsDefault,
        FKDelayCategoryId = delayCatalogue.FkDelayCatalogueCategoryId,
        ParentDelayCatalogueId = delayCatalogue.ParentDelayCatalogueId
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendAddDelayCatalogueAsync(dcDelayCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;

    }

    public async Task<VM_Base> DeleteDelayCatalogueAsync(ModelStateDictionary modelState, VM_DelayCatalogue delayCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref delayCatalogue);

      DCDelayCatalogue dcDelayCatalogue = new DCDelayCatalogue()
      {
        Id = delayCatalogue.Id
      };
      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDeleteDelayCatalogueAsync(dcDelayCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_Delay GetDelay(ModelStateDictionary modelState, long id)
    {
      VM_Delay result = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      using (PEContext ctx = new PEContext())
      {
        DLSDelay delay = ctx.DLSDelays
                       .Include(x => x.MVHRawMaterial)
                       .Include(x => x.DLSDelayCatalogue)
                       .SingleOrDefault(x => x.DelayId == id);
        result = delay == null ? null : new VM_Delay(delay);
      }

      return result;
    }

    public DataSourceResult GetDelayList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.DLSDelays                       
                       .Include(x => x.MVHRawMaterial)
                       .Include(x => x.DLSDelayCatalogue)
                       .OrderBy(o => o.DelayStart)
                    .ToDataSourceResult<DLSDelay, VM_Delay>(request, modelState, x => new VM_Delay(x));
      }

      return result;
    }


    public async Task<VM_Base> UpdateDelayAsync(ModelStateDictionary modelState, VM_Delay delay)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref delay);

      DCDelay dcDelay = new DCDelay()
      {

        Id = delay.Id,
        DelayStart = delay.DelayStart,
        DelayEnd = delay.DelayEnd,
        IsPlanned = delay.IsPlanned,
        Comment = delay.UserComment,
        FkDelayCatalogue = delay.FkDelayCatalogueId
      };


      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDelayAsync(dcDelay);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;

    }

    public IList<VM_DelayCatalogue> GetDelayCatalogue()
    {
      List<VM_DelayCatalogue> result = new List<VM_DelayCatalogue>();
      using (PEContext ctx = new PEContext())
      {

        IQueryable<DLSDelayCatalogue> dbList = ctx.DLSDelayCatalogues.AsQueryable();
        foreach (DLSDelayCatalogue item in dbList)
        {
          result.Add(new VM_DelayCatalogue(item));
        }
      }
      return result;
    }

    public async Task<VM_Base> DivideDelayAsync(ModelStateDictionary modelState, VM_Delay delay)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref delay);

      DCDelayToDivide dcDelayToDivide = new DCDelayToDivide()
      {
        DelayId = delay.Id,
        DurationOfNewDelay = (int)delay.NewDelayLength
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DivideDelayAsync(dcDelayToDivide);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;

    }
  }
}
