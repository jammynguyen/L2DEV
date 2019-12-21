using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMF.Module.Notification;

namespace CUS.L1A.Managers.MillOrganization
{
  public class MillMaterialSet
  {
    #region properties

    public List<MillMaterial> Materials { get; private set; }

    #endregion
    #region ctor

    public MillMaterialSet()
    {
      Materials = new List<MillMaterial>();
    }

    #endregion
    #region public methods

		public bool AddMaterial(MillMaterial material)
    {
      if (Materials.FindAll(m=>m.BasId == material.BasId).Count>0)
        return false;
      Materials.Add(material);

      return true;
    }
    public bool RemoveMaterial(MillMaterial material)
    {
      if (Materials.FindAll(m => m.BasId == material.BasId).Count == 0)
        return false;
      Materials.Remove(material);

      return true;
    }

		public MillMaterial GetOldest(bool processed=false)
    {
      return Materials.Where(x => x.IntroductionTime == Materials.Min(q => q.IntroductionTime)).Where(p=>p.Processed== processed).FirstOrDefault();
    }
    public MillMaterial GetNewest(bool processed = false)
    {
      return Materials.Where(x => x.IntroductionTime == Materials.Max(q => q.IntroductionTime)).Where(p => p.Processed == processed).FirstOrDefault();
    }

    public void PrintState()
    {
      foreach (MillMaterial m in Materials)
      {
        if (m.Processed)
          NotificationController.Warn(m.ToString());
        else
          NotificationController.Info(m.ToString());
      }
    }

    #endregion
  }
}
