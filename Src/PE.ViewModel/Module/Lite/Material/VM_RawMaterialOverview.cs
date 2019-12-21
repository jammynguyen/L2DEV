using PE.DbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.ComponentModel.DataAnnotations;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialOverview : VM_Base
  {
    [SmfDisplay(typeof(VM_RawMaterialOverview), "RawMaterialId", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "LastUpdateTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "IsDummy", "NAME_IsDummyMaterial")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "IsVirtual", "NAME_IsVirtual")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsVirtual { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "RawMaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "FKMaterialId", "NAME_FKMaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKMaterialId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "FKProductId", "NAME_FKProductId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKProductId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "ParentRawMaterial", "NAME_ParentName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParentRawMaterial { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "ExternalRawMaterialId", "NAME_ExternalId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public uint ExternalRawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "Status", "NAME_MaterialStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public virtual string Status { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "LastProcessingStepNo", "NAME_LastStepNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short LastProcessingStepNo { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "CuttingSeqNo", "NAME_CuttingSeqNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? CuttingSeqNo { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "ChildsNo", "NAME_ChildsNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? ChildsNo { get; set; }

    public bool AfterL3Assign { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialOverview), "Weight", "NAME_Weight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? Weight { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    public VM_RawMaterialMeasurements Measurements { get; set; }
    public VM_RawMaterialHistory History { get; set; }

    public VM_RawMaterialOverview(MVHRawMaterial material)
    {
      RawMaterialId = material.RawMaterialId;
      CreatedTs = material.CreatedTs;
      LastUpdateTs = material.LastUpdateTs;
      IsDummy = material.IsDummy;
      IsVirtual = material.IsVirtual;
      RawMaterialName = material.RawMaterialName;
      FKMaterialId = material.FKMaterialId;
      FKProductId = material.FKProductId;
      ParentRawMaterial = material.MVHRawMaterial1?.RawMaterialName;
      ExternalRawMaterialId = (uint)material.ExternalRawMaterialId;
      Status = ResxHelper.GetResxByKey(material.Status.ToString());
      LastProcessingStepNo = (short)material.LastProcessingStepNo;
      CuttingSeqNo = material.CuttingSeqNo;
      ChildsNo = material.ChildsNo;

      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_RawMaterialOverview(MVHRawMaterial material, V_RawMaterialMeasurements measurements, V_RawMaterialHistory history)
    //{
    //  RawMaterialId = material.RawMaterialId;
    //  CreatedTs = material.CreatedTs;
    //  LastUpdateTs = material.LastUpdateTs;
    //  IsDummy = material.IsDummy;
    //  IsVirtual = material.IsVirtual;
    //  RawMaterialName = material.RawMaterialName;
    //  FKMaterialId = material.FKMaterialId;
    //  FKProductId = material.FKProductId;
    //  ParentRawMaterial = material.MVHRawMaterial1?.RawMaterialName;
    //  ExternalRawMaterialId = (uint)material.ExternalRawMaterialId;
    //  Status = ResxHelper.GetResxByKey(material.Status.ToString());
    //  LastProcessingStepNo = (short)material.LastProcessingStepNo;
    //  CuttingSeqNo = material.CuttingSeqNo;
    //  ChildsNo = material.ChildsNo;

    //  if (measurements != null)
    //    Measurements = new VM_RawMaterialMeasurements(measurements);
    //  if (history != null)
    //    History = new VM_RawMaterialHistory(history);

    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    public VM_RawMaterialOverview(MVHRawMaterial material, bool afterL3Assign)
    {
      RawMaterialId = material.RawMaterialId;
      CreatedTs = material.CreatedTs;
      LastUpdateTs = material.LastUpdateTs;
      IsDummy = material.IsDummy;
      IsVirtual = material.IsVirtual;
      RawMaterialName = material.RawMaterialName;
      FKMaterialId = material.FKMaterialId;
      FKProductId = material.FKProductId;
      ParentRawMaterial = material.MVHRawMaterial1?.RawMaterialName;
      ExternalRawMaterialId = (uint)material.ExternalRawMaterialId;
      Status = ResxHelper.GetResxByKey(material.Status.ToString());
      LastProcessingStepNo = (short)material.LastProcessingStepNo;
      CuttingSeqNo = material.CuttingSeqNo;
      ChildsNo = material.ChildsNo;
      AfterL3Assign = afterL3Assign;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_RawMaterialOverview(V_RawMaterialOverview material)
    {
      RawMaterialId = material.RawMaterialId;
      CreatedTs = material.CreatedTs;
      LastUpdateTs = material.RawMaterialLastUpdated;
      IsVirtual = material.RawMaterialIsVirtual;
      RawMaterialName = material.RawMaterialName;
      ExternalRawMaterialId = (uint)material.RawMaterialExternalId;
      Weight = material.Asset29Weight;
      HeatName = material.HeatName;
      WorkOrderName = material.WorkOrderName;
      FKMaterialId = material.DimMaterialKey;


      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_RawMaterialOverview() { }
  }
}
