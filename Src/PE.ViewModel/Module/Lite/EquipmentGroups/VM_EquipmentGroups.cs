using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class EquipmentGroups : VM_Base
  {
    #region properties
		[SmfDisplayAttribute(typeof(EquipmentGroups), "EquipmentGroupId", "NAME_GroupDescriptionShort")]
    //[Required]
    public virtual long? EquipmentGroupId { get; set; }

    [SmfDisplayAttribute(typeof(EquipmentGroups), "EquipmentGroupCode", "NAME_GroupDescriptionShort")]
    public virtual string EquipmentGroupCode { get; set; }

		[SmfDisplayAttribute(typeof(EquipmentGroups), "EquipmentGroupName", "NAME_GroupDescriptionShort")]
    public virtual string EquipmentGroupName { get; set; }

		[SmfDisplayAttribute(typeof(EquipmentGroups), "EquipmentGroupDescription", "NAME_GroupDescriptionShort")]
    public virtual string EquipmentGroupDescription { get; set; }
    
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "GroupDescriptionSubGroup", "NAME_GroupDescriptionSubGroup")]
    //public virtual string GroupDescriptionSubGroup { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "EquipmentGroupIdSubGroup", "NAME_GroupDescriptionSubGroup")]
    ////[Required]
    //public virtual long? EquipmentGroupIdSubGroup { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "GroupDescriptionFamily", "NAME_GroupDescriptionFamily")]
    //public virtual string GroupDescriptionFamily { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "EquipmentGroupIdSubFamily", "NAME_GroupDescriptionFamily")]
    ////[Required]
    //public virtual long? EquipmentGroupIdSubFamily { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "EquipmentId", "NAME_Part")]
    ////[Required]
    //public virtual long? EquipmentId { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "EquipmentName", "NAME_Part")]
    ////[Required]
    //public virtual string EquipmentName { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "AccumulationEvent", "NAME_AccumulationEvent")]
    //public virtual short? AccumulationEvent { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "DescriptionDetails", "NAME_DescriptionDetails")]
    //public virtual string DescriptionDetails { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "Supplier", "NAME_Supplier")]
    //public virtual string Supplier { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "Status", "NAME_Status")]
    //public virtual short? Status { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "AccumulationType", "NAME_AccumulationType")]
    //public virtual short? AccumulationType { get; set; }
    //[SmfUnitAttribute("UNIT_MaintenanceWeight")]
    //[SmfFormatAttribute("FORMAT_Weight")]
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "TotalAccumulatedWeight", "NAME_TotalAccumulatedWeightShort")]
    //public virtual double? TotalAccumulatedWeight { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "TotalProcessedCounter", "NAME_TotalProcessedCounterShort")]
    //[SmfFormat("FORMAT_DefaultLong")]
    //public virtual long? TotalProcessedCounter { get; set; }
    //[SmfUnitAttribute("UNIT_MaintenanceWeight")]
    //[SmfFormatAttribute("FORMAT_Weight")]
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "TotalWeightWarningLevel", "NAME_TotalWeightWarningLevel")]
    //public virtual double? TotalWeightWarningLevel { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "TotalCounterWarningLevel", "NAME_TotalCounterWarningLevel")]
    //[SmfFormat("FORMAT_DefaultLong")]
    //public virtual long? TotalCounterWarningLevel { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "TotalTimeWariningLevel", "NAME_TotalTimeWariningLevelShort")]
    //public virtual DateTime? TotalTimeWariningLevel { get; set; }
    //[SmfUnitAttribute("UNIT_MaintenanceWeight")]
    //[SmfFormatAttribute("FORMAT_Weight")]
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "ActualAccumulatedWeight", "NAME_ActualAccumulatedWeightShort")]
    //public virtual double? ActualAccumulatedWeight { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "ActualProcessedCounter", "NAME_ActualProcessedCounter")]
    //[SmfFormat("FORMAT_DefaultLong")]
    //public virtual long? ActualProcessedCounter { get; set; }
    //[SmfUnitAttribute("UNIT_MaintenanceWeight")]
    //[SmfFormatAttribute("FORMAT_Weight")]
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "ActualWeightWarningLevel", "NAME_ActualWeightWarningLevelShort")]
    //public virtual double? ActualWeightWarningLevel { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "ActualCounterWarningLevel", "NAME_ActualCounterWarningLevelShort")]
    //[SmfFormat("FORMAT_DefaultLong")]
    //public virtual long? ActualCounterWarningLevel { get; set; }
    //[SmfDisplayAttribute(typeof(EquipmentGroups), "ActualTimeWarningLevel", "NAME_ActualTimeWariningLevelShort")]
    //public virtual DateTime? ActualTimeWarningLevel { get; set; }


    #endregion
    #region ctor
    public EquipmentGroups()
    {
      //this.EquipmentGroupId = 0;
    }
    public EquipmentGroups(int a /*VEquipmentGroupsAndDevice equipmentGroupdsEquipmentGroups*/)
    {
      //this.GroupDescription = equipmentGroupdsEquipmentGroups.GroupDescription;
      //if (equipmentGroupdsEquipmentGroups.EquipmentGroupId != null)
      //  this.EquipmentGroupId = equipmentGroupdsEquipmentGroups.EquipmentGroupId;
      //else
      //  this.EquipmentGroupId = 0;
      //this.GroupDescriptionSubGroup = equipmentGroupdsEquipmentGroups.GroupDescriptionSubGroup;
      //this.EquipmentGroupIdSubGroup = equipmentGroupdsEquipmentGroups.EquipmentGroupIdSubGroup;
      //this.GroupDescriptionFamily = equipmentGroupdsEquipmentGroups.GroupDescriptionFamily;
      //this.EquipmentGroupIdSubFamily = equipmentGroupdsEquipmentGroups.EquipmentGroupIdSubFamily;
      //this.EquipmentId = equipmentGroupdsEquipmentGroups.EquipmentId;
      //this.EquipmentName = equipmentGroupdsEquipmentGroups.EquipmentName;
      //this.AccumulationEvent = (short)equipmentGroupdsEquipmentGroups.AccumulationEvent;
      //this.DescriptionDetails = equipmentGroupdsEquipmentGroups.DescriptionDetails;
      //this.Supplier = equipmentGroupdsEquipmentGroups.Supplier;
      //this.Status = (short)equipmentGroupdsEquipmentGroups.Status;
      //this.AccumulationType = (short)equipmentGroupdsEquipmentGroups.AccumulationType;
      //this.TotalAccumulatedWeight = (double)equipmentGroupdsEquipmentGroups.TotalAccumulatedWeight;
      //this.TotalProcessedCounter = equipmentGroupdsEquipmentGroups.TotalProcessedCounter;
      //this.TotalWeightWarningLevel = (double)equipmentGroupdsEquipmentGroups.TotalWeightWarningLevel;
      //this.TotalCounterWarningLevel = equipmentGroupdsEquipmentGroups.TotalCounterWarningLevel;
      //this.TotalTimeWariningLevel = equipmentGroupdsEquipmentGroups.TotalTimeWariningLevel;
      //this.ActualAccumulatedWeight = (double)equipmentGroupdsEquipmentGroups.ActualAccumulatedWeight;
      //this.ActualProcessedCounter = equipmentGroupdsEquipmentGroups.ActualProcessedCounter;
      //this.ActualWeightWarningLevel = (double)equipmentGroupdsEquipmentGroups.ActualWeightWarningLevel;
      //this.ActualCounterWarningLevel = equipmentGroupdsEquipmentGroups.ActualCounterWarningLevel;
      //this.ActualTimeWarningLevel = equipmentGroupdsEquipmentGroups.ActualTimeWariningLevel;
      UnitConverterHelper.ConvertToLocal<EquipmentGroups>(this);
    }
    #endregion

  }
}
