using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUS.L1A.Handlers;
using CUS.L1A.Managers.MillOrganization;

namespace CUS.L1A.Managers.AssetTypes
{
  public class FurnaceAsset: MillAsset
  {

    #region ctor

    public FurnaceAsset(string name) : base(name)
    {

    }

    #endregion

    public override void MoveMaterialFromPreviousArea()
    {
      if (PreviousAsset != null)
      {
        MillMaterial material = PreviousAsset.MillMaterials.GetOldest(true);
        PreviousAsset.MillMaterials.RemoveMaterial(material);

        material.PerformMaterialCharge();
        material = AddMaterial(material);
       
        TriggerMaterialEnter(material);
      }
    }
    public override void MoveMaterialBackToPreviousArea()
    {
      if (PreviousAsset != null)
      {
        MillMaterial material = MillMaterials.GetNewest(false);
        MillMaterials.RemoveMaterial(material);

        material.PerformMaterialUncharge();
        PreviousAsset.AddProcessedMaterial(material);
        

        TriggerMaterialLeave(material);
      }
    }
    public override void MaterialLeavesArea(AdvosolOpcHandler opcHandler)
    {
      MillMaterial material = MillMaterials.GetOldest();
      material.PerformMaterialDischarge();
      material.ReadAllMMv(opcHandler, MaterialEventType.MaterialLeaves);

      SetProcessingFinished(material);
      TriggerMaterialLeave(material);
      
    }
    public override void MaterialReenterArea(AdvosolOpcHandler opcHandler)
    {
      MillMaterial material = MillMaterials.GetOldest(true);
      material.PerformMaterialUndischarge();
      UnsetProcessingFinished(material);

      TriggerMaterialEnter(material);
    }
  }
}
