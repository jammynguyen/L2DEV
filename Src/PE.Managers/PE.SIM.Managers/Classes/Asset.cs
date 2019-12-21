using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.SIM.Managers.Classes
{
  public enum AssetState { Entry, Active, Exit }
  public enum AssetResult { Enter, Exit, ExitLastPass, NOP }
  public class Asset
  {
    #region properties


    public DateTime AssetEntryTime { get; private set; }
    public long AssetID { get; private set; }
    public string AssetName { get; private set; }
    public int NoPasses { get; private set; }//after random
    public int CurrentPass { get; private set; }
   //public bool IsFirst { get; private set; }
    public int TimeInAsset { get; private set; }//per pass
    public AssetState AssetState { get; private set; }
    public int AsseetSequence { get; set; }
    public bool IsFurnace { get; set; }
    #endregion
    #region ctor

    public Asset(long assetId, string assetName, short? maxNoPasses, short? timeIn)
    {
      if (maxNoPasses == null)
        maxNoPasses = 1;
      if (timeIn == null)
        timeIn = 1;

      AssetID = assetId;
      AssetName = assetName;
      NoPasses = RandomNumberOdd(1, (int)maxNoPasses);
      CurrentPass = 0;
      AssetEntryTime = DateTime.Now;
      //IsFirst = true;
      TimeInAsset = (int)timeIn;
      AssetState = AssetState.Entry;
      AsseetSequence = 1;

      IsFurnace = false;
    }

    #endregion
    #region public methods

    public AssetResult AssetTimer()
    {
      if (AssetState == AssetState.Entry)
      {
        CurrentPass++;
        AssetState = AssetState.Active;
        AssetEntryTime = DateTime.Now;
        //Material enters to asset
        return AssetResult.Enter;
      }
      else if (AssetState == AssetState.Active && SecondsSinceStart() >= TimeInAsset)
      {
        if (CurrentPass == NoPasses)
        {
          AssetState = AssetState.Exit;
          return AssetResult.ExitLastPass;
        }
        else
        {
          AssetState = AssetState.Entry;
          return AssetResult.Exit;
        }
        //material leaves asset

      }
      return AssetResult.NOP;
    }

    #endregion
    #region private methods
    private static int RandomNumberOdd(int min, int max)
    {
      Random random = new Random();
      int ans = random.Next(min, max);
      if (ans % 2 == 1) return ans;
      else
      {
        if (ans + 1 <= max)
          return ans + 1;
        else if (ans - 1 >= min)
          return ans - 1;
        else return max;
      }
    }

    public int SecondsSinceStart()
    {
      if (AssetEntryTime == DateTime.MinValue)
        return Int32.MaxValue;
      return Convert.ToInt32(DateTime.Now.Subtract(AssetEntryTime).TotalSeconds);
    }

    public void MoveToNext(Asset asset)
    {
      AssetID = asset.AssetID;
      AssetName = asset.AssetName;
      NoPasses = asset.NoPasses;
      CurrentPass = 0;
      AssetEntryTime = DateTime.Now;
     // IsFirst = true;
      TimeInAsset = asset.TimeInAsset;
      AssetState = asset.AssetState;
      AsseetSequence++;

			if(AssetID==33 || AssetID==35)
        IsFurnace = true;
    }

    #endregion
  }
}
