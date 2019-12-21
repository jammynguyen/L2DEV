using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMI.Communication
{
	public static class SendOffice
	{
		#region ctor

		static SendOffice()
		{
			InterfaceHelper.Init();
		}

		#endregion
	}
}
