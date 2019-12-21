using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.External.MVHistory;
using PE.Interfaces.SendOffice;
using PE.L1S.Handlers.MillSetup;

namespace PE.SIM.Managers.MillOrganiztion
{
  public class RawMaterial
  {
    #region members

    protected ISimulationSendOffice _sendOffice;

    #endregion
    #region properties

    public UInt32 BasId { get; private set; }
    public int CurrentTimeInAsset { get; private set; }
    public MillAssetConfig AssetConfig { get; set; }
    public DateTime StartTime { get; private set; }
    #endregion
    #region ctor

    public RawMaterial(UInt32 basId, MillAssetConfig assetConofig , ISimulationSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      BasId = basId;
      StartTime = DateTime.Now;
      UpdateAsset(assetConofig);
    }

    #endregion
    #region public methods

		public void UpdateTimeInAsset()
    {
      CurrentTimeInAsset++;
    }
    public void UpdateAsset(MillAssetConfig assetConofig)
    {
      if(AssetConfig!=null)
        SendFeaturesData(AssetConfig.GetTailLeavesFeatures());
  
      CurrentTimeInAsset=0;
      AssetConfig = assetConofig;

      if (assetConofig != null)
        SendFeaturesData(AssetConfig.GetHeadEntetrFeatures());
    }

    private void SendFeaturesData(List<MillFeatureConfig> features)
    {
			foreach(MillFeatureConfig f in features)
      {
        DCMeasDataExt data = GenerateNonSampledMeasurement(f, 1, BasId);
        _sendOffice.SendL1AggregatedMeasDataToAdapterAsync(data);
				//Console.WriteLine("")
      }

    }

    internal bool IsReadyToMoveToNextAsset()
    {
      return AssetConfig.TimeInAsset <= CurrentTimeInAsset;
    }

    public override string ToString()
    {
      string assetPrint = string.Format($"BAS_ID_{BasId.ToString().PadLeft(4)}. CurrentTimeInAsset: {CurrentTimeInAsset.ToString().PadLeft(3)} {AssetConfig.AssetName.PadLeft(26)}, P: {AssetConfig.MaxPasses}, T: {AssetConfig.TimeInAsset} {StartTime.ToLongTimeString()}");


      return assetPrint;
    }
    public DCMeasDataExt GenerateNonSampledMeasurement(MillFeatureConfig feature, int passNumber, uint basId)
    {
      DCMeasDataExt dcMeasurement = new DCMeasDataExt();

      {
        dcMeasurement.BasId = Convert.ToUInt16(basId);
        dcMeasurement.PassNumber = Convert.ToUInt16(passNumber);
        dcMeasurement.BaseFeature = Convert.ToUInt16(feature.FeatureID);
        dcMeasurement.Valid = Convert.ToUInt16(new Random().Next(0, 21) <= 1 ? (short)0 : (short)1);
        dcMeasurement.EventCode = Convert.ToUInt16(feature.FeatureCode);
        dcMeasurement.IsReversed = Convert.ToUInt16(passNumber % 2 == 0 ? 1 : 0);
        dcMeasurement.Header = new DTO.External.Adapter.DCHeaderExt()
        {
          Counter = 1,
          TimeStamp = DateTime.Now.ToString(),
          MessageId = 3001,
        };

        if (new Random().Next(0, 51) <= 25)
        {
          dcMeasurement.Min = (float)feature.MinValue;
          dcMeasurement.Max = (float)feature.MinValue;
          dcMeasurement.Avg = (float)feature.MinValue;
        }
        else
        {
          dcMeasurement.Min = (float)feature.MaxValue;
          dcMeasurement.Max = (float)feature.MaxValue;
          dcMeasurement.Avg = (float)feature.MaxValue;
        }
      }

      return dcMeasurement;
    }

    #endregion
  }
}
