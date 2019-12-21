using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.System

{
    public class FeatureMapController : BaseController
    {
        private readonly IFeatureMap _FeatureMapService;

        public FeatureMapController(IFeatureMap service)
        {
            _FeatureMapService = service;
        }

        [SmfAuthorization(Constants.SmfAuthorization_Controller_FeatureMap, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
        public ActionResult Index()
        {
            return View("~/Views/Module/PE.Lite/FeatureMap/Index.cshtml");
        }

        [SmfAuthorization(Constants.SmfAuthorization_Controller_FeatureMap, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
        public async Task<JsonResult> GetFeatureMapOverList([DataSourceRequest] DataSourceRequest request)
        {
            return await PrepareJsonResultFromDataSourceResult(() => _FeatureMapService.GetFeatureMapOverList(ModelState, request));
        }

    }
}