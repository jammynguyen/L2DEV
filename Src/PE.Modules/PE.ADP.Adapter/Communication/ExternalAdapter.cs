using PE.DTO.External;
using PE.DTO.External.DBAdapter;
using PE.DTO.External.MVHistory;
using PE.DTO.External.Setup;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.DBAdapter;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.Setup;
using PE.DTO.Internal.TcpProxy;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Core.Interfaces.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.ADP.Adapter.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, Interfaces.Lite.IAdapter
  {
    #region ctor
    public ExternalAdapter() : base(typeof(PE.Interfaces.Lite.IAdapter)) { }
    #endregion
    #region L3 incomming messages

    public async Task<DCWorkOrderStatusExt> ExternalProccesWorkOrderMessageAsync(DCL3L2WorkOrderDefinitionExt message)
    {
      DCL3L2WorkOrderDefinition internalDc = message.ToInternal() as DCL3L2WorkOrderDefinition;

      DCWorkOrderStatus returnData = await HandleIncommingMethod(ExternalAdapterHandler.ExternalProccesWorkOrderMessageAsync, internalDc);

      DCWorkOrderStatusExt returnExtData = new DCWorkOrderStatusExt();
      returnExtData.ToExternal(returnData);

      return returnExtData;
    }

    #endregion
    #region L1 incomming messages

    public async Task<DCPEBasIdExt> L1BaseIdRequestAsync(DCL1BasIdRequestExt message)
    {
      DCL1BasIdRequest internalDc = message.ToInternal() as DCL1BasIdRequest;

      DCPEBasId returnData = await HandleIncommingMethod(ExternalAdapterHandler.ExternalProcessL1BaseIdRequestAsync, internalDc);

      DCPEBasIdExt returnExtData = new DCPEBasIdExt();
      returnExtData.ToExternal(returnData);

      return returnExtData;
    }
    public async Task<DCDefaultExt> L1CutMessageAsync(DCL1CutDataExt message)
    {
      DCL1CutData internalDc = message.ToInternal() as DCL1CutData;

      DataContractBase returnData = await HandleIncommingMethod(ExternalAdapterHandler.ExternalProcessL1CutMessageAsync, internalDc);

      DCDefaultExt returnExtData = new DCDefaultExt();

      return returnExtData;
    }
    public async Task<DCPEBasIdExt> L1DivisionMessageAsync(DCL1MaterialDivisionExt message)
    {
      DCL1MaterialDivision internalDc = message.ToInternal() as DCL1MaterialDivision;

      DCPEBasId returnData = await HandleIncommingMethod(ExternalAdapterHandler.ExternalProcessL1DivisionMessageAsync, internalDc);

      DCPEBasIdExt returnExtData = new DCPEBasIdExt();
      returnExtData.ToExternal(returnData);

      return returnExtData;
    }
    public async Task<DCDefaultExt> L1ScrapMessageAsync(DCL1ScrapDataExt message)
    {
      DCL1ScrapData internalDc = message.ToInternal() as DCL1ScrapData;

      DataContractBase returnData = await HandleIncommingMethod(ExternalAdapterHandler.ExternalProcessL1ScrapMessageAsync, internalDc);

      DCDefaultExt returnExtData = new DCDefaultExt();

      return returnExtData;
    }
    public async Task<DCDefaultExt> L1MeasDataMessageAsync(DCMeasDataExt message)
    {
      DCMeasData internalDc = message.ToInternal() as DCMeasData;

      DataContractBase returnData = await HandleIncommingMethod(ExternalAdapterHandler.ExternalProcessL1MeasDataMessageAsync, internalDc);

      DCDefaultExt returnExtData = new DCDefaultExt();

      return returnExtData;
    }
    public async Task<DCDefaultExt> L1SampleMeasMessageAsync(DCMeasDataSampleExt message)
    {
      DCMeasDataSample internalDc = message.ToInternal() as DCMeasDataSample;

      DataContractBase returnData = await HandleIncommingMethod(ExternalAdapterHandler.ExternalProcessL1SampleMeasMessageAsync, internalDc);

      DCDefaultExt returnExtData = new DCDefaultExt();

      return returnExtData;
    }

    #endregion
    #region L1 outgoing messages

    #endregion
    #region Setup messages


    public async Task<DataContractBase> SendSetupDataAsync(DCCommonSetupStructure message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ExternalSendTelegramSetupDataAsync, message);
    }

    public async Task<DataContractBase> SendSetupDataRequestAsync(DCCommonSetupStructure message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.ExternalSendSetupDataRequestAsync, message);
    }

    public async Task<DataContractBase> L1SetupUpdateMessageAsync(DCCommonSetupStructure message)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.SendSetupUpdateMessageAsync, message);
    }

    #endregion
  }
}
