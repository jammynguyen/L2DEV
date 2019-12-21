using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Setup
{
	public class DCTelegramSetupValue : DataContractBase
	{
		[DataMember]
		public int TelegramId { get; set; }
		[DataMember]
		public string Value { get; set; }
		[DataMember]
		public string TelegramStructureIndex { get; set; }
	}
}
