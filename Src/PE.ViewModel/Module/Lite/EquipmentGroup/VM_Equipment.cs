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
  public class VM_Equipment : VM_Base
  {
    public long EquipmentId { get; set; }

    public long EquipmentGroupId { get; set; } //FK

    [SmfDisplay(typeof(VM_Equipment), "EquipmentCode", "NAME_EquipmentCode")]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string EquipmentCode { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "EquipmentName", "NAME_EquipmentName")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string EquipmentName { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "EquipmentDescription", "NAME_Description")]
    [StringLength(100, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string EquipmentDescription { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "EquipmentGroupCode", "NAME_EquipmentGroup")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string EquipmentGroupCode { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "EquipmentStatus", "NAME_Status")]
    public short? EquipmentStatus { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "ActualValue", "NAME_ActualAccumulatedWeight")]
    [SmfUnit("UNIT_MaintenanceWeight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    public double? ActualValue { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "AlarmValue", "NAME_AccWeightLimit")]
    [SmfUnit("UNIT_MaintenanceWeight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    public double? AlarmValue { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "WarningValue", "NAME_TotalWeightWarningLevelShort")]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    public double? WarningValue { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "CreatedTs", "NAME_CreatedTs")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? Created { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "Updated", "NAME_UpdatedTs")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? Updated { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "NextServiceDate", "NAME_NextServiceDate")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? ServiceExpires { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "AccBilletCnt", "NAME_AccBilletCntShort")]
    public long AccBilletCnt { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "Remark", "NAME_Remark")]
    [StringLength(200, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string Remark { get; set; }


    public VM_Equipment()
    {
      this.Created = DateTime.Now;
      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_Equipment(MNTEquipment p)
    {
      this.Created = p.CreatedTs;
      this.AccBilletCnt = p.CntMatsProcessed ?? 0;
      this.ActualValue = p.ActualValue;
      this.AlarmValue = p.AlarmValue;
      this.WarningValue = p.WarningValue;
      this.EquipmentCode = p.EquipmentCode;
      this.EquipmentDescription = p.EquipmentDescription;
      this.EquipmentId = p.EquipmentId;
      this.EquipmentName = p.EquipmentName;
      //this.EquipmentStatus = ResxHelper.GetResxByKey(p.EquipmentStatus.ToString());
      this.EquipmentStatus = (short)p.EquipmentStatus;
      this.ServiceExpires = p.ServiceExpires;
      this.Updated = p.LastUpdateTs;
      this.EquipmentGroupCode = p.MNTEquipmentGroup.EquipmentGroupCode;
      this.EquipmentGroupId = p.FKEquipmentGroupId;
      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
