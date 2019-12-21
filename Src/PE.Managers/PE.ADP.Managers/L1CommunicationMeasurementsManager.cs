using PE.ADP.Handlers;
using PE.DTO.External.MVHistory;
using PE.DTO.Internal.MVHistory;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Managers;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PE.ADP.Managers
{
  public class L1CommunicationMeasurementsManager: IL1CommunicationMeasurementsManager
  {
    #region members

    private IAdapterL1MvSendOffice _sendOffice;

    #endregion

    private static List<DCMeasDataSample> transferList;
    private static Queue<DCMeasDataSample> waitingMeasurements;
    public static Stopwatch stopwatch;

    public L1CommunicationMeasurementsManager(IAdapterL1MvSendOffice sendOffice)
    {
      _sendOffice = sendOffice;

      transferList = new List<DCMeasDataSample>();
      waitingMeasurements = new Queue<DCMeasDataSample>();
      stopwatch = new Stopwatch();
      stopwatch.Start();
    }

    public virtual async Task<DataContractBase> ProcessMeasurementMessage<T>(T message)
    {
      DataContractBase result = new DataContractBase();

      // Reset stopwatch
      stopwatch.Stop();

      // Console info ---------
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.Write("\nTransferList contains " + transferList.Count + " elements");
      Console.ResetColor();
      // Console info end -----

      if (transferList.Count < Properties.SettingsAdapter.Default.ParcelSize)
      {
        lock (transferList)
        {
          if (typeof(T) == typeof(DCMeasDataSampleExt))
          {
            transferList.Add(L1CommunicationMeasurementsHandler.ConvertSampledMeasurementToInternal((DCMeasDataSampleExt)(object)message));
          }
          else
          {
            transferList.Add(L1CommunicationMeasurementsHandler.ConvertNonSampledMeasurementToInternal((DCMeasDataExt)(object)message));
          }
        }
      }
      else
      {
        // Add to queue
        lock (waitingMeasurements)
        {
          if (typeof(T) == typeof(DCMeasDataSampleExt))
          {

            waitingMeasurements.Enqueue(L1CommunicationMeasurementsHandler.ConvertSampledMeasurementToInternal((DCMeasDataSampleExt)(object)message));
          }
          else
          {

            waitingMeasurements.Enqueue(L1CommunicationMeasurementsHandler.ConvertNonSampledMeasurementToInternal((DCMeasDataExt)(object)message));
          }
        }

        SendToMvArchive();
      }

      stopwatch.Restart();

      return result;
    }

    /// <summary>
    /// Move elements from queue to transfer list
    /// </summary>
    private static void MoveFromQueueToTransferList()
    {
      lock (waitingMeasurements)
      {
        lock (transferList)
        {
          for (int i = 0; i < Properties.SettingsAdapter.Default.ParcelSize; i++)
          {
            if (waitingMeasurements.Count != 0)
            {
              transferList.Add(waitingMeasurements.Dequeue());
            }
            else
            {
              break;
            }
          }
        }
      }
    }

    private  bool SendToMvArchive()
    {
      bool operationResult = false;
      stopwatch.Stop();
      if (transferList != null && transferList.Count > 0)
      {

        lock (transferList)
        {
          DCMeasDataParcel dataToSend = new DCMeasDataParcel() { Measurements = transferList };

          _sendOffice.SendMeasurementsTelegramToMVH(dataToSend);
          //! two lines under to delete if uncomment rest
          transferList.Clear();
          MoveFromQueueToTransferList();
        }

      }
      stopwatch.Start();
      return operationResult;
    }
  }
}
