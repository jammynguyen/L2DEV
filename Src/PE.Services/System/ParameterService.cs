using System;
using SMF.DbEntity.Model;
using SMF.DbEntity.Enums;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Interfaces;
using SMF.Watchdog.Communication;
using System.ServiceModel;
using Kendo.Mvc.UI;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using PE.HMIWWW.Core.Parameter;
using PE.HMIWWW.Core.Service;
using SMF.DbEntity;
using System.Data.Entity;
using System.Linq;

namespace PE.HMIWWW.Services.System
{
  public interface IParameterService
  {
    DataSourceResult GetParameters(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    VM_Parameter UpdateParameter(ModelStateDictionary modelState, VM_Parameter viewModel);
  }

  public class ParameterService : BaseService, IParameterService
  {
    #region interface

    public DataSourceResult GetParameters(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      //DB OPERATION
      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      using (SMFContext SMFctx = new SMFContext())
      {
        IQueryable<Parameter> list = SMFctx.Parameters.Include(i => i.ParameterGroup).Include(j => j.UnitOfMeasure);
        //var list = uow.Repository<Parameter>()
        //                                .Query()
        //                                .Include(q => q.ParameterGroup).Include(q => q.UnitOfMeasure)
        //                                .Get();
        returnValue = list.ToDataSourceResult<Parameter, VM_Parameter>(request, modelState, data => new VM_Parameter(data));
      }
      //END OF DB OPERATION

      return returnValue;
    }
    public VM_Parameter UpdateParameter(ModelStateDictionary modelState, VM_Parameter viewModel)
    {
      VM_Parameter returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      using (SMFContext SMFctx = new SMFContext())
      {
        //Parameter parameter = uow.Repository<Parameter>().Find(viewModel.Id);
        Parameter parameter = SMFctx.Parameters.Where(x => x.ParameterId == viewModel.Id).Single();

        if (parameter != null)
        {
          parameter.Description = viewModel.Description;
          parameter.Name = viewModel.Name;
          parameter.ParameterGroupId = viewModel.GroupId;
          switch (parameter.ValueType)
          {
            case (int)ParameterValueType.ValueDate:
              {
                parameter.ValueDate = DateTime.Parse(viewModel.Value);
                break;
              }
            case (int)ParameterValueType.ValueFloat:
              {
                parameter.ValueFloat = double.Parse(viewModel.Value);
                break;
              }
            case (int)ParameterValueType.ValueInt:
              {
                parameter.ValueInt = Int32.Parse(viewModel.Value);
                break;
              }
            case (int)ParameterValueType.ValueText:
              {
                parameter.ValueText = viewModel.Value;
                break;
              }
          }
          //uow.Repository<Parameter>().Update(parameter);
          //uow.SaveChanges();
          SMFctx.SaveChanges();
          returnValueVm = viewModel;
          ParameterUpdateBroadcast(modelState, viewModel.GroupName);
        }
      }
      //END OF DB OPERATION      
      return returnValueVm;
    }

    #endregion
    #region private methods

    private void ParameterUpdateBroadcast(ModelStateDictionary modelState, string groupName)
    {
      ParameterController.ReadParamaters();

      IHmiModule client = null;
      try
      {
        client = WatchdogInterfaceHelper.GetModuleFactoryChannel<IHmiModule>(SMF.Core.Constants.HmiProcessName);
        if (client != null)
        {
          client.ParameterChangeFromWWW(groupName);

          WatchdogInterfaceHelper.CloseChannel((IClientChannel)client);
        }
      }
      catch (Exception ex)
      {
        if (client != null)
          WatchdogInterfaceHelper.AbortChannel((IClientChannel)client);

        SetCommunicationError(modelState);
        Logger.Trace(ex);
      }
    }

    #endregion
  }
}
