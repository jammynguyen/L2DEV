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
	public class VM_UserInRoles : VM_Base
	{
		public VM_UserInRoles()
		{
		}
		public VM_UserInRoles(User user, Role role)
		{
			UserId = user.Id;
			UserName = user.UserName;
			RoleId = role.Id;
			Role = role.Name;
			IsAssignedToUser = user.UserRoles.Any(z => z.Role.Id == role.Id); 

		}
		public virtual string UserId { get; set; }
		[SmfDisplayAttribute(typeof(VM_UserInRoles), "User", "NAME_UserName")]
		public virtual string UserName { get; set; }
		[SmfDisplayAttribute(typeof(VM_UserInRoles), "Role", "NAME_Role")]
		public virtual string Role { get; set; }
		public virtual string RoleId { get; set; }
		[SmfDisplayAttribute(typeof(VM_UserInRoles), "IsAssigned", "NAME_Assigned")]
		public virtual Boolean IsAssignedToUser { get; set; }

	}

	//public class VM_UserInRolesList : VM_BaseList<VM_UserInRoles>
	//{
	//	public VM_UserInRolesList()
	//	{
	//	}
	//	public VM_UserInRolesList(User user, List<Role> rolesList)
	//	{
	//		foreach (Role item in rolesList)
	//		{
	//			this.Add(new VM_UserInRoles(user, item));
	//		}
	//	}
	//}
}
