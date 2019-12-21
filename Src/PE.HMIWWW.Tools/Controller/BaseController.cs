using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NLog;
using System.Threading;
using System.Web.Routing;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using Kendo.Mvc.UI;
using PE.HMIWWW.Core.Parameter;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using SMF.RepositoryPatternExt;
using SMF.Module.Notification;
using SMF.Core.DC;
using SMF.HMIWWW.Core;
using SMF.HMIWWW.UnitConverter;
using SMF.HMIWWW.Attributes;
using SMF.Module.Core;
using SMF.Module.Limit;

namespace PE.HMIWWW.Core.Controllers
{
  public class BaseController : Controller
  {
    #region members
    public static Logger Logger { get; set; }
    private string _hostIp;
    private string _userId;
    protected string _userName;
    private bool _isAuthenticated;
    #endregion
    #region ctor
    public BaseController()
    {
      Logger = LogManager.GetLogger("WWW");
    }
    #endregion
    #region override
    protected override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      _hostIp = HttpContext.Request.UserHostAddress;

      ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      ApplicationUser applicationUser = userManager.FindById(HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId());

      ViewBag.User_IsAuthenticated = _isAuthenticated = User.Identity.IsAuthenticated;
      if (_isAuthenticated)
      {
        ViewBag.User_Id = _userId = User.Identity.GetUserId();
        ViewBag.User_Name = _userName = User.Identity.GetUserName();
        if (applicationUser.Claims.Count > 0)
        {
          try
          {
            ViewBag.User_IsSuperuser = (applicationUser.Claims.First(x => x.ClaimType == "superuser") != null ? true : false);
          }
          catch
          {
            ViewBag.User_IsSuperuser = false;
          }

          try
          {
            ViewBag.User_IsAdmin = (applicationUser.Claims.First(x => x.ClaimType == "admin") != null ? true : false);
          }
          catch
          {
            ViewBag.User_IsAdmin = false;
          }
        }
        else
        {
          ViewBag.User_IsSuperuser = false;
          ViewBag.User_IsAdmin = false;
        }
        ViewBag.User_ScreenOrientation = applicationUser.HMIViewOrientation;
        ViewBag.Page_Title = ResourceController.GePageTitleValue(HttpContext.Request.RequestContext.RouteData.Values["controller"].ToString(), HttpContext.Request.RequestContext.RouteData.Values["action"].ToString());
        ViewBag.Page_Controller = HttpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

        string iconName = "header_" + ViewBag.Page_Controller + ".png";
        ViewBag.Page_Icon = "background-image:url(\'/content/ControllerIcon/" + iconName + "\');";

        //ViewBag.Constans = Session["Constans"];
        //if (ViewBag.Constans == null)
        //{
        //  var constants = typeof(PE.Common.HMIRefreshKeys)
        // .GetFields()
        // .ToDictionary(x => x.Name, x => x.GetValue(null));
        //  var json = new JavaScriptSerializer().Serialize(constants);
        //  ViewBag.Constans = JavaScript("var HmiRefreshKeys = " + json + ";");
        //  Session["Constans"] = ViewBag.Constans;
        //}
      }

    }
    protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
    {
      string cultureName = null;

      // Attempt to read the culture cookie from Request
      HttpCookie cultureCookie = Request.Cookies["_culture"];
      if (cultureCookie != null)
        cultureName = cultureCookie.Value;
      else
        cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
      // Validate culture name
      cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

      // Modify current thread's cultures            
      Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
      Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

      //ViewBag.User_LanguageId = cultureName;
      ViewBag.User_LanguageCode = cultureName;
      ViewBag.NoRecordsInGrid = GetParameter("NoOfRowsInGrid").ValueInt;
      ViewBag.FullGridHeight = GetParameter("FullGridHeight").ValueInt;
      ViewBag.HalfGridHeight = GetParameter("HalfGridHeight").ValueInt;
      LimitController.Reload();
      return base.BeginExecuteCore(callback, state);
    }
    protected override void Initialize(RequestContext requestContext)
    {
      UnitConverterHelper.Init(VM_Resources.ResourceManager);
      SmfAttributeInitializator.Init(VM_Resources.ResourceManager);
      base.Initialize(requestContext);
    }
    #endregion
    #region logger
    public void LogInfo(string text)
    {
      if (_isAuthenticated)
      {
        Logger.Info("[ {0} ], {1}", _userName, text);
      }
      else
      {
        Logger.Info("[ {0} ], {1}", _hostIp, text);
      }
    }
    public void LogWarning(string text)
    {
      if (_isAuthenticated)
      {
        Logger.Warn("[ {0} ], {1}", _userName, text);
      }
      else
      {
        Logger.Warn("[ {0} ], {1}", _hostIp, text);
      }
    }
    public void LogError(string text)
    {
      if (_isAuthenticated)
      {
        Logger.Error("[ {0} ], {1}", _userName, text);
      }
      else
      {
        Logger.Error("[ {0} ], {1}", _hostIp, text);
      }
    }
    public void LogException(Exception exception)
    {
      Logger logger = LogManager.GetLogger("EXC");
      logger.Trace(exception);
    }
    #endregion
    #region helpers
    public static string GetCultureName()
    {
      if (Thread.CurrentThread.CurrentUICulture != null)
        return Thread.CurrentThread.CurrentUICulture.Name;
      return "";
    }

