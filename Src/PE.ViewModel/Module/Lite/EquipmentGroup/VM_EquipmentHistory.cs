using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Core.HtmlHelpers;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.Resources;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_EquipmentHistory : VM_Base
  {
    public long EquipmentHistoryId { get; set; }
    public long EquipmentId { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "CreatedTs", "NAME_CreatedTs")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? Created { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "Updated", "NAME_UpdatedTs")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? Updated { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "EquipmentStatus", "NAME_Status")]
    public short EquipmentStatus { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "AccmuliatedWeight", "NAME_ActualAccumulatedWeight")]
    [SmfUnit("UNIT_MaintenanceWeight")]
    public double? AccmuliatedWeight { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "AccBilletCnt", "NAME_AccBilletCntShort")]
    public long AccBilletCnt { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "Remark", "NAME_Remark")]
    public string Remark { get; set; }


    public VM_EquipmentHistory()
    {
      this.Created = DateTime.Now;
    }

    public VM_EquipmentHistory(MNTEquipmentHistory p)
    {
      this.Created = p.CreatedTs;
      this.EquipmentHistoryId = p.EquipmentHistoryId;
      this.AccBilletCnt = p.MaterialsProcessed ?? 0;
      this.AccmuliatedWeight = p.WeightProcessed;
			this.EquipmentStatus = (short)p.EquipmentStatus;
      this.Updated = p.LastUpdateTs;
      this.Remark = p.Remark;
      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
