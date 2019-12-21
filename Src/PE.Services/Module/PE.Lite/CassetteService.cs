using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.DTO.Internal.RollShop;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Cassette;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.Core;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class CassetteService : BaseService, ICassetteService
  {
    #region interface ICassetteService
    public DataSourceResult GetCassetteList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        IQueryable<V_CassettesOverview> list = ctx.V_CassettesOverview.AsQueryable();
        result = list.ToDataSourceResult<V_CassettesOverview, VM_CassetteOverview>(request, modelState, data => new VM_CassetteOverview(data));
      }
      return result;
    }
    public VM_CassetteOverview GetCassette(ModelStateDictionary modelState, long id)
    {
      VM_CassetteOverview returnValueVm = null;

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
      using (PEContext ctx = new PEContext())
      {
        V_CassettesOverview cassette = ctx.V_CassettesOverview.Where(x => x.CassetteId == id).FirstOrDefault(); // uow.Repository<Cassette>().Find(id);
        if (cassette != null)
        {
          returnValueVm = new VM_CassetteOverview(cassette);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public async Task<VM_Base> InsertCassette(ModelStateDictionary modelState, VM_CassetteOverview viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCCassetteData entryDataContract = new DCCassetteData();
      entryDataContract.CassetteName = viewModel.CassetteName;
      entryDataContract.CassetteTypeId = viewModel.FkCassetteType;
      entryDataContract.NumberOfPositions = viewModel.NumberOfPositions;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.InsertCassetteAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }


    public async Task<VM_Base> UpdateCassette(ModelStateDictionary modelState, VM_CassetteOverview viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCCassetteData entryDataContract = new DCCassetteData();
      entryDataContract.Id = viewModel.Id;
      entryDataContract.CassetteName = viewModel.CassetteName;
      entryDataContract.CassetteTypeId = viewModel.FkCassetteType;
      entryDataContract.NumberOfPositions = viewModel.NumberOfPositions;
      entryDataContract.Status = viewModel.Status;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateCassetteAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
    public async Task<VM_Base> DeleteCassette(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCCassetteData entryDataContract = new DCCassetteData();
      entryDataContract.Id = viewModel.Id;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteCassetteAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
    #endregion
  }
}
