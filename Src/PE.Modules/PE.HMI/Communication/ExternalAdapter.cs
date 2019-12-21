using SMF.HMI.Module;
using System.ServiceModel;
using PE.Interfaces;

namespace PE.HMI.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterHmi, IHmi
  {
    #region ctor

    public ExternalAdapter() : base( typeof(PE.Interfaces.IHmi)) { }

    #endregion
    #region interface


    #endregion
  }
}
