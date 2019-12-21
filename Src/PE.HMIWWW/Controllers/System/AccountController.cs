using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.ViewModel.System;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.Core.Controllers;
using Kendo.Mvc.UI;
using SMF.HMI.Core;
using System;

namespace PE.HMIWWW.Controllers
{
  [Authorize]
  public class AccountController : BaseController
  {
    #region services

    IAccountService _accountService;

    #endregion
    #region members

    private ApplicationSignInManager _signInManager;
    private ApplicationUserManager _userManager;

    #endregion
    #region properties

    public ApplicationSignInManager SignInManager
    {
      get
      {
        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
      }
      private set
      {
        _signInManager = value;
      }
    }
    public ApplicationUserManager UserManager
    {
      get
      {
        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
      private set
      {
        _userManager = value;
      }
    }

    #endregion
    #region ctor
    public AccountController(IAccountService service)
    {
      _accountService = service;
    }

    public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
    {
      UserManager = userManager;
      SignInManager = signInManager;
    }

    #endregion
    #region user view interface

    public ActionResult ChangePasswordDialog()
    {
      return View();
    }
    public async Task<JsonResult> ChangePassword(VM_AccountPassword viewModel)
    {
      return await PrepareJsonResultFromVm(() => ChangePasswordHelper(viewModel));
    }

    [AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
      ViewBag.ReturnUrl = returnUrl;
      ViewBag.Languages = _accountService.GetLanguages();
      return View();
    }

    //
    // POST: /Account/Login
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      // This doesn't count login failures towards account lockout
      // To enable password failures to trigger account lockout, change to shouldLockout: true
      SignInStatus result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
      switch (result)
      {
        case SignInStatus.Success:
          {
            return RedirectToLocal(returnUrl);
          }
        case SignInStatus.LockedOut:
          return View("Lockout");
        case SignInStatus.RequiresVerification:
          return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        case SignInStatus.Failure:
        default:
          ModelState.AddModelError("", "Invalid login attempt.");
          return View(model);
      }
    }
    [AllowAnonymous]
    public ActionResult Register()
    {
      return View();
    }

