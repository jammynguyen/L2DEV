using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Model;
using SMF.DbEntity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMF.HMIWWW.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PE.HMIWWW.ViewModel.System
{
	public class VM_Right : VM_Base
	{
		public VM_Right()
		{
		}
		public VM_Right(RoleRight roleRight)
		{
			if (roleRight != null)
			{
				this.Id = roleRight.RoleRightId;
				this.RoleId = roleRight.RoleId;
				this.AccessUnitId = roleRight.AccessUnitId;
				this.Assigned = false;
				if (roleRight.AccessUnit != null)
				{
					this.Name = roleRight.AccessUnit.Name;
					//Description = roleRight.AccessUnit.Decription;
					this.PermissionType = (PermissionType)roleRight.PermissionType;
				}
			}
		}
		public VM_Right(AccessUnit au, string RoleId)
		{
			this.Id = "0";
			this.Name = au.Name;
			this.AccessUnitId = au.AccessUnitId;
			this.RoleId = RoleId;
			this.PermissionType = SMF.DbEntity.Enums.PermissionType.View;
		}

		[SmfDisplayAttribute(typeof(VM_Right), "Assigned", "NAME_Assigned")]
		public virtual bool Assigned { get; set; }
		[SmfDisplayAttribute(typeof(VM_Right), "Id", "NAME_Id")]
		public virtual string Id { get; set; }
		[SmfDisplayAttribute(typeof(VM_Right), "RoleId", "NAME_RoleId")]
		public virtual string RoleId { get; set; }
		[Editable(false)]
		[SmfDisplayAttribute(typeof(VM_Right), "AccessUnitId", "NAME_RoleAccessUnit")]
		public virtual Int64 AccessUnitId { get; set; }
		[SmfDisplayAttribute(typeof(VM_Right), "PermissionType", "NAME_Permission")]
		public virtual PermissionType PermissionType { get; set; }
		[Editable(false)]
		[SmfDisplayAttribute(typeof(VM_Right), "Name", "NAME_Name")]
		public virtual String Name { get; set; }
		[Editable(false)]
		[SmfDisplayAttribute(typeof(VM_Right), "Description", "NAME_Description")]
		public virtual String Description { get; set; }

		#region methods

		public void CopyToEntity(ref RoleRight right, bool copyIndex)
		{
			if (copyIndex)
			{
				right.RoleRightId = Id;
			}
			right.AccessUnitId = AccessUnitId;
			right.RoleId = RoleId;
			right.PermissionType = (short)PermissionType;
		}
		public void CopyToEntity(ref RoleRight right, string roleId, bool copyIndex)
		{
			if (copyIndex)
			{
				right.RoleRightId = Id;
			}
			right.AccessUnitId = AccessUnitId;
			right.RoleId = roleId;
			right.PermissionType = (short)PermissionType;
		}

		#endregion
	}
    public class VM_RightList : List<VM_Right>
    {
        public VM_RightList()
        {
        }
        public VM_RightList(IList<RoleRight> dbClass)
        {
            foreach (RoleRight item in dbClass)
            {
                this.Add(new VM_Right(item));
            }
        }
        public VM_RightList(IList<AccessUnit> dbClass, string RoleId)
        {
            foreach (AccessUnit item in dbClass)
            {
                this.Add(new VM_Right(item, RoleId));
            }
        }
    }

}
