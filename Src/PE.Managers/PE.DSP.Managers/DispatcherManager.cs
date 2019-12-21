using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.MVHistory;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Managers;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.DSP.Managers
{
  public class DispatcherManager : IDispatcherManager
  {
    private readonly IDispatcherManagerSendOffice _sendOffice;


    public DispatcherManager(IDispatcherManagerSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
    }

    public virtual async Task<DataContractBase> ProcessAssetEventAsync(DCMeasData message)
    {
      DataContractBase result = new DataContractBase();
      await Task.Delay(1);
      return result;
    }

    public virtual async Task<DataContractBase> ProcessScrapEventAsync(DCL1ScrapData message)
    {
      DataContractBase result = new DataContractBase();
      await Task.Delay(1);
      return result;
    }
  }
}
