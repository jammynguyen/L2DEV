using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUS.L1A.Managers.MillOrganization
{
  public class MeasuredValue
  {
    #region properties

    public double Value { get; private set; }
    public DateTime Date { get; private set; }
    public bool Valid { get; private set; }

    #endregion
    #region ctor

    public MeasuredValue(double value, DateTime date, bool valid=true)
    {
      Value = value;
      Date = date;
      Valid = valid;
    }

    #endregion
  }
}
