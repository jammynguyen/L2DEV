using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.L1S.Handlers.MillSetup
{
  public class MillAssetConfig
  {
    #region properties

    public long AssetID { get; private set; }
    public string AssetName { get; private set; }
    public int MaxPasses { get; private set; }
    public int TimeInAsset { get; private set; }//per pass
    public int AssetSequence { get; private set; }
    public List<MillFeatureConfig> Features { get; private set; }
    public bool IsLast { get; set; }

    #endregion

    #region ctor

    public MillAssetConfig()
    {
      Features = new List<MillFeatureConfig>();
    }

    public MillAssetConfig(long assetId, string assetName, int maxPasses, int timeInAsset, int assetSequence, bool isLast)
    {
      AssetID = assetId;
      AssetName = assetName;
      MaxPasses = maxPasses;
      TimeInAsset = timeInAsset;
      AssetSequence = assetSequence;
      Features = new List<MillFeatureConfig>();
      IsLast = isLast;
    }

    public void AddFeature (MillFeatureConfig millFeatureConfig)
    {
      Features.Add(millFeatureConfig);
    }

    #endregion

    #region public methods
    public List<MillFeatureConfig> GetTailLeavesFeatures()
    {
      return Features.Where(x => x.Activation == FeatureActivation.Exit).ToList();
    }
    public List<MillFeatureConfig> GetHeadEntetrFeatures()
    {
      return Features.Where(x => x.Activation == FeatureActivation.Enter).ToList();
    }

    public override string ToString()
    {
			string assetPrint = string.Format($"{AssetSequence.ToString().PadLeft(3)}. [{AssetID.ToString().PadLeft(3)}] {AssetName.Replace(Environment.NewLine, "").PadLeft(26)}, P: {MaxPasses}, T: {TimeInAsset}");

      //assetPrint += "\n";
      //foreach(MillFeatureConfig f in Features)
      //   {
      //     assetPrint += "                                        -  "+f.ToString()+"\n";
      //   }

      return assetPrint;
    }

    #endregion
  }
}
