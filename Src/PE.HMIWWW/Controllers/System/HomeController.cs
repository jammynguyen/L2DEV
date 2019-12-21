using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.System;
using SMF.HMIWWW.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PE.HMIWWW.Controllers
{
  public class HomeController : BaseController
  {
    #region services

   // private IHomeService _service;

    #endregion
    #region ctor
    public HomeController()//IHmiServiceBase service)
    {
      //_service = service;
    }

    #endregion

    public ActionResult Constants()
    {
      global::System.Collections.Generic.Dictionary<string, object> constants = typeof(PE.Common.HMIRefreshKeys)
          .GetFields()
          .ToDictionary(x => x.Name, x => x.GetValue(null));
      string json = new JavaScriptSerializer().Serialize(constants);
      return JavaScript("var HmiRefreshKeys = " + json + ";");
    }
    public ActionResult Operations()
    {
      global::System.Collections.Generic.Dictionary<string, object> constants = typeof(PE.Common.HMICommands)
          .GetFields()
          .ToDictionary(x => x.Name, x => x.GetValue(null));
      string json = new JavaScriptSerializer().Serialize(constants);
      return JavaScript("var HMIOperations = " + json + ";");
    }

    //[AcceptVerbs(HttpVerbs.Post)]
    //public async Task<JsonResult> ModuleOperationRequest(VM_LongId viewModel)
    //{
    //  return await PrepareJsonResultFromVm(() => _service.ModuleOperationRequest(ModelState, viewModel));
    //}

    public ActionResult Index()
    {
      return View();
    }
    [HttpGet]
    public ActionResult SetCulture(string culture, string returnUrl)
    {
      // Validate input
      culture = CultureHelper.GetImplementedCulture(culture);
      // Save culture in a cookie
      HttpCookie cookie = Request.Cookies["_culture"];

      ////setup signalr
      //ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      //ApplicationUser applicationUser = userManager.FindById(HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId());
      //string languageCode = null;
      //if (applicationUser != null && applicationUser.Language != null)
      //{
      //	languageCode = applicationUser.Language.LanguageCode;
      //}
      //SignalRClient.ChangeLanguage((cookie==null? languageCode : cookie.Value), culture);

      if (cookie != null)
        cookie.Value = culture;   // update cookie value
      else
      {
        cookie = new HttpCookie("_culture");
        cookie.Value = culture;
        cookie.Expires = DateTime.Now.AddYears(1);
      }
      Response.Cookies.Add(cookie);
      //return RedirectToAction("Index");
      return RedirectToLocal(returnUrl);
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }
    //[SmfAuthorization("CONTROLLER_Schedule", "MODULE_NAME", RightLevel.View)]
    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";
      return View();
      // Modules.hmiexe.Name;
    }

    [HttpPost]
    public ActionResult ProcessForm()
    {
      //_service.SendHmiOperationRequest(PrepareInitiator(), Modules.hmiexe.Name, 100);
      return View("Contact");
    }

    #region Private
    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      return RedirectToAction("Index", "Home");
    }
    #endregion
  }
}
