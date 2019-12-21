using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Furnace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{

	public class FurnaceController : BaseController
	{

    IFurnaceService _furnaceService;
    public FurnaceController(IFurnaceService furnaceService)
    {
      _furnaceService = furnaceService;
    }

    // GET: Furnace
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Furnace, Constants.SmfAuthorization_Controller_Furnace, RightLevel.View)]
    public ActionResult Index()
		{
			return View("~/Views/Module/PE.Lite/Furnace/Index.cshtml");

		}



    [SmfAuthorization(Constants.SmfAuthorization_Controller_Furnace, Constants.SmfAuthorization_Controller_Furnace, RightLevel.View)]

    public JsonResult GetMaterialInFurnace()
    {
			return Json(_furnaceService.GetMaterialInFurnace(), JsonRequestBehavior.AllowGet);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Furnace, Constants.SmfAuthorization_Controller_Furnace, RightLevel.View)]

    //public async Task<ActionResult> MaterialDetails(long MaterialId,long? WorkOrderId,long? SteelgradeId,long? HeatId)
    //{
    //  return await PrepareActionResultFromVm(() => _furnaceService.GetMaterialDetails(ModelState, MaterialId), "~/Views/Module/PE.Lite/Furnace/_RawMaterialDetails.cshtml");
    //}

		public PartialViewResult MaterialDetails(VM_Furnace model)
    {
      if(model.HeatId != null) model.HeatOverview = _furnaceService.GetHeatDetails(ModelState, (long)model.HeatId);
      if(model.WorkOrderId != null) model.WorkOrderOverview = _furnaceService.GetWorkOrderDetails(ModelState, (long)model.WorkOrderId);
      return PartialView("~/Views/Module/PE.Lite/Furnace/_RawMaterialDetails.cshtml", model);
    }
  }
}
