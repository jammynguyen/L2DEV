using PE.HMIWWW.ViewModel.System;
using SMF.DbEntity;
using SMF.DbEntity.Model;
using SMF.RepositoryPatternExt;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Threading.Tasks;
using SMF.RepositoryPattern.Infrastructure;
using PE.HMIWWW.Core.Resources;
using SMF.Core.Helpers;
using SMF.Module.Notification;
using PE.Common;
using SMF.Module.Core;
using PE.HMIWWW.Core.Service;
using SMF.DbEntity.Enums;
using System.Data.Entity;

namespace PE.HMIWWW.Services.System
{
  public interface IRoleService
  {
    DataSourceResult GetRolesList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    DataSourceResult GetRightsType(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, string roleId, short rightsType);
    VM_Role InsertRole(ModelStateDictionary modelState, VM_Role viewModel);
    VM_StringId UpdateRole(ModelStateDictionary modelState, VM_Role viewModel);
    VM_StringId DeleteRole(ModelStateDictionary modelState, VM_StringId viewModel);
    VM_Role GetRole(ModelStateDictionary modelState, string id);
    VM_StringId UpdateRight(ModelStateDictionary modelState, string roleRightId, long accessUnitId, string roleId, bool isAssigned, short permission);
    SelectList GetPermissionTypesController();
    SelectList GetPermissionTypesMenu();
    SelectList GetAccessUnitsMenu();
    SelectList GetAccessUnitsController();
  }
  public class RoleService : BaseService, IRoleService
  {
    #region interface IRoleService
    public DataSourceResult GetRolesList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      using (SMFContext SMFctx = new SMFContext())
      {
        //var list = uow.Repository<Role>().Query().Include(z => z.RoleRights).Include(z => z.UserRoles).Get();
        IQueryable<Role> list = SMFctx.Roles.Include(i => i.RoleRights).Include(j => j.UserRoles);
        returnValue = list.ToDataSourceResult<Role, VM_Role>(request, modelState, data => new VM_Role(data));
      }

      return returnValue;
    }

