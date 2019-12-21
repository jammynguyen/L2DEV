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
  public abstract class Area
  {
    #region members

    protected ISimulationSendOffice _sendOffice;

    #endregion
    #region properties

    public int MaxCapacity { get; private set; }
    public List<MillAssetConfig> Assets { get; private set; }
    public List<RawMaterial> Materials { get; private set; }
    public Area PrecedingArea { get; private set; }

    #endregion
    #region private methods

    public bool IsFirstNAssetsFreeInArea(int max)
    {
      int counter = 0;
      foreach(MillAssetConfig c in Assets.OrderBy(x=>x.AssetSequence))
      {
        counter++;
				if(Materials.Where(x=>x.AssetConfig.AssetID==c.AssetID).Count()>0)
        {
          return false;
        }
        if (counter > max)
          break;
      }
      return true;
    }
    public MillAssetConfig GetNextAssetInArea(MillAssetConfig currentAsset)
    {
			int index = Assets.OrderBy(x=>x.AssetSequence).ToList().IndexOf(currentAsset);
      MillAssetConfig nextAsset = Assets.OrderBy(x => x.AssetSequence).ToList().ElementAtOrDefault(index + 1);

      //Console.WriteLine($"CURRENT Asset: {currentAsset.AssetName}, next asset: {nextAsset.AssetName}");

      return nextAsset;
    }
    public bool IsNextAssetsFree(MillAssetConfig nextAsset)
    {
      if (nextAsset != null)
        return !Materials.Any(x => x.AssetConfig.AssetID == nextAsset.AssetID);
      return false;
    }
    public bool IsLastAssetInArea(RawMaterial rawMaterial)
    {
      if (Assets == null)
        return true;
      return rawMaterial.AssetConfig.AssetID == (Assets.OrderByDescending(y=>y.AssetSequence).FirstOrDefault().AssetID);
    }

    #endregion
    #region protected methods

    protected bool AddMaterial(UInt32 basId)
    {
      if (Assets == null)
        return false;
      RawMaterial rm = new RawMaterial(basId, Assets.FirstOrDefault(), _sendOffice);

      if (Materials.Count< MaxCapacity)
      {
        Materials.Add(rm);
        return true;
      }
      return false;
    }
    protected bool IsLoadingMaterialPossible()
    {
      if (Assets == null)
        return false;

      if (Materials.Count < MaxCapacity)
        return true;
      return false;
    }
    public async Task MoveMaterials(bool moveToNextAreaPossible = true, Area nextArea = null)
    {
      RawMaterial materialReadyToBeMovedToNextArea = null;
      RawMaterial materialReadyToBeDeleted = null;
      //update time, move to next asset when is needed and necessary
      foreach (RawMaterial rm in Materials.OrderBy(x => x.StartTime))
      {
        if (rm.IsReadyToMoveToNextAsset())
        {
          if (rm.AssetConfig.IsLast)
          {
            materialReadyToBeDeleted = rm;
            continue;

            // material reached last asset, can be deleted
          }
          if (IsLastAssetInArea(rm) && moveToNextAreaPossible && materialReadyToBeMovedToNextArea==null)
          {
            //Console.WriteLine($"Move material {rm.BasId} to next area");
            //move to next area
            materialReadyToBeMovedToNextArea = rm;
            nextArea.SetMaterialToArea(rm);//dont forget to remove from current area
         
          }
          else
          {
            //is next asset free
            //move to next asset

            MillAssetConfig nextAsset = GetNextAssetInArea(rm.AssetConfig);
						if(IsNextAssetsFree(nextAsset))
            {
              //Console.WriteLine($"Move material {rm.BasId} to next asset: {nextAsset.AssetName}");
              rm.UpdateAsset(nextAsset);
            }
          }
        }
        rm.UpdateTimeInAsset();
      }

			//remove material when is moved to next area
      if (materialReadyToBeMovedToNextArea != null)
        Materials.Remove(materialReadyToBeMovedToNextArea);

      //remove material when is moved to next area
      if (materialReadyToBeDeleted != null)
        Materials.Remove(materialReadyToBeDeleted);

      if (PrecedingArea != null)
      {
        await PrecedingArea.MoveMaterials(IsShiftFromPreviousAreaPossible(1), this);
      }
    }

    #endregion
    #region public methods

    public string PrintLineConfiguration()
    {
      string returnValue = "==============================================\n";
			foreach(MillAssetConfig a in Assets)
      {
        returnValue+=string.Format($"{a.ToString()}\n");
      }
      returnValue += "==============================================\n";

      return returnValue;
    }
    internal virtual string PrintMaterials()
    {
      string returnValue = "";
      foreach (RawMaterial m in Materials.OrderBy(x => x.AssetConfig.AssetSequence).OrderByDescending(x=>x.StartTime))
      {
        returnValue += string.Format($"{m.ToString()}\n");
      }
      return returnValue;
    }
    public virtual bool IsShiftFromPreviousAreaPossible(int max)
    {
      return (Materials.Count < MaxCapacity) && IsFirstNAssetsFreeInArea(max);
    }
		public void SetMaterialToArea(RawMaterial rawMaterial)
    {
      rawMaterial.UpdateAsset(Assets.FirstOrDefault());
      Materials.Add(rawMaterial);
    }

    #endregion

    #region ctor

    public Area(int capacity, List<MillAssetConfig> assets,Area precedingArea, ISimulationSendOffice sendOffice)
    {
      MaxCapacity = capacity;
      Assets = assets;
      Materials = new List<RawMaterial>();
      _sendOffice = sendOffice;
      PrecedingArea = precedingArea;
    }
		 
    #endregion
  }
}
