using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using PE.HMIWWW.ViewModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class SteelgradeController : BaseController
  {
    private readonly ISteelgradeService _service;

    public SteelgradeController(ISteelgradeService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Steelgrade/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GetSteelgradeList([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _service.GetSteelgradeList(ModelState, request));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> UpdateSteelgrade(VM_Steelgrade steelgrade)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.UpdateSteelgrade(ModelState, steelgrade));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> SteelgradeEditPopup(long steelgradeId)
    {
      ViewBag.GroupList = _service.GetSteelgradeGroups();
      return await PreparePopupActionResultFromVm(() => _service.GetSteelgrade(ModelState, steelgradeId), "~/Views/Module/PE.Lite/Steelgrade/_SteelgradeEditPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> CreateSteelgrade(VM_Steelgrade steelgrade)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.CreateSteelgrade(ModelState, steelgrade));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Update)]
    public async Task<ActionResult> SteelgradeCreatePopup()
    {
      ViewBag.GroupList = _service.GetSteelgradeGroups();
      VM_Steelgrade result = new VM_Steelgrade();
      return await PreparePopupActionResultFromVm(() => result, "~/Views/Module/PE.Lite/Steelgrade/_SteelgradeCreatePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<ActionResult> ElementDetails(long Id)
    {
      return await PrepareActionResultFromVm(() => _service.GetSteelgradeDetails(ModelState, Id), "~/Views/Module/PE.Lite/Steelgrade/_SteelgradeBody.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager, RightLevel.Delete)]
    public async Task<JsonResult> DeleteSteelgrade(long itemId)
    {
      VM_Steelgrade steelgrade = new VM_Steelgrade()
      {
        Id = itemId
      };
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteSteelgrade(ModelState, steelgrade));
    }

  }
}
