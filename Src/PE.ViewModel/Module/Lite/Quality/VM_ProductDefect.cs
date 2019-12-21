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

namespace PE.HMIWWW.ViewModel.Module.Lite.Quality
{
  public class VM_ProductDefect: VM_Base
  {
    public long DefectId { get; set; }

    [SmfDisplay(typeof(VM_ProductDefect), "DefectCreated", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime DefectCreated { get; set; }
    public Nullable<long> ProductId { get; set; }
    [SmfDisplay(typeof(VM_ProductDefect), "DefectName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DefectName { get; set; }
    public string DefectDescription { get; set; }
    [SmfDisplay(typeof(VM_ProductDefect), "DefectCatalogueName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DefectCatalogueName { get; set; }
    public string DefectCatalogueCategoryName { get; set; }
    public Nullable<bool> IsProductRelated { get; set; }
    public string ProductName { get; set; }
    [SmfDisplay(typeof(VM_ProductDefect), "DefectCatalogueCode", "NAME_Code")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DefectCatalogueCode { get; set; }

    public VM_ProductDefect(V_DefectsSummary defect)
    {
      this.ProductId = defect.FKProductId;
      this.DefectId = defect.DefectId;
      this.DefectCreated = defect.CreatedTs;
      this.DefectCatalogueName = defect.DefectCatalogueName;
      this.DefectCatalogueCode = defect.DefectCatalogueCode;

      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
