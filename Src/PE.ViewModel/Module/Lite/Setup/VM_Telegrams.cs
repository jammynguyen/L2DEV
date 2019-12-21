using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Setup
{
  public class VM_Telegrams : VM_Base
  {
    [SmfDisplayAttribute(typeof(VM_Telegrams), "TelegramId", "NAME_TelegramId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? TelegramId { get; set; }

    [SmfDisplayAttribute(typeof(VM_Telegrams), "TelegramCode", "NAME_TelegramCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string TelegramCode { get; set; }

    [SmfDisplayAttribute(typeof(VM_Telegrams), "TelegramName", "NAME_TelegramName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string TelegramName { get; set; }
    [SmfDisplayAttribute(typeof(VM_Telegrams), "IPAddress", "NAME_IPAddress")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string IPAddress { get; set; }
    public short? Port { get; set; }
    [SmfDisplayAttribute(typeof(VM_Telegrams), "LastSent", "NAME_LastSent")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? LastSent { get; set; }

    #region ctor

    public VM_Telegrams()
    {

    }

    public VM_Telegrams(STPTelegram telegram)
    {
      TelegramId = telegram.TelegramId;
      TelegramCode = telegram.TelegramCode;
      TelegramName = telegram.TelegramName;
      IPAddress = telegram.TcpIp;
      Port = telegram.Port;
      LastSent = telegram.LastSent;
    }


    //public VM_Telegrams(V_STPTelegramStructures telegram)
    //{
    //	TelegramId = telegram.TelegramId;
    //	TelegramCode = telegram.TelegramCode;
    //	TelegramName = telegram.TelegramName;
    //}
    #endregion
  }
}
