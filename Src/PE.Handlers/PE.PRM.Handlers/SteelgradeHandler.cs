using PE.DbEntity.Models;
using PE.DTO.Internal.ProdManager;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PE.DTO.Internal.Adapter;

namespace PE.PRM.Handlers
{
  public sealed class SteelgradeHandler
  {
    /// <summary>
    /// Return steelgrade object found by his name or default steelgrade if any dont match (optional )
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="steelgradeCode"></param>
    /// <returns></returns>
    public async Task<PRMSteelgrade> GetSteelgradeByCodeAsync(PEContext ctx, string steelgradeCode, bool getDefault = false)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      PRMSteelgrade steelgrade = await ctx.PRMSteelgrades.Where(x => x.SteelgradeCode.ToLower().Equals(steelgradeCode.ToLower())).Include(i => i.PRMSteelgradeChemicalComposition).SingleOrDefaultAsync();

      if (steelgrade == null && getDefault == true)
      {
        steelgrade = await ctx.PRMSteelgrades.Where(x => x.IsDefault == true).SingleAsync();
      }

      return steelgrade;
    }

    /// <summary>
    /// Return steelgrade object found by his ID or null
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="steelgradeId"></param>
    /// <returns></returns>
    public async Task<PRMSteelgrade> GetSteelgradeByIdAsync(PEContext ctx, long steelgradeId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      return await ctx.PRMSteelgrades.FindAsync(steelgradeId);
    }

    /// <summary>
    /// For Work Order creation it is needed to connect steelgrade,
    /// if given not exist, then fill back information about it and return default
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="steelgradeCode"></param>
    /// <param name="backMsg"></param>
    /// <returns></returns>
    public async Task<PRMSteelgrade> GetSteelgradeForWorkOrderProcessingAsync(PEContext ctx, string steelgradeCode, DCWorkOrderStatus backMsg)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      PRMSteelgrade steelgrade = await GetSteelgradeByCodeAsync(ctx, steelgradeCode);

      if (steelgrade == null )
      {
        backMsg.BackMessage = " Steelgrade not found -> default;";
        steelgrade = await ctx.PRMSteelgrades.Where(x => x.IsDefault == true).SingleAsync();
      }

