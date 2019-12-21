using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.DTO.Internal.RollShop;

namespace PE.RLS.Handlers
{
  public sealed class RollTypeHandler
  {
    public RLSRollType GetRollTypeFromName(PEContext ctx, string rollTypeName)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSRollType rollType = ctx.RLSRollTypes.Where(x => x.RollTypeName == rollTypeName).FirstOrDefault();
      return rollType;
    }

    public RLSRollType GetRollTypeFromId(PEContext ctx, long? rollTypeId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSRollType rollType = ctx.RLSRollTypes.Where(x => x.RollTypeId == rollTypeId).FirstOrDefault();
      return rollType;
    }

    public void CreateRollType(PEContext ctx, ref RLSRollType rollType, DCRollTypeData dc)
    {
      rollType.AccBilletCntLimit = dc.AccBilletCntLimit;
      rollType.AccWeightLimit = dc.AccWeightLimit;
      rollType.ChokeType = dc.ChokeType;
      rollType.CreatedTs = DateTime.Now;
      rollType.LastUpdateTs = DateTime.Now;
      rollType.DiameterMax = dc.DiameterMax;
      rollType.DiameterMin = dc.DiameterMin;
      rollType.DrawingName = dc.DrawingName;
      rollType.Length = dc.Length;
      rollType.RollTypeDescription = dc.RollTypeDescription;
      rollType.RollTypeName = dc.RollTypeName;
      rollType.RoughnessMax = dc.RoughnessMax;
      rollType.RoughnessMin = dc.RoughnessMin;
      rollType.SteelgradeRoll = dc.SteelgradeRoll;
      rollType.YieldStrengthRef = dc.YieldStrengthRef;
      rollType.MatchingRollsetType = dc.MatchingRollSetType;
    }

    public void UpdateRollType(PEContext ctx, ref RLSRollType rollType, DCRollTypeData dc)
    {
      rollType.AccBilletCntLimit = dc.AccBilletCntLimit;
      rollType.AccWeightLimit = dc.AccWeightLimit;
      rollType.ChokeType = dc.ChokeType;
      rollType.CreatedTs = DateTime.Now;
      rollType.DiameterMax = dc.DiameterMax;
      rollType.DiameterMin = dc.DiameterMin;
      rollType.DrawingName = dc.DrawingName;
      rollType.Length = dc.Length;
      rollType.RollTypeDescription = dc.RollTypeDescription;
      rollType.RollTypeName = dc.RollTypeName;
      rollType.RoughnessMax = dc.RoughnessMax;
      rollType.RoughnessMin = dc.RoughnessMin;
      rollType.SteelgradeRoll = dc.SteelgradeRoll;
      rollType.YieldStrengthRef = dc.YieldStrengthRef;
      rollType.MatchingRollsetType = dc.MatchingRollSetType;
    }
  }
}
