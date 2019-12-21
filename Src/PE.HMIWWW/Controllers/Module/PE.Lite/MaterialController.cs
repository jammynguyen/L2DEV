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

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
    public class MaterialController : BaseController
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService service)
        {
            _materialService = service;
        }

        // GET: Material
        public ActionResult Index(long? materialId)
        {
            return View("~/Views/Module/PE.Lite/Material/Index.cshtml",materialId != null ? _materialService.GetMaterialById(materialId):null);
        }


        [SmfAuthorization(Constants.SmfAuthorization_Controller_Material, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
        public async Task<JsonResult> GetMaterialSearchList([DataSourceRequest] DataSourceRequest request)
        {
            return await PrepareJsonResultFromDataSourceResult(() => _materialService.GetMaterialSearchList(ModelState, request));
        }

        [SmfAuthorization(Constants.SmfAuthorization_Controller_Material, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
        public async Task<ActionResult> ElementDetails(long MaterialId)
        {
            return await PrepareActionResultFromVm(() => _materialService.GetMaterialDetails(ModelState, MaterialId), "~/Views/Module/PE.Lite/Material/_MaterialBody.cshtml");
        }
    }
}