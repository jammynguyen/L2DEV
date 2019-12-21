using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advosol.EasyUA;
using Opc.Ua;

namespace CUS.L1A.Handlers
{
  public class AdvosolOpcHandler
  {
    #region members

    private static string _moduleName;
    private static UaClient _client;
    private static Session _session = null;

    //subscriptions
    static Subscription _subscription = null;


    #endregion
    #region ctor

    public AdvosolOpcHandler(string moduleName)
    {
      _moduleName = moduleName;
    }

    #endregion
    #region private methods

    #endregion
    #region public methods

    public async Task ConnectToOpcServer(OnStatusChange StatusChange)
    {
      //Task task = new Task(() => _opcHandler.ConnectToOpcServer(OPCStatusChange));
      //task.Start();
      //await task;

      UaClient.UaAppConfigFileAutoCreate = _moduleName;

      _client = new UaClient(null);
      _client.LoadConfiguration();

      _session = _client.CreateSession(OPCConstants.OpcServer, false, "ProcessExpert");
      _session.StatusChange += StatusChange;

      _session.Connect(null);

      _subscription = _session.AddSubscription("Subscription", 200);

      await Task.Delay(1);
    }
    public async Task AddSubscription(string nodeId, OnMonitoredItemNotification notificationMethod)
    {
      if (_subscription == null)
        return;

      //init node with 0 value
      object val = 0;
      Node tmpNode = _session.ReadNode(nodeId);
      WriteValue wval = _session.MakeWriteValue((VariableNode)tmpNode, val);             
      _session.WriteAttribute(wval);
			//

      MonitoredItem triggerScaleEnter = _subscription.AddItem(nodeId);
      triggerScaleEnter.Notification +=  notificationMethod;

      await Task.Delay(1);
    }



    public async Task ApplySubscription()
    {
      if (_subscription == null)
        return;

      _subscription.ApplyChanges(); // create in the server

      await Task.Delay(1);
    }

    public double ReadValue(string nodeId)
    {
      List<ReadValueId> nodesToRead = new List<ReadValueId>();

      ReadValueId rv = new ReadValueId();
      rv.NodeId = nodeId;
      rv.AttributeId = Attributes.Value;
      nodesToRead.Add(rv);

      List<DataValue> rslt = _session.Read(nodesToRead);

      DataValue value = rslt.FirstOrDefault();

			if(value!=null)
      {
        return Convert.ToDouble(value.Value);
      }
      return 0;
    }

    #endregion

  }
}
