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

namespace PE.HMIWWW.ViewModel.Module.Lite.Furnace
{
  public class VM_Furnace : VM_Base
  {
    public long? Sorting { get; set; }
    public long RawMaterialId { get; set; }
    public long? ExternalRawMaterialId { get; set; }
    [SmfDisplayAttribute(typeof(VM_Furnace), "RawMaterialName", "NAME_RawMaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialName { get; set; }
    [SmfDisplayAttribute(typeof(VM_Furnace), "Status", "NAME_Status")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short Status { get; set; }
    public long AssetId { get; set; }
    [SmfDisplayAttribute(typeof(VM_Furnace), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }
    [SmfDisplayAttribute(typeof(VM_Furnace), "Weigth", "NAME_Weight")]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? Weight { get; set; }
    [SmfDisplayAttribute(typeof(VM_Furnace), "Length", "NAME_Length")]
    [SmfUnit("UNIT_Length")]
    [SmfFormat("FORMAT_Length")]
    public double? Length { get; set; }
    public long? L3MaterialId { get; set; }
    [SmfDisplayAttribute(typeof(VM_Furnace), "MaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }
    [SmfDisplayAttribute(typeof(VM_Furnace), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }
    [SmfDisplayAttribute(typeof(VM_Furnace), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }
    [SmfDisplayAttribute(typeof(VM_Furnace), "SteelgradeCode", "NAME_SteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }
		public long? WorkOrderId { get; set; }
		public long? SteelGradeId { get; set; }
		public long? HeatId { get; set; }

    public PE.HMIWWW.ViewModel.Module.Lite.WorkOrder.VM_WorkOrderOverview WorkOrderOverview;
    public PE.HMIWWW.ViewModel.Module.Lite.Heat.VM_HeatOverview HeatOverview;


    public VM_Furnace(V_MaterialsInFurnace entity)
    {
      this.Sorting = entity.Sorting;
      this.RawMaterialId = entity.RawMaterialId;
      this.ExternalRawMaterialId = entity.ExternalRawMaterialId;
      this.RawMaterialName = entity.RawMaterialName;
      this.Status = entity.Status;
      this.AssetId = entity.AssetId;
      this.AssetName = entity.AssetName;
      this.Weight = entity.Weight;
      this.Length = entity.Length;
      this.L3MaterialId = entity.L3MaterialId;
      this.MaterialName = entity.MaterialName;
      this.WorkOrderName = entity.WorkOrderName;
      this.HeatName = entity.HeatName;
      this.SteelgradeCode = entity.SteelgradeCode;
      this.WorkOrderId = entity.WorkOrderId;
      this.SteelGradeId = entity.SteelgradeId;
      this.HeatId = entity.HeatId;

      UnitConverterHelper.ConvertToLocal(this);
    }
    public VM_Furnace()
    {

    }

  }
}
