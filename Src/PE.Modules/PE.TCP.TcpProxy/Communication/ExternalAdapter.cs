using PE.DTO.External.MVHistory;
using PE.DTO.Internal.TcpProxy.Configuration;
using PE.Interfaces.Interfaces.Lite;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.TCP.TcpProxy.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, ITcpProxy
  {
    #region ctor
    public ExternalAdapter(string moduleName) : base(moduleName, typeof(ITcpProxy)) { }
    #endregion

    public async Task<DataContractBase> ProcessPEResponseWithBaseMaterialId(DCPEBasIdExt message)
    {
      return await HandleExternalMethod(message, () => ExternalAdapterHandler.ProcessPeBaseIdResponse(message));
    }

    public async Task<DataContractBase> SendTelegramSetupData(DCTelegramStructureByteData message)
    {
      return await HandleExternalMethod(message, () => ExternalAdapterHandler.ProcessForceSendTelegramSetupData(message));
    }
  }
}
