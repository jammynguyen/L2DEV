using PE.DbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
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
  public class VM_MaterialOverview : VM_Base
  {
    [SmfDisplay(typeof(VM_MaterialOverview), "MaterialId", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? MaterialId { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "LastUpdateTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "IsDummy", "NAME_IsDummyMaterial")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "MaterialName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "FKWorkOrderId", "NAME_FKWorkOrderId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKWorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "FKMaterialCatalogueIdRef", "NAME_FKMaterialCatalogueIdRef")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKMaterialCatalogueIdRef { get; set; }

    //[SmfDisplay(typeof(VM_MaterialOverview), "FKMaterialCatalogueIdRef", "NAME_MaterialCatalogueName")]
    //[DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    //public string MaterialCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "IsAssigned", "NAME_IsAssigned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsAssigned { get; set; }

    public VM_RawMaterialFacts Rmf {get; set;}

    public VM_WorkOrderOverview WorkOrderOverview  { get; set; } 


    public VM_MaterialOverview(PRMMaterial material)
    {
      MaterialId = material.MaterialId;
      CreatedTs = material.CreatedTs;
      LastUpdateTs = material.LastUpdateTs;
      IsDummy = material.IsDummy;
      MaterialName = material.MaterialName;
      FKWorkOrderId = material.FKWorkOrderId;
      //FKMaterialCatalogueIdRef = material.FKMaterialCatalogueIdRef;
      //MaterialStatus = ResxHelper.GetResxByKey(material.MaterialStatus.ToString());
      IsAssigned = material.IsAssigned;

      WorkOrderName = material.PRMWorkOrder?.WorkOrderName;
      HeatName = material.PRMHeat?.HeatName;
      //MaterialCatalogueName = material.PRMMaterialCatalogue?.MaterialCatalogueName;
      if (material.PRMWorkOrder != null)
      {
        WorkOrderOverview = new VM_WorkOrderOverview(material.PRMWorkOrder);
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_MaterialOverview(PRMMaterial material, PRMWorkOrder woo)
    {
      MaterialId = material.MaterialId;
      CreatedTs = material.CreatedTs;
      LastUpdateTs = material.LastUpdateTs;
      IsDummy = material.IsDummy;
      MaterialName = material.MaterialName;
      //FKHeatId = material.FKHeatId;
      FKWorkOrderId = material.FKWorkOrderId;
      //FKMaterialCatalogueIdRef = material.FKMaterialCatalogueIdRef;
      //MaterialStatus = ResxHelper.GetResxByKey(material.MaterialStatus.ToString());
      IsAssigned = material.IsAssigned;
      WorkOrderName = material.PRMWorkOrder?.WorkOrderName;
      HeatName = material.PRMHeat?.HeatName;
      //MaterialCatalogueName = material.PRMMaterialCatalogue?.MaterialCatalogueName;
      WorkOrderOverview = new VM_WorkOrderOverview(woo);
      //Rmf = new VM_RawMaterialFacts(rmf);
      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_MaterialOverview()
    {
    }

    public VM_MaterialOverview(V_MaterialOverview material)
    {
      MaterialId = material.MaterialId;
      CreatedTs = material.CreatedTs;
      LastUpdateTs = material.LastUpdateTs;
      IsDummy = material.IsDummy;
      MaterialName = material.MaterialName;
      //Weight
      IsAssigned = material.IsAssigned;
      WorkOrderName = material.WorkOrderName;
      HeatName = material.HeatName;
      //RawMaterialName
    }

  }
}
