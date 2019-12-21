using NLog;
using PE.Interfaces.Interfaces;
using SMF.HMIWWW.Communication;
using System;

namespace PE.HMIWWW.Core.Communication
{
  public class HmiExternalAdapter: HmiExternalAdapterBase, IHmiWww
	{
		#region ctor

		public HmiExternalAdapter(Logger logger, Type type):base(logger,type)
		{ }

		#endregion	
	}
}
