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
  public class L1TrackingManager : IL1TrackingManager
  {
    #region tracking areas

    private ScaleArea _scaleArea; 
    private FurnaceArea _furnaceArea;
    private RmArea _rmArea;

    #endregion

    #region members

    private static IL1TrackingSendOffice _sendOffice;

    #endregion
    #region handlers

    private static AdvosolOpcHandler _opcHandler;

    #endregion
    #region ctor
    public L1TrackingManager(IL1TrackingSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _opcHandler = new AdvosolOpcHandler(ModuleController.ModuleName);

      SetupTrackingAreas();

    }
    #endregion
    #region private methods

		private void SetupTrackingAreas()
    {
      _scaleArea = new ScaleArea(_opcHandler,"Scale");
      _furnaceArea = new FurnaceArea(_opcHandler,"Furnace");
      _rmArea = new RmArea(_opcHandler,"Roughing Mill");

      _scaleArea.SetNextAsset(_furnaceArea);
      _furnaceArea.SetPreviousAsset(_scaleArea);
      _furnaceArea.SetNextAsset(_rmArea);
      _rmArea.SetPreviousAsset(_furnaceArea);

      _scaleArea.OnMaterialIntroduction += _scaleArea_OnMaterialIntroduction;
      _scaleArea.OnMaterialLeave += _scaleArea_OnMaterialLeave;

      _furnaceArea.OnMaterialEnter += _furnaceArea_OnMaterialEnter;
      _furnaceArea.OnMaterialLeave += _furnaceArea_OnMaterialLeave;
    }



    #region events

    #region scale
    private void _scaleArea_OnMaterialIntroduction(object sender, MaterialStateEventArgs e)
    {
      try
      {
        NotificationController.Info("Request material basId from L2");
        Task<SendOfficeResult<DCPEBasIdExt>> task = _sendOffice.SendL1MaterialIdRequestToAdapterAsync(new DCL1BasIdRequestExt());
        task.Wait();
        SendOfficeResult<DCPEBasIdExt> result = task.Result;

        if (result.OperationSuccess)
        {
          _scaleArea.IntroduceMaterial(result.DataConctract.BasId);
        }
        else
          NotificationController.Error("Error during BasId request");
      }
      catch
      {
        NotificationController.Error("Error during BasId request");
      }
    }
    private void _scaleArea_OnMaterialLeave(object sender, MaterialStateEventArgs e)
    {
      try
      {
				if(e.Material==null|| e.MillAsset==null)
        {
          return;
        }
        NotificationController.Info("Material: {0} is leaving area {1}",e.Material, e.MillAsset.Name);


        foreach (MillFeature f in e.Material.GetPlaceLeaveFeatures())
        {
          SendMeasurements(f, e.Material);
        }
      }
      catch
      {
        NotificationController.Error("Error during sending measured values");
      }
    }

    private void SendMeasurements(MillFeature millFeature, MillMaterial material)
    {
      DCMeasDataExt measData = new DCMeasDataExt();
      measData.EventCode = millFeature.EventCode;
      measData.BasId = material.BasId;
      measData.IsLastPass = 1;
      measData.IsReversed = 0;
      measData.PassNumber = 1;
      measData.Valid = 1;
      measData.Min = Convert.ToSingle(millFeature.MeasuredValues.GetMinValue());
      measData.Max = Convert.ToSingle(millFeature.MeasuredValues.GetMaxValue());
      measData.Avg = Convert.ToSingle(millFeature.MeasuredValues.GetAvgValue());

      Task<SendOfficeResult<DCDefaultExt>> task = _sendOffice.SendL1AggregatedMeasDataToAdapterAsync(measData);
      task.Wait();
      SendOfficeResult<DCDefaultExt> result = task.Result;

      if (result.OperationSuccess)
      {
        NotificationController.Info("Measurement: {0} sent successfully", millFeature.FeatureName);
      }
      else
        NotificationController.Error("Error during sending measurement: {0}", millFeature.FeatureName);
    }

    #endregion

    #region furnace
    private void SendMeasurementBit(MillFeature millFeature, MillMaterial material)
    {
      DCMeasDataExt measData = new DCMeasDataExt();
      measData.EventCode = millFeature.EventCode;
      measData.BasId = material.BasId;
      measData.IsLastPass = 1;
      measData.IsReversed = 0;
      measData.PassNumber = 1;
      measData.Valid = 1;
      measData.Min = 1;
      measData.Max = 1;
      measData.Avg = 1;

      Task<SendOfficeResult<DCDefaultExt>> task = _sendOffice.SendL1AggregatedMeasDataToAdapterAsync(measData);
      task.Wait();
      SendOfficeResult<DCDefaultExt> result = task.Result;

      if (result.OperationSuccess)
      {
        NotificationController.Info("Measurement: {0} sent successfully", millFeature.FeatureName);
      }
      else
        NotificationController.Error("Error during sending measurement: {0}", millFeature.FeatureName);
    }
    private void _furnaceArea_OnMaterialLeave(object sender, MaterialStateEventArgs e)
    {
      try
      {
        if (e.Material == null || e.MillAsset == null)
        {
          return;
        }
        NotificationController.Info("Material: {0} is leaving area {1}", e.Material, e.MillAsset.Name);

				if(e.Material.LastDischargeTime==DateTime.MinValue && e.Material.LastChargeTime == DateTime.MinValue)
        {
          SendMeasurementBit(e.Material.Features.Where(x=>x.NodeId == OPCConstants.TrgUncharge).FirstOrDefault(), e.Material);
          //uncharge
        }
        if (e.Material.LastDischargeTime != DateTime.MinValue && e.Material.LastChargeTime != DateTime.MinValue)
        {
          SendMeasurementBit(e.Material.Features.Where(x => x.NodeId == OPCConstants.TrgDischarge).FirstOrDefault(), e.Material);
          //discharge
        }
      }
      catch
      {
        NotificationController.Error("Error during processing material leave");
      }
    }

    private void _furnaceArea_OnMaterialEnter(object sender, MaterialStateEventArgs e)
    {
      try
      {
        if (e.Material == null || e.MillAsset == null)
        {
          return;
        }
        NotificationController.Info("Material: {0} is entering area {1}", e.Material, e.MillAsset.Name);

        if (e.Material.LastDischargeTime == DateTime.MinValue && e.Material.LastChargeTime != DateTime.MinValue && !e.Material.Processed)
        {
          SendMeasurementBit(e.Material.Features.Where(x => x.NodeId == OPCConstants.TrgCharge).FirstOrDefault(), e.Material);
          //charge
        }
        if (e.Material.LastDischargeTime == DateTime.MinValue && e.Material.LastChargeTime != DateTime.MinValue && e.Material.Processed)
        {
          SendMeasurementBit(e.Material.Features.Where(x => x.NodeId == OPCConstants.TrgUnDischarge).FirstOrDefault(), e.Material);
          //undischarge
        }
      }
      catch
      {
        NotificationController.Error("Error during processing material enter");
      }
    }
    #endregion



    #region opc connection

    private void OPCStatusChange(Session session, StatusCheckEventArgs e)
    {
      NotificationController.Info("OPC client Session: " + e.CurrentState.ToString());
    }

    #endregion

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
        NotificationController.Error("OPC - connection failed with server: {0} ", OPCConstants.OpcServer);
      }
    }

    public async Task InitCommunicationAsync()
    {
      //scale
      await AddOpcSubscription(OPCConstants.TrgScaleEnter, _scaleArea.ScaleEnter_Notification);
      await AddOpcSubscription(OPCConstants.TrgScaleMeasReady, _scaleArea.ScaleMeasurementsReady_Notification);

      //fce
      await AddOpcSubscription(OPCConstants.TrgCharge, _furnaceArea.FurnaceCharge_Notification);
      await AddOpcSubscription(OPCConstants.TrgUncharge, _furnaceArea.FurnaceUncharge_Notification);
      await AddOpcSubscription(OPCConstants.TrgDischarge, _furnaceArea.FurnaceDischarge_Notification);
      await AddOpcSubscription(OPCConstants.TrgUncharge, _furnaceArea.FurnaceUndischarge_Notification);



      await _opcHandler.ApplySubscription();
    }

    public async Task PrintState()
    {
      _scaleArea.PrintState();
      await Task.Delay(1);
    }


    #endregion
  }
}
