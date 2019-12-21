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
using PE.HMIWWW.ViewModel.Module.Lite.CassetteType;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.Core;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class CassetteTypeService : BaseService, ICassetteTypeService
  {
    #region interface ICassetteTypeService
    public DataSourceResult GetCassetteTypeList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        IQueryable<RLSCassetteType> list = ctx.RLSCassetteTypes.AsQueryable();
        result = list.ToDataSourceResult<RLSCassetteType, VM_CassetteType>(request, modelState, data => new VM_CassetteType(data));
      }
      return result;
    }
    public VM_CassetteType GetCassetteType(ModelStateDictionary modelState, long id)
    {
      VM_CassetteType returnValueVm = null;

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
        RLSCassetteType cassetteType = ctx.RLSCassetteTypes.Where(x => x.CassetteTypeId == id).FirstOrDefault(); // uow.Repository<CassetteType>().Find(id);
        if (cassetteType != null)
        {
          returnValueVm = new VM_CassetteType(cassetteType);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public async Task<VM_Base> InsertCassetteType(ModelStateDictionary modelState, VM_CassetteType viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCCassetteTypeData entryDataContract = new DCCassetteTypeData();
      entryDataContract.CassetteTypeName = viewModel.CassetteTypeName;
      entryDataContract.Description = viewModel.Description;
      entryDataContract.NumberOfRolls = viewModel.NumberOfRolls;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.InsertCassetteTypeAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }


    public async Task<VM_Base> UpdateCassetteType(ModelStateDictionary modelState, VM_CassetteType viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCCassetteTypeData entryDataContract = new DCCassetteTypeData();
      entryDataContract.Id = viewModel.Id;
      entryDataContract.CassetteTypeName = viewModel.CassetteTypeName;
      entryDataContract.Description = viewModel.Description;
      entryDataContract.NumberOfRolls = viewModel.NumberOfRolls;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateCassetteTypeAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
    public async Task<VM_Base> DeleteCassetteType(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCCassetteTypeData entryDataContract = new DCCassetteTypeData();
      entryDataContract.Id = viewModel.Id;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteCassetteTypeAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
    #endregion
  }
}
