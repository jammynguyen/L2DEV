using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.System
{
    public class L3CommStatusController : BaseController
    {
        #region services

        private readonly IL3CommStatusService _L3CommStatusService;

        #endregion

        #region ctor
        public L3CommStatusController(IL3CommStatusService service)
        {
            _L3CommStatusService = service;
        }
        #endregion
        [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System, RightLevel.View)]
        public ActionResult Index()
        {
            //return View("~/Views/System/L3CommStatus/Index.cshtml");
            return View();
        }

        [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System, RightLevel.View)]
        public async Task<JsonResult> GetL3TransferTableWorkOrderList([DataSourceRequest]DataSourceRequest request)
        {
            return await PrepareJsonResultFromDataSourceResult(() => _L3CommStatusService.GetL3TransferTableWorkOrderList(ModelState, request));
        }

        [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System, RightLevel.View)]
        public async Task<JsonResult> GetL3TransferTableGeneralList([DataSourceRequest]DataSourceRequest request)
        {
            return await PrepareJsonResultFromDataSourceResult(() => _L3CommStatusService.GetL3TransferTableGeneralList(ModelState, request));
        }

    }
}