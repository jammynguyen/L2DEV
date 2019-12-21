using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advosol.EasyUA;
using CUS.L1A.Handlers;
using CUS.L1A.Managers.AssetTypes;
using CUS.L1A.Managers.MillOrganization;
using Opc.Ua;
using SMF.Module.Notification;

namespace CUS.L1A.Managers.TrackingAreas
{
  public class FurnaceArea: FurnaceAsset
  {
    #region properties
    private static AdvosolOpcHandler _opcHandler;
    #endregion
    #region ctor

    public FurnaceArea(AdvosolOpcHandler opcHandler, string name) : base(name)
    {
      _opcHandler = opcHandler;
      SetupFeatures();
    }

    #endregion
    #region private methods

    private void SetupFeatures()
    {
      AddFeature(new MillFeature(20, OPCConstants.TrgCharge, MaterialEventType.MaterialEnters, "Charge"));
      AddFeature(new MillFeature(30, OPCConstants.TrgUncharge, MaterialEventType.MaterialLeaves, "Uncharge"));

      AddFeature(new MillFeature(40, OPCConstants.TrgDischarge, MaterialEventType.MaterialLeaves, "Discharge"));
      AddFeature(new MillFeature(50, OPCConstants.TrgUnDischarge, MaterialEventType.MaterialEnters, "Undischarge"));
    }

    #endregion
    #region public methods

    public void FurnaceCharge_Notification(MonitoredItem monitoredItem, IEncodeable notification)
    {
      try
      {
        MonitoredItemNotification dataChange = notification as MonitoredItemNotification;
        if (dataChange != null)
        {
          bool val = Convert.ToBoolean(dataChange.Value.Value); // the changed value of the subscribed MonitoredItem               
          NotificationController.Info("FCE charge event: {0}", val.ToString());

          if (val)
          {
            MoveMaterialFromPreviousArea();
          }
        }
      }
      catch (Exception ex) { NotificationController.Error(monitoredItem.DisplayName + ": " + ex.Message); }
    }


    public void FurnaceUncharge_Notification(MonitoredItem monitoredItem, IEncodeable notification)
    {
      try
      {
        MonitoredItemNotification dataChange = notification as MonitoredItemNotification;
        if (dataChange != null)
        {
          bool val = Convert.ToBoolean(dataChange.Value.Value); // the changed value of the subscribed MonitoredItem               
          NotificationController.Info("FCE uncharge event: {0}", val.ToString());

          if (val)
          {
            MoveMaterialBackToPreviousArea();
          }
        }
      }
      catch (Exception ex) { NotificationController.Error(monitoredItem.DisplayName + ": " + ex.Message); }
    }



    public void FurnaceDischarge_Notification(MonitoredItem monitoredItem, IEncodeable notification)
    {
      try
      {
        MonitoredItemNotification dataChange = notification as MonitoredItemNotification;
        if (dataChange != null)
        {
          bool val = Convert.ToBoolean(dataChange.Value.Value); // the changed value of the subscribed MonitoredItem               
          NotificationController.Info("FCE discharge event: {0}", val.ToString());

          if (val)
          {
            // TriggerRequestNewMaterial();
          }
        }
      }
      catch (Exception ex) { NotificationController.Error(monitoredItem.DisplayName + ": " + ex.Message); }
    }
    public void FurnaceUndischarge_Notification(MonitoredItem monitoredItem, IEncodeable notification)
    {
      try
      {
        MonitoredItemNotification dataChange = notification as MonitoredItemNotification;
        if (dataChange != null)
        {
          bool val = Convert.ToBoolean(dataChange.Value.Value); // the changed value of the subscribed MonitoredItem               
          NotificationController.Info("FCE undischarge event: {0}", val.ToString());

          if (val)
          {
            // TriggerRequestNewMaterial();
          }
        }
      }
      catch (Exception ex) { NotificationController.Error(monitoredItem.DisplayName + ": " + ex.Message); }
    }

    #endregion
  }
}
