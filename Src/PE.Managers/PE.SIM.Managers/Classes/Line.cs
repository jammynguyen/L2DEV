using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NLog;

namespace PE.SIM.Managers.Classes
{
  public class Line
  {
    #region members

    private Timer _materialTimer;
    private Logger _logger;
    private int _sequence;
    private readonly object lockObject = new object();

    #endregion
    #region properties

    public List<Material> Materials { get; private set; }
    public static DateTime LastDischarge { get;  set; }
    #endregion
    #region ctor

    public Line(Logger logger)
    {
      _logger = logger;
      Materials = new List<Material>();
      _sequence = 0;
      LastDischarge = DateTime.MinValue;

      SetupMaterialTimer();
    }

    #endregion
    #region public methods
		
    public async Task ReleaseMaterial()
    {
      lock (lockObject)
      {
        if (VerifyIfPosibleReleaseingNewMaterial() && NoMaterialsInFurnace()<20)
        {
          Materials.Add(new Material(_logger, _sequence++));
        }
      }

      await Task.FromResult(0);
    }

    #endregion
    #region private methods

		private int NoMaterialsInFurnace()
    {
      return Materials.Count(x => x.Asset.IsFurnace);
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


        //_logger.Info("Timer tick Line");
        List<Material> toBeDeleted = new List<Material>();

        lock (lockObject)
        {
          foreach (Material m in Materials)
          {
            if (m.IsReadyToBeDeleted())
            {
              toBeDeleted.Add(m);
            }
          }
          foreach (Material m in toBeDeleted)
          {
            Materials.Remove(m);
          }

          StringBuilder sb = new StringBuilder();
					
          foreach (Material m in Materials)
          {
            sb.AppendLine(m.DisplayPosition());
          }
					lock(sb)
          {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(sb);
            Console.ResetColor();
          }

        }
      }
      catch (Exception ex)
      {
        _logger.Error($"Exceprion: {ex.Message}");
      }
    }
    private bool VerifyIfPosibleReleaseingNewMaterial()
    {
      foreach (Material m in Materials)
      {
        if (!m.IsNextReleaseSafe)
          return false;
      }

      return true;
    }
    #endregion
  }
}
