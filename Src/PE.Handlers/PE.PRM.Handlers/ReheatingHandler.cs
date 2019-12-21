using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.DTO.Internal.Adapter;

namespace PE.PRM.Handlers
{
  public sealed class ReheatingHandler
  {
    /// <summary>
    /// Return reheating group id or default id if reheating group with param-name does't exist (optional )
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="customerName"></param>
    /// <returns></returns>
    public async Task<PRMReheatingGroup> GetReheatingByNameAsync(PEContext ctx, string heatingGroup, bool getDefault = false)
    {
      if (ctx == null) { ctx = new PEContext(); }

      PRMReheatingGroup reheatingGroup = await ctx.PRMReheatingGroups.Where(x => x.ReheatingGroupName.ToLower().Equals(heatingGroup.ToLower())).SingleOrDefaultAsync();

      if (reheatingGroup == null && getDefault == true)
      {
        reheatingGroup = await ctx.PRMReheatingGroups.Where(x => x.IsDefault == true).SingleAsync();
      }

      return reheatingGroup;
    }

    /// <summary>
    /// Form processing Work Order Message ( L3 transfer table ) get reheating group
    /// If RG with given name not exist return default and fill backMsg
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="heatingGroup"></param>
    /// <param name="backMsg"></param>
    /// <returns></returns>
    public async Task<PRMReheatingGroup> GetReheatingByNameForWorkOrderProcessingAsync(PEContext ctx, string heatingGroup, DCWorkOrderStatus backMsg)
    {
      PRMReheatingGroup reheatingGroup = await GetReheatingByNameAsync(ctx, heatingGroup);

      if (reheatingGroup == null)
      {
        reheatingGroup = await ctx.PRMReheatingGroups.Where(x => x.IsDefault == true).SingleAsync();
        backMsg.BackMessage += " PRMReheatingGroups - default;";
      }

      return reheatingGroup;
    }
  }
}
