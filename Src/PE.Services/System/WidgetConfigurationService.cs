using PE.HMIWWW.ViewModel.System;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.DbEntity.Models;

namespace PE.HMIWWW.Services.System
{
  public interface IWidgetConfigurationService
  {
    DataSourceResult GetWidgetConfigurationsData(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, string userId);
    VM_WidgetConfigurations GetWidgetConfiguration(ModelStateDictionary modelState, long id);
    VM_LongId UpdateWidgetConfiguration(ModelStateDictionary modelState, VM_WidgetConfigurations viewModel);
    VM_LongId DeleteWidgetConfiguration(ModelStateDictionary modelState, VM_LongId viewModel);
    VM_LongId InsertWidgetConfiguration(ModelStateDictionary modelState, VM_WidgetConfigurations viewModel);
  }
  public class WidgetConfigurationService : BaseService, IWidgetConfigurationService
  {
    #region interface IWidgetConfigurationService
    public DataSourceResult GetWidgetConfigurationsData(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, string userId)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      //DB OPERATION
      using (PEContext uow = new PEContext())
      {
        List<HMIWidgetConfiguration> list = uow.HMIWidgetConfigurations.Where(z => z.FKUserId == userId).ToList();
        returnValue = list.ToDataSourceResult<HMIWidgetConfiguration, VM_WidgetConfigurations>(request, modelState, data => new VM_WidgetConfigurations(data));
      }
      //END OF DB OPERATION

      return returnValue;
    }

    public VM_WidgetConfigurations GetWidgetConfiguration(ModelStateDictionary modelState, long id)
    {
      VM_WidgetConfigurations returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (id == 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION


      //DB OPERATION
      using (PEContext uow = new PEContext())
      {
        HMIWidgetConfiguration widgetConfiguration = uow.HMIWidgetConfigurations.Where(z => z.WidgetConfigurationId == id).Single();
        if (widgetConfiguration != null)
        {
          returnValueVm = new VM_WidgetConfigurations(widgetConfiguration);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public VM_LongId UpdateWidgetConfiguration(ModelStateDictionary modelState, VM_WidgetConfigurations viewModel)
    {
      VM_LongId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.Id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      using (PEContext uow = new PEContext())
      {
        HMIWidgetConfiguration widgetConfiguration = uow.HMIWidgetConfigurations.Where(z => z.WidgetConfigurationId == (long)viewModel.Id).Single();
        widgetConfiguration.WidgetName = viewModel.WidgetName;
        widgetConfiguration.WidgetFileName = viewModel.WidgetFileName;
        widgetConfiguration.IsActive = viewModel.Active;
        //widgetConfiguration.State = ObjectState.Modified;
        //uow.Repository<WidgetConfiguration>().Update(widgetConfiguration);
        uow.SaveChanges();
        returnValueVm = new VM_LongId((long)viewModel.Id);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public VM_LongId DeleteWidgetConfiguration(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_LongId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.Id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      using (PEContext uow = new PEContext())
      {
        HMIWidgetConfiguration widgetConfiguration = uow.HMIWidgetConfigurations.Where(z => z.WidgetConfigurationId == viewModel.Id).Single();
        if (widgetConfiguration != null)
        {
          uow.HMIWidgetConfigurations.Remove(widgetConfiguration);
          uow.SaveChanges();
          returnValueVm = new VM_LongId(viewModel.Id);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public VM_LongId InsertWidgetConfiguration(ModelStateDictionary modelState, VM_WidgetConfigurations viewModel)
    {
      VM_LongId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.WidgetName == "" || viewModel.WidgetFileName == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      using (PEContext uow = new PEContext())
      {
        //Role role = uow.Repository<Role>().Find(viewModel.Id);
        HMIWidgetConfiguration widgetConfiguration = new HMIWidgetConfiguration();
        widgetConfiguration.WidgetName = viewModel.WidgetName;
        widgetConfiguration.WidgetFileName = viewModel.WidgetFileName;
        widgetConfiguration.IsActive = viewModel.Active;
        //widgetConfiguration.State = ObjectState.Added;
        uow.HMIWidgetConfigurations.Add(widgetConfiguration);
        uow.SaveChanges();

        returnValueVm = new VM_LongId(widgetConfiguration.WidgetConfigurationId);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    #endregion

    #region public methods
    public static List<VM_WidgetConfigurations> GetVMWidgetConfigurationsList(string userId)
    {
      List<HMIWidgetConfiguration> widgetConfiguration = null;
      List<VM_WidgetConfigurations> vmList = new List<VM_WidgetConfigurations>();
      try
      {
        using (PEContext uow = new PEContext())
        {
          widgetConfiguration = uow.HMIWidgetConfigurations.Where(z => z.IsActive == true && z.FKUserId == userId).OrderBy(z => z.OrderSeq).ToList();
          foreach (HMIWidgetConfiguration element in widgetConfiguration)
          {
            vmList.Add(new VM_WidgetConfigurations(element));
          }
        }
      }
      catch (Exception ex)
      {
        DbExceptionResult result = DbExceptionHelper.ProcessException(ex, "GetVMWidgetConfigurationsList::Database operation failed!", null);
      }
      return vmList;
    }
    #endregion


  }
}
