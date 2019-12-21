using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUS.L1A.Handlers;
using SMF.Module.Notification;

namespace CUS.L1A.Managers.MillOrganization
{
  public enum MaterialEventType {MaterialEnters, MaterialLeaves, All}
  public class MillFeature
  {
    #region properties

		public UInt16 EventCode { get; private set; }
		public string FeatureName { get; private set; }
    public MeasuredValuesSet MeasuredValues { get; set; }
    public MaterialEventType MaterialEventType { get; private set; }
    public string NodeId { get; private set; }

    #endregion
    #region ctor

    public MillFeature(UInt16 eventCode, string nodeId, MaterialEventType materialEventType= MaterialEventType.MaterialLeaves,  string featureName ="NotDefined")
    {
      MeasuredValues = new MeasuredValuesSet();
      EventCode = eventCode;
      FeatureName = featureName;
      MaterialEventType = materialEventType;
      NodeId = nodeId;
    }

    internal void ReadFormServer(AdvosolOpcHandler opcHandler)
    {
      double value = opcHandler.ReadValue(NodeId);

      MeasuredValues.AddMeasuredValue(value, DateTime.Now);

      NotificationController.Info("Read OPC node: {0}, value: {1}", NodeId, value);
    }

    #endregion
  }
}
