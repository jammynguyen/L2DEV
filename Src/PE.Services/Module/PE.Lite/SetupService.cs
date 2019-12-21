using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.DTO.Internal.Setup;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Setup;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class SetupService : BaseService, ISetupService
  {
    public VM_Telegrams FindTelegram(long telegramId)
    {
      VM_Telegrams result = new VM_Telegrams();
      using (PEContext ctx = new PEContext())
      {
        STPTelegram telegram = ctx.STPTelegrams.Find(telegramId);
        result = new VM_Telegrams(telegram);
      }
      return result;
    }
    public DataSourceResult GetTelegramLineData(ModelStateDictionary modelState, DataSourceRequest request, long telegramId)
    {
      using (PEContext ctx = new PEContext())
      {
        STPTelegram telegram = ctx.STPTelegrams.Find(telegramId);
        //var result = ctx.V_STPTelegramValues.Where(x => x.TelegramCode == telegram.TelegramCode || x.TelegramStructureIndex == telegram.FKTelegramStructureId.ToString()).OrderBy(x=>x.Sort).ToList();
        IQueryable<V_TelegramValues> result = ctx.V_TelegramValues
          .Where(x => x.TelegramId == telegram.TelegramId ||
          (x.TelegramStructureIndex.StartsWith(telegram.FKTelegramStructureId.ToString()) && x.IsStructure == true)).OrderBy(x => x.Sort).AsQueryable();
        if (!result.Any(x => x.DataType != "STRUCT"))
          result = Enumerable.Empty<V_TelegramValues>().AsQueryable();



        return result.ToDataSourceResult<V_TelegramValues, VM_TelegramStructure>(request, modelState, data => new VM_TelegramStructure(data));
      }
    }
    public DataSourceResult GetTelegramsSearchGridData(ModelStateDictionary modelState, DataSourceRequest request)
    {
      using (PEContext ctx = new PEContext())
      {
        IQueryable<STPTelegram> telegrams = ctx.STPTelegrams.OrderBy(x => x.TelegramName).AsQueryable();
        //var telegrams = ctx.STPTelegramStructures.Where(x => x.IsRoot);

        return telegrams.ToDataSourceResult<STPTelegram, VM_Telegrams>(request, modelState, data => new VM_Telegrams(data));
      }
    }
    #region module communication

    public async Task<VM_Base> SendTelegram(ModelStateDictionary modelState, long telegramId)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }
      //validate values

      DCTelegramSetupId dCTelegramSetup = new DCTelegramSetupId()
      {
        TelegramId = Convert.ToInt16(telegramId),
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendTelegramAsync(dCTelegramSetup);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }
    public async Task<VM_Base> UpdateValue(ModelStateDictionary modelState, VM_TelegramStructure model)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }
      //validate values
      try
      {
        switch (model.DataType)
        {
          case "BOOL":
            if (model.Value != "0" && model.Value != "1")
              model.Value = "0";
            break;
          case "INT":
            Int16.Parse(model.Value);
            break;
          case "DINT":
            Int32.Parse(model.Value);
            break;
          case "REAL":
            float.Parse(model.Value.Replace('.', ','));
            break;
        }
      }
      catch (Exception)
      {
        NotificationController.Error($"Operation failed - Value conversion failed!, method: {MethodBase.GetCurrentMethod().Name}");

      }

      DCTelegramSetupValue dCTelegramSetup = new DCTelegramSetupValue()
      {
        TelegramId = Convert.ToInt16(model.TelegramId),
        Value = model.Value,
        TelegramStructureIndex = model.TelegramStructureIndexString
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateTelegramValueAsync(dCTelegramSetup);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }
    public async Task<VM_Base> CreateNewVersion(ModelStateDictionary modelState, long telegramId)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }
      //validate values


      DCTelegramSetupId dCTelegramSetup = new DCTelegramSetupId()
      {
        TelegramId = Convert.ToInt16(telegramId),
      };


      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.CreateNewVersionOfTelegramAsync(dCTelegramSetup);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DeleteSetup(ModelStateDictionary modelState, long telegramId)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }
      //validate values


      DCTelegramSetupId dCTelegramSetup = new DCTelegramSetupId()
      {
        TelegramId = Convert.ToInt16(telegramId),
      };


      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteSetupAsync(dCTelegramSetup);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    #endregion




  }
}
