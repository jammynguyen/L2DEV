using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.Core.Helpers;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Defect
{
  public class VM_DefectCatalogueCategory : VM_Base
  {
    public long Id { get; set; }
    [SmfDisplay(typeof(VM_DefectCatalogueCategory), "DefectCategoryName", "NAME_CategoryName")]
    public string DefectCategoryName { get; set; }
    [SmfDisplay(typeof(VM_DefectCatalogueCategory), "DefectCategoryCode", "NAME_CategoryCode")]
    public string DefectCategoryCode { get; set; }
    [SmfDisplay(typeof(VM_DefectCatalogueCategory), "DefectCategoryDescription", "NAME_CategoryDescription")]
    public string DefectCategoryDescription { get; set; }


    public VM_DefectCatalogueCategory()
    {

    }

    public VM_DefectCatalogueCategory(MVHDefectCatalogueCategory d)
    {
      Id = d.DefectCatalogueCategoryId;
      DefectCategoryName = d.DefectCatalogueCategoryName;
      DefectCategoryCode = d.DefectCatalogueCategoryCode;
      DefectCategoryDescription = d.DefectCatalogueCategoryDescription;

      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
