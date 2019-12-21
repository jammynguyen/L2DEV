using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.L1S.Handlers.MillSetup
{
  public enum FeatureActivation { Enter, Exit }

  public class MillFeatureConfig
  {
    #region properties

    public long FeatureID { get; private set; }
    public string FeatureName { get; private set; }
    public double? MinValue { get; private set; }
    public double? MaxValue { get; private set; }
    public FeatureActivation Activation { get; private set; }
    public bool IsAggregated { get; private set; }
    public int FeatureCode { get; private set; }

    #endregion

    public MillFeatureConfig(long featureID, string featureName, double? minValue, double? maxValue, FeatureActivation featureActivation, bool isAggregated, int featureCode)
    {
      FeatureID = featureID;
      FeatureName = featureName;
      MinValue = minValue;
      MaxValue = maxValue;
      Activation = featureActivation;
      IsAggregated = isAggregated;
      FeatureCode = featureCode;
    }

    public override string ToString()
    {
      string featurePrint = string.Format($"{FeatureName.Replace(Environment.NewLine, "").PadLeft(24)}, Val:[ {MinValue} to {MaxValue}], T: {Activation}");


      return featurePrint;
    }
  }
}
