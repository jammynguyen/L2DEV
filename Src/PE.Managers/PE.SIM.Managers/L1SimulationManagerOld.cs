using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.DTO.External.MVHistory;
using PE.Interfaces.Interfaces.Managers;
using PE.Interfaces.SendOffice;
using PE.L1S.Handlers;
using PE.SIM.Managers.Classes;
using SMF.Module.Core;

namespace PE.SIM.Managers
{
  public class L1SimulationManagerOld:ILevel1SimulationManagerOld
  {
    #region members

    private static ISimulationSendOffice _sendOffice;
    private Line _line;

    #endregion
    #region handlers

    private static AssetHandler _assetHandler;

    #endregion
    #region ctor
    public L1SimulationManagerOld(ISimulationSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _assetHandler = new AssetHandler();
      _line = new Line(ModuleController.Logger);
    }
    #endregion
    #region public methods

    public async Task CallLine()
    {
      await _line.ReleaseMaterial();
    }

    public static long GetLastAssetId()
    {
      using (PEContext ctx = new PEContext())
      {
        return _assetHandler.GetLastAssetId(ctx);
      }
    }

    #endregion
    #region static methods

    public static UInt32 RequestMaterialId()
    {
      DCL1BasIdRequestExt dc = new DCL1BasIdRequestExt()
      {
        RequestToken = Convert.ToUInt16(new Random().Next(1, 60000)),
        Header = new DTO.External.Adapter.DCHeaderExt()
        {
          Counter = 1,
          MessageId = 1000,
          TimeStamp = DateTime.Now.ToString(),
        }
      };

      SendOfficeResult<DCPEBasIdExt> result = _sendOffice.SendL1MaterialIdRequestToAdapterAsync(dc).GetAwaiter().GetResult();
      if (result.OperationSuccess)
      {
        ModuleController.Logger.Info("Received new BasId: {0}", result.DataConctract.BasId);

        return result.DataConctract.BasId;
      }
        
      else
        throw new Exception("Id of material cannot be retrived from PE");
    }

    internal static void PrepareAssetEntryFeatures(Asset asset, uint materialUniqueId)
    {
      using (PEContext ctx = new PEContext())
      {
        List<MVHFeature> list = _assetHandler.GetListOffAssetFeatures(ctx, asset.AssetID, true);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Material {materialUniqueId} enters aset: {asset.AssetName}");
        if (list != null && list.Count > 0)
        {
          Console.WriteLine($"-Features---------------------------------------");
          foreach (MVHFeature f in list)
          {
            Console.WriteLine($"Feature: {f.FeatureName}");
            SendFeatureData(asset, materialUniqueId, f);
          }
          Console.WriteLine($"------------------------------------------------");
        }
        Console.ResetColor();
      }

    }



    internal static void PrepareAssetExitFeatures(Asset asset, uint materialUniqueId)
    {
      using (PEContext ctx = new PEContext())
      {
        List<MVHFeature> list = _assetHandler.GetListOffAssetFeatures(ctx, asset.AssetID, false);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Material {materialUniqueId} leaves asset: {asset.AssetName}");
        if (list != null && list.Count > 0)
        {
          Console.WriteLine($"-Features---------------------------------------");
          foreach (MVHFeature f in list)
          {
            Console.WriteLine($"Feature: {f.FeatureName}");
            SendFeatureData(asset, materialUniqueId, f);
          }
          Console.WriteLine($"------------------------------------------------");
        }
        Console.ResetColor();
       
      }
    }

    private static void SendFeatureData(Asset asset, uint materialUniqueId, MVHFeature f)
    {
      try
      {
        if (!f.MVHFeaturesEXT.IsSampled)
        {
          DCMeasDataExt data = _assetHandler.GenerateNonSampledMeasurement(f, asset.CurrentPass, materialUniqueId);
          _sendOffice.SendL1AggregatedMeasDataToAdapterAsync(data);

        }
        else
        {
          DCMeasDataSampleExt data = _assetHandler.GenerateSampledMeasurement(f, asset.CurrentPass, materialUniqueId);
          _sendOffice.SendL1SampleMeasDataToAdapterAsync(data);

        }
      }
      catch { }
    }
    public static Asset GetFirstAssetData()
    {
      using (PEContext ctx = new PEContext())
      {
        V_MVHFeaturesMap asset =  _assetHandler.GetFirstAsset(ctx);
        return new Asset(asset.AssetId, asset.AssetName, asset?.MaxPassNo, asset?.TimeIn);
      }
    }
    public static Asset GetNextAssetData(long assetId)
    {
      using (PEContext ctx = new PEContext())
      {
        V_MVHFeaturesMap asset = _assetHandler.GetNextAsset(ctx, assetId);
        return new Asset(asset.AssetId, asset.AssetName, asset?.MaxPassNo, asset?.TimeIn);
      }
    }
    

    #endregion
  }
}
