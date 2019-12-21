using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.TCP.Managers
{
  public class ManagerRoot
  {
    #region handlers
    //protected readonly IWorkOrderHandler _workOrderHandler;
    #endregion

    #region ctor
    public ManagerRoot()
    {
      //using (IKernel kernel = new StandardKernel())
      //{
      //  kernel.Bind(x =>
      //  {
      //    x.From("PE.TCP.Handlers.dll") // Scans assembly
      //     .SelectAllClasses() // Retrieve all non-abstract classes
      //     .BindDefaultInterface(); // Binds the default interface to them;
      //  });

      //  //_customerHandler = kernel.Get<ICustomerHandler>();
      //}
    }
    #endregion
  }
}
