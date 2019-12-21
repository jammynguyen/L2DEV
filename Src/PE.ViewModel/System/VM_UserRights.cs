using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Model;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.System
{
	public class VM_UserRights : VM_Base
	{
		public VM_UserRights()
		{
		}
		public VM_UserRights(VM_UserRights orig)
		{
			if (orig != null)
			{
				Name = orig.Name;
				Method = orig.Method;
				AccessName = orig.AccessName;
				//Module = orig.Module;
			}
		}

		public VM_UserRights(AccessUnit au)
		{
			if (au != null)
			{
        Name = au.Name;
        Module = au.Module;
			}
		}
		[SmfDisplayAttribute(typeof(VM_UserRights), "Controller", "NAME_Controller")]
		public virtual String Name { get; set; }
		[SmfDisplayAttribute(typeof(VM_UserRights), "Method", "NAME_Method")]
		public virtual String Method { get; set; }
		[SmfDisplayAttribute(typeof(VM_UserRights), "AccessName", "NAME_AccessName")]
		public virtual String AccessName { get; set; }
		public virtual SMF.DbEntity.Model.Module Module { get; set; }

	}


}
