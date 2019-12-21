using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.Common;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Interfaces;
using SMF.DbEntity;
using SMF.DbEntity.Enums;
using SMF.DbEntity.Model;
using SMF.Module.Core;
using SMF.Watchdog.Communication;
using System;
using System.ServiceModel;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;

namespace PE.HMIWWW.Services.System
{
  public interface ILimitService
  {
    DataSourceResult GetLimits(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    VM_Limit UpdateLimit(ModelStateDictionary modelState, VM_Limit viewModel);
  }

  public class LimitService : BaseService, ILimitService
  {
    #region interface ILimitService

    public DataSourceResult GetLimits(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
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
        //var list = uow.Repository<Limit>()
        //                                .Query()
        //                                .Include(q => q.ParameterGroup)
        //                                .Include(q => q.UnitOfMeasure)
        //                                .Get();
        IQueryable<Limit> list = SMFctx.Limits.Include(i => i.ParameterGroup).Include(j => j.UnitOfMeasure);
        returnValue = list.ToDataSourceResult<Limit, VM_Limit>(request, modelState, data => new VM_Limit(data));
      }
      //END OF DB OPERATION

      return returnValue;
    }

    public VM_Limit UpdateLimit(ModelStateDictionary modelState, VM_Limit viewModel)
    {
      VM_Limit returnValueVm = null;

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
        //Limit limit = uow.Repository<Limit>().Find(viewModel.Id);
        Limit limit = SMFctx.Limits.Where(x => x.LimitId == viewModel.Id).Single();

        switch (limit.ValueType)
        {
          case (int)LimitValueType.ValueDate:
            {
              limit.LowerValueDate = DateTime.Parse(viewModel.LowerValue);
              limit.UpperValueDate = DateTime.Parse(viewModel.UpperValue);

              if (limit.LowerValueDate > limit.UpperValueDate)
              {
                DateTime? tempDate = limit.LowerValueDate;
                limit.LowerValueDate = limit.UpperValueDate;
                limit.UpperValueDate = tempDate;
              }
              break;
            }
          case (int)LimitValueType.ValueFloat:
            {
              limit.LowerValueFloat = double.Parse(viewModel.LowerValue);
              limit.UpperValueFloat = double.Parse(viewModel.UpperValue);

              if (limit.LowerValueFloat > limit.UpperValueFloat)
              {
                double? tempFloat = limit.LowerValueFloat;
                limit.LowerValueFloat = limit.UpperValueFloat;
                limit.UpperValueFloat = tempFloat;
              }

              break;
            }
          case (int)LimitValueType.ValueInt:
            {
              limit.LowerValueInt = int.Parse(viewModel.LowerValue);
              limit.UpperValueInt = int.Parse(viewModel.UpperValue);

              if (limit.LowerValueInt > limit.UpperValueInt)
              {
                int? tempInt = limit.LowerValueInt;
                limit.LowerValueInt = limit.UpperValueInt;
                limit.UpperValueInt = tempInt;
              }

              break;
            }
        }
        //uow.Repository<Limit>().Update(limit);
        //uow.SaveChanges();
        SMFctx.SaveChanges();
        ModuleController.HmiRefresh(HMIRefreshKeys.Limits);
        returnValueVm = viewModel;
        LimitUpdateBroadcast(modelState, viewModel.GroupName);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    #endregion
    #region private methods

    private VM_Base LimitUpdateBroadcast(ModelStateDictionary modelState, string groupName)
    {
      VM_Base tmpRetValue = new VM_Base();
      IHmiModule client = null;
      try
      {
        client = WatchdogInterfaceHelper.GetModuleFactoryChannel<IHmiModule>(SMF.Core.Constants.HmiProcessName);
        if (client != null)
        {
          client.LimitChangeFromWWW(groupName);

          WatchdogInterfaceHelper.CloseChannel((IClientChannel)client);
        }
      }
      catch (Exception ex)
      {
        if (client != null)
        {
          WatchdogInterfaceHelper.AbortChannel((IClientChannel)client);
        }

        tmpRetValue = new VM_Base();
        SetCommunicationError(modelState);
        Logger.Trace(ex);
      }
      return tmpRetValue;
    }

    #endregion
  }
}
