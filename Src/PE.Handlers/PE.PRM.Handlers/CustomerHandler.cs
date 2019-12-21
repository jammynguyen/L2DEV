using PE.DbEntity.Models;
using PE.DTO.Internal.Adapter;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PE.PRM.Handlers
{
  public sealed class CustomerHandler
  {

    /// <summary>
    /// Return customer or default customer if customer with param-name does't exist
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="customerName"></param>
    /// <returns></returns>
    public async Task<PRMCustomer> GetCustomerByNameAsync(PEContext ctx, string customerName, bool getDefault = false)
    {
      if (ctx == null) { ctx = new PEContext(); }

      PRMCustomer customer = await ctx.PRMCustomers.Where(x => x.CustomerName.ToLower().Equals(customerName.ToLower())).SingleOrDefaultAsync();

      if (customer == null && getDefault == true)
      {
        customer = await ctx.PRMCustomers.Where(x => x.IsDefault == true).SingleAsync();
      }

      return customer;
    }

    /// <summary>
    /// Return customer or create customer if customer with param-name does't exist
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="customerName"></param>
    /// <param name="backMsg">Optional to fill return information to L3</param>
    /// <returns></returns>
    public async Task<PRMCustomer> GetCustomerByNameOrCreateNewAsync(PEContext ctx, string customerName, DCWorkOrderStatus backMsg = null)
    {
      if (ctx == null) { ctx = new PEContext(); }

      PRMCustomer customer = await GetCustomerByNameAsync(ctx, customerName);

      if (customer == null)
      {

        customer = new PRMCustomer()
        {
          CustomerName = customerName,
        };

        if (backMsg != null)
        {
          backMsg.BackMessage += " PRMCustomer created;";
        }

        ctx.PRMCustomers.Add(customer);
      }

      return customer;
    }
  }
}
