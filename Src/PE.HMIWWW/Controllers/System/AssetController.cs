using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.System
{
    public class AssetController : BaseController
    {
        private readonly IAssetService _AssetService;

        public AssetController(IAssetService service)
        {
            _AssetService = service;
        }

        [SmfAuthorization(Constants.SmfAuthorization_Controller_Asset, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
        public ActionResult Index()
        {
            return View("~/Views/Module/PE.Lite/Asset/Index.cshtml");
        }

        [SmfAuthorization(Constants.SmfAuthorization_Controller_Asset, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
        public async Task<JsonResult> GetAssetOverList([DataSourceRequest] DataSourceRequest request)
        {
            return await PrepareJsonResultFromDataSourceResult(() => _AssetService.GetAssetOverList(ModelState, request));
        }

        [SmfAuthorization(Constants.SmfAuthorization_Controller_Asset, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
        public async Task<JsonResult> GetFeatureByAssetId([DataSourceRequest] DataSourceRequest request, long assetId)
        {
            return await PrepareJsonResultFromDataSourceResult(() => _AssetService.GetFeatureByAssetId(ModelState, request, assetId));
        }

        [SmfAuthorization(Constants.SmfAuthorization_Controller_Asset, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
        public async Task<JsonResult> GetTriggerOverViewList([DataSourceRequest] DataSourceRequest request)
        {
            return await PrepareJsonResultFromDataSourceResult(() => _AssetService.GetTriggerOverViewList(ModelState, request));
        }

        [SmfAuthorization(Constants.SmfAuthorization_Controller_Asset, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
        public async Task<JsonResult> GetTriggerDetailByTriggerCode([DataSourceRequest] DataSourceRequest request, string triggerCode)
        {
            return await PrepareJsonResultFromDataSourceResult(() => _AssetService.GetTriggerDetailByTriggerCode(ModelState, request, triggerCode));
        }
    }
}