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

namespace PE.HMIWWW.ViewModel.Module.Lite.Steelgrade
{
  public class VM_SteelGroup : VM_Base
  {
    public long Id { get; set; }
    [SmfDisplay(typeof(VM_SteelGroup), "KeyName", "NAME_Name")]
    public string Name { get; set; }
    [SmfDisplay(typeof(VM_SteelGroup), "KeyDescription", "NAME_Description")]
    public string Description { get; set; }
    [SmfDisplay(typeof(VM_SteelGroup), "KeyCode", "NAME_Code")]
    public string Code { get; set; }
    [SmfDisplay(typeof(VM_SteelGroup), "KeySymbol", "NAME_Symbol")]
    public string Symbol { get; set; }
    [SmfDisplay(typeof(VM_SteelGroup), "KeyName", "NAME_IsDefault")]
    public bool IsDefault { get; set; }


    public VM_SteelGroup()
    {

    }

    public VM_SteelGroup(PRMSteelGroup sg)
    {
      Id = sg.SteelGroupId;
      Name = sg.SteelGroupName;
      Description = sg.SteelGroupDescription;
      Code = sg.SteelGroupCode;
      IsDefault = sg.IsDefault;

      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
