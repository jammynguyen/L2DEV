using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.Interfaces.SendOffice;
using PE.L1S.Handlers.MillSetup;

namespace PE.SIM.Managers.MillOrganiztion
{
  public class FurnaceArea : Area
  {
    #region ctor

    public FurnaceArea(int capacity, List<MillAssetConfig> assets,Area precedingArea, ISimulationSendOffice sendOffice) :base(capacity,assets, precedingArea, sendOffice)
    {

    }

    #endregion
    #region public methods

    public async Task TriggerMillMovemet()
    {
      await MoveMaterials();
    }
    public override bool IsShiftFromPreviousAreaPossible(int max)
    {
      return (Materials.Count < MaxCapacity);
    }
    //internal override string PrintMaterials()
    //{
    //  string returnValue = "";
    //  foreach (RawMaterial m in Materials.OrderBy(x => x.AssetConfig.AssetSequence).OrderBy(x=>x.CurrentTimeInAsset))
    //  {
    //    returnValue += string.Format($"{m.ToString()}\n");
    //  }
    //  return returnValue;

    //}
    //public async Task IntroduceMaterial()
    //  {

    //  }

    #endregion
  }
}
