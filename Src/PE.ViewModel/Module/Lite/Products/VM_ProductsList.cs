using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Products
{
  public class VM_ProductsList : VM_Base
  {
    [SmfDisplay(typeof(VM_ProductsList), "ProductId", "NAME_ProductId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? ProductId { get; set; }


    [SmfDisplay(typeof(VM_ProductsList), "ShiftKey", "NAME_ShiftKey")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ShiftKey { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "ProductName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public string ProductName { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "RawMaterialWeight", "NAME_RawMaterialWeight")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double Weight { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "IsDummy", "NAME_IsDummyMaterial")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "IsAssigned", "NAME_IsAssigned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsAssigned { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "LastUpdateTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? LastUpdateTs { get; set; }

    public VM_ProductsList() { }
    

    public VM_ProductsList(V_Products data)
    {
      ProductId = data.ProductId;
      ShiftKey = data.ShiftKey;
      ProductName = data.ProductName;
      WorkOrderName = data.WorkOrderName;
      Weight = data.Weight;
      IsDummy = data.IsDummy;
      IsAssigned = data.IsAssigned;
      CreatedTs = data.CreatedTs;
      LastUpdateTs = data.LastUpdateTs;
    }
  }
}
