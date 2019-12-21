using System;
using SMF.HMIWWW.Attributes;
using SMF.DbEntity.Model;
using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.Resources;

namespace PE.HMIWWW.ViewModel.System
{
	public class VM_Role : VM_Base
	{
		#region properties
		[Editable(false)]
		[SmfDisplayAttribute(typeof(VM_Role), "Id", "NAME_Id")]
		public virtual String Id { get; set; }

		[Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
		[SmfDisplayAttribute(typeof(VM_Role), "Name", "NAME_Name")]
		public virtual String Name { get; set; }

		[SmfDisplayAttribute(typeof(VM_Role), "Description", "NAME_Description")]
		public virtual String Description { get; set; }

		[SmfDisplayAttribute(typeof(VM_Role), "NumberOfUsers", "NAME_NumberOfUsers")]
		public virtual Int32 NumberOfUsers { get; set; }

		[SmfDisplayAttribute(typeof(VM_Role), "NumberOfPermissions", "NAME_NumberOfPermissions")]
		public virtual Int32 NumberOfPermissions { get; set; }
		#endregion

		#region ctor
		public VM_Role()
		{

		}
		public VM_Role(Role role)
		{
			this.Id = role.Id;
			this.Name = role.Name;
			this.Description = role.Description;
			this.NumberOfUsers = role.UserRoles.Count;
			this.NumberOfPermissions = role.RoleRights.Count;
		}
		#endregion
	}



}