      return steelgrade;
    }

    public PRMSteelgrade CreateSteelgrade(DCSteelgrade dc)
    {
      PRMSteelgrade steelgrade;
      try
      {
        steelgrade = new PRMSteelgrade()
        {
          SteelgradeCode = dc.SteelgradeCode,
          CreatedTs = DateTime.Now,
          LastUpdateTs = DateTime.Now,
          SteelgradeDescription = dc.Description,
          Density = dc.Density,
          IsDefault = dc.IsDefault.GetValueOrDefault(),
          QualitySpecification = dc.QualitySpecification,
          CommercialGroup = dc.CommercialGroup,
          CustomerUseCode = dc.CustomerUseCode,
          CustomerUseDescription = dc.CustomerUseDescription,
          FKSteelGroupId = dc.FkSteelGroup,
          //steelgrade.GroupCode = dc.GroupCode;
          //steelgrade.GroupDescription = dc.GroupDescription;
          OvenRecipeTemperature = dc.OvenRecipeTemperature
        };

        //steelgrade.PRMSteelgradeChemicalComposition = CreateUpdateSteelgradeChemicalComposition(steelgrade.PRMSteelgradeChemicalComposition, dc);

      }
      catch (Exception)
      {
        throw new Exception() { Source = "CreateSteelgrade - Error during saving" };
      }
      return steelgrade;
    }

    public void UpdateSteelgrade(PRMSteelgrade steelgrade, DCSteelgrade dc)
    {

      try
      {
        if (steelgrade != null)
        {
          steelgrade.SteelgradeCode = dc.SteelgradeCode;
          steelgrade.SteelgradeDescription = dc.Description;
          steelgrade.Density = dc.Density;
          steelgrade.IsDefault = dc.IsDefault.GetValueOrDefault();
          steelgrade.QualitySpecification = dc.QualitySpecification;
          steelgrade.CommercialGroup = dc.CommercialGroup;
          steelgrade.CustomerUseCode = dc.CustomerUseCode;
          steelgrade.CustomerUseDescription = dc.CustomerUseDescription;
          steelgrade.FKSteelGroupId = dc.FkSteelGroup;
          //steelgrade.GroupCode = dc.GroupCode;
          //steelgrade.GroupDescription = dc.GroupDescription;
          steelgrade.OvenRecipeTemperature = dc.OvenRecipeTemperature;

          steelgrade.PRMSteelgradeChemicalComposition = CreateUpdateSteelgradeChemicalComposition(steelgrade.PRMSteelgradeChemicalComposition, dc);
     
        }
        else
        {
          throw new Exception() { Source = "UpdateSteelgrade - SG not found" };
        }
      }
      catch(Exception)
      {
        throw new Exception() { Source = "UpdateSteelgrade - Error during saving" };
      }


    }


    private PRMSteelgradeChemicalComposition CreateUpdateSteelgradeChemicalComposition(PRMSteelgradeChemicalComposition steelgradeChemicalComposition, DCSteelgrade dc)
    {
      try
      {
        if (steelgradeChemicalComposition == null)
        {
          steelgradeChemicalComposition = new PRMSteelgradeChemicalComposition
          {
            FKSteelgradeId = dc.Id,
            LastUpdateTs = DateTime.Now,
            CreatedTs = DateTime.Now
          };
        }

        steelgradeChemicalComposition.FeMax = dc.FeMax == null ? 0 : dc.FeMax.Value;
        steelgradeChemicalComposition.FeMin = dc.FeMin == null ? 0 : dc.FeMin.Value;
        steelgradeChemicalComposition.CMax = dc.CMax == null ? 0 : dc.CMax.Value;
        steelgradeChemicalComposition.CMin = dc.CMin == null ? 0 : dc.CMin.Value;
        steelgradeChemicalComposition.MnMax = dc.MnMax == null ? 0 : dc.MnMax.Value;
        steelgradeChemicalComposition.MnMin = dc.MnMin == null ? 0 : dc.MnMin.Value;
        steelgradeChemicalComposition.CrMax = dc.CrMax == null ? 0 : dc.CrMax.Value;
        steelgradeChemicalComposition.CrMin = dc.CrMin == null ? 0 : dc.CrMin.Value;
        steelgradeChemicalComposition.MoMax = dc.MoMax == null ? 0 : dc.MoMax.Value;
        steelgradeChemicalComposition.MoMin = dc.MoMin == null ? 0 : dc.MoMin.Value;
        steelgradeChemicalComposition.VMax = dc.VMax == null ? 0 : dc.VMax.Value;
        steelgradeChemicalComposition.VMin = dc.VMin == null ? 0 : dc.VMin.Value;
        steelgradeChemicalComposition.NiMax = dc.NiMax == null ? 0 : dc.NiMax.Value;
        steelgradeChemicalComposition.NiMin = dc.NiMin == null ? 0 : dc.NiMin.Value;
        steelgradeChemicalComposition.CoMax = dc.CoMax == null ? 0 : dc.CoMax.Value;
        steelgradeChemicalComposition.CoMin = dc.CoMin == null ? 0 : dc.CoMin.Value;
        steelgradeChemicalComposition.SiMax = dc.SiMax == null ? 0 : dc.SiMax.Value;
        steelgradeChemicalComposition.SiMin = dc.SiMin == null ? 0 : dc.SiMin.Value;
        steelgradeChemicalComposition.PMax = dc.PMax == null ? 0 : dc.PMax.Value;
        steelgradeChemicalComposition.PMin = dc.PMin == null ? 0 : dc.PMin.Value;
        steelgradeChemicalComposition.SMax = dc.SMax == null ? 0 : dc.SMax.Value;
        steelgradeChemicalComposition.SMin = dc.SMin == null ? 0 : dc.SMin.Value;
        steelgradeChemicalComposition.CuMax = dc.CuMax == null ? 0 : dc.CuMax.Value;
        steelgradeChemicalComposition.CuMin = dc.CuMin == null ? 0 : dc.CuMin.Value;
        steelgradeChemicalComposition.NbMax = dc.NbMax == null ? 0 : dc.NbMax.Value;
        steelgradeChemicalComposition.NbMin = dc.NbMin == null ? 0 : dc.NbMin.Value;
        steelgradeChemicalComposition.AlMax = dc.AlMax == null ? 0 : dc.AlMax.Value;
        steelgradeChemicalComposition.AlMin = dc.AlMin == null ? 0 : dc.AlMin.Value;
        steelgradeChemicalComposition.NMax = dc.NMax == null ? 0 : dc.NMax.Value;
        steelgradeChemicalComposition.NMin = dc.NMin == null ? 0 : dc.NMin.Value;
        steelgradeChemicalComposition.CaMax = dc.CaMax == null ? 0 : dc.CaMax.Value;
        steelgradeChemicalComposition.CaMin = dc.CaMin == null ? 0 : dc.CaMin.Value;
        steelgradeChemicalComposition.BMax = dc.BMax == null ? 0 : dc.BMax.Value;
        steelgradeChemicalComposition.BMin = dc.BMin == null ? 0 : dc.BMin.Value;
        steelgradeChemicalComposition.TiMax = dc.TiMax == null ? 0 : dc.TiMax.Value;
        steelgradeChemicalComposition.TiMin = dc.TiMin == null ? 0 : dc.TiMin.Value;
        steelgradeChemicalComposition.SnMax = dc.SnMax == null ? 0 : dc.SnMax.Value;
        steelgradeChemicalComposition.SnMin = dc.SnMin == null ? 0 : dc.SnMin.Value;
        steelgradeChemicalComposition.OMax = dc.OMax == null ? 0 : dc.OMax.Value;
        steelgradeChemicalComposition.OMin = dc.OMin == null ? 0 : dc.OMin.Value;
        steelgradeChemicalComposition.HMax = dc.HMax == null ? 0 : dc.HMax.Value;
        steelgradeChemicalComposition.HMin = dc.HMin == null ? 0 : dc.HMin.Value;
        steelgradeChemicalComposition.WMax = dc.WMax == null ? 0 : dc.WMax.Value;
        steelgradeChemicalComposition.WMin = dc.WMin == null ? 0 : dc.WMin.Value;
        steelgradeChemicalComposition.PbMax = dc.PbMax == null ? 0 : dc.PbMax.Value;
        steelgradeChemicalComposition.PbMin = dc.PbMin == null ? 0 : dc.PbMin.Value;
        steelgradeChemicalComposition.ZnMax = dc.ZnMax == null ? 0 : dc.ZnMax.Value;
        steelgradeChemicalComposition.ZnMin = dc.ZnMin == null ? 0 : dc.ZnMin.Value;
        steelgradeChemicalComposition.AsMax = dc.AsMax == null ? 0 : dc.AsMax.Value;
        steelgradeChemicalComposition.AsMin = dc.AsMin == null ? 0 : dc.AsMin.Value;
        steelgradeChemicalComposition.MgMax = dc.MgMax == null ? 0 : dc.MgMax.Value;
        steelgradeChemicalComposition.MgMin = dc.MgMin == null ? 0 : dc.MgMin.Value;
        steelgradeChemicalComposition.SbMax = dc.SbMax == null ? 0 : dc.SbMax.Value;
        steelgradeChemicalComposition.SbMin = dc.SbMin == null ? 0 : dc.SbMin.Value;
        steelgradeChemicalComposition.BiMax = dc.BiMax == null ? 0 : dc.BiMax.Value;
        steelgradeChemicalComposition.BiMin = dc.BiMin == null ? 0 : dc.BiMin.Value;
        steelgradeChemicalComposition.TaMax = dc.TaMax == null ? 0 : dc.TaMax.Value;
        steelgradeChemicalComposition.TaMin = dc.TaMin == null ? 0 : dc.TaMin.Value;
        steelgradeChemicalComposition.ZrMax = dc.ZrMax == null ? 0 : dc.ZrMax.Value;
        steelgradeChemicalComposition.ZrMin = dc.ZrMin == null ? 0 : dc.ZrMin.Value;
        steelgradeChemicalComposition.CeMax = dc.CeMax == null ? 0 : dc.CeMax.Value;
        steelgradeChemicalComposition.CeMin = dc.CeMin == null ? 0 : dc.CeMin.Value;
        steelgradeChemicalComposition.TeMax = dc.TeMax == null ? 0 : dc.TeMax.Value;
        steelgradeChemicalComposition.TeMin = dc.TeMin == null ? 0 : dc.TeMin.Value;
      }
      catch (Exception)
      {
        throw new Exception() { Source = "CreateUpdateSteelgradeChemicalComposition - Error during creation" };
      }

      return steelgradeChemicalComposition;
    }
  }
}
