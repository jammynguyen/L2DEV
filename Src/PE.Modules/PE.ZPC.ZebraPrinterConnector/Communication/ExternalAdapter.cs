using PE.DTO.Internal.ZebraPrinter;
using PE.Interfaces.Lite;
using SMF.Module.Core;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.ZPC.ZebraPrinterConnector.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, IZebraPC
  {
    #region ctor

    public ExternalAdapter() : base( typeof(PE.Interfaces.Lite.IZebraPC)) { }

    #endregion

    public async Task<DCZebraResponse> RequestPDFForHMIAsync(DCZebraRequest dcZebraRequest)
    {
				return await HandleIncommingMethod(ExternalAdapterHandler.RequestPDFFromZebraWebServiceForHMIAsync, dcZebraRequest);	
    }
  }
}
