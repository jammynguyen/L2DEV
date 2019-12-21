using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.External.MVHistory;
using PE.DTO.Internal.TcpProxy.Configuration;
using PE.TCP.Managers;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.TCP.TcpProxy.Communication
{
  internal class ExternalAdapterHandler
  {
    internal static DataContractBase ProcessPeBaseIdResponse(DCPEBasIdExt message)
    {
      DataContractBase ret = new DataContractBase();
      ModuleController.InitDataContract(ret);

      TcpCommunicationSendersManager _managerInstance = new TcpCommunicationSendersManager();
      _managerInstance.SendBaseIdResponse(message);

      return ret;
    }

    internal static DataContractBase ProcessForceSendTelegramSetupData(DCTelegramStructureByteData message)
    {
      throw new NotImplementedException();

    }
  }
}
