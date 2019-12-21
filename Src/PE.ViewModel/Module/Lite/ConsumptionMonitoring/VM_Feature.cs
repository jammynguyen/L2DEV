using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.ConsumptionMonitoring
{
  public class VM_Feature : VM_Base
  {
    [SmfDisplay(typeof(VM_Feature), "FeatureId", "NAME_FeatureId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FeatureId { get; set; }

    [SmfDisplay(typeof(VM_Feature), "FeatureCode", "NAME_FeatureCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FeatureCode { get; set; }

    [SmfDisplay(typeof(VM_Feature), "FeatureName", "NAME_FeatureName")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public string FeatureName { get; set; }

    [SmfDisplay(typeof(VM_Feature), "FeatureUnit", "NAME_FeatureUnitSymbol")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public string FeatureUnitSymbol { get; set; }

    public VM_Feature() { }

    public VM_Feature(V_FeaturesMap data)
    {
      FeatureId = data.FeatureId;
      FeatureCode = data.FeatureCode;
      FeatureName = VM_Resources.ResourceManager.GetString(string.Format("FEATURE_Code_{0}", data.FeatureCode));
      FeatureUnitSymbol = data.UnitSymbol;

      ConvertToLocal(this);
    }
  }
}
