using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using System.Data.Entity;
using PE.DTO.Internal.RollShop;
using PE.DbEntity.Enums;

namespace PE.RLS.Handlers
{
  public sealed class RollHandler
  {
    public RLSRoll GetRollFromName(PEContext ctx, string rollName)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSRoll roll = ctx.RLSRolls.Where(x => x.RollName == rollName).FirstOrDefault();
      return roll;
    }

    public RLSRoll GetRollFromId(PEContext ctx, long? rollId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSRoll roll = ctx.RLSRolls.Where(x => x.RollId == rollId).FirstOrDefault();
      return roll;
    }

    public void UpdateRoll(PEContext ctx, ref RLSRoll roll, DCRollData dc)
    {
      roll.ActualDiameter = dc.ActualDiameter ?? 0;
      roll.CreatedTs = DateTime.Now;
      roll.RollDescription = dc.Description;
      roll.FKRollTypeId = dc.RollTypeId ?? -1;
      //if (dc.GroovesNumber == null)
      //  Roll.GroovesNumber = 1;  // 0
      //else
      //  Roll.GroovesNumber = (short)dc.GroovesNumber;
      roll.InitialDiameter = dc.InitialDiameter ?? 0.0;
      roll.MinimumDiameter = dc.MinimumDiameter ?? 0.0;
      roll.RollName = dc.RollName;
      roll.Status = dc.Status;
      roll.Supplier = dc.Supplier;
    }


    public RLSStand GetStandFromId(PEContext ctx, long? standId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSStand stand = ctx.RLSStands.Where(x => x.StandId == standId).FirstOrDefault();
      return stand;
    }

    public RLSStand GetStandFromStandNo(PEContext ctx, short standNo)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSStand stand = ctx.RLSStands.Where(x => x.StandNo == standNo).FirstOrDefault();
      return stand;
    }

    public RLSCassette GetCassetteFromStandId(PEContext ctx, long? standId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSCassette cassette = ctx.RLSCassettes.Where(x => x.FKStandId == standId).FirstOrDefault();
      return cassette;
    }
  }
}
