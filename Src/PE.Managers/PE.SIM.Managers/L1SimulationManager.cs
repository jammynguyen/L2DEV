using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.External.MVHistory;
using PE.Interfaces.Interfaces.Managers;
using PE.Interfaces.SendOffice;
using PE.L1S.Handlers;
using PE.SIM.Managers.MillOrganiztion;
using SMF.Module.Core;
using SMF.Module.Notification;

namespace PE.SIM.Managers
{
  public class L1SimulationManager : ILevel1SimulationManager
  {
    #region members

    private static ISimulationSendOffice _sendOffice;
    private static ChargingArea _charging;
    private static FurnaceArea _furnace;
    private static RollingMillArea _rolling;
    private static AfterCutArea _afterCut;

    #endregion
    #region handlers

    private static AssetHandler _assetHandler;

    #endregion
    #region ctor
    public L1SimulationManager(ISimulationSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _assetHandler = new AssetHandler();
      _charging = null;
      _furnace = null;
      _rolling = null;
      _afterCut = null;
    }
    #endregion
    #region public methods

    public async Task InitLine()
    {
      using (PEContext ctx = new PEContext())
      {
        try
        {
          _assetHandler.DeleteOldMaterials(ctx);
          await ctx.SaveChangesAsync();
        }
        catch (Exception ex){
          NotificationController.Error(ex.Message);
        }

        _charging = new ChargingArea(1, await _assetHandler.GetAreaAssets(ctx, AssetsArea.ENUM_Charging), null, _sendOffice);
        _furnace = new FurnaceArea(10, await _assetHandler.GetAreaAssets(ctx, AssetsArea.ENUM_Furnace), _charging, _sendOffice);
        _rolling = new RollingMillArea(2, await _assetHandler.GetAreaAssets(ctx, AssetsArea.ENUM_Rolling), _furnace, _sendOffice);
        _afterCut = new AfterCutArea(4, await _assetHandler.GetAreaAssets(ctx, AssetsArea.ENUM_AfterCut), _rolling, _sendOffice);
      }
    }

    public async Task StartSimulation()
    {
      await InitLine();
      PrintLineConfiguration();

      Task task = Task.Factory.StartNew(async() =>
      {
        while (true)
        {
          await DoWork();
          PrintMaterials();
          Thread.Sleep(1000);
        }
      }, TaskCreationOptions.None);
    }

    private async Task DoWork()
    {
      try
      {
        await _charging.IntroduceMaterial();
      }
			catch (Exception ex)
      {
        NotificationController.Error(ex.Message);
      }
      try
      {
        await _afterCut.TriggerMillMovemet();
      }
      catch (Exception ex)
      {
        NotificationController.Error(ex.Message);
      }

    }

    private void PrintLineConfiguration()
    {

      Console.WriteLine("Mill Configuration");
      Console.WriteLine("-----------------------------");
      Console.WriteLine(_charging.PrintLineConfiguration());
      Console.WriteLine(_furnace.PrintLineConfiguration());
      Console.WriteLine(_rolling.PrintLineConfiguration());
      Console.WriteLine(_afterCut.PrintLineConfiguration());
    }

    private void PrintMaterials()
    {

      Console.WriteLine("Materials");
      Console.WriteLine("-----------------------------");
      Console.WriteLine(_charging.PrintMaterials());
      Console.WriteLine(_furnace.PrintMaterials());
      Console.WriteLine(_rolling.PrintMaterials());
      Console.WriteLine(_afterCut.PrintMaterials());
    }

    #endregion
    #region static methods
    #endregion
  }
}
