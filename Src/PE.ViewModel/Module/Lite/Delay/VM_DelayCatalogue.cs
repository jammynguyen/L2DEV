using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.Core.Helpers;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Delay
{
  public class VM_DelayCatalogue: VM_Base
  {
    public long Id { get; set; }
    [SmfDisplay(typeof(VM_DelayCatalogue), "KeyName", "NAME_Name")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfRequired]
    public string DelayName { get; set; }
    [SmfDisplay(typeof(VM_DelayCatalogue), "LastUpdateTs", "NAME_LastUpdate")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? LastUpdateTs { get; set; }
    [SmfDisplay(typeof(VM_DelayCatalogue), "DelayCategory", "NAME_Category")]
    public string DelayCategory { get; set; }
    [SmfDisplay(typeof(VM_DelayCatalogue), "StdDelayTime", "NAME_StdDelayTime")]
    [SmfUnit("UNIT_Second")]
    [SmfRequired]
    public double? StdDelayTime { get; set; }
    [SmfDisplay(typeof(VM_DelayCatalogue), "AssetName", "NAME_AssetName")]
    public string AssetName { get; set; }
    [SmfDisplay(typeof(VM_DelayCatalogue), "IsActive", "NAME_IsActive")]
    public bool IsActive { get; set; }
    [SmfDisplay(typeof(VM_DelayCatalogue), "IsDefault", "NAME_IsDefault")]
    public bool IsDefault { get; set; }
    [SmfDisplay(typeof(VM_DelayCatalogue), "DelayCode", "NAME_Code")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfRequired]
    public string DelayCode { get; set; }
    [SmfDisplay(typeof(VM_DelayCatalogue), "DelayDescription", "NAME_Description")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string DelayDescription { get; set; }

    [SmfDisplay(typeof(VM_DelayCatalogue), "DelayCode", "NAME_Code")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string Code { get; set; }

    [SmfDisplay(typeof(VM_DelayCatalogue), "ParentDelayName", "NAME_ParentDelayName")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string ParentDelayName { get; set; }

    public double? FkDelayCatalogueCategoryId { get; set; }


    public long? ParentDelayCatalogueId { get; set; }

    public VM_DelayCatalogue()
    {

    }

    public VM_DelayCatalogue(DLSDelayCatalogue d)
    {
      Id = d.DelayCatalogueId;
      DelayName = d.DelayCatalogueName;
      LastUpdateTs = d.LastUpdateTs;

      DelayCategory = d.DLSDelayCatalogueCategory?
                       .DelayCatalogueCategoryName;

      StdDelayTime = d.StdDelayTime;
      
      IsActive = d.IsActive;
      IsDefault = d.IsDefault;
      //DelayCode = d.DLSDelayCatalogueCategory?
      //                 .DelayCatalogueCategoryCode;
      DelayCode = d.DelayCatalogueCode;
      DelayDescription = d.DelayCatalogueDescription;

      //Code = d.DelayCatalogueCode;

      FkDelayCatalogueCategoryId = d.FKDelayCatalogueCategoryId;

      ParentDelayCatalogueId = d.ParentDelayCatalogueId;

      ParentDelayName = d.DLSDelayCatalogue2?.DelayCatalogueName;

      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
