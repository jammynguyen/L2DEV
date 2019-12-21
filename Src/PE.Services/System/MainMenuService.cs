using SMF.DbEntity.Model;
using System.Collections.Generic;
using System.Linq;
using SMF.DbEntity;
using SMF.RepositoryPatternExt;
using SMF.Module.Core;
using System;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services;
using PE.HMIWWW.ViewModel.System;
using System.Web.Mvc;
using System.Data.Entity;

namespace PE.Services.System
{
  public interface IMainMenuService// :  IHmiServiceBase
  {
    VM_Menu GetMainMenu(ModelStateDictionary modelState, string user);
  }

  public class MainMenuService : BaseService, IMainMenuService
  {
    #region interface IMainMenuService
    public VM_Menu GetMainMenu(ModelStateDictionary modelState, string user)
    {
      VM_Menu vMenu = new VM_Menu();
      bool isAdmin = false;
      try
      {
        vMenu.Menuitems = GetParentItems(user);
        if (user != string.Empty)
        {
          //check whether use is superuser and complete menu should be displayed
          //using (SMFUnitOfWork uow = new SMFUnitOfWork())
          //{
          using (SMFContext SMFctx = new SMFContext())
          {
            //User dbUser = uow.Repository<User>().Query(z => z.UserName == user)
            //                                                                            .Include(z => z.UserClaims)   //.Select(x => x.ClaimType == "admin" && x.ClaimValue == "true")
            //                                                                            .Get().FirstOrDefault();
            User dbUser = SMFctx.Users.Where(w => w.UserName == user).Include(i => i.UserClaims).FirstOrDefault();

            if (dbUser != null && dbUser.UserClaims.Count > 0)
            {
              List<UserClaim> adminClaims = dbUser.UserClaims.Where(x => x.ClaimType == "admin" && x.ClaimValue == "true").ToList();
              if (adminClaims != null && adminClaims.Count > 0)
                isAdmin = true;
            }

          }
        }

        foreach (VM_MenuItem parent in vMenu.Menuitems)
        {
          GetChildren(parent, user, isAdmin);
        }

        vMenu.Menuitems.RemoveAll(x => ((x.Children == null || x.Children.Count == 0) && x.Controller == null));

        vMenu.Languages = GetMainMenuLanguageItems();

      }
      catch (Exception ex)
      {
        AddModelStateError(modelState, ex);
      }
      return vMenu;
    }

    #endregion

    #region private methods

    /// <summary>
    /// Return view model with main menu parent items.
    /// </summary>
    /// <returns>View model list object if found or null.</returns>
    private VM_MenuItemList GetParentItems(string user)
    {
      VM_MenuItemList vmMenuItemList = new VM_MenuItemList();

      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      using (SMFContext SMFctx = new SMFContext())
      {
        // List<HmiClientMenu> menuList = uow.Repository<HmiClientMenu>().Query(z => z.ParentId == null && z.Active == true).Get().ToList();
        List<HmiClientMenu> menuList = SMFctx.HmiClientMenus.Where(x => x.ParentId == null && x.Active == true).ToList();
        vmMenuItemList = new VM_MenuItemList(menuList);
      }
      return vmMenuItemList;
    }

    /// <summary>
    /// Return view model with main menu child items.
    /// <param name="parent">VM_MenuItem</param>
    /// </summary>
    /// <returns>View model list object if found or null.</returns>
    private VM_MenuItemList GetChildrenItems(VM_MenuItem parent, string user, bool isAdmin = false)
    {
      VM_MenuItemList vmMenuItemList = new VM_MenuItemList();

      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      using (SMFContext SMFctx = new SMFContext())
      {
        if (!isAdmin)
        {
          //List<V_HMIUserMenu> menuList = uow.Repository<V_HMIUserMenu>().Query(z => z.ParentId == parent.Id && z.Active == true && z.UserName == user).Get().ToList();
          List<V_HMIUserMenu> menuList = SMFctx.V_HMIUserMenu.Where(x => x.ParentId == parent.Id && x.Active == true && x.UserName == user).ToList();
          vmMenuItemList = new VM_MenuItemList(menuList);
        }
        else
        {
          //List<HmiClientMenu> menuList = uow.Repository<HmiClientMenu>().Query(z => z.ParentId == parent.Id && z.Active == true).Get().ToList();
          List<V_HMIUserMenu> menuList = SMFctx.V_HMIUserMenu.Where(x => x.ParentId == parent.Id && x.Active == true).ToList();
          vmMenuItemList = new VM_MenuItemList(menuList);
        }
      }
      return vmMenuItemList;
    }

    /// <summary>
    /// Return view model with languages items.
    /// </summary>
    /// <returns>View model list object if found or null.</returns>
    private List<VM_LanguageItem> GetMainMenuLanguageItems()
    {
      List<VM_LanguageItem> vLanguageItemList = new List<VM_LanguageItem>();

      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      using (SMFContext SMFctx = new SMFContext())
      {
        //List<Language> languageList = uow.Repository<Language>().Query(z => z.Active == true).OrderBy(k => k.OrderBy(z => z.LanguageName)).Get().ToList();
        List<Language> languageList = SMFctx.Languages.Where(x => x.Active == true).OrderBy(k => k.LanguageName).ToList();
        if (languageList.Count > 0)
        {
          vLanguageItemList = new VM_LanguageItemList(languageList);
        }
      }
      return vLanguageItemList;
    }

    /// <summary>
    /// Recursive function, gets child menu items from all levels.
    /// </summary>
    /// <param name="parent">Parent menu item</param>
    private void GetChildren(VM_MenuItem parent, string user, bool isAdmin = false)
    {
      parent.Children = GetChildrenItems(parent, user, isAdmin);

      if (parent.Children.Count > 0)
      {
        foreach (VM_MenuItem newParent in parent.Children)
        {
          GetChildren(newParent, user);
        }
      }
    }

    ///// <summary>
    ///// Filter menu items according to user role
    ///// </summary>
    ///// <param name="menuList">list of menu items</param>
    ///// <param name="user">logged in user</param>
    ///// <returns>list of filtered items</returns>
    //[Obsolete]
    //private List<HmiClientMenu> FilterAuthorizerItems(List<HmiClientMenu> menuList, User user)
    //{
    //	List<HmiClientMenu> returnList = new List<HmiClientMenu>();

    //	foreach (HmiClientMenu cm in menuList)
    //	{
    //		if (SmfAuthorization.CheckIfAuthorized(user, cm.Name))
    //		{
    //			returnList.Add(cm);
    //		}
    //	}

    //	return returnList;
    //}

    #endregion
  }
}