		#endregion
		#region prepare result

		private JsonResult HandleJsonError()
    {
      string errorText = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

      HttpContext.Response.TrySkipIisCustomErrors = false;
      HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

      return new JsonResult
      {
        Data = new
        {
          Errors = errorText,
          Url = HttpContext.Request.Url.AbsolutePath,
          Code = 500,
        }
      };
    }
    private void StoreExceptionsInModelState(Exception ex)
    {
      DbExceptionResult exceptionResult = DbExceptionHelper.ProcessException(ex, "Database operation failed!", null);

      ModelState.AddModelError("error", exceptionResult.Message);
      if (exceptionResult.InnerMessages != null)
      {
        foreach (string message in exceptionResult.InnerMessages)
        {
          ModelState.AddModelError("error", message);
        }
      }

    }

    /// <summary>
    /// Method prepares complete JSON result (including error handling) from DataSourceResult.
    /// DataSourceResult is main data source for Ttelerik Grids
    /// </summary>
    /// <param name="methodToGetDataFromService">
    /// Paremeter is method from service injected to current controller returning DataSourceResult
    /// </param>
    /// <returns>
    /// Return value is JosnResult
    /// </returns>
    protected async Task<JsonResult> PrepareJsonResultFromDataSourceResult<T>(Func<T> methodToGetDataFromService) where T : DataSourceResult
    {
      Task task = null;
      try
      {
        DataSourceResult dataSourceResult = null;
        task = new Task(delegate { dataSourceResult = methodToGetDataFromService(); });
        task.Start();
        await task.ConfigureAwait(false);
        if (!ModelState.IsValid)
        {
          return HandleJsonError();
        }
        return Json(dataSourceResult, JsonRequestBehavior.AllowGet);
      }
      catch (ModuleMessageException mEx)
      {
        await Task.Factory.StartNew(delegate { PrepareErrorMessageFromModuleMessageExceptions(mEx); }).ConfigureAwait(false);
        return HandleJsonError();
      }
      catch (Exception ex)
      {
        task = new Task(delegate { StoreExceptionsInModelState(ex); });
        task.Start();
        await task.ConfigureAwait(false);
        return HandleJsonError();
      }
    }
    /// <summary>
    /// Method prepares complete JSON result (including error handling) from ViewModel.
    /// </summary>
    /// <param name="methodToGetDataFromService">
    /// Paremeter is method from service injected to current controller returning ViewModel
    /// </param>
    /// <returns>
    /// Return value is JosnResult
    /// </returns>
    protected async Task<JsonResult> PrepareJsonResultFromVm<T>(Func<T> methodToGetDataFromService) where T : VM_Base
    {
      Task task = null;
      try
      {
        VM_Base vmResult = null;
        task = new Task(delegate { vmResult = methodToGetDataFromService(); });
        task.Start();
        await task.ConfigureAwait(false);
        if (!ModelState.IsValid)
        {
          return HandleJsonError();
        }
        return Json(vmResult, JsonRequestBehavior.AllowGet);
      }
      catch (ModuleMessageException mEx)
      {
        await Task.Factory.StartNew(delegate { PrepareErrorMessageFromModuleMessageExceptions(mEx); }).ConfigureAwait(false);
        return HandleJsonError();
      }
      catch (Exception ex)
      {
        // Type t = ex.InnerException.GetType();
        task = new Task(delegate { StoreExceptionsInModelState(ex); });
        task.Start();
        await task.ConfigureAwait(false);
        return HandleJsonError();
      }
    }
    /// <summary>
    /// Method prepares complete action result from ViewModel. In case error JsonResult is returned.
    /// </summary>
    /// <param name="methodToGetDataFromService">
    /// Paremeter is method from service injected to current controller returning ViewModel
    /// </param>
    /// <returns>
    /// Return value is ActionResult
    /// </returns>
    protected async Task<ActionResult> PreparePopupActionResultFromVm<T>(Func<T> methodToGetDataFromService, string partialViewModelName) where T : VM_Base
    {
      Task task = null;
      try
      {
        VM_Base vmResult = null;
        task = new Task(delegate { vmResult = methodToGetDataFromService(); });
        task.Start();
        await task.ConfigureAwait(false);
        if (!ModelState.IsValid)
        {
          return HandleJsonError();
        }
        return PartialView(partialViewModelName, vmResult);
      }
      catch (ModuleMessageException mEx)
      {
        await Task.Factory.StartNew(delegate { PrepareErrorMessageFromModuleMessageExceptions(mEx); }).ConfigureAwait(false);
        return HandleJsonError();
      }
      catch (Exception ex)
      {
        task = new Task(delegate { StoreExceptionsInModelState(ex); });
        task.Start();
        await task.ConfigureAwait(false);
        return HandleJsonError();
      }
    }
    /// <summary>
    /// Method prepares complete action result from ViewModel. .
    /// </summary>
    /// <param name="methodToGetDataFromService">
    /// Paremeter is method from service injected to current controller returning ViewModel
    /// </param>
    /// <returns>
    /// Return value is ActionResult
    /// </returns>
    protected async Task<ActionResult> PrepareActionResultFromVm<T>(Func<T> methodToGetDataFromService, string partialViewModelName) where T : VM_Base
    {
      Task task = null;
      try
      {
        VM_Base vmResult = null;
        task = new Task(delegate { vmResult = methodToGetDataFromService(); });
        task.Start();
        await task.ConfigureAwait(false);
        if (!ModelState.IsValid)
        {
          return PartialView("Error");
          // return HandleJsonError();
        }
        return PartialView(partialViewModelName, vmResult);
      }
      catch//(Exception ex)
      {
        return PartialView("Error");
        //task = new Task(delegate { StoreExceptionsInModelState(ex); });
        //task.Start();
        //await task.ConfigureAwait(false);
        //return HandleJsonError();
      }
    }
    protected async Task<ActionResult> TaskPrepareActionResultFromVm<T>(Task<T> methodToGetDataFromService, string partialViewModelName) where T : VM_Base
    {
      try
      {
        VM_Base vmResult = await methodToGetDataFromService;
        if (!ModelState.IsValid)
        {
          return PartialView("Error");
          // return HandleJsonError();
        }
        return PartialView(partialViewModelName, vmResult);
      }
      catch(Exception)
      {
        return PartialView("Error");
        //task = new Task(delegate { StoreExceptionsInModelState(ex); });
        //task.Start();
        //await task.ConfigureAwait(false);
        //return HandleJsonError();
      }
    }
    protected async Task<ActionResult> PrepareActionResultFromVmList<T>(Func<T> methodToGetDataFromService, string partialViewModelName) where T : IEnumerable
    {
      Task task = null;
      try
      {
        IEnumerable vmResult = null;
        task = new Task(delegate { vmResult = methodToGetDataFromService(); });
        task.Start();
        await task.ConfigureAwait(false);
        if (!ModelState.IsValid)
        {
          return PartialView("Error");
          // return HandleJsonError();
        }
        return PartialView(partialViewModelName, vmResult);
      }
      catch//(Exception ex)
      {
        return PartialView("Error");
        //task = new Task(delegate { StoreExceptionsInModelState(ex); });
        //task.Start();
        //await task.ConfigureAwait(false);
        //return HandleJsonError();
      }
    }
    protected async Task<JsonResult> TaskPrepareJsonResultFromVm<T, V>(Func<V> methodToGetDataFromService) where V : Task<T> where T : VM_Base
    {
      Task task = null;
      try
      {
        T vmResult = await methodToGetDataFromService();


				if (!ModelState.IsValid)
        {
          if (!HandleModelWarningMessage(ref vmResult))
            return HandleJsonError();
        }

        return Json(vmResult, JsonRequestBehavior.AllowGet);
      }
      catch (ModuleMessageException mEx)
      {
        await Task.Factory.StartNew(delegate { PrepareErrorMessageFromModuleMessageExceptions(mEx); }).ConfigureAwait(false);
        return HandleJsonError();
      }
      catch (Exception ex)
      {
        // Type t = ex.InnerException.GetType();
        task = new Task(delegate { StoreExceptionsInModelState(ex); });
        task.Start();
        await task.ConfigureAwait(false);
        return HandleJsonError();
      }
    }

