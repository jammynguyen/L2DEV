using PE.Interfaces.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using System.IO;
using PE.DTO.Internal.ZebraPrinter;
using Ninject.Parameters;
using PE.ZPC.Managers;

namespace PE.ZPC.ZebraPrinterConnector.Communication
{
  internal class ExternalAdapterHandler
  {
    private static readonly IZebraConnectionManager _zebraConnectionManager;


    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IZebraConnectionManager>().To<ZebraConnectionManager>();

        _zebraConnectionManager = kernel.Get<IZebraConnectionManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    internal static async Task<DCZebraResponse> RequestPDFFromZebraWebServiceForHMIAsync(DCZebraRequest dcZebraRequest)
    {
      return await _zebraConnectionManager.RequestPDFFromZebraWebServiceForHMIAsync(dcZebraRequest);
		}
    
  }
}
