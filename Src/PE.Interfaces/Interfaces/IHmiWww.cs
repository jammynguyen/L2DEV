using System.ServiceModel;
using SMF.Core.Interfaces;

namespace PE.Interfaces.Interfaces
{
	[ServiceContract(SessionMode = SessionMode.Allowed)]
	public interface IHmiWww: IHmiWwwBase
	{
	}
}
