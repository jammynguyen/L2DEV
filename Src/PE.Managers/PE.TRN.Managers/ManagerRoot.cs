using Ninject;
using PE.Interfaces.Interfaces.Handlers;
using PE.TRN.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.TRN.Managers
{
	public class ManagerRoot
	{
		#region handlers
		protected readonly IModuleAHandler _moduleAHandler;
		protected readonly IModuleBHandler _moduleBHandler;
		#endregion

		#region ctor
		public ManagerRoot()
		{
			using (IKernel kernel = new StandardKernel())
			{
				kernel.Bind<IModuleAHandler>().To<ModuleAHandler>(); //possible modification to ModuleA2Handler
				kernel.Bind<IModuleBHandler>().To<ModuleBHandler>(); //possible modification to ModuleB2Handler

				_moduleAHandler = kernel.Get<IModuleAHandler>();
				_moduleBHandler = kernel.Get<IModuleBHandler>();
			}

		}
		#endregion
	}
}
