using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration;
using PE.HMIWWW.ViewModel.Module.Lite.Cassette;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ActualStandsConfigurationService : BaseService, IActualStandsConfigurationService
  {
    #region interface IActualStandsConfigurationService
    public DataSourceResult GetStandConfigurationList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        //var list = uow.Repository<VActualStandConfiguration>().Query(z => z.StandId != 0).OrderBy(z => z.OrderBy(x => x.Position).ThenBy(x => x.StandNo)).Get();
        List<V_ActualStandConfiguration> list = ctx.V_ActualStandConfiguration.Where(x => x.StandId != 0).OrderBy(o => o.Position).ThenBy(o => o.StandNo).ToList();
        result = list.ToDataSourceResult<V_ActualStandConfiguration, VM_StandConfiguration>(request, modelState, data => new VM_StandConfiguration(data));
      }
      return result;
    }
    public DataSourceResult GetPassChangeActualList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
    {
      DataSourceResult result = new DataSourceResult();

      using (PEContext ctx = new PEContext())
      {
        //IList<VPassChangeDataActual> list = uow.Repository<VPassChangeDataActual>().Query(p => p.GrooveStatus == (short)PE.Core.Constants.RollGrooveStatus.Active).Get().ToList();
        IList<V_PassChangeDataActual> list = ctx.V_PassChangeDataActual.Where(p => p.GrooveStatus == (short)RollGrooveStatus.Active).ToList();

        if (request.Sorts.Count == 0) //default sorting
        {
          List<VM_PassChangeDataActual> allVM = new List<VM_PassChangeDataActual>();
          foreach (V_PassChangeDataActual el in list)
          {
            VM_PassChangeDataActual vm = new VM_PassChangeDataActual(el);
            allVM.Add(vm);
          }
          //sort by accWeightRatio
          List<VM_PassChangeDataActual> sortedVMlist = allVM.OrderByDescending(o => o.AccWeightRatio).ToList();

          result.Data = sortedVMlist;
          result.Total = sortedVMlist.Count;
        }
        else
        {
          result = list.ToDataSourceResult<V_PassChangeDataActual, VM_PassChangeDataActual>(request, modelState, data => new VM_PassChangeDataActual(data));
        }

        //not used in PE.Lite
        //  long StdProdFinishTime = PE.Common.Constants.StdProductionTimeAfterMill;
        //  long StdProdTime = PE.Common.Constants.StdRollingTimeInMill;
        //  long StdWeght = PE.Common.Constants.StdBilletWeight;

        //  List<VProductionSchedule> schedule = uow.Repository<VProductionSchedule>().Query().Get().OrderBy(x => x.SequenceNumber).ToList();
        //  //get opened delay
        //  Delay actualDelay = uow.Repository<Delay>().Query().OrderBy(o => o.OrderByDescending(p => p.DelayStart)).GetSingle();

        //  if (actualDelay != null)
        //  {
        //    if (actualDelay.DelayEnd == null)
        //    {
        //      foreach (VM_PassChangeDataActual item in result.Data)
        //      {
        //        item.CountEstimatedPassChangeTime(schedule, StdProdFinishTime, StdProdTime, StdWeght, actualDelay.DelayStart, actualDelay.FkDelayCatalogueId);
        //      }
        //    }
        //    else
        //    {
        //      foreach (VM_PassChangeDataActual item in result.Data)
        //      {
        //        item.CountEstimatedPassChangeTime(schedule, StdProdFinishTime, StdProdTime, StdWeght, null, null);
        //      }
        //    }
        //  }
        //  else
        //  {
        //    foreach (VM_PassChangeDataActual item in result.Data)
        //    {
        //      item.CountEstimatedPassChangeTime(schedule, StdProdFinishTime, StdProdTime, StdWeght, null, null);
        //    }
        //  }
        //}
      }
      return result;
    }
    public DataSourceResult GetCassetteRollSetsList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, long? cassetteId, short? standStatus)
    {

      DataSourceResult result = null;
      if (cassetteId == null)
      {
        cassetteId = 0;
      }
      using (PEContext ctx = new PEContext())
      {
        //Cassette Cassette = uow.Repository<Cassette>().Query(z => z.CassetteId == cassetteId).GetSingle();
        RLSCassette cassette = ctx.RLSCassettes.Where(z => z.CassetteId == cassetteId).FirstOrDefault();
        //var list = uow.Repository<VRollsetOverviewNewest>().Query(p => p.CassetteId == cassetteId && p.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Mounted).Get();
        List<V_RollsetOverviewNewest> list = ctx.V_RollsetOverviewNewest.Where(p => p.CassetteId == cassetteId && p.RollSetStatus == (short)RollSetStatus.Mounted).ToList();
        List<short?> listOfPositions = list.Select(x => x.PositionInCassette).ToList(); 
        if (list == null)
        {
          list = new List<V_RollsetOverviewNewest>();
        }
        if (cassette != null)
        {
          for (int i = 1; i <= cassette.NumberOfPositions; i++)
          {
            if (list.Count < cassette.NumberOfPositions && !listOfPositions.Contains((short?)i))
            {
              V_RollsetOverviewNewest blankRecord = new V_RollsetOverviewNewest();
              blankRecord.PositionInCassette = (short?)i;
              blankRecord.RollSetName = "";
              list.Add(blankRecord);
            }
          }
        }


        result = list.ToDataSourceResult<V_RollsetOverviewNewest, VM_RollSetOverviewRollChange>(request, modelState, data => new VM_RollSetOverviewRollChange(data, standStatus));

        //if (!list.Any())
        //{
        //    List<VRollsetOverviewNewest> EmptyRollSets = new List<VRollsetOverviewNewest>();

        //    for (int i = 0; i < (Cassette != null ? Cassette.NumberOfPositions : 1); i++)
        //    {
        //        VRollsetOverviewNewest RollsetOverview = new VRollsetOverviewNewest();
        //        RollsetOverview.CassetteId = 0;
        //        RollsetOverview.RollSetName = "";
        //        RollsetOverview.RollSetStatus = 0;
        //        RollsetOverview.DiameterBottom = 0;
        //        RollsetOverview.DiameterUpper = 0;
        //        RollsetOverview.DiameterThird = 0;
        //        RollsetOverview.PositionInCassette = (short)(i + 1);
        //        EmptyRollSets.Add(RollsetOverview);


        //    }
        //    result = EmptyRollSets.ToDataSourceResult(request);
        //}

      }

      return result;
    }
    public DataSourceResult GetRollSetInCassetteList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, long cassetteId)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        //var list = uow.Repository<VRollsetOverviewNewest>().Query(p => p.CassetteId == cassetteId).Get();
        List<V_RollsetOverviewNewest> list = ctx.V_RollsetOverviewNewest.Where(p => p.CassetteId == cassetteId).ToList();
        result = list.ToDataSourceResult<V_RollsetOverviewNewest, VM_RollSetOverview>(request, modelState, data => new VM_RollSetOverview(data));
      }
      return result;
    }
    public DataSourceResult GetRollSetGroovesList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request, long rollsetHistoryId, short? standStatus)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        IEnumerable<V_RollHistory> returnList = new List<V_RollHistory>();
        //var list = uow.Repository<VRollHistory>().Query(p => p.RollSetHistoryId == rollsetHistoryId).Get().ToList();
        List<V_RollHistory> list = ctx.V_RollHistory.Where(p => p.RollSetHistoryId == rollsetHistoryId).ToList();
        if (list.Count() > 0)
        {
          string rollName = list[0].RollName;
          returnList = list.Where(o => o.RollName == rollName).OrderBy(o => o.GrooveNumber);
        }
        result = returnList.ToDataSourceResult<V_RollHistory, VM_PassChangeGroovesRoll>(request, modelState, data => new VM_PassChangeGroovesRoll(data));
      }
      return result;
    }

    public static SelectList GetCassetteToStandList(long standId)
    {
      using (PEContext ctx = new PEContext())
      {

        RLSStand stand = ctx.RLSStands.Where(z => z.StandId == standId).FirstOrDefault(); // uow.Repository<Stand>().Query(z => z.StandId == standId).GetSingle();
                                                                                          //var list = uow.Repository<VCassettesOverview>().Query(p => p.Type.Value/*NumberOfRolls*/ == (short)Stand.NumberOfRolls
                                                                                          //                                                        && p.IsInterCas == false
                                                                                          //                                                        && p.Status == (short)PE.Core.Constants.CassetteStatus.RollSetInside)
                                                                                          //                                                               .Get()
                                                                                          //                                                               .ToList();

        //List<V_CassettesOverview> list = ctx.V_CassettesOverview.Where(p => p.Type.Value/*NumberOfRolls*/ == (short)stand.NumberOfRolls
        //                                                        && p.IsInterCassette == false
        //                                                        && p.Status == (short)CassetteStatus.RollSetInside)
        //                                                        .ToList();


        List<V_CassettesOverview> list = ctx.V_CassettesOverview.Where(p => p.Status == (short)CassetteStatus.RollSetInside)
                                                        .ToList();

        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
        foreach (V_CassettesOverview cs in list)
        {
          resultDictionary.Add(cs.CassetteId, cs.CassetteName);
        }
        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
        return tmpSelectList;


      }
    }


    public static SelectList GetEmptyCassetteToStandList(long standId)
    {
      using (PEContext ctx = new PEContext())
      {

        RLSStand stand = ctx.RLSStands.Where(z => z.StandId == standId).FirstOrDefault(); // uow.Repository<Stand>().Query(z => z.StandId == standId).GetSingle();
                                                                                          //var list = uow.Repository<VCassettesOverview>().Query(p => p.Type.Value/*NumberOfRolls*/ == (short)Stand.NumberOfRolls
                                                                                          //                                                        && p.IsInterCas == false
                                                                                          //                                                        && p.Status == (short)PE.Core.Constants.CassetteStatus.RollSetInside)
                                                                                          //                                                               .Get()
                                                                                          //                                                               .ToList();

        //List<V_CassettesOverview> list = ctx.V_CassettesOverview.Where(p => p.Type.Value/*NumberOfRolls*/ == (short)stand.NumberOfRolls
        //                                                        && p.IsInterCassette == false
        //                                                        && p.Status == (short)CassetteStatus.RollSetInside)
        //                                                        .ToList();


        List<V_CassettesOverview> list = ctx.V_CassettesOverview.Where(p => p.Status == (short)CassetteStatus.Empty)
                                                        .ToList();

        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
        foreach (V_CassettesOverview cs in list)
        {
          resultDictionary.Add(cs.CassetteId, cs.CassetteName);
        }
        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
        return tmpSelectList;


      }
    }

    //// List of  rollset
    //public static SelectList GetRollSetToCassetteList(long cassId)
    //{
    //    using (PEUnitOfWork uow = new PEUnitOfWork())
    //    {
    //        Cassette Cass = uow.Repository<Cassette>().Query(z => z.CassetteId == cassId).GetSingle();
    //        var list = uow.Repository<VRollsetOverviewNewest>().Query(p => p.IsThirdRoll == Cass.CassetteType.NumberOfRolls
    //                                                                    && (p.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Ready) 
    //                                                                    || (p.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.ReadyForMounting))
    //                                                                    .Get()
    //                                                                    .ToList();

    //        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();

    //        foreach (VRollsetOverviewNewest  rs in list)
    //        {
    //            resultDictionary.Add(rs.RollSetId, rs.RollSetName);
    //        }
    //        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
    //        return tmpSelectList;
    //    }
    //}


    public VM_StandConfiguration GetStandConfiguration(ModelStateDictionary modelState, long id)
    {
      VM_StandConfiguration returnValueVm = null;

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
        //VActualStandConfiguration actualStandConfiguration = uow.Repository<VActualStandConfiguration>().Query(z => z.StandId == id).GetSingle();
        V_ActualStandConfiguration actualStandConfig = ctx.V_ActualStandConfiguration.Where(z => z.StandId == id).FirstOrDefault();
        if (actualStandConfig != null)
        {
          returnValueVm = new VM_StandConfiguration(actualStandConfig);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public VM_RollChangeOperation GetRollChangeOperation(ModelStateDictionary modelState, long id, short? positionId)
    {
      VM_RollChangeOperation returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      //if (!modelState.IsValid)
      //{
      //	return returnValueVm;
      //}
      //END OF VALIDATION


      //DB OPERATION
      using (PEContext ctx = new PEContext())
      {
        long rollsetToDismountId = 0;
        V_RollsetOverviewNewest rollsetToDismount = new V_RollsetOverviewNewest();
        //Cassette Cassette = uow.Repository<Cassette>().Query(z => z.CassetteId == id).Include(z => z.Stand).Include(x => x.CassetteType).Include(x => x.RollSetHistories).GetSingle();
        RLSCassette cassette = ctx.RLSCassettes.Where(z => z.CassetteId == id).Include(z => z.RLSStand).Include(x => x.RLSCassetteType).FirstOrDefault();
        //var rollsetToDismountId = Cassette.RollSetHistories.Where(x => x.Status == PE.Core.Constants.RollSetHistoryStatus.Actual).FirstOrDefault().FkRollSetId;
        //RLSRollSetHistory rollsetToDismountFromHistories = cassette.RLSRollSetHistories.Where(x => x.Status == RollSetHistoryStatus.Actual).FirstOrDefault();
        RLSRollSetHistory rollsetToDismountFromHistories = ctx.RLSRollSetHistories.Where(x => x.FKCassetteId == cassette.CassetteId && x.Status == RollSetHistoryStatus.Actual).FirstOrDefault();

        if (rollsetToDismountFromHistories != null)
        {
          rollsetToDismountId = rollsetToDismountFromHistories.FKRollSetId;
          //var rollsetToDismount = uow.Repository<VRollsetOverviewNewest>().Query(x => x.RollSetId == rollsetToDismountId && x.Status == 1).GetSingle();
          rollsetToDismount = ctx.V_RollsetOverviewNewest.Where(x => x.RollSetId == rollsetToDismountId && x.Status == 1).FirstOrDefault();
        }

        if (cassette != null)
        {
          returnValueVm = new VM_RollChangeOperation(cassette, positionId, rollsetToDismount, null, RollChangeAction.MountRollSetOnly);
        }

      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public VM_RollChangeOperation GetRollChangeOperationForRollSet(ModelStateDictionary modelState, long id, short? positionId)
    {
      VM_RollChangeOperation returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      //if (!modelState.IsValid)
      //{
      //	return returnValueVm;
      //}
      //END OF VALIDATION


      //DB OPERATION
      using (PEContext ctx = new PEContext())
      {
        //VRollsetOverviewNewest RollSet = uow.Repository<VRollsetOverviewNewest>().Query(z => z.RollSetId == id).GetSingle();
        V_RollsetOverviewNewest rollSet = ctx.V_RollsetOverviewNewest.Where(z => z.RollSetId == id).FirstOrDefault();
        //Cassette Cassette = uow.Repository<Cassette>().Query(z => z.CassetteId == RollSet.CassetteId).Include(z => z.Stand).Include(z => z.CassetteType).GetSingle();
        RLSCassette cassette = ctx.RLSCassettes.Where(z => z.CassetteId == rollSet.CassetteId).Include(z => z.RLSStand).Include(z => z.RLSCassetteType).FirstOrDefault();
        if (cassette != null)
        {
          returnValueVm = new VM_RollChangeOperation(cassette, positionId, rollSet, null, RollChangeAction.MountRollSetOnly);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public VM_SwappingCassettes GetSwapCassette(ModelStateDictionary modelState, long id)
    {
      VM_SwappingCassettes returnValueVm = null;

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
        //Cassette Cassette = uow.Repository<Cassette>().Query(z => z.CassetteId == id).Include(z => z.Stand).Include(z => z.CassetteType)/*.Where(z => z.Cassette.)*/.GetSingle();
        RLSCassette cassette = ctx.RLSCassettes.Where(z => z.CassetteId == id).Include(z => z.RLSStand).Include(z => z.RLSCassetteType)/*.Where(z => z.Cassette.)*/.FirstOrDefault();
        if (cassette != null)
        {
          //IList<V_RollsetOverviewNewest> RollSetList = uow.Repository<VRollsetOverviewNewest>().Query(z => z.IsThirdRoll.Value == Cassette.CassetteType.NumberOfRolls.Value /*&& (z.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Ready) || (z.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.ReadyForMounting)*/ ).Get().ToList();

          IList<V_RollsetOverviewNewest> RollSetList = ctx.V_RollsetOverviewNewest.Where(z => z.IsThirdRoll.Value == cassette.RLSCassetteType.NumberOfRolls.Value).ToList();

          if (RollSetList.Count > 0)
            returnValueVm = new VM_SwappingCassettes(cassette, RollSetList);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
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
          //IList<VRollHistory> RollHistoryBottom = uow.Repository<VRollHistory>().Query(z => z.RollSetId == id && z.RollId == rsOverview.RollIdBottom && (z.GrooveStatus == (short)PE.Core.Constants.RollGrooveStatus.Actual || z.GrooveStatus == (short)PE.Core.Constants.RollGrooveStatus.Active)).OrderBy(o => o.OrderBy(g => g.GrooveNumber)).Get().ToList();

          IList<V_RollHistory> rollHistoryBottom = ctx.V_RollHistory.Where(z => z.RollSetId == id && z.RollId == rsOverview.RollIdBottom && (z.GrooveStatus == (short)RollGrooveStatus.Actual || z.GrooveStatus == (short)RollGrooveStatus.Active)).OrderBy(g => g.GrooveNumber).ToList();

          //IList<VRollHistory> RollHistoryUpper = uow.Repository<VRollHistory>().Query(z => z.RollSetId == id && z.RollId == rsOverview.RollIdUpper && (z.GrooveStatus == (short)PE.Core.Constants.RollGrooveStatus.Actual || z.GrooveStatus == (short)PE.Core.Constants.RollGrooveStatus.Active)).OrderBy(o => o.OrderBy(g => g.GrooveNumber)).Get().ToList();

          IList<V_RollHistory> rollHistoryUpper = ctx.V_RollHistory.Where(z => z.RollSetId == id && z.RollId == rsOverview.RollIdUpper && (z.GrooveStatus == (short)RollGrooveStatus.Actual || z.GrooveStatus == (short)RollGrooveStatus.Active)).OrderBy(g => g.GrooveNumber).ToList();


          //IList<VRollHistory> RollHistoryThird = uow.Repository<VRollHistory>().Query(z => z.RollSetId == id && z.RollId == rsOverview.RollIdThird && (z.GrooveStatus == (short)PE.Core.Constants.RollGrooveStatus.Actual || z.GrooveStatus == (short)PE.Core.Constants.RollGrooveStatus.Active)).OrderBy(o => o.OrderBy(g => g.GrooveNumber)).Get().ToList();
          returnValueVm = new VM_RollsetDisplay(rsOverview, rollHistoryUpper, rollHistoryBottom);
        }
        foreach (VM_GroovesRoll gr in returnValueVm.GrooveActualRollBottom)
        {
          gr.GrooveConfirmed = (gr.GrooveStatus == (short)RollGrooveStatus.Active);
          //gr.GrooveConfirmed = false;
        }
        foreach (VM_GroovesRoll gr in returnValueVm.GrooveActualRollUpper)
        {
          gr.GrooveConfirmed = (gr.GrooveStatus == (short)RollGrooveStatus.Active);
        }
        //foreach (VM_GroovesRoll gr in returnValueVm.GrooveActualRollThird)
        //{
        //  gr.GrooveConfirmed = (gr.GrooveStatus == (short)PE.Core.Constants.RollGrooveStatus.Active);
        //}
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
    public VM_CassetteOverviewWithPositions GetCassetteWithPositions(ModelStateDictionary modelState, long id, long standId)
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
        //VCassettesOverview Cassette = uow.Repository<VCassettesOverview>().Query(z => z.CassetteId == id).GetSingle();
        V_CassettesOverview cassette = ctx.V_CassettesOverview.Where(z => z.CassetteId == id).FirstOrDefault();
        if (cassette != null)
        {
          //IList<VRollsetOverviewNewest> rollsets = uow.Repository<VRollsetOverviewNewest>()
          //                                                                .Query(f => f.CassetteId == id)
          //                                                                .OrderBy(o => o.OrderBy(p => p.PositionInCassette))
          //                                                                .Get().ToList();

          IList<V_RollsetOverviewNewest> rollSets = ctx.V_RollsetOverviewNewest.Where(f => f.CassetteId == id).OrderBy(p => p.PositionInCassette).ToList();

          returnValueVm = new VM_CassetteOverviewWithPositions(cassette, rollSets);
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public async Task<VM_Base> MountCassette(ModelStateDictionary modelState, VM_StandConfiguration viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      //string targetModule = PE.Interfaces.Module.Modules.prodmanager.Name;
      //client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModule);

      //PE.Interfaces.DC.Internal.Rollshop.DCStandConfigurationData entryDataContract = new PE.Interfaces.DC.Internal.Rollshop.DCStandConfigurationData();
      //ModuleController.InitDataContract(entryDataContract);
      //entryDataContract.StandNo = (short)viewModel.Id;
      //entryDataContract.Id = viewModel.Id;
      //entryDataContract.Status = viewModel.StandStatus;
      //entryDataContract.MountedCassette = viewModel.CassetteId;
      //entryDataContract.MountedDate = DateTime.Now;

      DCRollChangeOperationData entryDataContract = new DCRollChangeOperationData();

      entryDataContract.StandId = (short)viewModel.Id;
      entryDataContract.MountedCassette = viewModel.CassetteId;
      entryDataContract.StandNo = (short)viewModel.StandNo;
      entryDataContract.StandStatus = (StandStatus)viewModel.StandStatus;
      entryDataContract.Action = (short)RollChangeAction.MountWithCassette;
      entryDataContract.Arrangement = (CassetteArrangement)viewModel.Arrangement;
      entryDataContract.Mounted = DateTime.Now;

      //PEUnitOfWork uow = new PEUnitOfWork();
      //RollSetHistory rollSetHis = uow.Repository<RollSetHistory>().Query(z => z.FkCassetteId == viewModel.CassetteId 
      //                                                                && z.Status == PE.Core.Constants.RollSetHistoryStatus.Actual
      //                                                                ).GetSingle();

      //// entryDataContract.Position = viewModel.Position;

      //if (rollSetHis != null)
      //{
      //   entryDataContract.InterCassId = rollSetHis.FkInterCassetteId;
      //    entryDataContract.Position = rollSetHis.PositionInCassette;
      //}


      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.RollChangeAction(entryDataContract);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
    public async Task<VM_Base> DismountCassette(ModelStateDictionary modelState, VM_StandConfiguration viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      //PE.Interfaces.DC.Internal.Rollshop.DCStandConfigurationData entryDataContract = new PE.Interfaces.DC.Internal.Rollshop.DCStandConfigurationData();
      //ModuleController.InitDataContract(entryDataContract);
      //entryDataContract.StandNo = (short)viewModel.Id;
      //entryDataContract.Id = viewModel.Id;
      //entryDataContract.Status = 0;
      //entryDataContract.MountedCassette = null;
      //entryDataContract.DismountedDate = DateTime.Now;

      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.UpdateStandConfiguration(entryDataContract);
      DCRollChangeOperationData entryDataContract = new DCRollChangeOperationData();
      entryDataContract.StandId = (short)viewModel.Id;
      entryDataContract.StandNo = (short)viewModel.StandNo;
      entryDataContract.DismountedCassette = viewModel.CassetteId;
      entryDataContract.MountedCassette = viewModel.CassetteId;
      entryDataContract.Dismounted = DateTime.Now;
      entryDataContract.StandStatus = (StandStatus)0;
      entryDataContract.Action = (short)RollChangeAction.DismountWithCassette;
      entryDataContract.Arrangement = (CassetteArrangement)0;


      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.RollChangeAction(entryDataContract);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }


    public async Task<VM_Base> DismountRollSet(ModelStateDictionary modelState, VM_RollChangeOperation viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      //PE.Interfaces.DC.Internal.Rollshop.DCStandConfigurationData entryDataContract = new PE.Interfaces.DC.Internal.Rollshop.DCStandConfigurationData();
      //ModuleController.InitDataContract(entryDataContract);
      //entryDataContract.StandNo = (short)viewModel.Id;
      //entryDataContract.Id = viewModel.Id;
      //entryDataContract.Status = 0;
      //entryDataContract.MountedCassette = null;
      //entryDataContract.DismountedDate = DateTime.Now;

      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.UpdateStandConfiguration(entryDataContract);
      DCRollChangeOperationData entryDataContract = new DCRollChangeOperationData();
      entryDataContract.StandId = (short)viewModel.Id;
      entryDataContract.StandNo = (short)viewModel.StandNo;
      entryDataContract.DismountedCassette = viewModel.CassetteId;
      entryDataContract.MountedCassette = viewModel.CassetteId;
      entryDataContract.Dismounted = DateTime.Now;
      entryDataContract.StandStatus = (StandStatus)0;
      entryDataContract.Action = (short)RollChangeAction.DismountRollSetOnly;
      entryDataContract.Arrangement = (CassetteArrangement)0;
      entryDataContract.DismountedRollSet = viewModel.RollsetToBeDismountedId;


      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.RollChangeAction(entryDataContract);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> SwapRollSet(ModelStateDictionary modelState, VM_RollChangeOperation viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      //PE.Interfaces.DC.Internal.Rollshop.DCStandConfigurationData entryDataContract = new PE.Interfaces.DC.Internal.Rollshop.DCStandConfigurationData();
      //ModuleController.InitDataContract(entryDataContract);
      //entryDataContract.StandNo = (short)viewModel.Id;
      //entryDataContract.Id = viewModel.Id;
      //entryDataContract.Status = 0;
      //entryDataContract.MountedCassette = null;
      //entryDataContract.DismountedDate = DateTime.Now;

      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.UpdateStandConfiguration(entryDataContract);
      DCRollChangeOperationData entryDataContract = new DCRollChangeOperationData();
      entryDataContract.StandId = (short)viewModel.Id;
      entryDataContract.StandNo = (short)viewModel.StandNo;
      //entryDataContract.DismountedCassette = viewModel.CassetteId;
      entryDataContract.MountedCassette = viewModel.CassetteId;
      entryDataContract.Dismounted = DateTime.Now;
      entryDataContract.StandStatus = (StandStatus)viewModel.StandStatus;
      entryDataContract.Action = (short)RollChangeAction.SwapRollSetOnly;
      entryDataContract.Arrangement = (CassetteArrangement)viewModel.Arrangement;
      entryDataContract.DismountedRollSet = viewModel.RollsetToBeDismountedId;
      entryDataContract.MountedRollSet = viewModel.RollsetToBeMountedId;
      entryDataContract.Position = viewModel.PositionInCassette;


      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.RollChangeAction(entryDataContract);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> UpdateGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVM)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (actualVM.RollSetId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION


      DCRollSetGrooveSetup entryDataContract = new DCRollSetGrooveSetup();

      entryDataContract.Id = actualVM.RollSetId;
      //entryDataContract.Action = PE.Core.Constants.GrindingTurningAction.;
      List<SingleGrooveSetup> GrooveSetupList = new List<SingleGrooveSetup>();
      short grooveIdx = 1;
      if (actualVM.GrooveActualRollUpper != null)
      {
        for (int i = 0; i < actualVM.GrooveActualRollBottom.Count; i++)
        {
          if (actualVM.GrooveActualRollUpper[i].GrooveConfirmed == true)
          {
            GrooveSetupList.Add(new SingleGrooveSetup { GrooveTemplate = (long)actualVM.GrooveActualRollBottom[i].GrooveTemplateId, GrooveIdx = grooveIdx, GrooveStatus = (short)RollGrooveStatus.Active });
            //GrooveSetupList.Add(new SingleGrooveSetup { GrooveTemplate = (long)actualVM.GrooveActualRollUpper[i].GrooveTemplateId, GrooveIdx = grooveIdx, GrooveStatus = (short)PE.Core.Constants.RollGrooveStatus.Active });
            //if (actualVM.GrooveActualRollThird != null)
            //{
            //  GrooveSetupList.Add(new SingleGrooveSetup { GrooveTemplate = (long)actualVM.GrooveActualRollThird[i].GrooveTemplateId, GrooveIdx = grooveIdx, GrooveStatus = (short)PE.Core.Constants.RollGrooveStatus.Active });
            //}
          }
          else
          {
            GrooveSetupList.Add(new SingleGrooveSetup { GrooveTemplate = (long)actualVM.GrooveActualRollBottom[i].GrooveTemplateId, GrooveIdx = grooveIdx, GrooveStatus = (short)RollGrooveStatus.Actual });
            //GrooveSetupList.Add(new SingleGrooveSetup { GrooveTemplate = (long)actualVM.GrooveActualRollUpper[i].GrooveTemplateId, GrooveIdx = grooveIdx, GrooveStatus = (short)PE.Core.Constants.RollGrooveStatus.Actual });
            //if (actualVM.GrooveActualRollThird != null)
            //{
            //  GrooveSetupList.Add(new SingleGrooveSetup { GrooveTemplate = (long)actualVM.GrooveActualRollThird[i].GrooveTemplateId, GrooveIdx = grooveIdx, GrooveStatus = (short)PE.Core.Constants.RollGrooveStatus.Actual });
            //}
          }
          grooveIdx++;
        }
      }
      entryDataContract.GrooveSetupList = GrooveSetupList;
      //save list

      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.UpdateGroovesStatuses(entryDataContract);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateGroovesStatusesAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }


    // commented out 07.10.2019. Data contract initialized, but not used.
    //public async Task<VM_Base> SwapStand(ModelStateDictionary modelState, VM_StandConfiguration viewModel)
    //{
    //  VM_Base returnVM = new VM_Base();
    //  IProdManager client = null;

    //  //VALIDATE ENTRY PARAMETERS
    //  if (!modelState.IsValid)
    //  {
    //    return returnVM;
    //  }
    //  //END OF VALIDATION

    //  string targetModule = PE.Interfaces.Module.Modules.prodmanager.Name;
    //  client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModule);

    //  PE.Interfaces.DC.Internal.Rollshop.DCStandSwapData entryDataContract = new DCStandSwapData();
    //  ModuleController.InitDataContract(entryDataContract);
    //  entryDataContract.StandId = (short)viewModel.Id;



    //  //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.SwapStand(entryDataContract);

    //  //if (await Task.WhenAny(taskOnRemoteModule) == taskOnRemoteModule)
    //  //{
    //  //  //task completed within timeout
    //  //  //use the result from "SendOffice"
    //  //  if (taskOnRemoteModule.Result != null)
    //  //  {
    //  //    //check communication
    //  //    if (taskOnRemoteModule.Result.OperationSuccess)
    //  //    {
    //  //      //check result of request
    //  //      if (taskOnRemoteModule.Result.DataConctract)
    //  //      {
    //  //        ModuleController.Logger.Info("RollShop operation: \"UpdateStandConfiguration\" result: OK");
    //  //      }
    //  //      else
    //  //      {
    //  //        ModuleController.Logger.Error("Error in RollShop during operation:  \"UpdateStandConfiguration\"");
    //  //        throw new Exception(PE.HMIWWW.Core.Resources.ResourceController.GetErrorText("ErrorInModuleRollShop"));
    //  //      }
    //  //    }
    //  //    else
    //  //      throw new Exception(ResourceController.GetErrorText("NoCommunication"));
    //  //  }
    //  //}
    //  //else
    //  //{
    //  //  // timeout logic
    //  //  throw new Exception(PE.HMIWWW.Core.Resources.ResourceController.GetErrorText("TimeoutInModuleRollShop"));
    //  //}
    //  return returnVM;
    //}

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

    // commented out 07.10.2019. Data contract initialized, but not used.
    // for mounting rollSet to cassette whichone is in Stand
    public async Task<VM_Base> MountRollSet(ModelStateDictionary modelState, VM_RollChangeOperation viewModel)
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

      DCRollChangeOperationData entryDataContract = new DCRollChangeOperationData();

      entryDataContract.StandId = (short)viewModel.Id;
      entryDataContract.MountedCassette = viewModel.CassetteId;
      entryDataContract.StandNo = (short)viewModel.StandNo;
      entryDataContract.StandStatus = (StandStatus)viewModel.StandStatus;
      entryDataContract.Action = (short)RollChangeAction.MountRollSetOnly;
      entryDataContract.Arrangement = (CassetteArrangement)viewModel.Arrangement;
      entryDataContract.MountedRollSet = viewModel.RollsetToBeMountedId;
      entryDataContract.Mounted = DateTime.Now;
      entryDataContract.Position = viewModel.PositionInCassette;

      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.AssembleRollSetAndCassetteWithConfirm(entryDataContract);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }


    public async Task<VM_Base> UpdateStandConfiguration(ModelStateDictionary modelState, VM_StandConfiguration actualVM)
    {
      VM_StandConfiguration result = null;
      if (actualVM == null)
      {
        return result;
      }

      DCStandConfigurationData entryDataContract = new DCStandConfigurationData(); // InStand
      entryDataContract.Id = actualVM.Id;
      entryDataContract.IsCalibrated = actualVM.IsCalibrated;
      entryDataContract.NumberOfRolls = actualVM.NumberOfRolls;
      entryDataContract.StandNo = actualVM.StandNo;
      entryDataContract.Status = actualVM.StandStatus;
      entryDataContract.Arrangement = actualVM.Arrangement;

      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.UpdateStandConfiguration(entryDataContract);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateStandConfigurationAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
    #endregion
  }
}