    public SMF.DbEntity.Model.Parameter GetParameter(string parameterName)
    {
      return ParameterController.GetParameter(parameterName);
    }

    #endregion
    protected void AddModelStateError(string errorText)
    {
      ModelState.AddModelError("", errorText);
    }

    private bool HandleModelWarningMessage<T>(ref T vmResult) where T : VM_Base
    {
      bool flag = true;
      IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
      foreach (ModelError error in allErrors)
      {
        if (error.Exception is ModuleWarningMessageException)
        {
          ModuleWarningMessageException ex = (ModuleWarningMessageException)error.Exception;
          PrepareWarningMessageFromModuleWarningMessageException(ref ex);

          vmResult.SetModuleWarningMessage(ex.ModuleWarningMessage);
        }
        else flag = false;
      }
      return flag;
    }
    private void PrepareWarningMessageFromModuleWarningMessageException(ref ModuleWarningMessageException ex)
    {
      ModuleWarningMessage moduleWarningMessage = ex.ModuleWarningMessage;
      // Validate
      if (moduleWarningMessage == null)
        throw new NullReferenceException();
      // Language
      string cultureName = GetCultureName();
      short loop = 10;
      do
      {
        foreach (ModuleMessage message in moduleWarningMessage.WarningMessage)
        {
          message.SetName(moduleWarningMessage.ModuleName);
          message.SetMessage(NotificationController.GetErrorCodeMessage(message.ErrorCode, message.ShortDescription, cultureName, message.Args));
        }

        if (moduleWarningMessage.InnerModuleWarningMessage != null)
        {
          moduleWarningMessage = moduleWarningMessage.InnerModuleWarningMessage;
        }
        else break;

        --loop;
      } while (loop > 0);
      //if (loop == 0) Notification controler. alarm -> more than 10 jumps between module
    }

