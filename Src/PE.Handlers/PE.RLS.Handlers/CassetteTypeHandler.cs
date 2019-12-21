using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.DTO.Internal.RollShop;

namespace PE.RLS.Handlers
{
  public sealed class CassetteTypeHandler
  {
    public RLSCassetteType GetCassetteTypeFromName(PEContext ctx, string cassetteTypeName)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSCassetteType cassetteType = ctx.RLSCassetteTypes.Where(x => x.CassetteTypeName == cassetteTypeName).FirstOrDefault();
      return cassetteType;
    }

    public RLSCassetteType GetCassetteTypeFromId(PEContext ctx, long? cassetteTypeId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSCassetteType cassetteType = ctx.RLSCassetteTypes.Where(x => x.CassetteTypeId == cassetteTypeId).FirstOrDefault();
      return cassetteType;
    }

    public void CreateCassetteType(PEContext ctx, ref RLSCassetteType cassetteType, DCCassetteTypeData dc)
    {
      cassetteType.CassetteTypeName = dc.CassetteTypeName;
      cassetteType.CreatedTs = DateTime.Now;
      cassetteType.LastUpdateTs = DateTime.Now;
      cassetteType.CassetteTypeDescription = dc.Description;
      cassetteType.NumberOfRolls = dc.NumberOfRolls;
    }

    public void UpdateCassetteType(PEContext ctx, ref RLSCassetteType cassetteType, DCCassetteTypeData dc)
    {
      cassetteType.CassetteTypeName = dc.CassetteTypeName;
      cassetteType.LastUpdateTs = DateTime.Now;
      cassetteType.CassetteTypeDescription = dc.Description;
      cassetteType.NumberOfRolls = dc.NumberOfRolls;
    }
  }
}
