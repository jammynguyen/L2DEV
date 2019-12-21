using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.RollShop;

namespace PE.RLS.Handlers
{
  public sealed class CassetteHandler
  {
    public RLSCassette GetCassetteFromName(PEContext ctx, string cassetteName)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSCassette cassette = ctx.RLSCassettes.Where(x => x.CassetteName == cassetteName).FirstOrDefault();
      return cassette;
    }

    public RLSCassette GetCassetteFromId(PEContext ctx, long? cassetteId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSCassette cassette = ctx.RLSCassettes.Where(x => x.CassetteId == cassetteId).FirstOrDefault();
      return cassette;
    }

    public void CreateCassette(PEContext ctx, ref RLSCassette cassette, DCCassetteData dc)
    {
      cassette.Arrangement = CassetteArrangement.Undefined;
      cassette.CassetteName = dc.CassetteName;
      cassette.CreatedTs = DateTime.Now;
      cassette.LastUpdateTs = DateTime.Now;
      cassette.FKCassetteTypeId = (short)dc.CassetteTypeId;
      cassette.FKStandId = dc.StandId;
      cassette.NumberOfPositions = (short)dc.NumberOfPositions;
      cassette.Status = CassetteStatus.New;
    }

    public void UpdateCassette(PEContext ctx, ref RLSCassette cassette, DCCassetteData dc)
    {
      cassette.CassetteName = dc.CassetteName;
      cassette.CreatedTs = DateTime.Now;
      cassette.FKCassetteTypeId = (short)dc.CassetteTypeId;
      cassette.FKStandId = dc.StandId;
      cassette.NumberOfPositions = (short)dc.NumberOfPositions;
      cassette.Status = (CassetteStatus)dc.Status;
    }


    public List<V_CassettesInStands> GetCassetteInStand(PEContext ctx, short standNo)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      List<V_CassettesInStands> cassetteinstand = ctx.V_CassettesInStands.Where(q => q.StandNo == standNo 
                                                                                && q.RollsetHistoryStatus == (short)RollSetHistoryStatus.Actual 
                                                                                && q.CassetteStatus == (short)CassetteStatus.MountedInStand)
                                                                         .ToList();
      return cassetteinstand;
    }
    
  }
}
