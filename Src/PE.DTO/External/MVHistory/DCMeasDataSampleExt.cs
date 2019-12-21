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
  public class DCMeasDataSampleExt : BaseExternalTelegram
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
    /// IsReversedPass
    /// </summary>
    public ushort IsReversed { get; set; }

    /// <summary>
    /// IsLast pass
    /// </summary>
    public ushort IsLastPass { get; set; }

    ///// <summary>
    ///// Feature id - defined by PE
    ///// </summary>
    //public ushort BaseFeature { get; set; }
    /// <summary>
    /// Event code defined in PE
    /// </summary>
    public ushort EventCode { get; set; }

    /// <summary>
    /// Date and time of first sample
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
    public string DateTimeOfFirstSample;

    /// <summary>
    /// Number of samples sent in Samples array
    /// </summary>
    public ushort NumberOfSamples { get; set; }

    /// <summary>
    /// Array of measured values. Max sixe (x ) has to be agreed with L1 and be used for all messages
    /// </summary>
    public DCSampleExt[] Samples { get; set; }

    public override DataContractBase ToInternal(int? telId = null)
    {
      return new DCMeasDataSample
      {
        BasId = this.BasId,
        PassNumber = Convert.ToInt32(this.PassNumber),
        IsReversed = Convert.ToBoolean(this.IsReversed),
        IsLastPass = Convert.ToBoolean(this.IsLastPass),
        EventCode = Convert.ToInt32(this.EventCode),
        DateTimeOfFirstSample = ConvertTableIntoDatetime(this.DateTimeOfFirstSample),
        NumberOfSamples = Convert.ToInt32(this.NumberOfSamples),

        Samples = this.Samples.Select(messageSample => new DCSample()
        {
          Value = messageSample.Value,
          HeadOffset = messageSample.HeadOffset,
          IsValid = messageSample.IsValid,
          TimeOffset = messageSample.TimeOffset
        }).ToList()

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
