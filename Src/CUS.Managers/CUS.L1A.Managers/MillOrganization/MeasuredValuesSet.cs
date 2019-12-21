using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUS.L1A.Managers.MillOrganization
{


  public class MeasuredValuesSet
  {
    #region properties

    public List<MeasuredValue> MeasuredValues { get; private set; }

    #endregion
    #region ctor

    public MeasuredValuesSet()
    {
      MeasuredValues = new List<MeasuredValue>();
    }

    #endregion
    #region public methods

    public void Reset()
    {
      MeasuredValues.Clear();
    }
    public void AddMeasuredValue(double measuredValue, DateTime date, bool isValid = true)
    {
      MeasuredValues.Add(new MeasuredValue(measuredValue, date, isValid));
    }
		public double GetMinValue()
    {
      return MeasuredValues.Min(x => Convert.ToDouble(x.Value));
    }
    public double GetMaxValue()
    {
      return MeasuredValues.Max(x => Convert.ToDouble(x.Value));
    }
    public double GetAvgValue()
    {
      return MeasuredValues.Average(x => Convert.ToDouble(x.Value));
    }

    #endregion
  }
}
