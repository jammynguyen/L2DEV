using PE.HMIWWW.Core.Resources;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.Core.ViewModel
{
	//public interface Ivm_Base
	//{
	//	bool ReadOperationOK { get; }
	//	string ReadOperationErrorText { get; }
	//	void SetError(string errorText);
	//	void ClearError();
	//	void Init();
	//}
	public class VM_Base//: Ivm_Base
	{
    public ModuleWarningMessage ModuleWarningMessage { get; private set; }
    protected void ConvertToLocal<T>(T objectData)
    {
      UnitConverterHelper.ConvertToLocal(objectData);
    }
    public void SetModuleWarningMessage(ModuleWarningMessage moduleWarningMessage)
    {
      ModuleWarningMessage = moduleWarningMessage;
    }
  }
}
