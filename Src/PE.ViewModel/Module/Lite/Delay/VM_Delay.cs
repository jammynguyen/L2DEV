using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.Core.Helpers;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Delay
{
  public class VM_Delay : VM_Base
  {
    public long Id { get; set; }

    [SmfDisplay(typeof(VM_Delay), "LastUpdateTs", "NAME_LastUpdate")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_Delay), "CreatedTs", "NAME_CreatedTs")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayStart", "NAME_DelayStart")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime DelayStart { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayEnd", "NAME_DelayEnd")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? DelayEnd { get; set; }

    [SmfDisplay(typeof(VM_Delay), "Comment", "NAME_Comment")]
    public string UserComment { get; set; }

    [SmfDisplay(typeof(VM_Delay), "IsPlanned", "NAME_IsPlanned")]
    public bool IsPlanned { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayCode", "NAME_Code")]
    public string DelayCatalogue { get; set; }
    [SmfDisplay(typeof(VM_Delay), "NewDelayLength", "NAME_NewDelayLength")]
    [SmfUnit("UNIT_Second")]
    public int? NewDelayLength { get; set; }
    [SmfDisplay(typeof(VM_Delay), "Duration", "NAME_Duration")]
    public TimeSpan? Duration { get; set; }

    [SmfDisplay(typeof(VM_Delay), "Duration", "NAME_Duration")]
    [SmfUnit("UNIT_Second")]
    public double? DurationInSeconds { get; set; }

    public long FkDelayCatalogueId { get; set; }
    public double? FkRawMaterialId { get; set; }
    public string FkUserId { get; set; }

    public VM_Delay()
    {

    }

    public VM_Delay(DLSDelay d)
    {
      Id = d.DelayId;
      LastUpdateTs = d.LastUpdateTs;
      CreatedTs = d.CreatedTs;
      DelayStart = d.DelayStart;
      DelayEnd = d.DelayEnd;
      IsPlanned = d.IsPlanned;
      UserComment = d.UserComment;

      DelayCatalogue = d.DLSDelayCatalogue?.DelayCatalogueCode;

      FkDelayCatalogueId = d.FKDelayCatalogueId;
      FkRawMaterialId = d.FKRawMaterialId;
      FkUserId = d.FKUserId;

      NewDelayLength = 0;

      if(d.DelayStart!=null && d.DelayEnd!=null)
      {
        Duration = d.DelayEnd - d.DelayStart;
        DurationInSeconds = Duration.Value.TotalSeconds;
      }

      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
