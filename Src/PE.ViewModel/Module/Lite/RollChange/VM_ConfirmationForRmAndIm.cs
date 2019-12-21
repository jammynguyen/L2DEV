using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollChange
{
  public class VM_ConfirmationForRmAndIm : VM_Base
  {
    public short? OperationType { get; set; }

    [SmfDisplayAttribute(typeof(VM_ConfirmationForRmAndIm), "RollsetId", "NAME_RollsetId")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public long? RollsetId { get; set; }

    [SmfDisplayAttribute(typeof(VM_ConfirmationForRmAndIm), "RollsetName", "NAME_RollSetName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string RollsetName { get; set; }

    [SmfDisplayAttribute(typeof(VM_ConfirmationForRmAndIm), "MountedRollsetId", "NAME_MountedRollsetId")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public long? MountedRollsetId { get; set; }

    [SmfDisplayAttribute(typeof(VM_ConfirmationForRmAndIm), "MountedRollsetName", "NAME_MountedRollSetName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string MountedRollsetName { get; set; }

    [SmfDisplayAttribute(typeof(VM_ConfirmationForRmAndIm), "Position", "NAME_Position")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short? Position { get; set; }

    [SmfDisplayAttribute(typeof(VM_ConfirmationForRmAndIm), "StandNo", "NAME_StandNo")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short? StandNo { get; set; }
  }
}
