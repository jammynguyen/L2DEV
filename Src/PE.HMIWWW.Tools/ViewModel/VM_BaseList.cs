using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.Core.ViewModel
{
	public class VM_BaseList<T> : List<T>, Ivm_Base
	{
		#region ctor

		public VM_BaseList()
		{
			Init();
		}

		#endregion


		#region interface

		public bool ReadOperationOK { get; private set; }
		public string ReadOperationErrorText { get; private set; }
		public void SetError(String errorText)
		{
			ReadOperationOK = false;
			ReadOperationErrorText = errorText;
		}
		public void ClearError()
		{
			ReadOperationOK = true;
			ReadOperationErrorText = null;
		}
		public void Init()
		{
			ReadOperationOK = true;
			ReadOperationErrorText = string.Empty;
		}

		#endregion
	}
}
