using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class MaterialFurnaceController : BaseController
  {
    IMaterialFurnaceService _service;

    public MaterialFurnaceController(IMaterialFurnaceService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_MaterialFurnace, Constants.SmfAuthorization_Module_MVHistory, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/MaterialFurnace/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_MaterialFurnace, Constants.SmfAuthorization_Module_MVHistory, RightLevel.View)]
    public ActionResult GetMaterialsList()
    {
      throw new NotImplementedException();
    }
  }
}

