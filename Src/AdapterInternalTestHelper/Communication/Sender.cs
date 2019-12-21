using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AdapterInternalTestHelper.DataContractSample;
using PE.DTO.External.MVHistory;
using PE.Interfaces.Lite;
using SMF.Module.Core;
using SMF.Module.Core.Interfaces.Telegram;

namespace AdapterInternalTestHelper.Communication
{
  public static class Sender
  {
    public static string SelectedMethod = "";
    public static string SelectedDataToSend = "";
    public static SendOfficeResult<DCDefaultExt> exResDef;

    public static async Task<SendOfficeResult<DCDefaultExt>> SendData(string methodName, string parameterType, string inputDC, uint baseId)
    {
      Type myType = (typeof(IAdapter));
      MethodInfo[] AdapterMethodsList = myType.GetMethods();
			if(AdapterMethodsList != null)
      {
        IEnumerable<MethodInfo> SelectedMethodName = AdapterMethodsList.Where(z => z.Name == methodName);
				if(SelectedMethodName != null)
        {
          BaseExternalTelegram dcLocal = DCTestDataHelper.CreateSelectedDC(parameterType);
          Type type = dcLocal.GetType();
          exResDef = new SendOfficeResult<DCDefaultExt>();

          switch (type.Name)
          {
            case "DCL1CutDataExt":
              exResDef = await SendL1L1CutMessageAsync((DCL1CutDataExt)dcLocal, baseId);
              break;
            case "DCL1MaterialDivisionExt":
              exResDef = await Sender.SendL1MeasDataMessageAsync((DCMeasDataExt)dcLocal, baseId);
              break;
            case "DCMeasDataExt":
              exResDef = await Sender.SendL1MeasDataMessageAsync((DCMeasDataExt)dcLocal, baseId);
              break;
            case "DCL1ScrapDataExt":
              exResDef= await Sender.SendL1ScrapMessageAsync((DCL1ScrapDataExt)dcLocal, baseId);
              break;
          }

        }
      }
      return exResDef;
    }

    #region Initial data
    public static async Task<SendOfficeResult<DCPEBasIdExt>> SendL1MaterialIdRequestToAdapterAsync(DCL1BasIdRequestExt dataToSend)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);
      return await NewSendOfficeBase.HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.L1BaseIdRequestAsync(dataToSend));
    }
    public static async Task<SendOfficeResult<DCDefaultExt>> SendL1L1CutMessageAsync(DCL1CutDataExt dataToSend, uint baseId)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);
      dataToSend.BasId = baseId;
      dataToSend.CutLength = 99;
      dataToSend.LocationId = 8;
      dataToSend.TypeOfCut = 1;
      return await NewSendOfficeBase.HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.L1CutMessageAsync(dataToSend));
    }
    public static async Task<SendOfficeResult<DCPEBasIdExt>> SendL1MaterialIdRequestToAdapterAsync(DCL1MaterialDivisionExt dataToSend, uint baseId)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);
      dataToSend.ParentBasId = baseId;
      return await NewSendOfficeBase.HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.L1DivisionMessageAsync(dataToSend));
    }
    public static async Task<SendOfficeResult<DCDefaultExt>> SendL1MeasDataMessageAsync(DCMeasDataExt dataToSend, uint baseId)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);
      dataToSend.BasId = Convert.ToUInt16(baseId);
      dataToSend.BaseFeature = 0;
      dataToSend.EventCode = 20;
      dataToSend.Avg = 1200;
      dataToSend.PassNumber = 1;
      return await NewSendOfficeBase.HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.L1MeasDataMessageAsync(dataToSend));
    }
    public static async Task<SendOfficeResult<DCDefaultExt>> SendL1SampleMeasMessageAsync(DCMeasDataSampleExt dataToSend, uint baseId)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);
      dataToSend.BasId = Convert.ToUInt16(baseId);
      return await NewSendOfficeBase.HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.L1SampleMeasMessageAsync(dataToSend));
    }
    public static async Task<SendOfficeResult<DCDefaultExt>> SendL1ScrapMessageAsync(DCL1ScrapDataExt dataToSend, uint baseId)
    {
      string targetModuleName = PE.Interfaces.Module.Modules.Adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);
      dataToSend.BasId = Convert.ToUInt16(baseId);
      return await NewSendOfficeBase.HandleModuleSendMethod((IClientChannel)client, targetModuleName, () => client.L1ScrapMessageAsync(dataToSend));
    }


    #endregion

    #region Rest

    #endregion
  }
}
