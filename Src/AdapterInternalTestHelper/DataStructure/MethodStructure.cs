using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterInternalTestHelper.DataStructure
{
  public class MethodStructure
  {
		public string MethodName { get; set; }
    public string InputDataType { get; set; }
    public string OutputDataType { get; set; }

		public MethodStructure()
    {

    }
    public MethodStructure(string methodName, string inputDataType, string outputDataType )
    {
      this.MethodName = methodName;
      this.InputDataType = inputDataType;
      this.OutputDataType = outputDataType;
    }
  }
}
