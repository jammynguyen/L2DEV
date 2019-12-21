using System;
using SMF.DbEntity.Model;
using SMF.DbEntity;
using System.Collections.Generic;
using PE.HMIWWW.Services;
using PE.HMIWWW.ViewModel.System;
using Kendo.Mvc.UI;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using SMF.RepositoryPattern.Infrastructure;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Core.Service;
using PE.DbEntity.Model;
using PE.DbEntity;
using System.Linq;
using SMF.RepositoryPattern.Ef6;
using SMF.RepositoryPattern.Repositories;

namespace PE.Services.System
{
  public interface IAlarmsService
  {
    DataSourceResult GetAlarmList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, string languageCode);
    DataSourceResult GetShortAlarmList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, string languageCode);
    VM_Base Confirm(ModelStateDictionary modelState, long alarmId, string userId);
    VM_AlarmItem GetDetails(ModelStateDictionary modelState, long alarmId);
  }

  public class AlarmsService : BaseService, IAlarmsService
  {
    #region interface IAlarmsService

    public DataSourceResult GetAlarmList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, string languageCode)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (languageCode == null || languageCode == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      using (SMFContext SMFctx = new SMFContext())
      {
        //IQueryObject<SMF.DbEntity.Model.Alarm> queryObj = new QueryObject<SMF.DbEntity.Model.Alarm>();
        //queryObj.And(x => x.LanguageCode == languageCode);

        //var list = uow.Repository<SMF.DbEntity.Model.Alarm>().Query(queryObj).Get();
        IQueryable<Alarm> list = SMFctx.Alarms.Where(w => w.LanguageCode == languageCode).AsQueryable();

        returnValue = list.ToDataSourceResult<SMF.DbEntity.Model.Alarm, VM_AlarmItem>(request, modelState, data => new VM_AlarmItem(data));
      }

      return returnValue;
    }


    public DataSourceResult GetShortAlarmList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, string languageCode)
    {
      DataSourceResult returnValue = null;
      //VALIDATE ENTRY PARAMETERS
      if (languageCode == null || languageCode == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      using (PE.DbEntity.Models.PEContext uow = new PE.DbEntity.Models.PEContext())
      {
        IList<PE.DbEntity.Models.V_NewestAlarmsList> list = uow.V_NewestAlarmsList.Where(z => z.LanguageCode == languageCode).OrderBy(z => z.RowNumber).ToList();
        returnValue = list.ToDataSourceResult<PE.DbEntity.Models.V_NewestAlarmsList, VM_AlarmShortItem>(request, modelState, data => new VM_AlarmShortItem(data));

      }
      return returnValue;
    }
    public VM_Base Confirm(ModelStateDictionary modelState, long alarmId, String userId)
    {
      VM_Base returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (alarmId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
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
        //Alarm alarm = uow.Repository<Alarm>().Find(alarmId);
        Alarm alarm = SMFctx.Alarms.Find(alarmId);
        if (alarm == null || alarm.Confirmation == true)
        {
          AddModelStateError(modelState, ResourceController.GetErrorText("NoConfirmationNeeded"));
          return returnValueVm;
        }

        //alarm.State = ObjectState.Modified;
        alarm.Confirmation = true;
        alarm.ConfirmationDate = DateTime.Now;
        alarm.ConfirmedBy = userId;
        //uow.Repository<Alarm>().Update(alarm);
        //uow.SaveChanges();
        SMFctx.SaveChanges();
        returnValueVm = new VM_Base();
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_AlarmItem GetDetails(ModelStateDictionary modelState, long alarmId)
    {
      VM_AlarmItem returnValueVm = new VM_AlarmItem();

      //VALIDATE ENTRY PARAMETERS
      if (alarmId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
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
        //Alarm alarm = uow.Repository<Alarm>().Find(alarmId);
        Alarm alarm = SMFctx.Alarms.Find(alarmId);
        if (alarm != null)
        {
          returnValueVm = new VM_AlarmItem(alarm);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    #endregion

  }
}