    private void PrepareErrorMessageFromModuleMessageExceptions(ModuleMessageException mEx)
    {
      // get module result and entry validate
      ModuleMessage moduleResult = mEx.ModuleMessage;
      if (moduleResult == null)
        throw new NullReferenceException();
      string cultureName = GetCultureName();

      //----------------end getting culture
      //TODO: Structure moduleResult needs tuning. ModuleName is missplaced with ShortDescription. Other attributs are wrong as well.
      string preparedMessage = String.Format("{0}: {1}, {2}: {3}", ResourceController.GetResourceTextByResourceKey("GLOB_Module_Name"), moduleResult.ModuleName, ResourceController.GetResourceTextByResourceKey("GLOB_Alarm_Code"), moduleResult.ErrorCode);

      try
      {
        preparedMessage += "\n" + NotificationController.GetErrorCodeMessage(moduleResult.ErrorCode, moduleResult.ShortDescription, cultureName, moduleResult.Args);
      }
      catch (Exception ex) {
        preparedMessage += "\n" + "Wrong alarm configuration, It is possible that alarm parameters are not matching template: "+ ex.Message;
      }
      //preparedMessage += "\n" + NotificationController.GetErrorCodeMessage(moduleResult.ModuleName, moduleResult.ErrorCode, cultureName, moduleResult.ShortDescription);
      AddModelStateError(preparedMessage);
    }

  }
}
