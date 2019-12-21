using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Parameters;
using PE.DBA.DataBaseAdapter.Module;
using PE.DBA.Managers;
using PE.DTO.External.DBAdapter;
using PE.DTO.Internal.MVHistory;
using PE.Interfaces.Managers;
using SMF.Core.DC;

namespace PE.DBA.DataBaseAdapter.Communication
{
  internal class ExternalAdapterHandler
  {
    private static readonly IL3DBCommunicationManager _L3DBCommunicationManager;


    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IL3DBCommunicationManager>().To<L3DBCommunicationManager>();
        _L3DBCommunicationManager = kernel.Get<IL3DBCommunicationManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    //internal static async Task<DataContractBase> UpdateTransferTableCommStatus(DCWorkOrderStatusExt dataToSend)
    //{
    //  return await _L3DBCommunicationManager.UpdateTransferTableCommStatusesAsync(dataToSend);
    //}
  }
}
