using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.Setup;

namespace CUS.L1A.Handlers
{
  public class SetupStructureHandler
  {
    public bool GetBoolValue(SetupProperty element)
    {
      return element.PropertyValue.Equals("1") ? true : false;
    }

    public Int16 GetIntValue(SetupProperty element)
    {
      return Int16.Parse(element.PropertyValue);
    }

    public float GetRealValue(SetupProperty element)
    {
      return float.Parse(element.PropertyValue, CultureInfo.InvariantCulture.NumberFormat);
    }

    public byte[] GetStringValue(SetupProperty element)
    {
      return Encoding.ASCII.GetBytes(element.PropertyValue); 
    }

    public Int32 GetDintValue(SetupProperty element)
    {
      return Int32.Parse(element.PropertyValue); ;
    }

    public void UpdateValue<T>(SetupProperty element, T value)
    {
      element.PropertyValue = value.ToString();
    }
  }
}
