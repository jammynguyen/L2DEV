using SMF.HMI.SignalR;

namespace PE.HMIWWW.Core.Signalr
{
  public static class SignalRClient
  {
    #region members

    #endregion
    #region ctor

    static SignalRClient() {}
    public static async void Init()
    {
      await BaseSignalRClient.Init();
    }

    #endregion
    #region Sent methods

    //your methods here

    #endregion
  }
}
