using PE.DbEntity.Models;
using PE.DTO.Internal.Setup;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PE.STP.Handlers
{
  public sealed class SetupTelegramsHandler
  {
    /// <summary>
    /// Get telegrams values for choosen telegram
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="telegramId"></param>
    /// <returns></returns>
    public List<V_TelegramValues> GetSetupTelegramStructureBySetupTelegramId(PEContext ctx, int telegramId)
    {
      if (ctx is null)
      {
        ctx = new PEContext();
      }

      if (telegramId <= 0)
      {
        throw new ArgumentException();
      }

      List<V_TelegramValues> result = null;

      result = ctx.V_TelegramValues.Where(w =>
                                          w.TelegramId == telegramId &&
                                          w.IsStructure == false)
                                   .OrderBy(o => o.Sort)
                                   .ToList();


      return result;

    }
    /// <summary>
    /// Update telegram value
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="dc"></param>
    /// <returns></returns>
    public STPTelegramValue UpdateTelegramValue(PEContext ctx, DCTelegramSetupValue dc)
    {
      if (ctx is null)
      {
        ctx = new PEContext();
      }

      if (dc.TelegramId <= 0)
      {
        throw new ArgumentException();
      }
      if (string.IsNullOrEmpty(dc.TelegramStructureIndex))
      {
        throw new ArgumentException();
      }
      STPTelegramValue result = null;
      result = ctx.STPTelegramValues.FirstOrDefault(x => x.FKTelegramId == dc.TelegramId && x.FKTelegramStructureIndex == dc.TelegramStructureIndex);
      if (result != null)
        result.Value = dc.Value;
      else //Create new value
      {

      }
      return result;

    }

    /// <summary>
    /// Create new version of choosen telegram
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="dc"></param>
    /// <returns></returns>
    public STPTelegram CreateNewVersionOfTelegram(PEContext ctx, DCTelegramSetupId dc)
    {

      if (ctx is null)
      {
        ctx = new PEContext();
      }
      if (dc.TelegramId <= 0)
      {
        throw new ArgumentException();
      }
      STPTelegram telegram = ExtractTelegram(ctx, dc.TelegramId);

      ctx.STPTelegrams.Add(telegram);

      return telegram;
    }

    private static STPTelegram ExtractTelegram(PEContext ctx, int telegramId)
    {
      STPTelegram telegram = ctx.STPTelegrams.AsNoTracking()
        .Include(x => x.STPTelegramValues)
        .FirstOrDefault(x => x.TelegramId == telegramId);


      List<string> name = ctx.STPTelegrams.AsNoTracking()
        .Where(x => x.TelegramName == telegram.TelegramName && x.TelegramCode.Contains("-"))
        .Select(x => x.TelegramCode).ToList();
      int versionNumber = 2;
      if (name.Count() > 0)
      {
        List<int> versionList = name.Select(x => int.Parse((x.Split('-')[1]))).ToList();
        versionNumber = versionList.Max(x => x) + 1;
        telegram.TelegramCode = telegram.TelegramCode.Split('-')[0];
      }


      telegram.TelegramCode = telegram.TelegramCode + "-" + versionNumber;
      return telegram;
    }

    public short GetPortNumber(List<V_TelegramValues> data)
    {
      if (data == null || data.Count < 1)
        return 0;
      return data.First().Port.Value;
    }

    public string GetIpAddress(List<V_TelegramValues> data)
    {
      if (data == null || data.Count < 1)
        return "";
      return data.First().TcpIp;
    }
    public DCCommonSetupStructure ConvertTelegramSetupDataToTransferObject(PEContext ctx, List<V_TelegramValues> data, int telegramId, bool clearValue = false)
    {
      if (data is null || data.Count == 0)
      {
        throw new ArgumentOutOfRangeException();
      }

      DCCommonSetupStructure dataContract = new DCCommonSetupStructure();
      dataContract.TelegramId = telegramId;
      dataContract.Port = GetPortNumber(data);
      dataContract.IpAddress = GetIpAddress(data);
      dataContract.SetupProperties = new List<SetupProperty>();


      foreach (V_TelegramValues element in data)
      {
        SetupProperty property = new SetupProperty();
        property.PropertyId = element.ElementId.Value;
        property.PropertyName = element.ElementCode;
        property.PropertyType = element.DataType;
        if (clearValue == false)
          property.PropertyValue = element.Value;
        else
          property.PropertyValue = string.Empty;

        dataContract.SetupProperties.Add(property);
      }

      return dataContract;
    }

    public STPTelegram UpdateSetupFromL1(PEContext ctx, DCCommonSetupStructure message)
    {
      if (ctx is null)
      {
        ctx = new PEContext();
      }
      if (message.TelegramId <= 0)
      {
        throw new ArgumentException();
      }
      STPTelegram telegram = ExtractTelegram(ctx, message.TelegramId);

      //todo update telegram element by L1 value

      //foreach (SetupProperty sp in message.SetupProperties)
      //{
      //  foreach (STPTelegramValue element in telegram.STPTelegramValues)
      //  {

      //    if (element. == sp.PropertyId)
      //      element.
      //  }

      //}


      ctx.STPTelegrams.Add(telegram);

      return telegram;

    }
  }
}
