using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMF.Module.Core;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using SMF.Core.Log;
using PE.TCP.Managers;

namespace PE.TCP.Managers
{
  public class AsyncSocketSender
  {
    // The port number for the remote device.
    private const int SleepPeriod = 10;
   // private int aliveCounter;
    private int telegramid;
    private int telIdCusotmer;
    private int telLength;
    private string ip;
    private int socket;
    private string descr;
    private int alive;
    private int alivecycle;
    private int aliveId;
    private int aliveLength;
    private int aliveOffset;
    private bool isConnected = false;
    private Socket client;
    private bool ifConnectCallBackIsOk = true;

    // ManualResetEvent instances signal completion.
    private ManualResetEvent connectDone = new ManualResetEvent(false);
    private ManualResetEvent sendDone = new ManualResetEvent(false);
    private ManualResetEvent receiveDone = new ManualResetEvent(false);

    // The response from the remote device.
    private static String response = String.Empty;

    public AsyncSocketSender(int telegramid, int telIdCustomer, int telLength, string ip, int socket, string descr, int alive, int alivecycle, int aliveId, int aliveLength, int aliveOffset)
    {
      this.telegramid = telegramid;
      this.telIdCusotmer = telIdCustomer;
      this.telLength = telLength;
      this.ip = ip;
      this.socket = socket;
      this.descr = descr;
      this.alive = alive;
      this.alivecycle = alivecycle;
      this.aliveOffset = aliveOffset;
      this.aliveId = aliveId;
      this.aliveLength = aliveLength;
    }

    public bool OpenConnection()
    {
      try
      {
        // Establish the remote endpoint for the socket.
        IPAddress[] ipAdd = Dns.GetHostAddresses(ip);
        IPAddress ipAddress = ipAdd[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, socket);
        ModuleController.Logger.Info("Initializing telegram {0}, remote address: {1}:{2}", telegramid, ip, socket);
        // Create a TCP/IP socket.
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // Connect to the remote endpoint.
        if (!isConnected)
        {
          bool wait = client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client).AsyncWaitHandle.WaitOne(10000);
        }
        if (ifConnectCallBackIsOk)
        {
          isConnected = true;
          return true;
        }
        else
        {
          isConnected = false;
          return false;
        }

      }
      catch (Exception ex)
      {
        //isConnected = false;
        //client.Disconnect(true);
        ModuleController.Logger.Error("Commincation error: unable to send data to {0} : {1}", ip, socket);
        throw new Exception($"Exception in AsynchronousSocketClient::OpenConnection, ex: {ex.Message}, inner: {ex.InnerException}");
      }
    }

    public void CloseConnection()
    {
      try
      {
        if (isConnected)
        {
          isConnected = false;
          client.Disconnect(true);
          ModuleController.Logger.Info($"Tcp connection was closed for ip: {ip} socket: {socket} telegramId: {telegramid}");
        }
        else
        {
          ModuleController.Logger.Warn($"Cannot close connection, propably connection was not opened: connectStatus: {isConnected}");
        }

      }
      catch (Exception ex)
      {
        ModuleController.Logger.Error($"Error during closing connection for telegramId: {telegramid} on ip: {ip}");
        LogHelper.LogException(ModuleController.Logger, ex, $"Error during closing connection for telegramId: {telegramid} on ip: {ip}");
        throw new Exception($"Exception in AsynchronousSocketClient::CloseConnection, ex: {ex.Message}, inner: {ex.InnerException}");
      }
    }
    private void ConnectCallback(IAsyncResult ar)
    {
      try
      {
        // Retrieve the socket from the state object.
        Socket client = (Socket)ar.AsyncState;

        // Complete the connection.
        client.EndConnect(ar);

        ModuleController.Logger.Info("Socket connected to {0}", client.RemoteEndPoint.ToString());

        // Signal that the connection has been made.
        connectDone.Set();
      }
      catch (Exception ex)
      {
        ifConnectCallBackIsOk = false;
        ModuleController.Logger.Error("Telegram {0}: Can not establish connection: {1}", telegramid.ToString(), ex.Message);
      }
    }

    private void Receive(Socket client)
    {
      try
      {
        // Create the state object.
        StateObject state = new StateObject();
        state.workSocket = client;

        // Begin receiving the data from the remote device.
        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ModuleController.Logger, ex, String.Format("Commincation error: receive data"));
      }
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
      try
      {
        // Retrieve the state object and the client socket 
        // from the asynchronous state object.
        StateObject state = (StateObject)ar.AsyncState;
        Socket client = state.workSocket;

        // Read data from the remote device.
        int bytesRead = client.EndReceive(ar);

        if (bytesRead > 0)
        {

          // Get the rest of the data.
          client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
        }
        else
        {
          // Signal that all bytes have been received.
          receiveDone.Set();
        }
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ModuleController.Logger, ex, String.Format("Callback exception"));
      }
    }

    public void Send(byte[] byteData)
    {
      // Convert the string data to byte data 
      //byte[] byteData = Encoding.Default.GetBytes(data);

      // Begin sending the data to the remote device.
      try
      {
        if (isConnected)
        {
          ModuleController.Logger.Info("Sending telegram {0}", telegramid);
          client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client).AsyncWaitHandle.WaitOne();
        }
        else
        {
          ModuleController.Logger.Error("Telegram {0} not sent! Channel is actually disconnected.", telegramid);

        }
      }
      catch (Exception ex)
      {
        throw new Exception($"Exception in AsynchronousSockenClient::Send, ex: {ex.Message}, inner: {ex.InnerException}");
      }

    }

    private void SendAlive(Socket client, int telLength, int aliveId, int aliveLength, int aliveOffset)
    {
      byte[] byteData = new byte[telLength];
      byte[] aliveIdLoc = new byte[10];
      aliveIdLoc = BitConverter.GetBytes(aliveId);
      for (int i = 0; i < aliveLength; i++)
      {
        if (i + aliveOffset < telLength)
          byteData[i + aliveOffset] = aliveIdLoc[i];
      }
      // Begin sending the data to the remote device.
      client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
    }

    private void SendCallback(IAsyncResult ar)
    {
      try
      {
        // Retrieve the socket from the state object.
        Socket client = (Socket)ar.AsyncState;

        // Complete sending the data to the remote device.
        int bytesSent = client.EndSend(ar);
        ModuleController.Logger.Info("Sent {0} bytes to server.", bytesSent);

        // Signal that all bytes have been sent.
        sendDone.Set();
      }
      catch (Exception ex)
      {
        ModuleController.Logger.Error($"Exception in AsynchronousSocketClient::SendCallBack, ex: {ex.Message}, inner: {ex.InnerException}");
        LogHelper.LogException(ModuleController.Logger, ex, String.Format("SendCallback exception"));
      }
    }
}
}
