using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PE.DTO;
using SMF.Module.Core.Interfaces.Telegram;

namespace AdapterInternalTestHelper.DataContractSample
{
  public static class DCTestDataHelper
  {
		public static BaseExternalTelegram CreateSelectedDC(string externalTelegramName)
    {
      BaseExternalTelegram dcLocal = null;
      Assembly assembly = Assembly.LoadFile(@"D:\source\repos\ProcessExpert\Src\AdapterInternalTestHelper\bin\Debug\PE.DTO.dll");
      foreach (Type type in assembly.GetTypes())
      {
        if(type.FullName == externalTelegramName)
        {
          return dcLocal = (BaseExternalTelegram)Activator.CreateInstance(type);
        }
      }
      return dcLocal;
    }
  }
}
