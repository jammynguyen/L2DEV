using PE.DTO.Internal.ZebraPrinter;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PE.Interfaces.Lite
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IZebraPC : IBaseModule
  {
    [OperationContract]
		[FaultContract(typeof(ModuleMessage))]
		Task<DCZebraResponse> RequestPDFForHMIAsync(DCZebraRequest message);
  }
}
