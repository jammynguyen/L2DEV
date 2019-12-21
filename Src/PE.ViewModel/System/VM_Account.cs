using System;
using SMF.HMIWWW.Attributes;
using PE.HMIWWW.Core.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PE.HMIWWW.Core.Resources;
using SMF.DbEntity.Model;

namespace PE.HMIWWW.ViewModel.System
{
	public class VM_Account : VM_Base
	{
		#region properties

		public virtual String Id { get; set; }

		[SmfDisplayAttribute(typeof(VM_Account), "FirstName", "NAME_FirstName")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
    public virtual String FirstName { get; set; }

		[SmfDisplayAttribute(typeof(VM_Account), "UserName", "NAME_UserName")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
    public virtual String UserName { get; set; }

		[SmfDisplayAttribute(typeof(VM_Account), "LastName", "NAME_LastName")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
    public virtual String LastName { get; set; }

		//[SmfDisplayAttribute(typeof(VM_Account), "Password", "NAME_Password")]
		//[Required()]
		//public virtual String Password { get; set; }

		//[SmfDisplayAttribute(typeof(VM_Account), "ConfirmPassword", "NAME_PasswordConfirm")]
		//[Required()]
		//public virtual String ConfirmPassword { get; set; }

		[SmfDisplayAttribute(typeof(VM_Account), "JobPosition", "NAME_JobPosition")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public virtual String JobPosition { get; set; }

		[SmfDisplayAttribute(typeof(VM_Account), "LeftToRight", "NAME_LeftToRight")]
		public virtual bool LeftToRight { get; set; }

		[SmfDisplayAttribute(typeof(VM_Account), "RolesList", "NAME_RoleList")]
		[UIHint("RolesEditor")]
		public virtual List<VM_Role> RolesList { get; set; }

		[SmfDisplayAttribute(typeof(VM_Account), "Superuser", "NAME_Superuser")]
		public bool Superuser { get; set; }

		[SmfDisplayAttribute(typeof(VM_Account), "LeaderId", "NAME_Leader")]
		public virtual long? LeaderId { get; set; }

		[SmfDisplayAttribute(typeof(VM_Account), "FullName", "NAME_FullName")]
		public virtual String FullName { get; set; }

		[SmfDisplayAttribute(typeof(VM_Account), "LanguageId", "NAME_LanguageId")]
		public virtual long? LanguageId { get; set; }

		[SmfDisplayAttribute(typeof(VM_Account), "ReportUser", "NAME_ReportUser")]
		public virtual bool ReportUser { get; set; }

		[SmfDisplayAttribute(typeof(VM_Account), "Roles", "NAME_RoleList")]
		[UIHint("RolesEditor")]
		public virtual IEnumerable<TextLookupModel> Roles { get; set; }

		#endregion
		#region ctor

		public VM_Account()
		{
		}

		public VM_Account(User user)
		{
			Id = user.Id;
			FirstName = user.FirstName;
			LastName = user.LastName;;
			JobPosition = user.JobPosition;
      LeftToRight = (user.HMIViewOrientation == 1 ? true : false);
      //LeftToRight = user.HMIViewOrientation;
      LeaderId = user.LeaderId;
			//Password = user.PasswordHash;
			Roles = user.UserRoles.Select(t => new TextLookupModel(t.RoleId, t.Role.Name));
			FullName = user.FirstName + " " + user.LastName;
			LanguageId = user.LanguageId;
			ReportUser = Convert.ToBoolean(user.ReportUser);
			UserName = user.UserName;
		}

		#endregion
	}
}
