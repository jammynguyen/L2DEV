using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using System.Data.Entity;
using PE.DTO.Internal.Maintenance;
using SMF.Core.DC;

namespace PE.MNT.Handlers
{
    public class EquipmentGroupsHandler
    {
    /// <summary>
    /// Return Equipment Group record or null if not found
    /// </summary>
    /// <param name="ctx">DB context for EF</param>
    /// <param name="name">Equipment group name</param>
    /// <returns></returns>
    public async Task<MNTEquipmentGroup> GetEquipmentGroupByNameAsync(PEContext ctx, string name)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      return await ctx.MNTEquipmentGroups.Where(x => x.EquipmentGroupName.ToLower().Equals(name.ToLower())).SingleOrDefaultAsync();
    }

    /// <summary>
    /// Return Equipment Group record or null if not found
    /// </summary>
    /// <param name="ctx">DB context for EF</param>
    /// <param name="id">Equipment group ID (PK)</param>
    /// <returns></returns>
    public async Task<MNTEquipmentGroup> GetEquipmentGroupById(PEContext ctx, long id, bool includeEquipment = false)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
			if (!includeEquipment)
      {
        return await ctx.MNTEquipmentGroups.Where(x => x.EquipmentGroupId.Equals(id)).SingleOrDefaultAsync();
      }
			else
      {
        return await ctx.MNTEquipmentGroups.Include(i => i.MNTEquipments).Where(x => x.EquipmentGroupId.Equals(id)).SingleOrDefaultAsync();
      }
      
    }

    /// <summary>
    /// Return Equipment Group record or null if not found
    /// </summary>
    /// <param name="ctx">DB context for EF</param>
    /// <param name="code">Equipment group short code</param>
    /// <param name="code">Equipment group short code</param>
    /// <returns></returns>
    public async Task<MNTEquipmentGroup> GetEquipmentGroupByCode(PEContext ctx, string code, long? id = null)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
			MNTEquipmentGroup grp;

			if (id == null)
      {
				return await ctx.MNTEquipmentGroups.Where(x => x.EquipmentGroupCode.ToLower().Equals(code.ToLower())).SingleOrDefaultAsync();
      }
			else //search with exclused id
      {
        return await ctx.MNTEquipmentGroups.Where(x => x.EquipmentGroupCode.ToLower().Equals(code.ToLower()) && x.EquipmentGroupId != id).SingleOrDefaultAsync();
      }
    }

    /// <summary>
    /// Created new equipment group and returns it. Returns null in case of
    /// </summary>
    /// <param name="ctx">DB context</param>
    /// <param name="grp">Equipment Group element</param>
    /// <returns></returns>
    public async Task CreateEquipmentGroupAsync(PEContext ctx, MNTEquipmentGroup grp)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      ctx.MNTEquipmentGroups.Add(grp);
    }

    /// <summary>
    /// Deletes equipment group
    /// </summary>
    /// <param name="ctx">DB context</param>
    /// <param name="item">MNTEquipmentGroup object representing record to be deleted from DB</param>
    /// <returns></returns>
    public async Task DeleteEquipmentGroup(PEContext ctx, MNTEquipmentGroup item)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }
      ctx.MNTEquipmentGroups.Remove(item);
    }


  }
}
