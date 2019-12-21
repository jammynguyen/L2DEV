using PE.DTO.Internal.TcpProxy;
using SMF.Core.Log;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PE.DTO.External.Adapter;
using SMF.Core.Helpers;
using PE.DTO.External.MVHistory;

namespace PE.TCP.Managers
{

  public class StateObject
  {
    // Client  socket.
    public Socket workSocket = null;
    // Size of receive buffer.
    public const int BufferSize = 1000;
    public int actualSize = 0;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];
  }


  public class AysncSocketListener
  {
    //???
    public static ManualResetEvent allDone = new ManualResetEvent(false);


    private int telegramid;
    private int telLength;
    private int socket;
    private string descr;
    private int alive;
    private int alivecycle;
    private Socket listener;
    private int aliveCounter;
    private int aliveId;
    private int aliveLength;
    private int aliveOffset;
    private const int SleepPeriod = 10;

    private bool isConnected = false;

    public AysncSocketListener(int telegramid, int telLength, int socket, string descr, int alive, int alivecycle, int aliveId, int aliveLength, int aliveOffset)
    {
      this.telegramid = telegramid;
      this.telLength = telLength;
      this.socket = socket;
      this.descr = descr;
      this.alive = alive;
      this.alivecycle = alivecycle;
      this.aliveOffset = aliveOffset;
      this.aliveId = aliveId;
      this.aliveLength = aliveLength;
    }

    /// <summary>
    /// start listening for specified socket (port) and for concrete telegram 
    /// </summary>
    public async void StartListening()
    {
      ModuleController.Logger.Info("Starting listener for id: {0}, socket: {1}, descr: {2}", telegramid, socket, descr);
      IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
      IPAddress ipAddress = ipHostInfo.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
      if (ipAddress == null)
      {
        ModuleController.Logger.Error("Can not find right address type, socket {0} is not intialized", socket);
        return;
      }
      IPEndPoint localEndPoint = new IPEndPoint(ipAddress, socket);

      // Create a TCP/IP socket.
      this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

      // Bind the socket to the local endpoint and listen for incoming connections.
      try
      {
        listener.Bind(localEndPoint);
        listener.Listen(100);
        ModuleController.Logger.Info("Telegram {0}: Waiting  for a connection...", telegramid);
        while (true)
        {
          // Set the event to nonsignaled state.
          allDone.Reset();
          listener.BeginAccept(new AsyncCallback(AcceptCallback), listener).AsyncWaitHandle.WaitOne();
          await Task.Delay(SleepPeriod);
          aliveCounter += SleepPeriod;

          if (isConnected && aliveCounter > alivecycle)
          {
            //disconnect channel
            ModuleController.Logger.Error("Telegram {0} timed out! Disconnecting client...", telegramid);
            isConnected = false;
            try
            {
              this.listener.Shutdown(SocketShutdown.Both);
            }
            catch
            {
              this.listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
            }
          }
        }

      }
      catch (Exception ex)
      {
        LogHelper.LogException(ModuleController.Logger, ex, String.Format("Commincation error: unable initialize listener"));
      }
    }

    public void AcceptCallback(IAsyncResult ar)
    {
      // Signal the main thread to continue.
      allDone.Set();
      Socket handler;
      Socket listener;

      // Get the socket that handles the client request.
      try
      {
        listener = (Socket)ar.AsyncState;
        handler = listener.EndAccept(ar);

        // Create the state object.
        StateObject state = new StateObject();
        state.workSocket = handler;

        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state).AsyncWaitHandle.WaitOne();
        this.isConnected = true;
        this.aliveCounter = 0;
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ModuleController.Logger, ex, String.Format("Commincation error: AcceptCallback failed"));
        return;
      }
      ModuleController.Logger.Info("Client connected on port {0}", ((IPEndPoint)handler.LocalEndPoint).Port.ToString());
    }


    public void ReadCallback(IAsyncResult ar)
    {
      // Retrieve the state object and the handler socket
      // from the asynchronous state object.
      StateObject state = (StateObject)ar.AsyncState;
      Socket handler = state.workSocket;
      bool connected = handler.Available != 0 || !handler.Poll(1, SelectMode.SelectRead);
      if (!connected)
      {
        ModuleController.Logger.Info("Client disconnected on port: {0} ", ((IPEndPoint)handler.LocalEndPoint).Port.ToString());
        this.aliveCounter = 0;
        state.actualSize = 0;
        handler.Close();
        return;
      }

      // Read data from the client socket. 
      int bytesRead = handler.EndReceive(ar);
      state.actualSize += bytesRead;
      ModuleController.Logger.Info(" Read {0} bytes from socket. Actual size {1}", bytesRead, state.actualSize);
      int telId;
      //int j;
      //int i;
      byte[] singleTelbuff = new byte[this.telLength];

      if (state.actualSize >= this.telLength)
      {
        telId = ExtractTelegramByHeader(state.buffer, this.aliveId, this.aliveOffset, this.aliveLength);
      }
    }

    /// <summary>
    /// Decode hradef of telegram and from information from it create specified object from bytes pack
    /// Push it to ADP
    /// </summary>
    /// <param name="buffer">received buffer</param>
    /// <param name="aliveId"></param>
    /// <param name="aliveOffset">offset to read header</param>
    /// <param name="aliveLength">header length</param>
    /// <returns></returns>
    private int ExtractTelegramByHeader(byte[] buffer, int aliveId, int aliveOffset, int aliveLength)
    {
      int telId = -999; //wrong telId
      byte[] telIdChar = new byte[aliveLength];
      for (int i = 0; i < aliveLength; i++)
      {
        telIdChar[i] = buffer[i + aliveOffset];
      }
      try
      {
        DCHeaderExt header = (DCHeaderExt)ObjectDump.GetObjectFromBytes(telIdChar, typeof(DCHeaderExt));
        
        switch (header.MessageId)
        {
          case 1000:
            {
              DCL1BasIdRequestExt tel = (DCL1BasIdRequestExt)ObjectDump.GetObjectFromBytes(buffer, typeof(DCL1BasIdRequestExt));
              SendOffice.SendBasIdRequestToAdapter(tel);
              break;
            }
          case 2001:
            {
              DCL1CutDataExt tel = (DCL1CutDataExt)ObjectDump.GetObjectFromBytes(buffer, typeof(DCL1CutDataExt));
              SendOffice.SendCutDataToAdapter(tel);
              break;
            }
          case 2002:
            {
              DCL1MaterialDivisionExt tel = (DCL1MaterialDivisionExt)ObjectDump.GetObjectFromBytes(buffer, typeof(DCL1MaterialDivisionExt));
              SendOffice.SendMaterialDivisionToAdapter(tel);
              break;
            }
          case 2003:
            {
              DCL1ScrapDataExt tel = (DCL1ScrapDataExt)ObjectDump.GetObjectFromBytes(buffer, typeof(DCL1ScrapDataExt));
              SendOffice.SendScrapDataToAdapter(tel);
              break;
            }
          case 2004:
            {
              DCL1MaterialFinishedExt tel = (DCL1MaterialFinishedExt)ObjectDump.GetObjectFromBytes(buffer, typeof(DCL1MaterialFinishedExt));
              SendOffice.SendFinishedSignalToAdapter(tel);
              break;
            }
          case 3001:
            {
              DCMeasDataExt tel = (DCMeasDataExt)ObjectDump.GetObjectFromBytes(buffer, typeof(DCMeasDataExt));
              SendOffice.SendMeasDataToAdapter(tel);
              break;
            }
          case 3002:
            {
              DCMeasDataSampleExt tel = (DCMeasDataSampleExt)ObjectDump.GetObjectFromBytes(buffer, typeof(DCMeasDataSampleExt));
              SendOffice.SendMeasDataSampleToAdapter(tel);
              break;
            }

        }
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ModuleController.Logger, ex, String.Format("Convert error"));
      }
      return telId;
    }

  }
}
