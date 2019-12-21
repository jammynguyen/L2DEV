using PE.DbEntity.Enums;
using PE.DTO.Internal.Adapter;
using SMF.Core.DC;
using SMF.Module.Core.Interfaces.Telegram;
using System.Runtime.Serialization;

namespace PE.DTO.External.DBAdapter
{
  public class DCWorkOrderStatusExt : BaseExternalTelegram
  {
    [DataMember]
    public long Counter { get; set; }

    [DataMember]
    public CommStatus Status { get; set; }

    [DataMember]
    public string BackMsg { get; set; }

    public override int ToExternal(DataContractBase dc)
    {
      Counter = (dc as DCWorkOrderStatus).Counter;
      Status = (dc as DCWorkOrderStatus).Status;
			BackMsg = (dc as DCWorkOrderStatus).BackMessage;
      return 0;
    }
  }
}
