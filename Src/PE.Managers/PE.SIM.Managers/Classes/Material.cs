using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NLog;

namespace PE.SIM.Managers.Classes
{
  public class Material
  {
    #region members

    private Logger _logger;
    private DateTime _startTime;
    private long _lastAssetID;
    private long _firstAssetID;
    private string _eventSymbol;
    private bool _dischargeActive;
    private Timer _materialTimer;

    #endregion
    #region properties

    public bool IsNextReleaseSafe { get { return _firstAssetID != Asset.AssetID; } }
    public UInt32 MaterialUniqueId { get; private set; }
    public string MaterialName { get; private set; }
    public Asset Asset { get; private set; }
    #endregion
    #region ctor

    public Material(Logger logger, int sequence)
    {
      _logger = logger;
      _startTime = DateTime.Now;

      MaterialName = String.Format("Test_{0}", sequence);
      MaterialUniqueId = 0;

      Asset = L1SimulationManagerOld.GetFirstAssetData();
      _lastAssetID = L1SimulationManagerOld.GetLastAssetId();
      _firstAssetID = Asset.AssetID;
      MaterialUniqueId = L1SimulationManagerOld.RequestMaterialId();

      _eventSymbol = "H";
      _dischargeActive = false;
      SetupMaterialTimer();
    }

    #endregion
    #region public methods
		public bool IsReadyToBeDeleted()
    {
      if (_lastAssetID == Asset.AssetID)
        return true;
      return false;
    }

    public string DisplayPosition()
    {
      string position = _eventSymbol.PadLeft(Asset.AsseetSequence, ' ');
      return string.Format($"{MaterialName.PadLeft(14, ' ')}" +
                          $"[{Asset.CurrentPass.ToString().PadLeft(2, ' ')} of {Asset.NoPasses.ToString().PadLeft(2, ' ')}]" +
                         $" {Asset.AssetName.PadLeft(24, ' ')}" +
             $"[{Asset.SecondsSinceStart().ToString().PadLeft(4, ' ')} of {Asset.TimeInAsset.ToString().PadLeft(4, ' ')}]" +
             $" {position}");
    }

    #endregion
    #region private methods
    private int SecondsSinceStart()
    {
      return Convert.ToInt32(DateTime.Now.Subtract(_startTime).TotalSeconds);
    }
    private void SetupMaterialTimer()
    {
      _materialTimer = new Timer();
      _materialTimer.AutoReset = true;
      _materialTimer.Interval = 500;
      _materialTimer.Enabled = true;
      _materialTimer.Elapsed += MaterialTimer_Elapsed;
      _materialTimer.Start();
    }
    private void MaterialTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        if (_lastAssetID == Asset.AssetID)
          return;

        //open discharge for next material
        if (_dischargeActive)
        {
          if (Line.LastDischarge != DateTime.MinValue && DateTime.Now.Subtract(Line.LastDischarge).TotalMilliseconds > 60 * 1000)
          {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Discharge deactivated: {DateTime.Now.Subtract(Line.LastDischarge).TotalMilliseconds}");
            Console.ResetColor();
            Line.LastDischarge = DateTime.MinValue;
            return;
          }
        }
       else if ((Asset.IsFurnace || Asset.AssetID== _firstAssetID )&& Line.LastDischarge != DateTime.MinValue)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine($"Discharge active: {DateTime.Now.Subtract(Line.LastDischarge).TotalMilliseconds} no processing for {MaterialName}");
          Console.ResetColor();
          return;
        }

        switch (Asset.AssetTimer())
        {
          case AssetResult.Enter:
            {
              L1SimulationManagerOld.PrepareAssetEntryFeatures(Asset, MaterialUniqueId);
              _eventSymbol = "H";
              break;
            }
          case AssetResult.Exit:
            {
              L1SimulationManagerOld.PrepareAssetExitFeatures(Asset, MaterialUniqueId);
              _eventSymbol = "T";
              break;
            }
          case AssetResult.ExitLastPass:
            {
              if (Asset.AssetID==35)
              {
                _dischargeActive = true;
                Line.LastDischarge = DateTime.Now;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Discharge active: {DateTime.Now.Subtract(Line.LastDischarge).TotalMilliseconds}");
                Console.ResetColor();
              }

              _eventSymbol = "H";
              L1SimulationManagerOld.PrepareAssetExitFeatures(Asset, MaterialUniqueId);
              if (_lastAssetID == Asset.AssetID)
                return;
              Asset.MoveToNext(L1SimulationManagerOld.GetNextAssetData(Asset.AssetID));
              break;
            }
        }
      }
      catch (Exception ex)
      {
        _logger.Error($"Exception: {ex.Message}");
      }
      #endregion
    }
  }
}
