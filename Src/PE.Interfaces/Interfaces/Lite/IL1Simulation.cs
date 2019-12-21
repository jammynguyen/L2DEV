using PE.DTO.External.MVHistory;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PE.Interfaces.Lite
{

  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IL1Simulation: IBaseModule
  {
  }
}
