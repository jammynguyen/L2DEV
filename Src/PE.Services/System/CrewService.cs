using PE.HMIWWW.ViewModel.System;
using SMF.DbEntity;
using SMF.DbEntity.Model;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Threading.Tasks;
using SMF.RepositoryPattern.Infrastructure;
using PE.DbEntity.Model;
using PE.DbEntity;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.DbEntity.Models;

namespace PE.HMIWWW.Services.System
{
  public interface ICrewService
  {
    DataSourceResult GetCrewList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    VM_LongId UpdateCrew(ModelStateDictionary modelState, VM_Crew viewModel);
    VM_LongId DeleteCrew(ModelStateDictionary modelState, VM_LongId viewModel);
    VM_Crew GetCrew(ModelStateDictionary modelState, long id);
    VM_LongId InsertCrew(ModelStateDictionary modelState, VM_Crew viewModel);
  }
  public class CrewService : BaseService, ICrewService
  {

    #region interface ICrewService

    public VM_LongId DeleteCrew(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_LongId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (viewModel.Id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      using (PEContext uow = new PEContext())
      {
        Crew role = uow.Crews.Find(viewModel.Id);
        if (role != null)
        {
          uow.Crews.Remove(role);
          uow.SaveChanges();
          returnValueVm = new VM_LongId(viewModel.Id);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_Crew GetCrew(ModelStateDictionary modelState, long id)
    {
      VM_Crew returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION


      //DB OPERATION
      using (PEContext uow = new PEContext())
      {
        Crew crew = uow.Crews.Find(id);
        if (crew != null)
        {
          returnValueVm = new VM_Crew(crew);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public DataSourceResult GetCrewList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      //DB OPERATION
      using (PEContext uow = new PEContext())
      {
        List<Crew> list = uow.Crews.ToList();
        returnValue = list.ToDataSourceResult<Crew, VM_Crew>(request, modelState, data => new VM_Crew(data));
      }
      //END OF DB OPERATION

      return returnValue;
    }

    public VM_LongId InsertCrew(ModelStateDictionary modelState, VM_Crew viewModel)
    {
      VM_LongId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (viewModel.CrewName == null || viewModel.CrewName == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      using (PEContext uow = new PEContext())
      {
        //Role role = uow.Repository<Role>().Find(viewModel.Id);
        Crew crew = new Crew();
        crew.CrewName = viewModel.CrewName;
        crew.Description = viewModel.Description;
        crew.CreatedTs = DateTime.Now;
        crew.LastUpdateTs = DateTime.Now;
        uow.Crews.Add(crew);
        uow.SaveChanges();

        returnValueVm = new VM_LongId(crew.CrewId);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_LongId UpdateCrew(ModelStateDictionary modelState, VM_Crew viewModel)
    {
      VM_LongId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (viewModel.CrewId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      using (PEContext uow = new PEContext())
      {
        Crew crew = uow.Crews.Find(viewModel.CrewId);
        crew.CrewName = viewModel.CrewName;
        crew.Description = viewModel.Description;
        uow.SaveChanges();
        returnValueVm = new VM_LongId((long)viewModel.CrewId);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    #endregion

    #region public methods
    public static List<Crew> GetCrewsList()
    {
      List<Crew> crews = null;
      try
      {
        using (PEContext uow = new PEContext())
        {
          crews = uow.Crews.ToList();
        }
      }
      catch
      {

      }
      return crews;
    }
    #endregion

    #region private methods
    private bool IfCrewExists(string crewName)
    {
      bool retValue;
      try
      {
        using (PEContext uow = new PEContext())
        {
          Crew crew = uow.Crews.Where(z => z.CrewName == crewName).Single();
          if (crew != null)
            retValue = true;
          else
            retValue = false;
        }
      }
      catch (Exception ex)
      {
        DbExceptionResult result = DbExceptionHelper.ProcessException(ex, "IfCrewExists::Database operation failed!", null);
        retValue = true;
      }
      return retValue;
    }
    #endregion
  }
}
