using PE.DTO.Internal.ZebraPrinter;
using PE.Interfaces.Managers;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Interfaces.Managers
{
  public interface  IZebraConnectionManager : IManagerBase
  {
		[FaultContract(typeof(ModuleMessage))]
		Task<DCZebraResponse> RequestPDFFromZebraWebServiceForHMIAsync(DCZebraRequest dcZebraRequest);
  }
}
