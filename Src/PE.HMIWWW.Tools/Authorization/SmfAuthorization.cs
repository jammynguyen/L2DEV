using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SMF.DbEntity.Model;
using SMF.DbEntity;
using System.Web.Routing;
using PE.HMIWWW.Core.Resources;
using PE.DbEntity.Models;
using System.Data.Entity;


namespace PE.HMIWWW.Core.Authorization
{

	public enum RightLevel : short { View = 1, Update, Delete }
	public class SmfAuthorization : AuthorizeAttribute
	{
		#region members

		private RightLevel _rightLevel;
		private string _authObjectName;
		private string _authController;
		private string _authModule;

		#endregion

		#region ctor

		public SmfAuthorization(string authObjectName, string module, RightLevel rightLevel)
		{
			_rightLevel = rightLevel;
			_authObjectName = authObjectName;
			_authModule = module;
		}

		#endregion

		#region properties
		public string AuthObjectName
		{
			get { return _authObjectName; }
		}
		public string AuthModule
		{
			get { return _authModule; }
		}
		#endregion

		#region actions

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			base.OnAuthorization(filterContext);

			_authController = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
		}
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			ApplicationUserManager userManager = httpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			ApplicationUser applicationUser = userManager.FindById(httpContext.GetOwinContext().Authentication.User.Identity.GetUserId());

			if (applicationUser == null)
				return false;
			else
			{
				return CheckIfRequestAuthorized( applicationUser, _authObjectName, (int)_rightLevel);
			}
		}
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			if (filterContext.HttpContext.Request.IsAjaxRequest())
			{
        UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
				filterContext.HttpContext.Response.StatusCode = 403;
				filterContext.HttpContext.Response.Charset = "utf-8";
				filterContext.Result = new JsonResult
				{
					Data = new
					{
						Data = new
						{
							Errors = String.Format(ResourceController.GetGlobalResourceTextByResourceKey("UnauthAccessError"), filterContext.HttpContext.Request.Url.AbsolutePath),
							Url = filterContext.HttpContext.Request.Url.AbsolutePath,
							Code = 403,
						}
					},
					JsonRequestBehavior = JsonRequestBehavior.AllowGet
				};
			}
			else
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login", returnUrl = filterContext.HttpContext.Request.Url.PathAndQuery }));
		}

		#endregion

		#region private methods
		private static bool CheckIfRequestAuthorized(ApplicationUser applicationUser, string authObjectName, int rightLevel = 1)
		{
			if (applicationUser.Claims.FirstOrDefault(x => x.ClaimType == "admin" && x.ClaimValue == "true") != null)
				return true;

			foreach (Role r in GetUserRoles(applicationUser.Id))
			{
				if (r == null || r.RoleRights == null)
					continue;
				foreach (RoleRight rr in r.RoleRights)
				{
					if (rr.AccessUnit != null)
					{
						if (rr.AccessUnit.Name == authObjectName && (int)rr.PermissionType >= rightLevel)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Get Full Info for roles.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns>List of Roles.</returns>
		private static List<Role> GetUserRoles(string userId)
		{
			List<Role> RolesList = new List<Role>();
			try
			{
        using (SMFContext smfCtx = new SMFContext())
        {
          List<UserRole> UserRoles = smfCtx.UserRoles.Where(x => x.UserId == userId).ToList();
        //}

        //using (SMFUnitOfWork uow = new SMFUnitOfWork())
				//{
				//	List<UserRole> UserRoles = uow.Repository<UserRole>().Query(z => z.UserId == userId).Get().ToList();
					if (UserRoles.Count > 0)
					{
						foreach (UserRole element in UserRoles)
						{
              Role Role = smfCtx.Roles.Where(i => i.Id == element.RoleId)
                .Include(z => z.RoleRights.Select(s => s.AccessUnit)).Single();


              //Role Role = uow.Repository<Role>().Query(z => z.Id == element.RoleId)
							//																	.Include(z => z.RoleRights.Select(s => s.AccessUnit))
							//																	.GetSingle();
							RolesList.Add(Role);
						}
					}
					return RolesList;
				}
			}
			catch
			{
				return RolesList;
			}
		}

		#endregion

		//public static 
	}
}
