using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;

namespace PE.STP.Handlers
{
  public sealed class SetupTelegramBinaryHandler
  {

    /// <summary>
    /// From given list create byte[] stream
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public byte[] ConvertTelegramSetupDataToByteArray(PEContext ctx, List<V_TelegramValues> data)
    {
      if (data is null || data.Count == 0)
      {
        throw new ArgumentOutOfRangeException();
      }

      byte[] result = new byte[0];

      foreach (V_TelegramValues element in data)
      {
        byte[] bytes;
        //convert from string to propper value
        switch (element.DataType)
        {
          case "BOOL":
            {
              bool value = element.Value.Equals("1") ? true : false;
              bytes = BitConverter.GetBytes(value);
              break;
            }
          case "INT":
            {
              short value = Int16.Parse(element.Value);
              bytes = BitConverter.GetBytes(value);
              break;
            }
          case "REAL":
            {
              float value = float.Parse(element.Value, CultureInfo.InvariantCulture.NumberFormat);
              bytes = BitConverter.GetBytes(value);
              break;
            }
          case "STRING":
            {
              bytes = Encoding.ASCII.GetBytes(element.Value);
              break;
            }
          case "DINT":
            {
              int value = Int32.Parse(element.Value);
              bytes = BitConverter.GetBytes(value);
              break;
            }
          default:
            {
              throw new NotImplementedException();
            }
        }
        result = addByteToArray(result, bytes);
      }

      return result;

    }

    private byte[] addByteToArray(byte[] result, byte[] bytes)
    {
      int newSize = result.Length + bytes.Length;
      MemoryStream ms = new MemoryStream(new byte[newSize], 0, newSize, true, true);
      ms.Write(result, 0, result.Length);
      ms.Write(bytes, 0, bytes.Length);
      byte[] merged = ms.GetBuffer();

      return merged;

    }
  }
}
