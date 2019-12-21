using PE.DbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialFacts : VM_Base
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


    [SmfDisplay(typeof(VM_RawMaterialOverview), "RawMaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "Status", "NAME_MaterialStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public virtual string Status { get; set; }


    [SmfDisplay(typeof(VM_RawMaterialOverview), "L3MaterialName", "NAME_L3MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "MaterialId", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? MaterialId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "ProductId", "NAME_ProductId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? ProductId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "WorkOrderKeyId", "NAME_WorkOrderKey")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? WorkOrderKey { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "CutNo", "NAME_CutNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? CutNo { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "ChildsNo", "NAME_ChildsNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? ChildsNo { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "RawDeclaredWeight", "NAME_RawDeclaredWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? RawDeclaredWeight { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "RawDeclaredLength", "NAME_RawDeclaredLength")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? RawDeclaredLength { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "RawInitialWeight", "NAME_RawInitialWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? RawInitialWeight { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "RawInitialLength", "NAME_RawInitialLength")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? RawInitialLength { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "LastWeight", "NAME_LastWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? LastWeight { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "LastLength", "NAME_LastLength")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? LastLength { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "IsVirtual", "NAME_IsVirtual")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsVirtual { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "CrewName", "NAME_CrewName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string CrewName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "ProductCatalogueName", "NAME_ProductCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "MaterialCatalogueName", "NAME_MaterialCatalogueName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "ParentRawMaterial", "NAME_ParentRawMaterial")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParentRawMaterial { get; set; }

    public VM_RawMaterialFacts()
    {
    }

    public VM_RawMaterialFacts(V_RawMaterialOverview data)
    {
      RawMaterialId = data.DimMaterialKey;
      RawMaterialName = data.RawMaterialName;
      MaterialName = data.MaterialName;
      RawDeclaredLength = data.RawMaterialDeclaredLength;
      RawDeclaredWeight = data.RawMaterialDeclaredWeight;
      RawInitialLength = data.RawMaterialInitialLength;
      RawInitialWeight = data.RawMaterialInitialWeight;
      LastLength = data.RawMaterialLastLength;
      LastWeight = data.RawMaterialLastWeight;
      CutNo = data.RawMaterialCutNo;
      ChildsNo = data.RawMaterialChildNo;
      IsVirtual = data.RawMaterialIsVirtual;
      CrewName = data.CrewName;
      ProductCatalogueName = data.ProductCatalogueName;
      MaterialCatalogueName = data.MaterialCatalogueName;
      WorkOrderName = data.WorkOrderName;
      HeatName = data.HeatName;
      ParentRawMaterial = data.ParentRawMaterialName;


      UnitConverterHelper.ConvertToLocal(this);
      //Status = ResxHelper.GetResxByKey(data.Status.ToString());
    }
  }
}
