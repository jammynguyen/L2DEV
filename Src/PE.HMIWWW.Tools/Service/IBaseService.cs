using NLog;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.Core.Service
{
	public interface IBaseService
	{
		//void InitService(Logger Logger);
		void SendHmiOperationRequest(HmiInitiator hmiInitiator, string moduleName, int operation);
  }
}
