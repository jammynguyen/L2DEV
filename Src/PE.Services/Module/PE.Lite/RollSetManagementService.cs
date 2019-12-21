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
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using PE.HMIWWW.ViewModel.System;
using PE.Interfaces.Lite;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class RollSetManagementService : BaseService, IRollSetManagementService
  {
    #region interface IRollSetManagementService
    public DataSourceResult GetRollSetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        List<V_RollsetOverviewNewest> list = ctx.V_RollsetOverviewNewest.Where(z => z.RollSetStatus != (short)RollSetStatus.ScheduledForAssemble).ToList();
        result = list.ToDataSourceResult<V_RollsetOverviewNewest, VM_RollSetOverviewFull>(request, modelState, data => new VM_RollSetOverviewFull(data));
      }
      return result;
    }
    public DataSourceResult GetScheduledRollSetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        //var list = uow.Repository<VRollsetOverviewNewest>().Query(z => z.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.ScheduledForAssemble).Get();
        List<V_RollsetOverviewNewest> list = ctx.V_RollsetOverviewNewest.Where(z => z.RollSetStatus == (short)RollSetStatus.ScheduledForAssemble).ToList();
        result = list.ToDataSourceResult<V_RollsetOverviewNewest, VM_RollSetOverviewFull>(request, modelState, data => new VM_RollSetOverviewFull(data));
      }
      return result;
    }
    public VM_RollSetOverviewFull GetRollSet(ModelStateDictionary modelState, long id)
    {
      VM_RollSetOverviewFull returnValueVm = null;

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
        //VRollsetOverviewNewest RollSet = uow.Repository<VRollsetOverviewNewest>().Query(z => z.RollSetId == id).GetSingle();
        V_RollsetOverviewNewest rollSet = ctx.V_RollsetOverviewNewest.Where(x => x.RollSetId == id).FirstOrDefault();
        if (rollSet != null)
        {
          returnValueVm = new VM_RollSetOverviewFull(rollSet);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public async Task<VM_Base> InsertRollSet(ModelStateDictionary modelState, VM_RollSetOverviewFull viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollSetData entryDataContract = new DCRollSetData
      {
        RollSetName = viewModel.RollSetName,
        RollSetType = viewModel.RollSetType,
        RollSetStatus = RollSetStatus.Empty
      };


      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.InsertRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> AssembleRollSet(ModelStateDictionary modelState, VM_RollSetOverviewFull viewModel)
    {
      VM_Base result = new VM_Base();
      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION
      DCRollSetData entryDataContract = new DCRollSetData
      {
        Id = viewModel.Id,
        RollSetName = viewModel.RollSetName,
        BottomRollId = viewModel.BottomRollId,
        UpperRollId = viewModel.UpperRollId,
        ThirdRollId = viewModel.ThirdRollId,
        RollSetType = viewModel.RollSetType,
        RollSetStatus = RollSetStatus.ScheduledForAssemble
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AssembleRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
    public async Task<VM_Base> UpdateRollSetStatus(ModelStateDictionary modelState, VM_RollSetOverviewFull viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollSetData entryDataContract = new DCRollSetData();
      entryDataContract.Id = viewModel.Id;
      entryDataContract.RollSetStatus = viewModel.RollSetStatus;


      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateRollSetStatusAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
    public async Task<VM_Base> ConfirmRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();
     
      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollSetData entryDataContract = new DCRollSetData
      {
        Id = viewModel.Id,
        RollSetStatus = RollSetStatus.Ready
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.ConfirmRollSetStatusAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> CancelRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base returnVM = new VM_Base();

      ////VALIDATE ENTRY PARAMETERS
      //if (!modelState.IsValid)
      //{
      //  return returnVM;
      //}
      ////END OF VALIDATION

      //DCRollSetData entryDataContract = new DCRollSetData();
      //ModuleController.InitDataContract(entryDataContract);
      //entryDataContract.Id = viewModel.Id;
      //entryDataContract.RollSetStatus = (short)PE.Core.Constants.RollSetStatus.Empty;
      //// entryDataContract.IsThirdRoll = (short)PE.Core.Constants.NumberOfActiveRoll.twoActiveRollsRM; // default = 0

      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.CancelRollSetStatus(entryDataContract);

      //if (await Task.WhenAny(taskOnRemoteModule) == taskOnRemoteModule)
      //{
      //  //task completed within timeout
      //  //use the result from "SendOffice"
      //  if (taskOnRemoteModule.Result != null)
      //  {
      //    //check communication
      //    if (taskOnRemoteModule.Result.OperationSuccess)
      //    {
      //      //check result of request
      //      if (taskOnRemoteModule.Result.DataConctract)
      //      {
      //        ModuleController.Logger.Info("ProdManager operation: \"UpdateRollSet\" result: OK");
      //      }
      //      else
      //      {
      //        ModuleController.Logger.Error("Error in ProdManager during operation:  \"UpdateRollSet\"");
      //        throw new Exception(PE.HMIWWW.Core.Resources.ResourceController.GetErrorText("ErrorInModuleProdManager"));
      //      }
      //    }
      //    else
      //      throw new Exception(ResourceController.GetErrorText("NoCommunication"));
      //  }
      //}
      //else
      //{
      //  // timeout logic
      //  throw new Exception(PE.HMIWWW.Core.Resources.ResourceController.GetErrorText("TimeoutInModuleProdManager"));
      //}
      return returnVM;
    }

    public async Task<VM_Base> DisassembleRollSet(ModelStateDictionary modelState, VM_RollSetOverviewFull viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollSetData entryDataContract = new DCRollSetData
      {
        Id = viewModel.Id
      };

      //entryDataContract.Dismounted = DateTime.Now;
      // RM = 0, IM = 1 , K500 = 2, K370 = 3
      //   entryDataContract.Dismounted = DateTime.Now;

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DisassembleRollSetAsync(entryDataContract);

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
        //VRollsetOverviewNewest rsOverview = uow.Repository<VRollsetOverviewNewest>().Query(z => z.RollSetId == id).GetSingle();
        V_RollsetOverviewNewest rollSetOverview = ctx.V_RollsetOverviewNewest.Where(z => z.RollSetId == id).FirstOrDefault();

        if (rollSetOverview != null)
        {
          //IList<V_RollHistory> RollHistoryBottom = uow.Repository<VRollHistory>().Query(z => z.RollId == rsOverview.RollIdBottom).Get().ToList();
          IList<V_RollHistory> RollHistoryBottom = ctx.V_RollHistory.Where(z => z.RollId == rollSetOverview.RollIdBottom && z.RollSetHistoryId == rollSetOverview.RollSetHistoryId).ToList();
          //IList <V_RollHistory> RollHistoryUpper = uow.Repository<VRollHistory>().Query(z => z.RollId == rsOverview.RollIdUpper).Get().ToList();
          IList<V_RollHistory> RollHistoryUpper = ctx.V_RollHistory.Where(z => z.RollId == rollSetOverview.RollIdUpper && z.RollSetHistoryId == rollSetOverview.RollSetHistoryId).ToList();
          returnValueVm = new VM_RollsetDisplay(rollSetOverview, RollHistoryUpper, RollHistoryBottom);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public async Task<VM_Base> DeleteRollSet(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

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
        return result;
      }
      //END OF VALIDATION

      DCRollSetData entryDataContract = new DCRollSetData
      {
        Id = viewModel.Id
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public static List<RLSRollSet> GetEmptyRollsetList()
    {
      List<RLSRollSet> rollSet = null;

      using (PEContext ctx = new PEContext())
      {
        rollSet = ctx.RLSRollSets.Where(r => r.Status == RollSetStatus.Empty).ToList();
      }
     
      return rollSet;
    }

    public static SelectList GetRolls(string RollSetId)  // filtr for Rolls (upperRoll). The same RollType in AssembleRollSetDilog & AssembleExtendedRollSetDialog

    {
      long rollSetId = -1;
      rollSetId = Int64.Parse(RollSetId);
      using (PEContext ctx = new PEContext())
      {
        RLSRollSet rollSet = ctx.RLSRollSets.Where(z => z.RollSetId == rollSetId).FirstOrDefault();
        if (rollSet != null)
        {
          //evalutate roll type based on Rollset type
          //IList<VRollsWithType> rolls4SelectList = uow.Repository<VRollsWithType>().Query(r => ((r.Status == (short)PE.Core.Constants.RollStatus.New || r.Status == (short)PE.Core.Constants.RollStatus.Unassigned)) && r.MatchingRollsetType == rollSet.RollSetType)
          //  .OrderBy(o => o.OrderBy(f => f.RollName)).Get().ToList();

          IList<V_RollsWithTypes> rollsWithType = ctx.V_RollsWithTypes
                                                          .Where(r => ((r.Status == (short)RollStatus.New || r.Status == (short)RollStatus.Unassigned)) && r.MatchingRollsetType == (short)rollSet.RollSetType)
                                                          .OrderBy(f => f.RollName)
                                                          .ToList();
          Dictionary<long, string> resultDictionary = new Dictionary<long, string>();

          foreach (V_RollsWithTypes rl in rollsWithType)
          {
            resultDictionary.Add(rl.RollId, rl.RollName);
          }
          SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
          return tmpSelectList;
        }
        else
        {
          return null;
        }
      }
    }

    public static SelectList GetGrooveTemplates()
    {
      using (PEContext ctx = new PEContext())
      {
        //IList<GrooveTemplate> GrooveTemplates = uow.Repository<GrooveTemplate>()
        //    .Query()
        //    .Get()
        //    .ToList();

        IList<RLSGrooveTemplate> grooveTemplates = ctx.RLSGrooveTemplates.ToList();

        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
        foreach (RLSGrooveTemplate element in grooveTemplates)
        {
          resultDictionary.Add(element.GrooveTemplateId, element.GrooveTemplateName);
        }
        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
        return tmpSelectList;
      }
    }

    //public DataSourceResult GetRollSetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, long id)
    //{
    //  DataSourceResult result = null;

    //  using (PEUnitOfWork uow = new PEUnitOfWork())
    //  {
    //    var list = uow.Repository<VRollsetOverviewNewest>().Query(z => z.RollSetId == id).Get();

    //    result = list.ToDataSourceResult<VRollsetOverviewNewest, VM_RollSetOverview>(request, modelState, data => new VM_RollSetOverview(data));
    //  }
    //  return result;
    //}

    public static SelectList GetRollsWithMaterials(string uRollId)  // filtr for Rolls (bottomRoll & thirdRoll). The same RollType and SteelgradeRoll in AssembleRollSetDilog & AssembleExtendedRollSetDialog
    {
      long upperRollId = -1;
      upperRollId = Int64.Parse(uRollId);

      using (PEContext ctx = new PEContext())
      {
        //VRollsWithType upperRoll = uow.Repository<VRollsWithType>().Query(z => z.RollId == upperRollId).GetSingle();

        V_RollsWithTypes upperRoll = ctx.V_RollsWithTypes.Where(x => x.RollId == upperRollId).FirstOrDefault();

        //VRollsWithType rollType = uow.Repository<VRollsWithType>().Query(r => ((r.Status == (short)PE.Core.Constants.RollStatus.New || r.Status == (short)PE.Core.Constants.RollStatus.Unassigned)) && r.RollType == upperRoll.RollType && r.SteelgradeRoll == upperRoll.SteelgradeRoll).GetSingle();

        //V_RollsWithTypes rollType = ctx.V_RollsWithTypes.Where(r => ((r.Status == (short)RollStatus.New || r.Status == (short)RollStatus.Unassigned)) && r.RollType == upperRoll.RollType && r.SteelgradeRoll == upperRoll.SteelgradeRoll).FirstOrDefault();

        //IList<VRollsWithType> rollList = uow.Repository<VRollsWithType>()
        //    .Query(r => ((r.Status == (short)PE.Core.Constants.RollStatus.New || r.Status == (short)PE.Core.Constants.RollStatus.Unassigned))
        //    && r.RollType == upperRoll.RollType
        //    && r.SteelgradeRoll == upperRoll.SteelgradeRoll)
        //    .Get()
        //    .ToList();

        IList<V_RollsWithTypes> rollList = ctx.V_RollsWithTypes.Where(r => ((r.Status == (short)RollStatus.New || r.Status == (short)RollStatus.Unassigned)) && r.RollType == upperRoll.RollType && r.SteelgradeRoll == upperRoll.SteelgradeRoll).ToList();

        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
        foreach (V_RollsWithTypes rl in rollList)
        {
          resultDictionary.Add(rl.RollId, rl.RollName);
        }
        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
        return tmpSelectList;
      }
    }


    #endregion
  }
}
