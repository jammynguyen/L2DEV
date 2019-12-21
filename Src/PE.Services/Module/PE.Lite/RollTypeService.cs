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
using PE.HMIWWW.ViewModel.Module.Lite.RollType;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.Core;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class RollTypeService : BaseService, IRollTypeService
  {
    #region interface ICustomerService
    public DataSourceResult GetRollTypeList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        IQueryable<RLSRollType> list = ctx.RLSRollTypes.AsQueryable();
        List<long> typesInUse = ctx.V_RollsWithTypes.Select(x => x.FKRollTypeId).Distinct().ToList();
        result = list.ToDataSourceResult<RLSRollType, VM_RollType>(request, modelState, data => new VM_RollType(data));
        foreach(VM_RollType rollType in result.Data)
        {
          if (typesInUse.Contains((long)rollType.Id))
            rollType.IsInUse = true;
          else
            rollType.IsInUse = false;
        }
      }
      return result;
    }
    public VM_RollType GetRollType(ModelStateDictionary modelState, long id)
    {
      VM_RollType returnValueVm = null;

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
        RLSRollType rollType = ctx.RLSRollTypes.Where(x => x.RollTypeId == id).Single();
        if(rollType!=null)
        {
          returnValueVm = new VM_RollType(rollType);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public async Task<VM_Base> InsertRollType(ModelStateDictionary modelState, VM_RollType viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollTypeData entryDataContract = new DCRollTypeData();
      entryDataContract.AccBilletCntLimit = viewModel.AccBilletCntLimit;
      entryDataContract.ChokeType = viewModel.ChokeType;
      entryDataContract.DiameterMax = viewModel.DiameterMax;
      entryDataContract.DiameterMin = viewModel.DiameterMin;
      entryDataContract.DrawingName = viewModel.DrawingName;
      entryDataContract.Length = viewModel.Length;
      entryDataContract.RollTypeDescription = viewModel.RollTypeDescription;
      entryDataContract.RollTypeName = viewModel.RollTypeName;
      entryDataContract.RoughnessMax = viewModel.RoughnessMax;
      entryDataContract.RoughnessMin = viewModel.RoughnessMin;
      entryDataContract.SteelgradeRoll = viewModel.SteelgradeRoll;
      entryDataContract.YieldStrengthRef = viewModel.YieldStrengthRef;
      entryDataContract.TimeStamp = DateTime.Now;
      entryDataContract.MatchingRollSetType = viewModel.MatchingRollsetType;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.InsertRollTypeAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }


    public async Task<VM_Base> UpdateRollType(ModelStateDictionary modelState, VM_RollType viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollTypeData entryDataContract = new DCRollTypeData();
      entryDataContract.Id = viewModel.Id;
      entryDataContract.AccBilletCntLimit = viewModel.AccBilletCntLimit;
      entryDataContract.ChokeType = viewModel.ChokeType;
      entryDataContract.DiameterMax = viewModel.DiameterMax;
      entryDataContract.DiameterMin = viewModel.DiameterMin;
      entryDataContract.DrawingName = viewModel.DrawingName;
      entryDataContract.Length = viewModel.Length;
      entryDataContract.RollTypeDescription = viewModel.RollTypeDescription;
      entryDataContract.RollTypeName = viewModel.RollTypeName;
      entryDataContract.RoughnessMax = viewModel.RoughnessMax;
      entryDataContract.RoughnessMin = viewModel.RoughnessMin;
      entryDataContract.SteelgradeRoll = viewModel.SteelgradeRoll;
      entryDataContract.YieldStrengthRef = viewModel.YieldStrengthRef;
      entryDataContract.TimeStamp = DateTime.Now;
      entryDataContract.MatchingRollSetType = viewModel.MatchingRollsetType;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateRollTypeAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteRollType(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollTypeData entryDataContract = new DCRollTypeData();
      entryDataContract.Id = viewModel.Id;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteRollTypeAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
    #endregion

  }
}
