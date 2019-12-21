using SMF.DbEntity.Model;
using System.Collections.Generic;
using System.Linq;
using SMF.DbEntity;
using System;
using System.Reflection;
using PE.HMIWWW.Core.Authorization;
using System.Web.Mvc;
using SMF.RepositoryPattern.Repositories;
using SMF.RepositoryPattern.Infrastructure;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services;
using PE.HMIWWW.ViewModel.System;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using PE.HMIWWW.Core.Service;
using System.Data.Entity;
using SMF.DbEntity.Enums;

namespace PE.Services.System
{
  public interface IAccessUnitsService
  {
    DataSourceResult UserRightsPopulate(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    DataSourceResult GetExistedAccessUnits(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
  }

  public class AccessUnitsService : BaseService, IAccessUnitsService
  {
    #region interface IMainMenuService    
    private readonly SMFContext smfctx = new SMFContext();
    public DataSourceResult UserRightsPopulate(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      List<VM_UserRights> userRightsFromControllers = this.GetUserRightsFromControllers().ToList();
      IList<string> userRightsFromDB = this.GetExistingUserRightsFromDB().ToList();

      List<VM_UserRights> itemsAdded = new List<VM_UserRights>();
      IList<AccessUnit> accessUnitList = smfctx.AccessUnits.Include(x => x.Module).ToList();

      foreach (VM_UserRights userRight in userRightsFromControllers)
      {
        if (!userRightsFromDB.Contains(userRight.AccessName))
        {
          //ToDo: wstawianie batchowe
          
            AccessUnit newRight = new AccessUnit()
            {
              Name = userRight.AccessName,
              AccessUnitType = (short)SMF.DbEntity.Enums.AccessUnitType.Controller,
              //Module = userRight.Module,
              FKModuleId = userRight.Module.Id
            };
          if (!accessUnitList.Any(x => x.Name == userRight.AccessName))
          {
            smfctx.AccessUnits.Add(newRight);
            accessUnitList.Add(newRight);
          }
          //  smfctx.SaveChanges();
          foreach (VM_UserRights itemAdded in userRightsFromControllers.Where(ur => ur.AccessName == userRight.AccessName))
          {
            itemsAdded.Add(new VM_UserRights(itemAdded));
          }
          
            userRightsFromDB.Add(newRight.Name);
        }
      }
      smfctx.SaveChanges();
      result = itemsAdded.ToDataSourceResult(request, modelState);

      return result;
    }

    public DataSourceResult GetExistedAccessUnits(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;
      List<AccessUnit> list;
      // SMFContext SMFctx = new SMFContext();
      smfctx.Configuration.LazyLoadingEnabled = false;
      list = smfctx.AccessUnits.Include(x => x.Module).AsNoTracking().ToList();
      foreach (AccessUnit item in list)
      {
        item.Module.HmiClientMenus = null;
        item.Module.AccessUnits = null;
      }

      result = list.ToDataSourceResult<AccessUnit, VM_UserRights>(request, modelState, data => new VM_UserRights(data));

      return result;
    }

    #endregion

    #region private methods	
    private IList<string> GetExistingUserRightsFromDB()
    {
      List<string> existingUserRights = new List<string>();
      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      //using (SMFContext SMFctx = new SMFContext())
      //{
        existingUserRights = smfctx.AccessUnits.Select(x => x.Name).ToList();
        //existingUserRights = uow.Repository<AccessUnit>()
        //									.Query()
        //									.Get()
        //									.Select(s => s.Name)
        //									.ToList();
     // }

      return existingUserRights;
    }

    private IList<VM_UserRights> GetUserRightsFromControllers()
    {
      List<VM_UserRights> userRights = new List<VM_UserRights>();

      Assembly assembly =/* Assembly.GetExecutingAssembly();*/Assembly.Load("PE.HMIWWW");
      Type[] types = assembly.GetTypes();
      foreach (Type controller in types.Where(c => ((c.BaseType.FullName == typeof(BaseController).FullName) || c.BaseType.FullName == typeof(Controller).FullName)))
      {
        MethodInfo[] methods = controller.GetMethods();
        foreach (MethodInfo method in methods)
        {
          object[] attributes = method.GetCustomAttributes(typeof(SmfAuthorization), false);
          foreach (SmfAuthorization attribute in attributes)
          {
            VM_UserRights urm = new VM_UserRights();
            urm.Name = controller.Name;
            urm.Method = method.Name;
            urm.AccessName = attribute.AuthObjectName;
            urm.Module = GetOrCreateModuleByName(attribute.AuthModule);

            if (!userRights.Contains(urm))
            {
              userRights.Add(urm);
            }
          }
        }
      }

      return userRights;
    }

    private SMF.DbEntity.Model.Module GetOrCreateModuleByName(string moduleName)
    {
      SMF.DbEntity.Model.Module module = new SMF.DbEntity.Model.Module();
      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      //using (SMFContext SMFctx = new SMFContext())
      //{
        module = smfctx.Modules.FirstOrDefault(x => x.ModuleName == moduleName);
        //module = uow.Repository<SMF.DbEntity.Model.Module>().Query(z=>z.ModuleName == moduleName).GetSingle();
        if (module == null)
        {
          module = new SMF.DbEntity.Model.Module();
          module.ModuleName = moduleName;
        //module.State = ObjectState.Added;
        smfctx.Modules.Add(module);
          //uow.Repository<SMF.DbEntity.Model.Module>().Insert(module);
          //uow.SaveChanges();
          smfctx.SaveChanges();
        }
     // }
      return module;
    }





    #endregion
  }
}
