using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using PE.DTO.Internal.ProdManager;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class SteelgradeService : BaseService, ISteelgradeService
  {
    public VM_Steelgrade GetSteelgrade(ModelStateDictionary modelState, long id)
    {
      VM_Steelgrade result = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      using (PEContext ctx = new PEContext())
      {
        PRMSteelgrade sg = ctx.PRMSteelgrades
                    .Include(i => i.PRMSteelgradeChemicalComposition)
                    .Include(i => i.PRMSteelGroup)
                    .SingleOrDefault(x => x.SteelgradeId == id);
        result = sg != null ? new VM_Steelgrade(sg) : null;
      }

      return result;
    }

    public IList<VM_SteelGroup> GetSteelgradeGroups()
    {
      List<VM_SteelGroup> result = new List<VM_SteelGroup>();
      using (PEContext ctx = new PEContext())
      {

        IQueryable<PRMSteelGroup> dbList = ctx.PRMSteelGroups.AsQueryable();
        foreach (PRMSteelGroup item in dbList)
        {
          result.Add(new VM_SteelGroup(item));
        }
      }
      return result;
    }

    public DataSourceResult GetSteelgradeList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.PRMSteelgrades
          .Include(i => i.PRMSteelgradeChemicalComposition)
          .Include(i => i.PRMSteelgrade1)
          .Include(i => i.PRMSteelGroup)
                    .ToDataSourceResult<PRMSteelgrade, VM_Steelgrade>(request, modelState, x => new VM_Steelgrade(x));
      }

      return result;
    }

    public async Task<VM_Base> CreateSteelgrade(ModelStateDictionary modelState, VM_Steelgrade steelgrade)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref steelgrade);

      DCSteelgrade dcSteelgrade = new DCSteelgrade()
      {
        Id = steelgrade.Id,
        SteelgradeCode = steelgrade.SteelgradeCode,
        Description = steelgrade.Description,
        Density = steelgrade.Density,
        IsDefault = steelgrade.IsDefault,
        QualitySpecification = steelgrade.QualitySpecification,
        CommercialGroup = steelgrade.CommercialGroup,
        CustomerUseCode = steelgrade.CustomerUseCode,
        CustomerUseDescription = steelgrade.CustomerUseDescription,
        GroupCode = steelgrade.GroupCode,
        GroupDescription = steelgrade.GroupDescription,
        OvenRecipeTemperature = steelgrade.OvenRecipeTemperature,
        FkSteelGroup = steelgrade.FkGroupId,

        FeMax = steelgrade.FeMax,
        FeMin = steelgrade.FeMin,
        CMax = steelgrade.CMax,
        CMin = steelgrade.CMin,
        MnMax = steelgrade.MnMax,
        MnMin = steelgrade.MnMin,
        CrMax = steelgrade.CrMax,
        CrMin = steelgrade.CrMin,
        MoMax = steelgrade.MoMax,
        MoMin = steelgrade.MoMin,
        VMax = steelgrade.VMax,
        VMin = steelgrade.VMin,
        NiMax = steelgrade.NiMax,
        NiMin = steelgrade.NiMin,
        CoMax = steelgrade.CoMax,
        CoMin = steelgrade.CoMin,
        SiMax = steelgrade.SiMax,
        SiMin = steelgrade.SiMin,
        PMax = steelgrade.PMax,
        PMin = steelgrade.PMin,
        SMax = steelgrade.SMax,
        SMin = steelgrade.SMin,
        CuMax = steelgrade.CuMax,
        CuMin = steelgrade.CuMin,
        NbMax = steelgrade.NbMax,
        NbMin = steelgrade.NbMin,
        AlMax = steelgrade.AlMax,
        AlMin = steelgrade.AlMin,
        NMax = steelgrade.NMax,
        NMin = steelgrade.NMin,
        CaMax = steelgrade.CaMax,
        CaMin = steelgrade.CaMin,
        BMax = steelgrade.BMax,
        BMin = steelgrade.BMin,
        TiMax = steelgrade.TiMax,
        TiMin = steelgrade.TiMin,
        SnMax = steelgrade.SnMax,
        SnMin = steelgrade.SnMin,
        OMax = steelgrade.OMax,
        OMin = steelgrade.OMin,
        HMax = steelgrade.HMax,
        HMin = steelgrade.HMin,
        WMax = steelgrade.WMax,
        WMin = steelgrade.WMin,
        PbMax = steelgrade.PbMax,
        PbMin = steelgrade.PbMin,
        ZnMax = steelgrade.ZnMax,
        ZnMin = steelgrade.ZnMin,
        AsMax = steelgrade.AsMax,
        AsMin = steelgrade.AsMin,
        MgMax = steelgrade.MgMax,
        MgMin = steelgrade.MgMin,
        SbMax = steelgrade.SbMax,
        SbMin = steelgrade.SbMin,
        BiMax = steelgrade.BiMax,
        BiMin = steelgrade.BiMin,
        TaMax = steelgrade.TaMax,
        TaMin = steelgrade.TaMin,
        ZrMax = steelgrade.ZrMax,
        ZrMin = steelgrade.ZrMin,
        CeMax = steelgrade.CeMax,
        CeMin = steelgrade.CeMin,
        TeMax = steelgrade.TeMax,
        TeMin = steelgrade.TeMin,
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendCreateSteelgradeAsync(dcSteelgrade);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> UpdateSteelgrade(ModelStateDictionary modelState, VM_Steelgrade steelgrade)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref steelgrade);

      DCSteelgrade dcSteelgrade = new DCSteelgrade()
      {
        Id = steelgrade.Id,
        SteelgradeCode = steelgrade.SteelgradeCode,
        Description = steelgrade.Description,
        Density = steelgrade.Density,
        IsDefault = steelgrade.IsDefault,
        QualitySpecification = steelgrade.QualitySpecification,
        CommercialGroup = steelgrade.CommercialGroup,
        CustomerUseCode = steelgrade.CustomerUseCode,
        CustomerUseDescription = steelgrade.CustomerUseDescription,
        GroupCode = steelgrade.GroupCode,
        GroupDescription = steelgrade.GroupDescription,
        OvenRecipeTemperature = steelgrade.OvenRecipeTemperature,
        FkSteelGroup = steelgrade.FkGroupId,

        FeMax = steelgrade.FeMax,
        FeMin = steelgrade.FeMin,
        CMax = steelgrade.CMax,
        CMin = steelgrade.CMin,
        MnMax = steelgrade.MnMax,
        MnMin = steelgrade.MnMin,
        CrMax = steelgrade.CrMax,
        CrMin = steelgrade.CrMin,
        MoMax = steelgrade.MoMax,
        MoMin = steelgrade.MoMin,
        VMax = steelgrade.VMax,
        VMin = steelgrade.VMin,
        NiMax = steelgrade.NiMax,
        NiMin = steelgrade.NiMin,
        CoMax = steelgrade.CoMax,
        CoMin = steelgrade.CoMin,
        SiMax = steelgrade.SiMax,
        SiMin = steelgrade.SiMin,
        PMax = steelgrade.PMax,
        PMin = steelgrade.PMin,
        SMax = steelgrade.SMax,
        SMin = steelgrade.SMin,
        CuMax = steelgrade.CuMax,
        CuMin = steelgrade.CuMin,
        NbMax = steelgrade.NbMax,
        NbMin = steelgrade.NbMin,
        AlMax = steelgrade.AlMax,
        AlMin = steelgrade.AlMin,
        NMax = steelgrade.NMax,
        NMin = steelgrade.NMin,
        CaMax = steelgrade.CaMax,
        CaMin = steelgrade.CaMin,
        BMax = steelgrade.BMax,
        BMin = steelgrade.BMin,
        TiMax = steelgrade.TiMax,
        TiMin = steelgrade.TiMin,
        SnMax = steelgrade.SnMax,
        SnMin = steelgrade.SnMin,
        OMax = steelgrade.OMax,
        OMin = steelgrade.OMin,
        HMax = steelgrade.HMax,
        HMin = steelgrade.HMin,
        WMax = steelgrade.WMax,
        WMin = steelgrade.WMin,
        PbMax = steelgrade.PbMax,
        PbMin = steelgrade.PbMin,
        ZnMax = steelgrade.ZnMax,
        ZnMin = steelgrade.ZnMin,
        AsMax = steelgrade.AsMax,
        AsMin = steelgrade.AsMin,
        MgMax = steelgrade.MgMax,
        MgMin = steelgrade.MgMin,
        SbMax = steelgrade.SbMax,
        SbMin = steelgrade.SbMin,
        BiMax = steelgrade.BiMax,
        BiMin = steelgrade.BiMin,
        TaMax = steelgrade.TaMax,
        TaMin = steelgrade.TaMin,
        ZrMax = steelgrade.ZrMax,
        ZrMin = steelgrade.ZrMin,
        CeMax = steelgrade.CeMax,
        CeMin = steelgrade.CeMin,
        TeMax = steelgrade.TeMax,
        TeMin = steelgrade.TeMin,
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendSteelgradeAsync(dcSteelgrade);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteSteelgrade(ModelStateDictionary modelState, VM_Steelgrade steelgrade)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref steelgrade);

      DCSteelgrade dcSteelgrade = new DCSteelgrade()
      {
        Id = steelgrade.Id
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDeleteSteelgradeAsync(dcSteelgrade);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_Steelgrade GetSteelgradeDetails(ModelStateDictionary modelState, long Id)
    {
      VM_Steelgrade result = null;

      if (Id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return result;
      }

      using (PEContext ctx = new PEContext())
      {
        PRMSteelgrade data = ctx.PRMSteelgrades
          .Include(i => i.PRMSteelgradeChemicalComposition)
          .Include(i => i.PRMSteelGroup)
          .Where(x => x.SteelgradeId == Id)
          .SingleOrDefault();

        result = new VM_Steelgrade(data);
      }

      return result;
    }
  }
}