    //
    // POST: /Account/Register
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult Register(RegisterViewModel model)
    {
      if (RegisterUserHelper(model))
      {
        return RedirectToAction("Index", "Home");
      }
      return View();
    }
    // POST: /Account/LogOff
    [HttpGet]
    public ActionResult LogOff()
    {
      AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
      return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [AllowAnonymous]
    public JsonResult ChangeLanguage(string culture)
    {
      // Validate input
      culture = CultureHelper.GetImplementedCulture(culture);
      // Save culture in a cookie
      HttpCookie cookie = Request.Cookies["_culture"];

      if (cookie != null)
        cookie.Value = culture;   // update cookie value
      else
      {
        cookie = new HttpCookie("_culture");
        cookie.Value = culture;
        cookie.Expires = DateTime.Now.AddYears(1);
      }
      Response.Cookies.Add(cookie);
      return Json(culture, JsonRequestBehavior.AllowGet);
    }
    #endregion
    #region admin view interface

    [SmfAuthorization(PE.Core.Constants.SmfAuthorization_Controller_UserAccountAdministration, PE.Core.Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(PE.Core.Constants.SmfAuthorization_Controller_UserAccountAdministration, PE.Core.Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetAccountData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _accountService.GetAccountList(ModelState, request));
    }

    [SmfAuthorization(PE.Core.Constants.SmfAuthorization_Controller_UserAccounts, PE.Core.Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetAccountRoles(string id, [DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _accountService.GetRolesList(ModelState, request, id));
    }

    #region edit account 

    [SmfAuthorization(PE.Core.Constants.SmfAuthorization_Controller_UserAccountAdministration, PE.Core.Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> EditAccountDialog(string id)
    {
      return await PreparePopupActionResultFromVm(() => _accountService.GetAccount(ModelState, id), "EditAccountDialog");
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(PE.Core.Constants.SmfAuthorization_Controller_UserAccountAdministration, PE.Core.Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> UpdateAccount(VM_Account viewModel)
    {
      return await PrepareJsonResultFromVm(() => _accountService.UpdateAccount(ModelState, viewModel));
    }

    #endregion

    [SmfAuthorization(PE.Core.Constants.SmfAuthorization_Controller_UserAccounts, PE.Core.Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> Details(string id)
    {
      return await PreparePopupActionResultFromVm(() => _accountService.GetAccount(ModelState, id), "EditAccountDialog");
    }

    #region edit roles 

    [SmfAuthorization(PE.Core.Constants.SmfAuthorization_Controller_UserAccounts, PE.Core.Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<ActionResult> EditAccountRolesDialog(string id)
    {
      return await PreparePopupActionResultFromVm(() => PrepareStringId(id), "EditAccountRolesDialog");
    }

    [HttpPost]
    [SmfAuthorization(PE.Core.Constants.SmfAuthorization_Controller_UserAccounts, PE.Core.Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> UpdateAccountRoles(string roleId, string userId, string isAssigned)
    {
      return await PrepareJsonResultFromVm(() => _accountService.UpdateUserInRole(ModelState, roleId, userId, isAssigned));
    }

    #endregion

    #region delete account

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(PE.Core.Constants.SmfAuthorization_Controller_UserAccountAdministration, PE.Core.Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> Delete([DataSourceRequest] DataSourceRequest request, VM_StringId viewModel)
    {
      return await PrepareJsonResultFromVm(() => _accountService.DeleteAccount(ModelState, viewModel));
    }

    #endregion

    #endregion

    #region Helpers

    private bool RegisterUserHelper(RegisterViewModel registerData)
    {
      //VM_StringId returnValue = null;
      if (ModelState.IsValid)
      {
        if (UserManager.FindByEmail(registerData.Email) == null)
        {
          ApplicationUser user = new ApplicationUser { UserName = registerData.Email, Email = registerData.Email, LanguageId = registerData.Language };
          IdentityResult result = UserManager.Create(user, registerData.Password);
          if (result.Succeeded)
          {
            SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
            // returnValue=new VM_StringId(user.Id);
            //RedirectToLocal("");
            return true;
          }
        }
        else
        {
          ModelState.AddModelError("", "User already exist.");
        }

      }
      return false;
      //return returnValue;
    }

    private VM_StringId PrepareStringId(string id)
    {
      return new VM_StringId(id);
    }
    private VM_AccountPassword ChangePasswordHelper(VM_AccountPassword viewModel)
    {
      VM_AccountPassword tmpResult = null;
      ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
      if (user == null)
      {
        AddModelStateError(PE.HMIWWW.Core.Resources.VM_Resources.GLOB_Account_UserNotFound);
      }
      bool tmpValue = UserManager.CheckPassword(user, viewModel.OldPassword);
      if (!tmpValue)
      {
        AddModelStateError(PE.HMIWWW.Core.Resources.VM_Resources.GLOB_Account_OldPassNotValid);
      }
      if (ModelState.IsValid)
      {
        IdentityResult result = UserManager.ChangePassword(user.Id, viewModel.OldPassword, viewModel.Password);
        if (!result.Succeeded)
        {
          AddModelStateError(PE.HMIWWW.Core.Resources.VM_Resources.GLOB_Account_PasswordNotChanged);
        }
        else
          tmpResult = viewModel;
      }
      return tmpResult;
    }

    // Used for XSRF protection when adding external logins
    private const string XsrfKey = "XsrfId";

    private IAuthenticationManager AuthenticationManager
    {
      get
      {
        return HttpContext.GetOwinContext().Authentication;
      }
    }

    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      return RedirectToAction("Index", "Home");
    }

    public class ChallengeResult : HttpUnauthorizedResult
    {
      public ChallengeResult(string provider, string redirectUri)
              : this(provider, redirectUri, null)
      {
      }

      public ChallengeResult(string provider, string redirectUri, string userId)
      {
        LoginProvider = provider;
        RedirectUri = redirectUri;
        UserId = userId;
      }

      public string LoginProvider { get; set; }
      public string RedirectUri { get; set; }
      public string UserId { get; set; }

      public override void ExecuteResult(ControllerContext context)
      {
        AuthenticationProperties properties = new AuthenticationProperties { RedirectUri = RedirectUri };
        if (UserId != null)
        {
          properties.Dictionary[XsrfKey] = UserId;
        }
        context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (_userManager != null)
        {
          _userManager.Dispose();
          _userManager = null;
        }

        if (_signInManager != null)
        {
          _signInManager.Dispose();
          _signInManager = null;
        }
      }

      base.Dispose(disposing);
    }
    #endregion

  }
}
