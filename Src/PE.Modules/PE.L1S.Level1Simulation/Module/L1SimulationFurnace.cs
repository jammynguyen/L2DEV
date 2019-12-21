using NLog;
using PE.DbEntity.Models;
using PE.DTO.External.MVHistory;
using PE.Interfaces.Interfaces.Managers;
using PE.L1S.Level1Simulation.Communication;
using SMF.Module.Core;
using SMF.Module.Parameter;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PE.L1S.Level1Simulation.Module
{
  class L1SimulationFurnace : IL1SimulationFurnace
  {
    private Logger _logger;
    private List<MVHAsset> assets;

    private L1SimulationManager _l1SimulationManager;

    public static ushort RequestToken = 0;

    private TimeSpan materialProcessingTime = TimeSpan.Zero;

    public MVHRawMaterial simulatedRawMaterialOnEntry;

    public MVHRawMaterial simulatedRawMaterialOnDischarge;

    private int furnaceSize;

    private List<KeyValuePair<int, MVHRawMaterial>> materialsInFurnace;
    public L1SimulationFurnace(Logger logger)
    {
      _logger = logger;
      initializeFurnace();
      _l1SimulationManager = new L1SimulationManager(logger);
      Task.Run(() => TcpListener());
      if (RequestToken == 0)
      {
        GenerateMaterial();
      }
    }

    private void initializeFurnace()
    {
      furnaceSize = ParameterController.GetParameter("SIM_L1FurnaceSize").ValueInt.GetValueOrDefault();
      materialsInFurnace = new List<KeyValuePair<int, MVHRawMaterial>>(furnaceSize);
      for (int i = 1; i <= furnaceSize; i++)
      {
        materialsInFurnace.Add(new KeyValuePair<int, MVHRawMaterial>(i, new MVHRawMaterial()));
      }
    }

    private void CalculateMaterialProcessingTime()
    {
      using (PEContext ctx = new PEContext())
      {
        assets = ctx.MVHAssets.Where(w =>
                                      w.IsCheckpoint == true &&
                                      w.MVHAssetsEXT.EnumArea == DbEntity.Enums.AssetsArea.ENUM_Furnace)
                              .OrderBy(o => o.OrderSeq)
                              .Include(i => i.MVHAssetsEXT)
                              .ToList();
        foreach (MVHAsset asset in assets)
        {
          materialProcessingTime = materialProcessingTime + TimeSpan.FromSeconds((int)asset.MVHAssetsEXT.TimeIn * (int)asset.MVHAssetsEXT.MaxPassNo);
        }
      }
    }

    //function allows to fill furnace with materials befor first discharge
    internal async void GenerateMaterial()
    {
      RequestBaseIdForMaterial();
    }

    internal async void RequestBaseIdForMaterial()
    {
      RequestToken = Convert.ToUInt16(new Random().Next(1, 60000));

      DCL1BasIdRequestExt dc = new DCL1BasIdRequestExt()
      {
        RequestToken = RequestToken,
        Header = new DTO.External.Adapter.DCHeaderExt()
        {
          Counter = 1,
          MessageId = 1000,
          TimeStamp = DateTime.Now.ToString(),
        }
      };

      _l1SimulationManager.SendTelegramToTcpProxy(dc);
    }

    public async void ProcessPEBaseIdTelegam(DCPEBasIdExt dataContract)
    {
      if (dataContract.Header.Counter >= 1)
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.WriteLine("Child created");
        Console.WriteLine();
        Console.ResetColor();

        using (PEContext ctx = new PEContext())
        {
          _l1SimulationManager.childs.Add(
            ctx.MVHRawMaterials.Where(w => w.ExternalRawMaterialId == dataContract.BasId).Include(i => i.MVHRawMaterialsSteps).Single()
          );
        }

        return;
      }
      else if (RequestToken != dataContract.RequestToken)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Console.WriteLine("Request token are not matching");
        Console.WriteLine();
        Console.ResetColor();

        return;
      }

      try
      {
        using (PEContext ctx = new PEContext())
        {
          simulatedRawMaterialOnEntry = ctx.MVHRawMaterials.Where(w => w.ExternalRawMaterialId == dataContract.BasId).Include(i => i.MVHRawMaterialsSteps).Single();
        }

        SendEntryWeightMessage();
        await Task.Delay(500);

        ModuleController.Logger.Info($"Billet {simulatedRawMaterialOnEntry.ExternalRawMaterialId} Simulation start time: {DateTime.Now} ");

        MakeFurnaceStep();
        SendFurnaceChargeMessage();

        RequestToken = 0;

        if (materialsInFurnace[furnaceSize - 1].Value.RawMaterialId == 0 || simulatedRawMaterialOnDischarge.RawMaterialId == 0)
        {
          GenerateMaterial();
        }
        else if (simulatedRawMaterialOnDischarge.RawMaterialId != 0)
        {
          SendFurnaceDischargeMessage();
          _l1SimulationManager.simulatedRawMaterial = simulatedRawMaterialOnDischarge;
          await _l1SimulationManager.CreateSimulationDataForRawMaterialAsync();
          simulatedRawMaterialOnDischarge = null;
          GenerateMaterial();
        }

      }
      catch (InvalidOperationException)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Console.WriteLine("Material not found in DB");
        Console.WriteLine();
        Console.ResetColor();
      }
      catch (Exception)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Console.WriteLine("Other Error");
        Console.WriteLine();
        Console.ResetColor();
        throw;
      }

      return;
    }

    internal async void SendEntryWeightMessage()
    {
      float weightMin = 0;
      float weightMax = 0;
      using (PEContext ctx = new PEContext())
      {
        MVHFeaturesEXT feature = ctx.MVHFeaturesEXTs.Where(w => w.FKFeatureId == 1).Single();
        weightMin = (float)feature.MinValue;
        weightMax = (float)feature.MaxValue;
      }

      DCMeasDataExt dcMeasurement = new DCMeasDataExt()
      {
        BasId = Convert.ToUInt16(simulatedRawMaterialOnEntry.ExternalRawMaterialId),
        PassNumber = 1,
        BaseFeature = 1,
        Valid = Convert.ToUInt16(new Random().Next(0, 21) <= 1 ? (short)0 : (short)1),
        EventCode = 1,

        Min = weightMin,
        Max = weightMax,
        Avg = (weightMin + weightMax) / 2,
        Header = new DTO.External.Adapter.DCHeaderExt()
        {
          Counter = 1,
          MessageId = 3001,
          TimeStamp = DateTime.Now.ToString(),
        }
      };
      _l1SimulationManager.SendTelegramToTcpProxy(dcMeasurement);
    }

    internal async void SendFurnaceChargeMessage()
    {
      float valueMin = 0;
      float valueMax = 0;

      using (PEContext ctx = new PEContext())
      {
        MVHFeaturesEXT feature = ctx.MVHFeaturesEXTs.Where(w => w.FKFeatureId == 2).Single();
        valueMin = (float)feature.MinValue;
        valueMax = (float)feature.MaxValue;
      }

      DCMeasDataExt dcMeasurement = new DCMeasDataExt()
      {
        BasId = Convert.ToUInt16(simulatedRawMaterialOnEntry.ExternalRawMaterialId),
        PassNumber = 1,
        BaseFeature = 2,
        Valid = Convert.ToUInt16(new Random().Next(0, 21) <= 1 ? (short)0 : (short)1),
        EventCode = 2,

        Min = valueMax,
        Max = valueMax,
        Avg = valueMax,
        Header = new DTO.External.Adapter.DCHeaderExt()
        {
          Counter = 1,
          TimeStamp = DateTime.Now.ToString(),
          MessageId = 3001,
        }
      };
      _l1SimulationManager.SendTelegramToTcpProxy(dcMeasurement);

      materialsInFurnace[0] = new KeyValuePair<int, MVHRawMaterial>(0, simulatedRawMaterialOnEntry);

      simulatedRawMaterialOnEntry = null;
    }

    internal async void SendFurnaceDischargeMessage()
    {
      float valueMin = 0;
      float valueMax = 0;

      using (PEContext ctx = new PEContext())
      {
        MVHFeaturesEXT feature = ctx.MVHFeaturesEXTs.Where(w => w.FKFeatureId == 4).Single();
        valueMin = (float)feature.MinValue;
        valueMax = (float)feature.MaxValue;
      }

      DCMeasDataExt dcMeasurement = new DCMeasDataExt()
      {
        BasId = Convert.ToUInt16(simulatedRawMaterialOnDischarge.ExternalRawMaterialId),
        PassNumber = 1,
        BaseFeature = 4,
        Valid = Convert.ToUInt16(new Random().Next(0, 21) <= 1 ? (short)0 : (short)1),
        EventCode = 4,

        Min = valueMax,
        Max = valueMax,
        Avg = valueMax,
        Header = new DTO.External.Adapter.DCHeaderExt()
        {
          MessageId = 3001,
          TimeStamp = DateTime.Now.ToString(),
          Counter = 1,
        }
      };
      _l1SimulationManager.SendTelegramToTcpProxy(dcMeasurement);
    }


    internal void MakeFurnaceStep()
    {

      simulatedRawMaterialOnDischarge = materialsInFurnace[furnaceSize - 1].Value;
      for (int i = furnaceSize - 2; i >= 0; i--)
      {
        if (materialsInFurnace[i].Value.RawMaterialId != 0)
        {
          materialsInFurnace[i + 1] = new KeyValuePair<int, MVHRawMaterial>(i + 2, materialsInFurnace[i].Value);
          materialsInFurnace[i] = new KeyValuePair<int, MVHRawMaterial>(i + 1, new MVHRawMaterial());
        }
      }
    }

    internal async void TcpListener()
    {
      const int port = 5006;
      IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
      IPAddress ipAddress = ipHostInfo.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
      IPEndPoint endpoint= new IPEndPoint(ipAddress, port);

      System.Net.Sockets.TcpListener listener = new System.Net.Sockets.TcpListener(endpoint);
      _logger.Info("Listening on {0}: {1}", endpoint.Address.ToString(), endpoint.Port);
      listener.Start();

      while (true)
      {
        //---incoming client connected---
        System.Net.Sockets.TcpClient client = await listener.AcceptTcpClientAsync();

        //---get the incoming data through a network stream---
        System.Net.Sockets.NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        //---read incoming stream---
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

        //---convert the data received into a string---
        DCPEBasIdExt dataReceived = (DTO.External.MVHistory.DCPEBasIdExt)SMF.Core.Helpers.ObjectDump.GetObjectFromBytes(buffer, typeof(DTO.External.MVHistory.DCPEBasIdExt));
        ProcessPEBaseIdTelegam(dataReceived);
      }
    }
  }
}
