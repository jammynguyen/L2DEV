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
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.RollChange;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.Core;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class RollChangeService : BaseService, IRollChangeService
  {
    #region ViewModel Methods

    public VM_ConfirmationForRmAndIm BuildVMConfirmationForRmAndIm(short? operationType, long? rollsetId, long? mountedRollsetId, short? position, short? standNo)
    {
      VM_ConfirmationForRmAndIm returnModel = new VM_ConfirmationForRmAndIm()
      {
        OperationType = operationType,
        RollsetId = rollsetId,
        MountedRollsetId = mountedRollsetId,
        Position = position,
        StandNo = standNo
      };
      if (rollsetId != null)
      {
        V_RollsetOverviewNewest rollset = GetRollSetDetails((long)rollsetId);
        returnModel.RollsetName = rollset.RollSetName;
      }
      if (mountedRollsetId != null)
      {
        V_RollsetOverviewNewest mountedRollset = GetRollSetDetails((long)mountedRollsetId);
        returnModel.MountedRollsetName = mountedRollset.RollSetName;
      }
      return returnModel;
    }

    #endregion

    #region Database Operations

    public DataSourceResult GetStandGridData(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, long firstStand, long lastStand)
    {
      List<V_RSCassettesInStands> list = GetStandList(firstStand, lastStand);
      return list.ToDataSourceResult<V_RSCassettesInStands, VM_StandGrid>(request, modelState, data => new VM_StandGrid(data));
    }

    public DataSourceResult GetRollsetGridData(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, long standNo)
    {
      return GetRollsets(standNo).ToDataSourceResult<V_RollsetOverviewNewest, VM_RollsetGrid>(request, modelState, data => new VM_RollsetGrid(data));
    }

    public DataSourceResult GetRollGroovesGridData(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, long rollSetId, long standNo)
    {
      return GetGrooves(rollSetId, standNo).ToDataSourceResult<V_RollHistoryPerGroove, VM_RollGrooveGrid>(request, modelState, data => new VM_RollGrooveGrid(data));
    }

    public List<V_RSCassettesInStands> GetCassettesInfoRmIm()
    {
      using (PEContext ctx = new PEContext())
      {
        //RM + IM are stands between 1 and 10
        //var list = uow.Repository<VRsCassettesInStand>().Query(o => o.StandNo >= 1 && o.StandNo <= 10).OrderBy(k => k.OrderBy(g => g.StandNo)).Get().ToList();
        List<V_RSCassettesInStands> list = ctx.V_RSCassettesInStands.Where(o => o.StandNo >= 1 && o.StandNo <= 10).OrderBy(g => g.StandNo).ToList();
        if (list.Count() > 0)
        {
          return list;
        }
        return null;
      }
    }

    public async Task<VM_Base> SwapRollSet(ModelStateDictionary modelState, VM_ConfirmationForRmAndIm viewModel)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi<VM_ConfirmationForRmAndIm>(ref viewModel);


      DCRollChangeOperationData dc = new DCRollChangeOperationData();
      
      dc.Action = (short)RollChangeAction.SwapRollSetOnly;
      dc.MountedRollSet = viewModel.RollsetId;
      dc.DismountedRollSet = viewModel.MountedRollsetId;
      dc.StandNo = viewModel.StandNo;
      dc.Position = viewModel.Position;

      using (PEContext ctx = new PEContext())
      {
        //get cassette on this standNo
        // VCassettesOverview rec = uow.Repository<VCassettesOverview>().Query(q => q.StandNo == viewModel.StandNo && q.Status == (short)PE.Core.Constants.CassetteStatus.MountedInStand).Get().FirstOrDefault();
        V_CassettesOverview mountedCassette = ctx.V_CassettesOverview.Where(q => q.StandNo == viewModel.StandNo && q.Status == (short)CassetteStatus.MountedInStand).FirstOrDefault();
        if (mountedCassette == null)
        {
          return result;
        }
        dc.MountedCassette = mountedCassette.CassetteId;
        //get dismounted rollset Id
        //VRollsetInCassette rs = uow.Repository<VRollsetInCassette>().Query(q => q.FkCassetteId == rec.CassetteId && q.PositionInCassette == viewModel.Position).Get().FirstOrDefault();
        V_RollsetInCassettes rollSet = ctx.V_RollsetInCassettes.Where(q => q.FKCassetteId == mountedCassette.CassetteId && q.PositionInCassette == viewModel.Position).FirstOrDefault();
        if (rollSet == null)
        {
          return result;
        }
        dc.DismountedRollSet = rollSet.RollSetId;
      }

      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.RollChangeAction(dc);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> MountRollset(ModelStateDictionary modelState, VM_ConfirmationForRmAndIm viewModel)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi<VM_ConfirmationForRmAndIm>(ref viewModel);

      DCRollChangeOperationData dc = new DCRollChangeOperationData();

      dc.Action = (short)RollChangeAction.MountRollSetOnly;
      dc.MountedRollSet = viewModel.RollsetId;
      dc.StandNo = viewModel.StandNo;
      dc.Position = viewModel.Position;
      //get cassette on this standNo
      using (PEContext ctx = new PEContext())
      {
        //VCassettesOverview rec = uow.Repository<VCassettesOverview>().Query(q => q.StandNo == viewModel.StandNo).Get().FirstOrDefault();
        V_CassettesOverview mountedCassette = ctx.V_CassettesOverview.Where(q => q.StandNo == viewModel.StandNo).FirstOrDefault();
        if (mountedCassette == null)
        {
          return result;
        }
        dc.MountedCassette = mountedCassette.CassetteId;
      }
      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.RollChangeAction(dc);

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DismountRollset(ModelStateDictionary modelState, VM_ConfirmationForRmAndIm viewModel)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi<VM_ConfirmationForRmAndIm>(ref viewModel);


      DCRollChangeOperationData dc = new DCRollChangeOperationData();

      dc.Action = (short)RollChangeAction.DismountRollSetOnly;
      dc.DismountedRollSet = viewModel.MountedRollsetId;
      dc.StandNo = viewModel.StandNo;
      //get cassette Id from which rollset is to be dismounted
      using (PEContext ctx = new PEContext())
      {
        //VRollsetInCassette rec = uow.Repository<VRollsetInCassette>().Query(q => q.RollSetId == viewModel.RollsetId).Get().FirstOrDefault();
        V_RollsetInCassettes rollSet = ctx.V_RollsetInCassettes.Where(q => q.RollSetId == viewModel.RollsetId).FirstOrDefault();
        if (rollSet == null || rollSet.FKCassetteId == null)
        {
          return result;
        }
        dc.MountedCassette = rollSet.FKCassetteId;
      }
      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.RollChangeAction(dc);

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public V_RollsetOverviewNewest GetRollSetDetails(long rollSetId)
    {
      using (PEContext ctx = new PEContext())
      {
        //return uow.Repository<VRollsetOverviewNewest>().Query(o => o.RollSetId == rollSetId).Get().FirstOrDefault();
        return ctx.V_RollsetOverviewNewest.Where(o => o.RollSetId == rollSetId).FirstOrDefault();
      }
    }

    public short GetGrooveNumber(long rollId)
    {
      using (PEContext ctx = new PEContext())
      {
        //var groovesHistory = uow.Repository<VRollHistoryPerGroove>().Query(o => o.RollId == rollId &&
        //                                                                            o.RollSetHistoryStatus == (short)Constants.RollSetHistoryStatus.Actual).Get().ToList();

        List<V_RollHistoryPerGroove> groovesHistory = ctx.V_RollHistoryPerGroove.Where(o => o.RollId == rollId && o.RollSetHistoryStatus == (short)RollSetHistoryStatus.Actual).ToList();

        return (short)groovesHistory.Count();
      }
    }

    #endregion

    #region Helper Methods

    public List<V_RollsetOverviewNewest> GetAvailableRollsets(short standNo)
    {
      return GetRollsets(standNo);
    }

    public long? GetRollId(V_RollsetOverviewNewest rollSet)
    {
      if (rollSet.RollIdUpper != null)
      {
        return (long)rollSet.RollIdUpper;
      }
      else if (rollSet.RollIdBottom != null)
      {
        return (long)rollSet.RollIdBottom;
      }
      return null;
    }

    #endregion

    #region Private Methods

    private List<V_RollHistoryPerGroove> GetGrooves(long rollSetId, long standNo)
    {
      using (PEContext ctx = new PEContext())
      {
        V_RollsetOverviewNewest rollSet = new V_RollsetOverviewNewest();
        if (rollSetId != 0)
        {
          rollSet = GetRollSetDetails(rollSetId);
        }
        else
        {
          List<V_RollsetOverviewNewest> rollsets = GetRollsets(standNo);

          if (rollsets.Count() > 0)
          {
            rollSet = rollsets[0];
          }
        }

        if (rollSet != null && rollSet.RollSetId != 0)
        {
          long? rollId = GetRollId(rollSet);
          if (rollId != null)
            //return uow.Repository<VRollHistoryPerGroove>().Query(o => o.RollId == rollId && o.RollSetHistoryStatus == (short)Constants.RollSetHistoryStatus.Actual).Get().ToList();
            return ctx.V_RollHistoryPerGroove.Where(o => o.RollId == rollId && o.RollSetHistoryStatus == (short)RollSetHistoryStatus.Actual).ToList();
        }
        return new List<V_RollHistoryPerGroove>();
      }
    }

    private List<V_RollsetOverviewNewest> GetRollsets(long standNo)
    {
      using (PEContext ctx = new PEContext())
      {
        //var stand = uow.Repository<Stand>().Query(o => o.StandNo == standNo).Get().FirstOrDefault();
        RLSStand stand = ctx.RLSStands.Where(x => x.StandNo == standNo).FirstOrDefault();
        if (stand != null)
        {
          short rollsetType = 0;
          //PROJECT SPECIFIC!!!! bad design probably
          //find matching rollSetType depending on standNo
          if (standNo >= 1 && standNo <= 6)
            rollsetType = (short)RollSetType.TwoActiveRollsRM;
          else if (standNo >= 7 && standNo <= 10)
            rollsetType = (short)RollSetType.TwoActiveRollsIM;
          else
            rollsetType = (short)RollSetType.Undefined;

          List<V_RollsetOverviewNewest> orderedList = new List<V_RollsetOverviewNewest>();
          //V_RollsetOverviewNewest mountedRollset = uow.Repository<VRollsetOverviewNewest>().Query(o => o.StandNo == standNo && o.RollSetStatus == (short)Constants.RollSetStatus.Mounted).Get().FirstOrDefault();
          V_RollsetOverviewNewest mountedRollset = ctx.V_RollsetOverviewNewest.Where(o => o.StandNo == standNo && o.RollSetStatus == (short)RollSetStatus.Mounted).FirstOrDefault();

          if (mountedRollset != null)
            orderedList.Add(mountedRollset);

          //IList<VRollsetOverviewNewest> rollsets = uow.Repository<VRollsetOverviewNewest>()
          //  .Query(o => o.RollSetHistoryStatus == (short)Constants.RollSetHistoryStatus.Actual && o.RollSetType == rollsetType &&
          //                (o.RollSetStatus == (short)Constants.RollSetStatus.Ready || o.RollSetStatus == (short)Constants.RollSetStatus.Dismounted))
          //  .OrderBy(g => g.OrderBy(h => h.RollSetName))
          //  .Get()
          //  .ToList();

          IList<V_RollsetOverviewNewest> rollsets = ctx.V_RollsetOverviewNewest.Where(o => o.RollSetHistoryStatus == (short)RollSetHistoryStatus.Actual && o.RollSetType == rollsetType &&
                          (o.RollSetStatus == (short)RollSetStatus.Ready || o.RollSetStatus == (short)RollSetStatus.Dismounted))
                          .OrderBy(h => h.RollSetName)
                          .ToList();

          if (rollsets.Count > 0)
          {
            foreach (V_RollsetOverviewNewest rollset in rollsets)
            {
              orderedList.Add(rollset);
            }
          }

          return orderedList;
        }
        return new List<V_RollsetOverviewNewest>();
      }
    }

    private List<V_RSCassettesInStands> GetStandList(long firstStand, long lastStand)
    {
      using (PEContext ctx = new PEContext())
      {
        //return uow.Repository<VRsCassettesInStand>().Query(o => o.StandNo >= firstStand && o.StandNo <= lastStand).OrderBy(s => s.OrderBy(g => g.StandNo)).Get().ToList();
        return ctx.V_RSCassettesInStands.Where(x => x.StandNo >= firstStand && x.StandNo <= lastStand).OrderBy(o => o.StandNo).ToList();
      }
    }
    #endregion
  }
}
