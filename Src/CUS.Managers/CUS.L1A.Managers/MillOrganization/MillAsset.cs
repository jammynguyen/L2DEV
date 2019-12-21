using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUS.L1A.Handlers;
using SMF.Module.Notification;

namespace CUS.L1A.Managers.MillOrganization
{
  public class MaterialStateEventArgs : EventArgs
  {
    public MillAsset MillAsset { get; set; }
    public MillMaterial Material { get; set; }
    public MaterialStateEventArgs(MillAsset asset, MillMaterial material)
    {
      MillAsset = asset;
      Material = material;
    }
  }
  

  public class MillAsset
  {
    public event EventHandler<MaterialStateEventArgs> OnMaterialEnter;
    public event EventHandler<MaterialStateEventArgs> OnMaterialLeave;

    #region properties	

    public MillAsset PreviousAsset { get; private set; }
		public MillAsset NextAsset { get; private set; }
		public List<MillFeature> Features { get; private set; }
		public MillMaterialSet MillMaterials { get; private set; }
    public string Name { get; private set; }

    #endregion

    #region ctor

    public MillAsset(string name)
    {
      PreviousAsset = null;
      NextAsset = null;
      Features = new List<MillFeature>();
      MillMaterials = new MillMaterialSet();
      Name = name;
      NotificationController.Info("----------------- Asset: {0}", Name);
    }

    #endregion

    #region public methods

		public void AddFeature(MillFeature feature)
    {
      Features.Add(feature);
      NotificationController.Info("Configuring feature: {0}, action: {1}, code: {2}", feature.FeatureName.PadLeft(15,' '), feature.MaterialEventType.ToString().PadLeft(10, ' '), feature.EventCode);
    }
    public virtual MillMaterial AddMaterial(MillMaterial material)
    {
      MillMaterial tmp = new MillMaterial(material, false, Features.Copy());
      if (MillMaterials.AddMaterial(tmp))
				NotificationController.Info("Material: [ {0} ] added to area: {1}", material.ToString(), Name);
			else
        NotificationController.Warn("Material: [ {0} ] already in area: {1}", material.ToString(), Name);
      return tmp;
    }
    public MillMaterial AddProcessedMaterial(MillMaterial material)
    {
      MillMaterial tmp = new MillMaterial(material, true, Features.Copy());

      if (MillMaterials.AddMaterial(tmp))
        NotificationController.Info("Material: [ {0} ] sent back to area: {1}", material.ToString(), Name);
      else
        NotificationController.Warn("Material: [ {0} ] already in area: {1}", material.ToString(), Name);

      return tmp;
    }
    public void RemoveMaterial(MillMaterial material)
    {
      if (MillMaterials.RemoveMaterial(material))
        material.RemoveFeatures(Features);

      NotificationController.Info("Material: [ {0} ] removed from area: {1}", material.ToString(), Name);
    }
    public void SetProcessingFinished(MillMaterial material)
    {
      material.MarkAsProcessed();

      NotificationController.Info("Material: [ {0} ] processinig finished in area: {1}", material.ToString(), Name);
    }
    public void UnsetProcessingFinished(MillMaterial material)
    {
      material.MarkAsProcessed(false);
    }
    public void PrintState()
    {
      NotificationController.Info("---- Asset {0}----", Name);
      MillMaterials.PrintState();

      if (NextAsset != null)
        NextAsset.PrintState();
    }

    public void SetNextAsset(MillAsset asset)
    {
      NextAsset = asset;
    }
    public void SetPreviousAsset(MillAsset asset)
    {
      PreviousAsset = asset;
    }
    public virtual void MoveMaterialFromPreviousArea()
    {
      if (PreviousAsset != null)
      {
        MillMaterial material = PreviousAsset.MillMaterials.GetOldest(true);
        AddMaterial(material);
        PreviousAsset.MillMaterials.RemoveMaterial(material);
      }
    }
    public virtual void MoveMaterialBackToPreviousArea()
    {
      if (PreviousAsset != null)
      {
        MillMaterial material = MillMaterials.GetNewest(false);
        PreviousAsset.AddProcessedMaterial(material);
        MillMaterials.RemoveMaterial(material);
      }
    }
    public virtual void MaterialLeavesArea(AdvosolOpcHandler opcHandler)
    {
      MillMaterial material = MillMaterials.GetOldest();
      material.ReadAllMMv(opcHandler, MaterialEventType.MaterialLeaves);

      TriggerMaterialLeave(material);

      SetProcessingFinished(material);
    }
    public virtual void MaterialReenterArea(AdvosolOpcHandler opcHandler)
    {
      MillMaterial material = MillMaterials.GetOldest(true);

      UnsetProcessingFinished(material);
    }



    #endregion

    #region protected methods

    protected void TriggerMaterialEnter(MillMaterial material)
    {
      OnMaterialEnter?.Invoke(this, new MaterialStateEventArgs(this, material));
    }



    protected void TriggerMaterialLeave(MillMaterial material)
    {
      OnMaterialLeave?.Invoke(this, new MaterialStateEventArgs(this, material));
    }



    #endregion
  }
}
