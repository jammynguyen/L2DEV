using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.Common
{
  public class Constants
  {
    /// <summary>
    /// This enum describes dictionary raw material property IDs stored in table PERawMaterialsData
    /// 
    /// Naming convention:
    /// 
    /// 1st byte defines type of the value (and target column in PERawMaterialsData: b: boolean/ValueBool, l: long/ValueLong, t: text/valueText (max 100 bytes), d: date time/ValueDateTime, f: double/ValueFloat
    /// 2nd byte defines whether this value is also to be stored in column in table: y: is going to be stored in PERawMaterialsStep table for or given step, n: value will be stored only in PERawMaterialsData as a property
    /// If 2nd byte is 'y' then name of the column in is taken from the enum definition starting from 3rd char till the end
    /// 
    /// Example:
    /// 
    /// fyDischargeTemp = 23
    /// Property is of a type double and will be stored in PERawMaterialsData with ValueID = 23 and value will be stored in column ValueFloat. Additionally it will be stored in table PERawMaterialsStep in column DischargeTemp
    /// </summary>
    public enum RawMaterialProperty : long
    {
      lyStatus = 1,
      lyFlagRejected = 2,
      lyShapeType = 3,
      fyWeight = 11,
      fyLength = 12,
      fyThickness = 13,
      fyWidth = 14,
      lyFurnaceId = 20,
      dyCharged = 21,
      dyDischarged = 22,
      fyDischargeTemp = 23,
      lyFlagHeating = 24,
      dyProduced = 30,
      lnTestProp1 = 1000,
      tnTestProp2 = 1001,
      dnTestProp3 = 1002,
      bnTestProp4 = 1003
    }

  }
}
