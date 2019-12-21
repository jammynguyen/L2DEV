using NLog;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.External.MVHistory;
using PE.L1S.Level1Simulation.Communication;
using SMF.Core.Log;
using SMF.Module.Core.Interfaces.Telegram;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PE.L1S.Level1Simulation.Module
{
  public sealed class L1SimulationManager //: IL1SimulationManager
  {
    #region fields and properties
    private Logger _logger;
    public bool IsRunning { get; private set; } = false;

    public MVHRawMaterial simulatedRawMaterial;
    public static ushort RequestToken;

    private List<MVHAsset> assetsRolling;
    private List<MVHAsset> assetsAfterCut;

    public List<MVHRawMaterial> childs;

    #endregion

    #region ctor
    public L1SimulationManager(Logger logger)
    {
      _logger = logger;
      using (PEContext ctx = new PEContext())
      {
        assetsRolling = ctx.MVHAssets.Where(w =>
                                      w.IsCheckpoint == true &&
                                      w.MVHAssetsEXT.EnumArea == AssetsArea.ENUM_Rolling)
                              .OrderBy(o => o.OrderSeq)
                              .Include(i => i.MVHAssetsEXT)
                              .ToList();

        assetsAfterCut = ctx.MVHAssets.Where(w =>
                                      w.IsCheckpoint == true &&
                                      w.MVHAssetsEXT.EnumArea == AssetsArea.ENUM_AfterCut)
                              .OrderBy(o => o.OrderSeq)
                              .Include(i => i.MVHAssetsEXT)
                              .ToList();

        foreach (MVHAsset asset in assetsRolling)
        {
          asset.MVHFeatures = ctx.MVHFeatures.Where(w => w.IsActive == true && w.FKAssetId == asset.AssetId).ToList();
          foreach (MVHFeature feature in asset.MVHFeatures)
          {
            feature.MVHFeaturesEXT = ctx.MVHFeaturesEXTs.Where(w => w.FKFeatureId == feature.FeatureId).Single();
          }
        }

        foreach (MVHAsset asset in assetsAfterCut)
        {
          asset.MVHFeatures = ctx.MVHFeatures.Where(w => w.IsActive == true && w.FKAssetId == asset.AssetId).ToList();
          foreach (MVHFeature feature in asset.MVHFeatures)
          {
            feature.MVHFeaturesEXT = ctx.MVHFeaturesEXTs.Where(w => w.FKFeatureId == feature.FeatureId).Single();
          }
        }

        childs = new List<MVHRawMaterial>();
      }
    }

    #endregion

    #region interface_functions


    internal async Task CreateSimulationDataForRawMaterialAsync()
    {
      MVHAsset randomedAssetToMarkMaterialAsScrap = assetsRolling.ElementAt(new Random().Next(assetsRolling.Count));

      foreach (MVHAsset asset in assetsRolling.OrderBy(o => o.OrderSeq))
      {

        if (asset == randomedAssetToMarkMaterialAsScrap)
        {
          int randomedNumberToDecideIfMaterialWillBeScrap = new Random().Next(0, 100);
          if (randomedNumberToDecideIfMaterialWillBeScrap <= 10)//TODO: Properties.Settings.Default.PercentOfMaterialsMarkedAsScrap)
          {

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"           Material: {simulatedRawMaterial.ExternalRawMaterialId} marked as scrap");
            Console.ResetColor();


            DCL1ScrapDataExt dCScrapData = new DCL1ScrapDataExt()
            {
              BasId = Convert.ToUInt16(simulatedRawMaterial.ExternalRawMaterialId),
              LocationId = Convert.ToUInt16(randomedAssetToMarkMaterialAsScrap.AssetId),
              TypeOfScrap = Convert.ToUInt16(TypeOfScrap.ScrapFromMill),
              Header = new DTO.External.Adapter.DCHeaderExt()
              {
                MessageId = 2003,
                Counter = 1,
                TimeStamp = DateTime.Now.ToString(),
              }
            };
            SendTelegramToTcpProxy(dCScrapData);
            //Task.Run(() => SendOfficeExt.SendScrapDataToAdapter(dCScrapData));
            return;
          }
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"           Asset: {asset.AssetName} | DateTime: {DateTime.Now}");
        Console.ResetColor();


        for (int passNumber = 1; passNumber <= asset.MVHAssetsEXT.MaxPassNo; passNumber++)
        {
          Console.ForegroundColor = ConsoleColor.Cyan;
          Console.WriteLine($"           Asset pass number: {passNumber}...");
          Console.ResetColor();

          await CreateAssetMeasurementAsync(asset, passNumber, asset.MVHAssetsEXT.MaxPassNo);
        }


      }
      await Task.Delay(10000);

      foreach(MVHRawMaterial child in childs)
      {

        simulatedRawMaterial = child;
        foreach (MVHAsset asset in assetsAfterCut.OrderBy(o => o.OrderSeq))
        {
          Console.ForegroundColor = ConsoleColor.Cyan;
          Console.WriteLine($"           Asset: {asset.AssetName} | DateTime: {DateTime.Now}");
          Console.ResetColor();


          for (int passNumber = 1; passNumber <= asset.MVHAssetsEXT.MaxPassNo; passNumber++)
          {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"           Asset pass number: {passNumber}...");
            Console.ResetColor();

            await CreateAssetMeasurementAsync(asset, passNumber, asset.MVHAssetsEXT.MaxPassNo);
          }


        }
      }
      childs.Clear();
    }

    private async Task CreateAssetMeasurementAsync(MVHAsset asset, int passNumber, int maxMassNumber)
    {
      L1SimulationMaterialDivision l1SimulationMaterialDivision = new L1SimulationMaterialDivision();
      TimeSpan perFeatureOperationDuration = TimeSpan.FromSeconds((int)asset.MVHAssetsEXT.TimeIn);



      foreach (MVHFeature feature in asset.MVHFeatures)
      {
        //bool isCutFeature = feature.MVHFeaturesEXT.EnumTypeOfCut == 10;

        if (feature.MVHFeaturesEXT.EnumTypeOfCut == (short)TypeOfCut.DivideCutN)
        {
          UpdateSumilatedRowMaterialAfterDevisionCut(Convert.ToInt32(simulatedRawMaterial.ExternalRawMaterialId));
          DCL1MaterialDivisionExt dataToSend = l1SimulationMaterialDivision.MaterialDivisionSimulation(simulatedRawMaterial, asset.AssetId);
          SendTelegramToTcpProxy(dataToSend);         

        }
        else if (feature.MVHFeaturesEXT.EnumTypeOfCut.HasValue)
        {
          DCL1CutDataExt dataToSend = l1SimulationMaterialDivision.MaterialCutSimulation(simulatedRawMaterial, asset.AssetId, feature.MVHFeaturesEXT.EnumTypeOfCut.Value);
          SendTelegramToTcpProxy(dataToSend);

        }
        else
        {
          if (feature.MVHFeaturesEXT.IsSampled)
          {
            DCMeasDataSampleExt dcMeasurement = GenerateSampledMeasurement(feature, passNumber);
            SendTelegramToTcpProxy(dcMeasurement);

          }
          else
          {
            DCMeasDataExt dcMeasurement = GenerateNonSampledMeasurement(feature, passNumber);
            SendTelegramToTcpProxy(dcMeasurement);

          }
        }
        await Task.Delay(perFeatureOperationDuration);
      }
    }

    private DCMeasDataExt GenerateNonSampledMeasurement(MVHFeature feature, int passNumber)
    {
      DCMeasDataExt dcMeasurement = new DCMeasDataExt();

      {
        dcMeasurement.BasId = Convert.ToUInt16(simulatedRawMaterial.ExternalRawMaterialId);
        dcMeasurement.PassNumber = Convert.ToUInt16(passNumber);
        dcMeasurement.BaseFeature = Convert.ToUInt16(feature.FeatureCode);
        dcMeasurement.Valid = Convert.ToUInt16(new Random().Next(0, 21) <= 1 ? (short)0 : (short)1);
        dcMeasurement.EventCode = Convert.ToUInt16(feature.FeatureCode);
        dcMeasurement.IsReversed = Convert.ToUInt16(passNumber % 2 == 0 ? 1 : 0);
        dcMeasurement.Header = new DTO.External.Adapter.DCHeaderExt()
        {
          Counter = 1,
          TimeStamp = DateTime.Now.ToString(),
          MessageId = 3001,
        };

        if (new Random().Next(0, 51) <= 25)
        {
          dcMeasurement.Min = (float)feature.MVHFeaturesEXT.MinValue;
          dcMeasurement.Max = (float)feature.MVHFeaturesEXT.MinValue;
          dcMeasurement.Avg = (float)feature.MVHFeaturesEXT.MinValue;
        }
        else
        {
          dcMeasurement.Min = (float)feature.MVHFeaturesEXT.MaxValue;
          dcMeasurement.Max = (float)feature.MVHFeaturesEXT.MaxValue;
          dcMeasurement.Avg = (float)feature.MVHFeaturesEXT.MaxValue;
        }
      }

      return dcMeasurement;
    }

    private DCMeasDataSampleExt GenerateSampledMeasurement(MVHFeature feature, int passNumber)
    {
      DCMeasDataSampleExt data = new DCMeasDataSampleExt();
      {
        data.NumberOfSamples = 10;
        data.Samples = new DCSampleExt[10];
        data.IsReversed = Convert.ToUInt16(passNumber % 2 == 0 ? 1 : 0);
        data.Header = new DTO.External.Adapter.DCHeaderExt()
        {
          MessageId = 3002,
          TimeStamp = DateTime.Now.ToString(),
          Counter = 1,
        };
        for (int i = 0; i < 10; i++)
        {
          data.Samples[i] = new DCSampleExt();
        }
        data.BasId = Convert.ToUInt16(simulatedRawMaterial.ExternalRawMaterialId);
      }

      return data;
    }

    internal async void SendTelegramToTcpProxy(BaseExternalTelegram dc)
    {
      int port = 0;
      IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
      IPAddress ipAddress = ipHostInfo.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
      

      TcpClient client=null;
      NetworkStream stream=null;

      if (dc is DCL1BasIdRequestExt)
      {
        port = 5000;
      }
      else if (dc is DCL1CutDataExt)
      {
        port = 5001;
      }
      else if (dc is DCL1MaterialDivisionExt)
      {
        port = 5002;
      }
      else if (dc is DCL1ScrapDataExt)
      {
        port = 5003;
      }
      else if (dc is DCMeasDataExt)
      {
        port = 5004;
      }
      else if (dc is DCMeasDataSampleExt)
      {
        port = 5005;
      }

      IPEndPoint endpoint = new IPEndPoint(ipAddress, port);

      try
      {
        client = new System.Net.Sockets.TcpClient();
        client.SendTimeout = 400; // in [ms]
        client.ReceiveTimeout = 400; // in [ms]
        client.Connect(endpoint);

        if (!client.Connected)
        {
          throw new Exception("Failed to connect.");
        }

        stream = client.GetStream();
        byte[] bytesToSend = SMF.Core.Helpers.ObjectDump.RawSerialize(dc);

        if (!stream.CanWrite)
        {
          throw new Exception("You cannot write data to this stream. Sending data imposible.");
        }

        //await System.Threading.Tasks.Task.Delay(2000); //[SF]: is this necessary? 
        stream.Write(bytesToSend, 0, bytesToSend.Length);
        _logger.Info("Telegram. IP: {0}, Port: {1}, Size: {2} successfully sent.", ipAddress.ToString(), port, bytesToSend.Length);
      }
			catch( Exception e)
      {
        LogHelper.LogException(_logger, e, "Error during sending message on port: "+port);
      }
			finally
      {
        if (client != null)
          client.Close();
        if (stream != null)
          stream.Close();
      }
    }

    private void UpdateSumilatedRowMaterialAfterDevisionCut(int externalRowMatId)
    {
      using (PEContext ctx = new PEContext())
      {
        simulatedRawMaterial = ctx.MVHRawMaterials.Where(w => w.ExternalRawMaterialId == externalRowMatId).Include(i => i.MVHRawMaterialsSteps).Single();
      }
    }
    #endregion
  }
}
