using PE.HMIWWW.ViewModel.System;
using SMF.DbEntity;
using SMF.DbEntity.Model;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Threading.Tasks;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using System.Data.Entity;

namespace PE.HMIWWW.Services.System
{
  public interface IAccountService
  {
    DataSourceResult GetAccountList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);

    DataSourceResult GetRolesList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, string Id);

    VM_Account GetAccount(ModelStateDictionary modelState, string id);

    VM_Account UpdateAccount(ModelStateDictionary modelState, VM_Account viewModel);

    VM_StringId DeleteAccount(ModelStateDictionary modelState, VM_StringId viewModel);

    VM_StringId UpdateUserInRole(ModelStateDictionary modelState, string roleId, string userId, string isAssigned);
    List<VM_LanguageItem> GetLanguages();
  }
  public class AccountService : BaseService, IAccountService
  {
    #region interface IAccountService

    public DataSourceResult GetAccountList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
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
        //var list = uow.Repository<User>().Query()
        //                                      .Include(z => z.UserLogins)
        //                                      .Include(z => z.UserRoles.Select(y => y.Role))
        //                                      .Include(z => z.UserClaims)
        //                                      .Get();
        IEnumerable<User> list = SMFctx.Users.Include(i => i.UserLogins).Include(j => j.UserRoles.Select(x => x.Role)).Include(k => k.UserClaims).ToList();
        returnValue = list.ToDataSourceResult<User, VM_Account>(request, modelState, data => new VM_Account(data));
      }

      return returnValue;
    }

    public DataSourceResult GetRolesList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, string id)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (id == string.Empty)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION
      User user = GetUserById(id);
      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      using (SMFContext SMFctx = new SMFContext())
      {
        //var list = uow.Repository<Role>().Query().Get();
        IEnumerable<Role> list = SMFctx.Roles.ToList();
        returnValue = list.ToDataSourceResult<Role, VM_UserInRoles>(request, modelState, data => new VM_UserInRoles(user, data));
      }

      return returnValue;
    }

    public VM_Account GetAccount(ModelStateDictionary modelState, string id)
    {
      VM_Account returnValueVm = new VM_Account();

      //VALIDATE ENTRY PARAMETERS
      if (id == string.Empty)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION


      //DB OPERATION
      // using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      using (SMFContext SMFctx = new SMFContext())
      {
        //User user = uow.Repository<User>().Find(id);
        User user = SMFctx.Users.Find(id);
        if (user != null)
        {
          returnValueVm = new VM_Account(user);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_Account UpdateAccount(ModelStateDictionary modelState, VM_Account viewModel)
    {
      VM_Account returnValueVm = viewModel;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (viewModel.Id == string.Empty)
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
        //User user = uow.Repository<User>().Find(viewModel.Id);
        User user = SMFctx.Users.Find(viewModel.Id);
        user.UserName = viewModel.UserName;
        user.FirstName = viewModel.FirstName;
        user.LastName = viewModel.LastName;
        user.JobPosition = viewModel.JobPosition;
        user.HMIViewOrientation = (Int16)(viewModel.LeftToRight ? 1 : 0);
        //uow.Repository<User>().UpdateGraph(user);
        //uow.SaveChanges();
        SMFctx.SaveChanges();
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_StringId DeleteAccount(ModelStateDictionary modelState, VM_StringId viewModel)
    {
      VM_StringId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (viewModel.Id == String.Empty)
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
        //User role = uow.Repository<User>().Find(viewModel.Id);
        User role = SMFctx.Users.Find(viewModel.Id);
        if (role != null)
        {
          //uow.Repository<User>().Delete(role);
          SMFctx.Users.Remove(role);
          //uow.SaveChanges();
          SMFctx.SaveChanges();
          returnValueVm = viewModel;
        }
      }
      //END OF DB OPERATION
      return returnValueVm;
    }

    public VM_StringId UpdateUserInRole(ModelStateDictionary modelState, string roleId, string userId, string isAssigned)
    {
      VM_StringId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (roleId == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (userId == string.Empty)
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
        //User user = uow.Repository<User>().Query(z => z.Id == userId).GetSingle();
        //Role role = uow.Repository<Role>().Query(z => z.Id == roleId).GetSingle();
        User user = SMFctx.Users.Where(w => w.Id == userId).Single();
        Role role = SMFctx.Roles.Where(w => w.Id == roleId).Single();
        returnValueVm = new VM_StringId(user.Id);
        if (user != null && role != null)
        {
          if (isAssigned == "true")
          {
            UserRole userRole = new UserRole();
            userRole.RoleId = role.Id;
            userRole.UserId = user.Id;
            userRole.Role = role;
            userRole.User = user;
            //userRole.State = SMF.RepositoryPattern.Infrastructure.ObjectState.Added;
            SMFctx.UserRoles.Add(userRole);
            //uow.Repository<UserRole>().InsertOrUpdateGraph(userRole);
            //uow.SaveChanges();
            SMFctx.SaveChanges();
          }
          else
          {
            //UserRole userRole = uow.Repository<UserRole>().Query(z => (z.UserId == user.Id) && (z.RoleId == role.Id)).GetSingle();
            UserRole userRole = SMFctx.UserRoles.Where(w => w.UserId == user.Id && w.RoleId == role.Id).Single();
            //uow.Repository<UserRole>().Delete(userRole);
            SMFctx.UserRoles.Remove(userRole);
            //uow.SaveChanges();
            SMFctx.SaveChanges();
          }

        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }


    public List<VM_LanguageItem> GetLanguages()
    {
      List<VM_LanguageItem> vLanguageItemList = new List<VM_LanguageItem>();

      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      using (SMFContext SMFctx = new SMFContext())
      {
        //List<Language> languageList = uow.Repository<Language>().Query(z => z.Active == true).Get().ToList();
        List<Language> languageList = SMFctx.Languages.Where(w => w.Active == true).ToList();
        if (languageList.Count > 0)
        {
          vLanguageItemList = new VM_LanguageItemList(languageList);
        }
      }
      return vLanguageItemList;
    }
    #endregion
    #region public methods

    #endregion
    #region private methods

    private User GetUserById(string Id)
    {
      User User = new User();

      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      using (SMFContext SMFctx = new SMFContext())
      {
        //User = uow.Repository<User>().Query(z => z.Id == Id)
        //                                                                    .Include(z => z.UserLogins)
        //                                                                    .Include(z => z.UserRoles.Select(y => y.Role))
        //                                                                    .Include(z => z.UserClaims)
        //                                                                    .GetSingle();
        User = SMFctx.Users.Where(w => w.Id == Id).Include(i => i.UserLogins).Include(i => i.UserRoles.Select(s => s.Role)).Include(i => i.UserClaims).Single();
      }

      return User;
    }

    #endregion
  }
}
