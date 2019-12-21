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
  public class VM_TelegramLine : VM_Base
  {
    [SmfDisplayAttribute(typeof(VM_TelegramLine), "TelegramLineId", "NAME_TelegramLineId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? TelegramLineId { get; set; }

    [SmfDisplayAttribute(typeof(VM_TelegramLine), "LineCode", "NAME_LineCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string LineCode { get; set; }

    [SmfDisplayAttribute(typeof(VM_TelegramLine), "LineName", "NAME_LineName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string LineName { get; set; }

    [SmfDisplayAttribute(typeof(VM_TelegramLine), "LineDescription", "NAME_LineDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string LineDescription { get; set; }


    public long? FKStructureId {get; set;}


    public VM_TelegramLine()
    {

    }

    //public VM_TelegramLine(TelegramLine line)
    //{
    //  TelegramLineId = line.TelegramLineId;
    //  LineCode = line.LineCode;
    //  LineName = line.LineName;
    //  LineDescription = line.LineDescription;

    //  FKStructureId = line.FKTelegramStructureId;
    //}

  }
}
