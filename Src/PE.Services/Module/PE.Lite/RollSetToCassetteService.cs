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
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.Module.Lite.Cassette;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public class RollsetToCassetteService : BaseService, IRollsetToCassetteService
  {
    #region interface IRollsetToCassetteService
    public DataSourceResult GetAvailableCassettesList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        //var list = uow.Repository<VCassettesOverview>().Query(f => (f.IsInterCas == false) && (f.Status == (short)PE.Core.Constants.CassetteStatus.New
        //                                                                                     || f.Status == (short)PE.Core.Constants.CassetteStatus.Empty
        //                                                                                     || (f.Status == (short)PE.Core.Constants.CassetteStatus.RollSetInside && f.RollsetsCnt < f.NumberOfPositions)
        //                                                                                     || (f.Status == (short)PE.Core.Constants.CassetteStatus.AssembleIncomplete && f.RollsetsCnt < f.NumberOfPositions))).Get();

        List<V_CassettesOverview> list = ctx.V_CassettesOverview.Where(f => (f.IsInterCassette == false) && (f.Status == (short)CassetteStatus.New
                                                                                             || f.Status == (short)CassetteStatus.Empty
                                                                                             || (f.Status == (short)CassetteStatus.RollSetInside && f.RollsetsCnt < f.NumberOfPositions)
                                                                                             || (f.Status == (short)CassetteStatus.AssembleIncomplete && f.RollsetsCnt < f.NumberOfPositions))).ToList();

        result = list.ToDataSourceResult<V_CassettesOverview, VM_CassetteOverview>(request, modelState, data => new VM_CassetteOverview(data));
      }
      return result;
    }
    public DataSourceResult GetAvailableInterCassettesList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        //var list = uow.Repository<VCassettesOverview>().Query(f => (f.IsInterCas == true) && (f.Status == (short)PE.Core.Constants.CassetteStatus.New
        //                                                                                                    || f.Status == (short)PE.Core.Constants.CassetteStatus.Empty
        //                                                                                                    || (f.Status == (short)PE.Core.Constants.CassetteStatus.RollSetInside && f.RollsetsCnt < f.NumberOfPositions)
        //                                                                                                    || (f.Status == (short)PE.Core.Constants.CassetteStatus.AssembleIncomplete && f.RollsetsCnt < f.NumberOfPositions))).Get();

        List<V_CassettesOverview> list = ctx.V_CassettesOverview.Where(f => (f.IsInterCassette == true) && (f.Status == (short)CassetteStatus.New
                                                                                                            || f.Status == (short)CassetteStatus.Empty
                                                                                                            || (f.Status == (short)CassetteStatus.RollSetInside && f.RollsetsCnt < f.NumberOfPositions)
                                                                                                            || (f.Status == (short)CassetteStatus.AssembleIncomplete && f.RollsetsCnt < f.NumberOfPositions))).ToList();

        result = list.ToDataSourceResult<V_CassettesOverview, VM_CassetteOverview>(request, modelState, data => new VM_CassetteOverview(data));
      }
      return result;
    }
    public DataSourceResult GetAvailableRollSetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        //var list = uow.Repository<VRollsetOverviewNewest>().Query(x => (x.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Ready && ((x.IsThirdRoll == (short)PE.Core.Constants.RollSetType.twoActiveRollsRM || x.IsThirdRoll == (short)PE.Core.Constants.RollSetType.twoActiveRollsIM
        //                                                            || ((x.IsThirdRoll == (short)PE.Core.Constants.RollSetType.threeActiveRollsK370
        //                                                            || x.IsThirdRoll == (short)PE.Core.Constants.RollSetType.threeActiveRollsK500)
        //                                                            && x.FkInterCassetteId != null)))))
        //                                                            .Get();

        List<V_RollsetOverviewNewest> list = ctx.V_RollsetOverviewNewest.Where(x => x.RollSetStatus == (short)RollSetStatus.Ready).ToList();

        result = list.ToDataSourceResult<V_RollsetOverviewNewest, VM_RollSetOverview>(request, modelState, data => new VM_RollSetOverview(data));
      }
      return result;
    }
    public DataSourceResult GetScheduledRollSetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {

        //var list = uow.Repository<VRollsetOverviewNewest>().Query(x => x.CassetteId != null && (x.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.ScheduledForCassette)).Get();

        List<V_RollsetOverviewNewest> list = ctx.V_RollsetOverviewNewest.Where(x => x.CassetteId != null && (x.RollSetStatus == (short)RollSetStatus.ScheduledForCassette)).ToList();

        result = list.ToDataSourceResult<V_RollsetOverviewNewest, VM_RollSetOverview>(request, modelState, data => new VM_RollSetOverview(data));
      }
      return result;
    }
    public DataSourceResult GetReadyRollSetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        //var list = uow.Repository<VRollsetOverviewNewest>().Query(x => x.CassetteId != null && (x.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.ReadyForMounting) || x.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Dismounted).Get();
        List<V_RollsetOverviewNewest> list = ctx.V_RollsetOverviewNewest.Where(x => x.CassetteId != null && (x.RollSetStatus == (short)RollSetStatus.ReadyForMounting)).ToList();

        result = list.ToDataSourceResult<V_RollsetOverviewNewest, VM_RollSetOverview>(request, modelState, data => new VM_RollSetOverview(data));
      }
      return result;
    }
    public async Task<VM_Base> ConfirmRsReadyForMounting(ModelStateDictionary modelState, VM_LongId viewModel)
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

      //DCRollSetData entryDataContract = new DCRollSetData();

      //entryDataContract.Id = viewModel.Id;
      //entryDataContract.RollSetStatus = RollSetStatus.ReadyForMounting;

      ////DB OPERATION
      //using (PEContext ctx = new PEContext())
      //{
      //  RLSRollSet rollSet = ctx.RLSRollSets.Where(z => z.RollSetId == viewModel.Id).FirstOrDefault();
      //  if (rollSet != null)
      //  {
      //    entryDataContract.RollSetName = rollSet.RollSetName;
      //  }
      //  V_RollsetOverviewNewest view = ctx.V_RollsetOverviewNewest.Where(z => z.RollSetId == rollSet.RollSetId).FirstOrDefault();
      //  entryDataContract.CassetteId = view.CassetteId;
      //}

      DCRollSetToCassetteAction dataToSend = new DCRollSetToCassetteAction();
      dataToSend.RollSetId = viewModel.Id;
      dataToSend.Action = RollSetCassetteAction.ConfirmRollSet;

      using (PEContext ctx = new PEContext())
      {
        RLSRollSet rollSet = ctx.RLSRollSets.Where(z => z.RollSetId == viewModel.Id).FirstOrDefault();
        if (rollSet != null)
        {
          //entryDataContract.RollSetName = rollSet.RollSetName;
        }
        V_RollsetOverviewNewest view = ctx.V_RollsetOverviewNewest.Where(z => z.RollSetId == rollSet.RollSetId).FirstOrDefault();
        dataToSend.CassetteId = view.CassetteId;
      }


      //END OF DB OPERATION
      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.ConfirmRollsetReadyForMounting(entryDataContract);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollSetToCassetteAction(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
    public VM_CassetteOverviewWithPositions GetCassetteOverviewWithPositions(ModelStateDictionary modelState, long id)
    {
      VM_CassetteOverviewWithPositions returnValueVm = null;

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
        //VCassettesOverview cassette = uow.Repository<VCassettesOverview>().Query(z => z.CassetteId == id).GetSingle();

        V_CassettesOverview cassette = ctx.V_CassettesOverview.Where(z => z.CassetteId == id).FirstOrDefault();

        if (cassette != null)
        {
          //IList<VRollsetOverviewNewest> AvailableRollets = uow.Repository<VRollsetOverviewNewest>().Query(x => (x.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Ready)).Get().ToList();

          IList<V_RollsetOverviewNewest> availableRollSets = ctx.V_RollsetOverviewNewest.Where(x => (x.RollSetStatus == (short)RollSetStatus.Ready)).ToList();

          returnValueVm = new VM_CassetteOverviewWithPositions(cassette, availableRollSets);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public async Task<VM_Base> AssembleRollSetAndCassette(ModelStateDictionary modelState, VM_CassetteOverviewWithPositions viewModel)
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


      DCRollSetToCassetteAction entryDataContract = new DCRollSetToCassetteAction();

      entryDataContract.CassetteId = viewModel.Id;
      if (viewModel.RollSetss.Count > 1)
      {
        entryDataContract.RollSetIdList = new List<long?>();
        foreach (VM_RollSetShort RollSetElement in viewModel.RollSetss)
        {
          entryDataContract.RollSetIdList.Add(RollSetElement.Id);
        }
      }
      else
      {
        foreach (VM_RollSetShort RollSetElement in viewModel.RollSetss)
        {
          entryDataContract.RollSetId = RollSetElement.Id;
          entryDataContract.Postion = 1;
        }
      }


      //    entryDataContract.InterCassetteId = viewModel.Id; // id InterCassette
      entryDataContract.CassetteId = viewModel.Id;
      entryDataContract.Action = RollSetCassetteAction.PlanRollSet;

      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.AssembleRollSetAndCassette(entryDataContract);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollSetToCassetteAction(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }


    //commented out on 07.10.2019. DataContract was initialized, but never used
    public async Task<VM_Base> UnloadRollSet(ModelStateDictionary modelState, VM_LongId viewModel)
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


      DCRollSetToCassetteAction entryDataContract = new DCRollSetToCassetteAction();

      entryDataContract.RollSetId = viewModel.Id;
      
      using (PEContext ctx = new PEContext())
      {
        V_RollSetOverview rollSet = ctx.V_RollSetOverview.Where(z => z.RollSetId == viewModel.Id && z.CassetteId != null).FirstOrDefault();//  uow.Repository<VRollsetOverview>().Query(z => z.RollSetId == viewModel.Id && z.CassetteId != null).GetSingle();
        if (rollSet != null)
        {
          entryDataContract.CassetteId = rollSet.CassetteId;
        }
      }

      entryDataContract.Action = RollSetCassetteAction.RemoveRollSet;

      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.UnloadRollSet(entryDataContract);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollSetToCassetteAction(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    //commented out on 07.10.2019. DataContract was initialized, but never used
    public async Task<VM_Base> CancelPlan(ModelStateDictionary modelState, VM_LongId viewModel)
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



      DCRollSetToCassetteAction entryDataContract = new DCRollSetToCassetteAction();

      entryDataContract.RollSetId = viewModel.Id;

      using (PEContext ctx = new PEContext())
      {
        V_RollSetOverview rollSet = ctx.V_RollSetOverview.Where(z => z.RollSetId == viewModel.Id && z.CassetteId != null).FirstOrDefault();//  uow.Repository<VRollsetOverview>().Query(z => z.RollSetId == viewModel.Id && z.CassetteId != null).GetSingle();
        if (rollSet != null)
        {
          entryDataContract.CassetteId = rollSet.CassetteId;
        }
      }

      entryDataContract.Action = RollSetCassetteAction.CancelPlan;

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollSetToCassetteAction(entryDataContract);

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
        V_RollsetOverviewNewest rsOverview = ctx.V_RollsetOverviewNewest.Where(z => z.RollSetId == id).FirstOrDefault();
        if (rsOverview != null)
        {
          //IList<VRollHistory> RollHistoryBottom = uow.Repository<VRollHistory>().Query(z => z.RollId == rsOverview.RollIdBottom).Get().ToList();
          IList<V_RollHistory> rollHistoryBottom = ctx.V_RollHistory.Where(z => z.RollId == rsOverview.RollIdBottom && z.RollSetHistoryId == rsOverview.RollSetHistoryId).ToList();

          //IList<VRollHistory> RollHistoryUpper = uow.Repository<VRollHistory>().Query(z => z.RollId == rsOverview.RollIdUpper).Get().ToList();
          IList<V_RollHistory> rollHistoryUpper = ctx.V_RollHistory.Where(z => z.RollId == rsOverview.RollIdUpper && z.RollSetHistoryId == rsOverview.RollSetHistoryId).ToList();
          //IList<VRollHistory> RollHistoryThird = uow.Repository<VRollHistory>().Query(z => z.RollId == rsOverview.RollIdThird).Get().ToList();
          returnValueVm = new VM_RollsetDisplay(rsOverview, rollHistoryUpper, rollHistoryBottom);
        }
        foreach (VM_GroovesRoll gr in returnValueVm.GrooveActualRollBottom)
        {
          gr.GrooveConfirmed = false;
        }
        foreach (VM_GroovesRoll gr in returnValueVm.GrooveActualRollUpper)
        {
          gr.GrooveConfirmed = false;
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
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
        //VCassettesOverview Cassette = uow.Repository<VCassettesOverview>().Query(z => z.CassetteId == id).GetSingle();
        V_CassettesOverview cassette = ctx.V_CassettesOverview.Where(z => z.CassetteId == id).FirstOrDefault();
        if (cassette != null)
        {
          returnValueVm = new VM_CassetteOverview(cassette);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public VM_RollSetOverview GetRollSet(ModelStateDictionary modelState, long id)
    {
      VM_RollSetOverview returnValueVm = null;

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
        V_RollsetOverviewNewest rollSet = ctx.V_RollsetOverviewNewest.Where(z => z.RollSetId == id).FirstOrDefault();
        if (rollSet != null)
        {
          returnValueVm = new VM_RollSetOverview(rollSet);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    #endregion
    public SelectList GetCassetteRSWithRollsList(long cassetteid)
    {
      using (PEContext ctx = new PEContext())
      {
        List<V_RollsetOverviewNewest> list = null;
        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
        //CassetteType CassetteType = uow.Repository<CassetteType>().Query(z => z.CassetteTypeId == cassetteType).GetSingle();
        RLSCassette cassette = ctx.RLSCassettes.Where(x => x.CassetteId == cassetteid).FirstOrDefault();
        RLSCassetteType cassType = ctx.RLSCassetteTypes.Where(z => z.CassetteTypeId == cassette.FKCassetteTypeId).FirstOrDefault();
        if ((cassType.Type == (short)RollSetType.TwoActiveRollsIM) || (cassType.Type == (short)RollSetType.TwoActiveRollsRM))
        {
          //list = uow.Repository<VRollsetOverviewNewest>().Query(p => /*p.Type == rollsetType NumberOfRolls &&*/
          //                                          p.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Ready)
          //                                          //.Include(k=> k.RollTypeUpper)
          //                                          .Get()
          //                                          .ToList();

          list = ctx.V_RollsetOverviewNewest.Where(x => x.RollSetStatus == (short)RollSetStatus.Ready).ToList();

          foreach (V_RollsetOverviewNewest rl in list)
          {
            resultDictionary.Add(Convert.ToInt64(rl.RollSetId), rl.RollSetName);
          }
        }
        
        //  return new SelectList(list, "RollSetId", "RollSetName");
        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
        return tmpSelectList;

      }
    }

    public static SelectList GetCassetteRSWith3RollsList(long cassetteType)
    {
      using (PEContext ctx = new PEContext())
      {
        List<V_RollsetOverviewNewest> list = null;
        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
        //CassetteType CassetteType = uow.Repository<CassetteType>().Query(z => z.CassetteTypeId == cassetteType).GetSingle();
        RLSCassetteType cassType = ctx.RLSCassetteTypes.Where(z => z.CassetteTypeId == cassetteType).FirstOrDefault();
        if ((cassType.Type == (short)RollSetType.TwoActiveRollsIM) || (cassType.Type == (short)RollSetType.TwoActiveRollsRM))
        {
          //list = uow.Repository<VRollsetOverviewNewest>().Query(p => /*p.Type == rollsetType NumberOfRolls &&*/
          //                                          p.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Ready)
          //                                          //.Include(k=> k.RollTypeUpper)
          //                                          .Get()
          //                                          .ToList();

          list = ctx.V_RollsetOverviewNewest.Where(x => x.RollSetStatus == (short)RollSetStatus.Ready).ToList();

          foreach (V_RollsetOverviewNewest rl in list)
          {
            resultDictionary.Add(Convert.ToInt64(rl.RollSetId), rl.RollSetName);
          }
        }

        //  return new SelectList(list, "RollSetId", "RollSetName");
        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
        return tmpSelectList;

      }
    }
  }
}

