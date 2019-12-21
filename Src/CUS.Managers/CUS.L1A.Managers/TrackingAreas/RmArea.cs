using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUS.L1A.Handlers;
using CUS.L1A.Managers.AssetTypes;
using CUS.L1A.Managers.MillOrganization;

namespace CUS.L1A.Managers.TrackingAreas
{
  public class RmArea: MillAsset
  {
    #region properties
    private static AdvosolOpcHandler _opcHandler;
    #endregion
    #region ctor

    public RmArea(AdvosolOpcHandler opcHandler, string name) : base(name)
    {
      _opcHandler = opcHandler;
      SetupFeatures();
    }

    #endregion
    #region private methods

    private void SetupFeatures()
    {
      AddFeature(new MillFeature(101, OPCConstants.MvRmTmp, MaterialEventType.MaterialLeaves, "Temperature 1"));

      AddFeature(new MillFeature(1020, OPCConstants.TrgHeRm, MaterialEventType.MaterialEnters, "Rolling Start"));
      AddFeature(new MillFeature(103, OPCConstants.MvRmTorque, MaterialEventType.MaterialLeaves, "Torque"));
      AddFeature(new MillFeature(104, OPCConstants.MvRmSpeed, MaterialEventType.MaterialLeaves, "Speed"));

      AddFeature(new MillFeature(1023, OPCConstants.TrgTlRm, MaterialEventType.MaterialLeaves, "Rolling End"));
    }

    #endregion
  }
}
