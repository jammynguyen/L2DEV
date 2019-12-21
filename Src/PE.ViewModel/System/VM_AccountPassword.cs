using System;
using SMF.HMIWWW.Attributes;
using SMF.DbEntity.Model;
using PE.HMIWWW.Core.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PE.HMIWWW.Core.Resources;

namespace PE.HMIWWW.ViewModel.System
{
	public class VM_AccountPassword : VM_Base
	{
		#region properties

		public virtual String UserName { get; set; }
		[SmfDisplayAttribute(typeof(VM_Account), "OldPassword", "NAME_OldPassword")]
		[Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
		[DataType(DataType.Password)]
		[StringLength(100, ErrorMessageResourceName = "FORM_ATTRIBUTE_WrongStringLength", ErrorMessageResourceType = typeof(VM_Resources), MinimumLength = 6)]
		public virtual String OldPassword { get; set; }
		[SmfDisplayAttribute(typeof(VM_Account), "Password", "NAME_Password")]
		[Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
		[DataType(DataType.Password)]
		[StringLength(100, ErrorMessageResourceName = "FORM_ATTRIBUTE_WrongStringLength", ErrorMessageResourceType = typeof(VM_Resources), MinimumLength = 6)]
		public virtual String Password { get; set; }
		[SmfDisplayAttribute(typeof(VM_Account), "ConfirmPassword", "NAME_PasswordConfirm")]
		[Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType =typeof(VM_Resources))]
		[DataType(DataType.Password)]
		[StringLength(100, ErrorMessageResourceName = "FORM_ATTRIBUTE_WrongStringLength", ErrorMessageResourceType = typeof(VM_Resources),MinimumLength = 6)]
		[Compare("Password", ErrorMessageResourceName = "FORM_ATTRIBUTE_PasswordsDoNotMatch", ErrorMessageResourceType = typeof(VM_Resources))]
		public virtual String ConfirmPassword { get; set; }
		
		#endregion
		#region ctor
		public VM_AccountPassword(string userName, string oldPassword, string password, string confirmPassword)
		{
			UserName = userName;
			OldPassword = oldPassword;
			Password = password;
			ConfirmPassword = confirmPassword;
		}
		public VM_AccountPassword(User user)
		{
			UserName = user.UserName;
			OldPassword = "";
			Password = user.PasswordHash;
			ConfirmPassword = "";	
		}
		public VM_AccountPassword()
		{
			UserName = "";
			OldPassword = "";
			Password = "";
			ConfirmPassword = "";
		}
		#endregion
	}
}
