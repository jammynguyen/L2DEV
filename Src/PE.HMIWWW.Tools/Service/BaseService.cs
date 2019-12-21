using System.Web;
using System.Web.Mvc;
using NLog;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Resources;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using SMF.HMIWWW.Core;
using SMF.Module.Core;
using SMF.RepositoryPatternExt;
using System;
using System.ServiceModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;




namespace PE.HMIWWW.Core.Service
{
  public class BaseService : IBaseService
  {
    #region members

    #endregion
    #region properties
    public Logger Logger { get; private set; }
    #endregion
    #region ctor
    public BaseService()
    {
      Logger = LogManager.GetLogger("EXC");
    }
    #endregion
    #region alarm methods
    public void LogInfo(string text)
    {
      Logger.Info("{0}", text);
    }
    public void LogWarning(string text)
    {
      Logger.Warn("{0}", text);
    }
    public void LogError(string text)
    {
      Logger.Error("{0}", text);
    }
    public void LogException(Exception exception)
    {
      Logger logger = LogManager.GetLogger("EXC");
      logger.Trace(exception);
    }
    #endregion
    #region helpers
    /// <summary>
    /// Get Current Language.
    /// </summary>
    /// <returns>Return Language Code.</returns>
    protected string GetCurrentLanguageName()
    {
      return CultureHelper.GetCurrentCulture();
    }
		//protected void InitDataContract(DataContractBase dataContract)
  //  {
  //    if (dataContract == null)
  //      dataContract = new DataContractBase();

  //    ApplicationUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
  //    ApplicationUser applicationUser = userManager.FindById(HttpContext.Current.GetOwinContext().Authentication.User.Identity.GetUserId());

  //    dataContract.Sender = "HmiWWW";
  //    dataContract.TimeStamp = DateTime.Now;
  //    dataContract.HmiInitiator = new HmiInitiator(applicationUser.UserName, HttpContext.Current.Request.UserHostAddress);

  //    if (applicationUser.Claims.Count > 0)
  //    {
  //      try
  //      {
  //        dataContract.HmiInitiator.SpecialPrivileges = (applicationUser.Claims.First(x => x.ClaimType == "superuser") != null ? 1 : 0);
  //      }
  //      catch
  //      {
  //        dataContract.HmiInitiator.SpecialPrivileges = 0;
  //      }
  //    }
  //  }

		protected void HandleWarnings<T>(SendOfficeResult<T> sendOfficeResult, ref ModelStateDictionary modelState) where T : DataContractBase
		{
			if (sendOfficeResult != null)
			{
				if (sendOfficeResult.OperationSuccess)
				{
					if (sendOfficeResult.DataConctract != null)
					{
						HandleModuleWarningMessage(sendOfficeResult.DataConctract, ref modelState);
					}
				}
			}
		}
		#endregion

		#region IBaseService
		public void SendHmiOperationRequest(HmiInitiator hmiInitiator, String moduleName, int operation)
    {
      IBaseModule client = null;
      try
      {
        client = InterfaceHelper.GetFactoryChannel<IBaseModule>(moduleName);
        DCHmiOperation dc = new DCHmiOperation();
				dc.HmiInitiator = hmiInitiator;
        dc.Operation = operation;
        client.ModuleHmiOperation(dc);

        InterfaceHelper.CloseChannel((IClientChannel)client);
      }
      catch (Exception ex)
      {
        if (client != null)
          InterfaceHelper.AbortChannel((IClientChannel)client);

        Logger.Trace(ex);
      }
    }
    #endregion














    //protected static DataSourceResult HandleError<T>(DataSourceRequest request, ModelStateDictionary modelState, Exception ex)
    //{
    //    DataSourceResult result;
    //    DbExceptionResult exceptionResult = DbExceptionHelper.ProcessException(ex, "GetAllAccounts::Database operation failed!", null);
    //    T list = default(T);

    //    modelState.AddModelError("error", exceptionResult.Message);
    //    foreach (string message in exceptionResult.InnerMessages)
    //    {
    //        modelState.AddModelError("error", message);
    //    }

    //    result = new[] { list }.ToDataSourceResult(request, modelState);
    //    return result;
    //}

    protected void SetCommunicationError(ModelStateDictionary modelState)
    {
      modelState.AddModelError("error", ResourceController.GetErrorText("NoCommunication"));
    }
    protected void AddModelStateError(ModelStateDictionary modelState, string errorText)
    {
      modelState.AddModelError("error", errorText);
    }
    protected void AddModelStateError(ModelStateDictionary modelState, Exception ex)
    {
      DbExceptionResult result = DbExceptionHelper.ProcessException(ex, "DeleteAccount::Database operation failed!", GetCurrentLanguageName());
      modelState.AddModelError("error", result.Message);
    }
    protected void HandleModuleWarningMessage<T>(T dataContract, ref ModelStateDictionary modelState) where T : DataContractBase
    {
      if (dataContract.ModuleWarningMessage != null && dataContract.ModuleWarningMessage.WarningMessage.Count > 0)
      {
        modelState.AddModelError("warning", new ModuleWarningMessageException(dataContract.ModuleWarningMessage));
      }
      // Module Result is empty
      else return;
    }
  }
}
