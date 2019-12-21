using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advosol.EasyUA;
using CUS.L1A.Handlers;
using CUS.L1A.Managers.MillOrganization;
using CUS.L1A.Managers.TrackingAreas;
using Opc.Ua;
using PE.DTO.External;
using PE.DTO.External.MVHistory;
using PE.Interfaces.Interfaces.Managers.Custom;
using PE.Interfaces.Interfaces.SendOffice.Custom;
using PE.L1S.Handlers;
using SMF.Module.Core;
using SMF.Module.Notification;

namespace CUS.L1A.Managers
{
  public class L1ConsumptionManager : IL1ConsumptionManager
  {
    #region members

    private static IL1ConsumptionSendOffice _sendOffice;

    #endregion
    #region handlers

    private static AdvosolOpcHandler _opcHandler;

    #endregion
    #region ctor
    public L1ConsumptionManager(IL1ConsumptionSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _opcHandler = new AdvosolOpcHandler(ModuleController.ModuleName);


    }
    #endregion
    #region private methods

    private async Task SendConsumptionMeasurements(ushort eventCode, double value)
    {
      DCMeasDataExt measData = new DCMeasDataExt();
      measData.EventCode = eventCode;
      measData.BasId = 0;
      measData.IsLastPass = 1;
      measData.IsReversed = 0;
      measData.PassNumber = 1;
      measData.Valid = 1;
      measData.Min = Convert.ToSingle(value);
      measData.Max = Convert.ToSingle(value);
      measData.Avg = Convert.ToSingle(value);

      SendOfficeResult<DCDefaultExt> result = await _sendOffice.SendL1AggregatedMeasDataToAdapterAsync(measData);

      if (result.OperationSuccess)
      {
        NotificationController.Info("Measurement id: {0} sent successfully", eventCode);
      }
      else
        NotificationController.Error("Error during sending measurement id: {0}", eventCode);
    }

    #region opc connection

    private void OPCStatusChange(Session session, StatusCheckEventArgs e)
    {
      NotificationController.Info("OPC client Session: {0} - Consumption", e.CurrentState.ToString());
    }

    #endregion

    private async Task AddOpcSubscription(string nodeId, OnMonitoredItemNotification notificationMethod)
    {
      try
      {
        await _opcHandler.AddSubscription(nodeId, notificationMethod);
      }
      catch
      {
        NotificationController.Error("OPC - error during adding subscription for node: {0} ", nodeId);
      }
    }


    #endregion
    #region interface

    public async Task ConnectToOpcServerAsync()
    {
			try
      {
        await _opcHandler.ConnectToOpcServer(OPCStatusChange);
      }
			catch
      {
        NotificationController.Error("OPC - connection failed with server: {0} (consumption)", OPCConstants.OpcServer);
      }
    }

    public async Task InitCommunicationAsync()
    {
      await Task.Delay(1);
    }

    public async Task ReadMeasurements()
    {
      double electricity = _opcHandler.ReadValue(OPCConstants.ConsumptionElectricity);
      double gas = _opcHandler.ReadValue(OPCConstants.ConsumptionGas);

      NotificationController.Info("Consumption: Gas: {0}, Electricity: {1}", gas, electricity);

      await SendConsumptionMeasurements(92, gas);
      await SendConsumptionMeasurements(91, electricity);

      await Task.Delay(1);
    }


    #endregion
  }
}
