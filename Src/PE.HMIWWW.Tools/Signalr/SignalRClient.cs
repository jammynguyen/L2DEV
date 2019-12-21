using SMF.HMIWWW.SignalR;

namespace PE.HMIWWW.Core.Signalr
{
  public static class SignalRClient
  {
    public static async void Init()
    {
      await BaseSignalRClient.Init();
    }
    #region Sent methods

    //static public async Task System2HmiNotification(string userId, SignalRNotification telegram)
    //{
    //  if (SignalrOk())
    //  {
    //    BaseSignalRClient.Logger.Info("Sending System2HmiNotification to HMI Server");

    //    try
    //    {
    //      await BaseSignalRClient.HubProxy.Invoke("System2HmiNotification", userId, telegram);
    //    }
    //    catch (Exception ex)
    //    {
    //      LogHelper.LogException(BaseSignalRClient.Logger, ex, "Error during sending: System2HmiNotification (signalR)");
    //    }
    //  }
    //}

    #endregion
  }
}
