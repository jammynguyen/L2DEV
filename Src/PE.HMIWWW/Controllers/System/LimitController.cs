using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.System
{
    public class LimitController : BaseController
    {
        #region services

        ILimitService _limitService;

        #endregion
        #region ctor

        public LimitController(ILimitService service)
        {
            _limitService = service;
        }

        #endregion
        #region view interface

        [SmfAuthorization(Constants.SmfAuthorization_Controller_Limit, Constants.SmfAuthorization_Module_System, RightLevel.View)]
        public ActionResult Index()
        {
            return View();
        }

        [SmfAuthorization(Constants.SmfAuthorization_Controller_Limit, Constants.SmfAuthorization_Module_System, RightLevel.View)]
        public async Task<JsonResult> LimitData([DataSourceRequest]DataSourceRequest request)
        {
            return await PrepareJsonResultFromDataSourceResult(() => _limitService.GetLimits(ModelState, request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [SmfAuthorization(Constants.SmfAuthorization_Controller_Limit, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
        public async Task<JsonResult> Update([DataSourceRequest] DataSourceRequest request, VM_Limit viewModel)
        {
            return await PrepareJsonResultFromVm(() => _limitService.UpdateLimit(ModelState, viewModel));
        }

        #endregion
    }
}