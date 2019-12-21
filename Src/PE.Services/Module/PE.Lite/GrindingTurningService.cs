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
using PE.HMIWWW.ViewModel.Module.Lite.GrindingTurning;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.Core;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class GrindingTurningService : BaseService, IGrindingTurningService
  {
    #region interface IGrindingTurningService
    public DataSourceResult GetPlannedRollsetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        //var list = uow.Repository<VRollsetOverviewNewest>().Query(x => (
        //                                                                  x.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Ready
        //                                                              || (x.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Dismounted)
        //                                                              || (x.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Empty)
        //                                                               )
        //                                                              && (x.RollSetHistoryStatus == (short)PE.Core.Constants.RollSetHistoryStatus.Actual)
        //                                                              && x.RollIdUpper != null)
        //                                                              .OrderBy(o => o.OrderBy(g => g.RollSetName))
        //                                                              .Get().ToList();

        List<V_RollsetOverviewNewest> list = ctx.V_RollsetOverviewNewest.Where(x => (
                                                                          x.RollSetStatus == (short)RollSetStatus.Ready
                                                                      || (x.RollSetStatus == (short)RollSetStatus.Dismounted)
                                                                      || (x.RollSetStatus == (short)RollSetStatus.Empty)
                                                                       )
                                                                       && (x.RollSetHistoryStatus == (short)RollSetHistoryStatus.Actual)
                                                                      && x.RollIdUpper != null)
                                                                      .OrderBy(g => g.RollSetName)
                                                                      .ToList();


        result = list.ToDataSourceResult<V_RollsetOverviewNewest, VM_RollSetOverview>(request, modelState, data => new VM_RollSetOverview(data));
      }
      return result;
    }
    public DataSourceResult GetScheduledRollsetList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        //var list = uow.Repository<VRollsetOverviewNewest>().Query(x => ((x.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Turning) && (x.RollSetHistoryStatus == (short)PE.Core.Constants.RollSetHistoryStatus.Planned) && (x.RollIdBottom != null) && (x.RollIdUpper != null))).Get();

        List<V_RollsetOverviewNewest> list = ctx.V_RollsetOverviewNewest.Where(x => ((x.RollSetStatus == (short)RollSetStatus.Turning) && (x.RollSetHistoryStatus == (short)RollSetHistoryStatus.Planned) && (x.RollIdBottom != null) && (x.RollIdUpper != null))).ToList();
        
        result = list.ToDataSourceResult<V_RollsetOverviewNewest, VM_RollSetOverview>(request, modelState, data => new VM_RollSetOverview(data));
      }
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
        V_RollsetOverviewNewest rsOverview = ctx.V_RollsetOverviewNewest.Where(x => x.RollSetId == id).FirstOrDefault();
        if (rsOverview != null)
        {
          //IList<VRollHistory> RollHistoryBottom = uow.Repository<VRollHistory>().Query(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.RollIdBottom).OrderBy(o => o.OrderBy(g => g.GrooveNumber)).Get().ToList();
          IList<V_RollHistory> rollHistoryBottom = ctx.V_RollHistory.Where(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.RollIdBottom).OrderBy(g => g.GrooveNumber).ToList();
          //IList<VRollHistory> RollHistoryUpper = uow.Repository<VRollHistory>().Query(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.RollIdUpper).OrderBy(o => o.OrderBy(g => g.GrooveNumber)).Get().ToList();
          IList<V_RollHistory> rollHistoryUpper = ctx.V_RollHistory.Where(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.RollIdUpper).OrderBy(g => g.GrooveNumber).ToList();
          //IList<VRollHistory> RollHistoryThird = uow.Repository<VRollHistory>().Query(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.RollIdThird).OrderBy(o => o.OrderBy(g => g.GrooveNumber)).Get().ToList();
          //returnValueVm = new VM_RollsetDisplay(rsOverview, RollHistoryUpper, RollHistoryBottom, RollHistoryThird);
          returnValueVm = new VM_RollsetDisplay(rsOverview, rollHistoryUpper, rollHistoryBottom);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public VM_RollSetTurningHistory GetRollSetHistoryActual(ModelStateDictionary modelState, long id)
    {
      VM_RollSetTurningHistory returnValueVm = null;

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
        //VRollsetOverviewNewest rsOverview = uow.Repository<VRollsetOverviewNewest>().Query(x => (x.RollSetId == id)).GetSingle();
        V_RollsetOverviewNewest rsOverview = ctx.V_RollsetOverviewNewest.Where(x => x.RollSetId == id).FirstOrDefault();
        if (rsOverview != null)
        {
          //IList<VRollHistory> RollHistoryBottom = uow.Repository<VRollHistory>().Query(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.RollIdBottom).OrderBy(o => o.OrderBy(g => g.GrooveNumber)).Get().ToList();
          IList<V_RollHistory> rollHistoryBottom = ctx.V_RollHistory.Where(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.RollIdBottom).OrderBy(g => g.GrooveNumber).ToList();
          //IList<VRollHistory> RollHistoryUpper = uow.Repository<VRollHistory>().Query(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.RollIdUpper).OrderBy(o => o.OrderBy(g => g.GrooveNumber)).Get().ToList();
          IList<V_RollHistory> rollHistoryUpper = ctx.V_RollHistory.Where(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.RollIdUpper).OrderBy(g => g.GrooveNumber).ToList();

          //IList<VRollHistory> RollHistoryThird = uow.Repository<VRollHistory>().Query(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.RollIdThird).OrderBy(o => o.OrderBy(g => g.GrooveNumber)).Get().ToList();
          //returnValueVm = new VM_RollSetTurningHistory(rsOverview, RollHistoryUpper, RollHistoryBottom, RollHistoryThird);
          returnValueVm = new VM_RollSetTurningHistory(rsOverview, rollHistoryUpper, rollHistoryBottom);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_RollSetTurningHistory GetRollSetHistoryById(ModelStateDictionary modelState, long id)
    {
      VM_RollSetTurningHistory returnValueVm = null;

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
        //VRollsetOverview rsOverview = uow.Repository<VRollsetOverview>().Query(x => (x.RollSetHistoryId == id)).GetSingle();
        V_RollSetOverview rsOverview = ctx.V_RollSetOverview.Where(x => x.RollSetHistoryId == id).FirstOrDefault();
        if (rsOverview != null)
        {
          //IList<VRollHistory> RollHistoryBottom = uow.Repository<VRollHistory>().Query(z => z.RollSetHistoryId == id && z.RollId == rsOverview.RollIdBottom).OrderBy(o => o.OrderBy(g => g.GrooveNumber)).Get().ToList();
          IList<V_RollHistory> rollHistoryBottom = ctx.V_RollHistory.Where(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.RollIdBottom).OrderBy(g => g.GrooveNumber).ToList();

          //IList<VRollHistory> RollHistoryUpper = uow.Repository<VRollHistory>().Query(z => z.RollSetHistoryId == id && z.RollId == rsOverview.RollIdUpper).OrderBy(o => o.OrderBy(g => g.GrooveNumber)).Get().ToList();
          IList<V_RollHistory> rollHistoryUpper = ctx.V_RollHistory.Where(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.RollIdUpper).OrderBy(g => g.GrooveNumber).ToList();

          //IList<VRollHistory> RollHistoryThird = uow.Repository<VRollHistory>().Query(z => z.RollSetHistoryId == id && z.RollId == rsOverview.RollIdThird).OrderBy(o => o.OrderBy(g => g.GrooveNumber)).Get().ToList();
          //returnValueVm = new VM_RollSetTurningHistory(rsOverview, RollHistoryUpper, RollHistoryBottom, RollHistoryThird);
          returnValueVm = new VM_RollSetTurningHistory(rsOverview, rollHistoryUpper, rollHistoryBottom);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }


    public VM_RollsetDisplay AddGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVM)
    {
      UnitConverterHelper.ConvertToSi<VM_RollsetDisplay>(ref actualVM);
      VM_RollsetDisplay returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (actualVM.RollSetId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      if (actualVM.GrooveActualRollBottom == null)
      {
        actualVM.GrooveActualRollBottom = new List<VM_GroovesRoll>();
      }
      if (actualVM.GrooveActualRollUpper == null)
      {
        actualVM.GrooveActualRollUpper = new List<VM_GroovesRoll>();
      }
      //if (actualVM.GrooveActualRollThird == null)
      //{
      //    actualVM.GrooveActualRollThird = new List<VM_GroovesRollThird>();
      //}
      //GrooveTemplate defGroove = LR.Helpers.Helpers.GetDefaultGrooveTemplate();
      using (PEContext ctx = new PEContext())
      {
        RLSGrooveTemplate defGroove = ctx.RLSGrooveTemplates.Where(x => x.IsDefault == true).FirstOrDefault();

        actualVM.GrooveActualRollBottom.Add(new VM_GroovesRoll { AccWeight = 0, AccBilletCnt = 0, GrooveTemplateId = defGroove.GrooveTemplateId, GrooveTemplateName = defGroove.GrooveTemplateName, GrooveConfirmed = true, AccBilletCntLimit = 0, GrooveNumber = 1, GrooveShortName = defGroove.GrooveTemplateCode, GrooveStatus = 1 });
        actualVM.GrooveActualRollUpper.Add(new VM_GroovesRoll { AccWeight = 0, AccBilletCnt = 0, GrooveTemplateId = defGroove.GrooveTemplateId, GrooveTemplateName = defGroove.GrooveTemplateName, GrooveConfirmed = true, AccBilletCntLimit = 0, GrooveNumber = 1, GrooveShortName = defGroove.GrooveTemplateCode, GrooveStatus = 1 });
        //  actualVM.GrooveActualRollThird.Add(new VM_GroovesRollThird { AccWeight = 0, AccBilletCnt = 0, GrooveTemplateId = defGroove.GrooveTemplateId, GrooveTemplateName = defGroove.GrooveTemplateName, GrooveConfirmed = true, AccBilletCntLimit = 0, AccWeightLimit = 0, GrooveNumber = 0, GrooveShortName = defGroove.NameShort, GrooveStatus = 1 }); 
      }
      return actualVM;
    }


    public VM_RollsetDisplay RemoveGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVM)
    {
      UnitConverterHelper.ConvertToSi<VM_RollsetDisplay>(ref actualVM);
      VM_RollsetDisplay returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (actualVM.RollSetId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      if (actualVM.GrooveActualRollBottom != null && actualVM.GrooveActualRollBottom.Exists(p => p.GrooveConfirmed == true))
        actualVM.GrooveActualRollBottom.Remove(actualVM.GrooveActualRollBottom.FirstOrDefault(p => p.GrooveConfirmed == true));
      if (actualVM.GrooveActualRollUpper != null && actualVM.GrooveActualRollUpper.Exists(p => p.GrooveConfirmed == true))
        actualVM.GrooveActualRollUpper.Remove(actualVM.GrooveActualRollUpper.FirstOrDefault(p => p.GrooveConfirmed == true));

      return actualVM;
    }


    public async Task<VM_Base> UpdateGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVM)
    {
      UnitConverterHelper.ConvertToSi<VM_RollsetDisplay>(ref actualVM);
      VM_Base returnVM = new VM_Base();
      


      //VALIDATE ENTRY PARAMETERS
      if (actualVM.RollSetId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (actualVM.RollSetType < 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnVM;
      }
      //END OF VALIDATION

      

      DCRollSetGrooveSetup entryDataContract = new DCRollSetGrooveSetup();

      entryDataContract.Id = actualVM.RollSetId;
      entryDataContract.Action = GrindingTurningAction.Plan;
      entryDataContract.RollSetType = actualVM.RollSetType;
      List<SingleRollDataInfo> rollDataInfoList = new List<SingleRollDataInfo>();
      //add Rolls to list
      if (actualVM.ActualUpperRollId != null)
      {
        SingleRollDataInfo s = new SingleRollDataInfo();
        s.RollId = actualVM.ActualUpperRollId ?? 0;
        s.Diameter = actualVM.NewUpperDiameter ?? 0.0;
        rollDataInfoList.Add(s);
      }
      if (actualVM.ActualBottomRollId != null)
      {
        SingleRollDataInfo s = new SingleRollDataInfo();
        s.RollId = actualVM.ActualBottomRollId ?? 0;
        s.Diameter = actualVM.NewBottomDiameter ?? 0.0;
        rollDataInfoList.Add(s);
      }
      //if (actualVM.ActualThirdRollId != null)
      //{
      //  SingleRollDataInfo s = new SingleRollDataInfo();
      //  s.RollId = actualVM.ActualThirdRollId ?? 0;
      //  s.Diameter = actualVM.NewThirdDiameter ?? 0.0;
      //  rollDataInfoList.Add(s);
      //}
      entryDataContract.RollDataInfoList = rollDataInfoList;
      List<SingleGrooveSetup> GrooveSetupList = new List<SingleGrooveSetup>();
      short grooveIdx = 1;
      if (actualVM.GrooveActualRollUpper != null)
      {
        foreach (VM_GroovesRoll rollgroove in actualVM.GrooveActualRollUpper)
        {
          if (rollgroove.GrooveTemplateId != 0)
          {
            GrooveSetupList.Add(new SingleGrooveSetup { GrooveTemplate = (long)rollgroove.GrooveTemplateId, GrooveIdx = grooveIdx, GrooveStatus = (short)RollGrooveStatus.PlannedAndTurning });
          }
          else
          {
            GrooveSetupList.Add(new SingleGrooveSetup { GrooveTemplate = (long)rollgroove.GrooveTemplateId, GrooveIdx = grooveIdx, GrooveStatus = (short)RollGrooveStatus.PlannedAndNotAvailable });
          }
          grooveIdx++;
        }
      }
      entryDataContract.GrooveSetupList = GrooveSetupList;
      //save list
      //Task<SendOfficeResult<bool>> taskOnRemoteModule = null;


      //taskOnRemoteModule = HmiSendOffice.UpdateGroovesToRollSet(entryDataContract);
      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateGroovesToRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return returnVM;
    }


    public async Task<VM_Base> ConfirmRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base returnVM = new VM_Base();
      
      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnVM;
      }
      //END OF VALIDATION

      
      DCRollSetData entryDataContract = new DCRollSetData();

      entryDataContract.Id = viewModel.Id;
      entryDataContract.RollSetStatus = RollSetStatus.Ready;

      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.ConfirmRollSetStatus(entryDataContract);

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.ConfirmRollSetStatusAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return returnVM;
    }


    
    public async Task<VM_Base> CancelRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base returnVM = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnVM;
      }
      //END OF VALIDATION


      //DCRollSetData entryDataContract = new DCRollSetData();

      //entryDataContract.Id = viewModel.Id;
      //entryDataContract.RollSetStatus = RollSetStatus.Ready;

      ////Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.CancelRollSetStatus(entryDataContract);

      //SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.CancelRollSetStatusAsync(entryDataContract);

      DCRollSetGrooveSetup entryDataContract = new DCRollSetGrooveSetup();

      entryDataContract.Id = viewModel.Id;
      entryDataContract.Action = GrindingTurningAction.Cancel;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateGroovesToRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return returnVM;
    }

    public async Task<VM_Base> ConfirmUpdateGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVM)
    {
      VM_Base returnVM = new VM_Base();

      UnitConverterHelper.ConvertToSi<VM_RollsetDisplay>(ref actualVM);

      //VALIDATE ENTRY PARAMETERS
      if (actualVM.RollSetId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnVM;
      }
      //END OF VALIDATION


      DCRollSetGrooveSetup entryDataContract = new DCRollSetGrooveSetup();

      entryDataContract.Id = actualVM.RollSetId;
      List<SingleRollDataInfo> rollDataInfoList = new List<SingleRollDataInfo>();
      //add Rolls to list
      if (actualVM.ActualUpperRollId != null)
      {
        SingleRollDataInfo s = new SingleRollDataInfo();
        s.RollId = actualVM.ActualUpperRollId ?? 0;
        s.Diameter = actualVM.NewUpperDiameter ?? 0.0;
        rollDataInfoList.Add(s);
      }
      if (actualVM.ActualBottomRollId != null)
      {
        SingleRollDataInfo s = new SingleRollDataInfo();
        s.RollId = actualVM.ActualBottomRollId ?? 0;
        s.Diameter = actualVM.NewBottomDiameter ?? 0.0;
        rollDataInfoList.Add(s);
      }
      //if (actualVM.ActualThirdRollId != null)
      //{
      //  SingleRollDataInfo s = new SingleRollDataInfo();
      //  s.RollId = actualVM.ActualThirdRollId ?? 0;
      //  s.Diameter = actualVM.NewThirdDiameter ?? 0.0;
      //  rollDataInfoList.Add(s);
      //}
      entryDataContract.RollDataInfoList = rollDataInfoList;
      entryDataContract.Action = GrindingTurningAction.Confirm;
      List<SingleGrooveSetup> GrooveSetupList = new List<SingleGrooveSetup>();
      short grooveIdx = 1;
      if (actualVM.GrooveActualRollUpper != null)
      {
        for (int i = 0; i < actualVM.GrooveActualRollUpper.Count; i++)
        {
          if (actualVM.GrooveActualRollUpper[i].GrooveTemplateId != 0)
          {
            GrooveSetupList.Add(new SingleGrooveSetup { GrooveTemplate = (long)actualVM.GrooveActualRollUpper[i].GrooveTemplateId, GrooveIdx = grooveIdx, GrooveStatus = (short)RollGrooveStatus.PlannedAndTurning, GrooveConfirmed = (actualVM.GrooveActualRollUpper[0].GrooveConfirmed ? (short)YesNo.Yes : (short)YesNo.No) });
          }
          else
          {
            GrooveSetupList.Add(new SingleGrooveSetup { GrooveTemplate = (long)actualVM.GrooveActualRollUpper[i].GrooveTemplateId, GrooveIdx = grooveIdx, GrooveStatus = (short)RollGrooveStatus.PlannedAndNotAvailable, GrooveConfirmed = (actualVM.GrooveActualRollUpper[0].GrooveConfirmed ? (short)YesNo.Yes : (short)YesNo.No) });
          }
          grooveIdx++;
        }
      }
      entryDataContract.GrooveSetupList = GrooveSetupList;
      //save list

      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.UpdateGroovesToRollSet(entryDataContract);

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateGroovesToRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return returnVM;
    }
    #endregion
  }
}
