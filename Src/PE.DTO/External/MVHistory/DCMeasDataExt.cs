using PE.DTO.External.Adapter;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using SMF.Module.Core.Interfaces.Telegram;
using PE.DTO.Internal.MVHistory;

namespace PE.DTO.External.MVHistory
{

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCMeasDataExt: BaseExternalTelegram
  {
    /// <summary>
    /// Common header telegram
    /// </summary>
    public DCHeaderExt Header { get; set; }

    /// <summary>
    /// Billet unique identification
    /// </summary>
    public uint BasId { get; set; }
    
    /// <summary>
    /// Pass number [1 - n]
    /// </summary>
    public ushort PassNumber { get; set; }

    /// <summary>
    /// Feature id - defined by PE
    /// </summary>
    public ushort BaseFeature { get; set; }

    /// <summary>
    /// 1 in case when measured value is valid
    /// default 0
    /// </summary>
    public ushort Valid { get; set; }

    /// <summary>
    /// Min value
    /// </summary>
    public float Min { get; set; }

    /// <summary>
    /// Avg value
    /// </summary>
    public float Avg { get; set; }

    /// <summary>
    /// Max value
    /// </summary>
    public float Max { get; set; }

    /// <summary>
    /// IsReversedPass
    /// </summary>
    public ushort IsReversed { get; set; }

    /// <summary>
    /// IsLast pass
    /// </summary>
    public ushort IsLastPass { get; set; }

    /// <summary>
    /// Event code defined in PE
    /// </summary>
    public ushort EventCode { get; set; }

    public override DataContractBase ToInternal(int? telId = null)
    {
      return new DCMeasData
      {
        BasId = this.BasId,
        PassNumber = Convert.ToInt32(this.PassNumber),
        IsReversed = Convert.ToBoolean(this.IsReversed),
        IsLastPass = Convert.ToBoolean(this.IsLastPass),
        EventCode = Convert.ToInt32(this.EventCode),
        Valid = Convert.ToInt32(this.Valid),
        Min = this.Min,
        Max = this.Max,
        Avg = this.Avg,
        //BaseFeature = Convert.ToInt32(this.BaseFeature), temp commented
        //DateTimeOfFirstSample = DateTime.Now,
        //NumberOfSamples = 0,
        //Samples = new System.Collections.Generic.List<DCSample>(),

      };

    }
    private static DateTime ConvertTableIntoDatetime(string dateTimeOfFirstSample)
    {
      //string temp = new string(dateTimeOfFirstSample);
      DateTime output = DateTime.ParseExact(dateTimeOfFirstSample, "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.InvariantCulture);

      return output;
    }
  }
}