    public VM_Role InsertRole(ModelStateDictionary modelState, VM_Role viewModel)
    {
      VM_Role returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (viewModel.Name == null || viewModel.Name == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (this.IfRoleExists(viewModel.Name))
      {
        AddModelStateError(modelState, @PE.HMIWWW.Core.Resources.VM_Resources.ERROR_MustBeUnique);
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
        //Role role = uow.Repository<Role>().Find(viewModel.Id);
        Role role = new Role();
        role.Name = viewModel.Name;
        role.Id = Guid.NewGuid().ToString();
        role.Description = viewModel.Description;
        //role.State = ObjectState.Added;
        //uow.Repository<Role>().InsertGraph(role);
        SMFctx.Roles.Add(role);
        SMFctx.SaveChanges();
        //uow.SaveChanges();
        returnValueVm = viewModel;
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_StringId UpdateRole(ModelStateDictionary modelState, VM_Role viewModel)
    {
      VM_StringId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (viewModel.Id == null || viewModel.Id == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      //if (this.IfRoleExists(viewModel.Name))//name and id to be checked in method
      //{
      //    AddModelStateError(modelState, @PE.HMIWWW.Core.Resources.VM_Resources.ERROR_MustBeUnique);
      //}
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
        //Role role = uow.Repository<Role>().Find(viewModel.Id);
        Role role = SMFctx.Roles.Where(w => w.Id == viewModel.Id).Single();
        role.Name = viewModel.Name;
        role.Description = viewModel.Description;
        //uow.Repository<Role>().UpdateGraph(role);
        //uow.SaveChanges();
        SMFctx.SaveChanges();
        returnValueVm = new VM_StringId(viewModel.Id);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_StringId DeleteRole(ModelStateDictionary modelState, VM_StringId viewModel)
    {
      VM_StringId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (viewModel.Id == null || viewModel.Id == "")
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
        //Role role = uow.Repository<Role>().Find(viewModel.Id);
        Role role = SMFctx.Roles.Where(w => w.Id == viewModel.Id).Single();
        if (role != null)
        {
          //uow.Repository<Role>().Delete(role);
          SMFctx.Roles.Remove(role);
          //uow.SaveChanges();
          SMFctx.SaveChanges();
          returnValueVm = viewModel;
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_Role GetRole(ModelStateDictionary modelState, string id)
    {
      VM_Role returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (id == null || id == "")
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
        Role user = SMFctx.Roles.Where(w => w.Id == id).Single();
      //Role user = uow.Repository<Role>().Find(id);
        if (user != null)
        {
          returnValueVm = new VM_Role(user);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_StringId UpdateRight(ModelStateDictionary modelState, string roleRightId, long accessUnitId, string roleId, bool isAssigned, short permission)
    {
      VM_StringId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (roleRightId == null || roleRightId == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (accessUnitId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (roleId == null || roleId == "")
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
        //RoleRight entityObject = uow.Repository<RoleRight>().Find(roleRightId);
        //RoleRight entityObject = uow.Repository<RoleRight>().Query(z => z.RoleRightId == roleRightId).GetSingle();
        RoleRight entityObject = SMFctx.RoleRights.FirstOrDefault(w => w.RoleRightId == roleRightId);
        if (isAssigned)
        {
          if (entityObject == null)
          {
            entityObject = new RoleRight();
            entityObject.RoleId = roleId;
            entityObject.AccessUnitId = accessUnitId;
            entityObject.RoleRightId = Guid.NewGuid().ToString();

            if (permission != 0) entityObject.PermissionType = permission;
            //entityObject.State = ObjectState.Added;
            //uow.Repository<RoleRight>().Insert(entityObject);
            SMFctx.RoleRights.Add(entityObject);
            //uow.SaveChanges();
            SMFctx.SaveChanges();
            returnValueVm = new VM_StringId(roleRightId);
          }
          else
          {
            if (permission != 0) entityObject.PermissionType = permission;
            //entityObject.State = ObjectState.Modified;
            //uow.SaveChanges();
            SMFctx.SaveChanges();
            returnValueVm = new VM_StringId(roleRightId);
          }
        }
        else
        {
          if (entityObject != null)
          {
            //entityObject.State = ObjectState.Deleted;
            SMFctx.RoleRights.Remove(entityObject);
            SMFctx.SaveChanges();
            returnValueVm = new VM_StringId(roleRightId);
          }
          else
          {
            returnValueVm = new VM_StringId(roleRightId);
          }
        }

      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public DataSourceResult GetRightsType(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, string roleId, short rightsType)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (roleId == null || roleId == "")
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
        VM_RightList rightListModel = null;
        VM_Right tmpElement = null;
        //IList<AccessUnit> accessUnits = uow.Repository<AccessUnit>().Query(x => (x.AccessUnitType == rightsType)).Get().OrderBy(o => o.Name).ToList();
        IList<AccessUnit> accessUnits = SMFctx.AccessUnits.Where(w => w.AccessUnitType == rightsType).OrderBy(o => o.Name).ToList();

        rightListModel = new VM_RightList(accessUnits, roleId);

        //IList<RoleRight> rights = uow.Repository<RoleRight>()
        //    .Query((x => (x.AccessUnit.AccessUnitType == rightsType) && (x.RoleId == roleId)))
        //    .Include(q => q.AccessUnit)
        //    .Get().ToList();
        IList<RoleRight> rights = SMFctx.RoleRights.Where(x => x.AccessUnit.AccessUnitType == rightsType && x.RoleId == roleId).Include(i => i.AccessUnit).ToList();

        foreach (RoleRight item in rights)
        {
          tmpElement = rightListModel.FirstOrDefault(x => x.AccessUnitId == item.AccessUnitId);
          if (tmpElement != null)
          {
            tmpElement.Id = item.RoleRightId;
            tmpElement.PermissionType = (PermissionType)item.PermissionType;
            tmpElement.RoleId = item.RoleId;
            tmpElement.Assigned = true;
          }
        }
        returnValue = rightListModel.ToDataSourceResult(request);

      }

      return returnValue;
    }

    #region helpers

    public SelectList GetPermissionTypesController()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();
      SelectList tmpSelectList = null;
      try
      {
        foreach (KeyValuePair<int, string> item in EnumHelper.EnumToDictionary(typeof(SMF.DbEntity.Enums.PermissionType)))
        {
          resultDictionary.Add((int)item.Key, VM_Resources.ResourceManager.GetString(string.Format("GLOB_PermissionType_{0}", item.Key)));
        }
        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.Alarm_ExceptionInViewBag, String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name, ex.Message), MethodBase.GetCurrentMethod().Name, ex.Message);
        LogHelper.LogException( ex, String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
      }
      return tmpSelectList;
    }
    public SelectList GetPermissionTypesMenu()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();
      SelectList tmpSelectList = null;
      try
      {
        resultDictionary.Add(1, VM_Resources.ResourceManager.GetString(string.Format("GLOB_PermissionType_1")));
        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.Alarm_ExceptionInViewBag, String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name, ex.Message), MethodBase.GetCurrentMethod().Name, ex.Message);
        LogHelper.LogException( ex, String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
      }
      return tmpSelectList;
    }
    public SelectList GetAccessUnitsMenu()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();
      SelectList tmpSelectList = null;
      try
      {
        //using (SMFUnitOfWork uow = new SMFUnitOfWork())
        //{
        using (SMFContext SMFctx = new SMFContext())
        {
          //IList<AccessUnit> accessUnits = uow.Repository<AccessUnit>().Query(x => (x.AccessUnitType == (short)SMF.DbEntity.Enums.AccessUnitType.HmiMenu)).Get().OrderBy(o => o.Name).ToList();
          IList<AccessUnit> accessUnits = SMFctx.AccessUnits.Where(w => w.AccessUnitType == (short)SMF.DbEntity.Enums.AccessUnitType.HmiMenu).OrderBy(o => o.Name).ToList();

          foreach (AccessUnit item in accessUnits)
          {
            resultDictionary.Add((int)item.AccessUnitId, String.Format("{0}", VM_Resources.ResourceManager.GetString(item.Name)));
          }
        }
        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.Alarm_ExceptionInViewBag, String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name, ex.Message), MethodBase.GetCurrentMethod().Name, ex.Message);
        LogHelper.LogException( ex, String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
      }
      return tmpSelectList;
    }
    public SelectList GetAccessUnitsController()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();
      SelectList tmpSelectList = null;
      try
      {
        //using (SMFUnitOfWork uow = new SMFUnitOfWork())
        //{
        using (SMFContext SMFctx = new SMFContext())
        {
          //IList<AccessUnit> accessUnits = uow.Repository<AccessUnit>().Query(x => (x.AccessUnitType == (short)SMF.DbEntity.Enums.AccessUnitType.Controller)).Get().OrderBy(o => o.Name).ToList();
          IList<AccessUnit> accessUnits = SMFctx.AccessUnits.Where(w => w.AccessUnitType == (short)SMF.DbEntity.Enums.AccessUnitType.Controller).OrderBy(o => o.Name).ToList();
          foreach (AccessUnit item in accessUnits)
          {
            string translation = VM_Resources.ResourceManager.GetString(item.Name);
            resultDictionary.Add((int)item.AccessUnitId, String.Format("{0}", (translation == null || translation == "") ? item.Name : translation));
          }
        }
        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.Alarm_ExceptionInViewBag, String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name, ex.Message), MethodBase.GetCurrentMethod().Name, ex.Message);
        LogHelper.LogException( ex, String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
      }
      return tmpSelectList;
    }

    #endregion
    #endregion

    #region public methods

    #endregion

    #region private methods
    /// <summary>
    /// Get list of all roles in system.
    /// </summary>
    /// <returns>List of Roles object.</returns>
    private List<Role> GetAllRoles()
    {
      List<Role> RolesList = new List<Role>();
      try
      {
        //using (SMFUnitOfWork uow = new SMFUnitOfWork())
        //{
        using (SMFContext SMFctx = new SMFContext())
        {
          //RolesList = uow.Repository<Role>().Query()
          //                                                                    .Include(z => z.RoleRights)
          //                                                                    .Include(z => z.UserRoles)
          //                                                                    .Get().ToList();
          RolesList = SMFctx.Roles.Include(i => i.RoleRights).Include(j => j.UserRoles).ToList();
        }
      }
      catch (Exception ex)
      {
        DbExceptionResult result = DbExceptionHelper.ProcessException(ex, "GetAllRoles::Database operation failed!", null);
      }
      return RolesList;

    }

    private bool IfRoleExists(string roleName)
    {
      bool retValue;
      try
      {
        //using (SMFUnitOfWork uow = new SMFUnitOfWork())
        //{
        using (SMFContext SMFctx = new SMFContext())
        {
          //Role role = uow.Repository<Role>().Query(z => z.Name == roleName).GetSingle();
          Role role = SMFctx.Roles.Where(w => w.Name == roleName).Single();
          if (role != null)
            retValue = true;
          else
            retValue = false;
        }
      }
      catch (Exception ex)
      {
        DbExceptionResult result = DbExceptionHelper.ProcessException(ex, "IfRoleExists::Database operation failed!", null);
        retValue = true;
      }
      return retValue;
    }



    #endregion


  }
}
