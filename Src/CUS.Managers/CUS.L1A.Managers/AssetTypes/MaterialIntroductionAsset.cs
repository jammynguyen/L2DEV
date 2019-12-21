using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUS.L1A.Managers.MillOrganization;

namespace CUS.L1A.Managers.AssetTypes
{
  public class MaterialIntroductionAsset : MillAsset
  {
    public event EventHandler<MaterialStateEventArgs> OnMaterialIntroduction;

    #region ctor

    public MaterialIntroductionAsset(string name) : base(name)
    {

    }

    #endregion
    #region public methods

    public void IntroduceMaterial(uint basId)
    {
      AddMaterial(new MillMaterial(basId));
    }

    protected void TriggerRequestNewMaterial()
    {
      OnMaterialIntroduction?.Invoke(this, new MaterialStateEventArgs(this,null ));
    }

    #endregion
  }
}
