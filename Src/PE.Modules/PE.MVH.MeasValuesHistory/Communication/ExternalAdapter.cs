using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.Schedule;
using PE.DTO.Internal.ProdManager;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.MVH.MeasValuesHistory.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, PE.Interfaces.Lite.IMVHistory
  {
    #region ctor
    public ExternalAdapter() : base(typeof(PE.Interfaces.Lite.IMVHistory)) { }
    #endregion

    #region L1 requests

    public async Task<DataContractBase> ProcessScrapMessageAsync(DCL1ScrapData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ProcessScrapMessageAsync, message);
    }

    public async Task<DataContractBase> ProcessCutMessageAsync(DCL1CutData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ProcessCutDataMessageAsync, message);
    }

    public async Task<DCPEBasId> ProcessDivisionMaterialMessageAsync(DCL1MaterialDivision message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ProcessDivisionMaterialMessageAsync, message);
    }

    public async Task<DCPEBasId> ProcessL1BaseIdRequestAsync(DCL1BasIdRequest message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ProcessL1BaseIdRequestAsync, message);
    }

    public async Task<DataContractBase> ProcessL1MeasurementAsync(DCMeasData message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ProcessL1MeasurementAsync, message);
    }
    public async Task<DataContractBase> ProcessL1SampleMeasurementAsync(DCMeasDataSample message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ProcessL1SampleMeasurementAsync, message);
    }

    #endregion

    #region HMI

    public async Task<DataContractBase> AssignMaterialsAsync(DCMaterialAssign dataToSend)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.AssignMaterialsAsync, dataToSend);
    }

    public async Task<DataContractBase> UnassignMaterialAsync(DCMaterialAssign dataToSend)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UnassignMaterialAsync, dataToSend);
    }
    public async Task<DataContractBase> MarkMaterialAsScrapAsync(DCMaterialMarkedAsScrap dataToSend)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.MarkMaterialAsScrapAsync, dataToSend);
    }

    #endregion

  }
}
