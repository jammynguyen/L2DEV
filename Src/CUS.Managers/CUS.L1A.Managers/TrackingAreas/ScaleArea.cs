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
  /// <summary>
  /// Needed OPC signals:
  /// 1 material booking request
  /// 2 weight
  /// 3 length
  /// 4 tail leaves
  /// </summary>
  public class ScaleArea : MaterialIntroductionAsset
  {
    #region properties

    private static AdvosolOpcHandler _opcHandler;

    #endregion
    #region ctor

    public ScaleArea(AdvosolOpcHandler opcHandler, string name) : base(name)
    {
      _opcHandler = opcHandler;
      SetupFeatures();
    }

    #endregion
    #region private methods

    private void SetupFeatures()
    {
      AddFeature(new MillFeature(10, OPCConstants.MvScaleWeight, MaterialEventType.MaterialLeaves, "Weight"));
      AddFeature(new MillFeature(15, OPCConstants.MvScaleLength, MaterialEventType.MaterialLeaves, "Length"));
    }


    #endregion

    #region public methods



    public void ScaleEnter_Notification(MonitoredItem monitoredItem, IEncodeable notification)
    {
      NotificationController.Info("OPC Trigger 1");
      try
      {
        MonitoredItemNotification dataChange = notification as MonitoredItemNotification;
        if (dataChange != null)
        {
          bool val = Convert.ToBoolean(dataChange.Value.Value); // the changed value of the subscribed MonitoredItem               
          NotificationController.Info("OPC Trigger - scale Enter: {0}", val.ToString());

          if (val)
          {
            TriggerRequestNewMaterial();
          }
        }
      }
      catch (Exception ex) { NotificationController.Error(monitoredItem.DisplayName + ": " + ex.Message); }
    }
    public void ScaleMeasurementsReady_Notification(MonitoredItem monitoredItem, IEncodeable notification)
    {

      NotificationController.Info("OPC Trigger 2");
      try
      {
        MonitoredItemNotification dataChange = notification as MonitoredItemNotification;
        if (dataChange != null)
        {
          bool val = Convert.ToBoolean(dataChange.Value.Value); // the changed value of the subscribed MonitoredItem               
          NotificationController.Info("OPC Trigger - scale measurements ready: {0}", val.ToString());

          if (val)
          {
            MaterialLeavesArea(_opcHandler);
          }
        }
      }
      catch (Exception ex) { NotificationController.Error(monitoredItem.DisplayName + ": " + ex.Message); }
    }



    #endregion

  }
}
