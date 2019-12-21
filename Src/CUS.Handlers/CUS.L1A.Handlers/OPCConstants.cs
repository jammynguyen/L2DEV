using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUS.L1A.Handlers
{
  public static class OPCConstants
  {
    public static readonly string OpcServer = "opc.tcp://localhost:62849/Advosol/uaPLUSsim";

    //-----CONSUMPTION---------------------------------------------------------------------
    public static readonly string ConsumptionGas = "ns=2;s=SimulatedData.Step";
    public static readonly string ConsumptionElectricity = "ns=2;s=SimulatedData.Random";


    //-----SCALE---------------------------------------------------------------------------
    public static readonly string TrgScaleEnter = "ns=2;s=Static.Simple Types.Boolean";
    public static readonly string TrgScaleMeasReady = "ns=2;s=Static.Simple Types.Byte";
    public static readonly string MvScaleWeight = "ns=2;s=Static.Simple Types.Int";
		public static readonly string MvScaleLength = "ns=2;s=Static.Simple Types.Double";

    //-----FCE ENTRY-----------------------------------------------------------------------
    public static readonly string TrgCharge = "ns=2;s=Static.Simple Types.UShort";
    public static readonly string TrgUncharge = "ns=2;s=Static.Simple Types.SByte";

    //-----FCE EXIT------------------------------------------------------------------------
    public static readonly string TrgDischarge = "ns=2;s=Static.Simple Types.Short";
    public static readonly string TrgUnDischarge = "ns=2;s=Static.Simple Types.UInt";

    //-----RM------------------------------------------------------------------------------
    public static readonly string TrgHeRm = "ns=2;s=Static.Simple Types.Boolean";
    public static readonly string TrgTlRm = "ns=2;s=Static.Simple Types.Boolean";
    public static readonly string MvRmTmp = "ns=2;s=Static.Simple Types.Double";
    public static readonly string MvRmTorque = "ns=2;s=Static.Simple Types.Double";
    public static readonly string MvRmSpeed = "ns=2;s=Static.Simple Types.Double";
  }
}
