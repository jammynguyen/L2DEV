using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.Setup;
using SMF.Core.DC;
using SMF.Module.Core.Interfaces.Telegram;

namespace PE.DTO.External.Setup
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCCommonSetupStructureExt: BaseExternalTelegram
  {
    [DataMember]
    public int TelegramId { get; set; }
    [DataMember]
    public List<SetupProperty> SetupProperties { get; set; }


    public override int ToExternal(DataContractBase dc)
    {
      TelegramId = (dc as DCCommonSetupStructure).TelegramId;
      SetupProperties = new List<SetupProperty>();
      foreach(PE.DTO.Internal.Setup.SetupProperty sp in (dc as DCCommonSetupStructure).SetupProperties)
      {
        PE.DTO.External.Setup.SetupProperty property = new SetupProperty();
        property.PropertyId = sp.PropertyId;
        property.PropertyName = sp.PropertyName;
        property.PropertyType = sp.PropertyType;
        property.PropertyValue = sp.PropertyValue;

        SetupProperties.Add(property);
      }

      return 0;
    }
  }
  public class SetupProperty
  {
    [DataMember]
    public long PropertyId { get; set; }
    public string PropertyName { get; set; }
    public string PropertyValue { get; set; }
    public string PropertyType { get; set; }
  }
}
