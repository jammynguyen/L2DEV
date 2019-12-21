using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Defect
{
  public class VM_DefectCatalogue : VM_Base
  {
    public long DefectCatalogueId { get; set; }
    [SmfDisplay(typeof(VM_DefectCatalogue), "DefectCatalogueName", "NAME_Name")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfRequired]
    public string DefectCatalogueName { get; set; }
    [SmfDisplay(typeof(VM_DefectCatalogue), "DefectCatalogueCode", "NAME_Code")]
    [StringLength(4, MinimumLength = 3, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfRequired]
    public string DefectCatalogueCode { get; set; }
    [SmfDisplay(typeof(VM_DefectCatalogue), "DefectCatalogueCategory", "NAME_DefectCatalogueCategory")]
    public string DefectCatalogueCategory { get; set; }
    [SmfDisplay(typeof(VM_DefectCatalogue), "DefectCatalogueDescription", "NAME_DefectCategoryDescription")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string DefectCatalogueDescription { get; set; }
    [SmfDisplay(typeof(VM_DefectCatalogue), "IsDefault", "NAME_IsDefault")]
    public bool IsDefault { get; set; }
    public long DefectCatalogueCategoryId { get; set; }


    public VM_DefectCatalogue()
    {
      DefectCatalogueCategoryId = -1;
    }

    public VM_DefectCatalogue(MVHDefectCatalogue c)
    {
      DefectCatalogueId = c.DefectCatalogueId;
      DefectCatalogueName = c.DefectCatalogueName;
      DefectCatalogueCode = c.DefectCatalogueCode;
      DefectCatalogueCategory = c.MVHDefectCatalogueCategory?.DefectCatalogueCategoryCode;
      DefectCatalogueDescription = c.DefectCatalogueDescription;
      IsDefault = c.IsDefault;
      DefectCatalogueCategoryId = c.FKDefectCatalogueCategoryId;

      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
