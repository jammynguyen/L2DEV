using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUS.L1A.Handlers;

namespace CUS.L1A.Managers.MillOrganization
{
  public class MillMaterial
  {
    #region properties

    public uint BasId { get; private set; }
    public DateTime IntroductionTime { get; private set; }
    public DateTime LastChargeTime { get; private set; }
    public DateTime LastDischargeTime { get; private set; }
    public List<MillFeature> Features { get; private set; }



    public bool Processed { get; private set; }

    #endregion
    #region ctor

    public MillMaterial(uint basId)
    {
      BasId = basId;
      IntroductionTime = DateTime.Now;
      Features = new List<MillFeature>();
      Processed = false;
    }
    public MillMaterial(MillMaterial material, bool processed=false, List<MillFeature> features =null)
    {
      BasId = material.BasId;
      IntroductionTime = material.IntroductionTime;
      LastChargeTime = material.LastChargeTime;
      LastDischargeTime = material.LastDischargeTime;
      Features = features;
      Processed = processed;
    }

    #endregion
    #region protected methods

    protected void Charge()
    {
      LastChargeTime = DateTime.Now;
    }
    protected void Discharge()
    {
      LastDischargeTime = DateTime.Now;
    }

    #endregion
    #region public methods

    public override string ToString()
    {
      return string.Format("Id: {0}, Introduced on: {1}, charged on: {2}, discharged on: {3}", BasId, IntroductionTime.ToShortTimeString(), LastChargeTime.ToShortTimeString(), LastDischargeTime.ToShortTimeString());
    }

    internal void ReadAllMMv(AdvosolOpcHandler opcHandler, MaterialEventType eventType)
    {
      foreach (MillFeature f in Features)
      {
        if (f.MaterialEventType == eventType || eventType == MaterialEventType.All)
          f.ReadFormServer(opcHandler);
      }
    }

    internal void AddFeatures(List<MillFeature> list)
    {
      Features.AddRange(list.Copy());
    }

    internal void RemoveFeatures(List<MillFeature> features)
    {
      Features.Clear();
    }

    internal List<MillFeature> GetPlaceLeaveFeatures()
    {
      return Features.Where(x => x.MaterialEventType == MaterialEventType.MaterialLeaves).ToList();
    }
    internal List<MillFeature> GetPlaceEnterFeatures()
    {
      return Features.Where(x => x.MaterialEventType == MaterialEventType.MaterialEnters).ToList();
    }
    internal void PerformMaterialCharge()
    {
      LastChargeTime = DateTime.Now;
    }
    internal void PerformMaterialUncharge()
    {
      LastChargeTime = DateTime.MinValue;
    }
    internal void PerformMaterialDischarge()
    {
      LastDischargeTime = DateTime.Now;
    }
    internal void PerformMaterialUndischarge()
    {
      LastDischargeTime = DateTime.MinValue;
    }

    internal void MarkAsProcessed(bool processing=true)
    {
      Processed = processing;
    }

    #endregion
  }
}
