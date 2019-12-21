using PE.ModuleB.Communication;
using PE.DTO.Internal.System;
using SMF.Module.Core;
using SMF.Module.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PE.ModuleB.Module
{
    public static class TestHandler
    {
		#region properties
		public static int TimeoutMethodExecute = (int)ParameterController.GetParameter("TimeoutMethodExecute").ValueInt;
		#endregion
		internal static bool IncommingMessageHandler(string param)
        {
            Thread.Sleep(4000);
            if (param == null)
                return false;
            return true;
        }
        internal static async Task SendResults()
        {
            DCTestData dcData = new DCTestData();
						dcData.TestItem = "Message to be sent from module B to module A";

            Task<SendOfficeResult<DCTestData>> taskOnRemoteModule = SendOffice.SendMessageToA(dcData);
            // independent work which doesn't need the result of "SendOffice" can be done here

            for (int i = 0; i < 100; i++)	
            {
                Thread.Sleep(30);
                ModuleController.Logger.Info("Doing some work {0}", i);
            }

            //and now we call await on the task 
            SendOfficeResult<DCTestData> sendOfficeResult = null;
            if (await Task.WhenAny(taskOnRemoteModule, Task.Delay(TestHandler.TimeoutMethodExecute)) == taskOnRemoteModule)
            {
                // task completed within timeout
                sendOfficeResult = await taskOnRemoteModule;
                //use the result from "SendOffice"
                if (sendOfficeResult != null)
                {
                    if (sendOfficeResult.OperationSuccess)
                        ModuleController.Logger.Info("Module A returned: {0}", sendOfficeResult.DataConctract.TestItem);
                    else
                        ModuleController.Logger.Error("Error in module A");
                }
            }
            else
            {
                // timeout logic
                ModuleController.Logger.Error("Timeout");
            }
        }
    }
}
