using Kendo.Mvc.UI;
using PE.Core;
using PE.DbEntity.Model;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Controllers.System
{
  public class CrewController : BaseController
  {
    #region services

    ICrewService _crewService;

    #endregion

    public CrewController(ICrewService service)
    {
      _crewService = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<JsonResult> GetCrewData([DataSourceRequest]DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _crewService.GetCrewList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> EditCrewDialog(long id)
    {
      return await PreparePopupActionResultFromVm(() => _crewService.GetCrew(ModelState, id), "EditCrewDialog");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> UpdateCrew(VM_Crew viewModel)
    {
      return await PrepareJsonResultFromVm(() => _crewService.UpdateCrew(ModelState, viewModel));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<JsonResult> Delete([DataSourceRequest] DataSourceRequest request, VM_LongId viewModel)
    {
      return await PrepareJsonResultFromVm(() => _crewService.DeleteCrew(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddCrewDialog()
    {
      return PartialView("AddCrewDialog");
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<JsonResult> InsertCrew(VM_Crew viewModel)
    {
      return await PrepareJsonResultFromVm(() => _crewService.InsertCrew(ModelState, viewModel));
    }

    public static List<Crew> GetCrewsList()
    {
      return CrewService.GetCrewsList();
    }
  }
}