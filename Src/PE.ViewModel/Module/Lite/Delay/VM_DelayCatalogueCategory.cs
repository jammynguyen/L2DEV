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

namespace PE.HMIWWW.ViewModel.Module.Lite.Delay
{
  public class VM_DelayCatalogueCategory : VM_Base
  {
    public long Id { get; set; }
    [SmfDisplay(typeof(VM_DelayCatalogueCategory), "DelayCategoryName", "NAME_CategoryName")]
    public string DelayCategoryName { get; set; }
    [SmfDisplay(typeof(VM_DelayCatalogueCategory), "DelayCategoryCode", "NAME_CategoryCode")]
    public string DelayCategoryCode { get; set; }
    [SmfDisplay(typeof(VM_DelayCatalogueCategory), "DelayCategoryDescription", "NAME_CategoryDescription")]
    public string DelayCategoryDescription { get; set; }
  

    public VM_DelayCatalogueCategory()
    {

    }

    public VM_DelayCatalogueCategory(DLSDelayCatalogueCategory d)
    {
      Id = d.DelayCatalogueCategoryId;
    DelayCategoryName = d.DelayCatalogueCategoryName;
    DelayCategoryCode = d.DelayCatalogueCategoryCode;
    DelayCategoryDescription = d.DelayCatalogueCategoryDescription;
    
      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
