using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
    public class VisualizationController : BaseController
  {
    // GET: Visualization
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult Index()
        {
      return View("~/Views/Module/PE.Lite/Visualization/Index.cshtml");
    }
    }
}
