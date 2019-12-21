using PE.DTO;
using PE.DTO.Internal;
using PE.DTO.Internal.System;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces
{
	[ServiceContract(SessionMode = SessionMode.Allowed)]
	public interface IModuleA : IBaseModule
  {
    [OperationContract]
		[FaultContract(typeof(ModuleMessage))]
		Task<DCTestData> TestMethodOnModuleA(DCTestData message);
  }
}
