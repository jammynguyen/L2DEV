using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Quality
{
  public class VM_ProductHistory : VM_Base
  {
    [SmfDisplay(typeof(VM_ProductHistory), "ProductId", "NAME_ProductId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long ProductId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ProductCreated", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime ProductCreated { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ProductName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public string ProductName { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "Weight", "NAME_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_ProductWeightKg")]
    public double Weight { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "QualityEnum", "NAME_Quality")]
    public short? QualityEnum { get; set; }

    public long WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "WorkOrderStatus", "NAME_OrderStatus")]
    public short? WorkOrderStatus { get; set; }

    public long ProductCatalogueId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ProductCatalogueName", "NAME_ProductCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueName { get; set; }

    public long FKShapeId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ShapeSymbol", "NAME_ShapeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ShapeSymbol { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ShapeName", "NAME_Shape")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ShapeName { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "Profile", "NAME_Profile")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Profile { get; set; } //artificially constructed out of thickness and width reference from Product Catalogue

    [SmfDisplay(typeof(VM_ProductHistory), "Thickness", "NAME_Thickness")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_ProductThickness")]
    [SmfFormat("FORMAT_Thickness")]
    
    public double? Thickness { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "Width", "NAME_Width")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Width")]
    [SmfFormat("FORMAT_Width")]
    public double? Width { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ProdCatRefWeight", "NAME_ProductWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_ProductWeightKg")]
    public double? ProdCatRefWeight { get; set; }

    public long SteelgradeId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "SteelgradeCode", "NAME_SteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }
    [SmfDisplay(typeof(VM_ProductHistory), "SteelgradeName", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }

    public long HeatId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "NumDefects", "NAME_NumberOfDefectsShort")]
    public long NumDefects { get; set; }

    public long ProductCatalogueTypeId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ProductCatalogueTypeSymbol", "NAME_ProductCatalogueType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueTypeSymbol { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ProductCatalogueTypeName", "NAME_ProductCatalogueType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueTypeName { get; set; }


    public VM_ProductHistory() { }
    public VM_ProductHistory(V_ProductionHistory r)
    {
      this.FKShapeId = r.FKShapeId;
      this.HeatId = r.HeatId;
      this.HeatName = r.HeatName;
      this.NumDefects = r.NumDefects ?? 0;
      this.ProdCatRefWeight = r.ProdCatRefWeight;
      this.ProductCatalogueId = r.ProductCatalogueId;
      this.ProductCatalogueName = r.ProductCatalogueName;
      this.ProductCatalogueTypeId = r.ProductCatalogueTypeId;
      this.ProductCatalogueTypeName = r.ProductCatalogueTypeName;
      this.ProductCatalogueTypeSymbol = r.ProductCatalogueTypeSymbol;
      this.ProductCreated = r.ProductCreated;
      this.ProductId = r.ProductId;
      this.ProductName = r.ProductName;
      this.QualityEnum = r.QualityEnum;
      this.ShapeName = r.ShapeName;
      this.ShapeSymbol = r.ShapeSymbol;
      this.SteelgradeCode = r.SteelgradeCode;
      this.SteelgradeId = r.SteelgradeId;
      this.SteelgradeName = r.SteelgradeName;
      this.Thickness = r.Thickness;
      this.Weight = r.Weight;
      this.Width = r.Width;
      this.WorkOrderId = r.WorkOrderId;
      this.WorkOrderName = r.WorkOrderName;
      this.WorkOrderStatus = (short)r.WorkOrderStatus;
      UnitConverterHelper.ConvertToLocal(this);
      this.Profile = GenerateProfileCode(this.Thickness, this.Width);
    }

		private string GenerateProfileCode(double? thickness, double? width)
    {
      StringBuilder sb = new StringBuilder("?");
			if (thickness != null)
      {
        sb.Clear();
        sb.Append(String.Format("{0:N1}", thickness));
        if (width != null)
        {
          sb.Append("x").Append(String.Format("{0:N1}", width));

        }
      }
			else
      {
        if (width != null)
        {
          sb.Append(String.Format("{0:N1}", width));
        }
      }

      return sb.ToString();
    }
  }
}
