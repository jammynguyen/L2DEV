using System;
using SMF.HMIWWW.Attributes;
using SMF.DbEntity.Model;
using PE.HMIWWW.Core.ViewModel;
using System.Collections.Generic;

namespace PE.HMIWWW.ViewModel.System
{
	public class VM_StringId : VM_Base
	{
        public VM_StringId(String id)
        {
            Id = id;
        }
        public VM_StringId()
        {
            Id = "";
        }

        #region properties	
        public virtual String Id { get; set; }
		#endregion
	}
}
