using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.RollShop;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.Module.Lite.RollsManagement;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.Core;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class RollsManagementService : BaseService, IRollsManagementService
  {
    public DataSourceResult GetRollsList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        IQueryable<V_RollsWithTypes> list = ctx.V_RollsWithTypes.AsQueryable();
        result = list.ToDataSourceResult<V_RollsWithTypes, VM_RollsWithTypes>(request, modelState, data => new VM_RollsWithTypes(data));
      }
      return result;
    }

    public VM_RollsWithTypes GetRoll(ModelStateDictionary modelState, long id)
    {
      VM_RollsWithTypes returnValueVm = null;

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
        V_RollsWithTypes roll = ctx.V_RollsWithTypes.Where(z => z.RollId == id).Single();
        if (roll != null)
        {
          returnValueVm = new VM_RollsWithTypes(roll);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public async Task<VM_Base> InsertRoll(ModelStateDictionary modelState, VM_RollsWithTypes viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollData entryDataContract = new DCRollData();
      entryDataContract.ActualDiameter = viewModel.ActualDiameter;
      entryDataContract.Description = viewModel.Description;
      entryDataContract.DiameterMax = viewModel.DiameterMax;
      entryDataContract.DiameterMin = viewModel.DiameterMin;
      entryDataContract.GroovesNumber = viewModel.GroovesNumber;
      entryDataContract.InitialDiameter = viewModel.InitialDiameter;
      entryDataContract.Length = viewModel.Length;
      entryDataContract.MinimumDiameter = viewModel.MinimumDiameter;
      entryDataContract.RollName = viewModel.RollName;
      entryDataContract.RollSetBottom = viewModel.RollSetBottom;
      entryDataContract.RollSetName = viewModel.RollSetName;
      entryDataContract.RollSetUpper = viewModel.RollSetUpper;
      entryDataContract.RollSetThird = viewModel.RollSetThird;
      entryDataContract.RollTypeDescription = viewModel.RollTypeDescription;
      entryDataContract.RollTypeId = viewModel.RollTypeId;
      entryDataContract.RollTypeName = viewModel.RollTypeName;
      entryDataContract.RoughnessMax = viewModel.RoughnessMax;
      entryDataContract.RoughnessMin = viewModel.RoughnessMin;
      entryDataContract.ScrapDate = viewModel.ScrapDate;
      entryDataContract.ScrapReason = viewModel.ScrapReason;
      entryDataContract.Status = RollStatus.New;
      entryDataContract.StatusName = viewModel.StatusName;
      entryDataContract.SteelgradeRoll = viewModel.SteelgradeRoll;
      entryDataContract.Supplier = viewModel.Supplier;
      entryDataContract.YieldStrengthRef = viewModel.YieldStrengthRef;

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.InsertRollAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> UpdateRoll(ModelStateDictionary modelState, VM_RollsWithTypes viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollData entryDataContract = new DCRollData();
      entryDataContract.ActualDiameter = viewModel.ActualDiameter;
      entryDataContract.Description = viewModel.Description;
      entryDataContract.DiameterMax = viewModel.DiameterMax;
      entryDataContract.DiameterMin = viewModel.DiameterMin;
      entryDataContract.GroovesNumber = viewModel.GroovesNumber;
      entryDataContract.InitialDiameter = viewModel.InitialDiameter;
      entryDataContract.Length = viewModel.Length;
      entryDataContract.MinimumDiameter = viewModel.MinimumDiameter;
      entryDataContract.RollName = viewModel.RollName;
      entryDataContract.RollSetBottom = viewModel.RollSetBottom;
      entryDataContract.RollSetName = viewModel.RollSetName;
      entryDataContract.RollSetUpper = viewModel.RollSetUpper;
      entryDataContract.RollSetThird = viewModel.RollSetThird;
      entryDataContract.RollTypeDescription = viewModel.RollTypeDescription;
      entryDataContract.RollTypeId = viewModel.RollTypeId;
      entryDataContract.RollTypeName = viewModel.RollTypeName;
      entryDataContract.RoughnessMax = viewModel.RoughnessMax;
      entryDataContract.RoughnessMin = viewModel.RoughnessMin;
      entryDataContract.ScrapDate = viewModel.ScrapDate;
      entryDataContract.ScrapReason = viewModel.ScrapReason;
      entryDataContract.Status = (RollStatus)viewModel.Status;
      entryDataContract.StatusName = viewModel.StatusName;
      entryDataContract.SteelgradeRoll = viewModel.SteelgradeRoll;
      entryDataContract.Supplier = viewModel.Supplier;
      entryDataContract.YieldStrengthRef = viewModel.YieldStrengthRef;
      entryDataContract.Id = viewModel.Id;

      //using (PEContext ctx = new PEContext())
      //{
      //  RLSRollType rollType = ctx.RLSRollTypes.Where(x => x.RollTypeId == viewModel.RollTypeId).Single();
      //  entryDataContract.RollTypeId = vi
      //}
      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateRollAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> ScrapRoll(ModelStateDictionary modelState, VM_RollsWithTypes viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollData entryDataContract = new DCRollData();
      entryDataContract.Id = viewModel.Id;
      entryDataContract.RollName = viewModel.RollName;
      entryDataContract.RollTypeId = viewModel.RollTypeId;
      entryDataContract.ScrapDate = viewModel.ScrapDate;
      entryDataContract.ScrapReason = viewModel.ScrapReason;
      entryDataContract.Status = (RollStatus)viewModel.Status;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.ScrapRollAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteRoll(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollData entryDataContract = new DCRollData();
      entryDataContract.Id = viewModel.Id;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteRollAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_RollsetDisplay GetRollSetDisplay(ModelStateDictionary modelState, long id)
    {
      VM_RollsetDisplay returnValueVm = null;

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
        V_RollsetOverviewNewest rsOverview = ctx.V_RollsetOverviewNewest.Where(z => z.RollSetId == id).Single();

        if (rsOverview != null)
        {
          IList<V_RollHistory> RollHistoryBottom = ctx.V_RollHistory.Where(z => z.RollId == rsOverview.RollIdBottom).ToList();
          IList<V_RollHistory> RollHistoryUpper = ctx.V_RollHistory.Where(z => z.RollId == rsOverview.RollIdUpper).ToList();
          IList<V_RollHistory> RollHistoryThird = ctx.V_RollHistory.Where(z => z.RollId == rsOverview.RollIdThird).ToList();
          returnValueVm = new VM_RollsetDisplay(rsOverview, RollHistoryUpper, RollHistoryBottom, RollHistoryThird);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
  }
}
