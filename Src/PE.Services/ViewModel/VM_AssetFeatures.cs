using System.Collections.Generic;

public class VM_AssetFeatures
{
  public long id { get; set; }
  public string Name { get; set; }
  public string AssetDescription { get; set; }
  public short? OrderSequence { get; set; }
  public List<VM_AssetFeatures> hasChildren { get; set; } = new List<VM_AssetFeatures>();
  public List<FeatureVM> Features { get; set; }
}

public class FeatureVM
{
  public long FeatureId { get; set; }
  public string FeatureName { get; set; }
  public string FeatureDescription { get; set; }
}